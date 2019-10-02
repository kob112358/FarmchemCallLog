using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using Outlook = Microsoft.Office.Interop.Outlook;
using BLL;
using ClassModels.CallClasses;
using System.IO;

namespace CallLog
{
    public partial class CallLogUI : Form
    {
        private readonly BusinessLogicLayer bll = new BusinessLogicLayer();
        private List<Address> _addressList;
        private static int _selectedAddressIndex;
        private const string _EXCEPTIONFILEPATH = "C:\\temp\\CallLogError.txt";

        public CallLogUI()
        {
            InitializeComponent();
            contactPhone.Select();
        }

        private void SetCityStateZip()
        {
            try
            {
                comboCityStateZip.Text = "";
                comboCityStateZip.Items.Clear();
                _addressList = bll.GetCompanyCityStateZip(customerCode.Text);
                foreach (var c in _addressList)
                {
                    comboCityStateZip.Items.Add($"{c.City}, {c.State} {c.Zip}");
                }
                comboCityStateZip.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                LogError(ex);
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
                        _addressList.Add(new Address()
                        {
                            City = addAddress.addCompanyCity.Text,
                            State = addAddress.addCompanyState.Text,
                            Zip = addAddress.addCompanyZip.Text
                        });
                    }
                    comboCityStateZip.Items.Add($" {_addressList[_addressList.Count - 1].City}, {_addressList[_addressList.Count - 1].State} {_addressList[_addressList.Count - 1].Zip}");
                    comboCityStateZip.SelectedItem = comboCityStateZip.Items[comboCityStateZip.Items.Count - 1];
                }
            }
            catch (Exception ex)
            {
                LogError(ex);
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
                        List<DTCall> db = bll.GetCallLogDataTableBySearchFromDB(searchForm.SearchValue);
                        BindingList<DTCall> view = new BindingList<DTCall>(db);
                        callLogGridView.DataSource = view;
                    }
                }
                callLogGridView.Select();
            }
            catch (Exception ex)
            {
                LogError(ex);
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
            catch(Exception ex)
            {
                LogError(ex);
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
                return bll.UpdateCallLogRecord(Convert.ToInt32(callRecord.Text), contactPhone.Text, contactName.Text, contactEmail.Text, customerCode.Text, companyName.Text, _addressList[_selectedAddressIndex].City,
                    _addressList[_selectedAddressIndex].State, _addressList[_selectedAddressIndex].Zip, reasonForCall.Text, notesParagraph.Text, callDate.Value, repEmail.Text, contactNotes, companyNotes, completedAnswer.Checked);
            }
            //returning 0 will cause the pop-up that the form didn't update
            catch (Exception ex)
            {
                LogError(ex);
                return 0;
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
                    PopulateCallLogFieldsByCall(bll.GetCallByRecordNumber(recordNum));
                    callRecord.Text = recordNum;
                    SelectTabBasedOnSelectedContact();
                }
                else if (result == DialogResult.No)
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading call from Grid.");
                LogError(ex);
            }
        }

        private void PopulateCallLogFieldsByCall(Call loadedCall)
        {
            try
            {
                contactPhone.Text = loadedCall.Cust.Phone;
                contactName.Text = loadedCall.Cust.ContactName;
                contactEmail.Text = loadedCall.Cust.Email;
                customerCode.Text = loadedCall.Bus.CustomerCode;
                companyName.Text = loadedCall.Bus.CompanyName;
                companyCity.Text = loadedCall.Add.City;
                companyState.Text = loadedCall.Add.State;
                companyZip.Text = loadedCall.Add.Zip;
                reasonForCall.Text = loadedCall.CallInformation.ReasonForCall;
                notesParagraph.Text = loadedCall.CallInformation.CallNotes;
                completedAnswer.Checked = loadedCall.CallInformation.CallResolved;
                repEmail.Text = loadedCall.Bus.OutsideRep;
                CreateContactsAndNotesTabs(bll.GetNameList(contactPhone.Text));
                CreateBusinessNotes();
            }
            catch (Exception ex)
            {
                LogError(ex);
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
            catch (Exception ex)
            {
                LogError(ex);
            }
        }
        private void ContactName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                SelectTabBasedOnSelectedContact();
                contactName.Select();
            }
            catch (Exception ex)
            {
                LogError(ex);
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
            catch (Exception ex)
            {
                LogError(ex);
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
            catch (Exception ex)
            {
                LogError(ex);
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
            catch (Exception ex)
            {
                MessageBox.Show("Problem validating the customer code. Please try again.");
                LogError(ex);
            }
        }

        private void CompanyCity_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (_addressList[_selectedAddressIndex].City.Length > 50)
                {
                    MessageBox.Show("Company City is too long - please Add a new address.");
                    e.Cancel = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Problem validating the city. Please try again.");
                LogError(ex);
            }
        }
        private void CompanyState_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if(_addressList[_selectedAddressIndex].State.Length > 15)
                {
                    MessageBox.Show("Company State is too long - please Add a new address.");
                    e.Cancel = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Problem validating the company state. Please try again.");
                LogError(ex);
            }
        }

        private void CompanyZip_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (_addressList[_selectedAddressIndex].Zip.Length > 10)
                {
                    MessageBox.Show("Company Zip code is too long - please Add a new address.");
                    e.Cancel = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Problem validating the zip code. Please try again.");
                LogError(ex);
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
            catch (Exception ex)
            {
                MessageBox.Show("Problem validating the reason for call. Please try again.");
                LogError(ex);
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
            catch (Exception ex)
            {
                LogError(ex);
            }
        }

        private void TurnValidationOffForAll()
        {
            try
            {
                foreach (Control con in this.Controls)
                {
                    con.CausesValidation = false;
                }
            }
            catch (Exception ex)
            {
                LogError(ex);
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
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error generating new call, please restart program.");
                        LogError(ex);
                    }
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                LogError(ex);
            }
        }

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
            catch (Exception ex)
            {
                LogError(ex);
            }
        }

        private void EmailRep_Click(object sender, EventArgs e)
        {
            try
            {
                Outlook.Application oApp = new Outlook.Application();
                Outlook._MailItem oMailItem = (Outlook._MailItem)oApp.CreateItem(Outlook.OlItemType.olMailItem);
                Outlook.Recipients oRecips = (Outlook.Recipients)oMailItem.Recipients;
                Outlook.Recipient oRecip = (Outlook.Recipient)oRecips.Add(repEmail.Text);
                oMailItem.Subject = $"{reasonForCall.Text} call from {contactName.Text} at {companyName.Text}";
                oMailItem.Body = $"{contactName.Text} called from {companyName.Text} in {comboCityStateZip.Text}. Their contact info is {contactPhone.Text} & {contactEmail.Text}. The reason they called: {notesParagraph.Text}.";
                oMailItem.Display(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error sending email to rep.");
                LogError(ex);
            }
        }

        private bool IsPhoneNumber(string number)
        {
            try
            {
                return Regex.Match(number, @"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$").Success;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error validating phone number, please check and try again.");
                LogError(ex);
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
            catch (Exception ex)
            {
                MessageBox.Show("Problem validating the email. Please try again.");
                LogError(ex);
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
            catch (Exception ex)
            {
                MessageBox.Show("Problem validating the phone. Please try again.");
                LogError(ex);
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
            catch (Exception ex)
            {
                MessageBox.Show("Problem validating the name. Please try again.");
                LogError(ex);
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
            catch (Exception ex)
            {
                MessageBox.Show("Problem validating the contact e-mail. Please try again.");
                LogError(ex);
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
                    return bll.SaveToDatabase(contactPhone.Text, contactName.Text, contactEmail.Text, customerCode.Text, companyName.Text, _addressList[_selectedAddressIndex].City, _addressList[_selectedAddressIndex].State,
                        _addressList[_selectedAddressIndex].Zip, reasonForCall.Text, notesParagraph.Text, callDate.Value, repEmail.Text, contactNotes, companyNotes, completedAnswer.Checked);
                }
                else
                {
                    TurnValidationOffForAll();
                    return 0;
                }
            }
            //returning 0 will make a pop-up stating the call didnt' save
            catch (Exception ex)
            {
                MessageBox.Show("Problem saving to database. Please try again");
                LogError(ex);
                return 0;
            }
        }

        public void PopulateDataGridViewByPhoneCompanyCity()
        {
            try
            {
                List<DTCall> db = bll.GetGridViewDataByPhoneCompanyCity(contactPhone.Text, companyName.Text, _addressList[_selectedAddressIndex].City);
                BindingList<DTCall> view = new BindingList<DTCall>(db);
                callLogGridView.DataSource = view;
            }
            catch (Exception ex)
            {
                LogError(ex);
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
            catch (Exception ex)
            {
                MessageBox.Show("Problem saving the call, please try again.");
                LogError(ex);
            }
        }
        public void CreateBusinessNotes()
        {
            try
            {
                var notes = bll.GetBusinessNotes(customerCode.Text);
                if (String.IsNullOrEmpty(notes))
                {
                    notes = "";
                }
                var textbox = new RichTextBox() { Height = 166, Width = 366 };
                textbox.Text = notes;
                textbox.Text = notes;
                businessTabControl.TabPages[0].Controls.Add(textbox);
            }
            catch (Exception ex)
            {
                LogError(ex);
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
            catch (Exception ex)
            {
                LogError(ex);
            }
        }

        public void ClearContactTabsAndNotes()
        {
            try
            {
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
            catch (Exception ex)
            {
                LogError(ex);
            }
        }

        private void ClearCompanyNotesText()
        {
            try
            {
                businessTabControl.TabPages[0].Controls.Clear();
            }
            catch (Exception ex)
            {
                LogError(ex);
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
            catch (Exception ex)
            {
                LogError(ex);
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
            catch (Exception ex)
            {
                LogError(ex);
            }
        }

        private void SetCompanyName()
        {
            try
            {
                companyName.Text = "";
                companyName.Items.Clear();
                foreach (var c in customerCode.Items)
                {
                    companyName.Items.Add(bll.GetCompanyName(c.ToString()));
                }
                companyName.Text = companyName.Items[0].ToString();
            }
            catch (Exception ex)
            {
                LogError(ex);
            }
        }
        
        private void CustomerCode_LostFocus(object sender, EventArgs e)
        {
            try
            {
                if (comboCityStateZip.Text == "")
                {
                    SetCityStateZip();
                }
            }
            catch(Exception ex)
            {
                LogError(ex);
            }
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
                    if(contactTabControl.TabPages.ContainsKey(contactName.Text) == false)
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
                        if(contactName.Items.Count > 0)
                        {
                            SetContactEmail();
                            SetCustomerCode();
                            SetCompanyName();
                            SetCityStateZip();
                            PopulateDataGridViewByPhoneCompanyCity();
                        }
                        if(contactTabControl.TabPages.ContainsKey(contactName.Text) == false)
                        {
                            contactName.Select();
                        }
                        else
                        {
                            SelectTabBasedOnSelectedContact();
                            contactName.Select();
                        }
                    }
                    else if(result == DialogResult.No)
                    {
                        return;
                    }
                }
            }
            catch(Exception ex)
            {
                LogError(ex);
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
                if (createdNewTab)
                {
                    contactEmail.Select();
                }
            }
            catch (Exception ex)
            {
                LogError(ex);
            }
        }

        private void ComboCityStateZip_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                _selectedAddressIndex = comboCityStateZip.SelectedIndex;
            }
            catch(Exception ex)
            {
                LogError(ex);
            }
        }

        private void LogError(Exception ex)
        {
            try
            {
                string message = string.Format("Time: {0}", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"));
                message += Environment.NewLine;
                message += "-----------------------------------------------------------";
                message += Environment.NewLine;
                message += string.Format("Message: {0}", ex.Message);
                message += Environment.NewLine;
                message += string.Format("StackTrace: {0}", ex.StackTrace);
                message += Environment.NewLine;
                message += string.Format("Source: {0}", ex.Source);
                message += Environment.NewLine;
                message += string.Format("TargetSite: {0}", ex.TargetSite.ToString());
                message += Environment.NewLine;
                message += "-----------------------------------------------------------";
                message += Environment.NewLine;
                using (StreamWriter writer = new StreamWriter(_EXCEPTIONFILEPATH, true))
                {
                    writer.WriteLine(message);
                    writer.Close();
                }
            }
            catch
            {
                MessageBox.Show("Error writing the exception to log, please contact IT");
            }
        }
    }
}
