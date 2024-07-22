using AppBlock;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class formcreation : System.Web.UI.Page
    {
        string con = ConfigurationManager.ConnectionStrings["formcreation"].ToString();
        string sql = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void btnSumbit_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            sql = "insert into form(firstname,lastname,address,date) values('" + firstname.Value + "','" + lastname.Value + "','" + address.Value + "',getDate())";
            ds = SqlHelper.ExecuteDataset(con, CommandType.Text, sql);
            firstname.Value = "";
            lastname.Value = "";
            address.Value = "";
            NoRecords_text.Visible = false;
            filldata();
            btndownload.Visible = true;
            cleardata.Visible = true;

        }

        protected void view_Click(object sender, EventArgs e)
        {
            SqlConnection sc = new SqlConnection(con);
            DataTable dtview = new DataTable();
            sql = "select * from form";
            sc.Open();
            SqlDataAdapter da = new SqlDataAdapter(sql, sc);
            da.Fill(dtview);
            sc.Close();

            gridview1.DataSource = dtview;
            gridview1.DataBind();
            int rowCount = dtview.Rows.Count;
            if (rowCount>0)
            {
                cleardata.Visible = true;
                NoRecords_text.Visible = false;
                btndownload.Visible = true;
            }
            else
            {
                cleardata.Visible = false;
                NoRecords_text.Visible = true;
                btndownload.Visible = false;
            }

        }
        public void filldata()
        {
            SqlConnection sc = new SqlConnection(con);
            DataTable dtview = new DataTable();
            sql = "select * from form";
            sc.Open();
            SqlDataAdapter da = new SqlDataAdapter(sql, sc);
            da.Fill(dtview);
            sc.Close();

            gridview1.DataSource = dtview;
            gridview1.DataBind();

        }

        protected void cleardata_Click(object sender, EventArgs e)
        {
            SqlConnection sc = new SqlConnection(con);
            DataSet ds = new DataSet();
            sql = "truncate table form";
            ds = SqlHelper.ExecuteDataset(con, CommandType.Text, sql);
            //filldata();
            cleardata.Visible = false;
            btndownload.Visible = false;
            NoRecords_text.Visible = true;
            gridview1.Visible = false; 


        }

        protected void reset_Click(object sender, EventArgs e)
        {
            firstname.Value = "";
            lastname.Value = "";
            address.Value = "";
        }

        protected void gridview1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            
            Label label_name =(Label) gridview1.Rows[e.RowIndex].FindControl("col1") as Label;
            SqlConnection sc = new SqlConnection(con);
            DataTable dtview = new DataTable();
            sql = "delete from form where firstname='" + label_name.Text + "'";
            SqlHelper.ExecuteNonQuery(sc, CommandType.Text, sql);
            filldata();
            int rowCount = dtview.Rows.Count;
            if (rowCount == 0)
            {
                NoRecords_text.Visible = true;
                cleardata.Visible = false;
                btndownload.Visible = false;
            }
        }

        protected void gridview1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gridview1.EditIndex = e.NewEditIndex;
            {
                filldata();
            }
            
           

        }

        protected void gridview1_RowUpdating(object sender, GridViewUpdateEventArgs e)
            {
            TextBox user = (TextBox)(gridview1.Rows[e.RowIndex].FindControl("editcol10") as TextBox); 
            Label label_name = (Label)gridview1.Rows[e.RowIndex].FindControl("user_id") as Label;
            Label update = (Label)(gridview1.Rows[e.RowIndex].FindControl("user_id") as Label);
            TextBox fname =(TextBox)(gridview1.Rows[e.RowIndex].FindControl("editcol1") as TextBox);
            TextBox lname = (TextBox)(gridview1.Rows[e.RowIndex].FindControl("editcol2") as TextBox);
            TextBox address = (TextBox)(gridview1.Rows[e.RowIndex].FindControl("editcol3") as TextBox);
            Label date = (Label)(gridview1.Rows[e.RowIndex].FindControl("date") as Label);
            SqlConnection sc = new SqlConnection(con);
            int ii = Convert.ToInt32(user.Text);
            sql = "update  form set firstname='" + fname.Text + "',lastname='" + lname.Text + "',address='" + address.Text + "' where user_id='" + ii + "' ";
            sc.Open();
            SqlHelper.ExecuteNonQuery(sc, CommandType.Text, sql);
            sc.Close();
            gridview1.EditIndex = -1;
            filldata();
            



        }

        protected void gridview1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gridview1.EditIndex = -1;
            filldata();
        }
  
        protected void btndownload_Click1(object sender, EventArgs e)
        {
            SqlConnection sc = new SqlConnection(con);

            sql = "select * from form";
            SqlDataAdapter da = new SqlDataAdapter(sql, sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataTable dt = ds.Tables[0];
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("Content-Disposition", "attachment;filename=form.xls");
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            //table
            sw.Write("<table border='1'>");

            //colname
            sw.Write("<tr>");
            foreach (DataColumn column in dt.Columns)
            {
                sw.Write("<th>" + column.ColumnName + "</th>");

            }
            sw.Write("</tr>");
            // rows
            foreach (DataRow row in dt.Rows)
            {
                sw.Write("<tr>");
                //each data
                foreach (var item in row.ItemArray)
                {
                    sw.Write("<td>" + item.ToString() + "</td>");
                }
                sw.Write("</tr>");
            }
            sw.Write("<table>");


            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
    }
}