using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RedCorona.Net;
using Newtonsoft.Json;
using System.IO;

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
                StringBuilder sb = new StringBuilder();
                StringWriter sw = new StringWriter(sb);

                using (JsonWriter jsonWriter = new JsonTextWriter(sw))
                {
                    jsonWriter.Formatting = Formatting.Indented;
                    jsonWriter.WriteStartObject();
                    jsonWriter.WritePropertyName("twain_source");
                    jsonWriter.WriteValue(Properties.Settings.Default.twain_source);
                    jsonWriter.WriteComment("ID of selected TWAIN source");
                    jsonWriter.WritePropertyName("server_autostart");
                    jsonWriter.WriteValue(Properties.Settings.Default.server_autostart);
                    jsonWriter.WriteComment("Shall web server be started automatically");
                    jsonWriter.WritePropertyName("start_minimized");
                    jsonWriter.WriteValue(Properties.Settings.Default.start_minimized);
                    jsonWriter.WriteComment("Shall application start minimized");
                    jsonWriter.WritePropertyName("port");
                    jsonWriter.WriteValue(Properties.Settings.Default.port);
                    jsonWriter.WriteComment("Web server listening port");
                    jsonWriter.WriteEndObject();
                }

                response.Content = sb.ToString();
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
