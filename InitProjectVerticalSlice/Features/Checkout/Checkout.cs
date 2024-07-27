using Carter;
using EventEchosAPI.Contracts.Checkout;
using EventEchosAPI.Database;
using EventEchosAPI.Shared;
using FluentValidation;
using MediatR;
using System.Net;

namespace EventEchosAPI.Features.Checkout
{
    public class Checkout
    {
        public sealed class Command : IRequest<string>
        {

        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
            }
        }

        internal sealed class CommandHandler : IRequestHandler<Command, string>
        {
            private readonly ApplicationDbContext _dbcontext;
            private readonly IValidator<Command> _validator;
            private readonly string secretkey;
            public CommandHandler(ApplicationDbContext dbContext, IConfiguration configuration, IValidator<Command> validator)
            {
                _dbcontext = dbContext;
                _validator = validator;
            }

            public Task<string> Handle(Command request, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }
    }
    public class CheckoutEndpoint : ICarterModule
    {
        private readonly APIResponse _response;

        public CheckoutEndpoint() => _response = new APIResponse();
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("api/checkout", async (CheckoutRequest request, ISender sender) =>
            {
                try
                {
                    var command = new Checkout.Command { };
                    var result = await sender.Send(command);
                    _response.IsSuccess = true;
                    _response.StatusCode = HttpStatusCode.OK;
                    _response.Result = new CheckoutResponse() { };
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
            }).WithTags("Checkout")
            .Produces<APIResponse>(StatusCodes.Status200OK)
            .Produces<APIResponse>(StatusCodes.Status400BadRequest);
        }
    }
}
