using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalconDotNet;

namespace VisionHalcon11CSVS19
{
    public class Camera
    {
        public bool IsConnected { get; private set; }
        public Camera()
        {
            Framegrabber = new HFramegrabber();
            IsConnected = false;
            HOperatorSet.SetSystem("width", CameraWidth);
            HOperatorSet.SetSystem("height", CameraHeight);
        }
        public void ConnectioCam()
        {
            //HOperatorSet.GenEmptyObj(out Image);
            Image = null;

            HInfo.InfoFramegrabber(CameraComData, "device", out HDeviceName);
            DeviceName = HDeviceName.ToString();

            if (!Framegrabber.IsInitialized())
            {
                Framegrabber.OpenFramegrabber(CameraComData, 1, 1, 0, 0, 0, 0, "default", -1, "default", -1, "false", "default", DeviceName, 0, -1);
            }
            Framegrabber.SetFramegrabberParam("TriggerMode", "Off");
            Framegrabber.SetFramegrabberParam("TriggerSource", "Freerun");
            Framegrabber.SetFramegrabberParam("AcquisitionMode", "SingleFrame");
            Framegrabber.SetFramegrabberParam("AcquisitionFrameRateAbs", 2.0);
            Framegrabber.SetFramegrabberParam("ExposureTimeAbs", 8000.0);
            Framegrabber.SetFramegrabberParam("Gain", 12.0);

            IsConnected = Framegrabber.IsInitialized();
            HOperatorSet.SetWindowAttr("background_color", "black");
            HOperatorSet.OpenWindow(0, 0, 800, 600, 0, "", "", out hv_WindowHandle);
            HDevWindowStack.Push(hv_WindowHandle);
        }

        public void DeconnectionCam()
        {
            if (HDevWindowStack.IsOpen())
            {
                HOperatorSet.CloseWindow(HDevWindowStack.Pop());
            }

            if (Framegrabber.IsInitialized())
            {
                Framegrabber.Dispose();
            }
            Image.Dispose();
            IsConnected = Framegrabber.IsInitialized();
        }

        public void TakePicture()
        {
            Image = null;
            Console.WriteLine("Grabing Image....");
            Image = Framegrabber.GrabImage();
            Console.WriteLine("Image Grabed.");
            if (HDevWindowStack.IsOpen())
            {
                HOperatorSet.DispObj(Image, HDevWindowStack.GetActive());
            }
        }

        private HFramegrabber Framegrabber;
        private HImage Image = null;
        private HTuple hv_WindowHandle = null;

        private HTuple CameraWidth = 1600;
        private HTuple CameraHeight = 1200;
        //private String CameraComData = "uEye";
        private String CameraComData = "GigEVision";
        //private String DeviceName = "000f315daec4_AlliedVisionTechnologies_MakoG503B";
        private HTuple HDeviceName = "";
        private String DeviceName = "";
    }
}
