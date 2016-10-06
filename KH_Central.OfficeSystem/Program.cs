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
using Campus.Message;

namespace KH_Central.OfficeSystem
{
    public class Program
    {
        private static BackgroundWorker _bgWorker;

        // 檢查是否載入局端回傳訊息
        private static bool _CheckLoadMsg = false;
        private static List<string>_ErrorMsg;
        private static List<string> _CheckRData1;
        private static List<string> _CheckRData2;

        [MainMethod()]
        static public void Main()
        {
            _bgWorker = new BackgroundWorker();
            _bgWorker.DoWork += new DoWorkEventHandler(_bgWorker_DoWork);
            _bgWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_bgWorker_RunWorkerCompleted);

            _ErrorMsg = new List<string>();
            _CheckRData1 = new List<string>();
            _CheckRData2 = new List<string>();
            FISCA.Presentation.RibbonBarItem edit = FISCA.Presentation.MotherForm.RibbonBarItems["局端", "管理"];
            edit["高雄市局端"].Image = Properties.Resources.ftp_site_64;
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


            edit["檢視已上傳名冊紀錄"].Image = Properties.Resources.admissions_zoom_64;
            edit["檢視已上傳名冊紀錄"].Size = RibbonBarButton.MenuButtonSize.Large;
            edit["檢視已上傳名冊紀錄"].Enable = UserAcl.Current["KH_Central.OfficeSystem_Catalog005"].Executable;
            edit["檢視已上傳名冊紀錄"].Click += delegate
            {
                UploadRosterView urv = new UploadRosterView();
                urv.ShowDialog();
            };

            edit["未達及格標準"].Image = Properties.Resources.columchart_zoom_64;
            edit["未達及格標準"].Size = RibbonBarButton.MenuButtonSize.Large;
            edit["未達及格標準"].Enable = UserAcl.Current["KH_Central.OfficeSystem_Catalog006"].Executable;
            edit["未達及格標準"].Click += delegate
            {
                RibbonBar.DomainScoreCountForm dscf = new RibbonBar.DomainScoreCountForm();
                dscf.ShowDialog();
            };

            edit["未達及格標準上傳檢視"].Image = Properties.Resources.system_zoom_64;
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

            if (_CheckLoadMsg==true)
            {

                if (_CheckRData1.Count > 0)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("局端檢核資料已回傳：");
                    foreach (string str in _CheckRData1)
                        sb.AppendLine(str);

                    CustomRecord cr = new CustomRecord();
                    cr.Title = "局端名冊檢核通知";
                    cr.Content = sb.ToString();
                    cr.Type = CrType.Type.Warning_Blue;

                    name n = new name();
                    n._messageTitle1 = "局端名冊檢核通知";
                    n._value1 = sb.ToString();
                    n.type = true;

                    IsViewForm_Open open = new IsViewForm_Open(n);
                    cr.OtherMore = open;

                    Campus.Message.MessageRobot.AddMessage(cr);
                }

                if (_CheckRData2.Count > 0)
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (string str in _CheckRData2)
                        sb.AppendLine(str);

                    CustomRecord cr = new CustomRecord();
                    cr.Title = "名冊審核狀態";
                    cr.Content = sb.ToString();
                    cr.Type = CrType.Type.Warning_Blue;

                    name n = new name();
                    n._messageTitle1 = "名冊審核狀態";
                    n._value1 = sb.ToString();
                    n.type = false;

                    IsViewForm_Open open = new IsViewForm_Open(n);
                    cr.OtherMore = open;

                    Campus.Message.MessageRobot.AddMessage(cr);
                }
            }

        }

        static void _bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                // 更新 UDS UDT 方式            
                if (!FISCA.RTContext.IsDiagMode)
                    FISCA.ServerModule.AutoManaged(url.局端系統UDM);

                // 檢查並建立 UDT tables
                DAO.UDTTransfer.UDTTablesCreate();

                if (_CheckLoadMsg)
                {

                    // 檢查局端資料是否有回傳                 
                    foreach (string msg in DAO.QueryData.CheckCentralDocReturn())
                        _CheckRData1.Add(msg);


                    #region 2016高雄小組會議討論修改成全部讀取巨曜回傳 Service 結果,又改:處理當上學期 新生一直通知道上傳，下學期畢業一直通知道上傳，其它只通知一次
                    // 讀取 Service 內容
                    Dictionary<string, List<RspDocMsg>> UnUpLoadMsgDict = Utility.GetCenteralOfficeUnuploadNotify();
                    //foreach (string name in UnUpLoadMsgDict.Keys)
                    //{
                    //    foreach(RspDocMsg rspMsg in UnUpLoadMsgDict[name])
                    //    {
                    //        string msg = name + " " + rspMsg.Name + "：" + rspMsg.Message + ", 通知時間：" + rspMsg.UploadDate;
                    //        _CheckRData2.Add(msg);
                    //    }          
                    //}


                    // 處理當上學期 新生一直通知道上傳，下學期畢業一直通知道上傳，其它只通知一次
                    List<string> docList = new List<string>(new string[] { "新生名冊", "畢業名冊", "轉入名冊", "轉出名冊", "復學名冊", "休學名冊", "死亡名冊"});
                    string cSchoolYear = K12.Data.School.DefaultSchoolYear;
                    string cSemester = K12.Data.School.DefaultSemester;
                    // 取得已上傳通知紀錄
                    Dictionary<string, UDT_CenteralOfficeUploadNotify> UploadNotifyDict = UDTTransfer.GetCenteralOfficeUploadNotifyBySchoolYearSemester(cSchoolYear, cSemester);

                    List<UDT_CenteralOfficeUploadNotify> UploadNotifyList = new List<UDT_CenteralOfficeUploadNotify>();

                    // 檢查沒有通知放入
                    foreach (string name in docList)
                    {
                        if (!UploadNotifyDict.ContainsKey(name))
                        {
                            UDT_CenteralOfficeUploadNotify data = new UDT_CenteralOfficeUploadNotify();
                            data.SchoolYear = int.Parse(cSchoolYear);
                            data.Semester = int.Parse(cSemester);
                            data.Name = name;
                            data.User = "admin";
                            data.isNotify = false;
                            data.NotifyDate = DateTime.Now;
                            UploadNotifyDict.Add(name, data);
                        }
                    }

                    // 判斷未上傳名冊
                    Dictionary<string, RspDocMsg> unLoadDocDict = new Dictionary<string, RspDocMsg>();
                    foreach (string name in UnUpLoadMsgDict.Keys)
                        foreach (RspDocMsg rsm in UnUpLoadMsgDict[name])
                        {
                            if (rsm.Message.Trim() == "未上傳" || rsm.Message.Trim() == "審核不通過")
                                if (!unLoadDocDict.ContainsKey(rsm.Name))
                                    unLoadDocDict.Add(rsm.Name, rsm);
                        }


                    foreach (string name in UploadNotifyDict.Keys)
                    {
                        bool addNotif = false;
                        if (name == "新生名冊")
                        {
                          
                            if (cSemester == "1" && unLoadDocDict.ContainsKey("新生名冊"))
                            {
                                UploadNotifyDict[name].isNotify = false;
                                addNotif = true;
                            }
                        }
                        else if (name == "畢業名冊")
                        {
                            if (cSemester == "2" && unLoadDocDict.ContainsKey("畢業名冊"))
                            {
                                UploadNotifyDict[name].isNotify = false;
                                addNotif = true;
                            }
                        }
                        else
                        {
                            if (unLoadDocDict.ContainsKey(name))
                            {
                                // 未上傳 通知一次，審核不通過 一直通知。
                                if (unLoadDocDict[name].Message == "審核不通過")
                                {
                                    addNotif = true;
                                }
                                else
                                {
                                    if (UploadNotifyDict[name].isNotify == false)
                                    {
                                        addNotif = true;
                                        UploadNotifyDict[name].isNotify = true;
                                    }
                                    else
                                    {
                                        // 通知過判斷日期，如果日期較新，再次通知
                                        if (unLoadDocDict.ContainsKey(name))
                                        {
                                            if (unLoadDocDict[name].UpdateDate > UploadNotifyDict[name].NotifyDate)
                                            {
                                                addNotif = true;

                                                UploadNotifyDict[name].NotifyDate = unLoadDocDict[name].UpdateDate;
                                            }
                                        }
                                    }
                                }     
                            }                                             
                        }

                        if (addNotif)
                        {
                            if (unLoadDocDict.ContainsKey(name))
                            {
                                string msg = unLoadDocDict[name].Name + "：" + unLoadDocDict[name].Message + ", 通知時間：" + unLoadDocDict[name].UpdateDate.ToString();
                                _CheckRData2.Add(msg);
                            }
                        }

                        UploadNotifyList.Add(UploadNotifyDict[name]);
                    }
                    // 回寫資料
                    UploadNotifyList.SaveAll();

                    #endregion

                    

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
