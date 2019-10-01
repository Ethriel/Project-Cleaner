using ProjectCleaner.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectCleaner
{
    public partial class ProjectCleaner : Form
    {
        Cleaner CleanerWork;
        string Path;
        public ProjectCleaner()
        {
            CleanerWork = new Cleaner();
            InitializeComponent();
        }

        private void butSelectFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog Dialog = new FolderBrowserDialog();
            string PathWithEnv = @"%USERPROFILE%\source\repos";
            string NormalPath = Environment.ExpandEnvironmentVariables(PathWithEnv);
            Dialog.SelectedPath = NormalPath;
            Dialog.ShowDialog();
            Path = Dialog.SelectedPath;
            if(PathOk())
                butStart.Visible = true;
        }

        private void butSeeResults_Click(object sender, EventArgs e)
        {
            Process.Start(Path);
        }

        private void butStart_Click(object sender, EventArgs e)
        {
            try
            {
                CleanerWork.SetParameters(Path);
                MessageBox.Show("Project folders were cleaned!", "All ok");
                butSeeResults.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Time: {DateTime.Now}\n" + $"Error: {ex.Message}", "Error");
            }
        }
        private bool PathOk()
        {
            return !(string.IsNullOrEmpty(Path));
        }
    }
}
