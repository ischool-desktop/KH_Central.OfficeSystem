﻿
namespace KH_Central.OfficeSystem
{
    partial class SchoolDistrict
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgData = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.colTown = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDistrict = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colArea = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnExit = new DevComponents.DotNetBar.ButtonX();
            this.btnGetCertData = new DevComponents.DotNetBar.ButtonX();
            this.lblMsg = new DevComponents.DotNetBar.LabelX();
            ((System.ComponentModel.ISupportInitialize)(this.dgData)).BeginInit();
            this.SuspendLayout();
            // 
            // dgData
            // 
            this.dgData.AllowUserToAddRows = false;
            this.dgData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgData.BackgroundColor = System.Drawing.Color.White;
            this.dgData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colTown,
            this.colDistrict,
            this.colArea});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgData.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgData.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgData.Location = new System.Drawing.Point(12, 12);
            this.dgData.Name = "dgData";
            this.dgData.RowTemplate.Height = 24;
            this.dgData.Size = new System.Drawing.Size(508, 231);
            this.dgData.TabIndex = 0;
            // 
            // colTown
            // 
            this.colTown.HeaderText = "區別";
            this.colTown.Name = "colTown";
            this.colTown.ReadOnly = true;
            // 
            // colDistrict
            // 
            this.colDistrict.HeaderText = "里別";
            this.colDistrict.Name = "colDistrict";
            this.colDistrict.ReadOnly = true;
            // 
            // colArea
            // 
            this.colArea.HeaderText = "鄰別";
            this.colArea.Name = "colArea";
            this.colArea.ReadOnly = true;
            this.colArea.Width = 250;
            // 
            // btnExit
            // 
            this.btnExit.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.AutoSize = true;
            this.btnExit.BackColor = System.Drawing.Color.Transparent;
            this.btnExit.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnExit.Location = new System.Drawing.Point(445, 254);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 25);
            this.btnExit.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnExit.TabIndex = 4;
            this.btnExit.Text = "離開";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnGetCertData
            // 
            this.btnGetCertData.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnGetCertData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnGetCertData.AutoSize = true;
            this.btnGetCertData.BackColor = System.Drawing.Color.Transparent;
            this.btnGetCertData.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnGetCertData.Location = new System.Drawing.Point(321, 254);
            this.btnGetCertData.Name = "btnGetCertData";
            this.btnGetCertData.Size = new System.Drawing.Size(118, 25);
            this.btnGetCertData.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnGetCertData.TabIndex = 5;
            this.btnGetCertData.Text = "取得局端學區資料";
            this.btnGetCertData.Click += new System.EventHandler(this.btnGetCertData_Click);
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
            this.lblMsg.Location = new System.Drawing.Point(12, 256);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(15, 21);
            this.lblMsg.TabIndex = 6;
            // 
            // SchoolDistrict
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(532, 285);
            this.Controls.Add(this.lblMsg);
            this.Controls.Add(this.btnGetCertData);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.dgData);
            this.DoubleBuffered = true;
            this.Name = "SchoolDistrict";
            this.Text = "學區資料檢視";
            this.Load += new System.EventHandler(this.SchoolDistrict_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevComponents.DotNetBar.Controls.DataGridViewX dgData;
        private DevComponents.DotNetBar.ButtonX btnExit;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTown;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDistrict;
        private System.Windows.Forms.DataGridViewTextBoxColumn colArea;
        private DevComponents.DotNetBar.ButtonX btnGetCertData;
        private DevComponents.DotNetBar.LabelX lblMsg;
    }
}