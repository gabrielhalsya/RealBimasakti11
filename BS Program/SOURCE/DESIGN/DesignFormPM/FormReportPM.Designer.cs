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
            SuspendLayout();
            // 
            // PMR002000
            // 
            PMR002000.Location = new Point(14, 16);
            PMR002000.Margin = new Padding(3, 4, 3, 4);
            PMR002000.Name = "PMR002000";
            PMR002000.Size = new Size(571, 66);
            PMR002000.TabIndex = 0;
            PMR002000.Text = "PMR00200Summary";
            PMR002000.UseVisualStyleBackColor = true;
            PMR002000.Click += button1_Click;
            // 
            // button1
            // 
            button1.Location = new Point(14, 89);
            button1.Name = "button1";
            button1.Size = new Size(571, 63);
            button1.TabIndex = 1;
            button1.Text = "PMR00600Summary";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click_1;
            // 
            // button2
            // 
            button2.Location = new Point(14, 162);
            button2.Name = "button2";
            button2.Size = new Size(571, 69);
            button2.TabIndex = 2;
            button2.Text = "PMR00601Detail";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // FastReportPM
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(914, 600);
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
    }
}
