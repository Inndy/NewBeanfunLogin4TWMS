using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace NewBeanfunLogin
{
    public partial class Form_Login : Form
    {
        public MapleBeanfun bf = null;

        public Form_Login()
        {
            InitializeComponent();
        }
        /*
        private void button5_Click(object sender, EventArgs e)
        {
            ac_list = bf.GetAccountList();

            listAccount.Items.Clear();
            txtOutput.Clear();

            foreach (BeanfunGameAccountData ac in ac_list)
            {
                listAccount.Items.Add(ac.Account + ", " + ac.Number + ", " + ac.Name);
                txtOutput.Text += ac.Account + ", " + ac.Number + ", " + ac.Name + Environment.NewLine;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (listAccount.SelectedIndex > -1)
                txtOutput.Text = bf.QueryOTP(ac_list[listAccount.SelectedIndex]);
        }*/

        private void Form_Bot_Load(object sender, EventArgs e)
        {
            bgwk.RunWorkerAsync(new string[] { "init" });
        }

        private void btnRecaptcha_Click(object sender, EventArgs e)
        {
            btnRecaptcha.Enabled = false;
            bgwk.RunWorkerAsync(new string[] { "recaptcha" });
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            btnLogin.Enabled = false;
            bgwk.RunWorkerAsync(new string[] { "login", txtID.Text, txtPwd.Text, txtCaptcha.Text });
        }

        private void bgwk_DoWork(object sender, DoWorkEventArgs e)
        {
            string[] arg = e.Argument as string[];
            if (arg == null) return;
            switch (arg[0])
            {
                case "init":
                    bf = new MapleBeanfun();
                    bool result = bf.Init();
                    e.Result = new Result(arg[0], !result, bf.GetCaptcha());
                    break;
                case "recaptcha":
                    Image captcha = bf.GetCaptcha();
                    e.Result = new Result(arg[0], captcha == null, captcha);
                    break;
                case "login":
                    try
                    {
                        bf.Login(arg[1], arg[2], arg[3]);
                        e.Result = new Result("login", false);
                    }
                    catch (BeanfunLoginFailedException ex)
                    {
                        e.Result = new Result("login", true, ex.Message);
                    }
                    break;
                case "list":
                    List<BeanfunGameAccountData> list = bf.GetAccountList();
                    e.Result = new Result(arg[0], list == null, list);
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
                case "init":
                    if (result.Error)
                    {
                        Err("初始化失敗");
                        this.Close();
                    }
                    else if (result.Argumment == null)
                    {
                        Err("無法取得驗證碼");
                    }
                    else
                    {
                        btnLogin.Enabled = true;
                        btnRecaptcha.Enabled = true;
                        pbCaptcha.Image = result.Argumment as Image;
                    }
                    break;
                case "recaptcha":
                    btnRecaptcha.Enabled = true;
                    if (result.Argumment == null)
                        Err("無法取得驗證碼");
                    else
                    {
                        pbCaptcha.Image = result.Argumment as Image;
                        txtCaptcha.Clear();
                        txtCaptcha.Focus();
                    }
                    break;
                case "login":
                    if (result.Error)
                    {
                        btnRecaptcha_Click(null, null);
                        Err("登入失敗\n錯誤訊息：" + result.Argumment);
                        btnLogin.Enabled = true;
                    }
                    else
                    {
                        bgwk.RunWorkerAsync(new string[] { "list" });
                    }
                    break;
                case "list":
                    if (result.Error)
                    {
                        Err("無法取得帳號列表");
                        Application.Exit();
                    }
                    List<BeanfunGameAccountData> list = (List<BeanfunGameAccountData>)result.Argumment;
                    Form_Account frmAcc = new Form_Account(bf);
                    foreach (BeanfunGameAccountData data in list)
                    {
                        ListViewItem lvi = frmAcc.lstAccount.Items.Add(data.Name);
                        lvi.SubItems.Add(data.Account);
                        lvi.SubItems.Add(data.Number);
                    }
                    frmAcc.Show();
                    this.Hide();
                    break;
            }
        }

        private void Err(string Str)
        {
            MessageBox.Show(Str, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
                btnLogin_Click(sender, null);
        }
    }

    public class Result
    {
        public string Work { get; private set; }
        public bool Error { get; private set; }
        public object Argumment { get; private set; }

        public Result(string Work, bool Error)
        {
            this.Work = Work;
            this.Error = Error;
            this.Argumment = null;
        }

        public Result(string Work, bool Error, object Argument)
        {
            this.Work = Work;
            this.Error = Error;
            this.Argumment = Argument;
        }
    }
}
