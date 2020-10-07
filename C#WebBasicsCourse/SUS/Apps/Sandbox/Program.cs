
namespace Sandbox
{
    using SUS.HTTP;
    using System;
    using System.IO;
    using System.Text;
    using System.Threading.Tasks;

    class Program
    {
        static async Task Main(string[] args)
        {
            IHttpServer server = new HttpServer();

            server.AddRoute("/", HomePage);
            server.AddRoute("/favicon.ico", Favicon);
            server.AddRoute("/about", About);
            server.AddRoute("/users/login", Login);

            await server.StartAsync(80);
        }

        private static HttpResponse Favicon(HttpRequest arg)
        {
            var fileBytes = File.ReadAllBytes("wwwroot/favicon.ico");
            var response = new HttpResponse("image/vnd.microsoft.icon", fileBytes);
            return response;
        }

        static HttpResponse HomePage(HttpRequest request)
        {

            var httpBodyResponse = "<h1>Welcome</h1>";
            var httpBodyResponseAsBytes = Encoding.UTF8.GetBytes(httpBodyResponse);
            var response = new HttpResponse("text/html", httpBodyResponseAsBytes);
            response.Cookies.Add(new ResponseCookie("test-cookie", "home"));
            response.Cookies.Add(new ResponseCookie("sid", Guid.NewGuid().ToString()) { HttpOnly = true, MaxAge = 60 * 24 * 60 * 60 });
            return response;
        }

        static HttpResponse About(HttpRequest request)
        {
            var httpBodyResponse = "<h1>About</h1>";
            var httpBodyResponseAsBytes = Encoding.UTF8.GetBytes(httpBodyResponse);
            var response = new HttpResponse("text/html", httpBodyResponseAsBytes);
            response.Cookies.Add(new ResponseCookie("test-cookie", "home"));
            response.Cookies.Add(new ResponseCookie("sid", Guid.NewGuid().ToString()) { HttpOnly = true, MaxAge = 60 * 24 * 60 * 60 });
            return response;
        }

        static HttpResponse Login(HttpRequest request)
        {
            var httpBodyResponse = "<h1>Login</h1>";
            var httpBodyResponseAsBytes = Encoding.UTF8.GetBytes(httpBodyResponse);
            var response = new HttpResponse("text/html", httpBodyResponseAsBytes);
            response.Cookies.Add(new ResponseCookie("test-cookie", "about"));
            response.Cookies.Add(new ResponseCookie("sid", Guid.NewGuid().ToString()) { HttpOnly = true, MaxAge = 60 * 24 * 60 * 60 });
            return response; throw new NotImplementedException();
        }
    }
}
