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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private SqlDataReader rdr1;
        private bool adminPwdChecked = false;
        private string secLvl;
        private string pwd;
        private bool invalid = false;

        private void adminPasswordCheck(string passwordString)
        {
            using (SqlConnection con = new SqlConnection())
            {
                SqlCommand cmd = new SqlCommand();
                con.ConnectionString = GetConnectionString.VFConString("Users Con", "usr", "C:\\HR Config\\HR.ini");
                con.Open();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from Users_TBL where secLvl = 'admin' and pWD = HASHBYTES('SHA2_256', '" + passwordString + "')";
                rdr1 = cmd.ExecuteReader();

                if (rdr1.HasRows)
                {
                    while (rdr1.Read())
                    {
                        adminPwdChecked = true;
                    }
                }
                else
                {
                    MessageBox.Show("Incorrect admin password.", "Error");
                    adminPwdTxt.BackColor = Color.Crimson;
                }
            }
        }

        private void passwordCheck()
        {
            string createPWD = createPwdTxt.Text;
            string confirmPWD = confirmPwdTxt.Text;
            string adminPWD = adminPwdTxt.Text;
            string adminString = "admin";

            secLvl = secLvlComBox.SelectedItem.ToString();

            if (createPWD != confirmPWD)
            {
                MessageBox.Show("Passwords do not match.", "Error");
                createPwdTxt.BackColor = Color.Crimson;
                confirmPwdTxt.BackColor = Color.Crimson;
            }
            
            if (secLvl == adminString)
            {
                adminPasswordCheck(adminPWD);
            }

            if (adminPwdChecked == true && createPWD == confirmPWD)
            {
                NotAdminPWD(confirmPWD);
                if (invalid == true)
                {
                    MessageBox.Show("Password not accepted. Please enter a new password.", "Error");
                    createPwdTxt.Text = "";
                    confirmPwdTxt.Text = "";
                }
                else 
                {
                    pwd = confirmPWD;
                    signUp(pwd);
                }
            }
            else if (createPWD == confirmPWD)
            {
                NotAdminPWD(confirmPWD);
                if (invalid == true)
                {
                    MessageBox.Show("Password not accepted. Please enter a new password.", "Error");
                    createPwdTxt.Text = "";
                    confirmPwdTxt.Text = "";
                }
                else
                {
                    if (adminPwdTxt.Text == "")
                    {
                        MessageBox.Show("Admin Password is Required.", "Error");
                        adminPwdTxt.BackColor = Color.Crimson;
                    }
                    else
                    {
                        pwd = confirmPWD;
                        signUp(pwd);
                    }
                }
            }
        }

        private void signUp(string uPWD)
        {
            string fName = firstNameTxt.Text;
            string mName = middleNametxt.Text;
            string lName = lastNameTxt.Text;
            string uName = userNameTxt.Text;
            Form2 f2 = new Form2();

            try
            {
                SqlCommand cmd = new SqlCommand();
                SqlConnection con = new SqlConnection();
                string cmdquery = "Insert_User";
                string constring = GetConnectionString.VFConString("Users Con", "usr", "C:\\HR Config\\HR.ini");
                con.ConnectionString = constring;
                con.Open();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = cmdquery;
                cmd.Parameters.AddWithValue("@fName", fName);
                cmd.Parameters.AddWithValue("@mName", mName);
                cmd.Parameters.AddWithValue("@lName", lName);
                cmd.Parameters.AddWithValue("@uName", uName);
                cmd.Parameters.AddWithValue("@uPWD", uPWD);
                cmd.Parameters.AddWithValue("@secLvl", secLvl);
                cmd.ExecuteNonQuery();

                MessageBoxButtons btn = MessageBoxButtons.YesNo;
                DialogResult result = MessageBox.Show("Would you like to sign up anymore people?", "", btn);
                if (result == DialogResult.Yes)
                {
                    
                }
                else
                {
                    Hide();
                    f2.Show();
                }

                clearAll();

                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please contact supervisor." +
                       " Database connection error: " + ex, "Error");
            }
        }

        private void NotAdminPWD(string passWD)
        {
            using (SqlConnection con = new SqlConnection())
            {
                SqlCommand cmd = new SqlCommand();
                con.ConnectionString = GetConnectionString.VFConString("Users Con", "usr", "C:\\HR Config\\HR.ini");
                con.Open();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select pWD from Users_TBL where pWD = HASHBYTES('SHA2_256', '" + passWD + "')";
                rdr1 = cmd.ExecuteReader();

                if (rdr1.HasRows)
                {
                    while (rdr1.Read())
                    {
                        invalid = true;
                    }
                }
                else
                {
                    invalid = false;
                }
            }
        }

        private void signUpBtn_Click(object sender, EventArgs e)
        {
            passwordCheck();
        }

        private void secLvlComBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (secLvlComBox.SelectedItem.ToString() == "admin")
            {
                adminPwdTxt.Visible = true;
                adminPasswordLbl.Visible = true;
            }
            else 
            {
                adminPwdTxt.Visible = false;
                adminPasswordLbl.Visible = false;
            }
        }

        private void clearAll()
        {
            firstNameTxt.Text = "";
            middleNametxt.Text = "";
            lastNameTxt.Text = "";
            userNameTxt.Text = "";
            createPwdTxt.Text = "";
            confirmPwdTxt.Text = "";
            secLvlComBox.SelectedIndex = 0;
            adminPwdTxt.Text = "";
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.Show();
            Hide();
        }

        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
