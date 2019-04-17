using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Aegis
{
    public partial class ToDo : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HttpCookie c = Request.Cookies["AegisProject"];
            if (!Page.IsPostBack)
            {
                if (c == null)
                {
                    Response.Redirect("default.aspx");
                }
                else
                {
                    lblUserID.Text = c.Values["userid"].ToString();
                }
            }       
            else
            {
                if (c != null)
                {
                    lblUserID.Text = c.Values["userid"].ToString();
                }
                else
                {
                    Response.Redirect("~/default.aspx");
                }
            }
        }
        public void lnkAdd_click(object s, EventArgs e)
        {
            pnlOptions.Visible = false;
            pnlAdd.Visible = true;
            pnlEdit.Visible = false;
            pnlComplete.Visible = false;
            pnlDisable.Visible = false;
            pnlGrid.Visible = false;
        }
        public void lnkEdit_click(object s, EventArgs e)
        {
            pnlOptions.Visible = false;
            pnlAdd.Visible = false;
            pnlEdit.Visible = false;
            pnlComplete.Visible = false;
            pnlDisable.Visible = false;
            pnlGrid.Visible = true;
            lblSelection.Text = "Edit";
            FillGrid(Convert.ToInt32(lblUserID.Text));
        }
        public void lnkComplete_click(object s, EventArgs e)
        {
            pnlOptions.Visible = false;
            pnlAdd.Visible = false;
            pnlEdit.Visible = false;
            pnlComplete.Visible = false;
            pnlDisable.Visible = false;
            pnlGrid.Visible = true;
            lblSelection.Text = "Complete";
            FillGrid(Convert.ToInt32(lblUserID.Text));
        }
        public void lnkDisable_click(object s, EventArgs e)
        {
            pnlOptions.Visible = false;
            pnlAdd.Visible = false;
            pnlEdit.Visible = false;
            pnlComplete.Visible = false;
            pnlDisable.Visible = false;
            pnlGrid.Visible = true;
            lblSelection.Text = "Disable";
            FillGrid(Convert.ToInt32(lblUserID.Text));
        }
        public void lnkLogOut_click(object s, EventArgs e)
        {
            //remove cookie from authentication
            HttpCookie c = HttpContext.Current.Request.Cookies["AegisProject"];
            HttpContext.Current.Response.Cookies.Remove("AegisProject");
            c.Expires = DateTime.Now.AddHours(-24);
            c.Value = null;
            HttpContext.Current.Response.SetCookie(c);
            Response.Redirect("~/default.aspx");
        }
        public void btnSubmitAdd_click(object s, EventArgs e)
        {
            if(ValidateForm(pnlAdd))
            {
                //Insert new record
                ToDoClass todo = new ToDoClass();
                todo.Title = txtTitleAdd.Text;
                todo.Description = txtDescriptionAdd.Text;
                todo.StartDate = Convert.ToDateTime(txtStartAdd.Text);
                todo.EndDate = Convert.ToDateTime(txtEndAdd.Text);
                todo.CreatedBy = 1; //use session id from cookie
                todo.Disabled = false;
                todo.Completed = false;
                string obj = JsonConvert.SerializeObject(todo);
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create("http://aegisservice20190412102455.azurewebsites.net/AegisService.svc/PostToDo");
                req.Method = "POST";
                req.ContentType = "application/json";
                req.ContentLength = obj.Length;
                using (var streamWriter = new StreamWriter(req.GetRequestStream()))
                {
                    streamWriter.Write(obj);
                }
                HttpWebResponse response = (HttpWebResponse)req.GetResponse();
                using (var streamReader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    string result = streamReader.ReadToEnd();
                    int res = Convert.ToInt32(result);
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        lblMsg.Text = "ToDo not inserted";
                    }
                    else
                    {
                        ClearForm(pnlAdd);
                        FillGrid(Convert.ToInt32(lblUserID.Text));
                        pnlGrid.Visible = true;
                        pnlAdd.Visible = false;
                    }
                }                
            }
        }
        public void btnSubmitEdit_click(object s, EventArgs e)
        {
            if (ValidateForm(pnlEdit))
            {
                //Insert new record
                ToDoClass todo = new ToDoClass();
                todo.ToDoID = Convert.ToInt32(lblSelectedID.Text);
                todo.Title = txtTitleEdit.Text;
                todo.Description = txtDescriptionEdit.Text;
                todo.StartDate = Convert.ToDateTime(txtStartEdit.Text);
                todo.EndDate = Convert.ToDateTime(txtEndEdit.Text);
                todo.UpdatedBy = 1; //use session id from cookie
                todo.Completed = Convert.ToBoolean(rblCompletedEdit.SelectedValue);
                string obj = JsonConvert.SerializeObject(todo);
                //aegisservice20190412102455.azurewebsites.net
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create("http://localhost:50364/AegisService.svc/PutToDo");
                req.Method = "PUT";
                req.ContentType = "application/json";
                req.ContentLength = obj.Length;
                using (var streamWriter = new StreamWriter(req.GetRequestStream()))
                {
                    streamWriter.Write(obj);
                }
                HttpWebResponse response = (HttpWebResponse)req.GetResponse();
                using (var streamReader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    string result = streamReader.ReadToEnd();
                    int res = Convert.ToInt32(result);
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        ClearForm(pnlEdit);
                        FillGrid(Convert.ToInt32(lblUserID.Text));
                        pnlGrid.Visible = true;
                        pnlEdit.Visible = false;
                    }
                    else
                    {
                        lblMsg.Text = "ToDo not inserted";
                        lblMsg.Visible = true;
                    }
                }
            }
        }
        public void btnSubmitComplete_click(object s, EventArgs e)
        {
            if (ValidateForm(pnlComplete))
            {
                ToDoClass todo = new ToDoClass();
                todo.ToDoID = Convert.ToInt32(lblSelectedID.Text);
                todo.Completed = Convert.ToBoolean(rblCompleteConfirm.SelectedValue);
                todo.UpdatedBy = Convert.ToInt32(lblUserID.Text);
                string obj = JsonConvert.SerializeObject(todo);
                //aegisservice20190412102455.azurewebsites.net
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create("http://localhost:50364/AegisService.svc/CompleteToDo");
                req.Method = "PUT";
                req.ContentType = "application/json";
                req.ContentLength = obj.Length;
                using (var streamWriter = new StreamWriter(req.GetRequestStream()))
                {
                    streamWriter.Write(obj);
                }
                HttpWebResponse response = (HttpWebResponse)req.GetResponse();
                using (var streamReader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    string result = streamReader.ReadToEnd();
                    int res = Convert.ToInt32(result);
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        ClearForm(pnlComplete);
                        FillGrid(Convert.ToInt32(lblUserID.Text));
                        pnlGrid.Visible = true;
                        pnlComplete.Visible = false;
                    }
                    else
                    {
                        lblMsg.Text = "ToDo not update";
                        lblMsg.Visible = true;
                    }
                }
            }
        }
        public void btnSubmitDisable_click(object s, EventArgs e)
        {
            if (ValidateForm(pnlDisable))
            {
                ToDoClass todo = new ToDoClass();
                todo.ToDoID = Convert.ToInt32(lblSelectedID.Text);
                todo.Disabled = Convert.ToBoolean(rblConfirmDisable.SelectedValue);
                string obj = JsonConvert.SerializeObject(todo);
                //aegisservice20190412102455.azurewebsites.net
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create("http://localhost:50364/AegisService.svc/DisableToDo");
                req.Method = "PUT";
                req.ContentType = "application/json";
                req.ContentLength = obj.Length;
                using (var streamWriter = new StreamWriter(req.GetRequestStream()))
                {
                    streamWriter.Write(obj);
                }
                HttpWebResponse response = (HttpWebResponse)req.GetResponse();
                using (var streamReader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    string result = streamReader.ReadToEnd();
                    int res = Convert.ToInt32(result);
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        ClearForm(pnlDisable);
                        FillGrid(Convert.ToInt32(lblUserID.Text));
                        pnlGrid.Visible = true;
                        pnlDisable.Visible = false;
                    }
                    else
                    {
                        lblMsg.Text = "ToDo not update";
                        lblMsg.Visible = true;
                    }
                }
            }
        }
        public void btnCancelAdd_click(object s, EventArgs e)
        {
            ClearForm(pnlAdd);
            pnlAdd.Visible = false;
            pnlOptions.Visible = true;
        }
        public void btnCancelEdit_click(object s, EventArgs e)
        {
            ClearForm(pnlEdit);
            pnlEdit.Visible = false;
            pnlOptions.Visible = true;
        }
        public void btnCancelComplete_click(object s, EventArgs e)
        {
            ClearForm(pnlComplete);
            pnlComplete.Visible = false;
            pnlOptions.Visible = true;
        }
        public void btnCancelDisable_click(object s, EventArgs e)
        {
            ClearForm(pnlDisable);
            pnlDisable.Visible = false;
            pnlOptions.Visible = true;
        }
        public void btnCancelGrid_click(object s, EventArgs e)
        {
            lblSelectedID.Text = "";
            lblSelection.Text = "";
            lblUserID.Text = "";
            pnlGrid.Visible = false;
            pnlOptions.Visible = true;
        }
        public void lnkGrid_click(object s, CommandEventArgs e)
        {
            pnlGrid.Visible = false;
            lblSelectedID.Text = e.CommandArgument.ToString();
            //Load data for selected item
            if (lblSelection.Text =="Edit")
            {
                pnlEdit.Visible = true;
                ToDoClass todo = GetToDo(Convert.ToInt32(lblSelectedID.Text));
                txtDescriptionEdit.Text = todo.Description;
                txtTitleEdit.Text = todo.Title;
                txtStartEdit.Text = todo.StartDate.ToString("yyyy-MM-dd");
                txtEndEdit.Text = todo.EndDate.ToString("yyyy-MM-dd");
                rblCompletedEdit.SelectedValue = todo.Completed.ToString();
                lblDisableEdit.Text = todo.Disabled.ToString();
            }
            else if(lblSelection.Text == "Complete")
            {
                pnlComplete.Visible = true;
                ToDoClass todo = GetToDo(Convert.ToInt32(lblSelectedID.Text));
                lblTitleComplete.Text = todo.Title;
                lblDescriptionComplete.Text = todo.Description;
                lblStartDateComplete.Text = todo.StartDate.ToString();
                lblEndDateComplete.Text = todo.EndDate.ToString();
                //rblCompleteConfirm.SelectedValue = todo.Completed.ToString();
            }
            else if(lblSelection.Text == "Disable")
            {
                pnlDisable.Visible = true;
                ToDoClass todo = GetToDo(Convert.ToInt32(lblSelectedID.Text));
                lblTitleDisable.Text = todo.Title;
                lblDescriptionDisable.Text = todo.Description;
                lblStartDateDisable.Text = todo.StartDate.ToString();
                lblEndDateDisable.Text = todo.EndDate.ToString();
                //rblConfirmDisable.SelectedValue = todo.Completed.ToString();
            }
        }
        public void ClearForm(Control c)
        {
            lblMsg.Text = "";
            lblSelection.Text = "";
            foreach(Control ctrl in c.Controls)
            {
                if (ctrl is TextBox)
                {
                    TextBox tx = ctrl as TextBox;
                    tx.Text = "";
                }
                else if (ctrl is RadioButtonList)
                {
                    RadioButtonList rbl = ctrl as RadioButtonList;
                    rbl.SelectedIndex = -1;
                }
                else if (ctrl is Label)
                {
                    Label lbl = ctrl as Label;
                    if (lbl.ID == "lblUserID")
                    {
                        //Do Nothing
                    }
                    else
                    {
                        lbl.Text = "";
                    }
                }
            }
        }
        public void FillGrid(int ID)
        {
            //Fill Grid
            //aegisservice20190412102455.azurewebsites.net
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://localhost:50364/AegisService.svc/GetToDos/" + ID.ToString());
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            Stream stream = response.GetResponseStream();
            StreamReader reader = new StreamReader(stream);
            string str = reader.ReadToEnd();
            GetToDosResult todo = JsonConvert.DeserializeObject<GetToDosResult>(str);
            List<ToDoClass> t = todo.todo;
            grdTODo.DataSource = t;
            grdTODo.DataBind();
        }
        public ToDoClass GetToDo(int ToDoID)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://localhost:50364/AegisService.svc/GetToDo/" + ToDoID);
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            Stream stream = response.GetResponseStream();
            StreamReader reader = new StreamReader(stream);
            string str = reader.ReadToEnd();
            GetToDoResult todo = JsonConvert.DeserializeObject<GetToDoResult>(str);
            ToDoClass t = todo.todo;
            return t;
        }
        public bool ValidateForm(Control c)
        {
            bool ret = true;
            foreach(Control ctrl in c.Controls)
            {
                if (ctrl is TextBox)
                {
                    TextBox tx = ctrl as TextBox;
                    if(tx.Text == "")
                    {
                        tx.BackColor = Color.Red;
                        ret = false;
                    }
                }
                else if (ctrl is RadioButtonList)
                {
                    RadioButtonList rbl = ctrl as RadioButtonList;
                    if(rbl.SelectedIndex == -1)
                    {
                        rbl.ForeColor = Color.Red;
                        ret = false;
                    }
                }
            }
            return ret;
        }
    }
}