namespace DesignFormPM
{
    partial class FastReportPM
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
            PMR002000 = new Button();
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            button4 = new Button();
            SuspendLayout();
            // 
            // PMR002000
            // 
            PMR002000.Location = new Point(14, 16);
            PMR002000.Margin = new Padding(3, 4, 3, 4);
            PMR002000.Name = "PMR002000";
            PMR002000.Size = new Size(198, 46);
            PMR002000.TabIndex = 0;
            PMR002000.Text = "PMR00200Summary";
            PMR002000.UseVisualStyleBackColor = true;
            PMR002000.Click += button1_Click;
            // 
            // button1
            // 
            button1.Location = new Point(12, 69);
            button1.Name = "button1";
            button1.Size = new Size(200, 42);
            button1.TabIndex = 1;
            button1.Text = "PMR00600Summary";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click_1;
            // 
            // button2
            // 
            button2.Location = new Point(12, 117);
            button2.Name = "button2";
            button2.Size = new Size(200, 51);
            button2.TabIndex = 2;
            button2.Text = "PMR00601Detail";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.Location = new Point(14, 174);
            button3.Name = "button3";
            button3.Size = new Size(198, 47);
            button3.TabIndex = 3;
            button3.Text = "PMR02400Summary";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button4
            // 
            button4.Location = new Point(14, 227);
            button4.Name = "button4";
            button4.Size = new Size(198, 46);
            button4.TabIndex = 4;
            button4.Text = "PMR02400Detail";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // FastReportPM
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(914, 600);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(PMR002000);
            Margin = new Padding(3, 4, 3, 4);
            Name = "FastReportPM";
            Text = "Form1";
            Load += DesignFormPMR_Load;
            ResumeLayout(false);
        }

        #endregion

        private Button PMR002000;
        private Button button1;
        private Button button2;
        private Button button3;
        private Button button4;
    }
}
