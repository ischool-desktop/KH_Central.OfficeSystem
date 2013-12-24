using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KH_Central.OfficeSystem
{
    class Permissions
    {
        public static string 學區資料 { get { return "KH_Central.Office.System0001"; } }
        public static bool 學區資料權限
        {
            get
            {
                return FISCA.Permission.UserAcl.Current[學區資料].Executable;
            }
        }
    }
}
