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
      
        public First_Window()
        {
            InitializeComponent();
            LoadContactList();
        }

        public void LoadContactList() {

            List<contactClass> contacts = SQLiteAccess.LoadContact();

            //contacts.Add(new contactClass { ContactID=1 ,FirstName = "Atheesh", LastName="Rathnaweera" });
            //contacts.Add(new contactClass { ContactID = 2, FirstName = "Buddhika", LastName = "Rathnaweera" });

            int numberOfContacts = contacts.Count;
            Console.WriteLine("Found : "+numberOfContacts+" Data "+contacts[0].FirstName+" "+contacts[1].FirstName);

            foreach (contactClass item in contacts.OrderBy(x => x.FirstName))
                Console.WriteLine(item);

            WiredUpPeopleList(contacts);
        }

        public void WiredUpPeopleList(List<contactClass> resContacts) {
            contactListBox.DataSource = null;
            contactListBox.DataSource = resContacts;
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

        public bool ValidateEntries() {

            bool success = false;

            if (!string.IsNullOrEmpty(txtboxFirstName.Text) && !string.IsNullOrEmpty(txtboxContactNumber.Text))
            {
                return success = true;
            }
            else {
                return success;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //Get values from input fields to save 
            Console.WriteLine("Add button clicked.");
            contactClass res = new contactClass();
            res = CreateAObject();
            string obj = res.FirstName + " "+ res.LastName +" "+res.ContactNumber+" "+res.Address;
            
            Console.WriteLine("obj : "+obj);

            if (ValidateEntries())
            {
                bool result = SQLiteAccess.SaveAContact(res);

                if (result)
                {

                    ClearFields();
                    MessageBox.Show("New contact saved.");
                }
                else
                {

                }
            }
            else {
                MessageBox.Show("First name and Contact number must be filled!");
            }
        
           

     

            //DataTable dt = SQLiteAccess.Select();
            //contactList.DataSource = dt;

        }

        public void ClearFields() {
            
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
            ClearFields();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            ShowUpdateForm();

           
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadContactList();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

            ShowMyDialogBox();

          /*  */
        }

        private void First_Window_Load(object sender, EventArgs e)
        {
            
        }

        public void ShowUpdateForm() {
            UpdateForm update = new UpdateForm();
            update.ShowDialog(this);
        }

        public void ShowMyDialogBox()
        {
            Form2 testDialog = new Form2();
           
            // Show testDialog as a modal dialog and determine if DialogResult = OK.
            if (testDialog.ShowDialog(this) == DialogResult.OK)
            {
                if (!string.IsNullOrEmpty(testDialog.txtBoxDeleteId.Text) ){
                    // Read the contents of testDialog's TextBox.
                    string id = testDialog.txtBoxDeleteId.Text;
                    testDialog.Dispose();
                    Console.WriteLine("Ok button clicked! This is the id: " + id);


                    contactClass temp = new contactClass();
                    temp = CreateAObject();
                    int x = 0;

                    Int32.TryParse(id, out x);

                    temp.ContactID = x;

                    bool res = SQLiteAccess.Delete(temp);

                    if (res)
                    {
                        ClearFields();
                        MessageBox.Show("Deleted successfully.");
                    }
                    else
                    {
                        MessageBox.Show("Error occured. Nothing deleted.");
                    }
                }
                else {
                    MessageBox.Show("Nothing deleted. Please set valid ID to delete!");
               
                }

            }  
            else
            {
                testDialog.Dispose();
               
                //this.txtBoxDeleteId.Text = "Cancelled";
            }
           
        }
    }
}
