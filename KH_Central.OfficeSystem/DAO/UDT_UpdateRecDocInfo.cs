using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FISCA.UDT;

namespace KH_Central.OfficeSystem.DAO
{

    /// <summary>
    /// 上傳異動名冊檢視用 UDT
    /// </summary>
    [TableName("kh_central.office_system.update_rec_doc_info")]
    public class UDT_UpdateRecDocInfo:ActiveRecord
    {
        ///<summary>
        /// 學年度
        ///</summary>
        [Field(Field = "school_year", Indexed = false)]
        public int SchoolYear { get; set; }

        ///<summary>
        /// 學期
        ///</summary>
        [Field(Field = "semester", Indexed = false)]
        public int Semester { get; set; }

        ///<summary>
        /// 名冊名稱
        ///</summary>
        [Field(Field = "name", Indexed = false)]
        public string Name { get; set; }


        ///<summary>
        /// 名冊類別
        ///</summary>
        [Field(Field = "type", Indexed = false)]
        public string Type { get; set; }

        ///<summary>
        /// 上傳日期
        ///</summary>
        [Field(Field = "upload_date", Indexed = false)]
        public DateTime UploadDate { get; set; }

        ///<summary>
        /// 局端檢核內容
        ///</summary>
        [Field(Field = "central_memo", Indexed = false)]
        public string CentralMemo { get; set; }

        ///<summary>
        /// 局端檢核未通過原因
        ///</summary>
        [Field(Field = "central_msg", Indexed = false)]
        public string CentralMsg { get; set; }
    }
}
