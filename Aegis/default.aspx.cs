using Newtonsoft.Json;
using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Web;
using System.Web.UI;

namespace Aegis
{
    public partial class _default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public void lnkNewUser_click(object s, EventArgs e)
        {
            pnlDefault.Visible = false;
            pnlCreate.Visible = true;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://aegisservice20190412102455.azurewebsites.net/AegisService.svc/GetSecQuestions");
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            Stream stream = response.GetResponseStream();
            StreamReader reader = new StreamReader(stream);
            string str = reader.ReadToEnd();
            GetSecQuestionsResult sec = JsonConvert.DeserializeObject<GetSecQuestionsResult>(str);
            ddlSecurityQuestion1.DataSource = sec.secQuestions;
            ddlSecurityQuestion1.DataTextField = "SecQuestion";
            ddlSecurityQuestion1.DataValueField = "SecQuestionID";
            ddlSecurityQuestion1.DataBind();
            ddlSecurityQuestion2.DataSource = sec.secQuestions;
            ddlSecurityQuestion2.DataTextField = "SecQuestion";
            ddlSecurityQuestion2.DataValueField = "SecQuestionID";
            ddlSecurityQuestion2.DataBind();
        }
        public void btnValidate_click(object s, EventArgs e)
        {
            User user = new User();
            user.UserName = txtUName.Text;
            Encrypt en = new Encrypt();
            user.Password = en.Convert(txtPword.Text).ToString();
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create("http://aegisservice20190412102455.azurewebsites.net/AegisService.svc/ValidateUser/" + user.UserName + "/" + user.Password);
            HttpWebResponse response = (HttpWebResponse)req.GetResponse();
            using (var streamReader = new StreamReader(response.GetResponseStream()))
            {
                int res;
                string result = streamReader.ReadToEnd();
                result = result.Replace("\"", "");
                res = int.Parse(result);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    lblMsg.Text = "User not validated";
                    lblMsg.Visible = true;
                }
                else
                {
                    HttpCookie c = HttpContext.Current.Request.Cookies["AegisProject"];
                    if (c != null)
                    {
                        HttpContext.Current.Response.Cookies.Remove("AegisProject");
                        c.Expires = DateTime.Now.AddHours(-24);
                        c.Value = null;
                        HttpContext.Current.Response.SetCookie(c);
                    }
                    c = new HttpCookie("AegisProject");
                    c.Values.Add("userid", res.ToString());
                    c.Expires = DateTime.Now.AddHours(8);
                    Response.Cookies.Add(c);
                    Response.Redirect("~/ToDo.aspx");
                }
            }

        }
        public void btnCreate_click(object s, EventArgs e)
        {
            bool val = true;
            if(txtUNameCreate.Text == "")
            {
                txtUNameCreate.BackColor = Color.Red;
                val = false;
            }
            else
            {
                txtUNameCreate.BackColor = Color.White;
            }
            if(txtPwordCreate.Text == "")
            {
                txtPwordCreate.BackColor = Color.Red;
                val = false;
            }
            else
            {
                txtPwordCreate.BackColor = Color.White;
            }
            if(txtSecAnswer1.Text == "" || ddlSecurityQuestion1.SelectedValue == ddlSecurityQuestion2.SelectedValue)
            {
                txtSecAnswer1.BackColor = Color.Red;
                val = false;
            }
            else
            {
                txtSecAnswer1.BackColor = Color.White;
            }
            if(txtSecAnswer2.Text == "" || ddlSecurityQuestion1.SelectedValue == ddlSecurityQuestion2.SelectedValue)
            {
                txtSecAnswer2.BackColor = Color.Red;
                val = false;
            }
            else
            {
                txtSecAnswer2.BackColor = Color.White;
            }
            if (val)
            {
                User user = new User();
                Encrypt en = new Encrypt();
                user.UserName = txtUNameCreate.Text;
                user.SecAnswer1 = en.Convert(txtSecAnswer1.Text).ToString();
                user.SecAnswer2 = en.Convert(txtSecAnswer2.Text).ToString();
                user.Password = en.Convert(txtPwordCreate.Text).ToString();
                user.SecQuestion1 = Convert.ToInt32(ddlSecurityQuestion1.SelectedValue.ToString());
                user.SecQuestion2 = Convert.ToInt32(ddlSecurityQuestion2.SelectedValue.ToString());
                string obj = JsonConvert.SerializeObject(user);
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create("http://aegisservice20190412102455.azurewebsites.net/AegisService.svc/PostUser");
                req.Method = "POST";
                req.ContentType = "application/json";
                using (var streamWriter = new StreamWriter(req.GetRequestStream()))
                {
                    streamWriter.Write(obj);
                }
                HttpWebResponse response = (HttpWebResponse)req.GetResponse();
                using (var streamReader = new StreamReader(response.GetResponseStream()))
                {
                    int res;
                    string result = streamReader.ReadToEnd();
                    result = result.Replace("\"", "");
                    res = int.Parse(result);
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        lblMsg.Text = "User not inserted";
                        lblMsg.Visible = true;
                    }
                    else
                    {
                       
                        HttpCookie c = HttpContext.Current.Request.Cookies["AegisProject"];
                        if (c != null)
                        {
                            HttpContext.Current.Response.Cookies.Remove("AegisProject");
                            c.Expires = DateTime.Now.AddHours(-24);
                            c.Value = null;
                            HttpContext.Current.Response.SetCookie(c);
                        }
                        c = new HttpCookie("AegisProject");
                        c.Values.Add("userid", res.ToString());
                        c.Expires = DateTime.Now.AddHours(8);
                        Response.Cookies.Add(c);
                        Response.Redirect("~/ToDo.aspx");
                    }
                }
            }
        }
    }
}