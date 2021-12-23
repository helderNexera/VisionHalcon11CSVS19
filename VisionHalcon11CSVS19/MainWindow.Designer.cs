
namespace VisionHalcon11CSVS19
{
    partial class MainWindow
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
            this.pnlGeneralInfo = new System.Windows.Forms.Panel();
            this.pnlMenu = new System.Windows.Forms.Panel();
            this.pnlOptionMenu = new System.Windows.Forms.Panel();
            this.pnlRTInfo = new System.Windows.Forms.Panel();
            this.pbImage = new System.Windows.Forms.PictureBox();
            this.pnlTools = new System.Windows.Forms.Panel();
            this.pnlDatas = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlGeneralInfo
            // 
            this.pnlGeneralInfo.BackColor = System.Drawing.SystemColors.Control;
            this.pnlGeneralInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlGeneralInfo.Location = new System.Drawing.Point(0, 0);
            this.pnlGeneralInfo.Name = "pnlGeneralInfo";
            this.pnlGeneralInfo.Size = new System.Drawing.Size(1160, 80);
            this.pnlGeneralInfo.TabIndex = 0;
            // 
            // pnlMenu
            // 
            this.pnlMenu.BackColor = System.Drawing.SystemColors.Control;
            this.pnlMenu.Location = new System.Drawing.Point(0, 86);
            this.pnlMenu.Name = "pnlMenu";
            this.pnlMenu.Size = new System.Drawing.Size(150, 572);
            this.pnlMenu.TabIndex = 1;
            // 
            // pnlOptionMenu
            // 
            this.pnlOptionMenu.BackColor = System.Drawing.SystemColors.Control;
            this.pnlOptionMenu.Location = new System.Drawing.Point(156, 86);
            this.pnlOptionMenu.MaximumSize = new System.Drawing.Size(2592, 1944);
            this.pnlOptionMenu.MinimumSize = new System.Drawing.Size(4, 3);
            this.pnlOptionMenu.Name = "pnlOptionMenu";
            this.pnlOptionMenu.Size = new System.Drawing.Size(1004, 80);
            this.pnlOptionMenu.TabIndex = 2;
            // 
            // pnlRTInfo
            // 
            this.pnlRTInfo.BackColor = System.Drawing.SystemColors.Control;
            this.pnlRTInfo.Location = new System.Drawing.Point(0, 664);
            this.pnlRTInfo.Name = "pnlRTInfo";
            this.pnlRTInfo.Size = new System.Drawing.Size(804, 80);
            this.pnlRTInfo.TabIndex = 3;
            // 
            // pbImage
            // 
            this.pbImage.BackColor = System.Drawing.SystemColors.Control;
            this.pbImage.Location = new System.Drawing.Point(156, 172);
            this.pbImage.MaximumSize = new System.Drawing.Size(2592, 1944);
            this.pbImage.MinimumSize = new System.Drawing.Size(4, 3);
            this.pbImage.Name = "pbImage";
            this.pbImage.Size = new System.Drawing.Size(648, 486);
            this.pbImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbImage.TabIndex = 4;
            this.pbImage.TabStop = false;
            this.pbImage.WaitOnLoad = true;
            // 
            // pnlTools
            // 
            this.pnlTools.BackColor = System.Drawing.SystemColors.Control;
            this.pnlTools.Location = new System.Drawing.Point(810, 172);
            this.pnlTools.Name = "pnlTools";
            this.pnlTools.Size = new System.Drawing.Size(350, 280);
            this.pnlTools.TabIndex = 5;
            // 
            // pnlDatas
            // 
            this.pnlDatas.BackColor = System.Drawing.SystemColors.Control;
            this.pnlDatas.Location = new System.Drawing.Point(810, 458);
            this.pnlDatas.Name = "pnlDatas";
            this.pnlDatas.Size = new System.Drawing.Size(350, 286);
            this.pnlDatas.TabIndex = 6;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SkyBlue;
            this.ClientSize = new System.Drawing.Size(1160, 745);
            this.Controls.Add(this.pnlDatas);
            this.Controls.Add(this.pnlTools);
            this.Controls.Add(this.pbImage);
            this.Controls.Add(this.pnlRTInfo);
            this.Controls.Add(this.pnlOptionMenu);
            this.Controls.Add(this.pnlMenu);
            this.Controls.Add(this.pnlGeneralInfo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "MainWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Nexera - NexVision";
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlGeneralInfo;
        private System.Windows.Forms.Panel pnlMenu;
        private System.Windows.Forms.Panel pnlOptionMenu;
        private System.Windows.Forms.Panel pnlRTInfo;
        private System.Windows.Forms.PictureBox pbImage;
        private System.Windows.Forms.Panel pnlTools;
        private System.Windows.Forms.Panel pnlDatas;
    }
}