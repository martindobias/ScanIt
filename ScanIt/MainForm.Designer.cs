namespace cz.martindobias.ScanIt
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.autostartCheckBox = new System.Windows.Forms.CheckBox();
            this.startMinimizedCheckBox = new System.Windows.Forms.CheckBox();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.serverStatusText = new System.Windows.Forms.ToolStripStatusLabel();
            this.trayIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.trayContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.startServerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sourceComboBox = new System.Windows.Forms.ComboBox();
            this.sourceLabel = new System.Windows.Forms.Label();
            this.startServerButton = new System.Windows.Forms.Button();
            this.quitButton = new System.Windows.Forms.Button();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.portUpDown = new System.Windows.Forms.NumericUpDown();
            this.portLabel = new System.Windows.Forms.Label();
            this.scanButton = new System.Windows.Forms.Button();
            this.xUpDown = new System.Windows.Forms.NumericUpDown();
            this.yUpDown = new System.Windows.Forms.NumericUpDown();
            this.xLabel = new System.Windows.Forms.Label();
            this.yLabel = new System.Windows.Forms.Label();
            this.hLabel = new System.Windows.Forms.Label();
            this.wLabel = new System.Windows.Forms.Label();
            this.hUpDown = new System.Windows.Forms.NumericUpDown();
            this.wUpDown = new System.Windows.Forms.NumericUpDown();
            this.encodingLabel = new System.Windows.Forms.Label();
            this.encodingComboBox = new System.Windows.Forms.ComboBox();
            this.blackAndWhiteRadioButton = new System.Windows.Forms.RadioButton();
            this.greyscaleRadioButton = new System.Windows.Forms.RadioButton();
            this.colourRadioButton = new System.Windows.Forms.RadioButton();
            this.adfCheckBox = new System.Windows.Forms.CheckBox();
            this.dpiLabel = new System.Windows.Forms.Label();
            this.dpiComboBox = new System.Windows.Forms.ComboBox();
            this.pdfCheckBox = new System.Windows.Forms.CheckBox();
            this.statusStrip.SuspendLayout();
            this.trayContextMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.portUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.yUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.wUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // autostartCheckBox
            // 
            this.autostartCheckBox.AutoSize = true;
            this.autostartCheckBox.Location = new System.Drawing.Point(206, 12);
            this.autostartCheckBox.Name = "autostartCheckBox";
            this.autostartCheckBox.Size = new System.Drawing.Size(99, 17);
            this.autostartCheckBox.TabIndex = 0;
            this.autostartCheckBox.Text = "autostart server";
            this.autostartCheckBox.UseVisualStyleBackColor = true;
            this.autostartCheckBox.CheckedChanged += new System.EventHandler(this.autostartCheckBox_CheckedChanged);
            // 
            // startMinimizedCheckBox
            // 
            this.startMinimizedCheckBox.AutoSize = true;
            this.startMinimizedCheckBox.Location = new System.Drawing.Point(206, 35);
            this.startMinimizedCheckBox.Name = "startMinimizedCheckBox";
            this.startMinimizedCheckBox.Size = new System.Drawing.Size(94, 17);
            this.startMinimizedCheckBox.TabIndex = 1;
            this.startMinimizedCheckBox.Text = "start minimized";
            this.startMinimizedCheckBox.UseVisualStyleBackColor = true;
            this.startMinimizedCheckBox.CheckedChanged += new System.EventHandler(this.startMinimizedCheckBox_CheckedChanged);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.serverStatusText});
            this.statusStrip.Location = new System.Drawing.Point(0, 261);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.statusStrip.Size = new System.Drawing.Size(508, 24);
            this.statusStrip.SizingGrip = false;
            this.statusStrip.TabIndex = 2;
            this.statusStrip.Text = "statusStrip";
            // 
            // serverStatusText
            // 
            this.serverStatusText.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.serverStatusText.Name = "serverStatusText";
            this.serverStatusText.Size = new System.Drawing.Size(80, 19);
            this.serverStatusText.Text = "ScanIt offline";
            // 
            // trayIcon
            // 
            this.trayIcon.ContextMenuStrip = this.trayContextMenuStrip;
            this.trayIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("trayIcon.Icon")));
            this.trayIcon.Text = "ScanIt offline";
            this.trayIcon.Visible = true;
            this.trayIcon.Click += new System.EventHandler(this.trayIcon_Click);
            this.trayIcon.DoubleClick += new System.EventHandler(this.trayIcon_DoubleClick);
            // 
            // trayContextMenuStrip
            // 
            this.trayContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startServerToolStripMenuItem,
            this.quitToolStripMenuItem});
            this.trayContextMenuStrip.Name = "trayContextMenuStrip";
            this.trayContextMenuStrip.Size = new System.Drawing.Size(133, 48);
            // 
            // startServerToolStripMenuItem
            // 
            this.startServerToolStripMenuItem.Name = "startServerToolStripMenuItem";
            this.startServerToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.startServerToolStripMenuItem.Text = "Start server";
            this.startServerToolStripMenuItem.Click += new System.EventHandler(this.startServerToolStripMenuItem_Click);
            // 
            // quitToolStripMenuItem
            // 
            this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            this.quitToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.quitToolStripMenuItem.Text = "Quit";
            this.quitToolStripMenuItem.Click += new System.EventHandler(this.quitToolStripMenuItem_Click);
            // 
            // sourceComboBox
            // 
            this.sourceComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.sourceComboBox.FormattingEnabled = true;
            this.sourceComboBox.Location = new System.Drawing.Point(15, 25);
            this.sourceComboBox.Name = "sourceComboBox";
            this.sourceComboBox.Size = new System.Drawing.Size(172, 21);
            this.sourceComboBox.TabIndex = 3;
            this.sourceComboBox.SelectedValueChanged += new System.EventHandler(this.sourceComboBox_SelectedValueChanged);
            // 
            // sourceLabel
            // 
            this.sourceLabel.AutoSize = true;
            this.sourceLabel.Location = new System.Drawing.Point(12, 9);
            this.sourceLabel.Name = "sourceLabel";
            this.sourceLabel.Size = new System.Drawing.Size(78, 13);
            this.sourceLabel.TabIndex = 4;
            this.sourceLabel.Text = "TWAIN source";
            // 
            // startServerButton
            // 
            this.startServerButton.Location = new System.Drawing.Point(15, 232);
            this.startServerButton.Name = "startServerButton";
            this.startServerButton.Size = new System.Drawing.Size(172, 23);
            this.startServerButton.TabIndex = 6;
            this.startServerButton.Text = "Start server";
            this.startServerButton.UseVisualStyleBackColor = true;
            this.startServerButton.Click += new System.EventHandler(this.startServerButton_Click);
            // 
            // quitButton
            // 
            this.quitButton.Location = new System.Drawing.Point(206, 232);
            this.quitButton.Name = "quitButton";
            this.quitButton.Size = new System.Drawing.Size(127, 23);
            this.quitButton.TabIndex = 7;
            this.quitButton.Text = "Quit";
            this.quitButton.UseVisualStyleBackColor = true;
            this.quitButton.Click += new System.EventHandler(this.quitButton_Click);
            // 
            // pictureBox
            // 
            this.pictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox.Location = new System.Drawing.Point(349, 20);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(147, 223);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox.TabIndex = 8;
            this.pictureBox.TabStop = false;
            // 
            // portUpDown
            // 
            this.portUpDown.Location = new System.Drawing.Point(206, 75);
            this.portUpDown.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.portUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.portUpDown.Name = "portUpDown";
            this.portUpDown.Size = new System.Drawing.Size(127, 20);
            this.portUpDown.TabIndex = 9;
            this.portUpDown.Value = new decimal(new int[] {
            8080,
            0,
            0,
            0});
            this.portUpDown.ValueChanged += new System.EventHandler(this.portUpDown_ValueChanged);
            // 
            // portLabel
            // 
            this.portLabel.AutoSize = true;
            this.portLabel.Location = new System.Drawing.Point(206, 59);
            this.portLabel.Name = "portLabel";
            this.portLabel.Size = new System.Drawing.Size(25, 13);
            this.portLabel.TabIndex = 10;
            this.portLabel.Text = "port";
            // 
            // scanButton
            // 
            this.scanButton.Enabled = false;
            this.scanButton.Location = new System.Drawing.Point(411, 211);
            this.scanButton.Name = "scanButton";
            this.scanButton.Size = new System.Drawing.Size(75, 23);
            this.scanButton.TabIndex = 11;
            this.scanButton.Text = "Test scan";
            this.scanButton.UseVisualStyleBackColor = true;
            this.scanButton.Click += new System.EventHandler(this.scanButton_Click);
            // 
            // xUpDown
            // 
            this.xUpDown.Location = new System.Drawing.Point(15, 65);
            this.xUpDown.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.xUpDown.Name = "xUpDown";
            this.xUpDown.Size = new System.Drawing.Size(83, 20);
            this.xUpDown.TabIndex = 12;
            this.xUpDown.ValueChanged += new System.EventHandler(this.xUpDown_ValueChanged);
            // 
            // yUpDown
            // 
            this.yUpDown.Location = new System.Drawing.Point(104, 65);
            this.yUpDown.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.yUpDown.Name = "yUpDown";
            this.yUpDown.Size = new System.Drawing.Size(83, 20);
            this.yUpDown.TabIndex = 13;
            this.yUpDown.ValueChanged += new System.EventHandler(this.yUpDown_ValueChanged);
            // 
            // xLabel
            // 
            this.xLabel.AutoSize = true;
            this.xLabel.Location = new System.Drawing.Point(12, 49);
            this.xLabel.Name = "xLabel";
            this.xLabel.Size = new System.Drawing.Size(14, 13);
            this.xLabel.TabIndex = 14;
            this.xLabel.Text = "X";
            // 
            // yLabel
            // 
            this.yLabel.AutoSize = true;
            this.yLabel.Location = new System.Drawing.Point(101, 49);
            this.yLabel.Name = "yLabel";
            this.yLabel.Size = new System.Drawing.Size(14, 13);
            this.yLabel.TabIndex = 15;
            this.yLabel.Text = "Y";
            // 
            // hLabel
            // 
            this.hLabel.AutoSize = true;
            this.hLabel.Location = new System.Drawing.Point(101, 88);
            this.hLabel.Name = "hLabel";
            this.hLabel.Size = new System.Drawing.Size(36, 13);
            this.hLabel.TabIndex = 19;
            this.hLabel.Text = "height";
            // 
            // wLabel
            // 
            this.wLabel.AutoSize = true;
            this.wLabel.Location = new System.Drawing.Point(12, 88);
            this.wLabel.Name = "wLabel";
            this.wLabel.Size = new System.Drawing.Size(32, 13);
            this.wLabel.TabIndex = 18;
            this.wLabel.Text = "width";
            // 
            // hUpDown
            // 
            this.hUpDown.Location = new System.Drawing.Point(104, 104);
            this.hUpDown.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.hUpDown.Name = "hUpDown";
            this.hUpDown.Size = new System.Drawing.Size(83, 20);
            this.hUpDown.TabIndex = 17;
            this.hUpDown.ValueChanged += new System.EventHandler(this.hUpDown_ValueChanged);
            // 
            // wUpDown
            // 
            this.wUpDown.Location = new System.Drawing.Point(15, 104);
            this.wUpDown.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.wUpDown.Name = "wUpDown";
            this.wUpDown.Size = new System.Drawing.Size(83, 20);
            this.wUpDown.TabIndex = 16;
            this.wUpDown.ValueChanged += new System.EventHandler(this.wUpDown_ValueChanged);
            // 
            // encodingLabel
            // 
            this.encodingLabel.AutoSize = true;
            this.encodingLabel.Location = new System.Drawing.Point(15, 166);
            this.encodingLabel.Name = "encodingLabel";
            this.encodingLabel.Size = new System.Drawing.Size(44, 13);
            this.encodingLabel.TabIndex = 20;
            this.encodingLabel.Text = "Encode";
            // 
            // encodingComboBox
            // 
            this.encodingComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.encodingComboBox.FormattingEnabled = true;
            this.encodingComboBox.Items.AddRange(new object[] {
            "",
            "base64"});
            this.encodingComboBox.Location = new System.Drawing.Point(15, 183);
            this.encodingComboBox.Name = "encodingComboBox";
            this.encodingComboBox.Size = new System.Drawing.Size(172, 21);
            this.encodingComboBox.TabIndex = 21;
            this.encodingComboBox.SelectedValueChanged += new System.EventHandler(this.encodingComboBox_SelectedValueChanged);
            // 
            // blackAndWhiteRadioButton
            // 
            this.blackAndWhiteRadioButton.AutoSize = true;
            this.blackAndWhiteRadioButton.Location = new System.Drawing.Point(206, 203);
            this.blackAndWhiteRadioButton.Name = "blackAndWhiteRadioButton";
            this.blackAndWhiteRadioButton.Size = new System.Drawing.Size(88, 17);
            this.blackAndWhiteRadioButton.TabIndex = 22;
            this.blackAndWhiteRadioButton.TabStop = true;
            this.blackAndWhiteRadioButton.Text = "black && white";
            this.blackAndWhiteRadioButton.UseVisualStyleBackColor = true;
            this.blackAndWhiteRadioButton.CheckedChanged += new System.EventHandler(this.colourRadioButton_CheckedChanged);
            // 
            // greyscaleRadioButton
            // 
            this.greyscaleRadioButton.AutoSize = true;
            this.greyscaleRadioButton.Location = new System.Drawing.Point(206, 180);
            this.greyscaleRadioButton.Name = "greyscaleRadioButton";
            this.greyscaleRadioButton.Size = new System.Drawing.Size(70, 17);
            this.greyscaleRadioButton.TabIndex = 23;
            this.greyscaleRadioButton.TabStop = true;
            this.greyscaleRadioButton.Text = "greyscale";
            this.greyscaleRadioButton.UseVisualStyleBackColor = true;
            this.greyscaleRadioButton.CheckedChanged += new System.EventHandler(this.colourRadioButton_CheckedChanged);
            // 
            // colourRadioButton
            // 
            this.colourRadioButton.AutoSize = true;
            this.colourRadioButton.Location = new System.Drawing.Point(206, 157);
            this.colourRadioButton.Name = "colourRadioButton";
            this.colourRadioButton.Size = new System.Drawing.Size(54, 17);
            this.colourRadioButton.TabIndex = 24;
            this.colourRadioButton.TabStop = true;
            this.colourRadioButton.Text = "colour";
            this.colourRadioButton.UseVisualStyleBackColor = true;
            this.colourRadioButton.CheckedChanged += new System.EventHandler(this.colourRadioButton_CheckedChanged);
            // 
            // adfCheckBox
            // 
            this.adfCheckBox.AutoSize = true;
            this.adfCheckBox.Location = new System.Drawing.Point(206, 107);
            this.adfCheckBox.Name = "adfCheckBox";
            this.adfCheckBox.Size = new System.Drawing.Size(67, 17);
            this.adfCheckBox.TabIndex = 25;
            this.adfCheckBox.Text = "use ADF";
            this.adfCheckBox.UseVisualStyleBackColor = true;
            this.adfCheckBox.CheckedChanged += new System.EventHandler(this.adfCheckBox_CheckedChanged);
            // 
            // dpiLabel
            // 
            this.dpiLabel.AutoSize = true;
            this.dpiLabel.Location = new System.Drawing.Point(15, 127);
            this.dpiLabel.Name = "dpiLabel";
            this.dpiLabel.Size = new System.Drawing.Size(25, 13);
            this.dpiLabel.TabIndex = 29;
            this.dpiLabel.Text = "DPI";
            // 
            // dpiComboBox
            // 
            this.dpiComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dpiComboBox.FormattingEnabled = true;
            this.dpiComboBox.Items.AddRange(new object[] {
            "150",
            "200",
            "300",
            "600"});
            this.dpiComboBox.Location = new System.Drawing.Point(15, 143);
            this.dpiComboBox.Name = "dpiComboBox";
            this.dpiComboBox.Size = new System.Drawing.Size(172, 21);
            this.dpiComboBox.TabIndex = 30;
            this.dpiComboBox.SelectedValueChanged += new System.EventHandler(this.dpiComboBox_SelectedValueChanged);
            // 
            // pdfCheckBox
            // 
            this.pdfCheckBox.AutoSize = true;
            this.pdfCheckBox.Location = new System.Drawing.Point(206, 130);
            this.pdfCheckBox.Name = "pdfCheckBox";
            this.pdfCheckBox.Size = new System.Drawing.Size(76, 17);
            this.pdfCheckBox.TabIndex = 31;
            this.pdfCheckBox.Text = "serve PDF";
            this.pdfCheckBox.UseVisualStyleBackColor = true;
            this.pdfCheckBox.CheckedChanged += new System.EventHandler(this.pdfCheckBox_CheckedChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(508, 285);
            this.Controls.Add(this.pdfCheckBox);
            this.Controls.Add(this.dpiComboBox);
            this.Controls.Add(this.dpiLabel);
            this.Controls.Add(this.adfCheckBox);
            this.Controls.Add(this.encodingComboBox);
            this.Controls.Add(this.colourRadioButton);
            this.Controls.Add(this.greyscaleRadioButton);
            this.Controls.Add(this.blackAndWhiteRadioButton);
            this.Controls.Add(this.hLabel);
            this.Controls.Add(this.encodingLabel);
            this.Controls.Add(this.wLabel);
            this.Controls.Add(this.hUpDown);
            this.Controls.Add(this.wUpDown);
            this.Controls.Add(this.yLabel);
            this.Controls.Add(this.xLabel);
            this.Controls.Add(this.yUpDown);
            this.Controls.Add(this.xUpDown);
            this.Controls.Add(this.scanButton);
            this.Controls.Add(this.portLabel);
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.portUpDown);
            this.Controls.Add(this.quitButton);
            this.Controls.Add(this.sourceLabel);
            this.Controls.Add(this.startServerButton);
            this.Controls.Add(this.sourceComboBox);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.autostartCheckBox);
            this.Controls.Add(this.startMinimizedCheckBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "ScanIt";
            this.Activated += new System.EventHandler(this.MainForm_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.trayContextMenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.portUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.yUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.wUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox autostartCheckBox;
        private System.Windows.Forms.CheckBox startMinimizedCheckBox;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel serverStatusText;
        private System.Windows.Forms.NotifyIcon trayIcon;
        private System.Windows.Forms.ComboBox sourceComboBox;
        private System.Windows.Forms.Label sourceLabel;
        private System.Windows.Forms.ContextMenuStrip trayContextMenuStrip;
        private System.Windows.Forms.Button startServerButton;
        private System.Windows.Forms.Button quitButton;
        private System.Windows.Forms.ToolStripMenuItem startServerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quitToolStripMenuItem;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.NumericUpDown portUpDown;
        private System.Windows.Forms.Label portLabel;
        private System.Windows.Forms.Button scanButton;
        private System.Windows.Forms.NumericUpDown xUpDown;
        private System.Windows.Forms.NumericUpDown yUpDown;
        private System.Windows.Forms.Label xLabel;
        private System.Windows.Forms.Label yLabel;
        private System.Windows.Forms.Label hLabel;
        private System.Windows.Forms.Label wLabel;
        private System.Windows.Forms.NumericUpDown hUpDown;
        private System.Windows.Forms.NumericUpDown wUpDown;
        private System.Windows.Forms.Label encodingLabel;
        private System.Windows.Forms.ComboBox encodingComboBox;
        private System.Windows.Forms.RadioButton blackAndWhiteRadioButton;
        private System.Windows.Forms.RadioButton greyscaleRadioButton;
        private System.Windows.Forms.RadioButton colourRadioButton;
        private System.Windows.Forms.CheckBox adfCheckBox;
        private System.Windows.Forms.Label dpiLabel;
        private System.Windows.Forms.ComboBox dpiComboBox;
        private System.Windows.Forms.CheckBox pdfCheckBox;
    }
}

