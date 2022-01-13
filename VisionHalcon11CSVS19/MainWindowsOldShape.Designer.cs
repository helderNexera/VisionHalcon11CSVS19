
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
            this.btnInit = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.hwcVideo = new HalconDotNet.HWindowControl();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lbMachine = new System.Windows.Forms.Label();
            this.lbMachineName = new System.Windows.Forms.Label();
            this.lbStatus = new System.Windows.Forms.Label();
            this.lbStatusValue = new System.Windows.Forms.Label();
            this.gbxDisplay = new System.Windows.Forms.GroupBox();
            this.cbxShowImage = new System.Windows.Forms.CheckBox();
            this.cbxCenter = new System.Windows.Forms.CheckBox();
            this.gbxManualMode = new System.Windows.Forms.GroupBox();
            this.cbxRealtime = new System.Windows.Forms.CheckBox();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.cbxManualAnalyse = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            this.gbxDisplay.SuspendLayout();
            this.gbxManualMode.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.gbxManualMode);
            this.panel1.Controls.Add(this.gbxDisplay);
            this.panel1.Controls.Add(this.lbStatusValue);
            this.panel1.Controls.Add(this.lbMachineName);
            this.panel1.Controls.Add(this.lbStatus);
            this.panel1.Controls.Add(this.lbMachine);
            this.panel1.Controls.Add(this.lbFileRefName);
            this.panel1.Controls.Add(this.lbFileRef);
            this.panel1.Controls.Add(this.btnInit);
            this.panel1.Controls.Add(this.btnExit);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 180);
            this.panel1.TabIndex = 1;
            // 
            // lbFileRefName
            // 
            this.lbFileRefName.AutoSize = true;
            this.lbFileRefName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbFileRefName.Location = new System.Drawing.Point(78, 50);
            this.lbFileRefName.Name = "lbFileRefName";
            this.lbFileRefName.Size = new System.Drawing.Size(69, 17);
            this.lbFileRefName.TabIndex = 2;
            this.lbFileRefName.Text = "Ref name";
            // 
            // lbFileRef
            // 
            this.lbFileRef.AutoSize = true;
            this.lbFileRef.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbFileRef.Location = new System.Drawing.Point(3, 50);
            this.lbFileRef.Name = "lbFileRef";
            this.lbFileRef.Size = new System.Drawing.Size(64, 17);
            this.lbFileRef.TabIndex = 1;
            this.lbFileRef.Text = "File Ref :";
            // 
            // btnInit
            // 
            this.btnInit.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInit.Location = new System.Drawing.Point(557, 3);
            this.btnInit.Name = "btnInit";
            this.btnInit.Size = new System.Drawing.Size(120, 60);
            this.btnInit.TabIndex = 0;
            this.btnInit.Text = "Init";
            this.btnInit.UseVisualStyleBackColor = true;
            this.btnInit.Click += new System.EventHandler(this.btnInit_Click);
            // 
            // btnExit
            // 
            this.btnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Location = new System.Drawing.Point(677, 3);
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
            this.hwcVideo.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hwcVideo.ImagePart = new System.Drawing.Rectangle(0, 0, 2592, 1944);
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
            // lbMachine
            // 
            this.lbMachine.AutoSize = true;
            this.lbMachine.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbMachine.Location = new System.Drawing.Point(3, 3);
            this.lbMachine.Name = "lbMachine";
            this.lbMachine.Size = new System.Drawing.Size(69, 17);
            this.lbMachine.TabIndex = 3;
            this.lbMachine.Text = "Machine :";
            // 
            // lbMachineName
            // 
            this.lbMachineName.AutoSize = true;
            this.lbMachineName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbMachineName.Location = new System.Drawing.Point(78, 3);
            this.lbMachineName.Name = "lbMachineName";
            this.lbMachineName.Size = new System.Drawing.Size(57, 17);
            this.lbMachineName.TabIndex = 3;
            this.lbMachineName.Text = "Perlage";
            // 
            // lbStatus
            // 
            this.lbStatus.AutoSize = true;
            this.lbStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbStatus.Location = new System.Drawing.Point(3, 27);
            this.lbStatus.Name = "lbStatus";
            this.lbStatus.Size = new System.Drawing.Size(56, 17);
            this.lbStatus.TabIndex = 3;
            this.lbStatus.Text = "Status :";
            // 
            // lbStatusValue
            // 
            this.lbStatusValue.AutoSize = true;
            this.lbStatusValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbStatusValue.Location = new System.Drawing.Point(78, 27);
            this.lbStatusValue.Name = "lbStatusValue";
            this.lbStatusValue.Size = new System.Drawing.Size(48, 17);
            this.lbStatusValue.TabIndex = 3;
            this.lbStatusValue.Text = "Status";
            // 
            // gbxDisplay
            // 
            this.gbxDisplay.Controls.Add(this.cbxShowImage);
            this.gbxDisplay.Location = new System.Drawing.Point(434, 15);
            this.gbxDisplay.Name = "gbxDisplay";
            this.gbxDisplay.Size = new System.Drawing.Size(117, 48);
            this.gbxDisplay.TabIndex = 4;
            this.gbxDisplay.TabStop = false;
            this.gbxDisplay.Text = "Display";
            // 
            // cbxShowImage
            // 
            this.cbxShowImage.AutoSize = true;
            this.cbxShowImage.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxShowImage.Location = new System.Drawing.Point(7, 20);
            this.cbxShowImage.Name = "cbxShowImage";
            this.cbxShowImage.Size = new System.Drawing.Size(103, 21);
            this.cbxShowImage.TabIndex = 0;
            this.cbxShowImage.Text = "Show image";
            this.cbxShowImage.UseVisualStyleBackColor = true;
            // 
            // cbxCenter
            // 
            this.cbxCenter.AutoSize = true;
            this.cbxCenter.Enabled = false;
            this.cbxCenter.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxCenter.Location = new System.Drawing.Point(6, 51);
            this.cbxCenter.Name = "cbxCenter";
            this.cbxCenter.Size = new System.Drawing.Size(69, 21);
            this.cbxCenter.TabIndex = 1;
            this.cbxCenter.Text = "Center";
            this.cbxCenter.UseVisualStyleBackColor = true;
            this.cbxCenter.CheckedChanged += new System.EventHandler(this.cbxCenter_CheckedChanged);
            // 
            // gbxManualMode
            // 
            this.gbxManualMode.Controls.Add(this.cbxManualAnalyse);
            this.gbxManualMode.Controls.Add(this.cbxRealtime);
            this.gbxManualMode.Controls.Add(this.cbxCenter);
            this.gbxManualMode.Location = new System.Drawing.Point(311, 15);
            this.gbxManualMode.Name = "gbxManualMode";
            this.gbxManualMode.Size = new System.Drawing.Size(117, 107);
            this.gbxManualMode.TabIndex = 5;
            this.gbxManualMode.TabStop = false;
            this.gbxManualMode.Text = "Manual mode";
            // 
            // cbxRealtime
            // 
            this.cbxRealtime.AutoSize = true;
            this.cbxRealtime.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxRealtime.Location = new System.Drawing.Point(6, 24);
            this.cbxRealtime.Name = "cbxRealtime";
            this.cbxRealtime.Size = new System.Drawing.Size(82, 21);
            this.cbxRealtime.TabIndex = 2;
            this.cbxRealtime.Text = "Realtime";
            this.cbxRealtime.UseVisualStyleBackColor = true;
            this.cbxRealtime.CheckedChanged += new System.EventHandler(this.cbxRealtime_CheckedChanged);
            // 
            // timer2
            // 
            this.timer2.Interval = 1000;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // cbxManualAnalyse
            // 
            this.cbxManualAnalyse.AutoSize = true;
            this.cbxManualAnalyse.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxManualAnalyse.Location = new System.Drawing.Point(6, 78);
            this.cbxManualAnalyse.Name = "cbxManualAnalyse";
            this.cbxManualAnalyse.Size = new System.Drawing.Size(99, 21);
            this.cbxManualAnalyse.TabIndex = 3;
            this.cbxManualAnalyse.Text = "Do Analyse";
            this.cbxManualAnalyse.UseVisualStyleBackColor = true;
            this.cbxManualAnalyse.CheckedChanged += new System.EventHandler(this.cbxManualAnalyse_CheckedChanged);
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
            this.gbxDisplay.ResumeLayout(false);
            this.gbxDisplay.PerformLayout();
            this.gbxManualMode.ResumeLayout(false);
            this.gbxManualMode.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnExit;
        private HalconDotNet.HWindowControl hwcVideo;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lbFileRefName;
        private System.Windows.Forms.Label lbFileRef;
        private System.Windows.Forms.Button btnInit;
        private System.Windows.Forms.Label lbMachine;
        private System.Windows.Forms.Label lbStatusValue;
        private System.Windows.Forms.Label lbMachineName;
        private System.Windows.Forms.Label lbStatus;
        private System.Windows.Forms.GroupBox gbxDisplay;
        private System.Windows.Forms.CheckBox cbxShowImage;
        private System.Windows.Forms.CheckBox cbxCenter;
        private System.Windows.Forms.GroupBox gbxManualMode;
        private System.Windows.Forms.CheckBox cbxRealtime;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.CheckBox cbxManualAnalyse;
    }
}