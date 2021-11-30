using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VFConnectionStrings;

namespace HR_Application
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Form2 f2 = new Form2();
        string firstText;
        string secondText;
        string thirdText;
        string fourthText;
        int v;
        SqlDataReader rdr2;

        private void KeyValidation()
        {
            string completeString;

            firstText = textBox1.Text;
            secondText = textBox2.Text;
            thirdText = textBox3.Text;
            fourthText = textBox4.Text;

            completeString = firstText + '-' + secondText + '-' + thirdText + '-' + fourthText;

            try
            {
                SqlConnection con = new SqlConnection();
                SqlCommand cmd = new SqlCommand();
                con.ConnectionString = GetConnectionString.VFConString("HR Con", "hr", "C:\\HR Config\\HR.ini");
                con.Open();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select hrKey from secChk where hrKey = HASHBYTES('SHA2_256', '" + completeString + "')";
                rdr2 = cmd.ExecuteReader();

                if (rdr2.HasRows)
                {
                    rdr2.Read();
                    IsValid(1);
                    Hide();
                    f2.Show();
                }
                else
                {
                    MessageBox.Show("Incorrect Key Entry", "Error");
                    ClearAll();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Please contact supervisor." +
                    " Database connection error: " + e, "Error");
            }
        }

        private void ClearAll()
        {
            firstText = "";
            secondText = "";
            thirdText = "";
            fourthText = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            KeyValidation();
        }

        private void IsValid(int vNum)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                SqlConnection con = new SqlConnection();
                string cmdquery = "Update_validChk";
                string constring = GetConnectionString.VFConString("HR Con", "hr", "C:\\HR Config\\HR.ini");
                con.ConnectionString = constring;
                con.Open();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = cmdquery;
                cmd.Parameters.AddWithValue("@vNum", vNum);
                //SqlParameter result = cmd.Parameters.Add("@status", SqlDbType.Char, 1);
                //result.Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();

                //string status = result.Value.ToString();
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please contact supervisor." +
                       " Database connection error: " + ex, "Error");
            }

            //string key = "9DR7-6HN2-0395-NDAR";

        }

        private void CheckValid()
        {
            SqlConnection con = new SqlConnection();
            SqlCommand cmd = new SqlCommand();
            con.ConnectionString = GetConnectionString.VFConString("HR Con", "hr", "C:\\HR Config\\HR.ini");
            con.Open();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select validChk from secChk";
            rdr2 = cmd.ExecuteReader();

            if (rdr2.HasRows)
            {
                rdr2.Read();

                v = rdr2.GetInt32(0);
               
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CheckValid();
            if (v == 1)
            {
                f2.Show();
                this.WindowState = FormWindowState.Minimized;
                this.ShowInTaskbar = false;
            }
        }
    }
}
