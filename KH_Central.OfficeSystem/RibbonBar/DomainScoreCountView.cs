using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FISCA.Presentation.Controls;
using Aspose.Cells;
using System.Xml.Linq;

namespace KH_Central.OfficeSystem.RibbonBar
{
    public partial class DomainScoreCountView : BaseForm
    {
        DataTable _dtTable;
        BackgroundWorker _bgWorker;
        XElement _DataXml = null;
        List<DAO.UDT_DomainScoreCount> _UDT_DomainScoreCountList;

        public DomainScoreCountView()
        {
            InitializeComponent();
            dgData.Columns.Clear();
            dgData.Rows.Clear();
            _dtTable = new DataTable();
            _bgWorker = new BackgroundWorker();
            _bgWorker.DoWork += new DoWorkEventHandler(_bgWorker_DoWork);
            _bgWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_bgWorker_RunWorkerCompleted);
        }

        void _bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            BindDataToDG();   
        }

        private void BindDataToDG()
        {
            dgData.DataSource = _dtTable;
            // 將 DataTable Caption set DataGridView
            for (int col = 0; col < _dtTable.Columns.Count; col++)
                dgData.Columns[col].HeaderText = _dtTable.Columns[col].Caption;
        }

        void _bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            LoadUDTData();   
        }

        private void DomainScoreCountView_Load(object sender, EventArgs e)
        {
            _bgWorker.RunWorkerAsync();
        }

        /// <summary>
        /// 載入 UDT 資料
        /// </summary>
        private void LoadUDTData()
        {
            _UDT_DomainScoreCountList = (from data in DAO.UDTTransfer.UDTDomainScoreCountSelectAll() orderby data.UploadDate descending select data).ToList();

            // 將資料放入 DataTable
            _dtTable.Columns.Add("上傳日期");
            _dtTable.Columns.Add("上傳狀態");
            _dtTable.Columns.Add("學年度");
            _dtTable.Columns.Add("學期");
            _dtTable.Columns.Add("學生人數");

            foreach (DAO.UDT_DomainScoreCount data in _UDT_DomainScoreCountList)
            {
                DataRow dr = _dtTable.NewRow();
                dr["學年度"] = data.SchoolYear;
                dr["學期"] = data.Semester;
                
                XElement elmRoot = XElement.Parse(data.Data);
                if (elmRoot != null)
                {
                    foreach (XElement elm in elmRoot.Elements("人數比率"))
                    {
                        if (elm.Attribute("年級").Value == "全")
                        {
                            dr["學生人數"] = elm.Attribute("學生人數").Value;
                            foreach (XElement elms1 in elm.Elements("領域"))
                            {
                                string k1 = elms1.Attribute("名稱").Value + "人數";
                                string k2 = elms1.Attribute("名稱").Value + "比率"+"%";

                                if (!_dtTable.Columns.Contains(k1))
                                    _dtTable.Columns.Add(k1);

                                if (!_dtTable.Columns.Contains(k2))
                                    _dtTable.Columns.Add(k2);

                                dr[k1] = elms1.Attribute("未達人數").Value;
                                dr[k2] = elms1.Attribute("未達比率").Value;
                            }
                        }
                    }
                }

                dr["上傳日期"] = data.UploadDate.ToString();
                dr["上傳狀態"] = data.Status;

                _dtTable.Rows.Add(dr);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            ExportData();
        }

        /// <summary>
        /// 匯出至 Excel
        /// </summary>
        private void ExportData()
        {
            Workbook wb = new Workbook();
            string name = "檢視學期成績未達及格人數比率";
            wb.Worksheets[0].Name = name;
            wb.Worksheets[0].Cells.ImportDataTable(_dtTable, true, "A1");
            Utility.CompletedXls(name, wb);
        }
    }
}
