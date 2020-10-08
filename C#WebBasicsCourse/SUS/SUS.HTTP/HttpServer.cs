using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace SUS.HTTP
{
    public class HttpServer : IHttpServer
    {
        List<Route> routeTable;

        public HttpServer(List<Route> routeTable)
        {
            this.routeTable = routeTable;
        }

        public async Task StartAsync(int port)
        {
            TcpListener tcpListener = new TcpListener(IPAddress.Loopback, port);
            tcpListener.Start();

            while (true)
            {
                TcpClient tcpClient = await tcpListener.AcceptTcpClientAsync();
                ProcessClient(tcpClient);
            }

        }

        private async Task ProcessClient(TcpClient tcpClient)
        {
            try
            {
                using (NetworkStream stream = tcpClient.GetStream())
                {
                    int position = 0;
                    byte[] buffer = new byte[HttpConstants.BufferSize];
                    List<byte> data = new List<byte>();

                    while (true)
                    {
                        int count = await stream.ReadAsync(buffer, position, buffer.Length);
                        position += count;

                        if (count < buffer.Length)
                        {
                            var partialBuffer = new byte[count];
                            Array.Copy(buffer, partialBuffer, count);
                            data.AddRange(partialBuffer);

                            // break because there isn't any data left
                            break;
                        }
                        else
                        {
                            data.AddRange(buffer);
                        }
                    }

                    var requestAsString = Encoding.UTF8.GetString(data.ToArray());

                    if (string.IsNullOrEmpty(requestAsString))
                    {
                        return;
                    }

                    var request = new HttpRequest(requestAsString);
                    Console.WriteLine($"{request.Method} {request.Path} {request.Headers.Count}");

                    HttpResponse response;
                    var route = this.routeTable.FirstOrDefault(x => string.Compare(x.Path, request.Path, true) == 0 && x.Method == request.Method);

                    if (route != null)
                    {
                        response = route.Action(request);
                    }
                    else
                    {
                        var notFoundHtml = "<h1>Not Found!</h1>";
                        response = new HttpResponse("text/html", Encoding.UTF8.GetBytes(notFoundHtml), HttpStatusCode.NotFound);
                    }


                    var httpResponseHeaderAsBytes = Encoding.UTF8.GetBytes(response.ToString());
                    await stream.WriteAsync(httpResponseHeaderAsBytes, 0, httpResponseHeaderAsBytes.Length);
                    await stream.WriteAsync(response.Body, 0, response.Body.Length);
                }
                tcpClient.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
            }
        }
    }
}
