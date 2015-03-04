namespace KH_Central.OfficeSystem
{
    partial class CheckSchoolDistrict
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnStartUpdata = new DevComponents.DotNetBar.ButtonX();
            this.btnExit = new DevComponents.DotNetBar.ButtonX();
            this.dgData = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.批次輸入說明ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lblMsg = new DevComponents.DotNetBar.LabelX();
            this.lblUploadMsg = new DevComponents.DotNetBar.LabelX();
            this.colStudentNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colClassName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colGradeYear = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colGender = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colIDNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colBirthday = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colURDef = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDef = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNote = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAddress = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgData)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnStartUpdata
            // 
            this.btnStartUpdata.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnStartUpdata.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStartUpdata.AutoSize = true;
            this.btnStartUpdata.BackColor = System.Drawing.Color.Transparent;
            this.btnStartUpdata.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnStartUpdata.Location = new System.Drawing.Point(822, 381);
            this.btnStartUpdata.Name = "btnStartUpdata";
            this.btnStartUpdata.Size = new System.Drawing.Size(75, 25);
            this.btnStartUpdata.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnStartUpdata.TabIndex = 0;
            this.btnStartUpdata.Text = "開始上傳";
            this.btnStartUpdata.Click += new System.EventHandler(this.btnStartUpdata_Click);
            // 
            // btnExit
            // 
            this.btnExit.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.AutoSize = true;
            this.btnExit.BackColor = System.Drawing.Color.Transparent;
            this.btnExit.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnExit.Location = new System.Drawing.Point(903, 381);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 25);
            this.btnExit.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnExit.TabIndex = 1;
            this.btnExit.Text = "離開";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // dgData
            // 
            this.dgData.AllowUserToAddRows = false;
            this.dgData.AllowUserToDeleteRows = false;
            this.dgData.AllowUserToResizeRows = false;
            this.dgData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgData.BackgroundColor = System.Drawing.Color.White;
            this.dgData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colStudentNumber,
            this.colClassName,
            this.colGradeYear,
            this.colGender,
            this.colName,
            this.colIDNumber,
            this.colBirthday,
            this.colURDef,
            this.colDef,
            this.colNote,
            this.colAddress});
            this.dgData.ContextMenuStrip = this.contextMenuStrip1;
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgData.DefaultCellStyle = dataGridViewCellStyle11;
            this.dgData.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgData.Location = new System.Drawing.Point(12, 39);
            this.dgData.Name = "dgData";
            this.dgData.RowHeadersVisible = false;
            this.dgData.RowTemplate.Height = 24;
            this.dgData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgData.Size = new System.Drawing.Size(966, 334);
            this.dgData.TabIndex = 2;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.批次輸入說明ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(147, 26);
            // 
            // 批次輸入說明ToolStripMenuItem
            // 
            this.批次輸入說明ToolStripMenuItem.Name = "批次輸入說明ToolStripMenuItem";
            this.批次輸入說明ToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.批次輸入說明ToolStripMenuItem.Text = "批次輸入說明";
            this.批次輸入說明ToolStripMenuItem.Click += new System.EventHandler(this.批次輸入說明ToolStripMenuItem_Click);
            // 
            // lblMsg
            // 
            this.lblMsg.AutoSize = true;
            this.lblMsg.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.lblMsg.BackgroundStyle.Class = "";
            this.lblMsg.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblMsg.Location = new System.Drawing.Point(12, 12);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(299, 21);
            this.lblMsg.TabIndex = 3;
            this.lblMsg.Text = "學區驗證結果：有1筆資料不在學區內,請填寫備註";
            // 
            // lblUploadMsg
            // 
            this.lblUploadMsg.AutoSize = true;
            this.lblUploadMsg.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.lblUploadMsg.BackgroundStyle.Class = "";
            this.lblUploadMsg.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblUploadMsg.Location = new System.Drawing.Point(12, 381);
            this.lblUploadMsg.Name = "lblUploadMsg";
            this.lblUploadMsg.Size = new System.Drawing.Size(74, 21);
            this.lblUploadMsg.TabIndex = 4;
            this.lblUploadMsg.Text = "上傳狀態：";
            // 
            // colStudentNumber
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.LightCyan;
            this.colStudentNumber.DefaultCellStyle = dataGridViewCellStyle1;
            this.colStudentNumber.HeaderText = "學號";
            this.colStudentNumber.Name = "colStudentNumber";
            this.colStudentNumber.ReadOnly = true;
            this.colStudentNumber.Width = 80;
            // 
            // colClassName
            // 
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.LightCyan;
            this.colClassName.DefaultCellStyle = dataGridViewCellStyle2;
            this.colClassName.HeaderText = "班級";
            this.colClassName.Name = "colClassName";
            this.colClassName.ReadOnly = true;
            this.colClassName.Width = 80;
            // 
            // colGradeYear
            // 
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.LightCyan;
            this.colGradeYear.DefaultCellStyle = dataGridViewCellStyle3;
            this.colGradeYear.HeaderText = "年級";
            this.colGradeYear.Name = "colGradeYear";
            this.colGradeYear.ReadOnly = true;
            this.colGradeYear.Width = 60;
            // 
            // colGender
            // 
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.LightCyan;
            this.colGender.DefaultCellStyle = dataGridViewCellStyle4;
            this.colGender.HeaderText = "性別";
            this.colGender.Name = "colGender";
            this.colGender.ReadOnly = true;
            this.colGender.Width = 60;
            // 
            // colName
            // 
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.LightCyan;
            this.colName.DefaultCellStyle = dataGridViewCellStyle5;
            this.colName.HeaderText = "姓名";
            this.colName.Name = "colName";
            this.colName.ReadOnly = true;
            this.colName.Width = 80;
            // 
            // colIDNumber
            // 
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.LightCyan;
            this.colIDNumber.DefaultCellStyle = dataGridViewCellStyle6;
            this.colIDNumber.HeaderText = "身分證號";
            this.colIDNumber.Name = "colIDNumber";
            this.colIDNumber.ReadOnly = true;
            // 
            // colBirthday
            // 
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.LightCyan;
            this.colBirthday.DefaultCellStyle = dataGridViewCellStyle7;
            this.colBirthday.HeaderText = "生日";
            this.colBirthday.Name = "colBirthday";
            this.colBirthday.ReadOnly = true;
            // 
            // colURDef
            // 
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.LightCyan;
            this.colURDef.DefaultCellStyle = dataGridViewCellStyle8;
            this.colURDef.HeaderText = "異動備註";
            this.colURDef.Name = "colURDef";
            this.colURDef.ReadOnly = true;
            // 
            // colDef
            // 
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.LightCyan;
            this.colDef.DefaultCellStyle = dataGridViewCellStyle9;
            this.colDef.HeaderText = "備註";
            this.colDef.Name = "colDef";
            this.colDef.ReadOnly = true;
            // 
            // colNote
            // 
            this.colNote.HeaderText = "說明";
            this.colNote.Name = "colNote";
            // 
            // colAddress
            // 
            this.colAddress.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.LightCyan;
            this.colAddress.DefaultCellStyle = dataGridViewCellStyle10;
            this.colAddress.HeaderText = "名冊地址";
            this.colAddress.Name = "colAddress";
            this.colAddress.ReadOnly = true;
            this.colAddress.Width = 85;
            // 
            // CheckSchoolDistrict
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(990, 412);
            this.Controls.Add(this.lblUploadMsg);
            this.Controls.Add(this.lblMsg);
            this.Controls.Add(this.dgData);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnStartUpdata);
            this.DoubleBuffered = true;
            this.Name = "CheckSchoolDistrict";
            this.Text = "學區資料驗證";
            this.Load += new System.EventHandler(this.CheckSchoolDistrict_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgData)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX btnStartUpdata;
        private DevComponents.DotNetBar.ButtonX btnExit;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgData;
        private DevComponents.DotNetBar.LabelX lblMsg;
        private DevComponents.DotNetBar.LabelX lblUploadMsg;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 批次輸入說明ToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStudentNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn colClassName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colGradeYear;
        private System.Windows.Forms.DataGridViewTextBoxColumn colGender;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colIDNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBirthday;
        private System.Windows.Forms.DataGridViewTextBoxColumn colURDef;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDef;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNote;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAddress;
    }
}