using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using KH_Central.OfficeSystem.DAO;

namespace KH_Central.OfficeSystem
{
    public partial class SchoolDistrict : FISCA.Presentation.Controls.BaseForm
    {
        BackgroundWorker _bgWorkerLoadUDT;
        BackgroundWorker _bgWorkerLoadCentral;
        List<AddressRec> _AddressRecList;
        List<AddressRec> _AddressRecGroup;
        List<UDT_CentralAddress> _UDT_CentralAddressList;
        int _DataCount = 0;
        public SchoolDistrict()
        {
            InitializeComponent();
            _AddressRecList = new List<AddressRec>();
            _AddressRecGroup = new List<AddressRec>();
            _bgWorkerLoadUDT = new BackgroundWorker();
            _bgWorkerLoadCentral = new BackgroundWorker();
            _UDT_CentralAddressList = new List<UDT_CentralAddress>();
            _bgWorkerLoadUDT.DoWork += new DoWorkEventHandler(_bgWorkerLoadUDT_DoWork);
            _bgWorkerLoadUDT.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_bgWorkerLoadUDT_RunWorkerCompleted);
            _bgWorkerLoadCentral.DoWork += new DoWorkEventHandler(_bgWorkerLoadCentral_DoWork);
            _bgWorkerLoadCentral.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_bgWorkerLoadCentral_RunWorkerCompleted);
            lblMsg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
        }

        void _bgWorkerLoadCentral_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            _bgWorkerLoadUDT.RunWorkerAsync();
        }

        void _bgWorkerLoadCentral_DoWork(object sender, DoWorkEventArgs e)
        {
            // 取得局端資料，並轉成UDT record
            _UDT_CentralAddressList = Utility.GetCentralAddress(K12.Data.School.DefaultSchoolYear);
   
            // 取得目前UDT 內局端，並刪除資料
            List<UDT_CentralAddress> delList = UDTTransfer.UDTCentralAddressSelectAll();
            UDTTransfer.UDTCentralAddressDelete(delList);

            // 新增最新資料到UDT
            if (_UDT_CentralAddressList.Count > 0)
                UDTTransfer.UDTCentralAddressInsert(_UDT_CentralAddressList);
        }

        void _bgWorkerLoadUDT_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btnGetCertData.Enabled = true;
            dgData.Rows.Clear();
            foreach (AddressRec rec in _AddressRecGroup)
            {
                int RowIdx = dgData.Rows.Add();
                dgData.Rows[RowIdx].Cells[colTown.Index].Value = rec.Town;
                dgData.Rows[RowIdx].Cells[colDistrict.Index].Value = rec.District;
                dgData.Rows[RowIdx].Cells[colArea.Index].Value = rec.Area;
            }
            lblMsg.Text = "區里資料總共： " + dgData.Rows.Count + " 筆,區里鄰資料總共： " + _DataCount + " 筆";
            
        }

        void _bgWorkerLoadUDT_DoWork(object sender, DoWorkEventArgs e)
        {
            // 取得 UDT 內資料
            List<UDT_CentralAddress> dataList = UDTTransfer.UDTCentralAddressSelectAll();
            _DataCount = dataList.Count;
            _AddressRecList = Utility.GetAddressRecList(dataList);
            _AddressRecGroup = Utility.AddressRecParse1(_AddressRecList);            
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }        

        private void SchoolDistrict_Load(object sender, EventArgs e)
        {
            _bgWorkerLoadUDT.RunWorkerAsync();
        }

        private void btnGetCertData_Click(object sender, EventArgs e)
        {
            btnGetCertData.Enabled = false;
            _bgWorkerLoadCentral.RunWorkerAsync();

        }
    }
}
