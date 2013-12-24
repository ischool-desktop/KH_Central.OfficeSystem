namespace KH_Central.OfficeSystem
{
    partial class StudentCategoryMapping
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgData = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.cboCentCategory = new DevComponents.DotNetBar.Controls.DataGridViewComboBoxExColumn();
            this.cboStudCategory = new DevComponents.DotNetBar.Controls.DataGridViewComboBoxExColumn();
            this.btnSave = new DevComponents.DotNetBar.ButtonX();
            this.btnExit = new DevComponents.DotNetBar.ButtonX();
            this.lblMsg = new DevComponents.DotNetBar.LabelX();
            this.getCentCategory = new DevComponents.DotNetBar.ButtonX();
            ((System.ComponentModel.ISupportInitialize)(this.dgData)).BeginInit();
            this.SuspendLayout();
            // 
            // dgData
            // 
            this.dgData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgData.BackgroundColor = System.Drawing.Color.White;
            this.dgData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cboCentCategory,
            this.cboStudCategory});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgData.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgData.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgData.Location = new System.Drawing.Point(13, 13);
            this.dgData.Name = "dgData";
            this.dgData.RowTemplate.Height = 24;
            this.dgData.Size = new System.Drawing.Size(419, 198);
            this.dgData.TabIndex = 0;
            this.dgData.CurrentCellDirtyStateChanged += new System.EventHandler(this.dgData_CurrentCellDirtyStateChanged);
            // 
            // cboCentCategory
            // 
            this.cboCentCategory.DisplayMember = "Text";
            this.cboCentCategory.DropDownHeight = 106;
            this.cboCentCategory.DropDownWidth = 121;
            this.cboCentCategory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboCentCategory.HeaderText = "局端類別";
            this.cboCentCategory.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cboCentCategory.IntegralHeight = false;
            this.cboCentCategory.ItemHeight = 17;
            this.cboCentCategory.Name = "cboCentCategory";
            this.cboCentCategory.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.cboCentCategory.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cboCentCategory.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.cboCentCategory.Width = 150;
            // 
            // cboStudCategory
            // 
            this.cboStudCategory.DisplayMember = "Text";
            this.cboStudCategory.DropDownHeight = 106;
            this.cboStudCategory.DropDownWidth = 121;
            this.cboStudCategory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboStudCategory.HeaderText = "學生類別";
            this.cboStudCategory.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cboStudCategory.IntegralHeight = false;
            this.cboStudCategory.ItemHeight = 17;
            this.cboStudCategory.Name = "cboStudCategory";
            this.cboStudCategory.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.cboStudCategory.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cboStudCategory.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.cboStudCategory.Width = 200;
            // 
            // btnSave
            // 
            this.btnSave.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.AutoSize = true;
            this.btnSave.BackColor = System.Drawing.Color.Transparent;
            this.btnSave.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSave.Location = new System.Drawing.Point(278, 218);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 25);
            this.btnSave.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "儲存";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnExit
            // 
            this.btnExit.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.AutoSize = true;
            this.btnExit.BackColor = System.Drawing.Color.Transparent;
            this.btnExit.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnExit.Location = new System.Drawing.Point(357, 218);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 25);
            this.btnExit.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnExit.TabIndex = 2;
            this.btnExit.Text = "離開";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
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
            this.lblMsg.Location = new System.Drawing.Point(13, 219);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(0, 0);
            this.lblMsg.TabIndex = 3;
            // 
            // getCentCategory
            // 
            this.getCentCategory.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.getCentCategory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.getCentCategory.AutoSize = true;
            this.getCentCategory.BackColor = System.Drawing.Color.Transparent;
            this.getCentCategory.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.getCentCategory.Location = new System.Drawing.Point(181, 218);
            this.getCentCategory.Name = "getCentCategory";
            this.getCentCategory.Size = new System.Drawing.Size(91, 25);
            this.getCentCategory.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.getCentCategory.TabIndex = 4;
            this.getCentCategory.Text = "取得局端類別";
            this.getCentCategory.Click += new System.EventHandler(this.getCentCategory_Click);
            // 
            // StudentCategoryMapping
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(444, 249);
            this.Controls.Add(this.getCentCategory);
            this.Controls.Add(this.lblMsg);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.dgData);
            this.DoubleBuffered = true;
            this.Name = "StudentCategoryMapping";
            this.Text = "局端類別與學生類別對照設定";
            this.Load += new System.EventHandler(this.StudentCategoryMapping_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevComponents.DotNetBar.Controls.DataGridViewX dgData;
        private DevComponents.DotNetBar.ButtonX btnSave;
        private DevComponents.DotNetBar.ButtonX btnExit;
        private DevComponents.DotNetBar.Controls.DataGridViewComboBoxExColumn cboCentCategory;
        private DevComponents.DotNetBar.Controls.DataGridViewComboBoxExColumn cboStudCategory;
        private DevComponents.DotNetBar.LabelX lblMsg;
        private DevComponents.DotNetBar.ButtonX getCentCategory;
    }
}