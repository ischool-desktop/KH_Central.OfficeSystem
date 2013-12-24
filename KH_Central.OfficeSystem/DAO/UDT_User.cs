using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FISCA.UDT;

namespace KH_Central.OfficeSystem.DAO
{
    /// <summary>
    /// 局端使用者
    /// </summary>
    [TableName("kh_central.office_system.user")]
    public class UDT_User:ActiveRecord
    {
        /// <summary>
        /// userid
        /// </summary>
        [Field(Field = "userid", Indexed = true)]
        public string userid { get; set; }        
    }
}
