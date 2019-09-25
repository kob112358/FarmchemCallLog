using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using Outlook = Microsoft.Office.Interop.Outlook;
using BLL;


namespace FarmchemCallLog
{
    public partial class CallLogUI : Form
    {
        private readonly BusinessLogicLayer bll = new BusinessLogicLayer();


        public CallLogUI()
        {
            InitializeComponent();
            contactPhone.Select();
        }

        private void ContactPhone_LostFocus(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(contactPhone.Text))
                {
                    return;
                }
                //if all fields are empty, then auto-populate the fields
                if (String.IsNullOrEmpty(contactName.Text) && String.IsNullOrEmpty(contactEmail.Text) && String.IsNullOrEmpty(customerCode.Text) && String.IsNullOrEmpty(companyName.Text)
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
                    if (contactTabControl.TabPages.ContainsKey(contactName.Text) == false)
                    {
                        contactName.Select();
                    }
                    else
                    {
                        SelectTabBasedOnSelectedContact();
                    }
                }
                //if some fields have info, then ask user if they want to overwrite everything
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
                        if (contactTabControl.TabPages.ContainsKey(contactName.Text) == false)
                        {
                            contactName.Select();
                        }
                        else
                        {
                            SelectTabBasedOnSelectedContact();
                            contactName.Select();
                        }
                    }
                    else if (result == DialogResult.No)
                    {
                        return;
                    }
                }
            }
            catch
            {
                MessageBox.Show("Hi guy/gal. You caused error #2 - contact Eric");
            }
        }

        private void ContactName_LostFocus(object sender, EventArgs e)
        {
            bool createdNewTab = false;
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
                    createdNewTab = true;
                }
                SetContactEmail();
                SelectTabBasedOnSelectedContact();
                if(createdNewTab)
                {
                    contactEmail.Select();
                }
            }
            catch
            {
                MessageBox.Show("Hi guy/gal. You caused error #3 - contact Eric");
            }
        }

        private void CustomerCode_LostFocus(object sender, EventArgs e)
        {
            if(comboCityStateZip.Text == "")
            {
                SetCityStateZip();
            }
        }

        public void PopulateDataGridViewByPhoneCompanyCity()
        {
            try
            {
                callLogGridView.DataSource = bll.GetGridViewData(contactPhone.Text, companyName.Text, companyCity.Text);
            }
            catch
            {
                MessageBox.Show("Hi guy/gal. You caused error #4 - contact Eric");
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
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
            catch
            {
                MessageBox.Show("Hi guy/gal. You caused error #5 - contact Eric");
            }
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            try
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
            catch
            {
                MessageBox.Show("Hi guy/gal. You caused error #6 - contact Eric");
            }
        }

        public void CreateBusinessNotes()
        {
            try
            {
                var textbox = new RichTextBox() { Height = 166, Width = 366 };
                textbox.Text = bll.GetBusinessNotes(contactPhone.Text, customerCode.Text);
                businessTabControl.TabPages[0].Controls.Add(textbox);
            }
            catch
            {
                MessageBox.Show("Hi guy/gal. You caused error #7 - contact Eric");
            }
        }

        private void SetContactName()
        {
            try
            {
                contactName.Text = "";
                contactName.Items.Clear();
                var list = bll.GetNameList(contactPhone.Text);
                if (list.Length == 1 && list[0] == "" || list.Length == 0)
                {
                    return;
                }
                else
                {
                    contactName.Items.AddRange(list);
                    contactName.Text = contactName.Items[0].ToString();
                    CreateContactsAndNotesTabs(list);
                }
            }
            catch
            {
                MessageBox.Show("Hi guy/gal. You caused error #8 - contact Eric");
            }
        }


        public void CreateContactsAndNotesTabs(string[] list)
        {
            try
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
            catch
            {
                MessageBox.Show("Hi guy/gal. You caused error #9 - contact Eric");
            }
        }

        public void ClearContactTabsAndNotes()
        {
            try
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
            catch
            {
                MessageBox.Show("Hi guy/gal. You caused error #10 - contact Eric");
            }
        }

        private void ClearCompanyNotesText()
        {
            try
            {
                businessTabControl.TabPages[0].Controls.Clear();
            }
            catch
            {
                MessageBox.Show("Hi guy/gal. You caused error #11 - contact Eric");
            }
        }

        private void SetContactEmail()
        {
            try
            {
                contactEmail.Text = "";
                contactEmail.Items.Clear();
                contactEmail.Items.AddRange(bll.GetCustomerEmail(contactPhone.Text, contactName.Text));
                contactEmail.Text = contactEmail.Items[0].ToString();
            }
            catch
            {
                MessageBox.Show("Hi guy/gal. You caused error #12 - contact Eric");
            }
        }

        private void SetCustomerCode()
        {
            try
            {
                customerCode.Text = "";
                customerCode.Items.Clear();
                customerCode.Items.AddRange(bll.GetCustomerCode(contactPhone.Text, contactName.Text));
                customerCode.Text = customerCode.Items[0].ToString();
                CreateBusinessNotes();
            }
            catch
            {
                MessageBox.Show("Hi guy/gal. You caused error #13 - contact Eric");
            }
        }

        private void SetCompanyName()
        {
            try
            {
                companyName.Text = "";
                companyName.Items.Clear();
                companyName.Items.AddRange(bll.GetCompanyName(customerCode.Text));
                companyName.Text = companyName.Items[0].ToString();
            }
            catch
            {
                MessageBox.Show("Hi guy/gal. You caused error #14 - contact Eric");
            }

        }

        private void SetCityStateZip()
        {
            try
            {
                comboCityStateZip.Text = "";
                comboCityStateZip.Items.Clear();
                comboCityStateZip.Items.AddRange(bll.GetCompanyCityStateZip(customerCode.Text));
                comboCityStateZip.Text = comboCityStateZip.Items[0].ToString();
            }
            catch
            {
                MessageBox.Show("Hi guy/gal. You caused error #15 - contact Eric");
            }
        }

        public int SaveFormToDatabase()
        {
            try
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
            //returning 0 will make a pop-up stating the call didnt' save
            catch
            {
                MessageBox.Show("Hi guy/gal. You caused error #16 - contact Eric");
                return 0;
            }
        }

        public int UpdateFormInDatabase()
        {
            try
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
            //returning 0 will cause the pop-up that the form didn't update
            catch
            {
                MessageBox.Show("Hi guy/gal. You caused error #17 - contact Eric");
                return 0;
            }
        }

        private void TurnValidationOnForAll()
        {
            try
            {
                foreach (Control con in this.Controls)
                {
                    con.CausesValidation = true;
                }
            }
            catch
            {
                MessageBox.Show("Hi guy/gal. You caused error #18 - contact Eric");
            }
        }

        private void TurnValidationOffForAll()
        {
            try { 
                foreach (Control con in this.Controls)
                {
                    con.CausesValidation = false;
                }
            }
            catch
            {
                MessageBox.Show("Hi guy/gal. You caused error #19 - contact Eric");
            }
        }


        //clears the form to create a new call
        private void BtnNewCall_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = MessageBox.Show("Do you want to delete the info currently in form?", "Warning!", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (result == DialogResult.Yes)
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
                    catch
                    {

                        throw;
                    }
                }
                else
                {
                    return;
                }
            }
            catch
            {
                MessageBox.Show("Hi guy/gal. You caused error #20 - contact Eric");
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
                callRecord.Text = "";
                ClearCompanyNotesText();
                CreateBusinessNotes();
            }
            catch
            {
                MessageBox.Show("Hi guy/gal. You caused error #21 - contact Eric");
            }
        }

        private void EmailRep_Click(object sender, EventArgs e)
        {
            try
            {
                //var fromAddress = new MailAddress("fccalllogtest@gmail.com", "Eric Kobliska");
                ////I would use 'emails.Text' in place of erickobliska@gmail.com here
                //var toAddress = new MailAddress("erickobliska@gmail.com", "Eric K");
                //const string FROMPASSWORD = "thisispassword";
                //string subject = $"{reasonForCall.Text} call from {contactName.Text} at {companyName.Text}";
                //string body = $"{contactName.Text} called from {companyName.Text} in {comboCityStateZip.Text}. Their contact info is {contactPhone.Text} & {contactEmail.Text}. The reason they called: {notesParagraph.Text}.";


                //var smtp = new SmtpClient
                //{
                //    Host = "smtp.gmail.com",
                //    Port = 587,
                //    EnableSsl = true,
                //    DeliveryMethod = SmtpDeliveryMethod.Network,
                //    UseDefaultCredentials = false,
                //    Credentials = new NetworkCredential(fromAddress.Address, FROMPASSWORD)
                //};
                //using (var message = new MailMessage(fromAddress, toAddress)
                //{
                //    Subject = subject,
                //    Body = body
                //})

                //{
                //    smtp.Send(message);
                //    MessageBox.Show("Email sent successfully.");
                //}
                //this.checkBox1.Checked = true;
                //this.checkBox1.Text = "Call record sent to rep(s).";
                //smtp.Dispose();
                Outlook.Application oApp = new Outlook.Application();
                Outlook._MailItem oMailItem = (Outlook._MailItem)oApp.CreateItem(Outlook.OlItemType.olMailItem);
                Outlook.Recipients oRecips = (Outlook.Recipients)oMailItem.Recipients;
                Outlook.Recipient oRecip = (Outlook.Recipient)oRecips.Add(repEmail.Text);
                oMailItem.Subject = $"{reasonForCall.Text} call from {contactName.Text} at {companyName.Text}";
                oMailItem.Body = $"{contactName.Text} called from {companyName.Text} in {comboCityStateZip.Text}. Their contact info is {contactPhone.Text} & {contactEmail.Text}. The reason they called: {notesParagraph.Text}.";
                oMailItem.Display(true);
            }
            catch
            {
                MessageBox.Show("Hi guy/gal. You caused error 22 - contact Eric");
            }
        }



        private static bool IsPhoneNumber(string number)
        {
            try
            {
                return Regex.Match(number, @"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$").Success;
            }
            catch
            {
                return false;
            }
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
            try
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
            catch
            {
                MessageBox.Show("Hi guy/gal. You caused error #23 - contact Eric");
            }
        }


        private void ContactName_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (contactName.Text.Length > 50)
                {
                    MessageBox.Show("Contact Name is too long");
                    e.Cancel = true;
                }
            }
            catch
            {
                MessageBox.Show("Hi guy/gal. You caused error #24 - contact Eric");
            }
        }

        private void ContactEmail_Validating(object sender, CancelEventArgs e)
        {
            try
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
            catch
            {
                MessageBox.Show("Hi guy/gal. You caused error #25 - contact Eric");
            }
        }

        private void AddAddress_Click(object sender, EventArgs e)
        {
            try
            {
                using (var addAddress = new AddAddressForm())
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
            catch
            {
                MessageBox.Show("Hi guy/gal. You caused error #26 - contact Eric");
            }
        }

        private void CustomerCode_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (customerCode.Text.Length > 10)
                {
                    MessageBox.Show("Customer Code is too long - please re-enter.");
                    e.Cancel = true;
                }
            }
            catch
            {
                MessageBox.Show("Hi guy/gal. You caused error #27 - contact Eric");
            }
        }

        private void CompanyCity_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (companyCity.Text.Length > 50)
                {
                    MessageBox.Show("Company City is too long - please Add a new address.");
                    e.Cancel = true;
                }
            }
            catch
            {
                MessageBox.Show("Hi guy/gal. You caused error #28 - contact Eric");
            }
        }

        private void CompanyState_Validating_1(object sender, CancelEventArgs e)
        {
            try
            {
                if (companyState.Text.Length > 15)
                {
                    MessageBox.Show("Company State is too long - please Add a new address.");
                    e.Cancel = true;
                }
            }
            catch
            {
                MessageBox.Show("Hi guy/gal. You caused error #29 - contact Eric");
            }
        }

        private void CompanyState_Validating(object sender, CancelEventArgs e)
        {

            try
            {
                if (companyState.Text.Length > 10)
                {
                    MessageBox.Show("State is too long");
                    e.Cancel = true;
                }
            }
            catch
            {
                MessageBox.Show("Hi guy/gal. You caused error #30 - contact Eric");
            }
        }

        private void CompanyZip_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (companyZip.Text.Length > 10)
                {
                    MessageBox.Show("Company Zip code is too long - please Add a new address.");
                    e.Cancel = true;
                }
            }
            catch
            {
                MessageBox.Show("Hi guy/gal. You caused error #31 - contact Eric");
            }
        }

        private void ReasonForCall_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (reasonForCall.Text.Length > 50)
                {
                    MessageBox.Show("Reason for call is too long - please be more succinct.");
                    e.Cancel = true;
                }
            }
            catch
            {
                MessageBox.Show("Hi guy/gal. You caused error #32 - contact Eric");
            }
        }


        //this is based on combocitystatezip being saved in the database with ', ' between each value
        //all commas removed from add address
        private void ComboCityStateZip_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
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
            catch
            {
                MessageBox.Show("Hi guy/gal. You caused error #33 - contact Eric");
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
                MessageBox.Show("Hi guy/gal. You caused error #34 - contact Eric");
            }

        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            try
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
            catch
            {
                MessageBox.Show("Hi guy/gal. You caused error #35 - contact Eric");
            }
        }

        private void LoadCallBasedOnGrid_Click(object sender, EventArgs e)
        {
            try
            {
                if (callLogGridView.SelectedCells.Count == 0 || Convert.ToInt32(callLogGridView.Rows[callLogGridView.CurrentRow.Index].Cells[0].Value.ToString()) < 1)
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
            catch
            {
                MessageBox.Show("Hi guy/gal. You caused error #36 - contact Eric");
            }
        }

        private void PopulateCallLogFieldsByDataTable(DataTable table)
        {
            try
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
                if (table.Rows[0]["customerCode"].ToString() == "yes")
                {
                    completedAnswer.Checked = true;
                }
                outsideRep.Text = table.Rows[0]["outsideRep"].ToString();
                CreateContactsAndNotesTabs(bll.GetNameList(contactPhone.Text));
                CreateBusinessNotes();
            }
            catch
            {
                MessageBox.Show("Hi guy/gal. You caused error #37 - contact Eric");
            }
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            try
            {
                using (var searchForm = new SearchForm())
                {
                    if (searchForm.ShowDialog() == DialogResult.OK)
                    {
                        callLogGridView.DataSource = bll.GetCallLogDataTableBySearchFromDB(searchForm.SearchValue);
                    }
                }
                callLogGridView.Select();
            }
            catch
            {
                MessageBox.Show("Hi guy/gal. You caused error #38 - contact Eric");
            }
        }

        private void ContactName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                SelectTabBasedOnSelectedContact();
                contactName.Select();
            }
            catch
            {
                MessageBox.Show("Hi guy/gal. You caused error #39 - contact Eric");
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                {
                    BtnNewCall_Click(sender, e);
                }
                if (e.KeyCode == Keys.F4)
                {
                    BtnSave_Click(sender, e);
                }
                if (e.KeyCode == Keys.F5)
                {
                    SearchButton_Click(sender, e);
                }
            }
            catch
            {
                MessageBox.Show("Hi guy/gal. You caused error #40 - contact Eric");
            }
        }
    }
}
