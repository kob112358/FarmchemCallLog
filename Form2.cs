using System;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;
using System.Windows.Forms;
using System.Collections.Generic;
using BLL;

namespace FarmchemCallLog
{
    public partial class rgaForm : Form
    {

        private SqlConnection _con = new SqlConnection("data source=kobpc\\sqlexpress;initial catalog=modifycalllog;integrated security=true");
        private DataTable _dt;
        BusinessLogicLayer bll = new BusinessLogicLayer();


        public rgaForm()
        {
            try
            {
                InitializeComponent();
                CF.Select();
                rgaDate.Format = DateTimePickerFormat.Custom;
                Order_status.SelectedItem = "Open";
                Notes_and_Activity.Text += " " + rgaDate.Value;
            }
            catch (Exception)
            {

                throw;
            }
        }

        //form populates text that was too long for the RGA return section
        private void RGA_Type_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (RGA_Type.SelectedIndex == 4 || RGA_Type.SelectedIndex == 5)
                {
                    bruceCredit.Text = "*BRUCE, please advise when we can credit dealer _________ (Bruce Carr initials)";
                }
                else
                {
                    bruceCredit.Text = "";
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        //saves RGA info to SQL and outputs a button giving the new record #
        private void SaveToDB_Click(object sender, EventArgs e)
        {
            try
            {

                int newRecord = bll.SaveRGAToDatabase(CF.Text, rgaDate.Text, Order_owner.Text, Order_status.Text, RGA_Type.Text, Energy_or_Chem.Text, Reason_RGA_Open.Text, Complaint.Text, vendor_rga.Text,
SALES_ORDER.Text, CUSTOMER.Text, Notes_and_Activity.Text, Final_Outcome.Text, Testing_Notes.Text, Invoice_Number.Text, Customer_Number.Text, Contact_Name.Text, Contact_Phone.Text,
Contact_Email.Text, Return_Qty_A.Text, Return_Item_A.Text, Return_Qty_B.Text, Return_Item_B.Text,
Return_Qty_C.Text, Return_Item_C.Text, Return_Qty_D.Text, Return_Item_D.Text, Return_Credit_A.Text, Return_Credit_B.Text, Return_Credit_C.Text, Return_Credit_D.Text,
Return_Ship_Method.Text, Replace_Qty_A.Text, Replace_Qty_B.Text, Replace_Qty_C.Text, Replace_Item_A.Text, Replace_Item_B.Text, Replace_Item_C.Text, Replace_SO_A.Text,
Replace_SO_B.Text, Replace_SO_C.Text, Energy_Price_Level.Text);
                if (newRecord != 0)
                {
                    MessageBox.Show($"{CF.Text}, Record '{newRecord}' has been saved.");
                }
                else
                {
                    MessageBox.Show("Hi guy/gal! Error in saving to database.");
                }
                
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                _con.Close();
            }
        }






        //Loads old RGAs into the form
        private void LoadRgaFormByRecordNumber_Click(object sender, EventArgs e)
        {
            //checks if the record value is empty
            if (ID.Text == "")
            {
                MessageBox.Show("Hi guy/gal! Please enter a record number and try again.");
                return;
            }

            PopulateRGAFormFieldsByDataTable(bll.GetDataTableOfRGAByRecordNumber(ID.Text));
            PopulateRGAVersions();


        }

        private void LoadRGAFormByRGANumber_Click(object sender, EventArgs e)
        {

            PopulateRGAVersions();

            ID.Text = RetrieveRecordNumberFromVersion();
            bll.GetDataTableOfRGAByRecordNumber(ID.Text);
        }



        private void PopulateRGAVersions()
        {
            rgaVersions.Items.Clear();
            List<string> versionList = bll.LoadRGAVersionsFromRecord(CF.Text);

            foreach(var version in versionList)
            {
                rgaVersions.Items.Add(version);
                if (version.Substring(0, version.IndexOf(" ")) == ID.Text.Trim())
                {
                    rgaVersions.SelectedItem = version;
                }
                else
                {
                    rgaVersions.SelectedItem = version;
                }
            }
        }




        private void LoadRGAVersion_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you want to overwrite information in the form currently?", "Warning!", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                var recordNum = RetrieveRecordNumberFromVersion();
                PopulateRGAFormFieldsByDataTable(bll.GetDataTableOfRGAByRecordNumber(recordNum));
                ID.Text = recordNum;

            }
            else if (result == DialogResult.No)
            {
                return;
            }
        }

        private string RetrieveRecordNumberFromVersion()
        {
            var spaceIndex = rgaVersions.SelectedItem.ToString().IndexOf(" ");
            return rgaVersions.SelectedItem.ToString().Substring(0, spaceIndex);
        }






        private void PopulateRGAFormFieldsByDataTable(DataTable table)
        {
            CF.Text = table.Rows[0]["CF"].ToString();
            Order_owner.Text = table.Rows[0]["Order_owner"].ToString();
            Order_status.Text = table.Rows[0]["Order_status"].ToString();
            RGA_Type.Text = table.Rows[0]["RGA_Type"].ToString();
            Energy_or_Chem.Text = table.Rows[0]["Energy_or_Chem"].ToString();
            Reason_RGA_Open.Text = table.Rows[0]["Reason_RGA_Open"].ToString();
            Complaint.Text = table.Rows[0]["Complaint"].ToString();
            vendor_rga.Text = table.Rows[0]["vendor_rga"].ToString();
            SALES_ORDER.Text = table.Rows[0]["SALES_ORDER"].ToString();
            CUSTOMER.Text = table.Rows[0]["CUSTOMER"].ToString();
            Notes_and_Activity.Text = table.Rows[0]["Notes_and_Activity"].ToString();
            Final_Outcome.Text = table.Rows[0]["Final_Outcome"].ToString();
            Testing_Notes.Text = table.Rows[0]["Testing_Notes"].ToString();
            Invoice_Number.Text = table.Rows[0]["Invoice_Number"].ToString();
            Customer_Number.Text = table.Rows[0]["Customer_Number"].ToString();
            Contact_Name.Text = table.Rows[0]["Contact_Name"].ToString();
            Contact_Phone.Text = table.Rows[0]["Contact_Phone"].ToString();
            Contact_Email.Text = table.Rows[0]["Contact_Email"].ToString();
            Return_Qty_A.Text = table.Rows[0]["Return_Qty_A"].ToString();
            Return_Item_A.Text = table.Rows[0]["Return_Item_A"].ToString();
            Return_Qty_B.Text = table.Rows[0]["Return_Qty_B"].ToString();
            Return_Item_B.Text = table.Rows[0]["Return_Item_B"].ToString();
            Return_Qty_C.Text = table.Rows[0]["Return_Qty_C"].ToString();
            Return_Item_C.Text = table.Rows[0]["Return_Item_C"].ToString();
            Return_Qty_D.Text = table.Rows[0]["Return_Qty_D"].ToString();
            Return_Item_D.Text = table.Rows[0]["Return_Item_D"].ToString();
            Return_Credit_A.Text = table.Rows[0]["Return_Credit_A"].ToString();
            Return_Credit_B.Text = table.Rows[0]["Return_Credit_B"].ToString();
            Return_Credit_C.Text = table.Rows[0]["Return_Credit_C"].ToString();
            Return_Credit_D.Text = table.Rows[0]["Return_Credit_D"].ToString();
            Return_Ship_Method.Text = table.Rows[0]["Return_Ship_Method"].ToString();
            Replace_Qty_A.Text = table.Rows[0]["Replace_Qty_A"].ToString();
            Replace_Qty_B.Text = table.Rows[0]["Replace_Qty_B"].ToString();
            Replace_Qty_C.Text = table.Rows[0]["Replace_Qty_C"].ToString();
            Replace_Item_A.Text = table.Rows[0]["Replace_Item_A"].ToString();
            Replace_Item_B.Text = table.Rows[0]["Replace_Item_B"].ToString();
            Replace_Item_C.Text = table.Rows[0]["Replace_Item_C"].ToString();
            Replace_SO_A.Text = table.Rows[0]["Replace_SO_A"].ToString();
            Replace_SO_B.Text = table.Rows[0]["Replace_SO_B"].ToString();
            Replace_SO_C.Text = table.Rows[0]["Replace_SO_C"].ToString();
            Energy_Price_Level.Text = table.Rows[0]["Energy_Price_Level"].ToString();

        }

        //this generates an e-mail that is sent to shipping telling them what the e-mail address is to send the return shipping label
        //I would like Button1_Click to open an outlook message for us to review
        private void SendToShipping_Click(object sender, EventArgs e)
        {
            var fromAddress = new MailAddress("fccalllogtest@gmail.com", "Eric Kobliska");
            var toAddress = new MailAddress("erickobliska@gmail.com", "Eric K");
            const string fromPassword = "thisispassword";
            string subject = CF.Text + " Email";
            string body = Contact_Email.Text + " is the email for " + CF.Text + ". It has + " + rgaTotalBox.Text + " number of boxes weighing " + rgaTotalWeight.Text + ".";

            try
            {
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
                    MessageBox.Show("Email sent to shipping successfully.");
                }

                this.checkBox2.Checked = true;
                this.checkBox2.Text = Contact_Email.Text + " sent to shipping.";
            }
            catch (Exception)
            {

                throw;
            }
        }

        //this will need to generate a new .pdf with the new RGA# and expiration date
        //generates e-mail to customer based on contactEmail
        //I would like this to open an outlook message we can review
        private void BtnCustomerEmail_Click(object sender, EventArgs e)
        {
            var fromAddress = new MailAddress("fccalllogtest@gmail.com", "Eric Kobliska");
            var toAddress = new MailAddress("erickobliska@gmail.com", "Eric K");
            const string fromPassword = "thisispassword";
            string subject = CF.Text + " Return Instructions";
            string body = Contact_Name.Text.Trim() + ", " + System.Environment.NewLine + "Thank you for your business. " + bll.GetRGATextBasedOnReturn(RGA_Type.Text.Substring(0, 1), CF.Text.Trim()) + System.Environment.NewLine + "Have a great day.";
            Attachment attachment = new Attachment("C:\\Users\\Kob\\Desktop\\CallLogAttachment.txt");

            try
            {
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
                    message.Attachments.Add(attachment);
                    smtp.Send(message);
                    MessageBox.Show("Message sent to " + Contact_Email.Text + " successfully.");
                }

                //checks the RGA sent box and changes the text to show who they were sent to
                this.checkBox1.Checked = true;
                this.checkBox1.Text = "RGA instructions sent to " + Contact_Email.Text;
            }
            catch (Exception)
            {

                throw;
            }
        }


    }
}
