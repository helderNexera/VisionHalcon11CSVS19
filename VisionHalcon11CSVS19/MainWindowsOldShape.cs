using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
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
            if (!TwincatInterface.IsConnected())
            {
                MessageBox.Show(null, "Impossible to connect to the automation's soft", "Connection Error", MessageBoxButtons.OK);
                Application.Exit();
            }
            Cam.InitHalcon(ref hwcVideo);
            Cam.ConnectionCam();
            cbxRealtime.Checked = false;
            cbxCenter.Checked = false;
            cbxCenter.Enabled = cbxRealtime.Checked;
            cbxManualAnalyse.Enabled = cbxCenter.Checked;
            timer2.Enabled = false;
            timer1.Enabled = true;
        }

        private readonly Camera Cam = new Camera();
        private readonly TTwincatinterface TwincatInterface;
        private static bool Alive = false;

        private void UpdateUI()
        {
            TTwincatinterface.VISION_REQUEST Request;
            TTwincatinterface.VISION_PART_DATA Data = new TTwincatinterface.VISION_PART_DATA();
            String RefFileName;

            Request = TwincatInterface.getVisionRequest();
            switch (Request)
            {
                case TTwincatinterface.VISION_REQUEST.VR_None:
                    // No command
                    break;
                case TTwincatinterface.VISION_REQUEST.VR_Init:
                    // Initialisation for file reference
                    RefFileName = TwincatInterface.getRefFileName();
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
                        TwincatInterface.ClearRequest();
                        MessageBox.Show("Le fichier " + RefFileName + " n'existe pas!");
                    }
                    break;
                case TTwincatinterface.VISION_REQUEST.VR_GrabImage:
                    // Try take a picture
                    if (Cam.TakePicture())
                    {
                        TwincatInterface.SetRequestError(false);
                        if (cbxShowImage.Checked)
                        {
                            Cam.DisplayImage();
                        }
                    }
                    else
                    {
                        TwincatInterface.SetRequestError(true);
                    }
                    break;
                case TTwincatinterface.VISION_REQUEST.VR_Analyse:
                    // Image must be analysed
                    Cam.DoAnalysis(ref Data, cbxShowImage.Checked);

                    TwincatInterface.ClearRequest();
                    TwincatInterface.WriteVisionData(ref Data);
                    break;
                default:
                    break;
            }

            if (Alive)
            {
                TwincatInterface.SetAliveData(true);
            }
            else
            {
                TwincatInterface.SetAliveData(false);
            }
            Alive = !Alive;

            TwincatInterface.SetConnectedData(true);

            TwincatInterface.ClearRequest();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (TwincatInterface.IsConnected())
            {
                TwincatInterface.readTcAllVisionData();
                UpdateUI();
            }
            else
            {
                timer1.Enabled = false;
                MessageBox.Show("ADS error. No connection to the automation's soft. \nQuit Application.");
                Application.Exit();
            }
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            DialogResult result = MessageBox.Show(null, "Are you sur you want to quit ?", "Confirmation request", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                this.Close();
            }
            else
            {
                timer1.Enabled = true;
            }
        }

        private void btnInit_Click(object sender, EventArgs e)
        {
            string RefName = TwincatInterface.getRefFileName();
            Cam.InitVision(RefName);
            lbFileRefName.Text = RefName;
        }

        private void cbxRealtime_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxRealtime.Checked)
            {
                DialogResult result = MessageBox.Show(null, "Do you want to pass in manual mode?", "Confirmation", MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {
                    timer1.Enabled = false;
                    TwincatInterface.SetReadyData(false);
                    TwincatInterface.ClearRequest();
                    cbxCenter.Enabled = true;
                    cbxManualAnalyse.Enabled = true;
                    timer2.Enabled = true;
                }
                else
                {
                    timer2.Enabled = false;
                    timer1.Enabled = true;
                    cbxRealtime.Checked = false;
                    cbxCenter.Checked = false;
                    cbxManualAnalyse.Checked = false;
                    cbxCenter.Enabled = cbxRealtime.Checked;
                    cbxManualAnalyse.Enabled = cbxRealtime.Checked;
                }
            }
            else
            {
                timer2.Enabled = false;
                timer1.Enabled = true;
                cbxRealtime.Checked = false;
                cbxCenter.Checked = false;
                cbxManualAnalyse.Checked = false;
                cbxCenter.Enabled = cbxRealtime.Checked;
                cbxManualAnalyse.Enabled = cbxRealtime.Checked;
            }

        }

        private void DisplayManualMode(bool Zoom, bool Analyse)
        {
            TTwincatinterface.VISION_PART_DATA Data = new TTwincatinterface.VISION_PART_DATA();

            Cam.DisplayImageManualMode(Zoom, Analyse, ref Data);
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            DisplayManualMode(cbxCenter.Checked, cbxManualAnalyse.Checked);
        }

        private void cbxCenter_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxCenter.Checked)
            {
                cbxManualAnalyse.Checked = !cbxCenter.Checked;
            }
        }

        private void cbxManualAnalyse_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxManualAnalyse.Checked)
            {
                cbxCenter.Checked = !cbxManualAnalyse.Checked;
            }
        }
    }
}
