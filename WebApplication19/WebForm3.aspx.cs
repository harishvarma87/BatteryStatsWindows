using System;
using System.Timers;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WebApplication19
{
    public partial class WebForm3 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            List<DateTime> time = new List<DateTime>();
            List<string> status = new List<string>();
            List<int> percentage = new List<int>();
            string connection = @"server=INL370; database=taskdb; trusted_connection=yes";
            SqlConnection conn = new SqlConnection(connection);
            conn.Open();
            SqlCommand comm = new SqlCommand();
            comm.Connection = conn;
            comm.CommandText = "select * from batterycycle order by [time] ASC";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(comm);
            // taking the data from database to datatable dt
            da.Fill(dt);
            //extracting individual column to individual lists
            foreach (DataRow row in dt.Rows)
            {
                time.Add(row.Field<DateTime>(0));
                percentage.Add(row.Field<int>(1));
                //min[cont] = time[cont].Minute;
                status.Add(row.Field<string>(2));
            }
            DateTime start = time[0];
            DateTime end = time[0];
            int count = 0;
            int normal = 0;
            DateTime sn = time[0];
            DateTime en = time[0];
            int fn = 0;
            int total_count = 0;
            List<string> res = new List<string>();
           
            string type = "Spot count";
            int flag = 0;
            //iterating through the data
            for (int i = 0; i < status.Count; i++)
            {
                
                    // storing start time of normal charge(when charging<100)
                if (percentage[i]!=100 && status[i].Contains("Charging") && fn==0)
                {
                    sn= time[i];
                }
                //storing end time of normal charge(when charge<100)
                if(percentage[i] != 100 && status[i].Contains("Charging"))
                {
                    fn = 1;
                    en= time[i];
                }
                //storing normal charge details
                if (fn==1 && !status[i].Contains("Charging"))
                {
                    res.Add("Charged from " + start.ToString("HH:mm") +" on "+ start.ToString("d")+" and count is " + "Spot");
                    fn = 0;
                }
                if (percentage[i] == 100 && flag == 0)
                {
                    fn = 0;
                    start = time[i];
                }
                //calculating the count
                if (percentage[i] == 100 && status[i].Contains("Charging"))
                {
                    flag = 1;
                    count++;
                    type = count == 0 ? "spot count" : "spot count";
                    type = count < 30 ? "optimal count" : "spot count";
                    type = count > 30 ? "bad count" : "spot count";
                    end = time[i];

                }
                // storing overcharge details
                if (flag == 1 && !status[i].Contains("Charging"))
                {
                    if (count < 30)
                    {
                        type = "Optimal Count";
                    }
                    else
                    {
                        type = "bad count";
                    }
                    flag = 0;
                    res.Add("Overcharged from " + start.ToString("HH:mm") +" on "+ start.ToString("d")+" and count is " + type);
                    total_count++;

                }

            }
            //displaying the total no of time battery overcharged
            if (total_count > 0)
            {
                res.Add("Total times overcharged:" + total_count);
                GridView1.DataSource = res;
                GridView1.DataBind();
            }
            else
            {
                res.Add("Battery is not overcharged so count is" + type);
                GridView1.DataSource = res;
                GridView1.DataBind();
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("Webform1.aspx");
        }
    }
}