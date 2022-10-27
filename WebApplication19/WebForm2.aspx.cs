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
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            List<DateTime> time = new List<DateTime>();
            
            List<int> percentage = new List<int>();
            
            List<string> status = new List<string>();
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
            int cont = 0;
            //extracting individual column to individual lists
            foreach (DataRow row in dt.Rows)
            {
                time.Add(row.Field<DateTime>(0));
                percentage.Add(row.Field<int>(1));
                status.Add(row.Field<string>(2));
                
                cont++;
            }
            
            List<int> intervals = new List<int>();
            cont = 1;
            intervals.Add(0);
            int initial = percentage[0];
            //storing the index of time array where new hour is getting started
            for (int j = 1; j < time.Count-1; j++)
            {
                if (time[j].Minute == 0)
                {
                    intervals.Add(j);
                    cont++;
                }
            }
            List<string> res = new List<string>();
            GridView1.DataSource = res;
            GridView1.DataBind();
           
            int calc = 0;
            DateTime start = time[0];
            DateTime time_start = time[0];
            DateTime time_end = time[0];
            res.Add("........................................On " + time[0].ToString("d")+"...........................................");
            int i = 0;
            int k = 1;
            int time_taken = 0;
            // iterating through the data and displaying the output
            for (i = 0; i < percentage.Count - 1; i++)
            {
                //checking if date is changing and adding the corresponding details   
                if (i != 0 && time[i].ToString("d") != time[i - 1].ToString("d"))
                {
                    time_taken = Convert.ToInt32((time_end - time_start).TotalMinutes);
                    if (time_taken > 0)
                    {
                        time_taken += 1;
                    }
                    if ((time[i]-start).TotalMinutes<=62)
                    {
                        if (calc == 0)
                        {
                            res.Add(start.ToString("HH:mm") + " to " + time[i].ToString("HH:mm") + " -> " + "there is no discharge");

                        }
                        else
                        {
                            res.Add(start.ToString("HH:mm") + " to " + time[i].ToString("HH:mm") + " -> " + calc + "% got discharged and time taken is " + time_taken + " minutes");
                        }
                    }
                    //adding the new date
                    res.Add("........................................On " + time[i].ToString("d") + "...........................................");
                    calc = 0;
                    initial = percentage[i];
                    time_start = time[i];
                    time_end = time[i];
                    time_taken = 0;
                    start = time[i];


                }

                //taking care of irrelavant data
                else if ((time[i + 1] - time[i]).TotalMinutes > 60)
                {
                    time_taken = time_taken + Convert.ToInt32((time_end - time_start).TotalMinutes);
                    if (time_taken > 0)
                    {
                        time_taken += 1;
                    }
                    time_start = time[i];
                    time_end = time[i];
                    initial = percentage[i];
                    if (calc == 0)
                    {
                        res.Add(start.ToString("HH:mm") + " to " + time[i].ToString("HH:mm") + " -> " + "there is no discharge");

                    }
                    else
                    {
                        res.Add(start.ToString("HH:mm") + " to " + time[i].ToString("HH:mm") + " -> " + calc + "% got discharged and time taken is " + time_taken + " minutes");
                    }
                    calc = 0;
                    time_taken = 0;
                    start = time[i];

                }

                //adding the data when new hour starts
                else if ((i != 0 && (intervals.Contains(i))))
                {
                    
                    time_taken = time_taken + Convert.ToInt32((time_end - time_start).TotalMinutes);
                    if (time_taken > 0)
                    {
                        time_taken += 1;
                    }
                    time_start = time[i];
                    time_end = time[i];
                    initial = percentage[i];
                    if (calc == 0)
                    {
                        res.Add(start.ToString("HH:mm") + " to " + time[i].ToString("HH:mm") + " -> " + "there is no discharge");

                    }
                    else
                    {
                        res.Add(start.ToString("HH:mm") + " to " + time[i].ToString("HH:mm") + " -> " + calc + "% got discharged and time taken is " + time_taken + " minutes");
                    }
                    calc = 0;
                    time_taken = 0;
                    start = time[i];

                }
                // taking care of irrelavant data
                else if (i != 0 && time[i].ToString("d") == time[i - 1].ToString("d") && time[i].ToString("HH") != time[i - 1].ToString("HH"))
                {
                    time_taken = time_taken + Convert.ToInt32((time_end - time_start).TotalMinutes);
                    if (time_taken > 0)
                    {
                        time_taken += 1;
                    }
                    time_start = time[i];
                    time_end = time[i];
                    initial = percentage[i];
                    if (calc == 0)
                    {
                        res.Add(start.ToString("HH:mm") + " to " + time[i-1].ToString("HH:mm") + " -> " + "there is no discharge");

                    }
                    else
                    {
                        res.Add(start.ToString("HH:mm") + " to " + time[i - 1].ToString("HH:mm") + " -> " + calc + "% got discharged and time taken is " + time_taken + " minutes");
                    }
                    calc = 0;
                    time_taken = 0;
                    start = time[i];
                }
                else
                {
                    //calculating the discharge
                    if (percentage[i] > percentage[i + 1])
                    {
                        calc = calc + (percentage[i] - percentage[i + 1]);

                    }
                }
                //storing the end time of discharge
                if (percentage[i] < initial && !status[i].Contains("Charging"))
                {
                    time_end = time[i];
                }
                //calculating the time taken to discharge
                if (status[i + 1].Contains("Charging") && !status[i].Contains("Charging"))
                {
                    time_taken = Convert.ToInt32((time_end - time_start).TotalMinutes);
                }
                if (status[i].Contains("Charging") )
                {
                    
                    initial = percentage[i];
                    time_start = time[i];
                    time_end = time[i];
                }
                
            }
            //taking care of present running time
            if (i == percentage.Count - 1)
            {
                res.Add(start.ToString("HH:mm") + " to " + time[i].ToString("HH:mm") + " -> " + calc + "% got discharged and its still processing");
            }
            GridView1.DataSource = res;
            GridView1.DataBind();

        
    }
        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("Webform1.aspx");
        }
       
    }
}