using Microsoft.AspNetCore.Antiforgery;
using MonitorV.Controllers;

namespace MonitorV.Web.Host.Controllers
{
    public class AntiForgeryController : MonitorVControllerBase
    {
        private readonly IAntiforgery _antiforgery;

        public AntiForgeryController(IAntiforgery antiforgery)
        {
            _antiforgery = antiforgery;
        }

        public void GetToken()
        {
            _antiforgery.SetCookieTokenAndHeader(HttpContext);
        }
    }
}
