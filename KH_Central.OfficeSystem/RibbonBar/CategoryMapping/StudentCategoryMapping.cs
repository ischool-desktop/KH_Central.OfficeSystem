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
    public partial class StudentCategoryMapping : BaseForm
    {
        BackgroundWorker _bgWorker = new BackgroundWorker();
        List<UDT_StudentCategoryMapping> _StudentCategoryMapping;
        List<string> _CentCategoryList;
        List<string> _StudCategoryList;
        public StudentCategoryMapping()
        {
            InitializeComponent();
            _StudCategoryList = new List<string>();
            _CentCategoryList = new List<string>();
            _StudentCategoryMapping = new List<UDT_StudentCategoryMapping>();
            _bgWorker.DoWork += new DoWorkEventHandler(_bgWorker_DoWork);
            _bgWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_bgWorker_RunWorkerCompleted);
        }

        void _bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            cboCentCategory.Items.AddRange(_CentCategoryList.ToArray());
            cboStudCategory.Items.AddRange(_StudCategoryList.ToArray());
            dgData.Rows.Clear();
            int count = 0;
            foreach (UDT_StudentCategoryMapping data in _StudentCategoryMapping)
            {
                int RowIdx = dgData.Rows.Add();
                dgData.Rows[RowIdx].Tag = data;
                dgData.Rows[RowIdx].Cells[cboCentCategory.Index].Value = data.CentralCategory;
                dgData.Rows[RowIdx].Cells[cboStudCategory.Index].Value = data.StudentCategory;
                count++;
            }
            lblMsg.Text = "共 " + count + " 筆";

        }

        void _bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            // 加入局端類別名稱
            _CentCategoryList.Add("父外籍");
            _CentCategoryList.Add("母外籍");
            _CentCategoryList.Add("平地原住民");
            _CentCategoryList.Add("山地原住民");
            _CentCategoryList.Add("特教");
            _CentCategoryList.Add("慈輝");
            _CentCategoryList.Add("資源");
            _CentCategoryList.Add("資源(巡迴輔導)");
            _CentCategoryList.Add("資源(床邊教學)");
            _CentCategoryList.Add("資源(啟聰)");
            _CentCategoryList.Add("資源(情障巡迴)");
            _CentCategoryList.Add("資優資源");
            _CentCategoryList.Add("資優資源(美術)");
            _CentCategoryList.Add("雙語(資源)");
            _CentCategoryList.Add("藝才美術");
            _CentCategoryList.Add("藝才音樂");
            _CentCategoryList.Add("藝才舞蹈");
            _CentCategoryList.Add("體育");
            _CentCategoryList.Add("體育(資源)");
            _CentCategoryList.Add("實驗學校學生");
            

            // 取得學生類別類別名稱
            _StudCategoryList = QueryData.GetStudentCategoryNameList();
            // 取得對照資料
            _StudentCategoryMapping = UDTTransfer.UDTStudentCategoryMappingSelectAll();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void StudentCategoryMapping_Load(object sender, EventArgs e)
        {
            cboCentCategory.Items.Clear();
            cboStudCategory.Items.Clear();
            cboCentCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            cboStudCategory.DropDownStyle = ComboBoxStyle.DropDownList;
          
            
            _bgWorker.RunWorkerAsync();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                bool pass = true;

                // 檢查資料是否空值
                foreach (DataGridViewRow dr in dgData.Rows)
                {
                    if (dr.IsNewRow)
                        continue;
                    dr.ErrorText = "";
                    foreach (DataGridViewCell cell in dr.Cells)
                    {
                        
                        if (cell.Value == null)
                        {
                            dr.ErrorText= "不能有空值!";
                            
                            pass = false;
                        }
                    }
                }

                if (pass)
                {

                    List<UDT_StudentCategoryMapping> insertList = new List<UDT_StudentCategoryMapping>();
                    List<UDT_StudentCategoryMapping> updateList = new List<UDT_StudentCategoryMapping>();
                    List<UDT_StudentCategoryMapping> delList = new List<UDT_StudentCategoryMapping>();

                    List<string> hasUID = new List<string>();

                    foreach (DataGridViewRow drv in dgData.Rows)
                    {
                        if (drv.IsNewRow)
                            continue;

                        UDT_StudentCategoryMapping data = drv.Tag as UDT_StudentCategoryMapping;
                        if (data == null)
                            data = new UDT_StudentCategoryMapping();

                        data.CentralCategory = drv.Cells[cboCentCategory.Index].Value.ToString();
                        data.StudentCategory = drv.Cells[cboStudCategory.Index].Value.ToString();

                        if (string.IsNullOrEmpty(data.UID))
                            insertList.Add(data);
                        else
                        {
                            hasUID.Add(data.UID);
                            updateList.Add(data);
                        }
                    }

                    // 刪除
                    foreach (UDT_StudentCategoryMapping data in _StudentCategoryMapping)
                    {
                        if (!hasUID.Contains(data.UID))
                            delList.Add(data);
                    }

                    if (delList.Count > 0)
                        UDTTransfer.UDTStudentCategoryMappingDelete(delList);

                    if (insertList.Count > 0)
                        UDTTransfer.UDTStudentCategoryMappingInsert(insertList);

                    if (updateList.Count > 0)
                        UDTTransfer.UDTStudentCategoryMappingUpdate(updateList);

                    FISCA.Presentation.Controls.MsgBox.Show("儲存成功");
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                FISCA.Presentation.Controls.MsgBox.Show("儲存失敗,"+ex.Message);
            }
        }

        private void dgData_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            dgData.EndEdit();
            if (dgData.CurrentCell.Value != null)
                dgData.CurrentRow.ErrorText = "";

            dgData.BeginEdit(false);
        }

        private void getCentCategory_Click(object sender, EventArgs e)
        {

        }  
    }
}
