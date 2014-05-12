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
    public partial class ChangeRosterList : BaseForm
    {
        UpdateRecDoc _UpdateRecDoc = null;
        public ChangeRosterList()
        {
            InitializeComponent();
        }

        private void ChangeRosterList_Load(object sender, EventArgs e)
        {

        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            SelectedFromTheRoster sfr = new SelectedFromTheRoster();
            if (sfr.ShowDialog() == System.Windows.Forms.DialogResult.Yes)
            {               
                // 取得使用者選的名冊
                _UpdateRecDoc = sfr.GetSelectUpdateRecDoc();
                txtName.Text = _UpdateRecDoc.Name;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtName.Text))
                {
                    FISCA.Presentation.Controls.MsgBox.Show("請選擇名冊!");
                    return;
                }

                // 選的名冊類別
                string SelNameType = _UpdateRecDoc.GetDocType();

                // 是否上傳
                bool isUpload = false;

                // 檢查所選名冊類別與畫面上所選是否相同
                foreach (Control cr in this.Controls)
                {
                    DevComponents.DotNetBar.Controls.CheckBoxX cb = cr as DevComponents.DotNetBar.Controls.CheckBoxX;
                    if (cb != null)
                    {
                        if (cb.Checked)
                        {
                            if (cb.Tag.ToString() == SelNameType)
                                isUpload = true;
                        }
                    }
                }

                // 如果所選名冊與畫面上不相同，提示還是給予上傳名冊
                if (isUpload == false)
                {
                    if (MsgBox.Show("選擇上傳的名冊類別與畫面上所選的名冊類別不同，請問是否上傳名冊?", "選擇名冊類別不同", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.Yes)
                        isUpload = true;
                }


                // 檢查局端名冊上傳狀態
                string strVal = Utility.GetCenteralOfficeDocUploadStatus(_UpdateRecDoc.SchoolYear, _UpdateRecDoc.Semester, SelNameType);
                CheckSchoolDistrict csd = new CheckSchoolDistrict(_UpdateRecDoc,txtName.Text,strVal,_UpdateRecDoc.SchoolYear.ToString());
                DialogResult dr = csd.ShowDialog();

                this.Close();
            }
            catch (Exception ex)
            {
                SmartSchool.ErrorReporting.ErrorMessgae errMsg = new SmartSchool.ErrorReporting.ErrorMessgae(ex);
                FISCA.Presentation.Controls.MsgBox.Show("名冊讀取失敗," + ex.Message);
            }

        }

        //private void btnUploadView_Click(object sender, EventArgs e)
        //{
        //    UploadRosterView urv = new UploadRosterView();
        //    urv.ShowDialog();
        //}
    }
}
