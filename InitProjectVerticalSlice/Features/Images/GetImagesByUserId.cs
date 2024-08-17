using Carter;
using EventEchosAPI.Database;
using EventEchosAPI.Helpers;
using EventEchosAPI.Shared;
using FluentValidation;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using static EventEchosAPI.Extensions.ClaimExtensions;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Net;
using Microsoft.EntityFrameworkCore;
using EventEchosAPI.Contracts.Images;
using Microsoft.AspNetCore.Authorization;
using Mapster;

namespace EventEchosAPI.Features.Images
{
    public static class GetImagesByUserId
    {
        public sealed class Command : IRequest<List<ImageResponse>>
        {
            public string UserId { get; set; }
            public string EventDate { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                {
                    RuleFor(x => x.UserId)
                        .NotEmpty().WithMessage("User Id is required"); 
                    RuleFor(x => x.EventDate)
                        .NotEmpty().WithMessage("Event Date is required");
                }
            }
        }

        internal sealed class CommandHandler : IRequestHandler<Command, List<ImageResponse>>
        {
            private readonly ApplicationDbContext _dbcontext;
            private readonly IValidator<Command> _validator;
            public CommandHandler(ApplicationDbContext dbContext, IValidator<Command> validator)
            {
                _dbcontext = dbContext;
                _validator = validator;
            }

            public async Task<List<ImageResponse>> Handle(Command request, CancellationToken cancellationToken)
            {
                var result = _validator.Validate(request);
                if (!result.IsValid)
                {
                    throw new ValidationException(result.Errors);
                }

                var eventDate = DateTime.Parse(request.EventDate);
                // query with event id 
                var images = await _dbcontext.ImageDatas
                    .Where(x => x.UserId == request.UserId && x.CreatedDate.Date == eventDate.Date)
                    .ToListAsync(cancellationToken);

                return images.Select(image => new ImageResponse
                {
                    UploadDate = image.CreatedDate.ToString("dd-mm-yyyy"),
                    ImageId = image.ImageId,
                    ImageUrl = image.ImageUrl.Split("\\").Last()
                }).ToList();

                
            }
        }
    }

    public class GetImagesByUserIdEndpoint : ICarterModule
    {
        private readonly APIResponse _response;

        public GetImagesByUserIdEndpoint() => _response = new APIResponse();
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("api/images/get/by/event/user-id", /*[Authorize]*/ async (ImageRequest request, ISender sender) =>
            {
                try
                {
                    var command = request.Adapt<GetImagesByUserId.Command>();
                    var result = await sender.Send(command);
                    _response.IsSuccess = true;
                    _response.StatusCode = HttpStatusCode.OK;
                    _response.Result = new { images = result };
                    return Results.Ok(_response);
                }
                catch (ValidationException ex)
                {
                    var errorMesseges = ex.Errors.Select(error => $"{error.PropertyName}: {error.ErrorMessage}");
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.InternalServerError;
                    _response.ErrorMessage = errorMesseges.ToList();
                    return Results.BadRequest(_response);
                }
                catch (Exception ex)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.InternalServerError;
                    _response.Result = ex.Message;
                    return Results.BadRequest(_response);
                }
            }).WithTags("Images")
            .Produces<APIResponse>(StatusCodes.Status200OK)
            .Produces<APIResponse>(StatusCodes.Status400BadRequest);
        }
    }
}
