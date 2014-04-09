using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using KH_Central.OfficeSystem.DAO;
using System.Xml.Linq;
using JHSchool.Data;
using JHSchool.Permrec.Feature.Legacy;
using FISCA.DSAUtil;

namespace KH_Central.OfficeSystem.RibbonBar
{
    public partial class CentralDataUpdateDocForm : FISCA.Presentation.Controls.BaseForm
    {
        BackgroundWorker _bgWorker = new BackgroundWorker();
        List<UDT_CentralData> _CentralDataList = new List<UDT_CentralData>();
        List<UpdateRecDoc> _UpdateRecDocList = new List<UpdateRecDoc>();
        List<string> _ErrorList = new List<string>();
        List<string> _CheckData1 = new List<string>();
        List<string> uids = new List<string>();
        List<string> _GUIDList;

        public CentralDataUpdateDocForm()
        {
            InitializeComponent();
            _GUIDList = new List<string>();
            _bgWorker.DoWork += new DoWorkEventHandler(_bgWorker_DoWork);
            _bgWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_bgWorker_RunWorkerCompleted);            
        }

        void _bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (_ErrorList.Count>0)
            {
                FISCA.Presentation.Controls.MsgBox.Show(string.Join(",",_ErrorList.ToArray()));            
            }

            List<UDT_CentralData> updateData = new List<UDT_CentralData>();
            // 放置畫面
            dgData.Rows.Clear();
            int co1 = 0, co2 = 0,count=0;
            foreach (UDT_CentralData data in _CentralDataList)
            {
                if (_GUIDList.Contains(data.DocUID))
                {
                    int rowIdx = dgData.Rows.Add();
                    dgData.Rows[rowIdx].Tag = data;
                    dgData.Rows[rowIdx].Cells[colSchoolYear.Index].Value = data.SchoolYear;
                    dgData.Rows[rowIdx].Cells[colSemester.Index].Value = data.Semester;
                    dgData.Rows[rowIdx].Cells[colType.Index].Value = data.DocType;
                    dgData.Rows[rowIdx].Cells[colName.Index].Value = data.DocName;
                    dgData.Rows[rowIdx].Cells[colDocNo.Index].Value = data.DocNo;
                    if (data.CDocUpdateDate.Year > 1950)
                        dgData.Rows[rowIdx].Cells[colCheckDate.Index].Value = data.CDocUpdateDate.ToShortDateString();
                    else
                        dgData.Rows[rowIdx].Cells[colCheckDate.Index].Value = "";

                    if (string.IsNullOrEmpty(data.CCheckStatus))
                        dgData.Rows[rowIdx].Cells[colCheckStaus.Index].Value = "尚未檢核";
                    else
                        dgData.Rows[rowIdx].Cells[colCheckStaus.Index].Value = data.CCheckStatus;

                    // 未通過原因
                    dgData.Rows[rowIdx].Cells[colChkMsg.Index].Value = data.CCheckMsg;

                    if (data.isUpdate)
                    {
                        dgData.Rows[rowIdx].Cells[colUpdated.Index].Value = "是";
                        co1++;
                    }
                    else
                    {
                        dgData.Rows[rowIdx].Cells[colUpdated.Index].Value = "否";
                        co2++;
                    }
                    count++;
                }
                else
                {
                    data.isDelete = true;
                }
            }
            // 當發現系統名冊有刪除，相對在UDT名冊資料標示並更新。
            if (updateData.Count > 0)
                UDTTransfer.UDTCentralDataUpdate(updateData);

            btnUpdate.Enabled = true;
            lblMsg.Text = "共 " + count + " 筆，已登錄 "+co1+"筆,未登錄 "+co2+"筆";
        }


        void _bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            // 取得紀錄局端回傳UDT
             uids = QueryData.GetkCentral_dataMaxUID1();
            _CentralDataList = UDTTransfer.UDTCentralDataSelectByUIDList(uids);

            // 取得系統內沒有核准文號的名冊
            _UpdateRecDocList = QueryData.GetUpdateRecDocListAdNumberNull();
            List<string> uuidList =new List<string> ();
            foreach (UDT_CentralData data in _CentralDataList)
                uuidList.Add(data.DocUID);

            // 取得目前系統內沒有核准文號名冊ID
            _GUIDList = QueryData.GetUpdateRecDocListAdNumberNullID();


            // 檢查新增資料
            List<UDT_CentralData> addData = new List<UDT_CentralData>();
            foreach (UpdateRecDoc data in _UpdateRecDocList)
            {
                if (!uuidList.Contains(data.ID))
                {
                    UDT_CentralData uData = new UDT_CentralData();
                    uData.DocName = data.Name;
                    uData.DocType = data.GetDocType();
                    uData.DocUID = data.ID;
                    uData.SchoolYear = data.SchoolYear;
                    uData.Semester = data.Semester;
                    uData.isUpdate = false;
                    uData.isDelete = false;
                    addData.Add(uData);
                }            
            }

            if (addData.Count > 0)
            {
                UDTTransfer.UDTCentralDataInsert(addData);
                uids = QueryData.GetkCentral_dataMaxUID1();
                _CentralDataList = UDTTransfer.UDTCentralDataSelectByUIDList(uids);
            }

            _ErrorList.Clear();
            // 有文號放入
            foreach (UDT_CentralData data in _CentralDataList)
            {
                try
                {
                    data.isDelete = false;
                    // 取得局端資料
                    XElement elmData = QueryData.GetCentralData(K12.Data.School.Code, data.SchoolYear, data.Semester, data.DocType, data.DocName);
                    if (elmData != null)
                    {
                        if (elmData.Attribute("名冊名稱") != null)
                        {
                            string key = data.SchoolYear + "_" + data.Semester + "_" + data.DocName + "_" + data.DocType;
                            string key1 = elmData.Attribute("學年度").Value + "_" + elmData.Attribute("學期").Value + "_" + elmData.Attribute("名冊名稱").Value + "_" + elmData.Attribute("名冊類別").Value;
                            if (key == key1)
                            {
                                if (elmData.Attribute("核准文號") == null)
                                    data.DocNo = "";
                                else
                                    data.DocNo = elmData.Attribute("核准文號").Value;

                                // 預設顯示 尚未檢核，當內容非空白再顯示值
                                data.CCheckStatus = "尚未檢核";
                                if (elmData.Attribute("檢核狀態") != null)
                                    if(elmData.Attribute("檢核狀態").Value.Trim()!="")
                                        data.CCheckStatus = elmData.Attribute("檢核狀態").Value;

                                // 未通過原因，預設空白
                                data.CCheckMsg = "";
                                if (elmData.Attribute("未通過原因") != null)
                                    if (elmData.Attribute("未通過原因").Value.Trim() != "")
                                        data.CCheckMsg = elmData.Attribute("未通過原因").Value;

                                DateTime dt;
                                if(elmData.Attribute("檢核日期")!=null)
                                if (DateTime.TryParse(elmData.Attribute("檢核日期").Value, out dt))
                                    data.CDocUpdateDate = dt;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    SmartSchool.ErrorReporting.ErrorMessgae errMsg = new SmartSchool.ErrorReporting.ErrorMessgae(ex);
                    _ErrorList.Add("取得局端錯誤:"+ex.Message);
                }
            }

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // 檢查是否已登錄
            if (dgData.Rows.Count == 0)
                return;

            btnUpdate.Enabled = false;
            if (dgData.SelectedRows.Count == 1)
            {
                UDT_CentralData data = dgData.SelectedRows[0].Tag as UDT_CentralData;

                if (string.IsNullOrEmpty(data.DocNo))
                {
                    FISCA.Presentation.Controls.MsgBox.Show("沒有核准文號無法登錄!");
                    btnUpdate.Enabled = true;
                    return;
                }

                if (data.isUpdate)
                {
                    FISCA.Presentation.Controls.MsgBox.Show("名冊核准文號已登錄，無法再次登錄");
                    btnUpdate.Enabled = true;
                    return;
                }
                else
                { 
                    // 更新資料
                    if (FISCA.Presentation.Controls.MsgBox.Show("將核准文號登錄名冊內，並自動更新名冊內學生異動紀錄的核准文號，請問是否登錄?", "登錄名冊文號", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
                    {
                        try
                        {
                            DSXmlHelper helper = new DSXmlHelper("AuthorizeBatchRequest");
                            helper.AddElement("AuthorizeBatch");
                            helper.AddElement("AuthorizeBatch", "Field");
                            helper.AddElement("AuthorizeBatch/Field", "ADNumber", data.DocNo);
                            helper.AddElement("AuthorizeBatch/Field", "ADDate", data.CDocUpdateDate.ToShortDateString());
                            helper.AddElement("AuthorizeBatch", "Condition");
                            helper.AddElement("AuthorizeBatch/Condition", "ID", data.DocUID);
                            EditStudent.ModifyUpdateRecordBatch(new DSRequest(helper));                


                            // 取得名冊內相關相關異動紀錄
                            List<string> urIDList = new List<string>();
                            List<JHUpdateRecordRecord> urRecList = new List<JHUpdateRecordRecord>();
                            foreach (UpdateRecDoc doc in _UpdateRecDocList)
                            {
                                if (doc.ID == data.DocUID)
                                {
                                    urIDList = doc.GetURIDList();
                                }
                            }

                            if (urIDList.Count > 0)
                            {
                                urRecList = JHUpdateRecord.SelectByIDs(urIDList);
                                // 更新學生異動紀錄核准日期與文號
                                foreach (JHUpdateRecordRecord rec in urRecList)
                                {
                                    rec.ADDate = data.CDocUpdateDate.ToShortDateString();
                                    rec.ADNumber = data.DocNo;
                                }
                                // 更新學生異動紀錄
                                JHUpdateRecord.Update(urRecList);
                            }
                            // 更新異動名冊


                            // 回寫 UDT 紀錄
                            List<UDT_CentralData> updateData = new List<UDT_CentralData>();
                            updateData.Add(data);
                            data.isUpdate = true;
                            UDTTransfer.UDTCentralDataUpdate(updateData);

                            FISCA.Presentation.Controls.MsgBox.Show("更新完成");
                            LoadData();
                        }
                        catch (Exception ex)
                        {
                            SmartSchool.ErrorReporting.ErrorMessgae errMsg = new SmartSchool.ErrorReporting.ErrorMessgae(ex);
                            FISCA.Presentation.Controls.MsgBox.Show("更新過程發生錯誤：" + ex.Message);
                        }
                    }
                }
            }
            btnUpdate.Enabled = true;
        }

        /// <summary>
        /// 載入資料
        /// </summary>
        private void LoadData()
        {
            btnUpdate.Enabled = false;
            lblMsg.Text = "資料讀取中..";
            _bgWorker.RunWorkerAsync();
        }

        private void CentralDataUpdateDocForm_Load(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
