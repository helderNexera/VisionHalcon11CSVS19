using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VisionHalcon11CSVS19
{
    class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainWindowsOldShape());

            /* ---------------- Console Version ---------------- */

            //Console.WriteLine("Hello World !!!");

            //bool takeNewImage = true;
            //int i = 0;

            //Camera UEyeCam = new Camera();
            //Image ImageCam = new Image(ref UEyeCam);

            //UEyeCam.ConnectionCam();
            //if (UEyeCam.IsConnected)
            //{
            //    Console.WriteLine("The Camera is connected!");
            //}
            //while (takeNewImage)
            //{
            //    UEyeCam.TakePicture();
            //    i++;
            //    Console.WriteLine("Picture " + i + " Taked.");
            //    Console.WriteLine("1 to continue");
            //    Console.WriteLine("0 to quit");
            //    takeNewImage = (Console.ReadLine() != "0");
            //}

            //UEyeCam.DeconnectionCam();
            //if (!UEyeCam.IsConnected)
            //{
            //    Console.WriteLine("The Camera is disconnected!");
            //}
        }
    }
}
