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
            //don't populate anything if there is no phone #
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
                //PopulateDataGridViewByPhoneCompanyCity();
            }
        }
        //populates Email/Customer code based on Contact selected
        private void ContactName_Leave(object sender, EventArgs e)
        {
            try
            {
                SetEmail();
                SetCompanyName();
                SetCityStateZip();
            }
            catch (Exception)
            {

                throw;
            }
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




        public void PopulateDataGridViewByPhoneCompanyCity()
        {
        //    try
        //    {
        //        if (contactPhone.Text == "")
        //        {
        //            return;
        //        }
        //        var adapter = new SqlDataAdapter("SELECT * FROM modify_data_calllog WHERE contactPhone LIKE '%' + @contactPhone + '%' OR (companyName LIKE '%' + @companyName + '%' AND companyCity LIKE '%' + @companyCity + '%')", _con);
        //        adapter.SelectCommand.Parameters.AddWithValue("contactPhone", contactPhone.Text.Trim());
        //        adapter.SelectCommand.Parameters.AddWithValue("companyName", companyName.Text.Trim());
        //        adapter.SelectCommand.Parameters.AddWithValue("companyCity", companyCity.Text.Trim());
        //        _dt = new DataTable();
        //        adapter.Fill(_dt);
        //        dataGridView1.DataSource = _dt;
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        }

        // 'Save' button that saves everything into sql database
        private void BtnSave_Click(object sender, EventArgs e)
        {
        //    if (SaveFormToDatabase() != 0)
        //    {
        //        MessageBox.Show("saved");
        //    }
        //    else
        //    {
        //        MessageBox.Show("error");
        //    }
        //    PopulateDataGridViewByPhoneCompanyCity();
        }

        public int SaveFormToDatabase()
        {
            //var cmd = new SqlCommand("insert into modify_data_CallLog(contactPhone,contactName,contactEmail,customercode,companyName,companyCity,companyState,companyZip,originalSO,partNumber,reasonForCall,notesParagraph,callDate) values (@contactPhone, @contactName, @contactEmail, @customerCode, @companyName, @companyCity, @companyState, @companyZip, @originalSalesOrder, @partNumber, @reasonForCall, @notesParagraph, @callDate)", _con);
            //AddCallLogParamtersToCmd(ref cmd);
            //try
            //{
            //    _con.Open();
            //    return cmd.ExecuteNonQuery();
            //}
            //finally
            //{
            //    _con.Close();
            //}
            return 1;

        }

        private SqlCommand AddCallLogParamtersToCmd(ref SqlCommand thisCmd)
        {
            thisCmd.Parameters.AddWithValue("@contactPhone", contactPhone.Text.Trim());
            thisCmd.Parameters.AddWithValue("@contactName", contactName.Text.Trim());
            thisCmd.Parameters.AddWithValue("@contactEmail", contactEmail.Text.Trim());
            thisCmd.Parameters.AddWithValue("@customerCode", customerCode.Text.Trim());
            thisCmd.Parameters.AddWithValue("@companyName", companyName.Text.Trim());
            thisCmd.Parameters.AddWithValue("@companyCity", companyCity.Text.Trim());
            thisCmd.Parameters.AddWithValue("@companyState", companyState.Text.Trim());
            thisCmd.Parameters.AddWithValue("@companyZip", companyZip.Text.Trim());
            thisCmd.Parameters.AddWithValue("@originalSalesOrder", originalSalesOrder.Text.Trim());
            thisCmd.Parameters.AddWithValue("@partNumber", partNumber.Text.Trim());
            thisCmd.Parameters.AddWithValue("@reasonForCall", reasonForCall.Text.Trim());
            thisCmd.Parameters.AddWithValue("@notesParagraph", notesParagraph.Text.Trim());
            thisCmd.Parameters.AddWithValue("@callDate", callDate.Text.Trim());
            return thisCmd;
        }

        //adds reps email to the email list 
        private void AddRepToEmail_Click(object sender, EventArgs e)
        {
            try
            {
                if (repEmail.Text.ToString() == "")
                {
                    return;
                }
                emails.Text += repEmail.Text.Trim() + ";";
                btnEmailRep.Text += " " + repEmail.Text.Replace("@farmchem.com","");
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
                const string fromPassword = "thisispassword";
                string subject = reasonForCall.Text + " call from " + contactName.Text + " at " + companyName.Text;
                string body = contactName.Text + " called from " + companyName.Text + " in " + companyCity.Text + " " + companyState.Text + ". Their contact info is " + contactPhone.Text + " & " + contactEmail.Text + ". The reason they called: " + notesParagraph.Text + ". This would be e-mailed to " + emails.Text;


                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
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
                previousCall.Visible = true;
                previousCall.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
