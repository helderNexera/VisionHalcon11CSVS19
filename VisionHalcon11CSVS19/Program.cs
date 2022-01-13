using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
        static int Main(string[] args)
        {
            Mutex mutex = new Mutex(true, "{DDE97D2C-179F-4A95-B132-B4D3AE3B060D}");
            String AdsAdress = args.Length >= 1 ? args[0] : "";

            if (!mutex.WaitOne(TimeSpan.Zero, true))
            {
                MessageBox.Show(null, "Application déjà démarrée", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }
            if (String.IsNullOrEmpty(AdsAdress))
            {
                MessageBox.Show(null, "Adresse ADS indéfinie", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainWindowsOldShape(args));

            return 0;

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
