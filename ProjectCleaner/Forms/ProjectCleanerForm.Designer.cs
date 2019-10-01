namespace ProjectCleaner
{
    partial class ProjectCleaner
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
            this.selectFolder = new System.Windows.Forms.FolderBrowserDialog();
            this.butSelectFolder = new System.Windows.Forms.Button();
            this.butSeeResults = new System.Windows.Forms.Button();
            this.butStart = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // butSelectFolder
            // 
            this.butSelectFolder.Location = new System.Drawing.Point(12, 12);
            this.butSelectFolder.Name = "butSelectFolder";
            this.butSelectFolder.Size = new System.Drawing.Size(75, 23);
            this.butSelectFolder.TabIndex = 0;
            this.butSelectFolder.Text = "SelectFolder";
            this.butSelectFolder.UseVisualStyleBackColor = true;
            this.butSelectFolder.Click += new System.EventHandler(this.butSelectFolder_Click);
            // 
            // butSeeResults
            // 
            this.butSeeResults.Location = new System.Drawing.Point(13, 52);
            this.butSeeResults.Name = "butSeeResults";
            this.butSeeResults.Size = new System.Drawing.Size(75, 23);
            this.butSeeResults.TabIndex = 1;
            this.butSeeResults.Text = "See Results";
            this.butSeeResults.UseVisualStyleBackColor = true;
            this.butSeeResults.Visible = false;
            this.butSeeResults.Click += new System.EventHandler(this.butSeeResults_Click);
            // 
            // butStart
            // 
            this.butStart.Location = new System.Drawing.Point(105, 11);
            this.butStart.Name = "butStart";
            this.butStart.Size = new System.Drawing.Size(75, 23);
            this.butStart.TabIndex = 2;
            this.butStart.Text = "Start";
            this.butStart.UseVisualStyleBackColor = true;
            this.butStart.Visible = false;
            this.butStart.Click += new System.EventHandler(this.butStart_Click);
            // 
            // ProjectCleaner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(239, 91);
            this.Controls.Add(this.butStart);
            this.Controls.Add(this.butSeeResults);
            this.Controls.Add(this.butSelectFolder);
            this.Name = "ProjectCleaner";
            this.Text = "Project Cleaner";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FolderBrowserDialog selectFolder;
        private System.Windows.Forms.Button butSelectFolder;
        private System.Windows.Forms.Button butSeeResults;
        private System.Windows.Forms.Button butStart;
    }
}

