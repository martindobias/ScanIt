using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Newtonsoft.Json;
using RedCorona.Net;
using TwainDotNet;
using TwainDotNet.WinFroms;
using Ionic.Zip;
using sharpPDF;
using sharpPDF.Elements;

namespace cz.martindobias.ScanIt
{
    public partial class MainForm : Form
    {
        private HttpServer server = null;
        private bool loaded = false;
        Twain twain = null;
        Semaphore scanSemaphore = new Semaphore(1, 1, "ScanIt_scanSemaphore");
        List<Bitmap> scanData = new List<Bitmap>();
        Boolean dataReady = false;
        ScanTarget scanTarget;

        public MainForm()
        {
            InitializeComponent();
            this.ResetControls();
            if (Properties.Settings.Default.server_autostart)
            {
                this.StartServer();
            }
        }

        void ResetControls()
        {
            this.autostartCheckBox.Checked = Properties.Settings.Default.server_autostart;
            this.startMinimizedCheckBox.Checked = Properties.Settings.Default.start_minimized;
            this.portUpDown.Value = Properties.Settings.Default.port;
            this.xUpDown.Value = Properties.Settings.Default.x;
            this.yUpDown.Value = Properties.Settings.Default.y;
            this.wUpDown.Value = Properties.Settings.Default.w;
            this.hUpDown.Value = Properties.Settings.Default.h;
            if (this.encodingComboBox.Items.Contains(Properties.Settings.Default.encode)) this.encodingComboBox.SelectedItem = Properties.Settings.Default.encode;
            this.adfCheckBox.Checked = Properties.Settings.Default.useADF;
            this.pdfCheckBox.Checked = Properties.Settings.Default.pdf;

            if (this.dpiComboBox.Items.Contains("" + Properties.Settings.Default.dpi)) this.dpiComboBox.SelectedItem = "" + Properties.Settings.Default.dpi;
            else this.dpiComboBox.SelectedItem = "300";

            if (Properties.Settings.Default.colour == ColourSetting.BlackAndWhite) this.blackAndWhiteRadioButton.Checked = true;
            else if (Properties.Settings.Default.colour == ColourSetting.GreyScale) this.greyscaleRadioButton.Checked = true;
            else this.colourRadioButton.Checked = true;

            this.sourceComboBox.Items.Add("");
            bool processed = false;
            this.twain = new Twain(new WinFormsWindowMessageHook(this));
            foreach (string name in this.twain.SourceNames)
            {
                this.sourceComboBox.Items.Add(name);
                if (name.Equals(Properties.Settings.Default.twain_source))
                {
                    this.sourceComboBox.SelectedItem = name;
                    processed = true;
                }
            }
            if (!processed)
            {
                Properties.Settings.Default.twain_source = "";
            }
            this.twain.TransferImage += new EventHandler<TransferImageEventArgs>(TransferImage);
            this.twain.ScanningComplete += new EventHandler<ScanningCompleteEventArgs>(ScanningComplete);
        }

        void ScanningComplete(object sender, ScanningCompleteEventArgs e)
        {
            this.dataReady = true;
            this.scanSemaphore.Release();
        }

        void TransferImage(object sender, TransferImageEventArgs e)
        {
            if (e.Image != null)
            {
                if (this.scanTarget == ScanTarget.APP)
                {
                    int x = Properties.Settings.Default.x;
                    int y = Properties.Settings.Default.y;
                    int w = Properties.Settings.Default.w;
                    int h = Properties.Settings.Default.h;
                    Rectangle r = MainForm.getRectangle(e.Image, x, y, w, h);
                    if (!r.IsEmpty)
                    {
                        this.pictureBox.Image = e.Image.Clone(MainForm.getRectangle(e.Image, x, y, w, h), e.Image.PixelFormat);
                    }
                    else
                    {
                        this.pictureBox.Image = e.Image;
                    }
                }
                else if (this.scanTarget == ScanTarget.WEB)
                {
                    this.scanData.Add(e.Image);
                }
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }

        private void startServerButton_Click(object sender, EventArgs e)
        {
            this.StartOrStopServer();
        }

        private void quitButton_Click(object sender, EventArgs e)
        {
            this.Quit();
        }

        private void startServerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.StartOrStopServer();
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Quit();
        }

        private void trayIcon_Click(object sender, EventArgs e)
        {
            this.Visible = !this.Visible;
        }

        private void trayIcon_DoubleClick(object sender, EventArgs e)
        {
            this.StartOrStopServer();
        }

        void Quit()
        {
            DialogResult r = MessageBox.Show("Really exit?", "Quit", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (r.Equals(DialogResult.Yes))
            {
                this.StopServer();
                this.Dispose();
                Properties.Settings.Default.Save();
            }
        }

        void StartServer()
        {
            if (this.server != null)
            {
                this.StopServer();
            }

            this.server = new HttpServer(new Server(Properties.Settings.Default.port));
            this.server.Hostmap.Add(".", ".");
            this.server.Handlers.Add(new BasicHandler());

            this.server.Handlers.Add(new SourceListHandler(this));
            this.server.Handlers.Add(new StatusHandler());
            this.server.Handlers.Add(new ScanHandler(this));

            string text = string.Format("ScanIt online {0}", Properties.Settings.Default.port);
            this.serverStatusText.Text = text;
            this.trayIcon.Text = text;
            this.startServerButton.Text = "Stop server";
            this.startServerToolStripMenuItem.Text = "Stop server";
        }

        void StopServer()
        {
            if (this.server != null)
            {
                this.server.Server.Close();
                this.server = null;
            }

            this.serverStatusText.Text = "ScanIt offline";
            this.trayIcon.Text = "ScanIt offline";
            this.startServerButton.Text = "Start server";
            this.startServerToolStripMenuItem.Text = "Start server";
        }

        void StartOrStopServer()
        {
            if (this.server == null)
            {
                this.StartServer();
            }
            else
            {
                this.StopServer();
            }
        }

        private void autostartCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.server_autostart = this.autostartCheckBox.Checked;
        }

        private void startMinimizedCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.start_minimized = this.startMinimizedCheckBox.Checked;
        }

        private void portUpDown_ValueChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.port = (int)this.portUpDown.Value;
        }

        private void MainForm_Activated(object sender, EventArgs e)
        {
            if (!this.loaded && Properties.Settings.Default.start_minimized)
            {
                this.Visible = false;
            }
            this.loaded = true;
        }

        private void scanButton_Click(object sender, EventArgs e)
        {
            if (!"".Equals(Properties.Settings.Default.twain_source))
                if (this.scanSemaphore.WaitOne(0))
                {
                    this.scanTarget = ScanTarget.APP;
                    this.scanData.Clear();
                    this.dataReady = false;
                    this.twain.SelectSource(Properties.Settings.Default.twain_source);
                    ScanSettings settings = MainForm.CreateDefaultSettings();
                    try
                    {
                        this.twain.StartScanning(settings);
                    }
                    catch (Exception ex)
                    {
                        if (Properties.Settings.Default.useADF)
                        {
                            settings.AbortWhenNoPaperDetectable = false;
                            settings.ShouldTransferAllPages = false;
                            settings.UseAutoFeeder = false;
                            settings.UseDocumentFeeder = false;
                            this.twain.StartScanning(settings);
                        }
                        else
                        {
                            throw ex;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Scanner in use, try to scan later or restart application in case of out-of-sync communication to scanner", "Scanner in use", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                }
        }

        static private ScanSettings CreateDefaultSettings()
        {
            return CreateDefaultSettings(Properties.Settings.Default.dpi, Properties.Settings.Default.colour, Properties.Settings.Default.useADF);
        }

        static private ScanSettings CreateDefaultSettings(int dpi, ColourSetting colour, bool adf)
        {
            ScanSettings scanSettings = new ScanSettings
            {
                ShowTwainUI = false,
                Resolution = new ResolutionSettings(),
                AbortWhenNoPaperDetectable = adf,
                ShouldTransferAllPages = adf,
                UseAutoFeeder = adf,
                UseDocumentFeeder = adf
            };
            scanSettings.Resolution.ColourSetting = colour;
            scanSettings.Resolution.Dpi = dpi;

            return scanSettings;
        }

        private void sourceComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.twain_source = (string)this.sourceComboBox.SelectedItem;
            this.scanButton.Enabled = !"".Equals(Properties.Settings.Default.twain_source);
        }

        class ScanHandler : SubstitutingFileReader
        {
            private MainForm mainForm;

            public ScanHandler(MainForm mainForm)
            {
                this.mainForm = mainForm;
            }

            delegate void ScanDelegate(string twain_source, int dpi, ColourSetting colour, bool adf);
            void Scan(string twain_source, int dpi, ColourSetting colour, bool adf)
            {
                this.mainForm.twain.SelectSource(twain_source == null ? Properties.Settings.Default.twain_source : twain_source);
                ScanSettings settings = MainForm.CreateDefaultSettings(dpi, colour, adf);
                this.mainForm.scanData.Clear();
                try
                {
                    this.mainForm.twain.StartScanning(settings);
                }
                catch (Exception ex)
                {
                    if (Properties.Settings.Default.useADF)
                    {
                        settings.AbortWhenNoPaperDetectable = false;
                        settings.ShouldTransferAllPages = false;
                        settings.UseAutoFeeder = false;
                        settings.UseDocumentFeeder = false;
                        this.mainForm.twain.StartScanning(settings);
                    }
                    else
                    {
                        throw ex;
                    }
                }
            }

            public override bool Process(HttpServer server, HttpRequest request, HttpResponse response)
            {
                if (request.Page.StartsWith("/scan"))
                {
                    string source = Properties.Settings.Default.twain_source;
                    int x = Properties.Settings.Default.x;
                    int y = Properties.Settings.Default.y;
                    int w = Properties.Settings.Default.w;
                    int h = Properties.Settings.Default.h;
                    string encode = Properties.Settings.Default.encode;
                    int dpi = Properties.Settings.Default.dpi;
                    bool adf = Properties.Settings.Default.useADF;
                    bool pdf = Properties.Settings.Default.pdf;
                    ColourSetting colour = Properties.Settings.Default.colour;

                    try
                    {
                        if (request.Query.ContainsKey("source"))
                        {
                            source = System.Web.HttpUtility.UrlDecode(request.Query["source"]);
                            if (!this.mainForm.twain.SourceNames.Contains(source))
                            {
                                throw new Exception("Unknown source: " + source);
                            }
                        }

                        if (request.Query.ContainsKey("encode"))
                        {
                            encode = request.Query["encode"];
                            if (!"base64".Equals(encode) && !"".Equals(encode))
                                throw new Exception("Unknown encoding: " + encode);
                        }

                        x = ScanHandler.getNumber(request, x, "x");
                        y = ScanHandler.getNumber(request, y, "y");
                        w = ScanHandler.getNumber(request, w, "w");
                        h = ScanHandler.getNumber(request, h, "h");

                        dpi = ScanHandler.getNumber(request, dpi, "dpi");
                        if (!this.mainForm.dpiComboBox.Items.Contains("" + dpi))
                            throw new Exception("Unsupported DPI value: " + dpi);

                        adf = request.Query.ContainsKey("adf") ? "1".Equals(request.Query["adf"]) : adf;
                        pdf = request.Query.ContainsKey("pdf") ? "1".Equals(request.Query["pdf"]) : adf;

                        if (request.Query.ContainsKey("colour"))
                        {
                            string col = request.Query["colour"];
                            if ("full".Equals(col)) colour = ColourSetting.Colour;
                            else if ("greyscale".Equals(col)) colour = ColourSetting.GreyScale;
                            else if ("blackandwhite".Equals(col)) colour = ColourSetting.BlackAndWhite;
                            else throw new Exception("Unknown colour settings: " + col);
                        }
                    }
                    catch (Exception e)
                    {
                        response.ReturnCode = 500;
                        response.Content = "Invalid URL arguments: " + e.Message;
                        return true;
                    }

                    bool testing = request.Query.ContainsKey("test") && "1".Equals(request.Query["test"]);

                    if ("".Equals(source) && !testing)
                    {
                        request.Host = ".";
                        request.Page = "/noscanner.html";
                        response.ReturnCode = 500;
                        return base.Process(server, request, response);
                    }

                    if (this.mainForm.scanSemaphore.WaitOne(0))
                    {
                        this.mainForm.scanData.Clear();
                        this.mainForm.dataReady = false;

                        if (testing)
                        {
                            try
                            {
                                this.mainForm.scanData.Add(new Bitmap("./TestImage.png"));
                            }
                            catch (Exception)
                            {
                                // left blank intentionally
                            }
                            this.mainForm.dataReady = true;
                            this.mainForm.scanSemaphore.Release();
                        }
                        else
                        {
                            this.mainForm.scanTarget = ScanTarget.WEB;
                            this.mainForm.Invoke(new ScanDelegate(Scan), new object[] { source, dpi, colour, adf });

                            DateTime startTime = DateTime.Now;
                            TimeSpan elapsed;
                            do
                            {
                                Thread.Sleep(100);
                                elapsed = DateTime.Now.Subtract(startTime);
                            } while (!this.mainForm.dataReady && elapsed.TotalMinutes < 5);
                        }

                        if (this.mainForm.dataReady && this.mainForm.scanData.Count > 0)
                        {
                            response.ReturnCode = 200;
                            using (MemoryStream ms = new MemoryStream())
                            {
                                if (!adf)
                                {
                                    ScanHandler.SaveImage(x, y, w, h, ms, this.mainForm.scanData.First(), pdf);
                                }
                                else
                                {
                                    ScanHandler.SaveImage(x, y, w, h, ms, this.mainForm.scanData, pdf);
                                }

                                if ("base64".Equals(encode))
                                {
                                    response.ContentType = "text/plain";
                                    response.Encoding = "base64";
                                    response.Content = Convert.ToBase64String(ms.ToArray());
                                }
                                else
                                {
                                    response.ContentType = (adf || pdf) ? "application/octet-stream" : (string)SubstitutingFileReader.MimeTypes[".png"];
                                    response.Encoding = null;
                                    response.RawContent = ms.ToArray();
                                }
                            }
                        }
                        else
                        {
                            request.Host = ".";
                            request.Page = "/failed.html";
                            response.ReturnCode = 500;
                            return base.Process(server, request, response);
                        }
                    }
                    else
                    {
                        request.Host = ".";
                        request.Page = "/blocked.html";
                        response.ReturnCode = 500;
                        return base.Process(server, request, response);
                    }
                    return true;
                }
                return false;
            }

            private static void SaveImage(int x, int y, int w, int h, MemoryStream ms, List<Bitmap> bs, bool pdf)
            {
                int count = 1;
                if (pdf)
                {
                    pdfDocument doc = new pdfDocument("Images scanned", "ScanIt by Martin Dobias");
                    foreach (Bitmap b in bs)
                    {
                        DrawPage(doc, count++, b);
                    }
                    doc.createPDF(ms);
                }
                else
                {
                    using (ZipFile zip = new ZipFile())
                    {
                        foreach (Bitmap b in bs)
                        {
                            using (MemoryStream mst = new MemoryStream())
                            {
                                ScanHandler.SaveImage(x, y, w, h, mst, b, false);
                                zip.AddEntry("" + count++ + ".png", mst.ToArray());
                            }
                        }
                        zip.Save(ms);
                    }
                }
            }

            private static void DrawPage(pdfDocument doc, int count, Bitmap b)
            {
                pdfPage page = doc.addPage();
                int bw = b.Width;
                int bh = b.Height;
                if (bw > page.width)
                {
                    bh = (int)((float)bh / ((float)bw / page.width));
                    bw = page.width;
                }

                if (bh > page.height)
                {
                    bw = (int)((float)bw / ((float)bh / page.height));
                    bh = page.height;
                }

                doc.addImageReference(b, "img" + count);
                pdfImageReference rf = doc.getImageReference("img" + count);
                page.addImage(rf, 0, page.height - bh, bh, bw);
            }

            private static void SaveImage(int x, int y, int w, int h, MemoryStream ms, Bitmap b, bool pdf)
            {
                Rectangle r = MainForm.getRectangle(b, x, y, w, h);
                if (!r.IsEmpty)
                {
                    b = b.Clone(r, b.PixelFormat);
                }
                if (pdf)
                {
                    pdfDocument doc = new pdfDocument("Image scanned", "ScanIt by Martin Dobias");
                    ScanHandler.DrawPage(doc, 1, b);
                    doc.createPDF(ms);
                }
                else
                {
                    b.Save(ms, ImageFormat.Png);
                }
            }

            private static int getNumber(HttpRequest request, int x, string attribute)
            {
                if (request.Query.ContainsKey(attribute))
                {
                    string value = request.Query[attribute];
                    if (!"".Equals(value))
                    {
                        x = Int32.Parse(value);
                        if (x < 0)
                        {
                            throw new Exception(attribute + " has to be positive, or zero: " + x);
                        }
                    }
                }
                return x;
            }
        }

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

                        jsonWriter.WritePropertyName("x");
                        jsonWriter.WriteValue(Properties.Settings.Default.x);
                        jsonWriter.WriteComment("The x-coordinate of the upper-left corner of the crop rectangle");

                        jsonWriter.WritePropertyName("y");
                        jsonWriter.WriteValue(Properties.Settings.Default.y);
                        jsonWriter.WriteComment("The y-coordinate of the upper-left corner of the crop rectangle");

                        jsonWriter.WritePropertyName("w");
                        jsonWriter.WriteValue(Properties.Settings.Default.w);
                        jsonWriter.WriteComment("The width of the crop rectangle");

                        jsonWriter.WritePropertyName("h");
                        jsonWriter.WriteValue(Properties.Settings.Default.h);
                        jsonWriter.WriteComment("The height of the crop rectangle");

                        jsonWriter.WritePropertyName("encode");
                        jsonWriter.WriteValue(Properties.Settings.Default.encode);
                        jsonWriter.WriteComment("Encoding setup");

                        jsonWriter.WritePropertyName("server_autostart");
                        jsonWriter.WriteValue(Properties.Settings.Default.server_autostart);
                        jsonWriter.WriteComment("Shall web server be started automatically");

                        jsonWriter.WritePropertyName("start_minimized");
                        jsonWriter.WriteValue(Properties.Settings.Default.start_minimized);
                        jsonWriter.WriteComment("Shall application start minimized");

                        jsonWriter.WritePropertyName("port");
                        jsonWriter.WriteValue(Properties.Settings.Default.port);
                        jsonWriter.WriteComment("Web server listening port");

                        jsonWriter.WritePropertyName("dpi");
                        jsonWriter.WriteValue(Properties.Settings.Default.dpi);
                        jsonWriter.WriteComment("DPI");

                        jsonWriter.WritePropertyName("colour");
                        jsonWriter.WriteValue(Properties.Settings.Default.colour == ColourSetting.Colour ? "full" : Properties.Settings.Default.colour == ColourSetting.GreyScale ? "greyscale" : "blackandwhite");
                        jsonWriter.WriteComment("Colouring setup");

                        jsonWriter.WritePropertyName("adf");
                        jsonWriter.WriteValue(Properties.Settings.Default.useADF);
                        jsonWriter.WriteComment("Shall ADF be used?");
                        jsonWriter.WriteEndObject();
                    }

                    response.Content = sb.ToString();
                    return true;
                }
                return false;
            }
        }

        class SourceListHandler : SubstitutingFileReader
        {
            private MainForm mainForm;

            public SourceListHandler(MainForm mainForm)
            {
                this.mainForm = mainForm;
            }

            public override bool Process(HttpServer server, HttpRequest request, HttpResponse response)
            {
                if (request.Page.StartsWith("/sources"))
                {
                    response.ContentType = (string)SubstitutingFileReader.MimeTypes[".html"];
                    response.ReturnCode = 200;
                    StringBuilder sb = new StringBuilder();
                    StringWriter sw = new StringWriter(sb);

                    using (JsonWriter jsonWriter = new JsonTextWriter(sw))
                    {
                        jsonWriter.Formatting = Formatting.Indented;
                        jsonWriter.WriteStartObject();
                        foreach (string name in this.mainForm.twain.SourceNames)
                        {
                            jsonWriter.WritePropertyName(name);
                            jsonWriter.WriteValue(System.Web.HttpUtility.UrlEncode(name));
                        }
                        jsonWriter.WriteEndObject();
                    }

                    response.Content = sb.ToString();
                    return true;
                }
                return false;
            }
        }

        class BasicHandler : SubstitutingFileReader
        {
            public override bool Process(HttpServer server, HttpRequest request, HttpResponse response)
            {
                request.Host = ".";
                request.Page = "/home.html";
                return base.Process(server, request, response);
            }
        }

        enum ScanTarget
        {
            APP,
            WEB
        }

        private void xUpDown_ValueChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.x = (int)this.xUpDown.Value;
        }

        private void yUpDown_ValueChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.y = (int)this.yUpDown.Value;
        }

        private void wUpDown_ValueChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.w = (int)this.wUpDown.Value;
        }

        private void hUpDown_ValueChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.h = (int)this.hUpDown.Value;
        }


        internal static Rectangle getRectangle(Bitmap bitmap, int x, int y, int w, int h)
        {
            return Rectangle.Intersect(new Rectangle(new Point(0, 0), bitmap.Size), new Rectangle
            {
                X = x,
                Y = y,
                Width = w,
                Height = h
            });
        }

        private void encodingComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.encode = (string)this.encodingComboBox.SelectedItem;
        }

        private void dpiComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                Properties.Settings.Default.dpi = Int32.Parse((string)this.dpiComboBox.SelectedItem);
            }
            catch (Exception)
            {
                Properties.Settings.Default.dpi = 300;
                this.dpiComboBox.SelectedItem = "300";
            }
        }

        private void adfCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.useADF = this.adfCheckBox.Checked;
        }

        private void colourRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (this.colourRadioButton.Checked) Properties.Settings.Default.colour = ColourSetting.Colour;
            else if (this.greyscaleRadioButton.Checked) Properties.Settings.Default.colour = ColourSetting.GreyScale;
            else Properties.Settings.Default.colour = ColourSetting.BlackAndWhite;
        }

        private void pdfCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.pdf = this.pdfCheckBox.Checked;
        }
    }
}
