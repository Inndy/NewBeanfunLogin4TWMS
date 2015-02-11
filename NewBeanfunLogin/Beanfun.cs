using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace NewBeanfunLogin
{
    public class MapleBeanfun
    {
        private CookieContainer cc;
        private SpWebClient spwc;
        private string login_skey = null, login_captchaUrl, login_captchaID, login_VIEWSTATE, login_EVENTVALIDATION, login_url;
        private string webtoken;
        private string _;

        public MapleBeanfun()
        {
            this.cc = new CookieContainer();
            this.spwc = new SpWebClient(cc);
            this.spwc.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/28.0.1500.72 Safari/537.36");
            this._ = "NewBeanfun By Inndy...";
        }

        public bool Init()
        {
            try
            {
                string ret = spwc.DownloadString("https://tw.beanfun.com/beanfun_block/bflogin/default.aspx?service=999999_T0", Encoding.UTF8);
                string uri = spwc.ResponseUri.ToString();
                Regex regLogin = new Regex(@"^https?://tw\.newlogin\.beanfun\.com/checkin_step2\.aspx\?skey=(\w{20})(&display)?");

                if (!regLogin.IsMatch(uri)) return false;

                login_skey = regLogin.Match(uri).Groups[1].Value;
                login_url = "https://tw.newlogin.beanfun.com/login/id-pass_form.aspx?skey=" + login_skey;
                ret = spwc.DownloadString(login_url, Encoding.UTF8);
                Regex regCaptchaUrl = new Regex(@"samplecaptcha\x22 value=\x22(\w{32})\x22");
                Regex regViewstate = new Regex(@"\x22__VIEWSTATE\x22 value=\x22([^\x22]+)\x22");
                Regex regEventvalidation = new Regex(@"\x22__EVENTVALIDATION\x22 value=\x22([^\x22]+)\x22");

                login_captchaID = regCaptchaUrl.Match(ret).Groups[1].Value;
                login_captchaUrl = "https://tw.newlogin.beanfun.com/login/BotDetectCaptcha.ashx?get=image&c=c_login_idpass_form_samplecaptcha&t=" + login_captchaID;
                login_VIEWSTATE = regViewstate.Match(ret).Groups[1].Value;
                login_EVENTVALIDATION = regEventvalidation.Match(ret).Groups[1].Value;

                return true;
            }
            catch (NotSupportedException)
            {
                throw new BeanfunIsBusyException("正在處理其他的事情");
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Image GetCaptcha()
        {
            try
            {
                byte[] buffer = spwc.DownloadData(login_captchaUrl + "&d=" + getTime().ToString());
                return Image.FromStream(new MemoryStream(buffer));
            }
            catch (NotSupportedException) { throw new BeanfunIsBusyException("正在處理其他的事情"); }
            catch (Exception) { return null; }
        }

        public void Login(string User, string Pass, string Captcha)
        {
            try
            {
                NameValueCollection data = new NameValueCollection();
                data.Add("__EVENTTARGET", "");
                data.Add("__EVENTARGUMENT", "");
                data.Add("__VIEWSTATE", login_VIEWSTATE);
                data.Add("__EVENTVALIDATION", login_EVENTVALIDATION);
                data.Add("t_AccountID", User);
                data.Add("t_Password", Pass);
                data.Add("CodeTextBox", Captcha);
                data.Add("btn_login.x", "46");
                data.Add("btn_login.y", "31");
                data.Add("LBD_VCID_c_login_idpass_form_samplecaptcha", login_captchaID);

                string ret = Encoding.UTF8.GetString(spwc.UploadValues(login_url, data));

                if (spwc.ResponseUri.ToString().Contains("id-pass_form.aspx"))
                {
                    string error_message = "";
                    try
                    {
                        Regex regErrorMessage = new Regex(@"MsgBox.Show\('([^']+)'\)");
                        error_message = regErrorMessage.Match(ret).Groups[1].Value;
                    }
                    catch (Exception) { throw new BeanfunLoginFailedException(); }

                    throw new BeanfunLoginFailedException(error_message);
                }

                Regex regAuthkey = new Regex(@"AuthKey\.value = \x22(\w+)\x22");
                Regex regSessionkey = new Regex(@"SessionKey\.value = \x22(\w+)\x22");

                data.Clear();
                try
                {
                    data.Add("SessionKey", regSessionkey.Match(ret).Groups[1].Value);
                    data.Add("AuthKey", regAuthkey.Match(ret).Groups[1].Value);
                }
                catch (Exception) { throw new BeanfunLoginFailedException(); }

                ret = Encoding.UTF8.GetString(spwc.UploadValues("https://tw.beanfun.com/beanfun_block/bflogin/return.aspx", data));

                Regex regSuccess = new Regex(@"https?://tw\.beanfun\.com/default\.aspx");

                if (!regSuccess.IsMatch(spwc.ResponseUri.ToString()))
                    throw new BeanfunLoginFailedException();

                /*
                 * In Cookie
                 *     bfWebToken
                 *     GET /beanfun_block/auth.aspx?channel=game_zone&page_and_query=game_start.aspx%3Fservice_code_and_region%3D610074_T9&web_token=3eb8306996a34ed7b8556bde31d0b0a3 HTTP/1.1
                 */

                CookieCollection cookies = cc.GetCookies(new Uri("http://tw.beanfun.com"));
                foreach (Cookie cookie in cookies)
                    if (cookie.Name == "bfWebToken")
                        this.webtoken = cookie.Value;
            }
            catch (NotSupportedException)
            {
                throw new BeanfunIsBusyException("正在處理其他的事情");
            }
        }

        public List<BeanfunGameAccountData> GetAccountList()
        {
            try
            {
                List<BeanfunGameAccountData> list = new List<BeanfunGameAccountData>();

                string ret = spwc.DownloadString("http://tw.beanfun.com/beanfun_block/auth.aspx?channel=game_zone&page_and_query=game_start.aspx%3Fservice_code_and_region%3D610074_T9&web_token=" + this.webtoken, Encoding.UTF8);
                try
                {
                    int start = ret.IndexOf("id=\"ulServiceAccountList\"");
                    start = ret.IndexOf("<li", start);
                    int end = ret.IndexOf("</ul>", start);
                    string[] accs = ret.Substring(start, end - start).Split(new string[] { "</li>" }, StringSplitOptions.RemoveEmptyEntries);
                    Regex regAccount = new Regex(@"<div id=\x22(\w+)\x22 sn=\x22(\d+)\x22 name=\x22([^\x22]+)\x22 inherited=\x22false\x22 visible=\x221\x22 class=\x22Account\x22 title=\x22(\S*)\x22");
                    foreach (string acc in accs)
                        if (regAccount.IsMatch(acc))
                        {
                            Match m = regAccount.Match(acc);
                            if (m.Groups[4].Value == "\u7de8\u8f2f\u5e33\u6236")
                                list.Add(new BeanfunGameAccountData(m.Groups[1].Value, m.Groups[2].Value, WebUtility.HtmlDecode(m.Groups[3].Value)));
                            else
                                break;
                        }
                }
                catch (Exception)
                {
                    return null;
                }

                return list;
            }
            catch (NotSupportedException)
            {
                throw new BeanfunIsBusyException("正在處理其他的事情");
            }
        }

        public string QueryOTP(BeanfunGameAccountData acc)
        {
            try
            {
                string ret = spwc.DownloadString("http://tw.beanfun.com/beanfun_block/game_zone/game_start_step2.aspx?service_code=610074&service_region=T9&sotp=" + acc.Number + "&dt=" + dt(), Encoding.UTF8);

                Regex regCT = new Regex(@"ServiceAccountCreateTime: \x22([^\x22]+)\x22");
                string ct = regCT.Match(ret).Groups[1].Value;
                Regex regKey = new Regex(@"GetResultByLongPolling&key=([^\x22]+)\x22");
                string key = regKey.Match(ret).Groups[1].Value;

                Regex regSecret = new Regex(@"var m_strSecretCode = '(\w+)';");
                string secret = regSecret.Match(spwc.DownloadString("http://tw.newlogin.beanfun.com/generic_handlers/get_cookies.ashx")).Groups[1].Value;


                NameValueCollection data = new NameValueCollection();

                data.Add("service_code", "610074");
                data.Add("service_region", "T9");
                data.Add("service_account_id", acc.Account);
                data.Add("service_sotp", acc.Number);
                data.Add("service_display_name", acc.Name);
                data.Add("service_create_time", ct);

                spwc.UploadValues("http://tw.beanfun.com/beanfun_block/generic_handlers/record_service_start.ashx", data);

                ret = spwc.DownloadString("http://tw.beanfun.com/generic_handlers/get_result.ashx?meth=GetResultByLongPolling&key=" + key + "&_=" + getUTCTime().ToString(), Encoding.UTF8);

                ret = spwc.DownloadString(
                    "http://tw.beanfun.com/beanfun_block/generic_handlers/get_webstart_otp.ashx" +
                    "?SN=" + key +
                    "&WebToken=" + this.webtoken +
                    "&SecretCode=" + secret +
                    "&ppppp=FE40250C435D81475BF8F8009348B2D7F56A5FFB163A12170AD615BBA534B932" +
                    "&ServiceCode=610074" +
                    "&ServiceRegion=T9" +
                    "&ServiceAccount=" + acc.Account +
                    "&CreateTime=" + ct.Replace(" ", "%20")
                );

                if (ret[0] == '1')
                {
                    ret = ret.Substring(2);
                    return WCDESComp.DecryptString(ret.Substring(0, 8), ret.Substring(8, 32));
                }
                return null;
            }
            catch (NotSupportedException)
            {
                throw new BeanfunIsBusyException("正在處理其他的事情");
            }
        }

        public bool Ping()
        {
            try
            {
                string ret = spwc.DownloadString("http://tw.beanfun.com/beanfun_block/generic_handlers/echo_token.ashx?webtoken=1");
                return ret.Contains("ResultCode:1");
            }
            catch (NotSupportedException)
            {
                throw new BeanfunIsBusyException("正在處理其他的事情");
            }
        }

        private static long getTime()
        {
            return getUTCTime() - 28800000;
        }

        private static long getUTCTime()
        {
            return DateTime.UtcNow.Ticks / 10000 - 62135596800000;
        }

        private static string dt()
        {
            return (DateTime.Now.Year - 1900).ToString() +
                   (DateTime.Now.Month - 1).ToString() +
                   (DateTime.Now.Day).ToString() +
                   (DateTime.Now.Hour).ToString() +
                   (DateTime.Now.Minute).ToString() +
                   (DateTime.Now.Second).ToString() +
                   (DateTime.Now.Millisecond % 100).ToString().PadLeft(2, '0');
        }
    }

    public class BeanfunLoginFailedException : Exception
    {
        public BeanfunLoginFailedException() : base() { }
        public BeanfunLoginFailedException(string message) : base(message) { }
    }

    public class BeanfunIsBusyException : Exception
    {
        public BeanfunIsBusyException() : base() { }
        public BeanfunIsBusyException(string message) : base(message) { }
    }

    public class BeanfunGameAccountData
    {
        public string Account { get; private set; }
        public string Number { get; private set; }
        public string Name { get; private set; }

        public BeanfunGameAccountData(string Account, string Number, string Name)
        {
            this.Account = Account;
            this.Number = Number;
            this.Name = Name;
        }
    }
}

