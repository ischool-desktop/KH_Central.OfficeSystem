using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KH_Central.OfficeSystem.DAO;
using FISCA.UDT;
using FISCA.DSAClient;
using FISCA.DSAUtil;
using System.Xml.Linq;

namespace KH_Central.OfficeSystem.DAO
{
    public class UDTTransfer
    {
        /// <summary>
        /// 取得所有上傳異動名冊的名稱和日期
        /// </summary>
        /// <returns></returns>
        public static List<UDT_UpdateRecDocInfo> UDTUpdateRecDocInfoSelectAll()
        {
            List<UDT_UpdateRecDocInfo> retVal = new List<UDT_UpdateRecDocInfo>();
            AccessHelper accessHelper = new AccessHelper();            
            retVal = accessHelper.Select<UDT_UpdateRecDocInfo>();
            return retVal;        
        }

        /// <summary>
        /// 透過uid取得上傳異動名冊的名稱和日期
        /// </summary>
        /// <returns></returns>
        public static List<UDT_UpdateRecDocInfo> UDTUpdateRecDocInfoSelectByUIDs(List<string> uidList)
        {
            List<UDT_UpdateRecDocInfo> retVal = new List<UDT_UpdateRecDocInfo>();
            if (uidList.Count > 0)
            {
                AccessHelper accessHelper = new AccessHelper();
                string query = "uid in(" + string.Join(",", uidList.ToArray()) + ")  order by upload_date desc";
                retVal = accessHelper.Select<UDT_UpdateRecDocInfo>();
            }
            return retVal;
        }

        /// <summary>
        /// 新增上傳異動名冊的名稱和日期
        /// </summary>
        /// <param name="dataList"></param>
        public static void UDTUpdateRecDocInfoInsert(List<UDT_UpdateRecDocInfo> dataList)
        {
            if (dataList.Count > 0)
            {
                AccessHelper accessHelper = new AccessHelper();
                accessHelper.InsertValues(dataList);
            }        
        }

        /// <summary>
        /// 刪除上傳異動名冊的名稱和日期
        /// </summary>
        /// <param name="dataList"></param>
        public static void UDTUpdateRecDocInfoDelete(List<UDT_UpdateRecDocInfo> dataList)
        {
            if (dataList.Count > 0)
            {
                foreach (UDT_UpdateRecDocInfo data in dataList)
                    data.Deleted = true;

                AccessHelper accessHelper = new AccessHelper();
                accessHelper.DeletedValues(dataList);
            }
        }

        /// <summary>
        /// 取得局端類別與學生類別對照
        /// </summary>
        /// <returns></returns>
        public static List<UDT_StudentCategoryMapping> UDTStudentCategoryMappingSelectAll()
        {
            List<UDT_StudentCategoryMapping> retVal = new List<UDT_StudentCategoryMapping>();            
            AccessHelper accessHelper = new AccessHelper();
            retVal = accessHelper.Select<UDT_StudentCategoryMapping>();
            return retVal;
        }
        
        /// <summary>
        /// 新增局端類別與學生類別對照
        /// </summary>
        /// <param name="dataList"></param>
        public static void UDTStudentCategoryMappingInsert(List<UDT_StudentCategoryMapping> dataList)
        {
            if (dataList.Count > 0)
            {
                AccessHelper accessHelper = new AccessHelper();
                accessHelper.InsertValues(dataList);
            }        
        }

        /// <summary>
        /// 更新局端類別與學生類別對照
        /// </summary>
        /// <param name="dataList"></param>
        public static void UDTStudentCategoryMappingUpdate(List<UDT_StudentCategoryMapping> dataList)
        {
            if (dataList.Count > 0)
            {
                AccessHelper accessHelper = new AccessHelper();
                accessHelper.UpdateValues(dataList);
            }
        }

        /// <summary>
        /// 刪除局端類別與學生類別對照
        /// </summary>
        /// <param name="dataList"></param>
        public static void UDTStudentCategoryMappingDelete(List<UDT_StudentCategoryMapping> dataList)
        {
            if (dataList.Count > 0)
            {
                foreach (UDT_StudentCategoryMapping data in dataList)
                    data.Deleted = true;

                AccessHelper accessHelper = new AccessHelper();
                accessHelper.DeletedValues(dataList);
            }
        }

        /// <summary>
        /// 取得系統UDT局端學區
        /// </summary>
        /// <returns></returns>
        public static List<UDT_CentralAddress> UDTCentralAddressSelectAll()
        {
            List<UDT_CentralAddress> retVal = new List<UDT_CentralAddress>();
            AccessHelper accessHelper = new AccessHelper();
            retVal = accessHelper.Select<UDT_CentralAddress>();
            return retVal;
        }

        /// <summary>
        /// 新增系統UDT局端學區
        /// </summary>
        /// <param name="dataList"></param>
        public static void UDTCentralAddressInsert(List<UDT_CentralAddress> dataList)
        {
            if (dataList.Count > 0)
            {
                AccessHelper accessHelper = new AccessHelper();
                accessHelper.InsertValues(dataList);
            }
        }

        /// <summary>
        /// 刪除系統UDT局端學區
        /// </summary>
        /// <param name="dataList"></param>
        public static void UDTCentralAddressDelete(List<UDT_CentralAddress> dataList)
        {
            if (dataList.Count > 0)
            {
                foreach (UDT_CentralAddress data in dataList)
                    data.Deleted = true;

                AccessHelper accessHelper = new AccessHelper();
                accessHelper.DeletedValues(dataList);
            }
        }

        /// <summary>
        /// 新增上傳說明
        /// </summary>
        /// <param name="dataList"></param>
        public static void UDTUploadMemoInsert(List<UDT_UploadMemo> dataList)
        {
            if (dataList.Count > 0)
            {
                AccessHelper accessHelper = new AccessHelper();
                accessHelper.InsertValues(dataList);
            }
        }

        /// <summary>
        /// 更新上傳說明
        /// </summary>
        /// <param name="dataList"></param>
        public static void UDTUploadMemoUpdate(List<UDT_UploadMemo> dataList)
        {
            if (dataList.Count > 0)
            {
                AccessHelper accessHelper = new AccessHelper();
                accessHelper.UpdateValues(dataList);
            }
        }

        /// <summary>
        /// 透過名冊名稱取得上傳說明
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public static List<UDT_UploadMemo> UDTUploadMemoSelectByName(string Name)
        {
            List<UDT_UploadMemo> retVal = new List<UDT_UploadMemo>();

            AccessHelper accHelper = new AccessHelper();
            string qry = "doc_name='"+Name+"'";
            retVal = accHelper.Select<UDT_UploadMemo>(qry);

            return retVal;
        }

        /// <summary>
        /// 新增特定使用者
        /// </summary>
        public static void UDTInsertDefaultUser()
        {
            // 檢查 UDT 內是否存在
            AccessHelper accHelperSelect = new AccessHelper();
            string qry = "userid='ksjh_edu@ks.edu.tw'";
            List<UDT_User> data = accHelperSelect.Select<UDT_User>(qry);

            // 不存在新增
            if (data.Count == 0)
            {
                List<UDT_User> insertData = new List<UDT_User>();
                UDT_User addData = new UDT_User();
                addData.userid = "ksjh_edu@ks.edu.tw";
                insertData.Add(addData);
                AccessHelper accHelperInsert = new AccessHelper();
                accHelperInsert.InsertValues(insertData);
            }            
        
        }

        /// <summary>
        /// 載入預設使用到UDT
        /// </summary>
        public static void UDTTablesCreate()
        {
            FISCA.UDT.SchemaManager Manager = new SchemaManager(new DSConnection(FISCA.Authentication.DSAServices.DefaultDataSource));
            Manager.SyncSchema(new UDT_CentralAddress());
            Manager.SyncSchema(new UDT_StudentCategoryMapping());
            Manager.SyncSchema(new UDT_UpdateRecDocInfo());
            Manager.SyncSchema(new UDT_UploadMemo());
            Manager.SyncSchema(new UDT_User());
            Manager.SyncSchema(new UDT_CentralData());
            Manager.SyncSchema(new UDT_DomainScoreCount());
            UDTInsertDefaultUser();
        }

        /// <summary>
        /// 新增局端檢核後資料
        /// </summary>
        public static void UDTCentralDataInsert(List<UDT_CentralData> dataList)
        {
            if (dataList.Count > 0)
            {
                AccessHelper accessHelper = new AccessHelper();
                accessHelper.InsertValues(dataList);            
            }
        }

        /// <summary>
        /// 更新局端檢核後資料
        /// </summary>
        public static void UDTCentralDataUpdate(List<UDT_CentralData> dataList)
        {
            if (dataList.Count > 0)
            {
                AccessHelper accessHelper = new AccessHelper();
                accessHelper.UpdateValues(dataList);
            }
        }

        /// <summary>
        /// 刪除局端檢核後資料
        /// </summary>
        public static void UDTCentralDataDelete(List<UDT_CentralData> dataList)
        {
            if (dataList.Count > 0)
            {
                foreach (UDT_CentralData data in dataList)
                    data.Deleted = true;

                AccessHelper accessHelper = new AccessHelper();
                accessHelper.DeletedValues(dataList);
            }
        }

        /// <summary>
        /// 取得儲存局端檢核資料
        /// </summary>
        /// <returns></returns>
        public static List<UDT_CentralData> UDTCentralDataSelectAll()
        {
            List<UDT_CentralData> retVal = new List<UDT_CentralData>();
            AccessHelper accessHelper = new AccessHelper();
            string query = "order by uid desc";
            retVal = accessHelper.Select<UDT_CentralData>(query);
            return retVal;
        }

        /// <summary>
        /// 取得儲存局端檢核資料
        /// </summary>
        /// <returns></returns>
        public static List<UDT_CentralData> UDTCentralDataSelectByUIDList(List<string> uidList)
        {
            List<UDT_CentralData> retVal = new List<UDT_CentralData>();
            if (uidList.Count > 0)
            {
                AccessHelper accessHelper = new AccessHelper();
                string query = "uid in (" + string.Join(",", uidList.ToArray()) + ")";
                retVal = accessHelper.Select<UDT_CentralData>(query);
            }
            return retVal;
        }

        /// <summary>
        /// 新增領域未達及格人數比率資料
        /// </summary>
        /// <param name="DataList"></param>
        public static void UDTDomainScoreCountInsert(List<UDT_DomainScoreCount> DataList)
        {
            if (DataList.Count > 0)
            {
                AccessHelper _AccessHelper = new AccessHelper();
                _AccessHelper.InsertValues(DataList);
            }        
        }

        /// <summary>
        /// 更新領域未達及格人數比率資料
        /// </summary>
        /// <param name="DataList"></param>
        public static void UDTDomainScoreCountUpdate(List<UDT_DomainScoreCount> DataList)
        {
            if (DataList.Count > 0)
            {
                AccessHelper _AccessHelper = new AccessHelper();
                _AccessHelper.UpdateValues(DataList);
            }
        }

        /// <summary>
        /// 刪除領域未達及格人數比率資料
        /// </summary>
        /// <param name="DataList"></param>
        public static void UDTDomainScoreCountDelete(List<UDT_DomainScoreCount> DataList)
        {
            if (DataList.Count > 0)
            {
                foreach (UDT_DomainScoreCount data in DataList)
                    data.Deleted = true;

                AccessHelper _AccessHelper = new AccessHelper();
                _AccessHelper.DeletedValues(DataList);
            }
        }

        /// <summary>
        /// 取得所有領域未達及格人數比率資料
        /// </summary>
        /// <returns></returns>
        public static List<UDT_DomainScoreCount> UDTDomainScoreCountSelectAll()
        {
            AccessHelper _AccessHelper = new AccessHelper();
            return _AccessHelper.Select<UDT_DomainScoreCount>();
        }


        /// <summary>
        /// 取得局端通知紀錄
        /// </summary>
        /// <param name="SchoolYear"></param>
        /// <param name="Semester"></param>
        /// <returns></returns>
        public static Dictionary<string, UDT_CenteralOfficeUploadNotify> GetCenteralOfficeUploadNotifyBySchoolYearSemester(string SchoolYear,string Semester)
        {
            Dictionary<string, UDT_CenteralOfficeUploadNotify> retVal = new Dictionary<string, UDT_CenteralOfficeUploadNotify>();
            AccessHelper accessHelper = new AccessHelper ();
            string query="school_year="+SchoolYear+" and semester="+Semester;
            List<UDT_CenteralOfficeUploadNotify> valList =accessHelper.Select<UDT_CenteralOfficeUploadNotify>(query);

            foreach (UDT_CenteralOfficeUploadNotify data in valList)
            {
                if (!retVal.ContainsKey(data.Name))
                    retVal.Add(data.Name, data);
            }

            return retVal;
        }
    }
}
