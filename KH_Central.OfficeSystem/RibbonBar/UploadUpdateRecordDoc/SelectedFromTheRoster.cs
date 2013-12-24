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

namespace KH_Central.OfficeSystem
{
    public partial class SelectedFromTheRoster : BaseForm
    {
        List<UpdateRecDoc> _UpdateRecDocList = new List<UpdateRecDoc>();
        BackgroundWorker _bgWorker = new BackgroundWorker();
        int _SchoolYear = 0;

        UpdateRecDoc _SelectUpdateRecDoc = null;

        public SelectedFromTheRoster()
        {
            InitializeComponent();           
            _bgWorker.DoWork += new DoWorkEventHandler(_bgWorker_DoWork);
            _bgWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_bgWorker_RunWorkerCompleted);           
        }

        void _bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btnSelect.Enabled = true;
            cboSchoolYear.Enabled = true;

                dgData.Rows.Clear();
            int count = 0;
            foreach (UpdateRecDoc data in _UpdateRecDocList)
            {
                int RowIdx = dgData.Rows.Add();
                dgData.Rows[RowIdx].Tag = data;
                dgData.Rows[RowIdx].Cells[colSemester.Index].Value = data.Semester;
                dgData.Rows[RowIdx].Cells[colDocName.Index].Value = data.Name;
                count++;
            }
            lblMsg.Text = "共 " + count + " 筆";
        }

        private void Run()
        {
            btnSelect.Enabled = false;
            cboSchoolYear.Enabled = false;            
            if (!_bgWorker.IsBusy)
            {   
                _bgWorker.RunWorkerAsync();
            }
        }

        void _bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            // 取得異動名冊資料
            _UpdateRecDocList = QueryData.GetUpdateRecDocListBySchoolYear(_SchoolYear);
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (dgData.SelectedRows.Count == 1)
            {
                _SelectUpdateRecDoc = dgData.SelectedRows[0].Tag as UpdateRecDoc;                
                this.DialogResult = System.Windows.Forms.DialogResult.Yes;
                this.Close();
            }

        }

        /// <summary>
        /// 取得使用者選名冊
        /// </summary>
        /// <returns></returns>
        public UpdateRecDoc GetSelectUpdateRecDoc()
        {
            return _SelectUpdateRecDoc;
        }
            

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.No;
            this.Close();
        }


        private void SelectedFromTheRoster_Load(object sender, EventArgs e)
        {
            _SchoolYear = int.Parse(K12.Data.School.DefaultSchoolYear);

            int begin = _SchoolYear - 3,end=_SchoolYear+3;

            for (int i = begin; i <= end; i++)
                cboSchoolYear.Items.Add(i);

            cboSchoolYear.Text = _SchoolYear.ToString();
            Run();
        }

        private void cboSchoolYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            int sc;
            if (int.TryParse(cboSchoolYear.Text, out sc))
            {
                _SchoolYear = sc;
                Run();
            }
            else
            {
                FISCA.Presentation.Controls.MsgBox.Show("學年度必須填入數字");
                return;
            }
        }

        private void cboSchoolYear_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                LoadData();
            }
        }
    }
}
