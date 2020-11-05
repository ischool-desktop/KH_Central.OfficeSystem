using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KH_Central.OfficeSystem
{
    class url
    {
        /// <summary>
        /// 用於desktop登入時的通知
        /// </summary>
        public static string 局端審核不過通知 = "http://163.16.244.53/khdc/unupload_notify.jsp";
        //範例:
        //http://163.16.244.53/khdc/unupload_notify.jsp?syear=103&seme=1&schno=888888

        /// <summary>
        /// 用於"局端核準文號登錄"功能
        /// </summary>
        public static string 局端檢核相關資訊 = "http://163.16.244.53/kht/vrf_no.jsp?";
        //範例:
        //http://163.16.244.53/kht/vrf_no.jsp?syear=103&seme=1&schNo=888888&chgKind=畢業名冊

        /// <summary>
        /// 學期領域成績未達60分人數與比率
        /// </summary>
        public static string 領域未達60分人數比率上傳資料 = "http://163.16.244.53/cc/ssup.jsp";
        //範例:
        //http://163.16.244.53/cc/ssup.jsp?schno=888888&user=admin&content=名冊內容

        /// <summary>
        /// 名冊上傳主要位置
        /// </summary>
        public static string 上傳異動名冊 = "http://163.16.244.53/cc/asc.jsp";
        //範例:
        //http://163.16.244.53/cc/asc.jsp?schno=888888&user=admin&content=名冊內容

        /// <summary>
        /// 學生資料取得位置
        /// </summary>
        public static string 讀取學區資料 = "http://163.16.244.53/newstd/sch_area.jsp";
        //範例:
        //http://163.16.244.53/newstd/sch_area.jsp?syear=103&schNo=888888

        /// <summary>
        /// 取得局端名冊上傳狀態
        /// </summary>
        public static string 取得局端名冊上傳狀態 = "http://163.16.244.53/kht/chk_dup.jsp";
        //範例:
        //http://163.16.244.53/kht/chk_dup.jsp?schNo=888888&syear=103&seme=1&chgKind=畢業名冊

        /// <summary>
        /// 局端系統的UDM位置
        /// </summary>
        public static string 局端系統UDM = "http://module.ischool.com.tw/module/137/KHCentralOffice/udm.xml";
    }
}
