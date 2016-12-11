using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ServiceControl
{
    public partial class Form1 : Form
    {
        string serviceName = "EasyJoinService";
        public Form1()
        {
            InitializeComponent();
        }

        private void btnInstall_Click(object sender, EventArgs e)
        {
            string CurrentDirectory = System.Environment.CurrentDirectory;
            System.Environment.CurrentDirectory = CurrentDirectory + "\\Service";
            Process process = new Process();
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.FileName = "Install.bat";
            process.StartInfo.CreateNoWindow = true;
            process.Start();
            System.Environment.CurrentDirectory = CurrentDirectory;
        }

        private void btnUninstall_Click(object sender, EventArgs e)
        {
            string CurrentDirectory = System.Environment.CurrentDirectory;
            System.Environment.CurrentDirectory = CurrentDirectory + "\\Service";
            Process process = new Process();
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.FileName = "Uninstall.bat";
            process.StartInfo.CreateNoWindow = true;
            process.Start();
            System.Environment.CurrentDirectory = CurrentDirectory;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            ServiceController serviceController = new ServiceController(serviceName);
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            ServiceController serviceController = new ServiceController(serviceName);
            if (serviceController.CanStop)
                serviceController.Stop();
        }

        private void btnPauseContinue_Click(object sender, EventArgs e)
        {
            ServiceController serviceController = new ServiceController(serviceName);
            if (serviceController.CanPauseAndContinue)
            {
                if (serviceController.Status == ServiceControllerStatus.Running)
                {
                    serviceController.Pause();
                    btnPauseContinue.Text = "继续";
                }
                else if (serviceController.Status == ServiceControllerStatus.Paused)
                {
                    serviceController.Continue();
                    btnPauseContinue.Text = "暂停";
                }
            }
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            ServiceController serviceController = new ServiceController(serviceName);
            string Status = serviceController.Status.ToString();
            lbState.Text = "状态:" + Status;
        }
    }
}
