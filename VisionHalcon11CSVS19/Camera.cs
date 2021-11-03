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
            
            if (!Framegrabber.IsInitialized())
            {
                Framegrabber.OpenFramegrabber(CameraComData, 1, 1, 0, 0, 0, 0, "default", 8, "default", -1, "false", "default", "1", 0, -1);
            }
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
        private String CameraComData = "uEye";
    }
}
