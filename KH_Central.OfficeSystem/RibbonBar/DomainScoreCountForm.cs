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
using K12.Data;
using JHSchool.Data;
using System.Net;
using System.IO;


namespace KH_Central.OfficeSystem.RibbonBar
{
    public partial class DomainScoreCountForm : BaseForm
    {
        private DataTable _dtTable;
        BackgroundWorker _bgWorker;
        BackgroundWorker _bgWorkerLoadData;
        // 一般狀態學生 年級人數
        private Dictionary<string, List<string>> _GradeStudIDDict;
        private List<string> _StudentIDList;
        int _SchoolYear;
        int _Semester;
        private Dictionary<string, decimal> _DomainCot;
        private Dictionary<string, decimal> _DomainRatio;
        private Dictionary<string, decimal> _DomainStudCot;
        decimal _PassScore = 60;
        XElement _DataXml = null;
        List<DAO.UDT_DomainScoreCount> _UDT_DomainScoreCountList;

        public DomainScoreCountForm()
        {
            InitializeComponent();
            _DomainCot = new Dictionary<string, decimal>();
            _DomainRatio = new Dictionary<string, decimal>();
            _DomainStudCot = new Dictionary<string, decimal>();
            _UDT_DomainScoreCountList = new List<DAO.UDT_DomainScoreCount>();
            _SchoolYear = int.Parse(School.DefaultSchoolYear);
            _Semester = int.Parse(School.DefaultSemester);
            _dtTable = new DataTable();            
            _GradeStudIDDict = new Dictionary<string, List<string>>();
            _StudentIDList = new List<string>();
            _bgWorkerLoadData = new BackgroundWorker();
            _bgWorkerLoadData.DoWork += new DoWorkEventHandler(_bgWorkerLoadData_DoWork);
            _bgWorkerLoadData.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_bgWorkerLoadData_RunWorkerCompleted);
            _bgWorker = new BackgroundWorker();
            _bgWorker.DoWork += new DoWorkEventHandler(_bgWorker_DoWork);
            _bgWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_bgWorker_RunWorkerCompleted);
        }

        void _bgWorkerLoadData_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // 檢查UDT內是否已經有資料
            DAO.UDT_DomainScoreCount da = null;
            foreach (DAO.UDT_DomainScoreCount data in _UDT_DomainScoreCountList)
            {
                if (data.SchoolYear == _SchoolYear && data.Semester == _Semester)
                {
                    da = data;
                    break;
                }
            }

            if (da != null)
            {
                _dtTable = Utility.ConvertDomainScoreCountXMLToDT(da.Data);
                dgData.DataSource = _dtTable;
            }

            btnUpload.Enabled = true;
        }

        void _bgWorkerLoadData_DoWork(object sender, DoWorkEventArgs e)
        {
            LoadUDTData();
        }

        void _bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btnGetData.Enabled = true;
            BindDataToDG();
        }

        void _bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            _dtTable.Clear();
            _dtTable.Columns.Clear();
            RelaodData();            
        }

        /// <summary>
        /// 載入 UDT 資料
        /// </summary>
        private void LoadUDTData()
        {
            _UDT_DomainScoreCountList = DAO.UDTTransfer.UDTDomainScoreCountSelectAll();
        }

        // 重新載入系統
        private void RelaodData()
        {
            _GradeStudIDDict.Clear();
            _StudentIDList.Clear();
        
            
            // 取得各年級學生狀態一般的學生ID
            _GradeStudIDDict = DAO.QueryData.GetGradeStudentDict1();
            
            // 所有一般學生 ID
            foreach (List<string> idList in _GradeStudIDDict.Values)
                _StudentIDList.AddRange(idList);

            _DomainCot.Clear();
            _DomainRatio.Clear();
            _DomainStudCot.Clear();

            // 取得學生學期成績
            List<JHSemesterScoreRecord> studSemsScoreList = JHSemesterScore.SelectBySchoolYearAndSemester(_StudentIDList, _SchoolYear, _Semester);

            // 收集成績內領域名稱
            List<string> DomainNameList = new List<string>();
            List<string> tmpList = new List<string>();
            List<string> srtList = new List<string>();
            srtList.Add("國語文");
            srtList.Add("英語");
            srtList.Add("數學");
            srtList.Add("社會");
            srtList.Add("自然與生活科技");
            srtList.Add("健康與體育");
            srtList.Add("藝術與人文");
            srtList.Add("綜合活動");

            foreach (JHSemesterScoreRecord rec in studSemsScoreList)
            {
                foreach (string str in rec.Domains.Keys)
                    if (!tmpList.Contains(str))
                        tmpList.Add(str);            
            }

            tmpList.Sort();

            foreach (string idx in srtList)
            {
                if (tmpList.Contains(idx))
                    DomainNameList.Add(idx);            
            }

            foreach (string idx in tmpList)
            {
                if (!DomainNameList.Contains(idx))
                    DomainNameList.Add(idx);
            }


            // 處理年級人數與比率
            string keyNG = "未分年級", keyAG = "全年級";

            foreach (string str in _GradeStudIDDict.Keys)
            {
                if (str == "999")
                {                   
                    foreach (string dStr in DomainNameList)
                    {
                        _DomainCot.Add(keyNG + dStr, 0);
                        _DomainRatio.Add(keyNG + dStr, 0);
                        _DomainStudCot.Add(keyNG + dStr, _GradeStudIDDict[str].Count);
                    }
                    _DomainStudCot.Add(keyNG, _GradeStudIDDict[str].Count);
                }
                else
                {
                    string keyi = str + "年級";
                    foreach (string dStr in DomainNameList)
                    {
                        _DomainCot.Add(keyi+dStr, 0);
                        _DomainRatio.Add(keyi+dStr, 0);
                        _DomainStudCot.Add(keyi+dStr, _GradeStudIDDict[str].Count);
                    }
                    _DomainStudCot.Add(keyi, _GradeStudIDDict[str].Count);
                }
            }

            _DomainStudCot.Add(keyAG, _StudentIDList.Count);

            foreach (string dStr in DomainNameList)
            {
                _DomainCot.Add(keyAG + dStr, 0);
                _DomainRatio.Add(keyAG + dStr, 0);
                _DomainStudCot.Add(keyAG + dStr, _StudentIDList.Count);
            }

            // 建立學生年級索引
            Dictionary<string, string> StudGradDict = new Dictionary<string, string>();
            foreach (string key in _GradeStudIDDict.Keys)
            {
                string strVal = key + "年級";
                if (key == "999")
                    strVal = "未分年級";

                foreach (string value in _GradeStudIDDict[key])
                    StudGradDict.Add(value, strVal);  
            }
           
            foreach (JHSemesterScoreRecord SemsRec in studSemsScoreList)
            {
                string keyGrade = "";
                if (StudGradDict.ContainsKey(SemsRec.RefStudentID))
                    keyGrade = StudGradDict[SemsRec.RefStudentID];

                foreach (DomainScore ds in SemsRec.Domains.Values)
                {
                    if (ds.Score.HasValue)
                    {
                        if (ds.Score.Value < _PassScore)
                        { 
                            // 統計人數                           
                            // 各年級
                            string keyE = keyGrade + ds.Domain;
                            if(_DomainCot.ContainsKey(keyE))
                                _DomainCot[keyE]++;

                            // 全年級
                            string keyIAG = keyAG + ds.Domain;
                            if(_DomainCot.ContainsKey(keyIAG))
                                _DomainCot[keyIAG]++;
                        }
                    }                
                }                
            }

            // 計算比率，小數下一位四捨五入
            foreach (string key in _DomainStudCot.Keys)
                if(_DomainCot.ContainsKey(key) && _DomainStudCot.ContainsKey(key))
                if (_DomainCot[key] > 0 && _DomainStudCot[key] > 0)
                    _DomainRatio[key] = Math.Round((_DomainCot[key] / _DomainStudCot[key])*100, 1, MidpointRounding.AwayFromZero);

            // 將資料放入 DataTable
            _dtTable.Columns.Add("年級");
            _dtTable.Columns.Add("學生人數");
            foreach (string colName in DomainNameList)
            { 
                _dtTable.Columns.Add(colName+"人數");
                string ccName = colName + "比率";
                _dtTable.Columns.Add(ccName);
                _dtTable.Columns[ccName].Caption = ccName + "%";                
            }

            // 填值
            List<string> row1NameList = new List<string>();
            foreach (string strGr in _GradeStudIDDict.Keys)
            {
                if (strGr == "999")
                    continue;

                row1NameList.Add(strGr + "年級");
            }
            row1NameList.Add("未分年級");
            row1NameList.Add("全年級");

            foreach (string strRow1 in row1NameList)
            {
                DataRow dr = _dtTable.NewRow();
                dr["年級"] = strRow1.Replace("年級", ""); ;
                foreach (string dName in DomainNameList)
                {
                    string key = strRow1 + dName;
                    string val1 = dName+"人數";
                    string val2 = dName + "比率";
                    if (_DomainCot.ContainsKey(key))
                        dr[val1] = _DomainCot[key];

                    if (_DomainRatio.ContainsKey(key))
                        dr[val2] = _DomainRatio[key];                
                }
                if (_DomainStudCot.ContainsKey(strRow1))
                    dr["學生人數"] = _DomainStudCot[strRow1];

                _dtTable.Rows.Add(dr);
            }

        

            _DataXml = Utility.ConvertDomainScoreCountDTtoXML(_dtTable, _SchoolYear, _Semester, School.Code, School.ChineseName, _PassScore, DomainNameList);
        }

        private void BindDataToDG()
        {
            dgData.DataSource = _dtTable;
            // 將 DataTable Caption set DataGridView
            for (int col = 0; col < _dtTable.Columns.Count; col++)
                dgData.Columns[col].HeaderText = _dtTable.Columns[col].Caption;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            btnUpload.Enabled = false;
            SaveUDT();

            // 上傳資料
            try
            {
                if (_DataXml == null)
                    _DataXml = new XElement("Null");

                HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create("http://163.32.129.9/cc/ssup.jsp");
                req.Method = "POST";
                StringBuilder sb = new StringBuilder();
                req.Accept = "*/*";
                sb.Append("schno=" + K12.Data.School.Code);
                sb.Append("&user=admin");
                sb.Append("&content=" + _DataXml.ToString());
                req.ContentType = "application/x-www-form-urlencoded";

                byte[] byteArray = Encoding.UTF8.GetBytes(sb.ToString());
                req.ContentLength = byteArray.Length;
                Stream dataStream = req.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();

                HttpWebResponse rsp;
                rsp = (HttpWebResponse)req.GetResponse();
                //= req.GetResponse();
                dataStream = rsp.GetResponseStream();

                Console.WriteLine(((HttpWebResponse)rsp).StatusDescription);
                StreamReader reader = new StreamReader(dataStream);
                // Read the content.
                string responseFromServer = reader.ReadToEnd();
                // Display the content.
                Console.WriteLine(responseFromServer);
                // Clean up the streams.
                reader.Close();
                dataStream.Close();
                rsp.Close();
            }
            catch (Exception ex)
            {
                FISCA.Presentation.Controls.MsgBox.Show("上傳錯誤:" + ex.Message);            
            }

            //_bgWorkerLoadData.RunWorkerAsync();
            FISCA.Presentation.Controls.MsgBox.Show("上傳完成");
            btnUpload.Enabled = true;
        }

        private void SaveUDT()
        {
            bool isInset = false;
            DAO.UDT_DomainScoreCount data = null;

            foreach (DAO.UDT_DomainScoreCount da in _UDT_DomainScoreCountList)
            {
                if (da.SchoolYear == _SchoolYear && da.Semester == _Semester)
                    data = da;
            }

            if (data == null)
            {
                isInset = true;
                data = new DAO.UDT_DomainScoreCount();
            }
            data.SchoolYear = _SchoolYear;
            data.Semester = _Semester;
            data.Data = _DataXml.ToString();
            data.UploadDate = DateTime.Now;
            List<DAO.UDT_DomainScoreCount> daList = new List<DAO.UDT_DomainScoreCount>();

            if (isInset)
            {
                
                daList.Add(data);
                DAO.UDTTransfer.UDTDomainScoreCountInsert(daList);
            }
            else
            {
                daList.Add(data);
                DAO.UDTTransfer.UDTDomainScoreCountUpdate(daList);
            }
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
            string name = "學期成績未達及格人數比率";
            wb.Worksheets[0].Name = name;
            wb.Worksheets[0].Cells.ImportDataTable(_dtTable, true, "A1");
            Utility.CompletedXls(name, wb);
        }

        private void btnGetData_Click(object sender, EventArgs e)
        {
            btnGetData.Enabled = false;
            _SchoolYear = iptSchoolYear.Value;
            _Semester = iptSemester.Value;
            dgData.DataSource = null;
            _bgWorker.RunWorkerAsync();
        }

        private void DomainScoreCountForm_Load(object sender, EventArgs e)
        {
            iptSchoolYear.Value = _SchoolYear;
            iptSemester.Value = _Semester;
            dgData.Columns.Clear();
            //// 載入 UDT 內資料
            //_bgWorkerLoadData.RunWorkerAsync();
        }

      
        private void GetUDTSelectUDTData()
        {
            dgData.DataSource = null;
            DAO.UDT_DomainScoreCount data = null;
            foreach (DAO.UDT_DomainScoreCount da in _UDT_DomainScoreCountList)
            {
                if (da.SchoolYear == _SchoolYear && da.Semester == _Semester)
                    data = da;
            }

            if (data != null)
            {
                _dtTable = Utility.ConvertDomainScoreCountXMLToDT(data.Data);
                BindDataToDG();
            }
        }

    }
}
