using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RedCorona.Net;

namespace cz.martindobias.ScanIt
{
    class StatusHandler : SubstitutingFileReader
    {
        public override bool Process(HttpServer server, HttpRequest request, HttpResponse response)
        {
            if (request.Page.StartsWith("/status"))
            {
                response.ContentType = (string)SubstitutingFileReader.MimeTypes[".html"];
                response.ReturnCode = 200;
                response.Content = "Working";
                return true;
            }
            return false;
        }
    }

    class EmptyHandler : SubstitutingFileReader
    {
        public override bool Process(HttpServer server, HttpRequest request, HttpResponse response)
        {
            request.Host = ".";
            request.Page = "/home.html";
            return base.Process(server, request, response);
        }
    }
}
