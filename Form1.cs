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
        public SearchForm searchForm;
        BusinessLogicLayer bll = new BusinessLogicLayer();




        public Form1()
        {
            InitializeComponent();
            contactPhone.Select();
        }


        private void ContactPhone_LostFocus(object sender, EventArgs e)
        {
            if(String.IsNullOrEmpty(contactPhone.Text))
            {
                return;
            }
            if(String.IsNullOrEmpty(contactName.Text) && String.IsNullOrEmpty(contactEmail.Text) && String.IsNullOrEmpty(customerCode.Text) && String.IsNullOrEmpty(companyName.Text)
                && String.IsNullOrEmpty(comboCityStateZip.Text) && String.IsNullOrEmpty(reasonForCall.Text) && String.IsNullOrEmpty(repEmail.Text) && String.IsNullOrEmpty(notesParagraph.Text))
            {
                ClearCallerData();
                ClearContactTabsAndNotes();
                SetContactName();

                if (contactName.Items.Count > 0)
                {
                    SetContactEmail();
                    SetCustomerCode();
                    SetCompanyName();
                    SetCityStateZip();
                    PopulateDataGridViewByPhoneCompanyCity();
                }
                SelectTabBasedOnSelectedContact();
            }
            else
            {
                DialogResult result = MessageBox.Show("Do you want to delete the info currently in form?", "Warning!", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (result == DialogResult.Yes)
                {
                    ClearCallerData();
                    ClearContactTabsAndNotes();
                    SetContactName();

                    if (contactName.Items.Count > 0)
                    {
                        SetContactEmail();
                        SetCustomerCode();
                        SetCompanyName();
                        SetCityStateZip();
                        PopulateDataGridViewByPhoneCompanyCity();
                    }
                    SelectTabBasedOnSelectedContact();
                }
                else if (result == DialogResult.No)
                {
                    return;
                }
            }
        }

        private void ContactName_LostFocus(object sender, EventArgs e)
        {
            try
            {
                if (contactTabControl.TabPages.ContainsKey(contactName.Text) == false && contactName.Text != "")
                {
                    var textbox = new RichTextBox() { Height = 166, Width = 366 };
                    textbox.Text = "";
                    var newTab = new TabPage(contactName.Text);
                    contactTabControl.TabPages.Add(newTab);
                    newTab.Name = contactName.Text;
                    newTab.Controls.Add(textbox);
                }
                SetContactEmail();
                SetCompanyName();
                SetCityStateZip();


            }
            catch
            {
                MessageBox.Show("Error populating data with contact name.");
            }
            SelectTabBasedOnSelectedContact();
        }

        public void PopulateDataGridViewByPhoneCompanyCity()
        {
            callLogGridView.DataSource = bll.GetGridViewData(contactPhone.Text, companyName.Text, companyCity.Text);
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (SaveFormToDatabase() != 0)
            {
                MessageBox.Show("Saved call");
            }
            else
            {
                MessageBox.Show("Did not save call");
            }
            PopulateDataGridViewByPhoneCompanyCity();
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (UpdateFormInDatabase() != 0)
            {
                MessageBox.Show("Record Updated");
            }
            else
            {
                MessageBox.Show("Did not update form");
            }
            PopulateDataGridViewByPhoneCompanyCity();
        }

        public void CreateBusinessNotes()
        {
            var textbox = new RichTextBox() { Height = 166, Width = 366 };
            textbox.Text = bll.GetBusinessNotes(contactPhone.Text, customerCode.Text);
            businessTabControl.TabPages[0].Controls.Add(textbox);
        }

        private void SetContactName()
        {
            contactName.Text = "";
            contactName.Items.Clear();
            var list = bll.GetNameList(contactPhone.Text);
            if (list.Length == 1 && list[0] == "" || list.Length == 0)
            {

            }
            else
            {
                contactName.Items.AddRange(list);
                contactName.Text = contactName.Items[0].ToString();
                CreateContactsAndNotesTabs(list);
            }

        }


        public void CreateContactsAndNotesTabs(string[] list)
        {
            ClearContactTabsAndNotes();

            if (list.Length > 0)
            {
                foreach (var p in list)
                {
                    var textbox = new RichTextBox() { Height = 166, Width = 366 };
                    textbox.Text = bll.GetNameNotes(contactPhone.Text, p.ToString());
                    var newTab = new TabPage(p.ToString());
                    contactTabControl.TabPages.Add(newTab);
                    newTab.Name = p.ToString();
                    newTab.Controls.Add(textbox);
                }
            }
            SelectTabBasedOnSelectedContact();
            contactName.Select();
        }

        public void ClearContactTabsAndNotes()
        {
            //contactTabControl.TabPages.Clear();
            //clear the controls from each tab page
            for (int i = 0; i < contactTabControl.TabPages.Count; i++)
            {
                if (contactTabControl.TabPages[i].Controls.Count > 0)
                {
                    contactTabControl.TabPages[0].Controls.Clear();
                }
            }
            while (contactTabControl.TabCount > 1)
            {
                contactTabControl.TabPages.RemoveAt(1);
            }

        }

        private void ClearCompanyNotesText()
        {
            businessTabControl.TabPages[0].Controls.Clear();
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
            CreateBusinessNotes();
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
            string contactNotes;
            string companyNotes = "";
            if (businessTabControl.TabPages[0].Controls[0].Text != "")
            {
                companyNotes = businessTabControl.TabPages[0].Controls[0].Text;
            }
            if (contactTabControl.TabPages.Count > 1)
            {
                try
                {
                    contactNotes = contactTabControl.TabPages[contactName.Text].Controls[0].Text;
                }
                catch { contactNotes = ""; }
            }
            else
            {
                contactNotes = "";
            }
            TurnValidationOnForAll();
            if (this.ValidateChildren())
            {
                TurnValidationOffForAll();
                return bll.SaveToDatabase(contactPhone.Text, contactName.Text, contactEmail.Text, customerCode.Text, companyName.Text, companyCity.Text, companyState.Text, companyZip.Text, reasonForCall.Text, notesParagraph.Text, callDate.Text, repEmail.Text, contactNotes, companyNotes);

            }
            else
            {
                TurnValidationOffForAll();
                return 0;
            }

        }

        public int UpdateFormInDatabase()
        {
            string contactNotes;
            string companyNotes = "";
            if (businessTabControl.TabPages[0].Controls[0].Text != "")
            {
                companyNotes = businessTabControl.TabPages[0].Controls[0].Text;
            }
            if (contactTabControl.TabPages.Count > 1)
            {
                try
                {
                    contactNotes = contactTabControl.TabPages[contactName.Text].Controls[0].Text;
                }
                catch { contactNotes = ""; }
            }
            else
            {
                contactNotes = "";
            }
            return bll.UpdateCallLogRecord(Convert.ToInt32(callRecord.Text), contactPhone.Text, contactName.Text, contactEmail.Text, customerCode.Text, companyName.Text, companyCity.Text, companyState.Text, companyZip.Text, reasonForCall.Text, notesParagraph.Text, callDate.Text, repEmail.Text, contactNotes, companyNotes);
        }



        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        private void TurnValidationOnForAll()
        {
            foreach (Control con in this.Controls)
            {
                con.CausesValidation = true;
            }
        }

        private void TurnValidationOffForAll()
        {
            foreach (Control con in this.Controls)
            {
                con.CausesValidation = false;
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
            rga.ShowDialog();
        }

        //clears the form to create a new call
        private void BtnNewCall_Click(object sender, EventArgs e)
        {
            try
            {
                contactPhone.Text = "";
                ClearCallerData();
                ClearContactTabsAndNotes();
                ClearCompanyNotesText();
                reasonForCall.Text = "";
                notesParagraph.Text = "";
                outsideRep.Text = "";
                completedAnswer.Checked = false;
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
                comboCityStateZip.Items.Clear();
                notesParagraph.Text = "";
                completedAnswer.Checked = false;
                repEmail.Text = "";
                reasonForCall.Text = "";
                ClearCompanyNotesText();
                CreateBusinessNotes();
            }
            catch (Exception)
            {
                MessageBox.Show("Hi guy/gal, there was an issue trying to ClearCallerData(). Please snip the screen and send to IT/Eric.");
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
                string subject = $"{reasonForCall.Text} call from {contactName.Text} at {companyName.Text}";
                string body = $"{contactName.Text} called from {companyName.Text} in {comboCityStateZip.Text}. Their contact info is {contactPhone.Text} & {contactEmail.Text}. The reason they called: {notesParagraph.Text}.";


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



        private void ContactPhone_Validating(object sender, CancelEventArgs e)
        {
            if (!IsPhoneNumber(contactPhone.Text))
            {
                MessageBox.Show("Please enter a valid phone number");
                e.Cancel = true;
            }
            if (contactPhone.TextLength > 20)
            {
                MessageBox.Show("Contact phone is too long - please re-enter.");
                e.Cancel = true;
            }
        }


        private void ContactName_Validating(object sender, CancelEventArgs e)
        {
            if (contactName.Text.Length > 50)
            {
                MessageBox.Show("Contact Name is too long");
                e.Cancel = true;
            }
        }

        private void ContactEmail_Validating(object sender, CancelEventArgs e)
        {
            if (contactEmail.Text.Length == 0)
            {
                return;
            }
            if (!IsValidEmail(contactEmail.Text))
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

        private void AddAddress_Click(object sender, EventArgs e)
        {
            using (addAddress = new AddAddressForm())
            {
                if (addAddress.ShowDialog() == DialogResult.OK)
                {
                    companyCity.Text = addAddress.addCompanyCity.Text.Replace(",", "");
                    companyState.Text = addAddress.addCompanyState.Text.Replace(",", "");
                    companyZip.Text = addAddress.addCompanyZip.Text.Replace(",", "");
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

        private void CompanyState_Validating(object sender, CancelEventArgs e)
        {
            if (companyState.Text.Length > 10)
            {
                MessageBox.Show("State is too long");
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


        //this is based on combocitystatezip being saved in the database with ', ' between each value
        //all commas removed from add address
        private void ComboCityStateZip_SelectedIndexChanged(object sender, EventArgs e)
        {
            companyCity.Text = "";
            companyState.Text = "";
            companyZip.Text = "";
            string[] line = comboCityStateZip.Text.Split(',');
            if (line.Length == 3)
            {
                companyCity.Text = line[0].Trim();
                companyState.Text = line[1].Trim();
                companyZip.Text = line[2].Trim();
            }

        }

        private void SelectTabBasedOnSelectedContact()
        {
            try
            {
                if (contactName.Text == "" || contactTabControl.TabCount <= 1)
                {
                    return;
                }
                else
                {
                    contactTabControl.SelectTab(contactName.Text);
                }
            }
            catch
            {

            }
            
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you want to delete the currently loaded call?", "Warning!", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {

            }
            else if (result == DialogResult.No)
            {
                return;
            }
        }

        private void LoadCallBasedOnGrid_Click(object sender, EventArgs e)
        {
            if(callLogGridView.SelectedCells.Count == 0 || Convert.ToInt32(callLogGridView.Rows[callLogGridView.CurrentRow.Index].Cells[0].Value.ToString()) < 1)
            {
                MessageBox.Show("Guy/gal, please select a call from the below grid and try again.");
                return;
            }
            DialogResult result = MessageBox.Show("Do you want to overwrite information in the form currently?", "Warning!", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (result == DialogResult.Yes)
            {
                var recordNum = callLogGridView.Rows[callLogGridView.CurrentRow.Index].Cells[0].Value.ToString();
                PopulateCallLogFieldsByDataTable(bll.GetDataTableOfCallLogByRecordNumber(recordNum));
                callRecord.Text = recordNum;
                SelectTabBasedOnSelectedContact();
            }
            else if (result == DialogResult.No)
            {
                return;
            }
        }

        private void PopulateCallLogFieldsByDataTable(DataTable table)
        {
            contactPhone.Text = table.Rows[0]["contactPhone"].ToString();
            contactName.Text = table.Rows[0]["contactName"].ToString();
            contactEmail.Text = table.Rows[0]["contactEmail"].ToString();
            customerCode.Text = table.Rows[0]["customerCode"].ToString();
            companyName.Text = table.Rows[0]["companyName"].ToString();
            companyCity.Text = table.Rows[0]["companyCity"].ToString();
            companyState.Text = table.Rows[0]["companyState"].ToString();
            companyZip.Text = table.Rows[0]["companyZip"].ToString();
            reasonForCall.Text = table.Rows[0]["reasonForCall"].ToString();
            notesParagraph.Text = table.Rows[0]["notesParagraph"].ToString();
            if(table.Rows[0]["customerCode"].ToString() == "yes")
            {
                completedAnswer.Checked = true;
            }
            outsideRep.Text = table.Rows[0]["outsideRep"].ToString();
            CreateContactsAndNotesTabs(bll.GetNameList(contactPhone.Text));
            CreateBusinessNotes();
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            using (searchForm = new SearchForm())
            {
                if (searchForm.ShowDialog() == DialogResult.OK)
                {
                    callLogGridView.DataSource = bll.GetCallLogDataTableBySearchFromDB(searchForm.SearchValue);
                }
            }
        }

        private void ContactName_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectTabBasedOnSelectedContact();
            contactName.Select();
        }
    }
}
