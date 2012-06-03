using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RedCorona.Net;
using TwainDotNet;
using TwainDotNet.WinFroms;
using Newtonsoft.Json;
using System.IO;
using System.Threading;
using System.Drawing.Imaging;

namespace cz.martindobias.ScanIt
{
    public partial class MainForm : Form
    {
        private HttpServer server = null;
        private bool loaded = false;
        static private Twain twain = null;
        static private Semaphore scanSemaphore = new Semaphore(1, 1, "ScanIt_scanSemaphore");
        static private Bitmap scanBitmap = null;
        static private ScanTarget scanTarget;
        static private ScanSettings scanSettings = new ScanSettings
                        {
                            ShowTwainUI = false,
                        };


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

            this.sourceComboBox.Items.Add("");
            bool processed = false;
            MainForm.twain = new Twain(new WinFormsWindowMessageHook(this));
            foreach (string name in MainForm.twain.SourceNames)
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
            MainForm.twain.TransferImage += new EventHandler<TransferImageEventArgs>(TransferImage);
            MainForm.twain.ScanningComplete += new EventHandler<ScanningCompleteEventArgs>(ScanningComplete);
        }

        void ScanningComplete(object sender, ScanningCompleteEventArgs e)
        {
            MainForm.scanSemaphore.Release();
        }

        void TransferImage(object sender, TransferImageEventArgs e)
        {
            if (e.Image != null)
            {
                if (MainForm.scanTarget == ScanTarget.APP)
                {
                    this.pictureBox.Image = e.Image;
                }
                else if (MainForm.scanTarget == ScanTarget.WEB)
                {
                    MainForm.scanBitmap = e.Image;
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
            if (MainForm.scanSemaphore.WaitOne(0))
            {
                MainForm.scanTarget = ScanTarget.APP;
                MainForm.twain.StartScanning(MainForm.scanSettings);
            }
            else
            {
                MessageBox.Show("Scanner in use, try to scan later or restart application in case of out-of-sync communication to scanner", "Scanner in use", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }

        private void sourceComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.twain_source = (string)this.sourceComboBox.SelectedItem;
            MainForm.twain.SelectSource(Properties.Settings.Default.twain_source);
            this.scanButton.Enabled = !"".Equals(Properties.Settings.Default.twain_source);
        }

        class ScanHandler : SubstitutingFileReader
        {
            private MainForm mainForm;

            public ScanHandler(MainForm mainForm)
            {
                this.mainForm = mainForm;
            }

            delegate void ScanDelegate();
            void Scan()
            {
                MainForm.twain.StartScanning(MainForm.scanSettings);
            }

            public override bool Process(HttpServer server, HttpRequest request, HttpResponse response)
            {
                if (request.Page.StartsWith("/scan"))
                {
                    if ("".Equals(Properties.Settings.Default.twain_source))
                    {
                        request.Host = ".";
                        request.Page = "/noscanner.html";
                        return base.Process(server, request, response);
                    }

                    if (MainForm.scanSemaphore.WaitOne(0))
                    {
                        MainForm.scanBitmap = null;
                        MainForm.scanTarget = ScanTarget.WEB;
                        this.mainForm.Invoke(new ScanDelegate(Scan));
                        //MainForm.twain.StartScanning(MainForm.scanSettings);

                        DateTime startTime = DateTime.Now;
                        TimeSpan elapsed;
                        do
                        {
                            Thread.Sleep(100);
                            elapsed = DateTime.Now.Subtract(startTime);
                        } while (MainForm.scanBitmap == null && elapsed.TotalMinutes < 1);


                        if (MainForm.scanBitmap != null)
                        {
                            response.ReturnCode = 200;
                            using (MemoryStream ms = new MemoryStream())
                            {
                                MainForm.scanBitmap.Save(ms, ImageFormat.Png);
                                if (request.Query.ContainsKey("encode") && "base64".Equals(request.Query["encode"]))
                                {
                                    response.ContentType = "application/octet-stream";
                                    response.Encoding = "base64";
                                    response.RawContent = Encoding.UTF7.GetBytes(Convert.ToBase64String(ms.ToArray()));
                                }
                                else
                                {
                                    response.ContentType = (string)SubstitutingFileReader.MimeTypes[".png"];
                                    response.RawContent = ms.ToArray();
                                }
                            }
                        }
                        else
                        {
                            request.Host = ".";
                            request.Page = "/failed.html";
                            return base.Process(server, request, response);
                        }
                    }
                    else
                    {
                        request.Host = ".";
                        request.Page = "/blocked.html";
                        return base.Process(server, request, response);
                    }
                    return true;
                }
                return false;
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

        class BasicHandler : SubstitutingFileReader
        {
            public override bool Process(HttpServer server, HttpRequest request, HttpResponse response)
            {
                if (request.Page.StartsWith("/test"))
                {
                    request.Host = ".";
                    request.Page = "/TestImage.png";
                    return base.Process(server, request, response);
                }
                else
                {
                    request.Host = ".";
                    request.Page = "/home.html";
                    return base.Process(server, request, response);
                }
            }
        }

        enum ScanTarget
        {
            APP,
            WEB
        }

    }
}
