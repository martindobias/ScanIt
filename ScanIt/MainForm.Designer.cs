namespace ScanIt
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
            this.trayIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.sourceComboBox = new System.Windows.Forms.ComboBox();
            this.sourceLabel = new System.Windows.Forms.Label();
            this.serverStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.serverStatusText = new System.Windows.Forms.ToolStripStatusLabel();
            this.trayContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.startServerButton = new System.Windows.Forms.Button();
            this.quitButton = new System.Windows.Forms.Button();
            this.startServerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.portUpDown = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.statusStrip.SuspendLayout();
            this.trayContextMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.portUpDown)).BeginInit();
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
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.serverStatusText,
            this.serverStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 238);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.statusStrip.Size = new System.Drawing.Size(508, 24);
            this.statusStrip.SizingGrip = false;
            this.statusStrip.TabIndex = 2;
            this.statusStrip.Text = "statusStrip";
            // 
            // trayIcon
            // 
            this.trayIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("trayIcon.Icon")));
            this.trayIcon.Text = "ScanIt";
            this.trayIcon.Visible = true;
            // 
            // sourceComboBox
            // 
            this.sourceComboBox.FormattingEnabled = true;
            this.sourceComboBox.Location = new System.Drawing.Point(15, 25);
            this.sourceComboBox.Name = "sourceComboBox";
            this.sourceComboBox.Size = new System.Drawing.Size(185, 21);
            this.sourceComboBox.TabIndex = 3;
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
            // serverStatusLabel
            // 
            this.serverStatusLabel.Name = "serverStatusLabel";
            this.serverStatusLabel.Size = new System.Drawing.Size(72, 19);
            this.serverStatusLabel.Text = "server status";
            // 
            // serverStatusText
            // 
            this.serverStatusText.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.serverStatusText.Name = "serverStatusText";
            this.serverStatusText.Size = new System.Drawing.Size(45, 19);
            this.serverStatusText.Text = "offline";
            // 
            // trayContextMenuStrip
            // 
            this.trayContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startServerToolStripMenuItem,
            this.quitToolStripMenuItem});
            this.trayContextMenuStrip.Name = "trayContextMenuStrip";
            this.trayContextMenuStrip.Size = new System.Drawing.Size(133, 48);
            // 
            // startServerButton
            // 
            this.startServerButton.Location = new System.Drawing.Point(12, 214);
            this.startServerButton.Name = "startServerButton";
            this.startServerButton.Size = new System.Drawing.Size(241, 23);
            this.startServerButton.TabIndex = 6;
            this.startServerButton.Text = "Start server";
            this.startServerButton.UseVisualStyleBackColor = true;
            // 
            // quitButton
            // 
            this.quitButton.Location = new System.Drawing.Point(259, 214);
            this.quitButton.Name = "quitButton";
            this.quitButton.Size = new System.Drawing.Size(241, 23);
            this.quitButton.TabIndex = 7;
            this.quitButton.Text = "Quit";
            this.quitButton.UseVisualStyleBackColor = true;
            // 
            // startServerToolStripMenuItem
            // 
            this.startServerToolStripMenuItem.Name = "startServerToolStripMenuItem";
            this.startServerToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.startServerToolStripMenuItem.Text = "Start server";
            // 
            // quitToolStripMenuItem
            // 
            this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            this.quitToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.quitToolStripMenuItem.Text = "Quit";
            // 
            // pictureBox
            // 
            this.pictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox.Location = new System.Drawing.Point(339, 12);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(157, 196);
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
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(206, 59);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(25, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "port";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(508, 262);
            this.Controls.Add(this.label1);
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
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.trayContextMenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.portUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox autostartCheckBox;
        private System.Windows.Forms.CheckBox startMinimizedCheckBox;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel serverStatusLabel;
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
        private System.Windows.Forms.Label label1;
    }
}

