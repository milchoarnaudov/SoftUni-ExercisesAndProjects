using SUS.HTTP;
using SUS.MvcFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace MvcApp.Controllers
{
    class HomeController : Controller
    {
        public HttpResponse Index(HttpRequest request)
        {

            return this.View();
        }
    }
}
