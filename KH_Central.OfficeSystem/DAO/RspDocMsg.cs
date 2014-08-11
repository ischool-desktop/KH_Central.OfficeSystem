using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KH_Central.OfficeSystem.DAO
{
    /// <summary>
    /// 上傳名冊回傳訊息用
    /// </summary>
    public class RspDocMsg
    {
        /// <summary>
        /// 學年度
        /// </summary>
        public string SchoolYear { get; set; }

        /// <summary>
        /// 學期
        /// </summary>
        public string Semester { get; set; }

        /// <summary>
        /// 名稱
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 訊息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 更新日期
        /// </summary>
        public DateTime UpdateDate { get; set; }
    }
}
