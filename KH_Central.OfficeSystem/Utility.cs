using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using KH_Central.OfficeSystem.DAO;
using System.IO;
using System.Windows.Forms;
using Aspose.Cells;
using FISCA.Presentation.Controls;
using System.Data;
using System.Net;

namespace KH_Central.OfficeSystem
{
    public class Utility
    {
        /// <summary>
        /// 取得 XML 內Attribute 值
        /// </summary>
        /// <param name="elm"></param>
        /// <param name="AttrName"></param>
        /// <returns></returns>
        public static string GetXMLAttributeStr(XElement elm, string AttrName)
        {
            string retVal = "";
            if (elm.Attribute(AttrName) != null)
                retVal = elm.Attribute(AttrName).Value.Trim();
            return retVal;
        }

        /// <summary>
        /// 取得局端區所資料
        /// </summary>
        /// <returns></returns>
        public static List<AddressRec> GetAddressRecList(List<UDT_CentralAddress> dataList)
        {            
            List<AddressRec> retVal = new List<AddressRec>();
            if (dataList.Count > 0)
            {
                foreach (UDT_CentralAddress data in dataList)
                {
                    AddressRec rec = new AddressRec();
                    rec.Town = data.Town;
                    rec.District = data.District;
                    rec.Area = data.Area;
                    retVal.Add(rec);
                }
            }
            return retVal;
        }

        /// <summary>
        /// 以區里為key，組合鄰資料
        /// </summary>
        /// <param name="dataList"></param>
        /// <returns></returns>
        public static List<AddressRec> AddressRecParse1(List<AddressRec> dataList)
        {
            List<AddressRec> retVal = new List<AddressRec>();
            Dictionary<string, AddressRec> strDict = new Dictionary<string, AddressRec>();
            foreach (AddressRec rec in dataList)
            {
                string key = rec.GetPKey();

                if (!strDict.ContainsKey(key))
                {
                    rec.dataAreaList = new List<string>();
                    strDict.Add(key, rec);
                }

                strDict[key].dataAreaList.Add(rec.Area);
                
            }

            // 組合
            foreach (KeyValuePair<string, AddressRec> data in strDict)
            {
                data.Value.Area = string.Join(",", data.Value.dataAreaList.ToArray());
                retVal.Add(data.Value);
            }
            return retVal;
        }

        /// <summary>
        /// 取得局端資料，並轉成UDT record
        /// </summary>
        /// <returns></returns>
        public static List<UDT_CentralAddress> GetCentralAddress()
        {
            List<UDT_CentralAddress> retVal = new List<UDT_CentralAddress>();

            //// 測試資料
            //Aspose.Cells.Workbook wb = new Aspose.Cells.Workbook();
            //wb.Open(new System.IO.MemoryStream(Properties.Resources.高雄市區里測試));

            //for(int row =1;row<=wb.Worksheets[0].Cells.MaxDataRow;row++)
            //{
            //    UDT_CentralAddress rec = new UDT_CentralAddress();
            //    rec.District = wb.Worksheets[0].Cells[row, 0].StringValue;
            //    rec.Town = wb.Worksheets[0].Cells[row, 1].StringValue;
            //    rec.Area = wb.Worksheets[0].Cells[row, 2].StringValue;
            //    retVal.Add(rec);
            //}

            // 取得學校本身學區
            string SchoolYear = K12.Data.School.DefaultSchoolYear;
            string SchoolCode = K12.Data.School.Code;
            
            //// test
            //SchoolCode = "593504";

            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create("http://163.32.129.9/kht/sch_area.jsp");
            req.Method = "POST";
            StringBuilder sb = new StringBuilder();
            req.Accept = "*/*";
            sb.Append("schNo=" + SchoolCode);
            sb.Append("&syear="+SchoolYear);            
            req.ContentType = "application/x-www-form-urlencoded";

            byte[] byteArray = Encoding.UTF8.GetBytes(sb.ToString());
            req.ContentLength = byteArray.Length;
            Stream dataStream = req.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();

            HttpWebResponse rsp;
            rsp = (HttpWebResponse)req.GetResponse();
            //= req.GetResponse();
            dataStream = rsp.GetResponseStream();

            Console.WriteLine(((HttpWebResponse)rsp).StatusDescription);
            StreamReader reader = new StreamReader(dataStream);
            // Read the content.
            string responseFromServer = reader.ReadToEnd();
            // Display the content.
            //Console.WriteLine(responseFromServer);

            XElement elmRoot = null;
            // 取得學區XML
            try
            {
                elmRoot = XElement.Parse(responseFromServer);
            }
            catch (Exception ex)
            {
                SmartSchool.ErrorReporting.ErrorMessgae msg = new SmartSchool.ErrorReporting.ErrorMessgae(ex);
            }
            // Clean up the streams.
            reader.Close();
            dataStream.Close();
            rsp.Close();

            if (elmRoot != null)
            {
                foreach (XElement elm in elmRoot.Elements("item"))
                {
                    UDT_CentralAddress rec = new UDT_CentralAddress();
                    if(elm.Attribute("里") !=null)
                        rec.District =elm.Attribute("里").Value;
                    if(elm.Attribute("區") !=null)
                        rec.Town = elm.Attribute("區").Value;
                    if(elm.Attribute("鄰") !=null)
                        rec.Area = elm.Attribute("鄰").Value;
                        retVal.Add(rec);
                }            
            }

            return retVal;        
        }

        /// <summary>
        /// 儲存字串
        /// </summary>
        /// <param name="name"></param>
        /// <param name="Data"></param>
        public static void SaveString(string name, string Data)
        {
            StreamWriter sw = new StreamWriter(System.Windows.Forms.Application.StartupPath + "\\" + name, false);
            sw.WriteLine(Data);            
            sw.Close();
        }

        /// <summary>
        /// 匯出 Excel
        /// </summary>
        /// <param name="inputReportName"></param>
        /// <param name="inputXls"></param>
        public static void CompletedXls(string inputReportName, Workbook inputXls)
        {
            string reportName = inputReportName;

            string path = Path.Combine(Application.StartupPath, "Reports");
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            path = Path.Combine(path, reportName + ".xls");

            Workbook wb = inputXls;

            if (File.Exists(path))
            {
                int i = 1;
                while (true)
                {
                    string newPath = Path.GetDirectoryName(path) + "\\" + Path.GetFileNameWithoutExtension(path) + (i++) + Path.GetExtension(path);
                    if (!File.Exists(newPath))
                    {
                        path = newPath;
                        break;
                    }
                }
            }

            try
            {
                wb.Save(path, Aspose.Cells.FileFormatType.Excel2003);
                System.Diagnostics.Process.Start(path);
            }
            catch
            {
                SaveFileDialog sd = new SaveFileDialog();
                sd.Title = "另存新檔";
                sd.FileName = reportName + ".xls";
                sd.Filter = "Excel檔案 (*.xls)|*.xls|所有檔案 (*.*)|*.*";
                if (sd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        wb.Save(sd.FileName, Aspose.Cells.FileFormatType.Excel2003);

                    }
                    catch
                    {
                        MsgBox.Show("指定路徑無法存取。", "建立檔案失敗", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
        }



        /// <summary>
        /// 將統計DataTable轉成XML
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static XElement ConvertDomainScoreCountDTtoXML(DataTable dt,int SchoolYear,int Semester,string SchoolCode,string SchoolName,decimal passScore,List<string> DomainNameList)
        {
            XElement retVal = new XElement("學期成績未達及格人數比率");
            retVal.SetAttributeValue("學年度", SchoolYear);
            retVal.SetAttributeValue("學期", Semester);
            retVal.SetAttributeValue("學校代碼", SchoolCode);
            retVal.SetAttributeValue("學校名稱", SchoolName);
            retVal.SetAttributeValue("及格分數", passScore);
            foreach (DataRow dr in dt.Rows)
            {
                XElement elm = new XElement("人數比率");
                elm.SetAttributeValue("年級", dr["年級"].ToString());
                elm.SetAttributeValue("學生人數", dr["學生人數"].ToString());
                foreach (string dName in DomainNameList)
                {
                    XElement subElm = new XElement("領域");
                    subElm.SetAttributeValue("名稱", dName);
                    subElm.SetAttributeValue("未達人數", dr[dName+"人數"].ToString());
                    subElm.SetAttributeValue("未達比率", dr[dName + "比率"].ToString());
                    elm.Add(subElm);
                }
                retVal.Add(elm);
            }
            return retVal;
        }

        /// <summary>
        /// 將統計XML轉成DataTable
        /// </summary>
        /// <param name="XmlString"></param>
        /// <returns></returns>
        public static DataTable ConvertDomainScoreCountXMLToDT(string XmlString)
        {
            DataTable retVal = new DataTable();
            try
            {
                XElement elmRoot = XElement.Parse(XmlString);
                if (elmRoot != null)
                {
                    // Columns
                    retVal.Columns.Add("年級");
                    retVal.Columns.Add("學生人數");
                    List<string> dNameList = new List<string>();
                    foreach (XElement elm in elmRoot.Elements("人數比率"))
                    {
                        foreach (XElement elms1 in elm.Elements("領域"))
                        {
                            string dname = elms1.Attribute("名稱").Value;
                            if (!dNameList.Contains(dname))
                                dNameList.Add(dname);
                        }
                    }

                    foreach (string name in dNameList)
                    {
                        retVal.Columns.Add(name + "人數");
                        retVal.Columns.Add(name + "比率");
                    }
                                        
                    foreach (XElement elm in elmRoot.Elements("人數比率"))
                    {
                        DataRow dr = retVal.NewRow();
                        dr["年級"] = elm.Attribute("年級").Value;
                        dr["學生人數"] = elm.Attribute("學生人數").Value;


                        foreach (XElement elms1 in elm.Elements("領域"))
                        {
                            string k1 = elms1.Attribute("名稱").Value + "人數";
                            string k2 = elms1.Attribute("名稱").Value + "比率";
                            dr[k1] = elms1.Attribute("未達人數").Value;
                            dr[k2] = elms1.Attribute("未達比率").Value;
                        }
                        retVal.Rows.Add(dr);
                    }
                }
            }
            catch (Exception ex)
            { 
            
            }

            return retVal;
        }


        /// <summary>
        /// 取得局端名冊上傳狀態,傳入學年度、學期、名冊類別
        /// </summary>        
        /// <param name="SchoolYear"></param>
        /// <param name="Semester"></param>
        /// <param name="Kind"></param>
        /// <returns></returns>
        public static string GetCenteralOfficeDocUploadStatus(int SchoolYear, int Semester, string Kind)
        {
            string retVal = "";
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create("http://163.32.129.9/kht/chk_dup.jsp");
            req.Method = "POST";
            StringBuilder sb = new StringBuilder();
            req.Accept = "*/*";
            sb.Append("schNo=" + K12.Data.School.Code);
            sb.Append("&syear="+SchoolYear);
            sb.Append("&seme="+Semester);
            sb.Append("&chgKind=" + Kind);
            req.ContentType = "application/x-www-form-urlencoded";

            byte[] byteArray = Encoding.UTF8.GetBytes(sb.ToString());
            req.ContentLength = byteArray.Length;
            Stream dataStream = req.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();

            HttpWebResponse rsp;
            rsp = (HttpWebResponse)req.GetResponse();
            //= req.GetResponse();
            dataStream = rsp.GetResponseStream();

            Console.WriteLine(((HttpWebResponse)rsp).StatusDescription);
            StreamReader reader = new StreamReader(dataStream);
            // Read the content.
            string responseFromServer = reader.ReadToEnd();
            XElement elmRoot = null;
            try
            {
                // <名冊狀態><狀態>已上傳未審核</狀態></名冊狀態>
                elmRoot = XElement.Parse(responseFromServer);
                if (elmRoot.Element("狀態") != null)                    
                        retVal = elmRoot.Element("狀態").Value;
            }
            catch (Exception ex)
            {
                SmartSchool.ErrorReporting.ErrorMessgae msg = new SmartSchool.ErrorReporting.ErrorMessgae(ex);
            }
            reader.Close();
            dataStream.Close();
            rsp.Close();

            return retVal;
        }


        /// <summary>
        /// 取得目前系統學年度、學期，學校名冊上傳訊息
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, List<string>> GetCenteralOfficeUnuploadNotify()
        {
            Dictionary<string, List<string>> returnData = new Dictionary<string, List<string>>();

            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create("http://163.32.129.9/khdc2/unupload_notify.jsp");
            req.Method = "POST";
            StringBuilder sb = new StringBuilder();
            req.Accept = "*/*";
            sb.Append("syear=" + K12.Data.School.DefaultSchoolYear);
            sb.Append("&seme=" + K12.Data.School.DefaultSemester);
            sb.Append("&schno=" + K12.Data.School.Code);            
            req.ContentType = "application/x-www-form-urlencoded";

            byte[] byteArray = Encoding.UTF8.GetBytes(sb.ToString());
            req.ContentLength = byteArray.Length;
            Stream dataStream = req.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();

            HttpWebResponse rsp;
            rsp = (HttpWebResponse)req.GetResponse();
            //= req.GetResponse();
            dataStream = rsp.GetResponseStream();

            Console.WriteLine(((HttpWebResponse)rsp).StatusDescription);
            StreamReader reader = new StreamReader(dataStream);
            // Read the content.
            string responseFromServer = reader.ReadToEnd();
            string rspXML = "<root>" + responseFromServer+"</root>";

            XElement elmRoot = null;
            // 當尚未設定上傳時間，不解析
            if (!rspXML.Contains("尚未設定上傳時間"))
            try
            {
                // <名冊狀態><狀態>已上傳未審核</狀態></名冊狀態>
                elmRoot = XElement.Parse(rspXML);
                foreach (XElement elm in elmRoot.Elements())
                {
                    string name = " "+elm.Attribute("學年度").Value+"學年度第"+elm.Attribute("學期").Value+"學期 "+elm.Name.ToString();                    if (!returnData.ContainsKey(name))
                        returnData.Add(name, new List<string>());
                    foreach(XElement elm1 in elm.Elements())
                        foreach (XAttribute attr in elm1.Attributes())
                        {
                            returnData[name].Add(attr.Name.ToString() + "：" + attr.Value);
                        }                
                }

            }
            catch (Exception ex)
            {
                SmartSchool.ErrorReporting.ErrorMessgae msg = new SmartSchool.ErrorReporting.ErrorMessgae(ex);
            }
            reader.Close();
            dataStream.Close();
            rsp.Close();

            return returnData;
        }
    }
}
