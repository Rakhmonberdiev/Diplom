using Diplom.Data;
using Diplom.DTO.TicketDtos;
using Diplom.Entities;
using Diplom.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using QRCoder;
using Diplom.Extensions;
namespace Diplom.Repositories.Implementation
{
    public class TicketRepo : ITicketRepo
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TicketRepo(AppDbContext dbContext, IWebHostEnvironment hostEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            _context = dbContext;     
            _webHostEnvironment = hostEnvironment;
            _httpContextAccessor = httpContextAccessor;
        }



        public async Task<Ticket> Create(TicketCreateDto ticket)
        {
            var id = Guid.NewGuid();
            // Генерируем QR-код
            var request = _httpContextAccessor.HttpContext.Request;
            var domain = $"{request.Scheme}://{request.Host}";
            string textToEncode = $"{domain}/ticket/{id}";
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(textToEncode, QRCodeGenerator.ECCLevel.Q);
            PngByteQRCode qrCode = new PngByteQRCode(qrCodeData);
            byte[] qrCodeImage = qrCode.GetGraphic(40); // 20 - размер в пикселях

            // Сохраняем изображение QR-кода в папку wwwroot/assets/qrcodes
            string qrCodeFileName = $"{id}.png"; // Имя файла
            string qrCodeImagePath = Path.Combine(_webHostEnvironment.WebRootPath, "assets/qrcodes", qrCodeFileName); // Путь к файлу
            File.WriteAllBytes(qrCodeImagePath, qrCodeImage);
        
            var model = new Ticket
            {   Id = id,
                Date = ticket.Date,
                RouteId = ticket.RouteId,
                ScheduleId = ticket.ScheduleId,
                QRUrl = "assets/qrcodes/"+qrCodeFileName,
                UserId = ticket.UserId
                
            };
          



            
            await _context.Tickets.AddAsync(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<Ticket> GetById(Guid id, string userId)
        {
            return await _context.Tickets
                .Where(s=>s.UserId==userId)
                .Include(s=>s.Route.StartPoint)
                .Include(s=>s.Route.EndPoint)
                .Include(s=>s.Schedule)
                .FirstOrDefaultAsync(x => x.Id==id);
        }
    }
}
