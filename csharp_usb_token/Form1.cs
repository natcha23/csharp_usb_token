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

    public partial class Form1 : Form
    {
        public string tokenString { get; set; }
        public string usernameLogin { get; set; }
        public object loginform = new LoginForm();

        //public string api_encode = "http://localhost/tcsd_investigation/api/v1/getuser";
        private string api_encode = "https://investigation.tcsd-police.com/api/v1/getuser";

        //public string api_logout = "http://localhost/tcsd_investigation/api/v1/tokenlogout";
        private string api_logout = "https://investigation.tcsd-police.com/api/v1/tokenlogout";

        public Form1()
        {
            InitializeComponent();
        }

        private void CheckEnterKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                MessageBox.Show("!!!!");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(tokenString);
            //getTokenValue();
            //lblAccountName.Text = usernameLogin;

        }

        private void button5_Click(object sender, EventArgs e)
        {
            var localString = tokenString;

            try
            {
                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection();
                    data["token"] = localString;

                    var response = wb.UploadValues(api_logout, "POST", data);
                    var jsonString = Encoding.Default.GetString(response);

                    //MessageBox.Show(jsonString);
                    var results = JsonConvert.DeserializeObject<dynamic>(jsonString);
                    usernameLogin = results.PrefixTitle + " " + results.FName + " " + results.LName;

                    //MessageBox.Show("json: " + results.FName);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            
            Environment.Exit(1);
            //if (MessageBox.Show("Are you sure want to exit?", "The Title",
            //    MessageBoxButtons.YesNo, MessageBoxIcon.Question,
            //    MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
            //{
            //    //TODO: Stuff
            //    Environment.Exit(1);
            //}


            //if (MessageBox.Show("Are you sure want to exit?",
            //           "My First Application",
            //            MessageBoxButtons.OKCancel,
            //            MessageBoxIcon.Information) == DialogResult.Ok)
            //{
            //    // this.Close(); // you don't need that, it's already closing
            //    Environment.Exit(1);
            //}
            //Environment.Exit(1);
            //this.Close();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            getTokenValue();
            lblAccountName.Text = usernameLogin;
        }

        private void logout()
        {
            //MessageBox.Show(tokenString);
            var localString = tokenString;

            try
            {

                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection();
                    data["token"] = localString;

                    var response = wb.UploadValues(api_logout, "POST", data);
                    var jsonString = Encoding.Default.GetString(response);

                    //MessageBox.Show(jsonString);
                    var results = JsonConvert.DeserializeObject<dynamic>(jsonString);
                    usernameLogin = results.PrefixTitle + " " + results.FName + " " + results.LName;

                    //MessageBox.Show("json: " + results.FName);
                }



            }
            catch (Exception ex)
            {
                MessageBox.Show("Logout Error: " + ex.Message);
            }
        }
        private void getTokenValue()
        {
            //MessageBox.Show(tokenString);
            var localString = tokenString;
            

            try
            {

                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection();
                    data["token"] = localString;

                    var response = wb.UploadValues(api_encode, "POST", data);
                    var jsonString = Encoding.Default.GetString(response);

                    //MessageBox.Show(jsonString);
                    var results = JsonConvert.DeserializeObject<dynamic>(jsonString);
                    usernameLogin = results.PrefixTitle + " " + results.FName + " " + results.LName;

                    MessageBox.Show("json: "+results.FName);
                }



            }
            catch (Exception ex)
            {
                MessageBox.Show("Token Error: " + ex.Message);
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            
        }

    }
}
