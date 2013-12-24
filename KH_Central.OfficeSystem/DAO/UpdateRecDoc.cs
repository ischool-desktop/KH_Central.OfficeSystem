using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace KH_Central.OfficeSystem.DAO
{
    /// <summary>
    /// 異動名冊資料
    /// </summary>
    public class UpdateRecDoc
    {
        /// <summary>
        /// 名冊編號
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// 名冊學年度
        /// </summary>
        public int SchoolYear { get; set; }

        /// <summary>
        /// 名冊學期
        /// </summary>
        public int Semester { get; set; }

        /// <summary>
        /// 名冊名稱
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 名冊內容
        /// </summary>
        public XElement Data { get; set; }

        /// <summary>
        /// 核准日期
        /// </summary>
        public DateTime adDate { get; set; }

        /// <summary>
        /// 核准文號
        /// </summary>
        public string adDocNo { get; set; }

        /// <summary>
        /// 取得異動名冊類別　
        /// </summary>
        /// <returns></returns>
        public string GetDocType()
        {
            string retVal = "";
            if (Data != null)
            {
                if (Data.Attribute("類別") != null)
                    retVal = Data.Attribute("類別").Value;
            }
            return retVal;
        }

        /// <summary>
        /// 取得名冊內異動紀錄編號
        /// </summary>
        /// <returns></returns>
        public List<string> GetURIDList()
        {
            List<string> retVal = new List<string>();
            foreach (XElement elm in Data.Elements("清單"))
            {
                foreach (XElement elm1 in elm.Elements("異動紀錄"))
                {
                    string id = "";
                    if (elm1.Attribute("編號") != null)
                        id = elm1.Attribute("編號").Value;

                    if (!string.IsNullOrEmpty(id))
                        retVal.Add(id);
                }
            }
            return retVal;
        }
    }
}
