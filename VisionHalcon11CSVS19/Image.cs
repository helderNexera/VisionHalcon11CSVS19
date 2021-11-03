using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalconDotNet;

namespace VisionHalcon11CSVS19
{
    public class Image
    {
        public Image(ref Camera cam)
        {
            camera = cam;
        }
        public void GetImageFromCam()
        {

        }

        private Camera camera = null;
    }
}
