namespace SUS.HTTP
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class HttpResponse
    {
        public HttpResponse(string contentType, byte[] body, HttpStatusCode statusCode = HttpStatusCode.Ok)
        {
            this.StatusCode = statusCode;
            this.Body = body ?? throw new ArgumentNullException(nameof(body));
            this.Headers = new List<Header>()
            {
                { new Header("Content-Type", contentType) },
                { new Header("Content-Length", body.Length.ToString()) },
                { new Header("Server", "SUS Server 1.0") }
            };
            this.Cookies = new List<ResponseCookie>();
        }

        public ICollection<Header> Headers { get; set; }
        public ICollection<ResponseCookie> Cookies { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public byte[] Body { get; set; }

        public override string ToString()
        {
            StringBuilder responseBuilder = new StringBuilder();
            responseBuilder.Append($"HTTP/1.1 {(int)this.StatusCode} {this.StatusCode}{HttpConstants.NewLine}");
            foreach (var header in this.Headers)
            {
                responseBuilder.Append(header.ToString() + HttpConstants.NewLine);
            }

            foreach (var cookie in this.Cookies)
            {
                responseBuilder.Append("Set-Cookie: " + cookie.ToString() + HttpConstants.NewLine);
            }

            responseBuilder.Append(HttpConstants.NewLine);

            return responseBuilder.ToString();
        }
    }
}
