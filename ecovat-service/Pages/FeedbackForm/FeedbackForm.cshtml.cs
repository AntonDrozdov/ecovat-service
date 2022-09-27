using ecovat_service.businessLogic.Interfaces;
using ecovat_service.businessLogic.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace ecovat_service.Pages.FeedbackForm
{
    public class FeedbackFormModel : PageModel
    {
        private readonly IEmailService _emailService;
        private readonly IValidationService _validationService;

        public FeedbackFormModel(IEmailService emailService, IValidationService validationService)
        {
            _emailService = emailService;
            _validationService = validationService;
        }

        public async Task<IActionResult> OnPost()
        {
                var Name = Request.Form["Name"].ToString();
            var Email = Request.Form["Email"].ToString();
            var Phone = Request.Form["Phone"].ToString();
            var Message = Request.Form["Message"].ToString();

            if (!_validationService.ValidateFeedbackForm(Name, Email, Phone, Message))
            {
                return new JsonResult(new { status = "fail" });
            }

            var emailMessage = new Message
            {
                Subject = "������ � ����� ecovat-service.ru",
                Body = $"��: {Name}\r\n\r\n" +
                       $"E-mail:{Email}\r\n\r\n" +
                       $"�������:{Phone}\r\n\r\n" +
                       $"���������:\r\n\r\n" +
                       $"{Message}"

            };

            var res = await _emailService.Send(emailMessage);
            if (res)
            {
                return new JsonResult(new { status = "success" });
            }

            return new JsonResult(new { status = "fail" });
        }
    }
}   
