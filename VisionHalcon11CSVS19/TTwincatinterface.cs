using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TwinCAT.Ads;
using System.Threading;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace VisionHalcon11CSVS19
{
    using TC_BOOL       = System.Byte;
    using TC_BYTE       = System.Byte;
    using TC_INT        = System.Int16;
    using TC_UINT       = System.UInt16;
    using TC_DWORD      = System.UInt32;
    using TC_REAL       = System.Single;
    using TC_LREAL      = System.Double; 
    using TC_ALIGN_8B   = System.Byte;
    using TC_ALIGN_16B  = System.Int16;
    using TC_ALIGN_32B  = System.UInt32;
    using TC_CHAR       = System.Char;

    public class TTwincatinterface
    {
        public TTwincatinterface(string[] args)
        {
            address = ArgParser.Parse(args);
            adsClient = new TcAdsClient();
            try
            {
                TCVarAccess = new Mutex();
                AdsConnected = false;
                AmsAddress address = ArgParser.Parse(args);
                adsClient.Connect(address);

                //create handles for the PLC variables;
                hStructVisionVar = adsClient.CreateVariableHandle(VISION_VAR);
                hStructVisionData = adsClient.CreateVariableHandle(VISION_VAR + ".TPartData");
                hVisionRequest = adsClient.CreateVariableHandle(VISION_VAR + ".eRequest");
                hVisionRequestError = adsClient.CreateVariableHandle(VISION_VAR + ".bRequestError");
                hVisionReady = adsClient.CreateVariableHandle(VISION_VAR + ".bReady");
                hVisionAlive = adsClient.CreateVariableHandle(VISION_VAR + ".bAlive");
                hVisionConnected = adsClient.CreateVariableHandle(VISION_VAR + ".bConnected");
                AdsConnected = true;
            }
            catch (Exception ex)
            {
                AdsConnected = false;
                MessageBox.Show(ex.Message);
            }
        }

        public bool IsConnected()
        {
            return AdsConnected;
        }

        public void readTcAllVisionData()
        {
            this.TCVarAccess.WaitOne();
            VisionData = (VISION_DATA)adsClient.ReadAny(hStructVisionVar, typeof(VISION_DATA));
            this.TCVarAccess.ReleaseMutex();
        }

        public void WriteVisionData(ref VISION_PART_DATA Datas)
        {
            this.TCVarAccess.WaitOne();
            adsClient.WriteAny(hStructVisionData, Datas);
            this.TCVarAccess.ReleaseMutex();
        }

        public VISION_REQUEST getVisionRequest()
        {
            return VisionData.VI_Request;
        }

        public void ClearRequest()
        {
            this.TCVarAccess.WaitOne();
            adsClient.WriteAny(hVisionRequest, (short)VISION_REQUEST.VR_None);
            VisionData.VI_Request = VISION_REQUEST.VR_None;
            this.TCVarAccess.ReleaseMutex();
        }

        public string getRefFileName()
        {
            return VisionData.VI_VisionRecipeName;
        }

        public void SetRequestError(bool Value)
        {
            this.TCVarAccess.WaitOne();
            adsClient.WriteAny(hVisionRequestError, Value);
            VisionData.VI_RequestError = Convert.ToByte(Value);
            this.TCVarAccess.ReleaseMutex();
        }

        public void SetReadyData(bool Value)
        {
            this.TCVarAccess.WaitOne();
            adsClient.WriteAny(hVisionReady, Value);
            this.TCVarAccess.ReleaseMutex();
        }

        public void SetAliveData(bool Value)
        {
            this.TCVarAccess.WaitOne();
            adsClient.WriteAny(hVisionAlive, Value);
            this.TCVarAccess.ReleaseMutex();
        }

        public void SetConnectedData(bool Value)
        {
            this.TCVarAccess.WaitOne();
            adsClient.WriteAny(hVisionConnected, Value);
            this.TCVarAccess.ReleaseMutex();
        }

        private const string VISION_VAR = "MAIN.sMMI_VisionVar";

        private readonly Mutex TCVarAccess;
        private readonly TcAdsClient adsClient;
        private readonly AmsAddress address;
        private VISION_DATA VisionData;

        private bool AdsConnected;

        //PLC variable handles
        private int hStructVisionVar;
        private int hStructVisionData;
        private int hVisionRequest;
        private int hVisionRequestError;
        private int hVisionReady;
        private int hVisionAlive;
        private int hVisionConnected;

        //Variable for visionHdl
        public enum VISION_REQUEST : TC_INT
        {
            VR_None = 0,
            VR_Init,
            VR_Analyse,
            VR_GrabImage
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class VISION_PART_DATA
        {
            public TC_LREAL VPD_X;
            public TC_LREAL VPD_Y;
            public TC_LREAL VPD_Theta;
            public TC_LREAL VPD_Score;
            [MarshalAs(UnmanagedType.I1)]
            public TC_BOOL VPD_Valid;
            [MarshalAs(UnmanagedType.I1)]
            public TC_BOOL VPD_Present;
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        private class VISION_DATA
        {
            public VISION_REQUEST VI_Request;
            [MarshalAs(UnmanagedType.I1)]
            private TC_BOOL VI_RequestDone;
            [MarshalAs(UnmanagedType.I1)]
            public TC_BOOL VI_RequestError;
            private TC_INT VI_MajorVersion;
            private TC_INT VI_MinorVersion;
            [MarshalAs(UnmanagedType.I1)]
            private TC_BOOL VI_Alive;
            [MarshalAs(UnmanagedType.I1)]
            private TC_BOOL VI_Connected;
            //specifies how .NET should marshal the string
            //SizeConst specifies the number of characters the string has.
            //'(inclusive the terminating null ).
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 21)]
            public string VI_VisionRecipeName = "";
            private VISION_PART_DATA VI_PartData;
            [MarshalAs(UnmanagedType.I1)]
            private TC_BOOL VI_Ready;
            [MarshalAs(UnmanagedType.I1)]
            TC_BOOL VI_SimRecipe;
            TC_INT VI_UseModel;
            TC_INT VI_MaxModel;
            [MarshalAs(UnmanagedType.I1)]
            TC_BOOL VI_MondemaMode;
            [MarshalAs(UnmanagedType.I1)]
            TC_BOOL VI_ConfigMode;
        };
    }
}
