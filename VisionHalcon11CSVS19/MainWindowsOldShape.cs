using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VisionHalcon11CSVS19
{
    public partial class MainWindowsOldShape : Form
    {
        public MainWindowsOldShape()
        {
            InitializeComponent();
            Camera Cam = new Camera();
            Cam.InitHalcon(hwcVideo);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }
    }
}
