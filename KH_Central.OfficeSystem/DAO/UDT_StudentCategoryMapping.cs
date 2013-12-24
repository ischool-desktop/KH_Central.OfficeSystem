using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FISCA.UDT;

namespace KH_Central.OfficeSystem.DAO
{
    /// <summary>
    /// 局端類別與學生類別對照 UDT
    /// </summary>
    [TableName("kh_central.office_system.student_category_mapping")]
    public class UDT_StudentCategoryMapping:ActiveRecord
    {
        ///<summary>
        /// 局端類別
        ///</summary>
        [Field(Field = "central_category", Indexed = false)]
        public string CentralCategory { get; set; }

        ///<summary>
        /// 學生類別
        ///</summary>
        [Field(Field = "student_category", Indexed = false)]
        public string StudentCategory { get; set; }
    }
}
