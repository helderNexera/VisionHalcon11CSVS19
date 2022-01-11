
namespace VisionHalcon11CSVS19
{
    partial class MainWindowsOldShape
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbFileRefName = new System.Windows.Forms.Label();
            this.lbFileRef = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.hwcVideo = new HalconDotNet.HWindowControl();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lbFileRefName);
            this.panel1.Controls.Add(this.lbFileRef);
            this.panel1.Controls.Add(this.btnExit);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 180);
            this.panel1.TabIndex = 1;
            // 
            // lbFileRefName
            // 
            this.lbFileRefName.AutoSize = true;
            this.lbFileRefName.Location = new System.Drawing.Point(57, 157);
            this.lbFileRefName.Name = "lbFileRefName";
            this.lbFileRefName.Size = new System.Drawing.Size(53, 13);
            this.lbFileRefName.TabIndex = 2;
            this.lbFileRefName.Text = "Ref name";
            // 
            // lbFileRef
            // 
            this.lbFileRef.AutoSize = true;
            this.lbFileRef.Location = new System.Drawing.Point(3, 157);
            this.lbFileRef.Name = "lbFileRef";
            this.lbFileRef.Size = new System.Drawing.Size(49, 13);
            this.lbFileRef.TabIndex = 1;
            this.lbFileRef.Text = "File Ref :";
            // 
            // btnExit
            // 
            this.btnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Location = new System.Drawing.Point(668, 12);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(120, 60);
            this.btnExit.TabIndex = 0;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // hwcVideo
            // 
            this.hwcVideo.BackColor = System.Drawing.Color.Black;
            this.hwcVideo.BorderColor = System.Drawing.Color.Black;
            this.hwcVideo.ImagePart = new System.Drawing.Rectangle(0, 0, 640, 480);
            this.hwcVideo.Location = new System.Drawing.Point(12, 198);
            this.hwcVideo.Name = "hwcVideo";
            this.hwcVideo.Size = new System.Drawing.Size(800, 600);
            this.hwcVideo.TabIndex = 2;
            this.hwcVideo.WindowSize = new System.Drawing.Size(800, 600);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // MainWindowsOldShape
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(825, 810);
            this.Controls.Add(this.hwcVideo);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "MainWindowsOldShape";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Nexera - NexVision Software";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnExit;
        private HalconDotNet.HWindowControl hwcVideo;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lbFileRefName;
        private System.Windows.Forms.Label lbFileRef;
    }
}