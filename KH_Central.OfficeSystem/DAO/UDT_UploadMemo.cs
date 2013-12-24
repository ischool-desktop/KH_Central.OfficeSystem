using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FISCA.UDT;

namespace KH_Central.OfficeSystem.DAO
{
    /// <summary>
    /// UDT 上傳說明
    /// </summary>
    [TableName("kh_central.office_system.upload_memo")]
    public class UDT_UploadMemo : ActiveRecord
    {
        /// <summary>
        /// 名冊名稱
        /// </summary>
        [Field(Field = "doc_name", Indexed = false)]
        public string DocName { get; set; }

        /// <summary>
        /// 辨識值
        /// </summary>
        [Field(Field = "key", Indexed = false)]
        public string Key { get; set; }

        /// <summary>
        /// 內容
        /// </summary>
        [Field(Field = "memo", Indexed = false)]
        public string Memo { get; set; }
    }
}
