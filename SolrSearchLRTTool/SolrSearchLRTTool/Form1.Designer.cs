namespace SolrSearchLRTTool
{
    partial class formLTRScore
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(formLTRScore));
            this.cbxType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.numScore = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.txtUrl = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtKey = new System.Windows.Forms.TextBox();
            this.btnCreateData = new System.Windows.Forms.Button();
            this.numCreateNum = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cbxSearchType = new System.Windows.Forms.ComboBox();
            this.numStart = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbtnCreateData = new System.Windows.Forms.ToolStripButton();
            this.label8 = new System.Windows.Forms.Label();
            this.lblnum = new System.Windows.Forms.Label();
            this.tsbtnExchangeKeyWord = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.numScore)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCreateNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numStart)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbxType
            // 
            this.cbxType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxType.FormattingEnabled = true;
            this.cbxType.Items.AddRange(new object[] {
            "CLICK_LOGS",
            "HUMAN_JUDGEMENT"});
            this.cbxType.Location = new System.Drawing.Point(131, 52);
            this.cbxType.Name = "cbxType";
            this.cbxType.Size = new System.Drawing.Size(149, 21);
            this.cbxType.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(58, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "打分方式：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(333, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "分数：";
            // 
            // numScore
            // 
            this.numScore.Location = new System.Drawing.Point(382, 52);
            this.numScore.Name = "numScore";
            this.numScore.Size = new System.Drawing.Size(149, 20);
            this.numScore.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(66, 114);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "SolrURL：";
            // 
            // txtUrl
            // 
            this.txtUrl.Location = new System.Drawing.Point(131, 111);
            this.txtUrl.Name = "txtUrl";
            this.txtUrl.Size = new System.Drawing.Size(400, 20);
            this.txtUrl.TabIndex = 5;
            this.txtUrl.Text = "http://localhost:8981/solr/JapanDocs/select?";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(46, 168);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "检索关键字：";
            // 
            // txtKey
            // 
            this.txtKey.Location = new System.Drawing.Point(131, 164);
            this.txtKey.Name = "txtKey";
            this.txtKey.Size = new System.Drawing.Size(400, 20);
            this.txtKey.TabIndex = 7;
            this.txtKey.Text = "金融商品取引法";
            // 
            // btnCreateData
            // 
            this.btnCreateData.Location = new System.Drawing.Point(222, 304);
            this.btnCreateData.Name = "btnCreateData";
            this.btnCreateData.Size = new System.Drawing.Size(154, 23);
            this.btnCreateData.TabIndex = 8;
            this.btnCreateData.Text = "生成训练数据";
            this.btnCreateData.UseVisualStyleBackColor = true;
            this.btnCreateData.Click += new System.EventHandler(this.btnCreateData_Click);
            // 
            // numCreateNum
            // 
            this.numCreateNum.Location = new System.Drawing.Point(131, 261);
            this.numCreateNum.Name = "numCreateNum";
            this.numCreateNum.Size = new System.Drawing.Size(149, 20);
            this.numCreateNum.TabIndex = 10;
            this.numCreateNum.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(58, 265);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "生成条数：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(58, 208);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "查询列名：";
            // 
            // cbxSearchType
            // 
            this.cbxSearchType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxSearchType.FormattingEnabled = true;
            this.cbxSearchType.Items.AddRange(new object[] {
            "Abstract",
            "Body",
            "Title"});
            this.cbxSearchType.Location = new System.Drawing.Point(131, 204);
            this.cbxSearchType.Name = "cbxSearchType";
            this.cbxSearchType.Size = new System.Drawing.Size(149, 21);
            this.cbxSearchType.TabIndex = 11;
            // 
            // numStart
            // 
            this.numStart.Location = new System.Drawing.Point(382, 205);
            this.numStart.Name = "numStart";
            this.numStart.Size = new System.Drawing.Size(149, 20);
            this.numStart.TabIndex = 14;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(309, 209);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(67, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = "开始页数：";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnCreateData,
            this.tsbtnExchangeKeyWord});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(593, 25);
            this.toolStrip1.TabIndex = 15;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbtnCreateData
            // 
            this.tsbtnCreateData.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbtnCreateData.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnCreateData.Image")));
            this.tsbtnCreateData.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnCreateData.Name = "tsbtnCreateData";
            this.tsbtnCreateData.Size = new System.Drawing.Size(89, 22);
            this.tsbtnCreateData.Text = "批量生成数据";
            this.tsbtnCreateData.Click += new System.EventHandler(this.tsbtnCreateData_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Red;
            this.label8.Location = new System.Drawing.Point(73, 347);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(122, 25);
            this.label8.TabIndex = 16;
            this.label8.Text = "生成数量：";
            // 
            // lblnum
            // 
            this.lblnum.AutoSize = true;
            this.lblnum.BackColor = System.Drawing.Color.Transparent;
            this.lblnum.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblnum.ForeColor = System.Drawing.Color.Red;
            this.lblnum.Location = new System.Drawing.Point(204, 347);
            this.lblnum.Name = "lblnum";
            this.lblnum.Size = new System.Drawing.Size(47, 25);
            this.lblnum.TabIndex = 17;
            this.lblnum.Text = "0条";
            // 
            // tsbtnExchangeKeyWord
            // 
            this.tsbtnExchangeKeyWord.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbtnExchangeKeyWord.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnExchangeKeyWord.Image")));
            this.tsbtnExchangeKeyWord.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnExchangeKeyWord.Name = "tsbtnExchangeKeyWord";
            this.tsbtnExchangeKeyWord.Size = new System.Drawing.Size(76, 22);
            this.tsbtnExchangeKeyWord.Text = "关键字转换";
            this.tsbtnExchangeKeyWord.Click += new System.EventHandler(this.tsbtnExchangeKeyWord_Click);
            // 
            // formLTRScore
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(593, 409);
            this.Controls.Add(this.lblnum);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.numStart);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cbxSearchType);
            this.Controls.Add(this.numCreateNum);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnCreateData);
            this.Controls.Add(this.txtKey);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtUrl);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.numScore);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbxType);
            this.Name = "formLTRScore";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "生成训练数据";
            ((System.ComponentModel.ISupportInitialize)(this.numScore)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCreateNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numStart)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbxType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numScore;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtUrl;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtKey;
        private System.Windows.Forms.Button btnCreateData;
        private System.Windows.Forms.NumericUpDown numCreateNum;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbxSearchType;
        private System.Windows.Forms.NumericUpDown numStart;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbtnCreateData;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblnum;
        private System.Windows.Forms.ToolStripButton tsbtnExchangeKeyWord;
    }
}

