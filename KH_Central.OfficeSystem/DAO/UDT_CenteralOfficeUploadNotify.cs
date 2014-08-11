using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FISCA.UDT;


namespace KH_Central.OfficeSystem.DAO
{
    /// <summary>
    /// 局端上傳通知紀錄
    /// </summary>
    [TableName("kh_central.office_system.CenteralOfficeUploadNotify")]
    public class UDT_CenteralOfficeUploadNotify:ActiveRecord
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

        /// <summary>
        /// 名稱
        /// </summary>
        [Field(Field = "name", Indexed = false)]
        public string Name { get; set; }

        ///<summary>
        /// 已通知
        ///</summary>
        [Field(Field = "is_notify", Indexed = false)]
        public bool isNotify { get; set; }

        /// <summary>
        /// 使用者
        /// </summary>
        [Field(Field = "user", Indexed = false)]
        public string User { get; set; }

        ///<summary>
        /// 通知日期
        ///</summary>
        [Field(Field = "notify_date", Indexed = false)]
        public DateTime NotifyDate { get; set; }
    }
}
