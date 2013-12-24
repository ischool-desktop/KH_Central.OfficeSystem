using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FISCA.UDT;

namespace KH_Central.OfficeSystem.DAO
{
    [TableName("kh_central.office_system.domain_score_count")]
    public class UDT_DomainScoreCount:ActiveRecord
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
        /// 領域成績統計資料
        ///</summary>
        [Field(Field = "data", Indexed = false)]
        public string Data { get; set; }

        ///<summary>
        /// 上傳日期時間
        ///</summary>
        [Field(Field = "upload_date", Indexed = false)]
        public DateTime UploadDate { get; set; }

        ///<summary>
        /// 上傳狀態
        ///</summary>
        [Field(Field = "status", Indexed = false)]
        public string Status { get; set; }
    }
}
