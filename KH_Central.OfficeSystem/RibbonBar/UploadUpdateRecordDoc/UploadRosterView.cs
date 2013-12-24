using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FISCA.Presentation.Controls;
using KH_Central.OfficeSystem.DAO;
using System.Xml.Linq;

namespace KH_Central.OfficeSystem
{
    public partial class UploadRosterView : BaseForm
    {
        List<UDT_UpdateRecDocInfo> _UpdateRecDocInfoList = new List<UDT_UpdateRecDocInfo>();
        BackgroundWorker _bgWorker = new BackgroundWorker();
        public UploadRosterView()
        {
            InitializeComponent();
            _bgWorker.DoWork += new DoWorkEventHandler(_bgWorker_DoWork);
            _bgWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_bgWorker_RunWorkerCompleted);
        }

        void _bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            dgData.Rows.Clear();
            int count = 0;
            foreach (UDT_UpdateRecDocInfo data in _UpdateRecDocInfoList)
            {
                int RowIdx = dgData.Rows.Add();
                dgData.Rows[RowIdx].Cells[colSchoolYear.Index].Value = data.SchoolYear;
                dgData.Rows[RowIdx].Cells[colSemester.Index].Value = data.Semester;
                dgData.Rows[RowIdx].Cells[colName.Index].Value = data.Name;
                dgData.Rows[RowIdx].Cells[colType.Index].Value = data.Type;
                dgData.Rows[RowIdx].Cells[colUploadDate.Index].Value = data.UploadDate.ToString();
                dgData.Rows[RowIdx].Cells[colCerMemo.Index].Value = data.CentralMemo;
                dgData.Rows[RowIdx].Cells[colChkMsg.Index].Value = data.CentralMsg;
                count++;
            }

            lblMsg.Text = "共 " + count + " 筆";
        }

        void _bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            // 依日期排序
            _UpdateRecDocInfoList = (from data in UDTTransfer.UDTUpdateRecDocInfoSelectAll() orderby data.UploadDate descending select data).ToList(); ;

            try
            {
                // 比對資料取局端檢核
                foreach (UDT_UpdateRecDocInfo data in _UpdateRecDocInfoList)
                {
                    XElement elm = DAO.QueryData.GetCentralData(K12.Data.School.Code, data.SchoolYear, data.Semester, data.Type, data.Name);
                    if(elm !=null)
                        if (elm.Attribute("名冊名稱") != null)
                        {
                            if(elm.Attribute("檢核狀態") !=null)
                                data.CentralMemo = elm.Attribute("檢核狀態").Value;
                            if(elm.Attribute("未通過原因") !=null)
                                data.CentralMsg = elm.Attribute("未通過原因").Value;
                        }
                }
                
            }
            catch (Exception ex)
            {
                SmartSchool.ErrorReporting.ErrorMessgae errMsg = new SmartSchool.ErrorReporting.ErrorMessgae(ex);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void UploadRosterView_Load(object sender, EventArgs e)
        {
            _bgWorker.RunWorkerAsync();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            btnClear.Enabled = false;
            if (FISCA.Presentation.Controls.MsgBox.Show("請問是否清空所有紀錄", "清空紀錄", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.Yes)
            {
                UDTTransfer.UDTUpdateRecDocInfoDelete(_UpdateRecDocInfoList);
                FISCA.Presentation.Controls.MsgBox.Show("已清空所有紀錄");
                dgData.Rows.Clear();
            }
            btnClear.Enabled = true;
        }
    }
}
