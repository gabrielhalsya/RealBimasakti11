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
            SuspendLayout();
            // 
            // button1
            // 
            PMR002000.Location = new Point(12, 12);
            PMR002000.Name = "button1";
            PMR002000.Size = new Size(500, 274);
            PMR002000.TabIndex = 0;
            PMR002000.Text = "PMR00200Summary";
            PMR002000.UseVisualStyleBackColor = true;
            PMR002000.Click += button1_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(PMR002000);
            Name = "Form1";
            Text = "Form1";
            Load += DesignFormPMR_Load;
            ResumeLayout(false);
        }

        #endregion

        private Button PMR002000;
    }
}
