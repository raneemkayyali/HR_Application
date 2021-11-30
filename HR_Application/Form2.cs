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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private SqlDataReader rdr1;

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string usernameString = usernameTextBox.Text;
            string passwordString = passwordTextBox.Text;

            using (SqlConnection con = new SqlConnection())
            {
                Form1 f1 = new Form1();
                Form3 f3 = new Form3();
                Form5 f5 = new Form5();

                SqlCommand cmd = new SqlCommand();
                con.ConnectionString = GetConnectionString.VFConString("Users Con", "usr", "C:\\HR Config\\HR.ini");
                con.Open();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from Users_TBL where userName = '" + usernameString + "' and pWD = HASHBYTES('SHA2_256', '" + passwordString + "')";
                rdr1 = cmd.ExecuteReader();

                if (rdr1.HasRows)
                {
                    while (rdr1.Read())
                    {                    
                        Hide();
                        f5.SetLabel(usernameString);
                        f5.Show();
                    }

                    //f1.SetLabel(textBox1.Text);
                }
                else
                {
                    MessageBox.Show("Incorrect username or password.");
                    usernameTextBox.Text = "";
                    passwordTextBox.Text = "";
                }
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form3 f3 = new Form3();
            Hide();
            f3.Show();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form4 f4 = new Form4();
            Hide();
            f4.Show();
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
