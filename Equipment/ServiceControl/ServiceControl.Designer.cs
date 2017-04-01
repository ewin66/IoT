namespace ServiceControl
{
    partial class ServiceControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ServiceControl));
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.启动ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.停止ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.显示主界面ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.退出ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.listLog = new System.Windows.Forms.ListBox();
            this.ckbMessage = new System.Windows.Forms.CheckBox();
            this.txtComm = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ckbClearData = new System.Windows.Forms.CheckBox();
            this.txtDB = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.rdbNet = new System.Windows.Forms.RadioButton();
            this.rdb485 = new System.Windows.Forms.RadioButton();
            this.cmbPort = new System.Windows.Forms.ComboBox();
            this.btnOpenport = new System.Windows.Forms.Button();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.启动ToolStripMenuItem,
            this.停止ToolStripMenuItem,
            this.显示主界面ToolStripMenuItem,
            this.退出ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(137, 92);
            // 
            // 启动ToolStripMenuItem
            // 
            this.启动ToolStripMenuItem.Name = "启动ToolStripMenuItem";
            this.启动ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.启动ToolStripMenuItem.Text = "启动";
            // 
            // 停止ToolStripMenuItem
            // 
            this.停止ToolStripMenuItem.Name = "停止ToolStripMenuItem";
            this.停止ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.停止ToolStripMenuItem.Text = "停止";
            // 
            // 显示主界面ToolStripMenuItem
            // 
            this.显示主界面ToolStripMenuItem.Name = "显示主界面ToolStripMenuItem";
            this.显示主界面ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.显示主界面ToolStripMenuItem.Text = "显示主界面";
            // 
            // 退出ToolStripMenuItem
            // 
            this.退出ToolStripMenuItem.Name = "退出ToolStripMenuItem";
            this.退出ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.退出ToolStripMenuItem.Text = "退出";
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(13, 13);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "启动";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(12, 42);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 23);
            this.btnStop.TabIndex = 1;
            this.btnStop.Text = "停止";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Visible = false;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // listLog
            // 
            this.listLog.FormattingEnabled = true;
            this.listLog.ItemHeight = 12;
            this.listLog.Location = new System.Drawing.Point(115, 12);
            this.listLog.Name = "listLog";
            this.listLog.Size = new System.Drawing.Size(615, 208);
            this.listLog.TabIndex = 2;
            this.listLog.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listLog_MouseDoubleClick);
            // 
            // ckbMessage
            // 
            this.ckbMessage.AutoSize = true;
            this.ckbMessage.Checked = true;
            this.ckbMessage.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbMessage.Location = new System.Drawing.Point(13, 71);
            this.ckbMessage.Name = "ckbMessage";
            this.ckbMessage.Size = new System.Drawing.Size(72, 16);
            this.ckbMessage.TabIndex = 3;
            this.ckbMessage.Text = "显示消息";
            this.ckbMessage.UseVisualStyleBackColor = true;
            // 
            // txtComm
            // 
            this.txtComm.Location = new System.Drawing.Point(168, 226);
            this.txtComm.Name = "txtComm";
            this.txtComm.Size = new System.Drawing.Size(350, 21);
            this.txtComm.TabIndex = 4;
            this.txtComm.Text = "RS232/RS485 TO RJ45&WIFI CONVERTER";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(113, 230);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "模组编号";
            // 
            // ckbClearData
            // 
            this.ckbClearData.AutoSize = true;
            this.ckbClearData.Checked = true;
            this.ckbClearData.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbClearData.Location = new System.Drawing.Point(13, 94);
            this.ckbClearData.Name = "ckbClearData";
            this.ckbClearData.Size = new System.Drawing.Size(96, 16);
            this.ckbClearData.TabIndex = 6;
            this.ckbClearData.Text = "自动清理数据";
            this.ckbClearData.UseVisualStyleBackColor = true;
            // 
            // txtDB
            // 
            this.txtDB.Location = new System.Drawing.Point(630, 226);
            this.txtDB.Name = "txtDB";
            this.txtDB.Size = new System.Drawing.Size(100, 21);
            this.txtDB.TabIndex = 7;
            this.txtDB.Text = "192.168.1.222";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(547, 230);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 8;
            this.label2.Text = "数据库服务器";
            // 
            // rdbNet
            // 
            this.rdbNet.AutoSize = true;
            this.rdbNet.Location = new System.Drawing.Point(13, 126);
            this.rdbNet.Name = "rdbNet";
            this.rdbNet.Size = new System.Drawing.Size(71, 16);
            this.rdbNet.TabIndex = 9;
            this.rdbNet.Text = "网络传输";
            this.rdbNet.UseVisualStyleBackColor = true;
            // 
            // rdb485
            // 
            this.rdb485.AutoSize = true;
            this.rdb485.Checked = true;
            this.rdb485.Location = new System.Drawing.Point(12, 148);
            this.rdb485.Name = "rdb485";
            this.rdb485.Size = new System.Drawing.Size(71, 16);
            this.rdb485.TabIndex = 10;
            this.rdb485.TabStop = true;
            this.rdb485.Text = "串口直连";
            this.rdb485.UseVisualStyleBackColor = true;
            // 
            // cmbPort
            // 
            this.cmbPort.FormattingEnabled = true;
            this.cmbPort.Location = new System.Drawing.Point(12, 170);
            this.cmbPort.Name = "cmbPort";
            this.cmbPort.Size = new System.Drawing.Size(89, 20);
            this.cmbPort.TabIndex = 11;
            // 
            // btnOpenport
            // 
            this.btnOpenport.Location = new System.Drawing.Point(12, 197);
            this.btnOpenport.Name = "btnOpenport";
            this.btnOpenport.Size = new System.Drawing.Size(75, 23);
            this.btnOpenport.TabIndex = 12;
            this.btnOpenport.Text = "打开端口";
            this.btnOpenport.UseVisualStyleBackColor = true;
            this.btnOpenport.Click += new System.EventHandler(this.btnOpenport_Click);
            // 
            // ServiceControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(742, 275);
            this.Controls.Add(this.btnOpenport);
            this.Controls.Add(this.cmbPort);
            this.Controls.Add(this.rdb485);
            this.Controls.Add(this.rdbNet);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtDB);
            this.Controls.Add(this.ckbClearData);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtComm);
            this.Controls.Add(this.ckbMessage);
            this.Controls.Add(this.listLog);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnStart);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "ServiceControl";
            this.Text = "聚云IoT数据通讯服务";
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.ListBox listLog;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 启动ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 停止ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 显示主界面ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 退出ToolStripMenuItem;
        private System.Windows.Forms.CheckBox ckbMessage;
        private System.Windows.Forms.TextBox txtComm;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox ckbClearData;
        private System.Windows.Forms.TextBox txtDB;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton rdbNet;
        private System.Windows.Forms.RadioButton rdb485;
        private System.Windows.Forms.ComboBox cmbPort;
        private System.Windows.Forms.Button btnOpenport;
    }
}