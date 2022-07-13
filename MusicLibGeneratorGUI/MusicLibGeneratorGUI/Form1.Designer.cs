namespace MusicLibGeneratorGUI {
    partial class Form1 {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.osuPathEntry = new System.Windows.Forms.TextBox();
            this.targetPathEntry = new System.Windows.Forms.TextBox();
            this.osuPathButton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dupecheck_Checkbox = new System.Windows.Forms.CheckBox();
            this.start_button = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.iniRadio = new System.Windows.Forms.RadioButton();
            this.logging_checkbox = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // osuPathEntry
            // 
            this.osuPathEntry.Location = new System.Drawing.Point(12, 22);
            this.osuPathEntry.Name = "osuPathEntry";
            this.osuPathEntry.Size = new System.Drawing.Size(190, 20);
            this.osuPathEntry.TabIndex = 0;
            this.osuPathEntry.TextChanged += new System.EventHandler(this.osuPathEntry_TextChanged);
            // 
            // targetPathEntry
            // 
            this.targetPathEntry.Location = new System.Drawing.Point(12, 68);
            this.targetPathEntry.Name = "targetPathEntry";
            this.targetPathEntry.Size = new System.Drawing.Size(190, 20);
            this.targetPathEntry.TabIndex = 0;
            this.targetPathEntry.TextChanged += new System.EventHandler(this.targetPathEntry_TextChanged);
            // 
            // osuPathButton
            // 
            this.osuPathButton.Location = new System.Drawing.Point(208, 22);
            this.osuPathButton.Name = "osuPathButton";
            this.osuPathButton.Size = new System.Drawing.Size(22, 20);
            this.osuPathButton.TabIndex = 1;
            this.osuPathButton.Text = "?";
            this.osuPathButton.UseVisualStyleBackColor = true;
            this.osuPathButton.Click += new System.EventHandler(this.osuPathButton_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(208, 68);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(22, 20);
            this.button1.TabIndex = 1;
            this.button1.Text = "?";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "osu! Song Folder";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Target Directory";
            // 
            // dupecheck_Checkbox
            // 
            this.dupecheck_Checkbox.AutoSize = true;
            this.dupecheck_Checkbox.Location = new System.Drawing.Point(112, 101);
            this.dupecheck_Checkbox.Name = "dupecheck_Checkbox";
            this.dupecheck_Checkbox.Size = new System.Drawing.Size(117, 17);
            this.dupecheck_Checkbox.TabIndex = 3;
            this.dupecheck_Checkbox.Text = "Skip same osu! IDs";
            this.dupecheck_Checkbox.UseVisualStyleBackColor = true;
            this.dupecheck_Checkbox.CheckedChanged += new System.EventHandler(this.dupecheck_Checkbox_CheckedChanged);
            // 
            // start_button
            // 
            this.start_button.Location = new System.Drawing.Point(13, 167);
            this.start_button.Name = "start_button";
            this.start_button.Size = new System.Drawing.Size(75, 23);
            this.start_button.TabIndex = 4;
            this.start_button.Text = "Start";
            this.start_button.UseVisualStyleBackColor = true;
            this.start_button.Click += new System.EventHandler(this.start_button_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(94, 167);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(136, 23);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar1.TabIndex = 5;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.iniRadio);
            this.groupBox1.Location = new System.Drawing.Point(13, 95);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(93, 66);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Search Type";
            // 
            // iniRadio
            // 
            this.iniRadio.AutoSize = true;
            this.iniRadio.Checked = true;
            this.iniRadio.Location = new System.Drawing.Point(7, 20);
            this.iniRadio.Name = "iniRadio";
            this.iniRadio.Size = new System.Drawing.Size(66, 17);
            this.iniRadio.TabIndex = 0;
            this.iniRadio.TabStop = true;
            this.iniRadio.Text = ".ini index";
            this.iniRadio.UseVisualStyleBackColor = true;
            this.iniRadio.CheckedChanged += new System.EventHandler(this.iniRadio_CheckedChanged);
            // 
            // logging_checkbox
            // 
            this.logging_checkbox.AutoSize = true;
            this.logging_checkbox.Location = new System.Drawing.Point(112, 124);
            this.logging_checkbox.Name = "logging_checkbox";
            this.logging_checkbox.Size = new System.Drawing.Size(100, 17);
            this.logging_checkbox.TabIndex = 3;
            this.logging_checkbox.Text = "Enable Logging";
            this.logging_checkbox.UseVisualStyleBackColor = true;
            this.logging_checkbox.CheckedChanged += new System.EventHandler(this.dupecheck_Checkbox_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(241, 197);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.start_button);
            this.Controls.Add(this.logging_checkbox);
            this.Controls.Add(this.dupecheck_Checkbox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.osuPathButton);
            this.Controls.Add(this.targetPathEntry);
            this.Controls.Add(this.osuPathEntry);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "osu! Song Extractor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox osuPathEntry;
        private System.Windows.Forms.TextBox targetPathEntry;
        private System.Windows.Forms.Button osuPathButton;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox dupecheck_Checkbox;
        private System.Windows.Forms.Button start_button;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton iniRadio;
        private System.Windows.Forms.CheckBox logging_checkbox;
    }
}

