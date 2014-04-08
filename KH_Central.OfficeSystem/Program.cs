using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FISCA;
using FISCA.Presentation;
using FISCA.Presentation.Controls;
using System.ComponentModel;
using KH_Central.OfficeSystem.DAO;
using FISCA.Permission;

namespace KH_Central.OfficeSystem
{
    public class Program
    {
        private static BackgroundWorker _bgWorker;

        // 檢查是否載入局端回傳訊息
        private static bool _CheckLoadMsg = false;
        private static List<string>_ErrorMsg;
        private static List<string> _CheckRData;

        [MainMethod()]
        static public void Main()
        {
            _bgWorker = new BackgroundWorker();
            _bgWorker.DoWork += new DoWorkEventHandler(_bgWorker_DoWork);
            _bgWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_bgWorker_RunWorkerCompleted);

            _ErrorMsg = new List<string>();
            _CheckRData = new List<string>();

            FISCA.Presentation.RibbonBarItem edit = FISCA.Presentation.MotherForm.RibbonBarItems["局端", "管理"];
            edit["高雄市局端"].Image = Properties.Resources.companies_save_64;
            edit["高雄市局端"].Size = RibbonBarButton.MenuButtonSize.Large;
            edit["高雄市局端"]["學區資料檢視"].Enable = UserAcl.Current["KH_Central.OfficeSystem_Catalog001"].Executable;
            edit["高雄市局端"]["學區資料檢視"].Click += delegate
            {
                SchoolDistrict sd = new SchoolDistrict();
                sd.ShowDialog();
            };

            edit["高雄市局端"]["上傳異動名冊清單"].Enable = UserAcl.Current["KH_Central.OfficeSystem_Catalog002"].Executable;
            edit["高雄市局端"]["上傳異動名冊清單"].Click += delegate
            {
                ChangeRosterList crl = new ChangeRosterList();
                crl.ShowDialog();
            };

            edit["高雄市局端"]["局端類別與學生類別設定"].Enable = UserAcl.Current["KH_Central.OfficeSystem_Catalog003"].Executable;
            edit["高雄市局端"]["局端類別與學生類別設定"].Click += delegate
            {
                StudentCategoryMapping scm = new StudentCategoryMapping();
                scm.ShowDialog();
            };

            edit["高雄市局端"]["局端核准文號登錄"].Enable = UserAcl.Current["KH_Central.OfficeSystem_Catalog004"].Executable;
            edit["高雄市局端"]["局端核准文號登錄"].Click += delegate
            {
                KH_Central.OfficeSystem.RibbonBar.CentralDataUpdateDocForm cdudf = new RibbonBar.CentralDataUpdateDocForm();
                cdudf.ShowDialog();                
            };


            edit["檢視已上傳名冊紀錄"].Image = Properties.Resources.companies_save_64;
            edit["檢視已上傳名冊紀錄"].Size = RibbonBarButton.MenuButtonSize.Large;
            edit["檢視已上傳名冊紀錄"].Enable = UserAcl.Current["KH_Central.OfficeSystem_Catalog005"].Executable;
            edit["檢視已上傳名冊紀錄"].Click += delegate
            {
                UploadRosterView urv = new UploadRosterView();
                urv.ShowDialog();
            };

            edit["未達及格標準"].Image = Properties.Resources.companies_save_64;
            edit["未達及格標準"].Size = RibbonBarButton.MenuButtonSize.Large;
            edit["未達及格標準"].Enable = UserAcl.Current["KH_Central.OfficeSystem_Catalog006"].Executable;
            edit["未達及格標準"].Click += delegate
            {
                RibbonBar.DomainScoreCountForm dscf = new RibbonBar.DomainScoreCountForm();
                dscf.ShowDialog();
            };

            edit["未達及格標準上傳檢視"].Image = Properties.Resources.companies_save_64;
            edit["未達及格標準上傳檢視"].Size = RibbonBarButton.MenuButtonSize.Large;
            edit["未達及格標準上傳檢視"].Enable = UserAcl.Current["KH_Central.OfficeSystem_Catalog007"].Executable;
            edit["未達及格標準上傳檢視"].Click += delegate
            {
                RibbonBar.DomainScoreCountView dscv = new RibbonBar.DomainScoreCountView();
                dscv.ShowDialog();
            };

            #region 處理權限註冊

            Catalog catalog1 = RoleAclSource.Instance["局端"]["功能按鈕"];
            catalog1.Add(new RibbonFeature("KH_Central.OfficeSystem_Catalog001", "學區資料檢視"));

            Catalog catalog2 = RoleAclSource.Instance["局端"]["功能按鈕"];
            catalog2.Add(new RibbonFeature("KH_Central.OfficeSystem_Catalog002", "上傳異動名冊清單"));

            Catalog catalog3 = RoleAclSource.Instance["局端"]["功能按鈕"];
            catalog3.Add(new RibbonFeature("KH_Central.OfficeSystem_Catalog003", "局端類別與學生類別設定"));

            Catalog catalog4 = RoleAclSource.Instance["局端"]["功能按鈕"];
            catalog4.Add(new RibbonFeature("KH_Central.OfficeSystem_Catalog004", "局端核准文號登錄"));

            Catalog catalog5 = RoleAclSource.Instance["局端"]["功能按鈕"];
            catalog5.Add(new RibbonFeature("KH_Central.OfficeSystem_Catalog005", "檢視已上傳名冊紀錄"));

            Catalog catalog6 = RoleAclSource.Instance["局端"]["功能按鈕"];
            catalog6.Add(new RibbonFeature("KH_Central.OfficeSystem_Catalog006", "未達及格標準"));

            Catalog catalog7 = RoleAclSource.Instance["局端"]["功能按鈕"];
            catalog7.Add(new RibbonFeature("KH_Central.OfficeSystem_Catalog007", "未達及格標準上傳檢視"));

            #endregion

            // 當權限設定上傳異動名冊清單可執行，就會檢查局端訊息與通知            
                if (UserAcl.Current["KH_Central.OfficeSystem_Catalog002"].Executable==true)
                {
                    _CheckLoadMsg = true;                    
                }
            MotherForm.AddPanel(ForumAdmin.Instance);

            _bgWorker.RunWorkerAsync();
        }

        static void _bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (_ErrorMsg.Count > 0)
            {
                FISCA.Presentation.Controls.MsgBox.Show(string.Join(",", _ErrorMsg.ToArray()));
            }

            if (_CheckLoadMsg==true && _CheckRData.Count > 0)
            {
                StringBuilder sb = new StringBuilder();                
                foreach (string str in _CheckRData)
                    sb.AppendLine(str);

                FISCA.Presentation.Controls.MsgBox.Show(sb.ToString(), "局端通知與訊息",System.Windows.Forms.MessageBoxButtons.OK,System.Windows.Forms.MessageBoxIcon.Information);
            }

        }

        static void _bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                // 更新 UDS UDT 方式            
                if (!FISCA.RTContext.IsDiagMode)
                    FISCA.ServerModule.AutoManaged("http://module.ischool.com.tw/module/137/KHCentralOffice/udm.xml");

                // 檢查並建立 UDT tables
                DAO.UDTTransfer.UDTTablesCreate();

                if (_CheckLoadMsg)
                {
                    // 檢查局端資料是否有回傳
                    _CheckRData.Add("[局端名冊檢核通知]");
                    _CheckRData.Add("局端檢核資料已回傳，請至 局端>高雄市局端>局端核准文號登錄，回傳名冊：");
                    foreach (string msg in DAO.QueryData.CheckCentralDocReturn())
                        _CheckRData.Add(msg);

                    // 空一行
                    _CheckRData.Add("");
                    // 取得局端未上傳訊息
                    Dictionary<string, List<string>> UnUpLoadMsgDict = Utility.GetCenteralOfficeUnuploadNotify();
                    foreach (string name in UnUpLoadMsgDict.Keys)
                    {
                        _CheckRData.Add("[局端名冊" + name + "]");
                        foreach (string attr in UnUpLoadMsgDict[name])
                            _CheckRData.Add(attr);
                    }
                }

                #region 待確認處理方式，先註解，不要每次讀取影響效能

                //// 取得局端資料，並轉成UDT record
                //List<UDT_CentralAddress> _UDT_CentralAddressList = Utility.GetCentralAddress();

                //// 取得目前UDT 內局端，並刪除資料
                //List<UDT_CentralAddress> delList = UDTTransfer.UDTCentralAddressSelectAll();
                //UDTTransfer.UDTCentralAddressDelete(delList);

                //// 新增最新資料到UDT
                //if (_UDT_CentralAddressList.Count > 0)
                //    UDTTransfer.UDTCentralAddressInsert(_UDT_CentralAddressList);

                #endregion
                

            }
            catch (Exception ex)
            {
                SmartSchool.ErrorReporting.ErrorMessgae errMsg = new SmartSchool.ErrorReporting.ErrorMessgae(ex);
                _ErrorMsg.Add("高雄局端系統載入發生錯誤：" + ex.Message);
            }
        }
    }
}
