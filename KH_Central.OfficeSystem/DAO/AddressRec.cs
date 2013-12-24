using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KH_Central.OfficeSystem.DAO
{
    /// <summary>
    /// 地址暫存
    /// </summary>
    public class AddressRec
    {
        /// <summary>
        /// 區
        /// </summary>
        public string Town { get; set; }

        /// <summary>
        /// 里
        /// </summary>
        public string District { get; set; }

        /// <summary>
        /// 鄰
        /// </summary>
        public string Area { get; set; }

        /// <summary>
        /// Key 值
        /// </summary>
        /// <returns></returns>
        public string GetPKey()
        {
            if (string.IsNullOrEmpty(Area))
                return Town + District;
            else
                return Town + District+Area+"鄰";
        }

        /// <summary>
        /// 鄰集合
        /// </summary>
        public List<string> dataAreaList { get; set; }
    }
}
