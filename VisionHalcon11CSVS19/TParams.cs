using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisionHalcon11CSVS19
{
    class TParams
    {
        static double RefSearchRadius;
        static double ModelRadius;
        static double ModelThr;
        public static void GetPartParams(string RefFileName)
        {
            String FileName = "C:/vision/reference/" + RefFileName + ".xml";
            if (!File.Exists(FileName))
            {
                RefSearchRadius = 0;
                ModelRadius = 0;
                ModelThr = 0;
            }
            else
            {
                string LastElement = "";
                XmlTextReader rdr = new XmlTextReader(FileName);

                while (rdr.Read())
                {
                    switch (rdr.NodeType)
                    {
                        case XmlNodeType.None:
                            break;
                        case XmlNodeType.Element:
                            LastElement = rdr.Name;
                            break;
                        case XmlNodeType.Attribute:
                            break;
                        case XmlNodeType.Text:
                            if (LastElement == "SearchRadius")
                            {
                                RefSearchRadius = Convert.ToDouble(rdr.Value);
                            }
                            else if (LastElement == "ModelRadius")
                            {
                                ModelRadius = Convert.ToDouble(rdr.Value);
                            }
                            else if (LastElement == "Threshold")
                            {
                                ModelThr = Convert.ToDouble(rdr.Value);
                            }
                            break;
                        case XmlNodeType.CDATA:
                            break;
                        case XmlNodeType.EntityReference:
                            break;
                        case XmlNodeType.Entity:
                            break;
                        case XmlNodeType.ProcessingInstruction:
                            break;
                        case XmlNodeType.Comment:
                            break;
                        case XmlNodeType.Document:
                            break;
                        case XmlNodeType.DocumentType:
                            break;
                        case XmlNodeType.DocumentFragment:
                            break;
                        case XmlNodeType.Notation:
                            break;
                        case XmlNodeType.Whitespace:
                            break;
                        case XmlNodeType.SignificantWhitespace:
                            break;
                        case XmlNodeType.EndElement:
                            break;
                        case XmlNodeType.EndEntity:
                            break;
                        case XmlNodeType.XmlDeclaration:
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        public static double GetSearchRadius()
        {
            return RefSearchRadius;
        }

        public static double GetModelRadius()
        {
            return ModelRadius;
        }


        public static double GetModelThreshold()
        {
            return ModelThr;
        }
    }
}
