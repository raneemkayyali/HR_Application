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
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }

        private string userLblTxt;
        private int userID;
        SqlDataReader rdr1;
        bool btnChkClr = false;

        public void SetLabel(string text)
        {
            this.userLbl.Text = text;
        }

        private void deleteBtn_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(deletePage);
            Load_DeleteListView();
        }

        private void activityBtn_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(activityPage);
            Load_ActivityListView();
            Load_UserComboBox();
        }

        private void enterBtn_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(enterPage);
        }

        private void GetUpdatePersonalEmps(string peid)
        {
            SqlConnection con = new SqlConnection();
            SqlCommand cmd = new SqlCommand();
            con.ConnectionString = GetConnectionString.VFConString("HR Con", "hr", "C:\\HR Config\\HR.ini");
            con.Open();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select FName, MName1, MName2, LName, NatType, NatNum, DOB, MarStat, HLoE from Personal_Emps where PEID = '" + peid + "'";
            rdr1 = cmd.ExecuteReader();

            if (rdr1.HasRows)
            {
                rdr1.Read();

                string first1 = rdr1.GetString(0);
                string mid1 = rdr1.GetString(1);
                string mid2 = rdr1.GetString(2);
                string last1 = rdr1.GetString(3);
                string nType = rdr1.GetString(4);
                string nNum = rdr1.GetString(5);
                DateTime dOb = rdr1.GetDateTime(6);
                string marStat = rdr1.GetString(7);
                string hlo = rdr1.GetString(8);

                fNameUpTxtBx.Text = first1;
                mN1UpTxtBx.Text = mid1;
                mN2UpTxtBx.Text = mid2;
                lnUpTxtBx.Text = last1;
                natUpComBx.Text = nType;
                natNumUpTxtBx.Text = nNum;
                dobUpDateTime.Value = dOb;
                marUpComBx.Text = marStat;
                hloeUpComBx.Text = hlo;

            }
        }

        private void GetUpdateContactEmps(string empID)
        {
            SqlConnection con = new SqlConnection();
            SqlCommand cmd = new SqlCommand();
            con.ConnectionString = GetConnectionString.VFConString("HR Con", "hr", "C:\\HR Config\\HR.ini");
            con.Open();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select EmpAddress, EmpCity, EmpCountry, PhoneNum, EmergNum from Contact_Emps where EmpID = '" + empID + "'";
            rdr1 = cmd.ExecuteReader();

            if (rdr1.HasRows)
            {
                rdr1.Read();

                string address = rdr1.GetString(0);
                string city = rdr1.GetString(1);
                string country = rdr1.GetString(2);
                string phone = rdr1.GetString(3);
                string emerg = rdr1.GetString(4);

                addUpTxtBx.Text = address;
                cityUpTxtBx.Text = city;
                countryUpTxtBx.Text = country;
                phnUpTxtBx.Text = phone;
                emergUpTxtBox.Text = emerg;
            }
        }

        private void GetUpdateSupNames(string empID2)
        {
            SqlConnection con = new SqlConnection();
            SqlCommand cmd = new SqlCommand();
            con.ConnectionString = GetConnectionString.VFConString("HR Con", "hr", "C:\\HR Config\\HR.ini");
            con.Open();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select SupFName, SupMName, SupLName, DeptName from Sup_Names where EmpID = '" + empID2 + "'";
            rdr1 = cmd.ExecuteReader();

            if (rdr1.HasRows)
            {
                rdr1.Read();

                string sfn = rdr1.GetString(0);
                string smn = rdr1.GetString(1);
                string slm = rdr1.GetString(2);
                string department = rdr1.GetString(3);

                sfnUpTxtBx.Text = sfn;
                smnUpTxtBx.Text = smn;
                slnUpTxtBx.Text = slm;
                deptUpTxtBx.Text = department;
            }
        }
        private void GetUpdateEmpStatus(string empID)
        {
            SqlConnection con = new SqlConnection();
            SqlCommand cmd = new SqlCommand();
            con.ConnectionString = GetConnectionString.VFConString("HR Con", "hr", "C:\\HR Config\\HR.ini");
            con.Open();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select Pos, EmpType, EmpStat, StartDate, EndDate, ReasonLeft from Emp_Status where EmpID = '" + empID + "'";
            rdr1 = cmd.ExecuteReader();

            if (rdr1.HasRows)
            {
                rdr1.Read();

                string position = rdr1.GetString(0);
                string type = rdr1.GetString(1);
                string status = rdr1.GetString(2);
                DateTime startDate = rdr1.GetDateTime(3);
                DateTime endDate = rdr1.GetDateTime(4);
                string reas = rdr1.GetString(5);

                posUpTxtBox.Text = position;
                empStatUpComBx.Text = status;
                startUpDateTime.Value = startDate;
                endUpDateTime.Value = endDate;
                reasonUpTxtBx.Text = reas;

                if (type == "Full Time")
                {
                    fullUpRdoBtn.Checked = true;
                }
                else if (type == "Part Time")
                {
                    partUpRdoBtn.Checked = true;
                }

            }
        }

        ListViewItem seItem;
        private void updateBtn_Click(object sender, EventArgs e)
        {
            try
            {
                seItem = searchListView.SelectedItems[0];
                tabControl1.SelectTab(updatePage);
                if (seItem.Text != null || seItem.Text != "")
                {
                    GetUpdatePersonalEmps(seItem.Text);
                    GetUpdateContactEmps(seItem.Text);
                    GetUpdateSupNames(seItem.Text);
                    GetUpdateEmpStatus(seItem.Text);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Please select a record to view.", "Error");
            }
        }
        private void LoadPositions()
        {
            posSearchDropDwn.Items.Clear();
            SqlConnection con = new SqlConnection();
            SqlCommand cmd = new SqlCommand();
            con.ConnectionString = GetConnectionString.VFConString("HR Con", "hr", "C:\\HR Config\\HR.ini");
            con.Open();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select distinct Pos from Emp_Status";
            rdr1 = cmd.ExecuteReader();

            if (rdr1.HasRows)
            {
                while (rdr1.Read())
                {
                    posSearchDropDwn.Items.Add(rdr1.GetString(0));
                }
            }
        }
        private void LoadDepartments()
        {
            deptSearchDropDwn.Items.Clear();
            SqlConnection con = new SqlConnection();
            SqlCommand cmd = new SqlCommand();
            con.ConnectionString = GetConnectionString.VFConString("HR Con", "hr", "C:\\HR Config\\HR.ini");
            con.Open();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select distinct DeptName from Sup_Names";
            rdr1 = cmd.ExecuteReader();

            if (rdr1.HasRows)
            {
                while (rdr1.Read())
                {
                    deptSearchDropDwn.Items.Add(rdr1.GetString(0));
                }
            }
        }

        private void searchBtn_Click(object sender, EventArgs e)
        {
            Load_Search();
            LoadDepartments();
            LoadPositions();
            tabControl1.SelectTab(searchPage);

        }

        private void exitBtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Load_Search()
        {
            searchListView.Clear();
            searchListView.Columns.Add("", 0);
            searchListView.Columns.Add("First Name", 100);
            searchListView.Columns.Add("Father's Name", 110);
            searchListView.Columns.Add("GrandFather's Name", 110);
            searchListView.Columns.Add("Last Name", 100);
            searchListView.Columns.Add("DOB", 100);
            searchListView.Columns.Add("Nationality", 100);
            searchListView.Columns.Add("Level of Education", 100);
            searchListView.Columns.Add("Position", 100);
            searchListView.Columns.Add("Department", 100);
            searchListView.Columns.Add("Employee Status", 100);
            searchListView.Columns.Add("Type", 100);
            searchListView.Columns.Add("Start Date", 100);
            searchListView.Columns.Add("End Date", 100);
            searchListView.View = View.Details;
            ListViewItem lvi;

            SqlCommand cmd = new SqlCommand();
            SqlConnection con = new SqlConnection();
            con.ConnectionString = GetConnectionString.VFConString("HR Con", "hr", "C:\\HR Config\\HR.ini");
            con.Open();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select PEID, FName, MName1, MName2, LName, DOB, NatType, HLoE, Pos, DeptName, EmpStat, EmpType, StartDate, EndDate from Personal_Emps join Sup_Names on Sup_Names.EmpID = PEID join Emp_Status on Sup_Names.EmpID = Emp_Status.EmpID";
            rdr1 = cmd.ExecuteReader();

            if (rdr1.HasRows)
            {
                while (rdr1.Read())
                {
                    int id = rdr1.GetInt32(0);
                    string fn = rdr1.GetString(1);
                    string mn1 = rdr1.GetString(2);
                    string mn2 = rdr1.GetString(3);
                    string ln = rdr1.GetString(4);
                    DateTime date3 = rdr1.GetDateTime(5);
                    string nat = rdr1.GetString(6);
                    string hl = rdr1.GetString(7);
                    string pos = rdr1.GetString(8);
                    string dep = rdr1.GetString(9);
                    string es = rdr1.GetString(10);
                    string et = rdr1.GetString(11);
                    DateTime date1 = rdr1.GetDateTime(12);
                    DateTime date2 = rdr1.GetDateTime(13);

                    lvi = new ListViewItem(new string[] { id.ToString(), fn, mn1, mn2, ln, date3.ToString("dd/MM/yyyy"), nat, hl, pos, dep, es, et, date1.ToString("dd/MM/yyyy"), date2.ToString("dd/MM/yyyy") });

                    if (es == "Active")
                        lvi.BackColor = Color.PaleGreen;
                    if (es == "Inactive")
                        lvi.BackColor = Color.LightSalmon;

                    searchListView.Items.Add(lvi);
                }
            }
        }

        private void Load_DeleteListView()
        {
            deleteListView.Clear();
            deleteListView.Columns.Add("", 0);
            deleteListView.Columns.Add("First Name", 100);
            deleteListView.Columns.Add("Father's Name", 110);
            deleteListView.Columns.Add("GrandFather's Name", 110);
            deleteListView.Columns.Add("Last Name", 100);
            deleteListView.Columns.Add("DOB", 100);
            deleteListView.Columns.Add("Nationality", 100);
            deleteListView.Columns.Add("Level of Education", 100);
            deleteListView.Columns.Add("Position", 100);
            deleteListView.Columns.Add("Department", 100);
            deleteListView.Columns.Add("Employee Status", 100);
            deleteListView.Columns.Add("Type", 100);
            deleteListView.Columns.Add("Start Date", 100);
            deleteListView.Columns.Add("End Date", 100);
            deleteListView.View = View.Details;
            ListViewItem lvi;

            SqlCommand cmd = new SqlCommand();
            SqlConnection con = new SqlConnection();
            con.ConnectionString = GetConnectionString.VFConString("HR Con", "hr", "C:\\HR Config\\HR.ini");
            con.Open();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select PEID, FName, MName1, MName2, LName, NatType, HLoE, Pos, DeptName, EmpStat, EmpType, StartDate, EndDate, DOB from Personal_Emps join Sup_Names on Sup_Names.EmpID = PEID join Emp_Status on Sup_Names.EmpID = Emp_Status.EmpID";
            rdr1 = cmd.ExecuteReader();

            if (rdr1.HasRows)
            {
                while (rdr1.Read())
                {
                    int id = rdr1.GetInt32(0);
                    string fn = rdr1.GetString(1);
                    string mn1 = rdr1.GetString(2);
                    string mn2 = rdr1.GetString(3);
                    string ln = rdr1.GetString(4);
                    string nat = rdr1.GetString(5);
                    string hl = rdr1.GetString(6);
                    string pos = rdr1.GetString(7);
                    string dep = rdr1.GetString(8);
                    string es = rdr1.GetString(9);
                    string et = rdr1.GetString(10);
                    DateTime date1 = rdr1.GetDateTime(11);
                    DateTime date2 = rdr1.GetDateTime(12);
                    DateTime date3 = rdr1.GetDateTime(13);

                    lvi = new ListViewItem(new string[] { id.ToString(), fn, mn1, mn2, ln, date3.ToString("dd/MM/yyyy"), nat, hl, pos, dep, es, et, date1.ToString("dd/MM/yyyy"), date2.ToString("dd/MM/yyyy") });

                    if (es == "Active")
                        lvi.BackColor = Color.PaleGreen;
                    if (es == "Inactive")
                        lvi.BackColor = Color.LightSalmon;

                    deleteListView.Items.Add(lvi);
                }
            }
        }

        private void Load_ActivityListView()
        {
            activityListView.Clear();
            activityListView.Columns.Add("", 0);
            activityListView.Columns.Add("Entered By", 100);
            activityListView.Columns.Add("Updated By", 100);
            activityListView.Columns.Add("DateEntered", 130);
            activityListView.Columns.Add("First Name", 100);
            activityListView.Columns.Add("Father's Name", 110);
            activityListView.Columns.Add("GrandFather's Name", 110);
            activityListView.Columns.Add("Last Name", 100);
            activityListView.Columns.Add("DOB", 100);
            activityListView.Columns.Add("Nationality", 100);
            activityListView.Columns.Add("Level of Education", 100);
            activityListView.Columns.Add("Position", 100);
            activityListView.Columns.Add("Department", 100);
            activityListView.Columns.Add("Employee Status", 100);
            activityListView.Columns.Add("Type", 100);
            activityListView.Columns.Add("Start Date", 100);
            activityListView.Columns.Add("End Date", 100);
            activityListView.View = View.Details;
            ListViewItem lvi;

            SqlCommand cmd = new SqlCommand();
            SqlConnection con = new SqlConnection();
            con.ConnectionString = GetConnectionString.VFConString("HR Con", "hr", "C:\\HR Config\\HR.ini");
            con.Open();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select PEID, FName, MName1, MName2, LName, NatType, HLoE, Pos, DeptName, EmpStat, EmpType, StartDate, EndDate, Personal_Emps.EnteredBy, Personal_Emps.UpdatedBy, Personal_Emps.DateEntered, DOB from Personal_Emps join Sup_Names on Sup_Names.EmpID = PEID join Emp_Status on Sup_Names.EmpID = Emp_Status.EmpID";
            rdr1 = cmd.ExecuteReader();

            if (rdr1.HasRows)
            {
                while (rdr1.Read())
                {
                    int id = rdr1.GetInt32(0);
                    string fn = rdr1.GetString(1);
                    string mn1 = rdr1.GetString(2);
                    string mn2 = rdr1.GetString(3);
                    string ln = rdr1.GetString(4);
                    string nat = rdr1.GetString(5);
                    string hl = rdr1.GetString(6);
                    string pos = rdr1.GetString(7);
                    string dep = rdr1.GetString(8);
                    string es = rdr1.GetString(9);
                    string et = rdr1.GetString(10);
                    DateTime date1 = rdr1.GetDateTime(11);
                    DateTime date2 = rdr1.GetDateTime(12);
                    string ent = rdr1.GetString(13);
                    string up = rdr1.GetString(14);
                    DateTime date3 = rdr1.GetDateTime(15);
                    DateTime date4 = rdr1.GetDateTime(16);

                    lvi = new ListViewItem(new string[] { id.ToString(), ent, up, date3.ToString(), fn, mn1, mn2, ln, date4.ToString("dd/MM/yyyy"), nat, hl, pos, dep, es, et, date1.ToString("dd/MM/yyyy"), date2.ToString("dd/MM/yyyy") });

                    if (es == "Active")
                        lvi.BackColor = Color.PaleGreen;
                    if (es == "Inactive")
                        lvi.BackColor = Color.LightSalmon;

                    activityListView.Items.Add(lvi);
                }
            }
        }

        private void Load_UserComboBox()
        {
            SqlConnection con = new SqlConnection();
            SqlCommand cmd = new SqlCommand();
            con.ConnectionString = GetConnectionString.VFConString("Users Con", "usr", "C:\\HR Config\\HR.ini");
            con.Open();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select distinct userName from Users_TBL";
            rdr1 = cmd.ExecuteReader();

            if (rdr1.HasRows)
            {
                while (rdr1.Read())
                {
                    userComboBox.Items.Add(rdr1.GetString(0));
                }

            }
        }

        private void Insert_PersonalEmp()
        {
            string fnTxt = fnTxtBox.Text;
            string mn1Txt = m1TxtBox.Text;
            string mn2Txt = m2TxtBox.Text;
            string lnTxt = lnTxtBox.Text;
            string natTxt = natComboBox.SelectedItem.ToString();
            string natIDTxt = idNumTxtBox.Text;
            DateTime dobTxt = dobDateTime.Value;
            string marStatTxt = marComboBox.SelectedItem.ToString();
            string hloeTxt = hloeComboBox.SelectedItem.ToString();

            try
            {
                SqlCommand cmd = new SqlCommand();
                SqlConnection con = new SqlConnection();
                string cmdquery = "Insert_PersonalEmp";
                string constring = GetConnectionString.VFConString("HR Con", "hr", "C:\\HR Config\\HR.ini");
                con.ConnectionString = constring;
                con.Open();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = cmdquery;
                cmd.Parameters.AddWithValue("@fName", fnTxt);
                cmd.Parameters.AddWithValue("@mName1", mn1Txt);
                cmd.Parameters.AddWithValue("@mName2", mn2Txt);
                cmd.Parameters.AddWithValue("@lName", lnTxt);
                cmd.Parameters.AddWithValue("@natType", natTxt);
                cmd.Parameters.AddWithValue("@natID", natIDTxt);
                cmd.Parameters.AddWithValue("@dob", dobTxt);
                cmd.Parameters.AddWithValue("@mar", marStatTxt);
                cmd.Parameters.AddWithValue("@hloe", hloeTxt);
                cmd.Parameters.AddWithValue("@eby", userLblTxt);
                cmd.ExecuteNonQuery();

                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please contact supervisor." +
                       " Database connection error: " + ex, "Error");
            }
        }

        private void Insert_ContactEmp()
        {
            GetID();
            string addTxt = addressTxtBox.Text;
            string cityTxt = cityTxtBox.Text;
            string countryTxt = countryTxtBox.Text;
            string phoneNumTxt = phnTxtBox.Text;
            string emNumTxt = emergTxtBox.Text;

            try
            {
                SqlCommand cmd = new SqlCommand();
                SqlConnection con = new SqlConnection();
                string cmdquery = "Insert_ContactEmp";
                string constring = GetConnectionString.VFConString("HR Con", "hr", "C:\\HR Config\\HR.ini");
                con.ConnectionString = constring;
                con.Open();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = cmdquery;
                cmd.Parameters.AddWithValue("@address", addTxt);
                cmd.Parameters.AddWithValue("@city", cityTxt);
                cmd.Parameters.AddWithValue("@country", countryTxt);
                cmd.Parameters.AddWithValue("@pn", phoneNumTxt);
                cmd.Parameters.AddWithValue("@en", emNumTxt);
                cmd.Parameters.AddWithValue("@eby", userLblTxt);
                cmd.Parameters.AddWithValue("@eID", userID);
                cmd.ExecuteNonQuery();

                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please contact supervisor." +
                       " Database connection error: " + ex, "Error");
            }
        }

        private void GetID()
        {
            try
            {
                SqlConnection con = new SqlConnection();
                SqlCommand cmd = new SqlCommand();
                con.ConnectionString = GetConnectionString.VFConString("HR Con", "hr", "C:\\HR Config\\HR.ini");
                con.Open();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select PEID from Personal_Emps where FName = '" + fnTxtBox.Text + "' " +
                    "and MName1 = '" + m1TxtBox.Text + "' and MName2 = '" + m2TxtBox.Text + "' and LName = '" + lnTxtBox.Text + "';";
                rdr1 = cmd.ExecuteReader();

                if (rdr1.HasRows)
                {
                    rdr1.Read();

                    userID = rdr1.GetInt32(0);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please contact supervisor." +
                       " Database connection error: " + ex, "Error");
            }
        }

        private void Insert_SupNames()
        {
            GetID();

            string sFNameTxt = supFnTxtBox.Text;
            string sMNameTxt = supMnTxtBox.Text;
            string sLNameTxt = supLnTxtBox.Text;
            string deptTxt = deptTxtBox.Text;

            try
            {
                SqlCommand cmd = new SqlCommand();
                SqlConnection con = new SqlConnection();
                string cmdquery = "Insert_SupNames";
                string constring = GetConnectionString.VFConString("HR Con", "hr", "C:\\HR Config\\HR.ini");
                con.ConnectionString = constring;
                con.Open();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = cmdquery;
                cmd.Parameters.AddWithValue("@supFn", sFNameTxt);
                cmd.Parameters.AddWithValue("@supMn", sMNameTxt);
                cmd.Parameters.AddWithValue("@supLn", sLNameTxt);
                cmd.Parameters.AddWithValue("@dept", deptTxt);
                cmd.Parameters.AddWithValue("@eby", userLblTxt);
                cmd.Parameters.AddWithValue("@eID", userID);
                cmd.ExecuteNonQuery();

                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please contact supervisor." +
                       " Database connection error: " + ex, "Error");
            }
        }

        private void Insert_EmpStat()
        {
            GetID();
            string posTxt = posTxtBox.Text;
            string empType = "";
            string empStat = empStatComboBox.SelectedItem.ToString();
            string reasonTxt = reasonTxtBox.Text;
            DateTime startDate = startDateTime.Value;
            DateTime endDate = endDateTime.Value;

            if (fullTimeRadioBtn.Checked == true)
            {
                empType = "Full Time";
            }
            else if (partTimeRadioBtn.Checked == true)
            {
                empType = "Part Time";
            }

            try
            {
                SqlCommand cmd = new SqlCommand();
                SqlConnection con = new SqlConnection();
                string cmdquery = "Insert_EmpStat";
                string constring = GetConnectionString.VFConString("HR Con", "hr", "C:\\HR Config\\HR.ini");
                con.ConnectionString = constring;
                con.Open();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = cmdquery;
                cmd.Parameters.AddWithValue("@pos", posTxt);
                cmd.Parameters.AddWithValue("@empType", empType);
                cmd.Parameters.AddWithValue("@empStat", empStat);
                cmd.Parameters.AddWithValue("@reason", reasonTxt);
                cmd.Parameters.AddWithValue("@startDate", startDate);
                cmd.Parameters.AddWithValue("@endDate", endDate);
                cmd.Parameters.AddWithValue("@eby", userLblTxt);
                cmd.Parameters.AddWithValue("@eID", userID);
                cmd.ExecuteNonQuery();

                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please contact supervisor." +
                       " Database connection error: " + ex, "Error")
            }
        }

        private void enterFormBtn_Click(object sender, EventArgs e)
        {
            try
            {
                Insert_PersonalEmp();
                Insert_ContactEmp();
                Insert_SupNames();
                Insert_EmpStat();
                MessageBox.Show("Data Successfully Entered!", "Success");
                ClearEnterPage();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please contact supervisor." +
                       " Error: " + ex, "Error");
            }
        }

        private void ClearUpdatePage()
        {
            fNameUpTxtBx.Text = "";
            mN1UpTxtBx.Text = "";
            mN2UpTxtBx.Text = "";
            lnUpTxtBx.Text = "";
            natUpComBx.SelectedIndex = -1;
            natNumUpTxtBx.Text = "";
            dobUpDateTime.Value = DateTime.Now;
            marUpComBx.SelectedIndex = -1;
            hloeUpComBx.SelectedIndex = -1;
            addUpTxtBx.Text = "";
            cityUpTxtBx.Text = "";
            countryUpTxtBx.Text = "";
            phnUpTxtBx.Text = "";
            emergUpTxtBox.Text = "";
            sfnUpTxtBx.Text = "";
            smnUpTxtBx.Text = "";
            slnUpTxtBx.Text = "";
            deptUpTxtBx.Text = "";
            posUpTxtBox.Text = "";
            empStatUpComBx.SelectedIndex = -1;
            startUpDateTime.Value = DateTime.Now;
            endUpDateTime.Value = DateTime.Now;
            reasonUpTxtBx.Text = "";
            btnChkClr = true;
            fullUpRdoBtn.Checked = false;
            partUpRdoBtn.Checked = false;
        }

        private void UpdateEmps(string peid)
        {
            string fNameUp = fNameUpTxtBx.Text;
            string mName1Up = mN1UpTxtBx.Text;
            string mName2Up = mN2UpTxtBx.Text;
            string lNameUp = lnUpTxtBx.Text;
            string natTypeUp = natUpComBx.SelectedItem.ToString();
            string natNumUp = natNumUpTxtBx.Text;
            DateTime dobUp = dobUpDateTime.Value;
            string marStatUp = marUpComBx.SelectedItem.ToString();
            string hloeUp = hloeUpComBx.SelectedItem.ToString();
            string upBy = userLblTxt;
            string empAddUp = addUpTxtBx.Text;
            string empCityUp = cityUpTxtBx.Text;
            string empCountryUp = countryUpTxtBx.Text;
            string phoneNumUp = phnUpTxtBx.Text;
            string emergNumUp = emergUpTxtBox.Text;
            string supFnameUp = sfnUpTxtBx.Text;
            string supMnameUp = smnUpTxtBx.Text;
            string supLnameUp = slnUpTxtBx.Text;
            string deptNameUp = deptUpTxtBx.Text;
            string posUp = posUpTxtBox.Text;
            string empTypeUp = "";
            string empStatUp = empStatUpComBx.SelectedItem.ToString();
            DateTime startDateUp = startUpDateTime.Value;
            DateTime endDateUp = endUpDateTime.Value;
            string reasonUp = reasonUpTxtBx.Text;

            if (fullUpRdoBtn.Checked == true)
            {
                empTypeUp = "Full Time";
            }
            else if (partUpRdoBtn.Checked == true)
            {
                empTypeUp = "Part Time";
            }


            try
            {
                SqlCommand cmd = new SqlCommand();
                SqlConnection con = new SqlConnection();
                string cmdquery = "Update_Emp";
                string constring = GetConnectionString.VFConString("HR Con", "hr", "C:\\HR Config\\HR.ini");
                con.ConnectionString = constring;
                con.Open();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = cmdquery;
                cmd.Parameters.AddWithValue("@empID", peid);
                cmd.Parameters.AddWithValue("@fname", fNameUp);
                cmd.Parameters.AddWithValue("@mname1", mName1Up);
                cmd.Parameters.AddWithValue("@mname2", mName2Up);
                cmd.Parameters.AddWithValue("@lname", lNameUp);
                cmd.Parameters.AddWithValue("@natType", natTypeUp);
                cmd.Parameters.AddWithValue("@natNum", natNumUp);
                cmd.Parameters.AddWithValue("@dob", dobUp);
                cmd.Parameters.AddWithValue("@marStat", marStatUp);
                cmd.Parameters.AddWithValue("@hloe", hloeUp);
                cmd.Parameters.AddWithValue("@upBy", upBy);
                cmd.Parameters.AddWithValue("@empAdd", empAddUp);
                cmd.Parameters.AddWithValue("@empCity", empCityUp);
                cmd.Parameters.AddWithValue("@empCountry", empCountryUp);
                cmd.Parameters.AddWithValue("@phoneNum", phoneNumUp);
                cmd.Parameters.AddWithValue("@emergNum", emergNumUp);
                cmd.Parameters.AddWithValue("@supFname", supFnameUp);
                cmd.Parameters.AddWithValue("@supMname", supMnameUp);
                cmd.Parameters.AddWithValue("@supLname", supLnameUp);
                cmd.Parameters.AddWithValue("@deptName", deptNameUp);
                cmd.Parameters.AddWithValue("@pos", posUp);
                cmd.Parameters.AddWithValue("@empType", empTypeUp);
                cmd.Parameters.AddWithValue("@empStat", empStatUp);
                cmd.Parameters.AddWithValue("@startDate", startDateUp);
                cmd.Parameters.AddWithValue("@endDate", endDateUp);
                cmd.Parameters.AddWithValue("@reason", reasonUp);
                cmd.ExecuteNonQuery();

                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please contact supervisor." +
                       " Database connection error: " + ex, "Error");
            }
        }

        private void CheckAuthorization(string userNm)
        {
            SqlConnection con = new SqlConnection();
            SqlCommand cmd = new SqlCommand();
            con.ConnectionString = GetConnectionString.VFConString("Users Con", "usr", "C:\\HR Config\\HR.ini");
            con.Open();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select secLvl from Users_TBL where userName = '" + userNm + "'";
            rdr1 = cmd.ExecuteReader();

            if (rdr1.HasRows)
            {
                rdr1.Read();

                string lvl = rdr1.GetString(0);

                if (lvl == "admin")
                {
                    activityBtn.Enabled = true;
                    deleteBtn.Enabled = true;

                    activityBtn.BackColor = Color.PeachPuff;
                    deleteBtn.BackColor = Color.IndianRed;
                }

            }
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            userLblTxt = userLbl.Text;
            CheckAuthorization(userLblTxt);
        }

        private void ClearEnterPage()
        {
            fnTxtBox.Text = "";
            m1TxtBox.Text = "";
            m2TxtBox.Text = "";
            lnTxtBox.Text = "";
            natComboBox.SelectedIndex = -1;
            idNumTxtBox.Text = "";
            dobDateTime.Value = DateTime.Now;
            marComboBox.SelectedIndex = -1;
            hloeComboBox.SelectedIndex = -1;
            addressTxtBox.Text = "";
            cityTxtBox.Text = "";
            countryTxtBox.Text = "";
            phnTxtBox.Text = "";
            emergTxtBox.Text = "";
            supFnTxtBox.Text = "";
            supMnTxtBox.Text = "";
            supLnTxtBox.Text = "";
            deptTxtBox.Text = "";
            posTxtBox.Text = "";
            btnChkClr = true;
            fullTimeRadioBtn.Checked = false;
            partTimeRadioBtn.Checked = false;
            empStatComboBox.SelectedIndex = -1;
            reasonTxtBox.Text = "";
            startDateTime.Value = DateTime.Now;
            endDateTime.Value = DateTime.Now;
        }

        private void fullTimeRadioBtn_CheckedChanged(object sender, EventArgs e)
        {
            if (btnChkClr == true)
            {
                fullTimeRadioBtn.Checked = false;
                btnChkClr = false;
            }
            else if (partTimeRadioBtn.Checked == false)
            {
                fullTimeRadioBtn.Checked = true;
            }
        }

        private void entClearBtn_Click(object sender, EventArgs e)
        {
            ClearEnterPage();
        }

        private void partTimeRadioBtn_CheckedChanged(object sender, EventArgs e)
        {
            if (btnChkClr == true)
            {
                partTimeRadioBtn.Checked = false;
                btnChkClr = false;
            }
            else if (fullTimeRadioBtn.Checked == false)
            {
                partTimeRadioBtn.Checked = true;
            }
        }

        private void empStatComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (empStatComboBox.SelectedIndex != -1 && empStatComboBox.SelectedItem.ToString() == "Inactive")
            {
                endDateTime.Visible = true;
                endDateLbl.Visible = true;
                reasonTxtBox.Visible = true;
                reasonLbl.Visible = true;
                redPicBxEnter.Visible = true;
                greenPicBxEnter.Visible = false;
            }
            else
            {
                endDateTime.Visible = false;
                endDateLbl.Visible = false;
                reasonTxtBox.Visible = false;
                reasonLbl.Visible = false;
                redPicBxEnter.Visible = false;
                greenPicBxEnter.Visible = true;
            }
        }

        private void fullUpRdoBtn_CheckedChanged(object sender, EventArgs e)
        {
            if (btnChkClr == true)
            {
                fullUpRdoBtn.Checked = false;
                btnChkClr = false;
            }
            else if (partUpRdoBtn.Checked == false)
            {
                fullUpRdoBtn.Checked = true;
            }
        }

        private void partUpRdoBtn_CheckedChanged(object sender, EventArgs e)
        {
            if (btnChkClr == true)
            {
                partUpRdoBtn.Checked = false;
                btnChkClr = false;
            }
            else if (fullUpRdoBtn.Checked == false)
            {
                partUpRdoBtn.Checked = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            UpdateEmps(seItem.Text);
            ClearUpdatePage();
            MessageBox.Show("Update Successful", "Success");
            Load_Search();
            LoadDepartments();
            LoadPositions();
            tabControl1.SelectTab(searchPage);
        }

        private void empStatUpComBx_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (empStatUpComBx.SelectedIndex != -1 && empStatUpComBx.SelectedItem.ToString() == "Inactive")
            {
                endUpDateTime.Visible = true;
                endDateUpLbl.Visible = true;
                reasonUpTxtBx.Visible = true;
                reasonUpLbl.Visible = true;
                redPicBx.Visible = true;
                greenPicBx.Visible = false;
            }
            else
            {
                endUpDateTime.Visible = false;
                endDateUpLbl.Visible = false;
                reasonUpTxtBx.Visible = false;
                reasonUpLbl.Visible = false;
                redPicBx.Visible = false;
                greenPicBx.Visible = true;
            }
        }

        private void SearchDB()
        {
            DateTime def = new DateTime(2020, 01, 01);
            string posSearch = "";
            string deptSearch = "";
            string empStatSearch = "";
            string fnameSearch = fnameSearchTxtBx.Text;
            string mnameSearch = mnameSearchTxtBx.Text;
            string lnameSearch = lnameSearchTxtBx.Text;
            DateTime dobSearch = dobSearchDateTime.Value;

            string date = dobSearch.ToString("yyyy/MM/dd");
            string defString = def.ToString("yyyy/MM/dd");

            if (posSearchDropDwn.Text != null)
            {
                posSearch = posSearchDropDwn.Text;
            }
            if (deptSearchDropDwn.Text != null)
            {
                deptSearch = deptSearchDropDwn.Text;
            }
            if (empStatComboBox.Text != null)
            {
                empStatSearch = empStatSearchComboBx.Text;
            }

            searchListView.Clear();
            searchListView.Columns.Add("", 0);
            searchListView.Columns.Add("First Name", 100);
            searchListView.Columns.Add("Father's Name", 110);
            searchListView.Columns.Add("GrandFather's Name", 110);
            searchListView.Columns.Add("Last Name", 100);
            searchListView.Columns.Add("DOB", 100);
            searchListView.Columns.Add("Nationality", 100);
            searchListView.Columns.Add("Level of Education", 100);
            searchListView.Columns.Add("Position", 100);
            searchListView.Columns.Add("Department", 100);
            searchListView.Columns.Add("Employee Status", 100);
            searchListView.Columns.Add("Type", 100);
            searchListView.Columns.Add("Start Date", 100);
            searchListView.Columns.Add("End Date", 100);
            searchListView.View = View.Details;
            ListViewItem lvi;

            SqlCommand cmd = new SqlCommand();
            SqlConnection con = new SqlConnection();
            con.ConnectionString = GetConnectionString.VFConString("HR Con", "hr", "C:\\HR Config\\HR.ini");
            con.Open();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            if ((fnameSearch != "" || fnameSearch != null) && (mnameSearch == "" || mnameSearch == null)
                && (lnameSearch == "" || lnameSearch == null) && (date == defString)
                && (posSearch == "") && (deptSearch == "")
                && (empStatSearch == ""))
            {
                cmd.CommandText = "select PEID, FName, MName1, MName2, LName, DOB, NatType, HLoE, Pos, DeptName, " +
                "EmpStat, EmpType, StartDate, EndDate from Personal_Emps join Sup_Names on " +
                "Sup_Names.EmpID = PEID join Emp_Status on Sup_Names.EmpID = Emp_Status.EmpID " +
                "where FName like'" + fnameSearch + "%'";
            }
            else if ((fnameSearch == "" || fnameSearch == null) && (mnameSearch != "" || mnameSearch != null)
                && (lnameSearch == "" || lnameSearch == null) && (date == defString)
                && (posSearch == "") && (deptSearch == "")
                && (empStatSearch == ""))
            {
                cmd.CommandText = "select PEID, FName, MName1, MName2, LName, DOB, NatType, HLoE, Pos, DeptName, " +
                    "EmpStat, EmpType, StartDate, EndDate from Personal_Emps join Sup_Names on " +
                    "Sup_Names.EmpID = PEID join Emp_Status on Sup_Names.EmpID = Emp_Status.EmpID " +
                    "where MName1 like'" + mnameSearch + "%'";
            }
            else if ((fnameSearch == "" || fnameSearch == null) && (mnameSearch == "" || mnameSearch == null)
                && (lnameSearch != "" || lnameSearch != null) && (date == defString)
                && (posSearch == "") && (deptSearch == "")
                && (empStatSearch == ""))
            {
                cmd.CommandText = "select PEID, FName, MName1, MName2, LName, DOB, NatType, HLoE, Pos, DeptName, " +
                    "EmpStat, EmpType, StartDate, EndDate from Personal_Emps join Sup_Names on " +
                    "Sup_Names.EmpID = PEID join Emp_Status on Sup_Names.EmpID = Emp_Status.EmpID " +
                    "where LName like'" + lnameSearch + "%'";
            }
            else if ((fnameSearch == "" || fnameSearch == null) && (mnameSearch == "" || mnameSearch == null)
                && (lnameSearch == "" || lnameSearch == null) && (date != defString)
                && (posSearch == "") && (deptSearch == "")
                && (empStatSearch == ""))
            {
                cmd.CommandText = "select PEID, FName, MName1, MName2, LName, DOB, NatType, HLoE, Pos, DeptName, " +
                    "EmpStat, EmpType, StartDate, EndDate from Personal_Emps join Sup_Names on " +
                    "Sup_Names.EmpID = PEID join Emp_Status on Sup_Names.EmpID = Emp_Status.EmpID " +
                    "where CONVERT(VARCHAR(10), DOB, 111) ='" + date + "'";
            }
            else if ((fnameSearch == "" || fnameSearch == null) && (mnameSearch == "" || mnameSearch == null)
                && (lnameSearch == "" || lnameSearch == null) && (date == defString)
                && (posSearch != "") && (deptSearch == "")
                && (empStatSearch == ""))
            {
                cmd.CommandText = "select PEID, FName, MName1, MName2, LName, DOB, NatType, HLoE, Pos, DeptName, " +
                    "EmpStat, EmpType, StartDate, EndDate from Personal_Emps join Sup_Names on " +
                    "Sup_Names.EmpID = PEID join Emp_Status on Sup_Names.EmpID = Emp_Status.EmpID " +
                    "where Pos ='" + posSearch + "'";
            }
            else if ((fnameSearch == "" || fnameSearch == null) && (mnameSearch == "" || mnameSearch == null)
                && (lnameSearch == "" || lnameSearch == null) && (date == defString)
                && (posSearch == "") && (deptSearch != "")
                && (empStatSearch == ""))
            {
                cmd.CommandText = "select PEID, FName, MName1, MName2, LName, DOB, NatType, HLoE, Pos, DeptName, " +
                    "EmpStat, EmpType, StartDate, EndDate from Personal_Emps join Sup_Names on " +
                    "Sup_Names.EmpID = PEID join Emp_Status on Sup_Names.EmpID = Emp_Status.EmpID " +
                    "where DeptName ='" + deptSearch + "'";
            }
            else if ((fnameSearch == "" || fnameSearch == null) && (mnameSearch == "" || mnameSearch == null)
                && (lnameSearch == "" || lnameSearch == null) && (date == defString)
                && (posSearch == "") && (deptSearch == "")
                && (empStatSearch != ""))
            {
                cmd.CommandText = "select PEID, FName, MName1, MName2, LName, DOB, NatType, HLoE, Pos, DeptName, " +
                    "EmpStat, EmpType, StartDate, EndDate from Personal_Emps join Sup_Names on " +
                    "Sup_Names.EmpID = PEID join Emp_Status on Sup_Names.EmpID = Emp_Status.EmpID " +
                    "where EmpStat ='" + empStatSearch + "'";
            }
            else if ((fnameSearch != "" || fnameSearch != null) && (mnameSearch != "" || mnameSearch != null)
                && (lnameSearch == "" || lnameSearch == null) && (date == defString)
                && (posSearch == "") && (deptSearch == "")
                && (empStatSearch == ""))
            {
                cmd.CommandText = "select PEID, FName, MName1, MName2, LName, DOB, NatType, HLoE, Pos, DeptName, " +
                    "EmpStat, EmpType, StartDate, EndDate from Personal_Emps join Sup_Names on " +
                    "Sup_Names.EmpID = PEID join Emp_Status on Sup_Names.EmpID = Emp_Status.EmpID " +
                    "where FName ='" + fnameSearch + "' and MName1 ='" + mnameSearch + "'";
            }
            else if ((fnameSearch != "" || fnameSearch != null) && (mnameSearch == "" || mnameSearch == null)
                && (lnameSearch != "" || lnameSearch != null) && (date == defString)
                && (posSearch == "") && (deptSearch == "")
                && (empStatSearch == ""))
            {
                cmd.CommandText = "select PEID, FName, MName1, MName2, LName, DOB, NatType, HLoE, Pos, DeptName, " +
                    "EmpStat, EmpType, StartDate, EndDate from Personal_Emps join Sup_Names on " +
                    "Sup_Names.EmpID = PEID join Emp_Status on Sup_Names.EmpID = Emp_Status.EmpID " +
                    "where FName ='" + fnameSearch + "' and LName ='" + lnameSearch + "'";
            }
            else if ((fnameSearch != "" || fnameSearch != null) && (mnameSearch != "" || mnameSearch != null)
                && (lnameSearch != "" || lnameSearch != null) && (date == defString)
                && (posSearch == "") && (deptSearch == "")
                && (empStatSearch == ""))
            {
                cmd.CommandText = "select PEID, FName, MName1, MName2, LName, DOB, NatType, HLoE, Pos, DeptName, " +
                    "EmpStat, EmpType, StartDate, EndDate from Personal_Emps join Sup_Names on " +
                    "Sup_Names.EmpID = PEID join Emp_Status on Sup_Names.EmpID = Emp_Status.EmpID " +
                    "where FName ='" + fnameSearch + "' and MName1 ='" + mnameSearch + "' and LName ='" + lnameSearch + "'";
            }
            else if ((fnameSearch != "" || fnameSearch != null) && (mnameSearch != "" || mnameSearch != null)
                && (lnameSearch != "" || lnameSearch != null) && (date != defString)
                && (posSearch == "") && (deptSearch == "")
                && (empStatSearch == ""))
            {
                cmd.CommandText = "select PEID, FName, MName1, MName2, LName, DOB, NatType, HLoE, Pos, DeptName, " +
                    "EmpStat, EmpType, StartDate, EndDate from Personal_Emps join Sup_Names on " +
                    "Sup_Names.EmpID = PEID join Emp_Status on Sup_Names.EmpID = Emp_Status.EmpID " +
                    "where FName ='" + fnameSearch + "' and MName1 ='" + mnameSearch + "' " +
                    "and LName ='" + lnameSearch + "' and CONVERT(VARCHAR(10), DOB, 111) = '" + date + "'";
            }
            else if ((fnameSearch != "" || fnameSearch != null) && (mnameSearch != "" || mnameSearch != null)
                && (lnameSearch != "" || lnameSearch != null) && (date == defString)
                && (posSearch != "") && (deptSearch == "")
                && (empStatSearch == ""))
            {
                cmd.CommandText = "select PEID, FName, MName1, MName2, LName, DOB, NatType, HLoE, Pos, DeptName, " +
                    "EmpStat, EmpType, StartDate, EndDate from Personal_Emps join Sup_Names on " +
                    "Sup_Names.EmpID = PEID join Emp_Status on Sup_Names.EmpID = Emp_Status.EmpID " +
                    "where FName ='" + fnameSearch + "' and MName1 ='" + mnameSearch + "' " +
                    "and LName ='" + lnameSearch + "' and Pos = '" + posSearch + "'";
            }
            else if ((fnameSearch != "" || fnameSearch != null) && (mnameSearch != "" || mnameSearch != null)
                && (lnameSearch != "" || lnameSearch != null) && (date == defString)
                && (posSearch == "") && (deptSearch != "")
                && (empStatSearch == ""))
            {
                cmd.CommandText = "select PEID, FName, MName1, MName2, LName, DOB, NatType, HLoE, Pos, DeptName, " +
                    "EmpStat, EmpType, StartDate, EndDate from Personal_Emps join Sup_Names on " +
                    "Sup_Names.EmpID = PEID join Emp_Status on Sup_Names.EmpID = Emp_Status.EmpID " +
                    "where FName ='" + fnameSearch + "' and MName1 ='" + mnameSearch + "' " +
                    "and LName ='" + lnameSearch + "' and DeptName = '" + deptSearch + "'";
            }
            else if ((fnameSearch != "" || fnameSearch != null) && (mnameSearch != "" || mnameSearch != null)
                && (lnameSearch != "" || lnameSearch != null) && (date == defString)
                && (posSearch == "") && (deptSearch == "")
                && (empStatSearch != ""))
            {
                cmd.CommandText = "select PEID, FName, MName1, MName2, LName, DOB, NatType, HLoE, Pos, DeptName, " +
                    "EmpStat, EmpType, StartDate, EndDate from Personal_Emps join Sup_Names on " +
                    "Sup_Names.EmpID = PEID join Emp_Status on Sup_Names.EmpID = Emp_Status.EmpID " +
                    "where FName ='" + fnameSearch + "' and MName1 ='" + mnameSearch + "' " +
                    "and LName ='" + lnameSearch + "' and EmpStat = '" + empStatSearch + "'";
            }
            else 
            { 
                cmd.CommandText = "select PEID, FName, MName1, MName2, LName, DOB, NatType, HLoE, Pos, DeptName, " +
                    "EmpStat, EmpType, StartDate, EndDate from Personal_Emps join Sup_Names on " +
                    "Sup_Names.EmpID = PEID join Emp_Status on Sup_Names.EmpID = Emp_Status.EmpID ";
            }

            rdr1 = cmd.ExecuteReader();

            if (rdr1.HasRows)
            {
                while (rdr1.Read())
                {
                    int id = rdr1.GetInt32(0);
                    string fn = rdr1.GetString(1);
                    string mn1 = rdr1.GetString(2);
                    string mn2 = rdr1.GetString(3);
                    string ln = rdr1.GetString(4);
                    DateTime db = rdr1.GetDateTime(5);
                    string nat = rdr1.GetString(6);
                    string hl = rdr1.GetString(7);
                    string pos = rdr1.GetString(8);
                    string dep = rdr1.GetString(9);
                    string es = rdr1.GetString(10);
                    string et = rdr1.GetString(11);
                    DateTime date1 = rdr1.GetDateTime(12);
                    DateTime date2 = rdr1.GetDateTime(13);

                    lvi = new ListViewItem(new string[] { id.ToString(), fn, mn1, mn2, ln, db.ToString("dd/MM/yyyy"), nat, hl, pos, dep, es, et, date1.ToString("dd/MM/yyyy"), date2.ToString("dd/MM/yyyy") });

                    if (es == "Active")
                        lvi.BackColor = Color.PaleGreen;
                    if (es == "Inactive")
                        lvi.BackColor = Color.LightSalmon;

                    searchListView.Items.Add(lvi);
                }
            }
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            SearchDB();
        }

        private void DeleteEmp(string peid)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                SqlConnection con = new SqlConnection();
                string cmdquery = "Delete_Emp";
                string constring = GetConnectionString.VFConString("HR Con", "hr", "C:\\HR Config\\HR.ini");
                con.ConnectionString = constring;
                con.Open();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = cmdquery;
                cmd.Parameters.AddWithValue("@empID", peid);
                cmd.ExecuteNonQuery();

                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please contact supervisor." +
                       " Database connection error: " + ex, "Error");
            }

        }

        private void deletePageBtn_Click(object sender, EventArgs e)
        {
            try
            {
                ListViewItem dItem;
                dItem = deleteListView.SelectedItems[0];
                MessageBoxButtons btn = MessageBoxButtons.YesNo;
                DialogResult result = MessageBox.Show("Are you sure you want to delete the following record?" +
                    " This action will PERMANENTLY delete this record from your database.", "WARNING!", btn);
                if (result == DialogResult.Yes)
                {
                    DeleteEmp(dItem.Text);
                    Load_DeleteListView();
                    MessageBox.Show("Record Successfully Deleted.", "Success");
                }
                else
                {
                    // do nothing
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Please select a record to delete.", "Error");
            }

        }

        private void ApplyFilter()
        {
            string usrString = userComboBox.Text;
            string actString = actionComboBox.Text;

            activityListView.Clear();
            activityListView.Columns.Add("", 0);
            activityListView.Columns.Add("Entered By", 100);
            activityListView.Columns.Add("Updated By", 100);
            activityListView.Columns.Add("DateEntered", 130);
            activityListView.Columns.Add("First Name", 100);
            activityListView.Columns.Add("Father's Name", 110);
            activityListView.Columns.Add("GrandFather's Name", 110);
            activityListView.Columns.Add("Last Name", 100);
            activityListView.Columns.Add("DOB", 100);
            activityListView.Columns.Add("Nationality", 100);
            activityListView.Columns.Add("Level of Education", 100);
            activityListView.Columns.Add("Position", 100);
            activityListView.Columns.Add("Department", 100);
            activityListView.Columns.Add("Employee Status", 100);
            activityListView.Columns.Add("Type", 100);
            activityListView.Columns.Add("Start Date", 100);
            activityListView.Columns.Add("End Date", 100);
            activityListView.View = View.Details;
            ListViewItem lvi;

            SqlCommand cmd = new SqlCommand();
            SqlConnection con = new SqlConnection();
            con.ConnectionString = GetConnectionString.VFConString("HR Con", "hr", "C:\\HR Config\\HR.ini");
            con.Open();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            if ((usrString != "" || usrString != null) && actString == "EnteredBy")
            {
                cmd.CommandText = "select PEID, FName, MName1, MName2, LName, NatType, HLoE, Pos, DeptName, EmpStat, EmpType, StartDate, EndDate, Personal_Emps.EnteredBy, Personal_Emps.UpdatedBy, Personal_Emps.DateEntered, DOB from Personal_Emps join Sup_Names on Sup_Names.EmpID = PEID join Emp_Status on Sup_Names.EmpID = Emp_Status.EmpID where Personal_Emps.EnteredBy = '" + usrString + "'";
            }
            else if ((usrString != "" || usrString != null) && actString == "UpdatedBy")
            {
                cmd.CommandText = "select PEID, FName, MName1, MName2, LName, NatType, HLoE, Pos, DeptName, EmpStat, EmpType, StartDate, EndDate, Personal_Emps.EnteredBy, Personal_Emps.UpdatedBy, Personal_Emps.DateEntered, DOB from Personal_Emps join Sup_Names on Sup_Names.EmpID = PEID join Emp_Status on Sup_Names.EmpID = Emp_Status.EmpID where Personal_Emps.UpdatedBy = '" + usrString + "'";
            }
            else if ((usrString == "" || usrString == null) && actString == "UpdatedBy")
            {
                cmd.CommandText = "select PEID, FName, MName1, MName2, LName, NatType, HLoE, Pos, DeptName, EmpStat, EmpType, StartDate, EndDate, Personal_Emps.EnteredBy, Personal_Emps.UpdatedBy, Personal_Emps.DateEntered, DOB from Personal_Emps join Sup_Names on Sup_Names.EmpID = PEID join Emp_Status on Sup_Names.EmpID = Emp_Status.EmpID where Personal_Emps.UpdatedBy != '' order by Personal_Emps.UpdatedBy";
            }
            else if ((usrString == "" || usrString == null) && actString == "EnteredBy")
            {
                cmd.CommandText = "select PEID, FName, MName1, MName2, LName, NatType, HLoE, Pos, DeptName, EmpStat, EmpType, StartDate, EndDate, Personal_Emps.EnteredBy, Personal_Emps.UpdatedBy, Personal_Emps.DateEntered, DOB from Personal_Emps join Sup_Names on Sup_Names.EmpID = PEID join Emp_Status on Sup_Names.EmpID = Emp_Status.EmpID where Personal_Emps.EnteredBy != '' order by Personal_Emps.EnteredBy";
            }
            else if ((usrString != "" || usrString != null) && actString != "EnteredBy" || actString != "UpdatedBy")
            {
                cmd.CommandText = "select PEID, FName, MName1, MName2, LName, NatType, HLoE, Pos, DeptName, EmpStat, EmpType, StartDate, EndDate, Personal_Emps.EnteredBy, Personal_Emps.UpdatedBy, Personal_Emps.DateEntered, DOB from Personal_Emps join Sup_Names on Sup_Names.EmpID = PEID join Emp_Status on Sup_Names.EmpID = Emp_Status.EmpID where Personal_Emps.EnteredBy = '" + usrString + "' or Personal_Emps.UpdatedBy = '" + usrString + "'";

            }
            rdr1 = cmd.ExecuteReader();

            if (rdr1.HasRows)
            {
                while (rdr1.Read())
                {
                    int id = rdr1.GetInt32(0);
                    string fn = rdr1.GetString(1);
                    string mn1 = rdr1.GetString(2);
                    string mn2 = rdr1.GetString(3);
                    string ln = rdr1.GetString(4);
                    string nat = rdr1.GetString(5);
                    string hl = rdr1.GetString(6);
                    string pos = rdr1.GetString(7);
                    string dep = rdr1.GetString(8);
                    string es = rdr1.GetString(9);
                    string et = rdr1.GetString(10);
                    DateTime date1 = rdr1.GetDateTime(11);
                    DateTime date2 = rdr1.GetDateTime(12);
                    string ent = rdr1.GetString(13);
                    string up = rdr1.GetString(14);
                    DateTime date3 = rdr1.GetDateTime(15);
                    DateTime date4 = rdr1.GetDateTime(16);

                    lvi = new ListViewItem(new string[] { id.ToString(), ent, up, date3.ToString(), fn, mn1, mn2, ln, date4.ToString("dd/MM/yyyy"), nat, hl, pos, dep, es, et, date1.ToString("dd/MM/yyyy"), date2.ToString("dd/MM/yyyy") });

                    if (es == "Active")
                        lvi.BackColor = Color.PaleGreen;
                    if (es == "Inactive")
                        lvi.BackColor = Color.LightSalmon;

                    activityListView.Items.Add(lvi);
                }
            }
        }

        private void filterBtn_Click(object sender, EventArgs e)
        {
            ApplyFilter();
        }

        private void clearActivityBtn_Click(object sender, EventArgs e)
        {
            userComboBox.SelectedIndex = -1;
            actionComboBox.SelectedIndex = -1;
            Load_ActivityListView();
        }

        private void ClearSearch() 
        {
            DateTime res = new DateTime(2020, 01, 01);
            fnameSearchTxtBx.Text = "";
            mnameSearchTxtBx.Text = "";
            lnameSearchTxtBx.Text = "";
            dobSearchDateTime.Value = res;
            posSearchDropDwn.SelectedIndex = -1;
            deptSearchDropDwn.SelectedIndex = -1;
            empStatSearchComboBx.SelectedIndex = -1;
        }

        private void clearSearchBtn_Click(object sender, EventArgs e)
        {
            ClearSearch();
            Load_Search();
        }

        private void Form5_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
