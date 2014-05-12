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
using System.Net;
using System.IO;

namespace KH_Central.OfficeSystem
{
    public partial class CheckSchoolDistrict : BaseForm
    {
        UpdateRecDoc _UpdateRecDoc = null;
        BackgroundWorker _bgWorker;
        List<string> _AddressKeyList;
        string _Name = "";
        int _RecCount = 0;
        string _uploadMessage = "";
        Dictionary<string, UDT_UploadMemo> _hasUDT_UploadMemo;
        string _SchoolYear;

        public CheckSchoolDistrict(UpdateRecDoc uDoc,string DocName,string UploadMessage,string strSchoolYear)
        {
            InitializeComponent();
            _uploadMessage = UploadMessage;
            _SchoolYear = strSchoolYear;
            btnStartUpdata.Enabled = false;
            _AddressKeyList = new List<string>();
            _hasUDT_UploadMemo = new Dictionary<string, UDT_UploadMemo>();
            _UpdateRecDoc = uDoc;
            _Name = DocName;

            btnStartUpdata.Enabled = false;

       
            _bgWorker = new BackgroundWorker();
            _bgWorker.DoWork += new DoWorkEventHandler(_bgWorker_DoWork);
            _bgWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_bgWorker_RunWorkerCompleted);
            _bgWorker.RunWorkerAsync();
        }

        void _bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btnStartUpdata.Enabled = true;

            // 已上傳已通過無法上傳,
            if (_uploadMessage == "" || _uploadMessage.Contains("已上傳已通過")||_uploadMessage.Contains("已上傳已審核"))
            {
                btnStartUpdata.Enabled = false;
            }

            lblUploadMsg.Text = "上傳狀態： "+_uploadMessage;
            dgData.Rows.Clear();
            int errorCount = 0;
            _RecCount = 0;
            // 取得名冊，名稱與統計男女人數
            if (_UpdateRecDoc != null)
            {
                if (_UpdateRecDoc.Data != null)
                {
                    // 是否需要檢查學區
                    bool ChkAreaData = false;

                    int gB = 0, gG = 0, gT = 0;
                    string Name = "";
                    if (_UpdateRecDoc.Data.Attribute("類別") != null)
                    {
                        Name = _UpdateRecDoc.Data.Attribute("類別").Value;

                        if (Name.Contains("新生") || Name.Contains("轉入"))
                            ChkAreaData = true;
                    }
                  
                    foreach (XElement elm in _UpdateRecDoc.Data.Elements("清單"))
                    {
                       
                        string sGradeYear = Utility.GetXMLAttributeStr(elm, "年級");
                        string sDeptName = Utility.GetXMLAttributeStr(elm, "科別");

                        foreach (XElement elmE in elm.Elements("異動紀錄"))
                        {
                            _RecCount++;
                            int RowIdx = dgData.Rows.Add();
                            string key = Utility.GetXMLAttributeStr(elmE, "編號");
                            dgData.Rows[RowIdx].Tag = key;
                            // 學號
                            dgData.Rows[RowIdx].Cells[colStudentNumber.Index].Value = Utility.GetXMLAttributeStr(elmE, "學號");

                            // 異動備註 
                            dgData.Rows[RowIdx].Cells[colURDef.Index].Value = Utility.GetXMLAttributeStr(elmE, "備註");

                            // 班級
                            dgData.Rows[RowIdx].Cells[colClassName.Index].Value = Utility.GetXMLAttributeStr(elmE, "班別");
                            // 年級
                            dgData.Rows[RowIdx].Cells[colGradeYear.Index].Value = sGradeYear;
                            // 性別
                            string sGender = Utility.GetXMLAttributeStr(elmE, "性別");
                            gT++;
                            if (sGender == "男")
                                gB++;

                            if (sGender == "女")
                                gG++;

                            dgData.Rows[RowIdx].Cells[colGender.Index].Value = sGender;

                            // 姓名
                            dgData.Rows[RowIdx].Cells[colName.Index].Value = Utility.GetXMLAttributeStr(elmE, "姓名");
                            

                            // 身分證號
                            dgData.Rows[RowIdx].Cells[colIDNumber.Index].Value = Utility.GetXMLAttributeStr(elmE, "身分證號");

                            // 生日
                            dgData.Rows[RowIdx].Cells[colBirthday.Index].Value = Utility.GetXMLAttributeStr(elmE, "出生年月日");

                            // 學區不符
                            // 說明
                            // 地址
                            string strAddress = Utility.GetXMLAttributeStr(elmE, "地址");
                            dgData.Rows[RowIdx].Cells[colAddress.Index].Value = strAddress;
                            bool pass = false;
                            foreach (string str in _AddressKeyList)
                            {
                                if (strAddress.Contains(str))
                                {
                                    pass = true;
                                    break;
                                }
                            }
                            // 當學區不符合並且需要檢查學區才標示
                            if (pass == false && ChkAreaData==true)
                            {
                                foreach (DataGridViewCell cell in dgData.Rows[RowIdx].Cells)
                                    cell.Style.BackColor = Color.Red;

            

                                dgData.Rows[RowIdx].Cells[colDef.Index].Value = "學區不符合";
                                errorCount++;
                            }

                            dgData.Rows[RowIdx].Cells[colNote.Index].Value = string.Empty;

                            // 已有說明
                            if (_hasUDT_UploadMemo.ContainsKey(key))
                            {
                                dgData.Rows[RowIdx].Cells[colNote.Index].Value = _hasUDT_UploadMemo[key].Memo;
                            }
                        }
                    }


                    lblMsg.Text = "名冊名稱：" + Name + ",   男生： " + gB + " 人,女生： " + gG + " 人,共 " + gT + " 人";
                    if(errorCount>0)
                        lblMsg.Text += " ， 學區驗證結果：有 "+errorCount+" 筆資料不在學區內,請填寫說明。";
                }
            }
        }

        void _bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            // 修改成及時讀取學區資料
            List<UDT_CentralAddress> dataList = Utility.GetCentralAddress(_SchoolYear); //UDTTransfer.UDTCentralAddressSelectAll();
            foreach (AddressRec data in Utility.GetAddressRecList(dataList))
                _AddressKeyList.Add(data.GetPKey ());

            // 取得已有上傳說明
            List<UDT_UploadMemo> umList = UDTTransfer.UDTUploadMemoSelectByName(_UpdateRecDoc.Name);

            _hasUDT_UploadMemo.Clear();
            foreach (UDT_UploadMemo da in umList)
            {
                if (!_hasUDT_UploadMemo.ContainsKey(da.Key))
                    _hasUDT_UploadMemo.Add(da.Key, da);
            }

        }

        private void btnStartUpdata_Click(object sender, EventArgs e)
        {
            try
            {
                // 檢查上傳資料
                bool pass = true;
                foreach (DataGridViewRow dr in dgData.Rows)
                { 
                    // 備註有值，說明必須填值
                    if (dr.Cells[colDef.Index].Value != null)
                    {
                        if (dr.Cells[colDef.Index].Value.ToString().Length > 1)
                        {
                            if (dr.Cells[colNote.Index].Value == null)
                            {
                                pass = false;
                            }
                            else
                            { 
                                if(string.IsNullOrEmpty(dr.Cells[colNote.Index].Value.ToString()))
                                    pass=false;
                            }
                        }                    
                    }                
                }

                if(pass ==false)
                {
                    if (FISCA.Presentation.Controls.MsgBox.Show("[學區不符合]未填寫說明，確認是否繼續上傳資料?", "學區不符合", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.No)
                    {
                        return;
                    }
                    
                }

                // 儲存上傳資料
                int scY, scS;
                UDT_UpdateRecDocInfo data = new UDT_UpdateRecDocInfo();
                int.TryParse(Utility.GetXMLAttributeStr(_UpdateRecDoc.Data, "學年度"), out scY);
                int.TryParse(Utility.GetXMLAttributeStr(_UpdateRecDoc.Data, "學期"), out scS);
                data.SchoolYear = scY;
                data.Semester = scS;
                data.Type = Utility.GetXMLAttributeStr(_UpdateRecDoc.Data, "類別");
                data.Name =_Name;
                data.UploadDate = DateTime.Now;

                List<UDT_UploadMemo> uDataList = new List<UDT_UploadMemo>();
                List<UDT_UploadMemo> iDataList = new List<UDT_UploadMemo>();

                // 記錄不符合說明
                foreach (DataGridViewRow dr in dgData.Rows)
                {
                    if (dr.Tag != null)
                    {
                        string key = dr.Tag.ToString();
                        UDT_UploadMemo dataMemo = null;
                        if (key != "")
                        {
                            if (_hasUDT_UploadMemo.ContainsKey(key))
                            {
                                dataMemo = _hasUDT_UploadMemo[key];
                                dataMemo.Memo = dr.Cells[colNote.Index].Value.ToString();
                                uDataList.Add(dataMemo);
                            }
                        }

                        if (dataMemo == null)
                        {
                            dataMemo = new UDT_UploadMemo();
                            dataMemo.DocName = _UpdateRecDoc.Name;
                            dataMemo.Key = key;
                            dataMemo.Memo = dr.Cells[colNote.Index].Value.ToString();
                            iDataList.Add(dataMemo);
                        }
                    }
                }

                // 寫入名冊名稱
                _UpdateRecDoc.Data.SetAttributeValue("名稱", _Name);
                // 名冊 XML 傳送前加入資料驗證備註，資料驗證說明
                foreach (XElement elm in _UpdateRecDoc.Data.Elements("清單"))
                {
                    foreach (XElement elmE in elm.Elements("異動紀錄"))
                    {
                        string id = Utility.GetXMLAttributeStr(elmE, "編號");
                        if (id != "")
                        {
                            foreach (DataGridViewRow drv in dgData.Rows)
                            {
                                if (drv.IsNewRow)
                                    continue;
                                if (drv.Tag != null)
                                {
                                    string tid = drv.Tag.ToString();
                                    if (id == tid)
                                    {
                                        if (drv.Cells[colDef.Index].Value == null)
                                            elmE.SetAttributeValue("資料驗證備註", "");
                                        else
                                            elmE.SetAttributeValue("資料驗證備註", drv.Cells[colDef.Index].Value.ToString());

                                        if (drv.Cells[colNote.Index].Value == null)
                                            elmE.SetAttributeValue("資料驗證說明", "");
                                        else
                                            elmE.SetAttributeValue("資料驗證說明", drv.Cells[colNote.Index].Value.ToString());
                                        break;
                                    }
                                }                            
                            }
                        }
                    }
                }


                HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create("http://163.32.129.9/cc/asc.jsp");
                req.Method = "POST";
                StringBuilder sb = new StringBuilder();
                req.Accept="*/*";
                sb.Append("schno=" + K12.Data.School.Code);
                sb.Append("&user=admin");
                sb.Append("&content=" + _UpdateRecDoc.Data.ToString());
                req.ContentType="application/x-www-form-urlencoded";

                byte[] byteArray = Encoding.UTF8.GetBytes(sb.ToString());
                req.ContentLength = byteArray.Length;
                Stream dataStream = req.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();

                HttpWebResponse rsp;
                rsp = (HttpWebResponse)req.GetResponse();
                //= req.GetResponse();
                dataStream = rsp.GetResponseStream();

               // Console.WriteLine(((HttpWebResponse)rsp).StatusDescription);
                StreamReader reader = new StreamReader(dataStream);
                // Read the content.
                string responseFromServer = reader.ReadToEnd();

                // 解析回傳後訊息
                string RspMsg = string.Empty;

                XElement rspMsgElm = null;
                try
                {
                    rspMsgElm = XElement.Parse(responseFromServer);
                    if (rspMsgElm != null)
                    {
                        if (rspMsgElm.Element("訊息") != null)
                        {
                            RspMsg = rspMsgElm.Element("訊息").Value;
                        }                    
                    }                        
                }
                catch (Exception exRsp){
                    SmartSchool.ErrorReporting.ErrorMessgae errMsg = new SmartSchool.ErrorReporting.ErrorMessgae(exRsp);
                }
                
                // Display the content.
               // Console.WriteLine(responseFromServer);
                // Clean up the streams.
                reader.Close();
                dataStream.Close();
                rsp.Close();

                //Utility.SaveString(Utility.GetXMLAttributeStr(_UpdateRecDoc.Data, "類別")+".xml",_UpdateRecDoc.Data.ToString());


                if (iDataList.Count > 0)
                    UDTTransfer.UDTUploadMemoInsert(iDataList);

                if (uDataList.Count > 0)
                    UDTTransfer.UDTUploadMemoUpdate(uDataList);

                // 清除舊有紀錄保留最新，key:學年度+學期+類別
                List<string> uidList = QueryData.GetUpdateRecDocInfoUID(data.SchoolYear, data.Semester, data.Type);
                if (uidList.Count > 0)
                {
                    List<UDT_UpdateRecDocInfo> deleteData = UDTTransfer.UDTUpdateRecDocInfoSelectByUIDs(uidList);
                    UDTTransfer.UDTUpdateRecDocInfoDelete(deleteData);
                }

                List<UDT_UpdateRecDocInfo> InsertData = new List<UDT_UpdateRecDocInfo>();
                InsertData.Add(data);
                UDTTransfer.UDTUpdateRecDocInfoInsert(InsertData);

                // 判斷後顯示回傳訊息
                if (!string.IsNullOrEmpty(RspMsg))
                {
                    MsgBox.Show(_Name + " "+RspMsg+" ,共傳送 " + _RecCount + " 筆");
                }
                else
                    MsgBox.Show(_Name + " 無法上傳");

                this.Close();
            }
            catch (Exception ex)
            {
                SmartSchool.ErrorReporting.ErrorMessgae errMsg = new SmartSchool.ErrorReporting.ErrorMessgae(ex);
                FISCA.Presentation.Controls.MsgBox.Show("上傳失敗,"+ex.Message);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CheckSchoolDistrict_Load(object sender, EventArgs e)
        {
           
        }
    }
}
