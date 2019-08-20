using System;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;
using System.Windows.Forms;

namespace FarmchemCallLog
{
    public partial class rgaForm : Form
    {

        private SqlConnection _con = new SqlConnection("data source=kobpc\\sqlexpress;initial catalog=modifycalllog;integrated security=true");
        private DataTable _dt;


        public rgaForm()
        {
            try
            {
                InitializeComponent();
                CF.Select();
                rgaDate.Format = DateTimePickerFormat.Custom;
                //doesn't work ---> rgaDate.CustomFormat = "MM'/'dd'/'yyyy";
                Order_status.SelectedItem = "Open";
                Notes_and_Activity.Text += " " + rgaDate.Value;
            }
            catch (Exception)
            {

                throw;
            }
        }

        //this generates an e-mail that is sent to shipping telling them what the e-mail address is to send the return shipping label
        //I would like Button1_Click to open an outlook message for us to review
        private void Button1_Click(object sender, EventArgs e)
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
            string body = Contact_Email.Text + " is the email for " + CF.Text + ". It has + " + rgaTotalBox.Text + " number of boxes weighing " + rgaTotalWeight.Text + ".";
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

        //form populates text that was too long for the RGA return section
        private void RGA_Type_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (RGA_Type.SelectedIndex == 5 || RGA_Type.SelectedIndex == 4)
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
                
                SqlCommand cmd = new SqlCommand("insert into RgaDatabase(CF, rgaDate, Order_owner, Order_status, RGA_Type, Energy_or_Chem, Reason_RGA_Open, Complaint, vendor_rga, SALES_ORDER, CUSTOMER, Notes_and_Activity, Final_Outcome, Testing_Notes, Invoice_Number, Customer_Number, Contact_Name, Contact_Phone, Contact_Email, Return_Qty_A, Return_Item_A, Return_Qty_B, Return_Item_B, Return_Qty_C, Return_Item_C, Return_Qty_D, Return_Item_D, Return_Credit_A, Return_Credit_B, Return_Credit_C, Return_Credit_D, Return_Ship_Method, Replace_Qty_A, Replace_Qty_B, Replace_Qty_C, Replace_Item_A, Replace_Item_B, Replace_Item_C, Replace_SO_A, Replace_SO_B, Replace_SO_C, Energy_Price_Level) output INSERTED.ID values (@CF, @rgaDate, @Order_owner, @Order_status, @RGA_Type, @Energy_or_Chem, @Reason_RGA_Open, @Complaint, @vendor_rga, @SALES_ORDER, @CUSTOMER, @Notes_and_Activity, @Final_Outcome, @Testing_Notes, @Invoice_Number, @Customer_Number, @Contact_Name, @Contact_Phone, @Contact_Email, @Return_Qty_A, @Return_Item_A, @Return_Qty_B, @Return_Item_B, @Return_Qty_C, @Return_Item_C, @Return_Qty_D, @Return_Item_D, @Return_Credit_A, @Return_Credit_B, @Return_Credit_C, @Return_Credit_D, @Return_Ship_Method, @Replace_Qty_A, @Replace_Qty_B, @Replace_Qty_C, @Replace_Item_A, @Replace_Item_B, @Replace_Item_C, @Replace_SO_A, @Replace_SO_B, @Replace_SO_C, @Energy_Price_Level)", _con);
                cmd = AddAllParametersToCmd(ref cmd);
                _con.Open();
                int newRecord = (int)cmd.ExecuteScalar();
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

        private SqlCommand AddAllParametersToCmd(ref SqlCommand thisCmd)
        {
            thisCmd.Parameters.AddWithValue("@CF", CF.Text.Trim());
            thisCmd.Parameters.AddWithValue("@rgaDate", rgaDate.Text.Trim());
            thisCmd.Parameters.AddWithValue("@Order_owner", Order_owner.Text.Trim());
            thisCmd.Parameters.AddWithValue("@Order_status", Order_status.Text.Trim());
            thisCmd.Parameters.AddWithValue("@RGA_Type", RGA_Type.Text.Trim());
            thisCmd.Parameters.AddWithValue("@Energy_or_Chem", Energy_or_Chem.Text.Trim());
            thisCmd.Parameters.AddWithValue("@Reason_RGA_Open", Reason_RGA_Open.Text.Trim());
            thisCmd.Parameters.AddWithValue("@Complaint", Complaint.Text.Trim());
            thisCmd.Parameters.AddWithValue("@vendor_rga", vendor_rga.Text.Trim());
            thisCmd.Parameters.AddWithValue("@SALES_ORDER", SALES_ORDER.Text.Trim());
            thisCmd.Parameters.AddWithValue("@CUSTOMER", CUSTOMER.Text.Trim());
            thisCmd.Parameters.AddWithValue("@Notes_and_Activity", Notes_and_Activity.Text.Trim());
            thisCmd.Parameters.AddWithValue("@Final_Outcome", Final_Outcome.Text.Trim());
            thisCmd.Parameters.AddWithValue("@Testing_Notes", Testing_Notes.Text.Trim());
            thisCmd.Parameters.AddWithValue("@Invoice_Number", Invoice_Number.Text.Trim());
            thisCmd.Parameters.AddWithValue("@Customer_Number", Customer_Number.Text.Trim());
            thisCmd.Parameters.AddWithValue("@Contact_Name", Contact_Name.Text.Trim());
            thisCmd.Parameters.AddWithValue("@Contact_Phone", Contact_Phone.Text.Trim());
            thisCmd.Parameters.AddWithValue("@Contact_Email", Contact_Email.Text.Trim());
            thisCmd.Parameters.AddWithValue("@Return_Qty_A", Return_Qty_A.Text.Trim());
            thisCmd.Parameters.AddWithValue("@Return_Item_A", Return_Item_A.Text.Trim());
            thisCmd.Parameters.AddWithValue("@Return_Qty_B", Return_Qty_B.Text.Trim());
            thisCmd.Parameters.AddWithValue("@Return_Item_B", Return_Item_B.Text.Trim());
            thisCmd.Parameters.AddWithValue("@Return_Qty_C", Return_Qty_C.Text.Trim());
            thisCmd.Parameters.AddWithValue("@Return_Item_C", Return_Item_C.Text.Trim());
            thisCmd.Parameters.AddWithValue("@Return_Qty_D", Return_Qty_D.Text.Trim());
            thisCmd.Parameters.AddWithValue("@Return_Item_D", Return_Item_D.Text.Trim());
            thisCmd.Parameters.AddWithValue("@Return_Credit_A", Return_Credit_A.Text.Trim());
            thisCmd.Parameters.AddWithValue("@Return_Credit_B", Return_Credit_B.Text.Trim());
            thisCmd.Parameters.AddWithValue("@Return_Credit_C", Return_Credit_C.Text.Trim());
            thisCmd.Parameters.AddWithValue("@Return_Credit_D", Return_Credit_D.Text.Trim());
            thisCmd.Parameters.AddWithValue("@Return_Ship_Method", Return_Ship_Method.Text.Trim());
            thisCmd.Parameters.AddWithValue("@Replace_Qty_A", Replace_Qty_A.Text.Trim());
            thisCmd.Parameters.AddWithValue("@Replace_Qty_B", Replace_Qty_B.Text.Trim());
            thisCmd.Parameters.AddWithValue("@Replace_Qty_C", Replace_Qty_C.Text.Trim());
            thisCmd.Parameters.AddWithValue("@Replace_Item_A", Replace_Item_A.Text.Trim());
            thisCmd.Parameters.AddWithValue("@Replace_Item_B", Replace_Item_B.Text.Trim());
            thisCmd.Parameters.AddWithValue("@Replace_Item_C", Replace_Item_C.Text.Trim());
            thisCmd.Parameters.AddWithValue("@Replace_SO_A", Replace_SO_A.Text.Trim());
            thisCmd.Parameters.AddWithValue("@Replace_SO_B", Replace_SO_B.Text.Trim());
            thisCmd.Parameters.AddWithValue("@Replace_SO_C", Replace_SO_C.Text.Trim());
            thisCmd.Parameters.AddWithValue("@Energy_Price_Level", Energy_Price_Level.Text.Trim());
            return thisCmd;
        }




        //Loads old RGAs into the form
        private void LoadRgaFormByRecord_Click(object sender, EventArgs e)
        {
            //checks if the record value is empty
            if (ID.Text == "")
            {
                MessageBox.Show("Hi guy/gal! Please enter a record number and try again.");
                return;
            }

            LoadRGAFormByRecordNumber(ID.Text.Trim());
            LoadRGAVersionsFromRecord();


        }


        private void LoadRGAFormByRGANumber_Click(object sender, EventArgs e)
        {
            LoadRGAVersionsFromRecord();
            ID.Text = RetrieveRecordNumberFromVersion();            
            LoadRGAFormByRecordNumber(ID.Text.Trim());
        }

        private void LoadRGAVersionsFromRecord()
        {
            var adapter = new SqlDataAdapter("SELECT ID FROM RgaDatabase WHERE CF LIKE '%' + @CF + '%'", _con);
            adapter.SelectCommand.Parameters.AddWithValue("CF", CF.Text.Trim());
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            rgaVersions.Items.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                rgaVersions.Items.Add(dt.Rows[i]["ID"].ToString() + " V" + (i+1));
                if(dt.Rows[i]["ID"].ToString() == ID.Text)
                {
                    rgaVersions.SelectedIndex = i;
                }
                else
                {
                    rgaVersions.SelectedIndex = rgaVersions.Items.Count - 1;
                }
            }
        }


        private void LoadRGAVersion_Click(object sender, EventArgs e)
        {
            var recordNum = RetrieveRecordNumberFromVersion();
            LoadRGAFormByRecordNumber(recordNum);
            ID.Text = recordNum;
        }

        private string RetrieveRecordNumberFromVersion()
        {
            var spaceIndex = rgaVersions.SelectedItem.ToString().IndexOf(" ");
            return rgaVersions.SelectedItem.ToString().Substring(0, spaceIndex);
        }


        private void LoadRGAFormByRecordNumber(string recordNumber)
        {
            DialogResult result = MessageBox.Show("Do you want to overwrite information in the form currently?", "Warning!", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                //creates new DataTable with all of the info related to the record value
                string searchID = "SELECT * FROM RgaDatabase WHERE ID LIKE '%' + @ID + '%'";
                var adapter = new SqlDataAdapter(searchID, _con);
                adapter.SelectCommand.Parameters.AddWithValue("ID", recordNumber);
                _dt = new DataTable();
                adapter.Fill(_dt);

                //enters the info into the fields

                    PopulateRGAFormFieldsByDataTable(_dt);

                

            }
            else if (result == DialogResult.No)
            {
                return;
            }

        }



        private void PopulateRGAFormFieldsByDataTable(DataTable info)
        {
            CF.Text = _dt.Rows[0]["CF"].ToString();
            Order_owner.Text = _dt.Rows[0]["Order_owner"].ToString();
            Order_status.Text = _dt.Rows[0]["Order_status"].ToString();
            RGA_Type.Text = _dt.Rows[0]["RGA_Type"].ToString();
            Energy_or_Chem.Text = _dt.Rows[0]["Energy_or_Chem"].ToString();
            Reason_RGA_Open.Text = _dt.Rows[0]["Reason_RGA_Open"].ToString();
            Complaint.Text = _dt.Rows[0]["Complaint"].ToString();
            vendor_rga.Text = _dt.Rows[0]["vendor_rga"].ToString();
            SALES_ORDER.Text = _dt.Rows[0]["SALES_ORDER"].ToString();
            CUSTOMER.Text = _dt.Rows[0]["CUSTOMER"].ToString();
            Notes_and_Activity.Text = _dt.Rows[0]["Notes_and_Activity"].ToString();
            Final_Outcome.Text = _dt.Rows[0]["Final_Outcome"].ToString();
            Testing_Notes.Text = _dt.Rows[0]["Testing_Notes"].ToString();
            Invoice_Number.Text = _dt.Rows[0]["Invoice_Number"].ToString();
            Customer_Number.Text = _dt.Rows[0]["Customer_Number"].ToString();
            Contact_Name.Text = _dt.Rows[0]["Contact_Name"].ToString();
            Contact_Phone.Text = _dt.Rows[0]["Contact_Phone"].ToString();
            Contact_Email.Text = _dt.Rows[0]["Contact_Email"].ToString();
            Return_Qty_A.Text = _dt.Rows[0]["Return_Qty_A"].ToString();
            Return_Item_A.Text = _dt.Rows[0]["Return_Item_A"].ToString();
            Return_Qty_B.Text = _dt.Rows[0]["Return_Qty_B"].ToString();
            Return_Item_B.Text = _dt.Rows[0]["Return_Item_B"].ToString();
            Return_Qty_C.Text = _dt.Rows[0]["Return_Qty_C"].ToString();
            Return_Item_C.Text = _dt.Rows[0]["Return_Item_C"].ToString();
            Return_Qty_D.Text = _dt.Rows[0]["Return_Qty_D"].ToString();
            Return_Item_D.Text = _dt.Rows[0]["Return_Item_D"].ToString();
            Return_Credit_A.Text = _dt.Rows[0]["Return_Credit_A"].ToString();
            Return_Credit_B.Text = _dt.Rows[0]["Return_Credit_B"].ToString();
            Return_Credit_C.Text = _dt.Rows[0]["Return_Credit_C"].ToString();
            Return_Credit_D.Text = _dt.Rows[0]["Return_Credit_D"].ToString();
            Return_Ship_Method.Text = _dt.Rows[0]["Return_Ship_Method"].ToString();
            Replace_Qty_A.Text = _dt.Rows[0]["Replace_Qty_A"].ToString();
            Replace_Qty_B.Text = _dt.Rows[0]["Replace_Qty_B"].ToString();
            Replace_Qty_C.Text = _dt.Rows[0]["Replace_Qty_C"].ToString();
            Replace_Item_A.Text = _dt.Rows[0]["Replace_Item_A"].ToString();
            Replace_Item_B.Text = _dt.Rows[0]["Replace_Item_B"].ToString();
            Replace_Item_C.Text = _dt.Rows[0]["Replace_Item_C"].ToString();
            Replace_SO_A.Text = _dt.Rows[0]["Replace_SO_A"].ToString();
            Replace_SO_B.Text = _dt.Rows[0]["Replace_SO_B"].ToString();
            Replace_SO_C.Text = _dt.Rows[0]["Replace_SO_C"].ToString();
            Energy_Price_Level.Text = _dt.Rows[0]["Energy_Price_Level"].ToString();

        }






        //1a.Correct item(s) ordered; but wrong item(s) shipped - _________(Bruce Carr initials)
        //    1b.Customer ordered wrong item(s)
        //    1c.Order entered incorrectly by salesman - _________(James Bluhm initials)
        //1d. Item(s) no longer needed
        //1e. Defective item(s) Vender is responsible for repair/replacement/shipping charges. Work through vendor for warranty claim.*
        //1f. Item(s) received damaged by shipping company - ("damaged" must be noted on the delivery receipt to turn in claim)*
        //1g. Fusion Rental Return _________ Fax or Email for BOL ____________________________________________________
        //1h. Other/Comments -  

    }
}
