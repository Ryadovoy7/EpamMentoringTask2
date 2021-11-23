namespace FileLister
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.textboxStartingPoint = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonStop = new System.Windows.Forms.Button();
            this.checkboxAddEventTexts = new System.Windows.Forms.CheckBox();
            this.checkboxStopAfter = new System.Windows.Forms.CheckBox();
            this.buttonStart = new System.Windows.Forms.Button();
            this.updownStopAfter = new System.Windows.Forms.NumericUpDown();
            this.textboxMustContain = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textboxResult = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.updownStopAfter)).BeginInit();
            this.SuspendLayout();
            // 
            // textboxStartingPoint
            // 
            this.textboxStartingPoint.Dock = System.Windows.Forms.DockStyle.Top;
            this.textboxStartingPoint.Location = new System.Drawing.Point(8, 23);
            this.textboxStartingPoint.Name = "textboxStartingPoint";
            this.textboxStartingPoint.Size = new System.Drawing.Size(526, 23);
            this.textboxStartingPoint.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Starting point";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Location = new System.Drawing.Point(8, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "Path must contain";
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.Controls.Add(this.buttonStop);
            this.panel1.Controls.Add(this.checkboxAddEventTexts);
            this.panel1.Controls.Add(this.checkboxStopAfter);
            this.panel1.Controls.Add(this.buttonStart);
            this.panel1.Controls.Add(this.updownStopAfter);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(8, 631);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(526, 29);
            this.panel1.TabIndex = 4;
            // 
            // buttonStop
            // 
            this.buttonStop.Location = new System.Drawing.Point(84, 3);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(75, 23);
            this.buttonStop.TabIndex = 5;
            this.buttonStop.Text = "Stop";
            this.buttonStop.UseVisualStyleBackColor = true;
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // checkboxAddEventTexts
            // 
            this.checkboxAddEventTexts.AutoSize = true;
            this.checkboxAddEventTexts.Dock = System.Windows.Forms.DockStyle.Right;
            this.checkboxAddEventTexts.Location = new System.Drawing.Point(298, 0);
            this.checkboxAddEventTexts.Name = "checkboxAddEventTexts";
            this.checkboxAddEventTexts.Size = new System.Drawing.Size(108, 29);
            this.checkboxAddEventTexts.TabIndex = 4;
            this.checkboxAddEventTexts.Text = "Add event texts";
            this.checkboxAddEventTexts.UseVisualStyleBackColor = true;
            // 
            // checkboxStopAfter
            // 
            this.checkboxStopAfter.AutoSize = true;
            this.checkboxStopAfter.Dock = System.Windows.Forms.DockStyle.Right;
            this.checkboxStopAfter.Location = new System.Drawing.Point(406, 0);
            this.checkboxStopAfter.Name = "checkboxStopAfter";
            this.checkboxStopAfter.Size = new System.Drawing.Size(80, 29);
            this.checkboxStopAfter.TabIndex = 2;
            this.checkboxStopAfter.Text = "Stop after:";
            this.checkboxStopAfter.UseVisualStyleBackColor = true;
            // 
            // buttonStart
            // 
            this.buttonStart.Location = new System.Drawing.Point(3, 3);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(75, 23);
            this.buttonStart.TabIndex = 0;
            this.buttonStart.Text = "Start";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.button1_Click);
            // 
            // updownStopAfter
            // 
            this.updownStopAfter.Dock = System.Windows.Forms.DockStyle.Right;
            this.updownStopAfter.Location = new System.Drawing.Point(486, 0);
            this.updownStopAfter.Name = "updownStopAfter";
            this.updownStopAfter.Size = new System.Drawing.Size(40, 23);
            this.updownStopAfter.TabIndex = 3;
            // 
            // textboxMustContain
            // 
            this.textboxMustContain.Dock = System.Windows.Forms.DockStyle.Top;
            this.textboxMustContain.Location = new System.Drawing.Point(8, 61);
            this.textboxMustContain.Name = "textboxMustContain";
            this.textboxMustContain.Size = new System.Drawing.Size(526, 23);
            this.textboxMustContain.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Top;
            this.label3.Location = new System.Drawing.Point(8, 84);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 15);
            this.label3.TabIndex = 5;
            this.label3.Text = "Result";
            // 
            // textboxResult
            // 
            this.textboxResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textboxResult.Location = new System.Drawing.Point(8, 99);
            this.textboxResult.Multiline = true;
            this.textboxResult.Name = "textboxResult";
            this.textboxResult.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textboxResult.Size = new System.Drawing.Size(526, 532);
            this.textboxResult.TabIndex = 6;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(542, 668);
            this.Controls.Add(this.textboxResult);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textboxMustContain);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textboxStartingPoint);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Padding = new System.Windows.Forms.Padding(8);
            this.Text = "File system visitor";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.updownStopAfter)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox textboxStartingPoint;
        private Label label1;
        private Label label2;
        private Panel panel1;
        private Button buttonStart;
        private TextBox textboxMustContain;
        private Label label3;
        private TextBox textboxResult;
        private CheckBox checkboxAddEventTexts;
        private NumericUpDown updownStopAfter;
        private CheckBox checkboxStopAfter;
        private Button buttonStop;
    }
}