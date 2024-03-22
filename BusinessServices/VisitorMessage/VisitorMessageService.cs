using WebRexErpAPI.Data;
using WebRexErpAPI.Helper;
using WebRexErpAPI.Services.VisitorMessage.Dto;

namespace WebRexErpAPI.Services.VisitorMessage
{
    public class VisitorMessageService : IVisitorMessageService,IDisposable
    {
        private readonly ApplicationDbContext _context;
        public VisitorMessageService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> CreateAsync(VisitorMessageDto input)
        {
            try
            {
                var visitorMessage = new WebRexErpAPI.Models.VisitorMessage();

                visitorMessage.Subject = input.Subject;
                visitorMessage.Street1 = input.Street1;
                visitorMessage.Street2 = input.Street2;
                visitorMessage.City = input.City;
                visitorMessage.State = input.State;
                visitorMessage.PostalZip = input.PostalZip;
                visitorMessage.Message   = input.Message;
                visitorMessage.VisitorId = input.VisitorId;
                visitorMessage.FullName = input.FullName;
                visitorMessage.IndustryQBID = input.IndustryQBID;
                visitorMessage.TypeQBID = input.TypeQBID;
                visitorMessage.Id =input.Id;
                visitorMessage.CreatedDate = DateTime.UtcNow;
                visitorMessage.Email = input.Email;
                visitorMessage.CompanyName = input.CompanyName;
                visitorMessage.HowCanWeReply = input.HowCanWeReply;

               await  _context.tblVisitorMessages.AddAsync(visitorMessage);
               await  _context.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {

            }
        }
    }
}
