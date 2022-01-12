using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TwinCAT.Ads;

namespace VisionHalcon11CSVS19
{
    public partial class MainWindowsOldShape : Form
    {
        public MainWindowsOldShape(string[] args)
        {
            InitializeComponent();
            TwincatInterface = new TTwincatinterface(args);
            Cam.InitHalcon(ref hwcVideo, ref TwincatInterface);
            Cam.ConnectionCam();
            timer1.Enabled = true;
        }

        private Camera Cam = new Camera();
        private TTwincatinterface TwincatInterface;

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            TwincatInterface.readTcAllVisionData();
            UpdateUI();
        }

        private void UpdateUI()
        {
            TTwincatinterface.VISION_REQUEST Request = 0;
            String RefFileName = "";
            //String FileName = "";

            Request = TwincatInterface.getVisionRequest();
            switch (Request)
            {
                case TTwincatinterface.VISION_REQUEST.VR_None:
                    // No command
                    break;
                case TTwincatinterface.VISION_REQUEST.VR_Init:
                    // Initialisation for file reference
                    RefFileName = TwincatInterface.getRefFileName();
                    //FileName = "c:\\vision\\reference\\" + "shm";
                    if (Cam.InitVision(RefFileName))
                    {
                        lbFileRefName.Text = RefFileName;
                        TwincatInterface.SetRequestError(false);
                        TwincatInterface.SetReadyData(true);
                    }
                    else
                    {
                        TwincatInterface.SetRequestError(true);
                        TwincatInterface.SetReadyData(false);
                    }
                    break;
                case TTwincatinterface.VISION_REQUEST.VR_GrabImage:

                    break;
                case TTwincatinterface.VISION_REQUEST.VR_Analyse:
                    break;
                default:
                    break;
            }

            TwincatInterface.ClearRequest();
        }
    }
}
