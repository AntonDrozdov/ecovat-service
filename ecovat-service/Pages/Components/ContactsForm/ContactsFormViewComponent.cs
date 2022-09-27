using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace kcconstruction.Web.Pages.Components.FeedbackForm
{
    public class ContactsFormViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}

