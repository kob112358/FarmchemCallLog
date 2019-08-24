using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;
using FluentValidation;
using DAL;
using BLL;

namespace FarmchemCallLog
{
    public partial class Form1 : Form
    {
        public rgaForm rga;
        BusinessLogicLayer bll = new BusinessLogicLayer();



        public Form1()
        {
            InitializeComponent();
            contactPhone.Select();
        }
        

        //populates contactNameField and updates displayed info to calls from phone
        private void ContactPhone_Leave(object sender, EventArgs e)
        {
            if (contactPhone.Text == "")
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
                SetEmail();
                SetCompanyName();
                SetCityStateZip();
        }

        public void PopulateDataGridViewByPhoneCompanyCity()
        {
            dataGridView1.DataSource = bll.GetGridViewData(contactPhone.Text, companyName.Text, companyCity.Text);
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (SaveFormToDatabase() != 0)
            {
                MessageBox.Show("saved");
            }
            else
            {
                MessageBox.Show("error");
            }
            PopulateDataGridViewByPhoneCompanyCity();
        }



        private void SetContactName()
        {
            contactName.Items.Clear();
            contactName.Items.AddRange(bll.GetNameList(contactPhone.Text));
            contactName.Text = contactName.Items[0].ToString();
        }

        private void SetContactEmail()
        {
            contactEmail.Text = "";
            contactEmail.Items.Clear();
            contactEmail.Text = bll.GetCustomerEmail(contactPhone.Text, contactName.Text);
        }

        private void SetCustomerCode()
        {
            customerCode.Text = "";
            customerCode.Items.Clear();
            customerCode.Text = bll.GetCustomerCode(contactPhone.Text, contactName.Text);
        }

        private void SetCompanyName()
        {
            companyName.Text = "";
            companyName.Items.Clear();
            companyName.Text = bll.GetCompanyName(customerCode.Text);
        }

        private void SetEmail()
        {
            contactEmail.Text = "";
            contactEmail.Items.Clear();
            contactEmail.Text = bll.GetCustomerEmail(contactPhone.Text, contactName.Text);
        }

        private void SetCityStateZip()
        {
            companyCity.Items.Clear();
            companyCity.Text = bll.GetCompanyCity(contactPhone.Text, customerCode.Text);

            companyState.Items.Clear();
            companyState.Text = bll.GetCompanyState(contactPhone.Text, customerCode.Text);

            companyZip.Items.Clear();
            companyZip.Text = bll.GetCompanyZip(contactPhone.Text, customerCode.Text);
        }

        public int SaveFormToDatabase()
        {
            return bll.SaveToDatabase(contactPhone.Text, contactName.Text, contactEmail.Text, customerCode.Text, companyName.Text, companyCity.Text, companyState.Text, companyZip.Text, originalSalesOrder.Text, partNumber.Text, reasonForCall.Text, notesParagraph.Text, callDate.Text);
        }

        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


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
                companyCity.Items.Clear();
                companyState.Text = "";
                companyState.Items.Clear();
                companyZip.Text = "";
                companyZip.Items.Clear();
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

    }
}
