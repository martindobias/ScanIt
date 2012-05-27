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

namespace cz.martindobias.ScanIt
{
    public partial class MainForm : Form
    {
        private HttpServer server = null;
        private bool loaded = false;
        private Twain twain = null;

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
            this.server.Handlers.Add(new EmptyHandler());
            this.server.Handlers.Add(new StatusHandler());

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

        }

        private void sourceComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.twain_source = (string)this.sourceComboBox.SelectedItem;
            this.scanButton.Enabled = !"".Equals(Properties.Settings.Default.twain_source);
        }
    }
}
