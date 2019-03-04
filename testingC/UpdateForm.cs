using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using testingC.ProjectClasses;

namespace testingC
{
    public partial class UpdateForm : Form
    {
        public UpdateForm()
        {
            InitializeComponent();
        }

        public contactClass CreateAObject()
        {
            //Create a new contactClass object to pass add and update methods

            contactClass tempContact = new contactClass();

            tempContact.FirstName = UpdateFirstName.Text;
            tempContact.LastName = UpdateLastName.Text;
            tempContact.ContactNumber = UpdateContactNumber.Text;
            tempContact.Address = UpdateAddress.Text;
            tempContact.Gender = cmbUpdateGender.Text;

            return tempContact;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (ValidateEntries()) {
                Console.WriteLine("Update button clicked.");
                contactClass res = new contactClass();
                res = CreateAObject();

                string idString = UpdateID.Text;
                int x = 0;

                 Int32.TryParse(idString, out x);

                res.ContactID = x;

                bool result = SQLiteAccess.UpdateAContact(res);

                if (result)
                {
                    this.Close();
                    ClearFields();
                    MessageBox.Show("Updated successfully.");
                }
            }

        }

        public void ClearFields()
        {
            UpdateID.Text = "";
            UpdateFirstName.Text = "";
            UpdateLastName.Text = "";
            UpdateContactNumber.Text = "";
            UpdateAddress.Text = "";
            cmbUpdateGender.Text = "";

        }

        public bool ValidateEntries()
        {
            bool success = false;

            if (!string.IsNullOrEmpty(UpdateFirstName.Text) && !string.IsNullOrEmpty(UpdateContactNumber.Text) && !string.IsNullOrEmpty(UpdateID.Text))
            {
                return success = true;
            }
            else
            {
                return success;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
