using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Mail;
using BLL;
using System.Text.RegularExpressions;

namespace FarmchemCallLog
{
    public partial class Form1 : Form
    {
        public rgaForm rga;
        public AddAddressForm addAddress;
        BusinessLogicLayer bll = new BusinessLogicLayer();




        public Form1()
        {
            InitializeComponent();
            contactPhone.Select();
        }
        

        //populates contactNameField and updates displayed info to calls from phone
        private void ContactPhone_Leave(object sender, EventArgs e)
        {
            if (!IsPhoneNumber(contactPhone.Text))
            {
                return;
            }
            ClearCallerData();
            SetContactName();
            if (contactName.Items.Count > 0)
            {
                SetContactEmail();
                SetCustomerCode();
                SetCompanyName();
                SetCityStateZip();
                PopulateDataGridViewByPhoneCompanyCity();
            }

        }
        private void ContactName_Leave(object sender, EventArgs e)
        {
            try
            {
                SetContactEmail();
                SetCompanyName();
                SetCityStateZip();
            }
            catch
            {
                MessageBox.Show("Error populating data with contact name.");
            }
                
        }

        public void PopulateDataGridViewByPhoneCompanyCity()
        {
            dataGridView1.DataSource = bll.GetGridViewData(contactPhone.Text, companyName.Text, companyCity.Text);
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (SaveFormToDatabase() != 0)
            {
                MessageBox.Show("Saved");
            }
            else
            {
                MessageBox.Show("Did not save form");
            }
            PopulateDataGridViewByPhoneCompanyCity();
        }



        private void SetContactName()
        {
            contactName.Text = "";
            contactName.Items.Clear();
            contactName.Items.AddRange(bll.GetNameList(contactPhone.Text));
            contactName.Text = contactName.Items[0].ToString();
        }

        private void SetContactEmail()
        {
            contactEmail.Text = "";
            contactEmail.Items.Clear();
            contactEmail.Items.AddRange(bll.GetCustomerEmail(contactPhone.Text, contactName.Text));
            contactEmail.Text = contactEmail.Items[0].ToString();
        }

        private void SetCustomerCode()
        {
            customerCode.Text = "";
            customerCode.Items.Clear();
            customerCode.Items.AddRange(bll.GetCustomerCode(contactPhone.Text, contactName.Text));
            customerCode.Text = customerCode.Items[0].ToString();
        }

        private void SetCompanyName()
        {
            companyName.Text = "";
            companyName.Items.Clear();
            companyName.Items.AddRange(bll.GetCompanyName(customerCode.Text));
            companyName.Text = companyName.Items[0].ToString();
        }


        private void SetCityStateZip()
        {
            comboCityStateZip.Text = "";
            comboCityStateZip.Items.Clear();
            comboCityStateZip.Items.AddRange(bll.GetCompanyCityStateZip(contactPhone.Text, customerCode.Text));
            comboCityStateZip.Text = comboCityStateZip.Items[0].ToString();
        }

        public int SaveFormToDatabase()
        {
            TurnValidationOnForAll();
            if (this.ValidateChildren())
            {
                TurnValidationOffForAll();
                return bll.SaveToDatabase(contactPhone.Text, contactName.Text, contactEmail.Text, customerCode.Text, companyName.Text, companyCity.Text, companyState.Text, companyZip.Text, originalSalesOrder.Text, partNumber.Text, reasonForCall.Text, notesParagraph.Text, callDate.Text);
                
            }
            else
            {
                TurnValidationOffForAll();
                return 0;
            }
            
        }

        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        private void TurnValidationOnForAll()
        {
            foreach(Control con in this.Controls)
            {
                con.CausesValidation = true;
            }
        }

        private void TurnValidationOffForAll()
        {
            foreach(Control con in this.Controls)
            {
                con.CausesValidation = false;
            }
        }

        private void AddRepToEmail_Click(object sender, EventArgs e)
        {
            try
            {
                if (repEmail.Text.ToString() == "")
                {
                    return;
                }
                emails.Text += repEmail.Text.Trim() + ";";
                btnEmailRep.Text += " " + repEmail.Text.Replace("@farmchem.com", "");
            }
            catch (Exception)
            {
                throw;
            }
        }


        //convertToRga button, opens an RGA form
        private void ConvertToRga_Click(object sender, EventArgs e)
        {
                rga = new rgaForm();
                rga.Customer_Number.Text = this.customerCode.Text.Trim();
                rga.Contact_Name.Text = this.contactName.Text.Trim();
                rga.Contact_Phone.Text = this.contactPhone.Text.Trim();
                rga.Contact_Email.Text = this.contactEmail.Text.Trim();
                rga.Complaint.Text = this.notesParagraph.Text.Trim();
                rga.CUSTOMER.Text = this.companyName.Text.Trim();
                rga.SALES_ORDER.Text = this.originalSalesOrder.Text.Trim();
                rga.Return_Item_A.Text = this.partNumber.Text.Trim();
                rga.ShowDialog();
        }

        //clears the form to create a new call
        private void BtnNewCall_Click(object sender, EventArgs e)
        {
            try
            {
                contactPhone.Text = "";
                ClearCallerData();
                originalSalesOrder.Text = "";
                partNumber.Text = "";
                reasonForCall.Text = "";
                notesParagraph.Text = "";
                repEmail.Text = "";
                completedAnswer.Checked = false;
                btnEmailRep.Text = "Email to";
                emails.Text = "";
                contactPhone.Select();
            }
            catch (Exception)
            {

                throw;
            }
        }

        //clears the contact name - company zip fields
        private void ClearCallerData()
        {
            try
            {
                contactName.Text = "";
                contactName.Items.Clear();
                contactEmail.Text = "";
                contactEmail.Items.Clear();
                customerCode.Text = "";
                customerCode.Items.Clear();
                companyName.Text = "";
                companyName.Items.Clear();
                companyCity.Text = "";
                companyState.Text = "";
                companyZip.Text = "";
            }
            catch (Exception)
            {

                throw;
            }
        }

        //populates the notes from a previous call for a bigger viewing area
        private void PopulateNotesFromCall_Click(object sender, EventArgs e)
        {
            try
            {
                contactNotes.Visible = true;
                contactNotes.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            }
            catch (Exception)
            {
                throw;
            }
        }


        //generates email to be sent to an outside
        //I want 'btnEmailRep to'(Button3_Click) to pull open a new outlook message for us to look over before actually sending
        // -'Email to' button 
        private void EmailRep_Click(object sender, EventArgs e)
        {
            try
            {
                var fromAddress = new MailAddress("fccalllogtest@gmail.com", "Eric Kobliska");
                //I would use 'emails.Text' in place of erickobliska@gmail.com here
                var toAddress = new MailAddress("erickobliska@gmail.com", "Eric K");
                const string FROMPASSWORD = "thisispassword";
                string subject = reasonForCall.Text + " call from " + contactName.Text + " at " + companyName.Text;
                string body = contactName.Text + " called from " + companyName.Text + " in " + companyCity.Text + " " + companyState.Text + ". Their contact info is " + contactPhone.Text + " & " + contactEmail.Text + ". The reason they called: " + notesParagraph.Text + ". This would be e-mailed to " + emails.Text;


                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, FROMPASSWORD)
                };
                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body
                })

                {
                    smtp.Send(message);
                    MessageBox.Show("Email sent successfully.");
                }
                this.checkBox1.Checked = true;
                this.checkBox1.Text = "Call record sent to rep(s).";
            }
            catch (Exception)
            {

                throw;
            }
        }



        private static bool IsPhoneNumber(string number)
        {
            return Regex.Match(number, @"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$").Success;
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private void CompanyState_Validating(object sender, CancelEventArgs e)
        {
            if (companyState.Text.Length > 10)
            {
                MessageBox.Show("State is too long");
                e.Cancel = true;
            }
        }

        private void ContactPhone_Validating(object sender, CancelEventArgs e)
        {
            if(!IsPhoneNumber(contactPhone.Text))
            {
                MessageBox.Show("Please enter a valid phone number");
                e.Cancel = true;
            }
            if(contactPhone.TextLength > 20)
            {
                MessageBox.Show("Contact phone is too long - please re-enter.");
                e.Cancel = true;
            }
        }


        private void ContactName_Validating(object sender, CancelEventArgs e)
        {
            if(contactName.Text.Length > 50)
            {
                MessageBox.Show("Contact Name is too long");
                e.Cancel = true;
            }
        }

        private void ContactEmail_Validating(object sender, CancelEventArgs e)
        {
            if(contactEmail.Text.Length == 0)
            {
                return;
            }
            if(!IsValidEmail(contactEmail.Text))
            {
                DialogResult result = MessageBox.Show("It appears your email may not be formatted correctly, would you like to save anyway?", "Warning!", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    e.Cancel = false;

                }
                else if (result == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
            if (contactEmail.Text.Length > 50)
            {
                MessageBox.Show("Email is too long - please re-enter.");
                e.Cancel = true;
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            using(addAddress = new AddAddressForm())
            {
                if(addAddress.ShowDialog() == DialogResult.OK)
                {
                    companyCity.Text = addAddress.addCompanyCity.Text;
                    companyState.Text = addAddress.addCompanyState.Text;
                    companyZip.Text = addAddress.addCompanyZip.Text;
                }
            }
            comboCityStateZip.Items.Add($"{ companyCity.Text}, {companyState.Text}, {companyZip.Text}");
            comboCityStateZip.SelectedItem = comboCityStateZip.Items[comboCityStateZip.Items.Count - 1];
        }

        private void CustomerCode_Validating(object sender, CancelEventArgs e)
        {
            if (customerCode.Text.Length > 10)
            {
                MessageBox.Show("Customer Code is too long - please re-enter.");
                e.Cancel = true;
            }
        }

        private void CompanyCity_Validating(object sender, CancelEventArgs e)
        {
            if (companyCity.Text.Length > 50)
            {
                MessageBox.Show("Company City is too long - please Add a new address.");
                e.Cancel = true;
            }
        }

        private void CompanyState_Validating_1(object sender, CancelEventArgs e)
        {
            if (companyState.Text.Length > 15)
            {
                MessageBox.Show("Company State is too long - please Add a new address.");
                e.Cancel = true;
            }
        }

        private void CompanyZip_Validating(object sender, CancelEventArgs e)
        {
            if (companyZip.Text.Length > 10)
            {
                MessageBox.Show("Company Zip code is too long - please Add a new address.");
                e.Cancel = true;
            }
        }

        private void ReasonForCall_Validating(object sender, CancelEventArgs e)
        {
            if (reasonForCall.Text.Length > 50)
            {
                MessageBox.Show("Reason for call is too long - please be more succinct.");
                e.Cancel = true;
            }
        }

        private void PartNumber_Validating(object sender, CancelEventArgs e)
        {
            if (partNumber.Text.Length > 50)
            {
                MessageBox.Show("Part number field is too long - please be more succint.");
                e.Cancel = true;
            }
        }

        private void OriginalSalesOrder_Validating(object sender, CancelEventArgs e)
        {
            if (originalSalesOrder.Text.Length > 50)
            {
                MessageBox.Show("Sales order field text is too long - please be more succint.");
                e.Cancel = true;
            }
        }


    }
}
