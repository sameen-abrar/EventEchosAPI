using Azure.Core;
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
using System.Reflection.Metadata;
using System.Security.Claims;
using System.Text;
using static EventEchosAPI.Extensions.ClaimExtensions;

namespace EventEchosAPI.Features.Authentication
{
    public static class Login
    {
        public sealed class Command : IRequest<string>
        {
            public string UserName { get; set; }
            public string Password { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                {
                    RuleFor(x => x.UserName)
                        .NotEmpty().WithMessage("Username is required");
                    RuleFor(x => x.Password).NotNull().NotEmpty();
                }
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
                secretkey = configuration.GetSection("ApiSettings:Secret").Value;
            }

            public async Task<string> Handle(Command request, CancellationToken cancellationToken)
            {
                var result = _validator.Validate(request);
                if (!result.IsValid)
                {
                    throw new ValidationException(result.Errors);
                }

                var authUser = await _dbcontext.Auths
                    .FirstOrDefaultAsync(x => x.UserName == request.UserName, cancellationToken);

                if (authUser is null) throw new Exception("User does not exist");

                var checkHashedPassword = PasswordHelper.VerifyPassword(request.Password, authUser.Password);

                if (!checkHashedPassword) throw new Exception("User Credentials Invalid");

                var user = await _dbcontext.Users
                    .Include(x => x.UserRole)
                    .ThenInclude(x => x.UserRolePermissions)
                    .FirstOrDefaultAsync(x => x.UserId == authUser.UserId, cancellationToken);

                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtKey = Encoding.ASCII.GetBytes(secretkey ?? "");
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.UserData, authUser.UserName),
                        new Claim(ClaimTypes.Role, user?.UserRole?.RoleName ?? ""),
                        new Claim(CustomClaimTypes.UserId, user.UserId), 
                    }),
                    Expires = DateTime.UtcNow.AddDays(10),
                    SigningCredentials = new(new SymmetricSecurityKey(jwtKey), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
        }
    }

    public class LoginEndpoint : ICarterModule
    {
        private readonly APIResponse _response;

        public LoginEndpoint() => _response = new APIResponse();
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("api/user/login", async (LoginRequest request, ISender sender) =>
            {
                try
                {
                    var command = new Login.Command { UserName = request.UserName, Password = request.Password };
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
