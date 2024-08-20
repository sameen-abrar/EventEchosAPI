using Carter;
using EventEchosAPI.Database;
using EventEchosAPI.Entities;
using EventEchosAPI.Helpers;
using EventEchosAPI.Shared;
using FluentValidation;
using MediatR;
using System.Drawing.Imaging;
using System.Drawing;
using System.Net;
using EventEchosAPI.Contracts.Events;
using Microsoft.EntityFrameworkCore;
using Mapster;

namespace EventEchosAPI.Features.Events
{
    public class CreateEvent
    {
        public sealed class Command : IRequest<string>
        {
            public string EventName { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
            public int UserLimit { get; set; }
            public string CreatedBy { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(x => x.EventName).NotEmpty();
                RuleFor(x => x.CreatedBy).NotEmpty();
            }
        }

        internal sealed class CommandHandler : IRequestHandler<Command, string>
        {
            private readonly ApplicationDbContext _dbcontext;
            private readonly IValidator<Command> _validator;
            public CommandHandler(ApplicationDbContext dbContext, IValidator<Command> validator)
            {
                _dbcontext = dbContext;
                _validator = validator;
            }

            public async Task<string> Handle(Command request, CancellationToken cancellationToken)
            {
                var result = _validator.Validate(request);
                if (!result.IsValid)
                {
                    throw new ValidationException(result.Errors);
                }

                using (var transaction = _dbcontext.Database.BeginTransaction())
                {
                    try
                    {
                        var validUser = await _dbcontext.Auths
                            .Include(u => u.User)
                            .FirstOrDefaultAsync(u => u.AuthId == request.CreatedBy);

                        if (validUser != null)
                        {

                        }

                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw ex;
                    }
                }

                return "Invalid request";
            }
            private string ConvertIFormFileToString(IFormFile imageFile)
            {
                if (imageFile == null || imageFile.Length == 0)
                {
                    throw new ArgumentException("Invalid image file");
                }

                using (var stream = new MemoryStream())
                {
                    imageFile.CopyTo(stream);
                    stream.Seek(0, SeekOrigin.Begin);
                    var originalBitmap = new Bitmap(stream);
                    //Bitmap downscaledBitmap = originalBitmap
                    //    .Downscale(originalBitmap.Height / 2, originalBitmap.Width / 2);
                    Bitmap downscaledBitmap = new Bitmap(originalBitmap, new Size(50, 50));
                    using (var outputStream = new MemoryStream())
                    {
                        downscaledBitmap.Save(outputStream, ImageFormat.Png);
                        return Convert.ToBase64String(outputStream.ToArray()) ?? string.Empty;
                    }

                }
            }
        }

    }

    public class CreateEventEndpoint : ICarterModule
    {
        private readonly APIResponse _response;

        public CreateEventEndpoint() => _response = new APIResponse();
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("api/events/create", /*[Authorize]*/ async (EventRequest request, ISender sender) =>
            {
                try
                {
                    var command = request.Adapt(new CreateEvent.Command());
                    var result = await sender.Send(command);
                    _response.IsSuccess = true;
                    _response.StatusCode = HttpStatusCode.OK;
                    _response.Result = new { Result = result};
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
            }).WithTags("Images").DisableAntiforgery()
            .Produces<APIResponse>(StatusCodes.Status200OK)
            .Produces<APIResponse>(StatusCodes.Status400BadRequest);
        }
    }
}
