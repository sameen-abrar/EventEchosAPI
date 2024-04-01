using Carter;
using EventEchosAPI.Contracts.Auths;
using EventEchosAPI.Database;
using EventEchosAPI.Helpers;
using EventEchosAPI.Shared;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace EventEchosAPI.Features.Auth
{
    public class Register
    {
        public sealed class Query : IRequest<string>
        {

        }

        public class Validator : AbstractValidator<Query>
        {
            public Validator()
            {
            }
        }

        internal sealed class CommandHandler : IRequestHandler<Query, string>
        {
            private readonly ApplicationDbContext _dbcontext;
            private readonly IValidator<Query> _validator;
            private readonly string secretkey;
            public CommandHandler(ApplicationDbContext dbContext, IConfiguration configuration, IValidator<Query> validator)
            {
                _dbcontext = dbContext;
                _validator = validator;
            }

            public Task<string> Handle(Query request, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }
    }

    public class RegisterEndpoint : ICarterModule
    {
        private readonly APIResponse _response;

        public RegisterEndpoint() => _response = new APIResponse();
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("api/user/register", async (RegisterRequest request, ISender sender) =>
            {
                try
                {
                    var command = new Register.Query {  };
                    var result = await sender.Send(command);
                    _response.IsSuccess = true;
                    _response.StatusCode = HttpStatusCode.OK;
                    _response.Result = new LoginResponse { AccessToken = result };
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
            }).WithTags("Auth")
            .Produces<APIResponse>(StatusCodes.Status200OK)
            .Produces<APIResponse>(StatusCodes.Status400BadRequest);
        }
    }
}