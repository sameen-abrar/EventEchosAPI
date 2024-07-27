using Azure.Identity;
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
using EventEchosAPI.Entities.Users;

namespace EventEchosAPI.Features.Authentication
{
    public class Register
    {
        public sealed class Command : IRequest<string>
        {
            public string UserName { get; set; }
            public string Phone { get; set; }
            public string Password { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(x => x.UserName).NotEmpty();
                //RuleFor(x => x.Password)
                //.NotEmpty().WithMessage("Password is required.")
                //.MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
                //.Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$")
                //.WithMessage("Password must contain at least one uppercase letter, one lowercase letter, one digit, and one special character.");
                RuleFor(x => x.Phone).MinimumLength(7);
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

            public async Task<string> Handle(Command request, CancellationToken cancellationToken)
            {
                var result = _validator.Validate(request);

                if(result.IsValid)
                {
                    var userInfo = new User
                    {
                        Phone = request.Phone,
                        UserId = "USER-" + GenerateId.MakeId(),
                    };
                    await _dbcontext.Users.AddAsync(userInfo);
                    await _dbcontext.SaveChangesAsync();

                    var authUser = new Auth
                    {
                        UserId = userInfo.UserId,
                        UserName = request.UserName,
                        Password = PasswordHelper.HashPassword(request.Password)
                    };
                    await _dbcontext.Auths.AddAsync(authUser);
                    await _dbcontext.SaveChangesAsync();
                    return await Task.FromResult(authUser.UserName);
                }
                return "Could not Register User";
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
                    var command = new Register.Command { UserName = request.UserName, Password = request.Password, Phone = request.Phone };
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