using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FISCA.UDT;

namespace KH_Central.OfficeSystem.DAO
{
    /// <summary>
    /// UDT 局端學區
    /// </summary>
    [TableName("kh_central.office_system.central_address")]
    public class UDT_CentralAddress : ActiveRecord
    {
        /// <summary>
        /// 區
        /// </summary>
        [Field(Field = "town", Indexed = false)]
        public string Town { get; set; }

        /// <summary>
        /// 里
        /// </summary>
        [Field(Field = "district", Indexed = false)] 
        public string District { get; set; }

        /// <summary>
        /// 鄰
        /// </summary>
         [Field(Field = "area", Indexed = false)] 
        public string Area { get; set; }

        /// <summary>
        /// Key 值
        /// </summary>
        /// <returns></returns>
        public string GetPKey()
        {
            return Town + District;
        }    
    }
}
