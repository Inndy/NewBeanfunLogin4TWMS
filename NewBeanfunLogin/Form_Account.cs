using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace NewBeanfunLogin
{
    public partial class Form_Account : Form
    {
        MapleBeanfun bf;
        string GamePath = @"C:\Program Files\Gamania\MapleStory\MapleStory.exe";
        string[] AutoInject = null;
        BeanfunGameAccountData last = null;
        Process lastGame = null;
        long lastTick = 0;
        bool startingGame = false;

        public Form_Account(MapleBeanfun session)
        {
            InitializeComponent();
            bf = session;
        }

        void OperateWithSelectedAccount(string method = "start")
        {
            if (lstAccount.SelectedItems.Count == 0 || startingGame) return;
            ListViewItem.ListViewSubItemCollection subs = lstAccount.SelectedItems[0].SubItems;
            switch (method)
            {
                case "start":
                    lblStatus.Text = "正在啟動帳號：" + subs[0].Text + "...";
                    break;
                case "getpwd":
                    lblStatus.Text = "正在取得密碼：" + subs[0].Text + "...";
                    break;
                default:
                    lblStatus.Text = "Operation " + method + "：" + subs[0].Text + "...";
                    break;
            }
            bgwk.RunWorkerAsync(new string[] { method, subs[1].Text, subs[2].Text, subs[0].Text });
        }

        private void btnStartGame_ButtonClick(object sender, EventArgs e)
        {
            OperateWithSelectedAccount("start");
        }

        private void btnGetPassword_ButtonClick(object sender, EventArgs e)
        {
            OperateWithSelectedAccount("getpwd");
        }

        private void lstAccount_DoubleClick(object sender, EventArgs e)
        {
            OperateWithSelectedAccount("start");
        }

        private void Form_Account_FormClosing(object sender, FormClosingEventArgs e)
        {
            bgwk_keepsession.CancelAsync();
            bgwk_InjectDLL.CancelAsync();
            bgwk.CancelAsync();
            Application.Exit();
        }

        private void bgwk_keepsession_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                try
                {
                    if (!bf.Ping()) return;
                }
                catch (BeanfunIsBusyException)
                {
                    Thread.Sleep(1000);
                    continue;
                }
                Thread.Sleep(60000);
            }
        }

        private void bgwk_keepsession_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("這次的登入已經無效", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            Application.Exit();
        }

        private void Form_Account_Load(object sender, EventArgs e)
        {
            if (File.Exists("gamepath.txt"))
                GamePath = File.ReadAllText("gamepath.txt");
            else
            {
                StreamWriter sw = File.CreateText("gamepath.txt");
                sw.Write(GamePath);
                sw.Close();
            }
            if (File.Exists("autoinject.txt"))
                AutoInject = File.ReadAllLines("autoinject.txt");
            else
                File.CreateText("autoinject.txt").Close();

            bgwk_keepsession.RunWorkerAsync();
            bgwk_InjectDLL.RunWorkerAsync();
        }

        private void bgwk_DoWork(object sender, DoWorkEventArgs e)
        {
            string[] arg = e.Argument as string[];
            if (arg == null) return;
            string acc, pwd;
            switch (arg[0])
            {
                case "start":
                    startingGame = true;
                    acc = arg[1];
                    last = new BeanfunGameAccountData(acc, arg[2], arg[3]);
                    pwd = bf.QueryOTP(last);
                    if (pwd == null)
                    {
                        e.Result = new Result(arg[0], true);
                    }
                    else
                    {
                        try
                        {
                            e.Result = new Result(arg[0], false);
                            lastGame = Process.Start(GamePath, "tw.login.maplestory.gamania.com 8484 BeanFun " + acc + " " + pwd);
                        }
                        catch(Win32Exception w)
                        {
                            startingGame = false;
                            MessageBox.Show("楓之谷在哪呢？" + w.Message);
                        }
                    }
                    break;
                case "getpwd":
                    startingGame = true;
                    acc = arg[1];
                    last = new BeanfunGameAccountData(acc, arg[2], arg[3]);
                    pwd = bf.QueryOTP(last);
                    if (pwd == null)
                    {
                        e.Result = new Result(arg[0], true);
                    }
                    else
                    {
                        e.Result = new Result(arg[0], false, new string[] { acc, pwd });
                    }
                    break;
            }
        }

        private void bgwk_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            
            Result result = e.Result as Result;
            if (result == null)
            {
                Err("不明的錯誤");
                return;
            }
            switch (result.Work)
            {
                case "start":
                    if (result.Error)
                    {
                        lblStatus.Text = "遊戲啟動失敗...";
                    }
                    else
                    {
                        lastTick = DateTime.Now.Ticks;
                        lblStatus.Text = "遊戲啟動成功...";
                    }
                    startingGame = false;
                    break;
                case "getpwd":
                    if (result.Error)
                    {
                        lblStatus.Text = "密碼取得失敗...";
                    }
                    else
                    {
                        string[] data = result.Argumment as string[];
                        if (data == null)
                        {
                            lblStatus.Text = "密碼取得失敗...";
                        }
                        else
                        {
                            lblStatus.Text = "密碼取得成功...";
                            new Form_CopyPassword(data[0], data[1]).ShowDialog(this);
                        }
                        startingGame = false;
                    }
                    break;
            }
        }

        private void Err(string Str)
        {
            MessageBox.Show(Str, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void chkAutoLogin_CheckedChanged(object sender, EventArgs e)
        {
            tmrAutoLogin.Enabled = chkAutoLogin.Checked;
        }

        private void tmrAutoLogin_Tick(object sender, EventArgs e)
        {
            try
            {
                if (last == null || startingGame) return;
                if (lastTick + 10000000 * 10 > DateTime.Now.Ticks) return;
                if (lastGame.HasExited)
                {
                    lblStatus.Text = "正在重新啟動帳號" + last.Name + "...";
                    bgwk.RunWorkerAsync(new string[] { "start", last.Account, last.Number, last.Name });
                }
            }
            catch (Exception) { }
        }

        private void lblPath_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Where is MapleStory.exe...?";
            ofd.Filter = "MapleStory.exe|MapleStory.exe";
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                StreamWriter sw = File.CreateText("gamepath.txt");
                sw.Write(GamePath = ofd.FileName);
                sw.Close();
                lblPath.Text = GamePath;
            }
        }

        private void lblPath_Paint(object sender, PaintEventArgs e)
        {
            var label = (Label)sender;
            label.Text = GamePath;
            e.Graphics.Clear(label.BackColor);
            Size sz = TextRenderer.MeasureText(label.Text, label.Font, label.Size, TextFormatFlags.PathEllipsis);
            Rectangle rect = label.DisplayRectangle;
            rect.Location = new Point(rect.Left, (rect.Height - sz.Height) / 2);
            TextRenderer.DrawText(e.Graphics, label.Text, label.Font, rect, label.ForeColor, TextFormatFlags.PathEllipsis);
        }

        private void bgwk_InjectDLL_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true && AutoInject != null)
            {
                try
                {
                    IntPtr hWnd;
                    if ((hWnd = WindowsAPI.FindWindow("StartUpDlgClass", "MapleStory")) != IntPtr.Zero)
                    {
                        int pid;
                        WindowsAPI.GetWindowThreadProcessId(hWnd, out pid);
                        Process game = Process.GetProcessById(pid);
                        foreach (string dll in AutoInject)
                        {
                            string dll_file = (dll.Substring(1, 2) != ":\\") ?
								Path.Combine(Application.StartupPath, dll) : dll;
                            DLLInjector.InjectDLL(lastGame, dll_file);
                        }
                    }
                }
                catch (Exception) { }
                finally { Thread.Sleep(3000); }
            }
        }
    }
}
