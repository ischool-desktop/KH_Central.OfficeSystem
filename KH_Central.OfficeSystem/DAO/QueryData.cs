using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FISCA.Data;
using System.Data;
using System.Xml.Linq;
using System.Net;
using System.IO;
using System.Xml;

namespace KH_Central.OfficeSystem.DAO
{
    public class QueryData
    {
        /// <summary>
        /// 傳入學年度，取得沒有函報日期的名冊主資料
        /// </summary>
        /// <param name="SchoolYear"></param>
        /// <returns></returns>
        public static List<UpdateRecDoc> GetUpdateRecDocListBySchoolYear(int SchoolYear)
        {
            List<UpdateRecDoc> retVal = new List<UpdateRecDoc>();
            QueryHelper qh = new QueryHelper();
            string strSQL = "select id,school_year,semester,name,content from update_record_batch where school_year="+SchoolYear+" and ad_date is null order by school_year,semester,name";
            DataTable dt = qh.Select(strSQL);
            foreach (DataRow dr in dt.Rows)
            {
                UpdateRecDoc data = new UpdateRecDoc();
                data.ID = dr["id"].ToString();
                data.SchoolYear = int.Parse(dr["school_year"].ToString());
                data.Semester = int.Parse(dr["semester"].ToString());
                data.Name = dr["name"].ToString();
                string content = dr["content"].ToString();
                if (!string.IsNullOrEmpty(content))
                    data.Data = XElement.Parse(content);

                retVal.Add(data);
            }
            return retVal;
        }

        /// <summary>
        /// 取得學生類類別名稱
        /// </summary>
        /// <returns></returns>
        public static List<string> GetStudentCategoryNameList()
        {
            List<string> retVal = new List<string>();
            QueryHelper qh = new QueryHelper();
            string strSQL = "select prefix,name from tag where category='Student' order by prefix,name";
            DataTable dt = qh.Select(strSQL);
            foreach (DataRow dr in dt.Rows)
            {
                string p = "";
                if (dr[0] != null)
                    if (dr[0].ToString() != "")
                        p = dr[0].ToString()+":";

                retVal.Add(p + dr[1].ToString());
            }
            return retVal;
        }

        /// <summary>
        /// 取得沒有核准文號名冊
        /// </summary>
        /// <returns></returns>
        public static List<UpdateRecDoc> GetUpdateRecDocListAdNumberNull()
        {
            List<UpdateRecDoc> retVal = new List<UpdateRecDoc>();
            QueryHelper qh = new QueryHelper();
            string strSQL = "select id,school_year,semester,name,content from update_record_batch where ad_number is null order by school_year,semester,name";
            DataTable dt = qh.Select(strSQL);
            foreach (DataRow dr in dt.Rows)
            {
                UpdateRecDoc data = new UpdateRecDoc();
                data.ID = dr["id"].ToString();
                data.SchoolYear = int.Parse(dr["school_year"].ToString());
                data.Semester = int.Parse(dr["semester"].ToString());
                data.Name = dr["name"].ToString();
                string content = dr["content"].ToString();
                if (!string.IsNullOrEmpty(content))
                    data.Data = XElement.Parse(content);

                retVal.Add(data);
            }
            return retVal;
        }

        /// <summary>
        /// 取得沒有核准文號名冊ID
        /// </summary>
        /// <returns></returns>
        public static List<string> GetUpdateRecDocListAdNumberNullID()
        {
            List<string> retVal = new List<string>();
            QueryHelper qh = new QueryHelper();
            string strSQL = "select id from update_record_batch where ad_number is null;";
            DataTable dt = qh.Select(strSQL);
            foreach (DataRow dr in dt.Rows)
                retVal.Add(dr[0].ToString());

            return retVal;
        }

        /// <summary>
        /// 取得局端回傳資料
        /// </summary>
        /// <param name="SchoolNo"></param>
        /// <param name="SchoolYear"></param>
        /// <param name="Semester"></param>
        /// <param name="docType"></param>
        /// <param name="docName"></param>
        /// <returns></returns>
        public static XElement GetCentralData(string SchoolCode, int SchoolYear, int Semester, string docType, string docName)
        {
            XElement retVal=null;
            
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(url.局端檢核相關資訊);
                // 測試資料
                //sb.Append("schNo=888888");
                //sb.Append("&syear=101");
                //sb.Append("&seme=2");
                //sb.Append("&chgKind=轉入學生名冊");
                //sb.Append("&name=101_2_轉入學生名冊");

                sb.Append("schNo=" + SchoolCode);
                sb.Append("&syear=" + SchoolYear);
                sb.Append("&seme=" + Semester);
                sb.Append("&chgKind=" + docType);
                sb.Append("&name=" + docName);

                HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(sb.ToString());
                req.Method = "GET";
                req.Accept = "*/*";
                req.ContentType = "text/xml";
                Stream dataStream;
                HttpWebResponse rsp;
                rsp = (HttpWebResponse)req.GetResponse();
                //= req.GetResponse();
                dataStream = rsp.GetResponseStream();
                Console.WriteLine(((HttpWebResponse)rsp).StatusDescription);
                StreamReader reader = new StreamReader(dataStream);
                // Read the content.
                string responseFromServer = reader.ReadToEnd();
                string xData = "<root>" + responseFromServer + "</root>";
                retVal = XElement.Parse(xData).Element("局端檢核");

                //// debug
                //StreamWriter sw = new StreamWriter(System.Windows.Forms.Application.StartupPath + "\\debug.txt", true);
                //sw.WriteLine(DateTime.Now.ToString());
                //sw.WriteLine(xData);
                //sw.Close();

                // 解析資料
                // Clean up the streams.
                reader.Close();
                dataStream.Close();
            }
            catch (Exception ex)
            {
                SmartSchool.ErrorReporting.ErrorMessgae errMsg = new SmartSchool.ErrorReporting.ErrorMessgae(ex);
            }
            return retVal;
        }


        ///// <summary>
        ///// 取得模擬局端傳回資料
        ///// </summary>
        ///// <returns></returns>
        //public static Dictionary<string,XElement> GetCentralTestTempDict()
        //{
        //    Dictionary<string, XElement> retVal = new Dictionary<string, XElement>();
        //    // 取得沒有核准文號名冊
        //    List<UpdateRecDoc> UpdateRecDocList = GetUpdateRecDocListAdNumberNull();

        //    string SchoolCode = K12.Data.School.Code;

        //    try
        //    {
        //        foreach (UpdateRecDoc rec in UpdateRecDocList)
        //        {
        //            StringBuilder sb = new StringBuilder();
        //            sb.Append("http://163.16.244.53/kht/vrf_no.jsp?");
        //            // 測試資料
        //            sb.Append("schNo=888888");
        //            sb.Append("&syear=101");
        //            sb.Append("&seme=2");
        //            sb.Append("&chgKind=轉入學生名冊");
        //            sb.Append("&name=101_2_轉入學生名冊");

        //            //sb.Append("schNo=" + SchoolCode);
        //            //sb.Append("&syear=" + rec.SchoolYear);
        //            //sb.Append("&seme=" + rec.Semester);
        //            //sb.Append("&chgKind=" + rec.GetDocType());
        //            //sb.Append("&name=" + rec.Name);

        //            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(sb.ToString());
        //            req.Method = "GET";
        //            req.Accept = "*/*";
        //            req.ContentType = "text/xml";
        //            Stream dataStream;
        //            HttpWebResponse rsp;
        //            rsp = (HttpWebResponse)req.GetResponse();
        //            //= req.GetResponse();
        //            dataStream = rsp.GetResponseStream();
        //            Console.WriteLine(((HttpWebResponse)rsp).StatusDescription);
        //            StreamReader reader = new StreamReader(dataStream);
        //            // Read the content.
        //            string responseFromServer = reader.ReadToEnd();
        //            string xxx = "<root>" + responseFromServer + "</root>";
        //            XElement docElm = XElement.Parse(xxx);
        //            // 解析資料
                    
                    
        //            // Clean up the streams.
        //            reader.Close();
        //            dataStream.Close();
        //        }
        //    }
        //    catch (Exception ex)
        //    { 
                
        //    }
        //    //XElement elmRoot = XElement.Parse(Properties.Resources.temp);
        //    //if (elmRoot != null)
        //    //{
        //    //    foreach (XElement elm in elmRoot.Elements("局端檢核"))
        //    //    { 
        //    //        string key =elm.Attribute("學年度").Value+"_"+elm.Attribute("學期").Value+"_"+elm.Attribute("名冊名稱").Value;
        //    //        retVal.Add(key, elm);
        //    //    }            
        //    //}

        //    return retVal;
        //}

       /// <summary>
       /// 檢查局端名冊是否回傳
       /// </summary>
       /// <returns></returns>
        public static List<string> CheckCentralDocReturn()
        {
            List<string> retVal = new List<string>();

            // 取得沒有核准文號名冊
            List<UpdateRecDoc> UpdateRecDocList=GetUpdateRecDocListAdNumberNull();

            foreach(UpdateRecDoc rec in UpdateRecDocList)
            {   
                // 呼叫局端取得資料
                XElement elm = GetCentralData(K12.Data.School.Code, rec.SchoolYear, rec.Semester, rec.GetDocType(), rec.Name);
                if (elm != null)
                {
                    // 有回傳名冊資料，目前會回傳，需要檢查檢核日期。
                    if (elm.Attribute("名冊名稱") != null)
                    {
                        bool add = false;
                        
                        if (elm.Attribute("檢核日期") != null)
                        {
                            DateTime dt;
                            if (DateTime.TryParse(elm.Attribute("檢核日期").Value, out dt))
                                if(dt.Year>1950)
                                    add = true;
                        }
                        if(add)                        
                            retVal.Add(rec.SchoolYear + "學年度 第" + rec.Semester + "學期, " + rec.Name);
                    }
                }
            }
            return retVal;
        }

        /// <summary>
        /// 取得各年級一般生學生ID，999表示未分年級
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, List<string>> GetGradeStudentDict1()
        {
            Dictionary<string, List<string>> retVal = new Dictionary<string,List<string>> ();
            QueryHelper qh = new QueryHelper();
            string strSQL = "select case when class.grade_year is null then 999 else class.grade_year end,student.id from student left join class on student.ref_class_id=class.id where student.status=1 order by class.grade_year;";
            DataTable dt = qh.Select(strSQL);
            foreach (DataRow dr in dt.Rows)
            {
                string gr = dr[0].ToString();
                if (!retVal.ContainsKey(gr))
                    retVal.Add(gr, new List<string>());

                retVal[gr].Add(dr[1].ToString());            
            }
            return retVal;
        }

        /// <summary>
        /// 取得kh_central.office_system.central_data UID,過律條件用：學年度+學期+名冊類別
        /// </summary>
        /// <returns></returns>
        public static List<string> GetkCentral_dataMaxUID1()
        {
            List<string> returnData = new List<string>();
            string query = @"select max(uid) from $kh_central.office_system.central_data  group by school_year,semester,doc_type";
            QueryHelper qh = new QueryHelper();
            DataTable dt = qh.Select(query);
            foreach(DataRow dr in dt.Rows)
                returnData.Add(dr[0].ToString());

            return returnData;
        }

        /// <summary>
        /// 取得kh_central.office_system.update_rec_doc_info UID,過律條件用：學年度+學期+名冊類別
        /// </summary>
        /// <returns></returns>
        public static List<string> GetUpdateRecDocInfoMaxUID()
        {
            List<string> returnData = new List<string>();
            string query = @"select max(uid) from $kh_central.office_system.update_rec_doc_info group by school_year,semester,type";
            QueryHelper qh = new QueryHelper();
            DataTable dt = qh.Select(query);
            foreach (DataRow dr in dt.Rows)
                returnData.Add(dr[0].ToString());

            return returnData;
        }

        /// <summary>
        /// 透過學年度、學期、類別取得uid
        /// </summary>
        /// <param name="SchoolYear"></param>
        /// <param name="Semester"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static List<string> GetUpdateRecDocInfoUID(int SchoolYear, int Semester, string type)
        {
            List<string> returnData = new List<string>();
            string query = @"select uid from $kh_central.office_system.update_rec_doc_info where school_year="+SchoolYear+" and semester="+Semester+" and type='"+type+"'";
            QueryHelper qh = new QueryHelper();
            DataTable dt = qh.Select(query);
            foreach (DataRow dr in dt.Rows)
                returnData.Add(dr[0].ToString());
            return returnData;
        }

        /// <summary>
        /// 取得局端已登錄到系統名稱
        /// </summary>
        /// <returns></returns>
        public static List<string> GetCenterDataTrue()
        {
            List<string> retData = new List<string>();
            //string query = @"select distinct school_year||'_'||semester||'_'||doc_type as p from $kh_central.office_system.central_data where is_update=true";
            string query = @"select distinct doc_name as p from $kh_central.office_system.central_data where is_update=true";
            QueryHelper qh = new QueryHelper();
            DataTable dt = qh.Select(query);
            foreach (DataRow dr in dt.Rows)
                retData.Add(dr[0].ToString());

            return retData;
        }
    }
}
