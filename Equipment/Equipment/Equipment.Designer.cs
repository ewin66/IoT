namespace Equipment
{
    partial class Equipment
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnOpenPort = new System.Windows.Forms.Button();
            this.btnAutomatic = new System.Windows.Forms.Button();
            this.txtSend = new System.Windows.Forms.TextBox();
            this.cobSendSelect = new System.Windows.Forms.ComboBox();
            this.listReceive = new System.Windows.Forms.ListBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.chartWaterLevel = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.rdBtnUSBRS485 = new System.Windows.Forms.RadioButton();
            this.rdBtnNetRS485 = new System.Windows.Forms.RadioButton();
            this.btnTestMySql = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartWaterLevel)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.btnTestMySql);
            this.groupBox1.Controls.Add(this.rdBtnNetRS485);
            this.groupBox1.Controls.Add(this.rdBtnUSBRS485);
            this.groupBox1.Controls.Add(this.btnOpenPort);
            this.groupBox1.Controls.Add(this.btnAutomatic);
            this.groupBox1.Controls.Add(this.txtSend);
            this.groupBox1.Controls.Add(this.cobSendSelect);
            this.groupBox1.Controls.Add(this.listReceive);
            this.groupBox1.Controls.Add(this.btnSend);
            this.groupBox1.Location = new System.Drawing.Point(18, 319);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(818, 109);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "水位";
            // 
            // btnOpenPort
            // 
            this.btnOpenPort.Location = new System.Drawing.Point(737, 78);
            this.btnOpenPort.Name = "btnOpenPort";
            this.btnOpenPort.Size = new System.Drawing.Size(75, 23);
            this.btnOpenPort.TabIndex = 6;
            this.btnOpenPort.Text = "打开端口";
            this.btnOpenPort.UseVisualStyleBackColor = true;
            this.btnOpenPort.Click += new System.EventHandler(this.btnOpenPort_Click);
            // 
            // btnAutomatic
            // 
            this.btnAutomatic.Location = new System.Drawing.Point(737, 49);
            this.btnAutomatic.Name = "btnAutomatic";
            this.btnAutomatic.Size = new System.Drawing.Size(75, 23);
            this.btnAutomatic.TabIndex = 4;
            this.btnAutomatic.Text = "Automatic";
            this.btnAutomatic.UseVisualStyleBackColor = true;
            this.btnAutomatic.Click += new System.EventHandler(this.btnAutomatic_Click);
            // 
            // txtSend
            // 
            this.txtSend.Location = new System.Drawing.Point(17, 64);
            this.txtSend.Name = "txtSend";
            this.txtSend.Size = new System.Drawing.Size(193, 21);
            this.txtSend.TabIndex = 3;
            // 
            // cobSendSelect
            // 
            this.cobSendSelect.FormattingEnabled = true;
            this.cobSendSelect.Location = new System.Drawing.Point(17, 22);
            this.cobSendSelect.Name = "cobSendSelect";
            this.cobSendSelect.Size = new System.Drawing.Size(193, 20);
            this.cobSendSelect.TabIndex = 2;
            this.cobSendSelect.SelectedIndexChanged += new System.EventHandler(this.cobSendSelect_SelectedIndexChanged);
            // 
            // listReceive
            // 
            this.listReceive.FormattingEnabled = true;
            this.listReceive.ItemHeight = 12;
            this.listReceive.Location = new System.Drawing.Point(216, 22);
            this.listReceive.Name = "listReceive";
            this.listReceive.Size = new System.Drawing.Size(515, 64);
            this.listReceive.TabIndex = 1;
            this.listReceive.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listBox1_MouseDoubleClick);
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(737, 20);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 23);
            this.btnSend.TabIndex = 0;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Location = new System.Drawing.Point(12, 434);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(818, 35);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "水压";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Location = new System.Drawing.Point(12, 475);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(818, 40);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "温度";
            // 
            // chartWaterLevel
            // 
            chartArea1.Name = "ChartArea1";
            this.chartWaterLevel.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chartWaterLevel.Legends.Add(legend1);
            this.chartWaterLevel.Location = new System.Drawing.Point(18, 12);
            this.chartWaterLevel.Name = "chartWaterLevel";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chartWaterLevel.Series.Add(series1);
            this.chartWaterLevel.Size = new System.Drawing.Size(812, 301);
            this.chartWaterLevel.TabIndex = 5;
            this.chartWaterLevel.Text = "chart1";
            // 
            // rdBtnUSBRS485
            // 
            this.rdBtnUSBRS485.AutoSize = true;
            this.rdBtnUSBRS485.Checked = true;
            this.rdBtnUSBRS485.Location = new System.Drawing.Point(365, 87);
            this.rdBtnUSBRS485.Name = "rdBtnUSBRS485";
            this.rdBtnUSBRS485.Size = new System.Drawing.Size(83, 16);
            this.rdBtnUSBRS485.TabIndex = 7;
            this.rdBtnUSBRS485.TabStop = true;
            this.rdBtnUSBRS485.Text = "USB=>RS485";
            this.rdBtnUSBRS485.UseVisualStyleBackColor = true;
            // 
            // rdBtnNetRS485
            // 
            this.rdBtnNetRS485.AutoSize = true;
            this.rdBtnNetRS485.Location = new System.Drawing.Point(466, 87);
            this.rdBtnNetRS485.Name = "rdBtnNetRS485";
            this.rdBtnNetRS485.Size = new System.Drawing.Size(83, 16);
            this.rdBtnNetRS485.TabIndex = 8;
            this.rdBtnNetRS485.Text = "Net=>RS485";
            this.rdBtnNetRS485.UseVisualStyleBackColor = true;
            // 
            // btnTestMySql
            // 
            this.btnTestMySql.Location = new System.Drawing.Point(652, 85);
            this.btnTestMySql.Name = "btnTestMySql";
            this.btnTestMySql.Size = new System.Drawing.Size(75, 23);
            this.btnTestMySql.TabIndex = 9;
            this.btnTestMySql.Text = "测试Mysql";
            this.btnTestMySql.UseVisualStyleBackColor = true;
            this.btnTestMySql.Click += new System.EventHandler(this.btnTestMySql_Click);
            // 
            // Equipment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(842, 527);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.chartWaterLevel);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Equipment";
            this.Text = "聚云软件科技有限公司";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartWaterLevel)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox cobSendSelect;
        private System.Windows.Forms.ListBox listReceive;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartWaterLevel;
        private System.Windows.Forms.Button btnAutomatic;
        private System.Windows.Forms.TextBox txtSend;
        private System.Windows.Forms.Button btnOpenPort;
        private System.Windows.Forms.RadioButton rdBtnNetRS485;
        private System.Windows.Forms.RadioButton rdBtnUSBRS485;
        private System.Windows.Forms.Button btnTestMySql;
    }
}

