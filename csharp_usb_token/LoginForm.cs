using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.Specialized;

using Newtonsoft.Json.Linq;

namespace TestGUI
{
    delegate string MyDelegates(string s);
    public delegate void SendControl(String text); //ประกาศ Delegate ใช้สำหรับส่งไปฟอร์ม 2

    public partial class LoginForm : Form
    {

        //public string api_login = "http://localhost/tcsd_investigation/api/v1/tokenlogin";
        public string api_login = "https://investigation.tcsd-police.com/api/v1/tokenlogin";
        public string responseString { get; set; }

        public LoginForm()
        {
            InitializeComponent();
            //this.IsMdiContainer = true;
        }

        public static void DelegateFunction(int i)
        {
            System.Console.WriteLine("Called by delegate with number: {0}.", i);
        }

        public class Account
        {
            public string Email { get; set; }
            public bool Active { get; set; }
            public DateTime CreatedDate { get; set; }
            public IList<string> Roles { get; set; }
            public string responseString { get; set; }

        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            //string username;
            //string password;

            //username = UserNameBox1.Text;
            //password = PasswordBox1.Text;


            // Validation if there is inputted text or not
            if (UserNameBox1.Text == "")
            {
                MessageBox.Show("Please enter a valid user name!");
                UserNameBox1.Focus();
            }
            else if (PasswordBox1.Text == "")
            {
                MessageBox.Show("Please enter a valid password!");
                PasswordBox1.Focus();
            }
            else
            {
                Login();
                //if ( CheckForInternetConnection() )
                //{
                //    LoginData();
                //}
                //else
                //{
                //    MessageBox.Show("Can not connect internet");
                //}
                //__getTokenValue();
            }

        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Login()
        {

            try
            {
                string user = UserNameBox1.Text;
                string pass = PasswordBox1.Text;

                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection();
                    data["username"] = user;
                    data["password"] = pass;

                    var response = wb.UploadValues(api_login, "POST", data);
                    responseString = Encoding.Default.GetString(response);

                    //MessageBox.Show(responseString);

                    if (responseString != "[]")
                    {
                        //SendControl del = new SendControl(frm1.funData);
                        //del(responseString);
                        //MyDelegates de = frm1.returnString;
                        //de(responseString);

                        var frm = new Form1();
                        MessageBox.Show("Login Successful!");
                        frm.tokenString = responseString;

                        //frm.MdiParent = this;
                        this.Hide();
                        frm.Show();

                    }
                    else
                    {
                        MessageBox.Show("Login Fail!!");
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Login Error: " + ex.Message);
            }

            //return responseString;
        }

        private void __getTokenValue()
        {
            //MessageBox.Show(responseString);
            var localString = responseString;
            // var encode_url = "http://localhost/tcsd_investigation/api/v1/gettoken";
            var encode_url = "https://investigation.tcsd-police.com/api/v1/getuser";

            try
            {

                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection();
                    data["token"] = localString;

                    var response = wb.UploadValues(encode_url, "POST", data);
                    var jsonString = Encoding.Default.GetString(response);

                    MessageBox.Show(jsonString);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void PasswordBox1_TextChanged(object sender, EventArgs e)
        {
            PasswordBox1.PasswordChar = '\u25CF';
        }

        public static bool CheckForInternetConnection()
        {
            MessageBox.Show("CheckForInternetConnection: " );
            try
            {
                using (var client = new WebClient())
                {
                    MessageBox.Show(client.BaseAddress);
                    using (var stream = client.OpenRead("www.sanook2.com"))
                    {
                        return true;
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }
    }



}
