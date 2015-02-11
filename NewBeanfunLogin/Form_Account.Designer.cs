namespace NewBeanfunLogin
{
    partial class Form_Account
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_Account));
            this.lstAccount = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.bgwk_keepsession = new System.ComponentModel.BackgroundWorker();
            this.bgwk = new System.ComponentModel.BackgroundWorker();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblPath = new System.Windows.Forms.Label();
            this.lblSpace2 = new System.Windows.Forms.Label();
            this.chkAutoLogin = new System.Windows.Forms.CheckBox();
            this.lblSpace1 = new System.Windows.Forms.Label();
            this.tmrAutoLogin = new System.Windows.Forms.Timer(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.btnStartGame = new System.Windows.Forms.ToolStripSplitButton();
            this.btnGetPassword = new System.Windows.Forms.ToolStripSplitButton();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.bgwk_InjectDLL = new System.ComponentModel.BackgroundWorker();
            this.panel1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lstAccount
            // 
            this.lstAccount.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.lstAccount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstAccount.FullRowSelect = true;
            this.lstAccount.GridLines = true;
            this.lstAccount.Location = new System.Drawing.Point(0, 20);
            this.lstAccount.Name = "lstAccount";
            this.lstAccount.Size = new System.Drawing.Size(344, 148);
            this.lstAccount.TabIndex = 1;
            this.lstAccount.UseCompatibleStateImageBehavior = false;
            this.lstAccount.View = System.Windows.Forms.View.Details;
            this.lstAccount.DoubleClick += new System.EventHandler(this.lstAccount_DoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "帳號名稱";
            this.columnHeader1.Width = 120;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "遊戲帳號";
            this.columnHeader2.Width = 150;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "帳號編號";
            // 
            // bgwk_keepsession
            // 
            this.bgwk_keepsession.WorkerSupportsCancellation = true;
            this.bgwk_keepsession.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwk_keepsession_DoWork);
            this.bgwk_keepsession.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwk_keepsession_RunWorkerCompleted);
            // 
            // bgwk
            // 
            this.bgwk.WorkerSupportsCancellation = true;
            this.bgwk.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwk_DoWork);
            this.bgwk.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwk_RunWorkerCompleted);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblPath);
            this.panel1.Controls.Add(this.lblSpace2);
            this.panel1.Controls.Add(this.chkAutoLogin);
            this.panel1.Controls.Add(this.lblSpace1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(344, 20);
            this.panel1.TabIndex = 2;
            // 
            // lblPath
            // 
            this.lblPath.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPath.Location = new System.Drawing.Point(83, 0);
            this.lblPath.Name = "lblPath";
            this.lblPath.Size = new System.Drawing.Size(255, 20);
            this.lblPath.TabIndex = 3;
            this.lblPath.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblPath.Click += new System.EventHandler(this.lblPath_Click);
            this.lblPath.Paint += new System.Windows.Forms.PaintEventHandler(this.lblPath_Paint);
            // 
            // lblSpace2
            // 
            this.lblSpace2.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblSpace2.Location = new System.Drawing.Point(338, 0);
            this.lblSpace2.Name = "lblSpace2";
            this.lblSpace2.Size = new System.Drawing.Size(6, 20);
            this.lblSpace2.TabIndex = 2;
            // 
            // chkAutoLogin
            // 
            this.chkAutoLogin.AutoSize = true;
            this.chkAutoLogin.Dock = System.Windows.Forms.DockStyle.Left;
            this.chkAutoLogin.Location = new System.Drawing.Point(11, 0);
            this.chkAutoLogin.Name = "chkAutoLogin";
            this.chkAutoLogin.Size = new System.Drawing.Size(72, 20);
            this.chkAutoLogin.TabIndex = 0;
            this.chkAutoLogin.Text = "自動重啟";
            this.chkAutoLogin.UseVisualStyleBackColor = true;
            this.chkAutoLogin.CheckedChanged += new System.EventHandler(this.chkAutoLogin_CheckedChanged);
            // 
            // lblSpace1
            // 
            this.lblSpace1.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblSpace1.Location = new System.Drawing.Point(0, 0);
            this.lblSpace1.Name = "lblSpace1";
            this.lblSpace1.Size = new System.Drawing.Size(11, 20);
            this.lblSpace1.TabIndex = 1;
            // 
            // tmrAutoLogin
            // 
            this.tmrAutoLogin.Interval = 5000;
            this.tmrAutoLogin.Tick += new System.EventHandler(this.tmrAutoLogin_Tick);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnStartGame,
            this.btnGetPassword,
            this.lblStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 168);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(344, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // btnStartGame
            // 
            this.btnStartGame.Image = ((System.Drawing.Image)(resources.GetObject("btnStartGame.Image")));
            this.btnStartGame.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnStartGame.Name = "btnStartGame";
            this.btnStartGame.Size = new System.Drawing.Size(87, 20);
            this.btnStartGame.Text = "啟動遊戲";
            this.btnStartGame.ButtonClick += new System.EventHandler(this.btnStartGame_ButtonClick);
            // 
            // btnGetPassword
            // 
            this.btnGetPassword.Image = ((System.Drawing.Image)(resources.GetObject("btnGetPassword.Image")));
            this.btnGetPassword.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGetPassword.Name = "btnGetPassword";
            this.btnGetPassword.Size = new System.Drawing.Size(87, 20);
            this.btnGetPassword.Text = "取得帳密";
            this.btnGetPassword.ButtonClick += new System.EventHandler(this.btnGetPassword_ButtonClick);
            // 
            // lblStatus
            // 
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(52, 17);
            this.lblStatus.Text = "預備中...";
            // 
            // bgwk_InjectDLL
            // 
            this.bgwk_InjectDLL.WorkerSupportsCancellation = true;
            this.bgwk_InjectDLL.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwk_InjectDLL_DoWork);
            // 
            // Form_Account
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(344, 190);
            this.Controls.Add(this.lstAccount);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStrip1);
            this.Name = "Form_Account";
            this.Text = "啟動遊戲";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_Account_FormClosing);
            this.Load += new System.EventHandler(this.Form_Account_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        public System.Windows.Forms.ListView lstAccount;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.ComponentModel.BackgroundWorker bgwk_keepsession;
        private System.ComponentModel.BackgroundWorker bgwk;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox chkAutoLogin;
        private System.Windows.Forms.Timer tmrAutoLogin;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.ToolStripSplitButton btnStartGame;
        private System.Windows.Forms.Label lblPath;
        private System.Windows.Forms.Label lblSpace2;
        private System.Windows.Forms.Label lblSpace1;
        private System.ComponentModel.BackgroundWorker bgwk_InjectDLL;
        private System.Windows.Forms.ToolStripSplitButton btnGetPassword;
    }
}
