using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FISCA.UDT;

namespace KH_Central.OfficeSystem.DAO
{
    /// <summary>
    /// 局端回傳檢核資料
    /// </summary>
     [TableName("kh_central.office_system.central_data")]
    public class UDT_CentralData:ActiveRecord
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
        [Field(Field = "doc_name", Indexed = false)]
        public string DocName { get; set; }

        ///<summary>
        /// 名冊類別
        ///</summary>
        [Field(Field = "doc_type", Indexed = false)]
        public string DocType { get; set; }

        ///<summary>
        /// 名冊文號
        ///</summary>
        [Field(Field = "doc_no", Indexed = false)]
        public string DocNo { get; set; }

        ///<summary>
        /// 是否已更新至系統異動名冊與學生異動
        ///</summary>
        [Field(Field = "is_update", Indexed = false)]
        public bool isUpdate { get; set; }

        ///<summary>
        /// 名冊更新置系統日期
        ///</summary>
        [Field(Field = "doc_update_date", Indexed = false)]
        public DateTime DocUpdateDate { get; set; }

        ///<summary>
        /// 局端名冊檢核狀態
        ///</summary>
        [Field(Field = "c_check_status", Indexed = false)]
        public string CCheckStatus { get; set; }

        ///<summary>
        /// 局端名冊檢核日期
        ///</summary>
        [Field(Field = "c_check_date", Indexed = false)]
        public DateTime CDocUpdateDate { get; set; }

        ///<summary>
        /// 系統內名冊UID
        ///</summary>
        [Field(Field = "doc_uid", Indexed = false)]
        public string DocUID { get; set; }

        ///<summary>
        /// 名冊是否已經被刪傳(True表示ischool名冊已經被刪)
        ///</summary>
        [Field(Field = "is_delete", Indexed = false)]
        public bool isDelete { get; set; }

        ///<summary>
        /// 局端名冊檢核未通過原因
        ///</summary>
        [Field(Field = "c_check_msg", Indexed = false)]
        public string CCheckMsg { get; set; }

    }
}
