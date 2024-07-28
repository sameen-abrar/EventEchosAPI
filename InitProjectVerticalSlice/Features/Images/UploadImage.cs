using Carter;
using EventEchosAPI.Contracts.Checkout;
using EventEchosAPI.Contracts.Images;
using EventEchosAPI.Database;
using EventEchosAPI.Entities.Events;
using EventEchosAPI.Helpers;
using EventEchosAPI.Shared;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using EventEchosAPI.Extensions;

namespace EventEchosAPI.Features.Images
{
    public class UploadImage
    {
        public sealed class Command : IRequest<string>
        {
            public string? UserId { get; set; }
            public IFormFile Image { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(x => x.Image).NotEmpty();
                RuleFor(x => x.UserId).NotEmpty();
            }
        }

        internal sealed class CommandHandler : IRequestHandler<Command, string>
        {
            private readonly ApplicationDbContext _dbcontext;
            private readonly IValidator<Command> _validator;
            private readonly string _uploadDirectory;
            public CommandHandler(ApplicationDbContext dbContext, IConfiguration config, IConfiguration configuration, IValidator<Command> validator)
            {
                _dbcontext = dbContext;
                _validator = validator;
                _uploadDirectory = config.GetValue<string>("UploadDirectory");
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
                        var now = DateTime.UtcNow;
                        var appUser = await _dbcontext.Users
                            .FirstOrDefaultAsync(x => x.UserId == request.UserId, cancellationToken);

                        if (appUser == null) throw new Exception("User not found");

                        var imageCount = await _dbcontext.ImageDatas
                            .Where(x => x.UserId == appUser.UserId && x.CreatedDate.Date == now.Date)
                            .CountAsync();

                        var extension = Path.GetExtension(request.Image.FileName);
                        var fileName =  $"{request.UserId}_{now.ToString("MMddyyyyHHmm")}_{++imageCount}{extension}";
                        var filePath = Path.Combine(_uploadDirectory, fileName);
                        string base64Image = "";

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await request.Image.CopyToAsync(stream);
                            base64Image = ConvertIFormFileToString(request.Image);
                        }

                        var imageData = new ImageData
                        {
                            ImageId = "Img-" + GenerateId.MakeId(),
                            UserId = request.UserId,
                            CreatedDate = now,
                            ImageUrl = filePath,
                            ImageBase64 = base64Image,
                        };
                        _dbcontext.ImageDatas.Add(imageData);
                        await _dbcontext.SaveChangesAsync(cancellationToken);

                        await transaction.CommitAsync(cancellationToken);                        

                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw ex;
                    }
                }

                    return string.Empty;
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
                    var originalBitmap =  new Bitmap(stream);
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

    public class UplaodImageEndpoint : ICarterModule
    {
        private readonly APIResponse _response;

        public UplaodImageEndpoint() => _response = new APIResponse();
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("api/images/upload", /*[Authorize]*/ async (string userId, IFormFile image, ISender sender) =>
            {
                try
                {
                    var command = new UploadImage.Command { UserId = userId, Image = image};
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
            }).WithTags("Images").DisableAntiforgery()
            .Produces<APIResponse>(StatusCodes.Status200OK)
            .Produces<APIResponse>(StatusCodes.Status400BadRequest);
        }
    }
}
