using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ecovat_service.businessLogic.Models;
using ecovat_service.businessLogic.Interfaces;

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

        public IActionResult OnPost()
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
                Subject = "«‡ÔÓÒ Ò Ò‡ÈÚ‡ kc-construction.ru",
                Body = $"Œ“: {Name}\r\n" +
                       $"E-mail:{Email}\r\n" +
                       $"“≈À≈‘ŒÕ:{Phone}\r\n" +
                       $"—ŒŒ¡Ÿ≈Õ»≈:\r\n" +
                       $"{Message}"

            };

            if (_emailService.Send(emailMessage))
            {
                return new JsonResult(new { status = "success" });
            }
            return new JsonResult(new { status = "fail" });

        }
    }
}   
