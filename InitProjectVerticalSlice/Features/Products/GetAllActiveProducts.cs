using Carter;
using EventEchosAPI.Contracts.Products;
using EventEchosAPI.Database;
using EventEchosAPI.Shared;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using System.Net;

namespace EventEchosAPI.Features.Products
{
    public class GetAllActiveProducts
    {
        public sealed class Query: IRequest<ProductResponse>
        {
            public int? LoadMore { get; set; } = 3;
        }

        public class Validator : AbstractValidator<Query>
        {
            public Validator()
            {
                RuleFor(x => x.LoadMore).GreaterThan(0);
            }
        }
        internal sealed class QueryHandler : IRequestHandler<Query, ProductResponse>
        {
            private readonly ApplicationDbContext _dbcontext;
            public QueryHandler(ApplicationDbContext dbContext)
            {
                _dbcontext = dbContext;
            }

            public async Task<ProductResponse> Handle(Query request, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }
    }

    public class GetAllActiveProductsEndpoint : ICarterModule
    {
        private readonly APIResponse _response;

        public GetAllActiveProductsEndpoint() => _response = new APIResponse();
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("api/products", [Authorize] async (ISender sender, int? LoadMore, HttpContext context) => 
            {
                try
                {
                    var query = await sender.Send(new GetAllActiveProducts.Query { LoadMore = LoadMore });
                    if( query == null)
                    {
                        _response.IsSuccess = false;
                        _response.StatusCode = HttpStatusCode.NotFound;
                        _response.Result = "No products found";
                        return Results.BadRequest();
                    }

                    _response.IsSuccess = true;
                    _response.StatusCode = HttpStatusCode.OK;
                    _response.Result = query;
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
            }).WithTags("Products")
            .Produces<APIResponse>(StatusCodes.Status200OK)
            .Produces<APIResponse>(StatusCodes.Status400BadRequest);
        }
    }
}
