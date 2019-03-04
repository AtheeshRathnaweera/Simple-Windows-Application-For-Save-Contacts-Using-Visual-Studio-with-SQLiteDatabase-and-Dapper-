using Dapper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using testingC.ProjectClasses;

namespace testingC
{
    

    public partial class First_Window : Form
    {


       List<contactClass> contacts = new List<contactClass>();
      

        public First_Window()
        {
            InitializeComponent();
            LoadContactList();
        }

        public void LoadContactList() {
           
            contacts = SQLiteAccess.LoadContact();

            //contacts.Add(new contactClass { ContactID=1 ,FirstName = "Atheesh", LastName="Rathnaweera" });
            //contacts.Add(new contactClass { ContactID = 2, FirstName = "Buddhika", LastName = "Rathnaweera" });

            int numberOfContacts = contacts.Count;
            Console.WriteLine("Found : "+numberOfContacts+" Data "+contacts[0].FirstName+" "+contacts[1].FirstName);

      


            WiredUpPeopleList();
        }

        public void WiredUpPeopleList() {
            contactListBox.DataSource = null;
            contactListBox.DataSource = contacts;
            contactListBox.DisplayMember = "FullName";
        }

        public contactClass CreateAObject() {
            //Create a new contactClass object to pass add and update methods

            contactClass tempContact = new contactClass();

            tempContact.FirstName = txtboxFirstName.Text;
            tempContact.LastName = txtboxLastName.Text;
            tempContact.ContactNumber = txtboxContactNumber.Text;
            tempContact.Address = txtboxAddress.Text;
            tempContact.Gender = cmbGender.Text;

            return tempContact;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void cmbGender_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //Get values from input fields to save 
            Console.WriteLine("Add button clicked.");
            contactClass res = new contactClass();
            res = CreateAObject();
            string obj = res.FirstName + " "+ res.LastName +" "+res.ContactNumber+" "+res.Address;
            
            Console.WriteLine("obj : "+obj);

        
            bool result = SQLiteAccess.SaveAContact(res);

            if (result)
            {
               
                ClearFields();
                MessageBox.Show("New contact saved.");
            }
            else {

            }

     

            //DataTable dt = SQLiteAccess.Select();
            //contactList.DataSource = dt;

        }

        public void ClearFields() {
            txtboxContactID.Text = "";
            txtboxFirstName.Text = "";
            txtboxLastName.Text = "";
            txtboxContactNumber.Text = "";
            txtboxAddress.Text = "";
            cmbGender.Text="";

        }

      

        private static string LoadConnectionString(string id = "Default")
        {
            Console.WriteLine("LoadConnectionString method executed." + ConfigurationManager.ConnectionStrings[id].ConnectionString);
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }

        private void button4_Click(object sender, EventArgs e) {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Update button clicked.");
            contactClass res = new contactClass();
            res = CreateAObject();
            int x = 0;

            Int32.TryParse(txtboxContactID.Text, out x);

            res.ContactID = x;

            bool result = SQLiteAccess.UpdateAContact(res);

            if (result) {

                ClearFields();
                MessageBox.Show("Updated successfully.");
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadContactList();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            contactClass temp = new contactClass();
            temp = CreateAObject();
            int x = 0;

            Int32.TryParse(txtboxContactID.Text, out x);

            temp.ContactID = x;

            bool res = SQLiteAccess.Delete(temp);

            if (res) {
                ClearFields();
                MessageBox.Show("Deleted successfully.");
            }
            else {

            }
        }
    }
}
