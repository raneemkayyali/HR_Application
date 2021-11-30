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
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        SqlDataReader rdr1;
        bool valid = false;

        private void ValidateAdmin()
        {
            string adminPWD = adminPWDTxtBx.Text;

            SqlConnection con = new SqlConnection();
            SqlCommand cmd = new SqlCommand();
            con.ConnectionString = GetConnectionString.VFConString("Users Con", "usr", "C:\\HR Config\\HR.ini");
            con.Open();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select secLvl from Users_TBL where pWD = HASHBYTES('SHA2_256','" + adminPWD + "')";
            rdr1 = cmd.ExecuteReader();

            if (rdr1.HasRows)
            {
                rdr1.Read();

                string lvl = rdr1.GetString(0);

                if (lvl == "admin")
                {
                    valid = true;
                }
                else
                {
                    valid = false;
                }

            }
        }
        private void ChangePassword()
        {
            string userName = userNameTxtBx.Text;
            string newPWD = newPWDTxtBx.Text;
            string confirmPWD = confirmPWDTxtBx.Text;

            if (newPWD == confirmPWD)
            {
                if (valid == true)
                {
                    try
                    {
                        SqlCommand cmd = new SqlCommand();
                        SqlConnection con = new SqlConnection();
                        string cmdquery = "Change_PWD";
                        string constring = GetConnectionString.VFConString("Users Con", "usr", "C:\\HR Config\\HR.ini");
                        con.ConnectionString = constring;
                        con.Open();
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = cmdquery;
                        cmd.Parameters.AddWithValue("@uPWD", confirmPWD);
                        cmd.Parameters.AddWithValue("@user", userName);
                        cmd.ExecuteNonQuery();

                        MessageBoxButtons btn = MessageBoxButtons.YesNo;
                        DialogResult result = MessageBox.Show("\tPassword Changed Successfully!\nWould you like to return to the login page?", "Success", btn);
                        if (result == DialogResult.Yes)
                        {
                            Form2 f2 = new Form2();
                            f2.Show();
                            Hide();
                        }
                        else
                        {
                            // do nothing
                        }

                        ClearAll();

                        return;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Please contact supervisor." +
                               " Database connection error: " + ex, "Error");
                    }
                }
                else
                {
                    MessageBox.Show("Incorrect Admin Password.", "Error");
                }
            }
            else 
            {
                MessageBox.Show("Password fields do not match.", "Error");
            }
        }

        private void ClearAll()
        {
            userNameTxtBx.Text = "";
            newPWDTxtBx.Text = "";
            confirmPWDTxtBx.Text = "";
            adminPWDTxtBx.Text = "";
            valid = false;
        }

        private void resetBtn_Click(object sender, EventArgs e)
        {
            ValidateAdmin();
            ChangePassword();
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            Form2 fp2 = new Form2();
            fp2.Show();
            Hide();
        }

        private void Form4_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
