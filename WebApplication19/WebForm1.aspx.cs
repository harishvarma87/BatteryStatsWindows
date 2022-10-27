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
    public partial class WebForm1 : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            // the function "data collect" is called for every one minute
            int seconds = 60 * 1000;
            System.Timers.Timer t = new System.Timers.Timer(TimeSpan.FromMinutes(1).TotalMilliseconds); 
            t.AutoReset = true;
            t.Elapsed += new System.Timers.ElapsedEventHandler(datacollect);
            t.Start();

        }

        //This function collects the battery details and stores them in the database
        private static void datacollect(object o, ElapsedEventArgs e)
        {
            string connection = @"server=INL370; database=taskdb; trusted_connection=yes";
            PowerStatus pwr = SystemInformation.PowerStatus;
            float strBatterylife;
            strBatterylife = pwr.BatteryLifePercent * 100;
            string battery_perc = strBatterylife.ToString();
            String ChargingStatus;
            ChargingStatus = pwr.BatteryChargeStatus.ToString();
            DateTime timestamp = DateTime.Now;
            List<string> time = new List<string>();
            SqlConnection conn = new SqlConnection(connection);
            conn.Open();
            SqlCommand comm = new SqlCommand();
            SqlCommand comm2 = new SqlCommand();
            comm2.CommandText = "select time from batterycycle order by [time] ASC";
            comm2.Connection = conn;
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(comm2);
            da.Fill(dt);
            foreach (DataRow row in dt.Rows)
            {
                time.Add(row.Field<DateTime>(0).ToString("d HH:mm")); 
            }
            comm.Connection = conn;
            if (!time.Contains(timestamp.ToString("d HH:mm"))) //making sure that duplicate data is not being added
            {
                comm.CommandText = "insert into batterycycle(time, percentage, status) values('" + timestamp + "'," + battery_perc + ",'" + ChargingStatus + "')";
                comm.ExecuteNonQuery();
            }
            time.Add(timestamp.ToString("d HH:mm"));
            conn.Close();
        }
        
       
        //protected void Button1_Click(object sender, EventArgs e)
        //{
        //    string connection = @"server=INL370; database=taskdb; trusted_connection=yes";
        //    SqlConnection conn = new SqlConnection(connection);
        //    conn.Open();
        //    SqlCommand comm = new SqlCommand();
        //    comm.Connection = conn;
        //    comm.CommandText = "select * from batterycycle order by [time] ASC";
        //    SqlDataReader data = comm.ExecuteReader();
        //    var time = "2022-10-13 17:30:10";
        //    int percentage;
        //    while (data.Read())
        //    {
                
        //        GridView1.DataSource = data;
        //        GridView1.DataBind();
        //    }

        //}

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("Webform2.aspx");

        }
            protected void Button3_Click(object sender, EventArgs e)
        {
            Response.Redirect("Webform3.aspx");

        }

    }
}
