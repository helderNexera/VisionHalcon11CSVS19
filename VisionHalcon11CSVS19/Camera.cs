using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalconDotNet;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace VisionHalcon11CSVS19
{
    public class Camera
    {
        public bool IsConnected { get; private set; }
        public bool ModelLoaded { get; private set; }

        public Camera()
        {
            Framegrabber = new HFramegrabber();
            IsConnected = false;
            HOperatorSet.SetSystem("width", CameraWidth);
            HOperatorSet.SetSystem("height", CameraHeight);
        }

        public bool InitHalcon(ref HWindowControl WindowsHalcon)
        {
            hv_WindowHandle = WindowsHalcon.HalconWindow;
            hv_WindowHandle.SetWindowParam("background_color", "black");
            hv_WindowHandle.SetDraw("margin");
            hv_WindowHandle.SetLineWidth(2);
            //set_display_font(hv_WindowHandle, 18, "mono", "false", "false");
            HOperatorSet.SetSystem("border_shape_models", "false");
            hv_WindowHandle.SetPart(0, 0, CameraHeight - 1, CameraWidth - 1);
            return true;
        }

        public void ConnectionCam()
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

            //HOperatorSet.SetWindowAttr("background_color", "black");
            //HOperatorSet.OpenWindow(0, 0, 800, 600, 0, "", "", out hv_WindowHandle);
            //HDevWindowStack.Push(hv_WindowHandle);
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

        public bool InitVision(string RefName)
        {
            try
            {
                if (ModelLoaded)
                {
                    ShapeModel.Dispose();
                }
                ModelLoaded = false;

                if (File.Exists("c:/vision/reference/" + RefName + ".shm"))
                {
                    LoadModel(RefName);
                    TParams.GetPartParams(RefName);
                    ModelLoaded = true;
                    hv_WindowHandle.ClearWindow();
                }
                else
                {
                    disp_message(hv_WindowHandle, "Le fichier " + RefName + " n'existe pas", "window", 40, 10, "red", "false");
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            return true;
        }

        public bool TakePicture()
        {
            Image = null;
            try
            {
                Thread.Sleep(2000);
                Image = Framegrabber.GrabImage();
                return true;
            }
            catch (Exception ex)
            {
                disp_message(hv_WindowHandle, "Error with Framgrabber\n" + ex.Message, "window", 40, 10, "red", "false");
                return false;
            }
        }

        public void DisplayImage()
        {
            if (IsConnected && hv_WindowHandle.IsInitialized() && Image.IsInitialized())
            {
                Image.DispObj(hv_WindowHandle);
            }
        }

        public void DoAnalysis(ref TTwincatinterface.VISION_PART_DATA Data, bool DispayInWindow)
        {
            HTuple DeltaX, DeltaY;
            HTuple RefSearchRadius;
            HTuple ModelRadius, ModelThr;

            bool PartFound = false;

            if(ModelLoaded && Image.IsInitialized())
            {
                RefSearchRadius = TParams.GetSearchRadius();
                ModelRadius = TParams.GetModelRadius();
                ModelThr = TParams.GetModelThreshold();

                hv_WindowHandle.ClearWindow();
                if (DispayInWindow)
                {
                    Image.DispObj(hv_WindowHandle);
                    hv_WindowHandle.SetColor("cyan");
                    hv_WindowHandle.DispCircle(CameraHeight / 2, CameraWidth / 2, RefSearchRadius);
                }

                Image.FindShapeModel(ShapeModel, 0, 360, 0.4, 0, 0.5, "least_squares", 0, 0.7, out HTuple Row, out HTuple Column, out HTuple Angle, out HTuple Score);

                DeltaX = (Column - CameraWidth / 2) * PixToMm;
                DeltaY = (Row - CameraHeight / 2) * PixToMm;

                if ((Angle.TupleLength() > 0))
                {
                    if (Score > 0.5)
                    {
                        PartFound = true;
                        Angle = Angle.TupleDeg();

                        HOperatorSet.TupleSin((Angle + 90).TupleRad(), out HTuple SinAngle);
                        HOperatorSet.TupleCos((Angle + 90).TupleRad(), out HTuple CosAngle);

                        if (DispayInWindow)
                        {
                            hv_WindowHandle.SetLineWidth(3);
                            hv_WindowHandle.SetColor("blue");
                            hv_WindowHandle.DispLine(Row, Column, Row + 600 * CosAngle, Column + 600 * SinAngle);
                            hv_WindowHandle.SetLineWidth(1);
                            hv_WindowHandle.SetColor("blue");
                            hv_WindowHandle.DispCircle(Row, Column, ModelRadius);
                            disp_message(hv_WindowHandle, ((((("Correction : [" + (DeltaX.TupleString(".2f"))) + "; ") + (DeltaY.TupleString(".2f"))) + "; ") + (Angle.TupleString(".1f"))) + "]", "Windows", 40, 10, "green", "false");
                            disp_message(hv_WindowHandle, ("Score : " + ((Score * 100).TupleString(".0f"))) + " %", "window", 30, 5, "green", "false");
                        }

                        Data.VPD_Present = Convert.ToByte(true);
                        Data.VPD_X = DeltaX;
                        Data.VPD_Y = DeltaY;
                        Data.VPD_Theta = Angle;
                        Data.VPD_Score = Score;
                        Data.VPD_Valid = Convert.ToByte(true);
                    }
                }
                if (!PartFound)
                {
                    // no good part found, check if a part is present
                    Data.VPD_Valid = Convert.ToByte(false);

                    HOperatorSet.GenCircle(out HObject Circle, CameraHeight / 2, CameraWidth / 2, RefSearchRadius);
                    HOperatorSet.ReduceDomain(Image, Circle, out HObject ImageReduced);
                    HOperatorSet.Threshold(ImageReduced, out HObject Region1, ModelThr, 255);
                    HOperatorSet.Connection(Region1, out HObject ConnectedRegion);
                    HOperatorSet.SelectShape(ConnectedRegion, out HObject SelectedRegions, "area", "and", 20000, 1000000);
                    HOperatorSet.Union1(SelectedRegions, out HObject RegionUnion);

                    if (RegionUnion.CountObj() > 0)
                    {
                        // No good part found, check if a part is present
                        Data.VPD_Present = Convert.ToByte(true);

                        if (DispayInWindow)
                        {
                            disp_message(hv_WindowHandle, "Bad part found", "window", 40, 10, "red", "false");
                        }
                    }
                    else
                    {
                        // no part
                        if (DispayInWindow)
                        {
                            disp_message(hv_WindowHandle, "No part found", "window", 40, 10, "white", "false");
                        }
                    }
                }
            }
            else
            {
                disp_message(hv_WindowHandle, "Model not loaded or Image not taked", "window", 40, 10, "white", "false");
            }
        }

        public bool DisplayImageManualMode(bool Zoom, bool Analyse, ref TTwincatinterface.VISION_PART_DATA Data)
        {
            Image = null;
            try
            {
                TakePicture();
            }
            catch (Exception ex)
            {
                disp_message(hv_WindowHandle, "Error with Framgrabber\n" + ex.Message, "window", 40, 10, "red", "false");
                return false;
            }

            Image.DispObj(hv_WindowHandle);

            if (Zoom)
            {
                hv_WindowHandle.SetColor("magenta");
                hv_WindowHandle.SetPart(Convert.ToInt16((double)CameraHeight * 0.4), Convert.ToInt16((double)CameraWidth * 0.4), Convert.ToInt16((double)CameraHeight * 0.6), Convert.ToInt16((double)CameraWidth * 0.6));
                hv_WindowHandle.SetDraw("margin");
                hv_WindowHandle.DispCircle(CameraHeight / 2, CameraWidth / 2, 100);
                hv_WindowHandle.DispCross(CameraHeight / 2, CameraWidth / 2, 50, 0);
            }
            else
            {
                hv_WindowHandle.SetPart(0, 0, CameraHeight, CameraWidth);
            }

            if (Analyse)
            {
                DoAnalysis(ref Data, true);
            }

            return true;
        }

        private readonly HFramegrabber Framegrabber;
        private HImage Image = null;
        private HWindow hv_WindowHandle = null;
        private HShapeModel ShapeModel = null;

        private readonly HTuple CameraWidth = 2592;
        private readonly HTuple CameraHeight = 1944;
        private readonly String CameraComData = "GigEVision";
        //private String CameraComData = "uEye";
        //private String DeviceName = "000f315daec4_AlliedVisionTechnologies_MakoG503B";
        private HTuple HDeviceName = "";
        private String DeviceName = "";
        private readonly double PixToMm = 0.022903;              // ((EtalonReel / EtalonImage) * WindowSize) / WindowSizePixel

        private void LoadModel(string RefFileName)
        {
            //ShapeModel.ReadShapeModel("C:/vision/referencence/" + RefFileName + ".shm");
            ShapeModel = new HShapeModel("C:/vision/reference/" + RefFileName + ".shm");
            ModelLoaded = true;
        }

        // Chapter: Graphics / Text
        // Short Description: Set font independent of OS 
        private void set_display_font(HTuple hv_WindowHandle, HTuple hv_Size, HTuple hv_Font, HTuple hv_Bold, HTuple hv_Slant)
        {


            // Local control variables 

            HTuple hv_OS = null, hv_Exception = new HTuple();
            HTuple hv_SubFamily = new HTuple(), hv_Fonts = new HTuple();
            HTuple hv_SystemFonts = new HTuple(), hv_Guess = new HTuple();
            HTuple hv_I = new HTuple(), hv_Index = new HTuple(), hv_AllowedFontSizes = new HTuple();
            HTuple hv_Distances = new HTuple(), hv_Indices = new HTuple();
            HTuple hv_FontSelRegexp = new HTuple(), hv_FontsCourier = new HTuple();

            HTuple hv_Bold_COPY_INP_TMP = hv_Bold.Clone();
            HTuple hv_Font_COPY_INP_TMP = hv_Font.Clone();
            HTuple hv_Size_COPY_INP_TMP = hv_Size.Clone();
            HTuple hv_Slant_COPY_INP_TMP = hv_Slant.Clone();

            // Initialize local and output iconic variables 

            //This procedure sets the text font of the current window with
            //the specified attributes.
            //It is assumed that following fonts are installed on the system:
            //Windows: Courier New, Arial Times New Roman
            //Mac OS X: Menlo, Arial, TimesNewRomanPS
            //Linux: courier, helvetica, times
            //Because fonts are displayed smaller on Linux than on Windows,
            //a scaling factor of 1.25 is used the get comparable results.
            //For Linux, only a limited number of font sizes is supported,
            //to get comparable results, it is recommended to use one of the
            //following sizes: 9, 11, 14, 16, 20, 27
            //(which will be mapped internally on Linux systems to 11, 14, 17, 20, 25, 34)
            //
            //Input parameters:
            //WindowHandle: The graphics window for which the font will be set
            //Size: The font size. If Size=-1, the default of 16 is used.
            //Bold: If set to 'true', a bold font is used
            //Slant: If set to 'true', a slanted font is used
            //
            HOperatorSet.GetSystem("operating_system", out hv_OS);
            // dev_get_preferences(...); only in hdevelop
            // dev_set_preferences(...); only in hdevelop
            if ((int)((new HTuple(hv_Size_COPY_INP_TMP.TupleEqual(new HTuple()))).TupleOr(
                new HTuple(hv_Size_COPY_INP_TMP.TupleEqual(-1)))) != 0)
            {
                hv_Size_COPY_INP_TMP = 16;
            }
            if ((int)(new HTuple(((hv_OS.TupleSubstr(0, 2))).TupleEqual("Win"))) != 0)
            {
                //Set font on Windows systems
                if ((int)((new HTuple((new HTuple(hv_Font_COPY_INP_TMP.TupleEqual("mono"))).TupleOr(
                    new HTuple(hv_Font_COPY_INP_TMP.TupleEqual("Courier"))))).TupleOr(new HTuple(hv_Font_COPY_INP_TMP.TupleEqual(
                    "courier")))) != 0)
                {
                    hv_Font_COPY_INP_TMP = "Courier New";
                }
                else if ((int)(new HTuple(hv_Font_COPY_INP_TMP.TupleEqual("sans"))) != 0)
                {
                    hv_Font_COPY_INP_TMP = "Arial";
                }
                else if ((int)(new HTuple(hv_Font_COPY_INP_TMP.TupleEqual("serif"))) != 0)
                {
                    hv_Font_COPY_INP_TMP = "Times New Roman";
                }
                if ((int)(new HTuple(hv_Bold_COPY_INP_TMP.TupleEqual("true"))) != 0)
                {
                    hv_Bold_COPY_INP_TMP = 1;
                }
                else if ((int)(new HTuple(hv_Bold_COPY_INP_TMP.TupleEqual("false"))) != 0)
                {
                    hv_Bold_COPY_INP_TMP = 0;
                }
                else
                {
                    hv_Exception = "Wrong value of control parameter Bold";
                    throw new HalconException(hv_Exception);
                }
                if ((int)(new HTuple(hv_Slant_COPY_INP_TMP.TupleEqual("true"))) != 0)
                {
                    hv_Slant_COPY_INP_TMP = 1;
                }
                else if ((int)(new HTuple(hv_Slant_COPY_INP_TMP.TupleEqual("false"))) != 0)
                {
                    hv_Slant_COPY_INP_TMP = 0;
                }
                else
                {
                    hv_Exception = "Wrong value of control parameter Slant";
                    throw new HalconException(hv_Exception);
                }
                try
                {
                    HOperatorSet.SetFont(hv_WindowHandle, ((((((("-" + hv_Font_COPY_INP_TMP) + "-") + hv_Size_COPY_INP_TMP) + "-*-") + hv_Slant_COPY_INP_TMP) + "-*-*-") + hv_Bold_COPY_INP_TMP) + "-");
                }
                // catch (Exception) 
                catch (HalconException HDevExpDefaultException1)
                {
                    HDevExpDefaultException1.ToHTuple(out hv_Exception);
                    //throw (Exception)
                }
            }
            else if ((int)(new HTuple(((hv_OS.TupleSubstr(0, 2))).TupleEqual("Dar"))) != 0)
            {
                //Set font on Mac OS X systems. Since OS X does not have a strict naming
                //scheme for font attributes, we use tables to determine the correct font
                //name.
                hv_SubFamily = 0;
                if ((int)(new HTuple(hv_Slant_COPY_INP_TMP.TupleEqual("true"))) != 0)
                {
                    hv_SubFamily = hv_SubFamily.TupleBor(1);
                }
                else if ((int)(new HTuple(hv_Slant_COPY_INP_TMP.TupleNotEqual("false"))) != 0)
                {
                    hv_Exception = "Wrong value of control parameter Slant";
                    throw new HalconException(hv_Exception);
                }
                if ((int)(new HTuple(hv_Bold_COPY_INP_TMP.TupleEqual("true"))) != 0)
                {
                    hv_SubFamily = hv_SubFamily.TupleBor(2);
                }
                else if ((int)(new HTuple(hv_Bold_COPY_INP_TMP.TupleNotEqual("false"))) != 0)
                {
                    hv_Exception = "Wrong value of control parameter Bold";
                    throw new HalconException(hv_Exception);
                }
                if ((int)(new HTuple(hv_Font_COPY_INP_TMP.TupleEqual("mono"))) != 0)
                {
                    hv_Fonts = new HTuple();
                    hv_Fonts[0] = "Menlo-Regular";
                    hv_Fonts[1] = "Menlo-Italic";
                    hv_Fonts[2] = "Menlo-Bold";
                    hv_Fonts[3] = "Menlo-BoldItalic";
                }
                else if ((int)((new HTuple(hv_Font_COPY_INP_TMP.TupleEqual("Courier"))).TupleOr(
                    new HTuple(hv_Font_COPY_INP_TMP.TupleEqual("courier")))) != 0)
                {
                    hv_Fonts = new HTuple();
                    hv_Fonts[0] = "CourierNewPSMT";
                    hv_Fonts[1] = "CourierNewPS-ItalicMT";
                    hv_Fonts[2] = "CourierNewPS-BoldMT";
                    hv_Fonts[3] = "CourierNewPS-BoldItalicMT";
                }
                else if ((int)(new HTuple(hv_Font_COPY_INP_TMP.TupleEqual("sans"))) != 0)
                {
                    hv_Fonts = new HTuple();
                    hv_Fonts[0] = "ArialMT";
                    hv_Fonts[1] = "Arial-ItalicMT";
                    hv_Fonts[2] = "Arial-BoldMT";
                    hv_Fonts[3] = "Arial-BoldItalicMT";
                }
                else if ((int)(new HTuple(hv_Font_COPY_INP_TMP.TupleEqual("serif"))) != 0)
                {
                    hv_Fonts = new HTuple();
                    hv_Fonts[0] = "TimesNewRomanPSMT";
                    hv_Fonts[1] = "TimesNewRomanPS-ItalicMT";
                    hv_Fonts[2] = "TimesNewRomanPS-BoldMT";
                    hv_Fonts[3] = "TimesNewRomanPS-BoldItalicMT";
                }
                else
                {
                    //Attempt to figure out which of the fonts installed on the system
                    //the user could have meant.
                    HOperatorSet.QueryFont(hv_WindowHandle, out hv_SystemFonts);
                    hv_Fonts = new HTuple();
                    hv_Fonts = hv_Fonts.TupleConcat(hv_Font_COPY_INP_TMP);
                    hv_Fonts = hv_Fonts.TupleConcat(hv_Font_COPY_INP_TMP);
                    hv_Fonts = hv_Fonts.TupleConcat(hv_Font_COPY_INP_TMP);
                    hv_Fonts = hv_Fonts.TupleConcat(hv_Font_COPY_INP_TMP);
                    hv_Guess = new HTuple();
                    hv_Guess = hv_Guess.TupleConcat(hv_Font_COPY_INP_TMP);
                    hv_Guess = hv_Guess.TupleConcat(hv_Font_COPY_INP_TMP + "-Regular");
                    hv_Guess = hv_Guess.TupleConcat(hv_Font_COPY_INP_TMP + "MT");
                    for (hv_I = 0; (int)hv_I <= (int)((new HTuple(hv_Guess.TupleLength())) - 1); hv_I = (int)hv_I + 1)
                    {
                        HOperatorSet.TupleFind(hv_SystemFonts, hv_Guess.TupleSelect(hv_I), out hv_Index);
                        if ((int)(new HTuple(hv_Index.TupleNotEqual(-1))) != 0)
                        {
                            if (hv_Fonts == null)
                                hv_Fonts = new HTuple();
                            hv_Fonts[0] = hv_Guess.TupleSelect(hv_I);
                            break;
                        }
                    }
                    //Guess name of slanted font
                    hv_Guess = new HTuple();
                    hv_Guess = hv_Guess.TupleConcat(hv_Font_COPY_INP_TMP + "-Italic");
                    hv_Guess = hv_Guess.TupleConcat(hv_Font_COPY_INP_TMP + "-ItalicMT");
                    hv_Guess = hv_Guess.TupleConcat(hv_Font_COPY_INP_TMP + "-Oblique");
                    for (hv_I = 0; (int)hv_I <= (int)((new HTuple(hv_Guess.TupleLength())) - 1); hv_I = (int)hv_I + 1)
                    {
                        HOperatorSet.TupleFind(hv_SystemFonts, hv_Guess.TupleSelect(hv_I), out hv_Index);
                        if ((int)(new HTuple(hv_Index.TupleNotEqual(-1))) != 0)
                        {
                            if (hv_Fonts == null)
                                hv_Fonts = new HTuple();
                            hv_Fonts[1] = hv_Guess.TupleSelect(hv_I);
                            break;
                        }
                    }
                    //Guess name of bold font
                    hv_Guess = new HTuple();
                    hv_Guess = hv_Guess.TupleConcat(hv_Font_COPY_INP_TMP + "-Bold");
                    hv_Guess = hv_Guess.TupleConcat(hv_Font_COPY_INP_TMP + "-BoldMT");
                    for (hv_I = 0; (int)hv_I <= (int)((new HTuple(hv_Guess.TupleLength())) - 1); hv_I = (int)hv_I + 1)
                    {
                        HOperatorSet.TupleFind(hv_SystemFonts, hv_Guess.TupleSelect(hv_I), out hv_Index);
                        if ((int)(new HTuple(hv_Index.TupleNotEqual(-1))) != 0)
                        {
                            if (hv_Fonts == null)
                                hv_Fonts = new HTuple();
                            hv_Fonts[2] = hv_Guess.TupleSelect(hv_I);
                            break;
                        }
                    }
                    //Guess name of bold slanted font
                    hv_Guess = new HTuple();
                    hv_Guess = hv_Guess.TupleConcat(hv_Font_COPY_INP_TMP + "-BoldItalic");
                    hv_Guess = hv_Guess.TupleConcat(hv_Font_COPY_INP_TMP + "-BoldItalicMT");
                    hv_Guess = hv_Guess.TupleConcat(hv_Font_COPY_INP_TMP + "-BoldOblique");
                    for (hv_I = 0; (int)hv_I <= (int)((new HTuple(hv_Guess.TupleLength())) - 1); hv_I = (int)hv_I + 1)
                    {
                        HOperatorSet.TupleFind(hv_SystemFonts, hv_Guess.TupleSelect(hv_I), out hv_Index);
                        if ((int)(new HTuple(hv_Index.TupleNotEqual(-1))) != 0)
                        {
                            if (hv_Fonts == null)
                                hv_Fonts = new HTuple();
                            hv_Fonts[3] = hv_Guess.TupleSelect(hv_I);
                            break;
                        }
                    }
                }
                hv_Font_COPY_INP_TMP = hv_Fonts.TupleSelect(hv_SubFamily);
                try
                {
                    HOperatorSet.SetFont(hv_WindowHandle, (hv_Font_COPY_INP_TMP + "-") + hv_Size_COPY_INP_TMP);
                }
                // catch (Exception) 
                catch (HalconException HDevExpDefaultException1)
                {
                    HDevExpDefaultException1.ToHTuple(out hv_Exception);
                    //throw (Exception)
                }
            }
            else
            {
                //Set font for UNIX systems
                hv_Size_COPY_INP_TMP = hv_Size_COPY_INP_TMP * 1.25;
                hv_AllowedFontSizes = new HTuple();
                hv_AllowedFontSizes[0] = 11;
                hv_AllowedFontSizes[1] = 14;
                hv_AllowedFontSizes[2] = 17;
                hv_AllowedFontSizes[3] = 20;
                hv_AllowedFontSizes[4] = 25;
                hv_AllowedFontSizes[5] = 34;
                if ((int)(new HTuple(((hv_AllowedFontSizes.TupleFind(hv_Size_COPY_INP_TMP))).TupleEqual(
                    -1))) != 0)
                {
                    hv_Distances = ((hv_AllowedFontSizes - hv_Size_COPY_INP_TMP)).TupleAbs();
                    HOperatorSet.TupleSortIndex(hv_Distances, out hv_Indices);
                    hv_Size_COPY_INP_TMP = hv_AllowedFontSizes.TupleSelect(hv_Indices.TupleSelect(
                        0));
                }
                if ((int)((new HTuple(hv_Font_COPY_INP_TMP.TupleEqual("mono"))).TupleOr(new HTuple(hv_Font_COPY_INP_TMP.TupleEqual(
                    "Courier")))) != 0)
                {
                    hv_Font_COPY_INP_TMP = "courier";
                }
                else if ((int)(new HTuple(hv_Font_COPY_INP_TMP.TupleEqual("sans"))) != 0)
                {
                    hv_Font_COPY_INP_TMP = "helvetica";
                }
                else if ((int)(new HTuple(hv_Font_COPY_INP_TMP.TupleEqual("serif"))) != 0)
                {
                    hv_Font_COPY_INP_TMP = "times";
                }
                if ((int)(new HTuple(hv_Bold_COPY_INP_TMP.TupleEqual("true"))) != 0)
                {
                    hv_Bold_COPY_INP_TMP = "bold";
                }
                else if ((int)(new HTuple(hv_Bold_COPY_INP_TMP.TupleEqual("false"))) != 0)
                {
                    hv_Bold_COPY_INP_TMP = "medium";
                }
                else
                {
                    hv_Exception = "Wrong value of control parameter Bold";
                    throw new HalconException(hv_Exception);
                }
                if ((int)(new HTuple(hv_Slant_COPY_INP_TMP.TupleEqual("true"))) != 0)
                {
                    if ((int)(new HTuple(hv_Font_COPY_INP_TMP.TupleEqual("times"))) != 0)
                    {
                        hv_Slant_COPY_INP_TMP = "i";
                    }
                    else
                    {
                        hv_Slant_COPY_INP_TMP = "o";
                    }
                }
                else if ((int)(new HTuple(hv_Slant_COPY_INP_TMP.TupleEqual("false"))) != 0)
                {
                    hv_Slant_COPY_INP_TMP = "r";
                }
                else
                {
                    hv_Exception = "Wrong value of control parameter Slant";
                    throw new HalconException(hv_Exception);
                }
                try
                {
                    HOperatorSet.SetFont(hv_WindowHandle, ((((((("-adobe-" + hv_Font_COPY_INP_TMP) + "-") + hv_Bold_COPY_INP_TMP) + "-") + hv_Slant_COPY_INP_TMP) + "-normal-*-") + hv_Size_COPY_INP_TMP) + "-*-*-*-*-*-*-*");
                }
                // catch (Exception) 
                catch (HalconException HDevExpDefaultException1)
                {
                    HDevExpDefaultException1.ToHTuple(out hv_Exception);
                    if ((int)((new HTuple(((hv_OS.TupleSubstr(0, 4))).TupleEqual("Linux"))).TupleAnd(
                        new HTuple(hv_Font_COPY_INP_TMP.TupleEqual("courier")))) != 0)
                    {
                        HOperatorSet.QueryFont(hv_WindowHandle, out hv_Fonts);
                        hv_FontSelRegexp = (("^-[^-]*-[^-]*[Cc]ourier[^-]*-" + hv_Bold_COPY_INP_TMP) + "-") + hv_Slant_COPY_INP_TMP;
                        hv_FontsCourier = ((hv_Fonts.TupleRegexpSelect(hv_FontSelRegexp))).TupleRegexpMatch(
                            hv_FontSelRegexp);
                        if ((int)(new HTuple((new HTuple(hv_FontsCourier.TupleLength())).TupleEqual(
                            0))) != 0)
                        {
                            hv_Exception = "Wrong font name";
                            //throw (Exception)
                        }
                        else
                        {
                            try
                            {
                                HOperatorSet.SetFont(hv_WindowHandle, (((hv_FontsCourier.TupleSelect(
                                    0)) + "-normal-*-") + hv_Size_COPY_INP_TMP) + "-*-*-*-*-*-*-*");
                            }
                            // catch (Exception) 
                            catch (HalconException HDevExpDefaultException2)
                            {
                                HDevExpDefaultException2.ToHTuple(out hv_Exception);
                                //throw (Exception)
                            }
                        }
                    }
                    //throw (Exception)
                }
            }
            // dev_set_preferences(...); only in hdevelop

            return;
        }

        // Chapter: Graphics / Text
        // Short Description: This procedure writes a text message. 
        private void disp_message(HTuple hv_WindowHandle, HTuple hv_String, HTuple hv_CoordSystem,
            HTuple hv_Row, HTuple hv_Column, HTuple hv_Color, HTuple hv_Box)
        {


            // Local control variables 

            HTuple hv_M = null, hv_N = null, hv_Red = null;
            HTuple hv_Green = null, hv_Blue = null, hv_RowI1Part = null;
            HTuple hv_ColumnI1Part = null, hv_RowI2Part = null, hv_ColumnI2Part = null;
            HTuple hv_RowIWin = null, hv_ColumnIWin = null, hv_WidthWin = null;
            HTuple hv_HeightWin = null, hv_I = null, hv_RowI = new HTuple();
            HTuple hv_ColumnI = new HTuple(), hv_StringI = new HTuple();
            HTuple hv_MaxAscent = new HTuple(), hv_MaxDescent = new HTuple();
            HTuple hv_MaxWidth = new HTuple(), hv_MaxHeight = new HTuple();
            HTuple hv_R1 = new HTuple(), hv_C1 = new HTuple(), hv_FactorRowI = new HTuple();
            HTuple hv_FactorColumnI = new HTuple(), hv_UseShadow = new HTuple();
            HTuple hv_ShadowColor = new HTuple(), hv_Exception = new HTuple();
            HTuple hv_Width = new HTuple(), hv_Index = new HTuple();
            HTuple hv_Ascent = new HTuple(), hv_Descent = new HTuple();
            HTuple hv_W = new HTuple(), hv_H = new HTuple(), hv_FrameHeight = new HTuple();
            HTuple hv_FrameWidth = new HTuple(), hv_R2 = new HTuple();
            HTuple hv_C2 = new HTuple(), hv_DrawMode = new HTuple();
            HTuple hv_CurrentColor = new HTuple();

            HTuple hv_Box_COPY_INP_TMP = hv_Box.Clone();
            HTuple hv_Color_COPY_INP_TMP = hv_Color.Clone();
            HTuple hv_Column_COPY_INP_TMP = hv_Column.Clone();
            HTuple hv_Row_COPY_INP_TMP = hv_Row.Clone();
            HTuple hv_String_COPY_INP_TMP = hv_String.Clone();

            // Initialize local and output iconic variables 

            //This procedure displays text in a graphics window.
            //
            //Input parameters:
            //WindowHandle: The WindowHandle of the graphics window, where
            //   the message should be displayed
            //String: A tuple of strings containing the text message to be displayed
            //CoordSystem: If set to 'window', the text position is given
            //   with respect to the window coordinate system.
            //   If set to 'image', image coordinates are used.
            //   (This may be useful in zoomed images.)
            //Row: The row coordinate of the desired text position
            //   If set to -1, a default value of 12 is used.
            //   A tuple of values is allowed to display text at different
            //   positions.
            //Column: The column coordinate of the desired text position
            //   If set to -1, a default value of 12 is used.
            //   A tuple of values is allowed to display text at different
            //   positions.
            //Color: defines the color of the text as string.
            //   If set to [], '' or 'auto' the currently set color is used.
            //   If a tuple of strings is passed, the colors are used cyclically...
            //   - if |Row| == |Column| == 1: for each new textline
            //   = else for each text position.
            //Box: If Box[0] is set to 'true', the text is written within a white box.
            //     If set to' false', no box is displayed.
            //     If set to a color string (e.g. 'black', '#FF00CC', etc.),
            //       the text is written in a box of that color.
            //     An optional second value for Box (Box[1]) controls if a shadow is displayed:
            //       'true' -> display a shadow in a default color
            //       'false' -> display no shadow
            //       otherwise -> use given string as color string for the shadow color
            //
            //It is possible to display multiple text strings in a single call.
            //In this case, some restrictions apply:
            //- Multiple text positions can be defined by specifying a tuple
            //  with multiple Row and/or Column coordinates, i.e.:
            //  - |Row| == n, |Column| == n
            //  - |Row| == n, |Column| == 1
            //  - |Row| == 1, |Column| == n
            //- If |Row| == |Column| == 1,
            //  each element of String is display in a new textline.
            //- If multiple positions or specified, the number of Strings
            //  must match the number of positions, i.e.:
            //  - Either |String| == n (each string is displayed at the
            //                          corresponding position),
            //  - or     |String| == 1 (The string is displayed n times).
            //
            if ((int)(new HTuple(hv_Color_COPY_INP_TMP.TupleEqual(new HTuple()))) != 0)
            {
                hv_Color_COPY_INP_TMP = "";
            }
            if ((int)(new HTuple(hv_Box_COPY_INP_TMP.TupleEqual(new HTuple()))) != 0)
            {
                hv_Box_COPY_INP_TMP = "false";
            }
            //
            //
            //Check conditions
            //
            hv_M = (new HTuple(hv_Row_COPY_INP_TMP.TupleLength())) * (new HTuple(hv_Column_COPY_INP_TMP.TupleLength()
                ));
            hv_N = new HTuple(hv_Row_COPY_INP_TMP.TupleLength());
            if ((int)((new HTuple(hv_M.TupleEqual(0))).TupleOr(new HTuple(hv_String_COPY_INP_TMP.TupleEqual(
                new HTuple())))) != 0)
            {

                return;
            }
            if ((int)(new HTuple(hv_M.TupleNotEqual(1))) != 0)
            {
                //Multiple positions
                //
                //Expand single parameters
                if ((int)(new HTuple((new HTuple(hv_Row_COPY_INP_TMP.TupleLength())).TupleEqual(
                    1))) != 0)
                {
                    hv_N = new HTuple(hv_Column_COPY_INP_TMP.TupleLength());
                    HOperatorSet.TupleGenConst(hv_N, hv_Row_COPY_INP_TMP, out hv_Row_COPY_INP_TMP);
                }
                else if ((int)(new HTuple((new HTuple(hv_Column_COPY_INP_TMP.TupleLength()
                    )).TupleEqual(1))) != 0)
                {
                    HOperatorSet.TupleGenConst(hv_N, hv_Column_COPY_INP_TMP, out hv_Column_COPY_INP_TMP);
                }
                else if ((int)(new HTuple((new HTuple(hv_Column_COPY_INP_TMP.TupleLength()
                    )).TupleNotEqual(new HTuple(hv_Row_COPY_INP_TMP.TupleLength())))) != 0)
                {
                    throw new HalconException("Number of elements in Row and Column does not match.");
                }
                if ((int)(new HTuple((new HTuple(hv_String_COPY_INP_TMP.TupleLength())).TupleEqual(
                    1))) != 0)
                {
                    HOperatorSet.TupleGenConst(hv_N, hv_String_COPY_INP_TMP, out hv_String_COPY_INP_TMP);
                }
                else if ((int)(new HTuple((new HTuple(hv_String_COPY_INP_TMP.TupleLength()
                    )).TupleNotEqual(hv_N))) != 0)
                {
                    throw new HalconException("Number of elements in Strings does not match number of positions.");
                }
                //
            }
            //
            //Prepare window
            HOperatorSet.GetRgb(hv_WindowHandle, out hv_Red, out hv_Green, out hv_Blue);
            HOperatorSet.GetPart(hv_WindowHandle, out hv_RowI1Part, out hv_ColumnI1Part,
                out hv_RowI2Part, out hv_ColumnI2Part);
            HOperatorSet.GetWindowExtents(hv_WindowHandle, out hv_RowIWin, out hv_ColumnIWin,
                out hv_WidthWin, out hv_HeightWin);
            HOperatorSet.SetPart(hv_WindowHandle, 0, 0, hv_HeightWin - 1, hv_WidthWin - 1);
            //
            //Loop over all positions
            HTuple end_val89 = hv_N - 1;
            HTuple step_val89 = 1;
            for (hv_I = 0; hv_I.Continue(end_val89, step_val89); hv_I = hv_I.TupleAdd(step_val89))
            {
                hv_RowI = hv_Row_COPY_INP_TMP.TupleSelect(hv_I);
                hv_ColumnI = hv_Column_COPY_INP_TMP.TupleSelect(hv_I);
                //Allow multiple strings for a single position.
                if ((int)(new HTuple(hv_N.TupleEqual(1))) != 0)
                {
                    hv_StringI = hv_String_COPY_INP_TMP.Clone();
                }
                else
                {
                    //In case of multiple positions, only single strings
                    //are allowed per position.
                    //For line breaks, use \n in this case.
                    hv_StringI = hv_String_COPY_INP_TMP.TupleSelect(hv_I);
                }
                //Default settings
                //-1 is mapped to 12.
                if ((int)(new HTuple(hv_RowI.TupleEqual(-1))) != 0)
                {
                    hv_RowI = 12;
                }
                if ((int)(new HTuple(hv_ColumnI.TupleEqual(-1))) != 0)
                {
                    hv_ColumnI = 12;
                }
                //
                //Split string into one string per line.
                hv_StringI = ((("" + hv_StringI) + "")).TupleSplit("\n");
                //
                //Estimate extentions of text depending on font size.
                HOperatorSet.GetFontExtents(hv_WindowHandle, out hv_MaxAscent, out hv_MaxDescent,
                    out hv_MaxWidth, out hv_MaxHeight);
                if ((int)(new HTuple(hv_CoordSystem.TupleEqual("window"))) != 0)
                {
                    hv_R1 = hv_RowI.Clone();
                    hv_C1 = hv_ColumnI.Clone();
                }
                else
                {
                    //Transform image to window coordinates.
                    hv_FactorRowI = (1.0 * hv_HeightWin) / ((hv_RowI2Part - hv_RowI1Part) + 1);
                    hv_FactorColumnI = (1.0 * hv_WidthWin) / ((hv_ColumnI2Part - hv_ColumnI1Part) + 1);
                    hv_R1 = (((hv_RowI - hv_RowI1Part) + 0.5) * hv_FactorRowI) - 0.5;
                    hv_C1 = (((hv_ColumnI - hv_ColumnI1Part) + 0.5) * hv_FactorColumnI) - 0.5;
                }
                //
                //Display text box depending on text size.
                hv_UseShadow = 1;
                hv_ShadowColor = "gray";
                if ((int)(new HTuple(((hv_Box_COPY_INP_TMP.TupleSelect(0))).TupleEqual("true"))) != 0)
                {
                    if (hv_Box_COPY_INP_TMP == null)
                        hv_Box_COPY_INP_TMP = new HTuple();
                    hv_Box_COPY_INP_TMP[0] = "white";
                }
                if ((int)(new HTuple((new HTuple(hv_Box_COPY_INP_TMP.TupleLength())).TupleGreater(
                    1))) != 0)
                {
                    if ((int)(new HTuple(((hv_Box_COPY_INP_TMP.TupleSelect(1))).TupleEqual("true"))) != 0)
                    {
                        //Use default ShadowColor set above
                    }
                    else if ((int)(new HTuple(((hv_Box_COPY_INP_TMP.TupleSelect(1))).TupleEqual(
                        "false"))) != 0)
                    {
                        hv_UseShadow = 0;
                    }
                    else
                    {
                        hv_ShadowColor = hv_Box_COPY_INP_TMP[1];
                        //Valid color?
                        try
                        {
                            HOperatorSet.SetColor(hv_WindowHandle, hv_Box_COPY_INP_TMP.TupleSelect(
                                1));
                        }
                        // catch (Exception) 
                        catch (HalconException HDevExpDefaultException1)
                        {
                            HDevExpDefaultException1.ToHTuple(out hv_Exception);
                            hv_Exception = "Wrong value of control parameter Box[1] (must be a 'true', 'false', or a valid color string)";
                            throw new HalconException(hv_Exception);
                        }
                    }
                }
                if ((int)(new HTuple(((hv_Box_COPY_INP_TMP.TupleSelect(0))).TupleNotEqual("false"))) != 0)
                {
                    //Valid color?
                    try
                    {
                        HOperatorSet.SetColor(hv_WindowHandle, hv_Box_COPY_INP_TMP.TupleSelect(
                            0));
                    }
                    // catch (Exception) 
                    catch (HalconException HDevExpDefaultException1)
                    {
                        HDevExpDefaultException1.ToHTuple(out hv_Exception);
                        hv_Exception = "Wrong value of control parameter Box[0] (must be a 'true', 'false', or a valid color string)";
                        throw new HalconException(hv_Exception);
                    }
                    //Calculate box extents
                    hv_StringI = (" " + hv_StringI) + " ";
                    hv_Width = new HTuple();
                    for (hv_Index = 0; (int)hv_Index <= (int)((new HTuple(hv_StringI.TupleLength()
                        )) - 1); hv_Index = (int)hv_Index + 1)
                    {
                        HOperatorSet.GetStringExtents(hv_WindowHandle, hv_StringI.TupleSelect(hv_Index),
                            out hv_Ascent, out hv_Descent, out hv_W, out hv_H);
                        hv_Width = hv_Width.TupleConcat(hv_W);
                    }
                    hv_FrameHeight = hv_MaxHeight * (new HTuple(hv_StringI.TupleLength()));
                    hv_FrameWidth = (((new HTuple(0)).TupleConcat(hv_Width))).TupleMax();
                    hv_R2 = hv_R1 + hv_FrameHeight;
                    hv_C2 = hv_C1 + hv_FrameWidth;
                    //Display rectangles
                    HOperatorSet.GetDraw(hv_WindowHandle, out hv_DrawMode);
                    HOperatorSet.SetDraw(hv_WindowHandle, "fill");
                    //Set shadow color
                    HOperatorSet.SetColor(hv_WindowHandle, hv_ShadowColor);
                    if ((int)(hv_UseShadow) != 0)
                    {
                        HOperatorSet.DispRectangle1(hv_WindowHandle, hv_R1 + 1, hv_C1 + 1, hv_R2 + 1,
                            hv_C2 + 1);
                    }
                    //Set box color
                    HOperatorSet.SetColor(hv_WindowHandle, hv_Box_COPY_INP_TMP.TupleSelect(0));
                    HOperatorSet.DispRectangle1(hv_WindowHandle, hv_R1, hv_C1, hv_R2, hv_C2);
                    HOperatorSet.SetDraw(hv_WindowHandle, hv_DrawMode);
                }
                //Write text.
                for (hv_Index = 0; (int)hv_Index <= (int)((new HTuple(hv_StringI.TupleLength())) - 1); hv_Index = (int)hv_Index + 1)
                {
                    //Set color
                    if ((int)(new HTuple(hv_N.TupleEqual(1))) != 0)
                    {
                        //Wiht a single text position, each text line
                        //may get a different color.
                        hv_CurrentColor = hv_Color_COPY_INP_TMP.TupleSelect(hv_Index % (new HTuple(hv_Color_COPY_INP_TMP.TupleLength()
                            )));
                    }
                    else
                    {
                        //With multiple text positions, each position
                        //gets a single color for all text lines.
                        hv_CurrentColor = hv_Color_COPY_INP_TMP.TupleSelect(hv_I % (new HTuple(hv_Color_COPY_INP_TMP.TupleLength()
                            )));
                    }
                    if ((int)((new HTuple(hv_CurrentColor.TupleNotEqual(""))).TupleAnd(new HTuple(hv_CurrentColor.TupleNotEqual(
                        "auto")))) != 0)
                    {
                        //Valid color?
                        try
                        {
                            HOperatorSet.SetColor(hv_WindowHandle, hv_CurrentColor);
                        }
                        // catch (Exception) 
                        catch (HalconException HDevExpDefaultException1)
                        {
                            HDevExpDefaultException1.ToHTuple(out hv_Exception);
                            hv_Exception = ((("Wrong value of control parameter Color[" + (hv_Index % (new HTuple(hv_Color_COPY_INP_TMP.TupleLength()
                                )))) + "] == '") + hv_CurrentColor) + "' (must be a valid color string)";
                            throw new HalconException(hv_Exception);
                        }
                    }
                    else
                    {
                        HOperatorSet.SetRgb(hv_WindowHandle, hv_Red, hv_Green, hv_Blue);
                    }
                    //Finally display text
                    hv_RowI = hv_R1 + (hv_MaxHeight * hv_Index);
                    HOperatorSet.SetTposition(hv_WindowHandle, hv_RowI, hv_C1);
                    HOperatorSet.WriteString(hv_WindowHandle, hv_StringI.TupleSelect(hv_Index));
                }
            }
            //Reset changed window settings
            HOperatorSet.SetRgb(hv_WindowHandle, hv_Red, hv_Green, hv_Blue);
            HOperatorSet.SetPart(hv_WindowHandle, hv_RowI1Part, hv_ColumnI1Part, hv_RowI2Part,
                hv_ColumnI2Part);

            return;
        }
    }
}
