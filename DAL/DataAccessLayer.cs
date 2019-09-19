using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class DataAccessLayer
    {
        private SqlConnection _con = new SqlConnection("data source=kobpc\\sqlexpress;initial catalog=modifycalllog;integrated security=true;Connect Timeout=60");
        private DataTable _dt;


        public List<string> PopulateNameField(string phone)
        {
            var adapter = new SqlDataAdapter("SELECT contactName, COUNT(contactName) AS MOST_FREQUENT FROM modify_data_calllog WHERE contactPhone LIKE '%' + @contactPhone + '%' GROUP BY contactName ORDER BY MOST_FREQUENT DESC", _con);
            adapter.SelectCommand.Parameters.AddWithValue("contactPhone", phone);
            return GenerateListForComboboxFromDB(adapter, "contactName");
        }

        public List<string> PopulateNameNotes(string phone, string name)
        {
            var adapter = new SqlDataAdapter("SELECT customerNotes FROM modify_data_calllog WHERE contactPhone LIKE '%' + @contactPhone + '%' AND contactName LIKE '%' + @contactName + '%'", _con);
            adapter.SelectCommand.Parameters.AddWithValue("contactPhone", phone);
            adapter.SelectCommand.Parameters.AddWithValue("contactName", name);
            return GenerateListForComboboxFromDB(adapter, "customerNotes");
        }

        public List<string> PopulateBusinessNotes(string phone, string code)
        {
            var adapter = new SqlDataAdapter("SELECT businessNotes FROM modify_data_calllog WHERE contactPhone LIKE '%' + @contactPhone + '%' AND customerCode LIKE '%' + @customerCode + '%'", _con);
            adapter.SelectCommand.Parameters.AddWithValue("contactPhone", phone);
            adapter.SelectCommand.Parameters.AddWithValue("customerCode", code);
            return GenerateListForComboboxFromDB(adapter, "businessNotes");
        }


        public List<string> PopulateCustomerEmail(string phone, string name)
        {
            var adapter = new SqlDataAdapter("SELECT contactEmail, COUNT(contactEmail) AS MOST_FREQUENT FROM modify_data_calllog WHERE contactPhone LIKE '%' + @contactPhone + '%' AND contactName LIKE '%' + @contactName + '%' GROUP BY contactEmail ORDER BY MOST_FREQUENT DESC", _con);
            adapter.SelectCommand.Parameters.AddWithValue("contactPhone", phone);
            adapter.SelectCommand.Parameters.AddWithValue("contactName", name);
            return GenerateListForComboboxFromDB(adapter, "contactEmail");
        }

        public List<string> PopulateCustomerCode(string phone, string name)
        {
            var adapter = new SqlDataAdapter("SELECT customerCode, COUNT(customerCode) AS MOST_FREQUENT FROM modify_data_calllog WHERE contactPhone LIKE '%' + @contactPhone + '%' AND contactName LIKE '%' + @contactName + '%' GROUP BY customerCode ORDER BY MOST_FREQUENT DESC", _con);
            adapter.SelectCommand.Parameters.AddWithValue("contactPhone", phone);
            adapter.SelectCommand.Parameters.AddWithValue("contactName", name);
            return GenerateListForComboboxFromDB(adapter, "customerCode");
        }

        public List<string> PopulateCompanyName(string code)
        {
            var adapter = new SqlDataAdapter("SELECT TOP (1) companyName FROM modify_data_calllog WHERE customerCode LIKE '%' + @customerCode + '%' GROUP BY companyName ORDER BY companyName DESC", _con);
            adapter.SelectCommand.Parameters.AddWithValue("customerCode", code);
            return GenerateListForComboboxFromDB(adapter, "companyName");
        }

        public List<string> PopulateCompanyCityStateZip(string code)
        {
            var adapter = new SqlDataAdapter("SELECT companyCityStateZip, COUNT(companyCityStateZip) AS MOST_FREQUENT FROM modify_data_calllog WHERE customerCode LIKE '%' + @customerCode + '%' GROUP BY companyCityStatezip ORDER BY MOST_FREQUENT DESC", _con);
            adapter.SelectCommand.Parameters.AddWithValue("customerCode", code);
            return GenerateListForComboboxFromDB(adapter, "companyCityStateZip");
        }


        public string PopulateCompanyCity(string phone, string code)
        {
            var adapter = new SqlDataAdapter("SELECT TOP (3) companyCity FROM modify_data_calllog WHERE contactPhone LIKE '%' + @contactPhone + '%' AND customerCode LIKE '%' + @customerCode + '%' GROUP BY companyCity ORDER BY COUNT(companyCity) DESC", _con);
            adapter.SelectCommand.Parameters.AddWithValue("customerCode", code);
            adapter.SelectCommand.Parameters.AddWithValue("contactPhone", phone);
            return GenerateListForComboboxFromDB(adapter, "companyCity")[0];
        }

        public string PopulateCompanyState(string phone, string code)
        {
            var adapter = new SqlDataAdapter("SELECT TOP (3) companyState FROM modify_data_calllog WHERE contactPhone LIKE '%' + @contactPhone + '%' AND customerCode LIKE '%' + @customerCode + '%' GROUP BY companyState ORDER BY COUNT(companyState) DESC", _con);
            adapter.SelectCommand.Parameters.AddWithValue("customerCode", code);
            adapter.SelectCommand.Parameters.AddWithValue("contactPhone", phone);
            return GenerateListForComboboxFromDB(adapter, "companyState")[0];
        }

        public string PopulateCompanyZip(string phone, string code)
        {
            var adapter = new SqlDataAdapter("SELECT TOP (3) companyZip FROM modify_data_calllog WHERE contactPhone LIKE '%' + @contactPhone + '%' AND customerCode LIKE '%' + @customerCode + '%' GROUP BY companyZip ORDER BY COUNT(companyZip) DESC", _con);
            adapter.SelectCommand.Parameters.AddWithValue("customerCode", code);
            adapter.SelectCommand.Parameters.AddWithValue("contactPhone", phone);
            return GenerateListForComboboxFromDB(adapter, "companyZip")[0];
        }

        public List<string> GenerateListForComboboxFromDB(SqlDataAdapter da, string field)
        {
            _dt = new DataTable();
            da.Fill(_dt);
            List<string> itemList = new List<string>();
            for (int i = 0; i < _dt.Rows.Count; i++)
            {
                if (_dt.Rows[i][field].ToString() == "" || _dt.Rows[i][field].ToString() == null)
                {
                    continue;
                }
                else
                {
                    itemList.Add(_dt.Rows[i][field].ToString());
                }
            }
            if(itemList.Count < 1)
            {
                itemList.Add("");
            }
            return itemList;
        }

        public DataTable PopulateDataGridViewByPhoneCompanyCity(string phone, string company, string city)
        {
            try
            {
                var adapter = new SqlDataAdapter("SELECT * FROM modify_data_calllog WHERE contactPhone LIKE '%' + @contactPhone + '%' OR (companyName LIKE '%' + @companyName + '%' AND companyCity LIKE '%' + @companyCity + '%')", _con);
                adapter.SelectCommand.Parameters.AddWithValue("contactPhone", phone);
                adapter.SelectCommand.Parameters.AddWithValue("companyName", company);
                adapter.SelectCommand.Parameters.AddWithValue("companyCity", city);
                _dt = new DataTable();
                adapter.Fill(_dt);
                
            }
            catch (Exception)
            {

                throw;
            }
            return _dt;
        }

        public int SaveToDatabase(string phone, string contactName, string email, string code, string companyName, string city, string state, string zip, string reason, string notes, string date, string rep, string contactnotes, string businessNotes)
        {
            var cmd = new SqlCommand("insert into modify_data_CallLog(contactPhone,contactName,contactEmail,customercode,companyName,companyCity,companyState,companyZip,reasonForCall,notesParagraph,callDate,outsideRep,customerNotes,businessNotes) values (@contactPhone, @contactName, @contactEmail, @customerCode, @companyName, @companyCity, @companyState, @companyZip, @reasonForCall, @notesParagraph, @callDate, @outsideRep, @customerNotes, @businessNotes)", _con);
            AddCallLogParamtersToCmd(ref cmd, phone, contactName, email, code, companyName, city, state, zip, reason, notes, date, rep, contactnotes, businessNotes);
            try
            {
                _con.Open();
                return cmd.ExecuteNonQuery();
            }
            finally
            {
                _con.Close();
            }
        }

        private SqlCommand AddCallLogParamtersToCmd(ref SqlCommand thisCmd, string phone, string contactName, string email, string code, string companyName, string city, string state, string zip, string reason, string notes, string date, string rep, string contactNotes, string businessNotes)
        {
            thisCmd.Parameters.AddWithValue("@contactPhone", phone);
            thisCmd.Parameters.AddWithValue("@contactName", contactName);
            thisCmd.Parameters.AddWithValue("@contactEmail", email);
            thisCmd.Parameters.AddWithValue("@customerCode", code);
            thisCmd.Parameters.AddWithValue("@companyName", companyName);
            thisCmd.Parameters.AddWithValue("@companyCity", city);
            thisCmd.Parameters.AddWithValue("@companyState", state);
            thisCmd.Parameters.AddWithValue("@companyZip", zip);
            thisCmd.Parameters.AddWithValue("@reasonForCall", reason);
            thisCmd.Parameters.AddWithValue("@notesParagraph", notes);
            thisCmd.Parameters.AddWithValue("@callDate", date);
            thisCmd.Parameters.AddWithValue("@outsideRep", rep);
            thisCmd.Parameters.AddWithValue("@customerNotes", contactNotes);
            thisCmd.Parameters.AddWithValue("@businessnotes", businessNotes);
            return thisCmd;
        }




        public int SaveRGAToDatabase(string CF, string rgaDate, string Order_owner, string Order_status, string RGA_Type, string Energy_or_Chem, string Reason_RGA_Open, string Complaint, string vendor_rga,
string SALES_ORDER, string CUSTOMER, string Notes_and_Activity, string Final_Outcome, string Testing_Notes, string Invoice_Number, string Customer_Number, string Contact_Name, string Contact_Phone,
string Contact_Email, string Return_Qty_A, string Return_Item_A, string Return_Qty_B, string Return_Item_B,
string Return_Qty_C, string Return_Item_C, string Return_Qty_D, string Return_Item_D, string Return_Credit_A, string Return_Credit_B, string Return_Credit_C, string Return_Credit_D,
string Return_Ship_Method, string Replace_Qty_A, string Replace_Qty_B, string Replace_Qty_C, string Replace_Item_A, string Replace_Item_B, string Replace_Item_C, string Replace_SO_A,
string Replace_SO_B, string Replace_SO_C, string Energy_Price_Level, string Outside_Rep)
        {
            try
            {
                var cmd = new SqlCommand("insert into RgaDatabase(CF, rgaDate, Order_owner, Order_status, RGA_Type, Energy_or_Chem, Reason_RGA_Open, Complaint, vendor_rga, SALES_ORDER, CUSTOMER, Notes_and_Activity, Final_Outcome, Testing_Notes, Invoice_Number, Customer_Number, Contact_Name, Contact_Phone, Contact_Email, Return_Qty_A, Return_Item_A, Return_Qty_B, Return_Item_B, Return_Qty_C, Return_Item_C, Return_Qty_D, Return_Item_D, Return_Credit_A, Return_Credit_B, Return_Credit_C, Return_Credit_D, Return_Ship_Method, Replace_Qty_A, Replace_Qty_B, Replace_Qty_C, Replace_Item_A, Replace_Item_B, Replace_Item_C, Replace_SO_A, Replace_SO_B, Replace_SO_C, Energy_Price_Level, Outside_Rep) output INSERTED.ID values (@CF, @rgaDate, @Order_owner, @Order_status, @RGA_Type, @Energy_or_Chem, @Reason_RGA_Open, @Complaint, @vendor_rga, @SALES_ORDER, @CUSTOMER, @Notes_and_Activity, @Final_Outcome, @Testing_Notes, @Invoice_Number, @Customer_Number, @Contact_Name, @Contact_Phone, @Contact_Email, @Return_Qty_A, @Return_Item_A, @Return_Qty_B, @Return_Item_B, @Return_Qty_C, @Return_Item_C, @Return_Qty_D, @Return_Item_D, @Return_Credit_A, @Return_Credit_B, @Return_Credit_C, @Return_Credit_D, @Return_Ship_Method, @Replace_Qty_A, @Replace_Qty_B, @Replace_Qty_C, @Replace_Item_A, @Replace_Item_B, @Replace_Item_C, @Replace_SO_A, @Replace_SO_B, @Replace_SO_C, @Energy_Price_Level, @Outside_Rep)", _con);
                cmd = AddAllRGAParametersToCmd(ref cmd, CF, rgaDate, Order_owner, Order_status, RGA_Type, Energy_or_Chem, Reason_RGA_Open, Complaint, vendor_rga,
    SALES_ORDER, CUSTOMER, Notes_and_Activity, Final_Outcome, Testing_Notes, Invoice_Number, Customer_Number, Contact_Name, Contact_Phone,
    Contact_Email, Return_Qty_A, Return_Item_A, Return_Qty_B, Return_Item_B,
    Return_Qty_C, Return_Item_C, Return_Qty_D, Return_Item_D, Return_Credit_A, Return_Credit_B, Return_Credit_C, Return_Credit_D,
    Return_Ship_Method, Replace_Qty_A, Replace_Qty_B, Replace_Qty_C, Replace_Item_A, Replace_Item_B, Replace_Item_C, Replace_SO_A,
    Replace_SO_B, Replace_SO_C, Energy_Price_Level, Outside_Rep);
                _con.Open();
                return (int)cmd.ExecuteScalar();
            }
            finally
            {
                _con.Close();
            }
        }

        private SqlCommand AddAllRGAParametersToCmd(ref SqlCommand thisCmd, string CF, string rgaDate, string Order_owner, string Order_status, string RGA_Type, string Energy_or_Chem, string Reason_RGA_Open, string Complaint, string vendor_rga,
string SALES_ORDER, string CUSTOMER, string Notes_and_Activity, string Final_Outcome, string Testing_Notes, string Invoice_Number, string Customer_Number, string Contact_Name, string Contact_Phone,
string Contact_Email, string Return_Qty_A, string Return_Item_A, string Return_Qty_B, string Return_Item_B,
string Return_Qty_C, string Return_Item_C, string Return_Qty_D, string Return_Item_D, string Return_Credit_A, string Return_Credit_B, string Return_Credit_C, string Return_Credit_D,
string Return_Ship_Method, string Replace_Qty_A, string Replace_Qty_B, string Replace_Qty_C, string Replace_Item_A, string Replace_Item_B, string Replace_Item_C, string Replace_SO_A,
string Replace_SO_B, string Replace_SO_C, string Energy_Price_Level, string Outside_Rep)
        {
            thisCmd.Parameters.AddWithValue("@CF", CF);
            thisCmd.Parameters.AddWithValue("@rgaDate", rgaDate);
            thisCmd.Parameters.AddWithValue("@Order_owner", Order_owner);
            thisCmd.Parameters.AddWithValue("@Order_status", Order_status);
            thisCmd.Parameters.AddWithValue("@RGA_Type", RGA_Type);
            thisCmd.Parameters.AddWithValue("@Energy_or_Chem", Energy_or_Chem);
            thisCmd.Parameters.AddWithValue("@Reason_RGA_Open", Reason_RGA_Open);
            thisCmd.Parameters.AddWithValue("@Complaint", Complaint);
            thisCmd.Parameters.AddWithValue("@vendor_rga", vendor_rga);
            thisCmd.Parameters.AddWithValue("@SALES_ORDER", SALES_ORDER);
            thisCmd.Parameters.AddWithValue("@CUSTOMER", CUSTOMER);
            thisCmd.Parameters.AddWithValue("@Notes_and_Activity", Notes_and_Activity);
            thisCmd.Parameters.AddWithValue("@Final_Outcome", Final_Outcome);
            thisCmd.Parameters.AddWithValue("@Testing_Notes", Testing_Notes);
            thisCmd.Parameters.AddWithValue("@Invoice_Number", Invoice_Number);
            thisCmd.Parameters.AddWithValue("@Customer_Number", Customer_Number);
            thisCmd.Parameters.AddWithValue("@Contact_Name", Contact_Name);
            thisCmd.Parameters.AddWithValue("@Contact_Phone", Contact_Phone);
            thisCmd.Parameters.AddWithValue("@Contact_Email", Contact_Email);
            thisCmd.Parameters.AddWithValue("@Return_Qty_A", Return_Qty_A);
            thisCmd.Parameters.AddWithValue("@Return_Item_A", Return_Item_A);
            thisCmd.Parameters.AddWithValue("@Return_Qty_B", Return_Qty_B);
            thisCmd.Parameters.AddWithValue("@Return_Item_B", Return_Item_B);
            thisCmd.Parameters.AddWithValue("@Return_Qty_C", Return_Qty_C);
            thisCmd.Parameters.AddWithValue("@Return_Item_C", Return_Item_C);
            thisCmd.Parameters.AddWithValue("@Return_Qty_D", Return_Qty_D);
            thisCmd.Parameters.AddWithValue("@Return_Item_D", Return_Item_D);
            thisCmd.Parameters.AddWithValue("@Return_Credit_A", Return_Credit_A);
            thisCmd.Parameters.AddWithValue("@Return_Credit_B", Return_Credit_B);
            thisCmd.Parameters.AddWithValue("@Return_Credit_C", Return_Credit_C);
            thisCmd.Parameters.AddWithValue("@Return_Credit_D", Return_Credit_D);
            thisCmd.Parameters.AddWithValue("@Return_Ship_Method", Return_Ship_Method);
            thisCmd.Parameters.AddWithValue("@Replace_Qty_A", Replace_Qty_A);
            thisCmd.Parameters.AddWithValue("@Replace_Qty_B", Replace_Qty_B);
            thisCmd.Parameters.AddWithValue("@Replace_Qty_C", Replace_Qty_C);
            thisCmd.Parameters.AddWithValue("@Replace_Item_A", Replace_Item_A);
            thisCmd.Parameters.AddWithValue("@Replace_Item_B", Replace_Item_B);
            thisCmd.Parameters.AddWithValue("@Replace_Item_C", Replace_Item_C);
            thisCmd.Parameters.AddWithValue("@Replace_SO_A", Replace_SO_A);
            thisCmd.Parameters.AddWithValue("@Replace_SO_B", Replace_SO_B);
            thisCmd.Parameters.AddWithValue("@Replace_SO_C", Replace_SO_C);
            thisCmd.Parameters.AddWithValue("@Energy_Price_Level", Energy_Price_Level);
            thisCmd.Parameters.AddWithValue("@Outside_Rep", Outside_Rep);
            return thisCmd;
        }

        public DataTable GetDataTableOfRecordNumberFromDB(string recordNumber)
        {
            try
            {
                string searchID = "SELECT * FROM RgaDatabase WHERE ID LIKE '%' + @ID + '%'";
                var adapter = new SqlDataAdapter(searchID, _con);
                adapter.SelectCommand.Parameters.AddWithValue("ID", recordNumber);
                _dt = new DataTable();
                adapter.Fill(_dt);
            }
            finally
            {
                _con.Close();
            }
            return _dt;
        }

        public DataTable GetCallLogDataTableByRecordFromDB(string recordNumber)
        {
            try
            { 
                string searchID = "SELECT * FROM modify_data_CallLog WHERE ID LIKE '%' + @ID + '%'";
                var adapter = new SqlDataAdapter(searchID, _con);
                adapter.SelectCommand.Parameters.AddWithValue("ID", recordNumber);
                _dt = new DataTable();
                adapter.Fill(_dt);
            }
            finally
            {
                _con.Close();
            }
            return _dt;
        }

        public DataTable GetCallLogDataTableBySearchFromDB(string search)
        {
            try
            {
                string searchID = "SELECT * FROM modify_data_CallLog WHERE CONCAT(contactPhone,contactName,contactEmail,customercode,companyName,companyCity,companyState,companyZip,reasonForCall,notesParagraph,callDate,outsideRep,customerNotes,businessNotes)  LIKE '%' + @search + '%'";
                var adapter = new SqlDataAdapter(searchID, _con);
                adapter.SelectCommand.Parameters.AddWithValue("@search", search);
                _dt = new DataTable();
                adapter.Fill(_dt);
            }
            finally
            {
                _con.Close();
            }
            return _dt;
        }

        public DataTable GetDataTableOfRGAVersions(string rgaNum)
        {
            try
            {
                var adapter = new SqlDataAdapter("SELECT ID FROM RgaDatabase WHERE CF LIKE '%' + @CF + '%'", _con);
                adapter.SelectCommand.Parameters.AddWithValue("CF", rgaNum);
                _dt = new DataTable();
                adapter.Fill(_dt);
            }
            finally
            {
                _con.Close();
            }
            return _dt;
        }

        public int UpdateCallLogRecord(int ID, string phone, string contactName, string email, string code, string companyName, string city, string state, string zip, string reason, string notes, string date, string rep, string contactnotes, string businessNotes)
        {
            var cmd = new SqlCommand("UPDATE modify_data_CallLog SET contactPhone = @contactPhone,contactName = @contactName,contactEmail = @contactEmail,customercode = @customerCode,companyName = @companyName,companyCity = @companyCity,companyState = @companyState,companyZip = @companyZip,reasonForCall = @reasonForCall,notesParagraph = @notesParagraph,callDate = @callDate,customerNotes = @customerNotes,businessNotes = @businessNotes WHERE ID = @ID", _con);
            AddCallLogParamtersToCmd(ref cmd, phone, contactName, email, code, companyName, city, state, zip, reason, notes, date, rep, contactnotes, businessNotes);
            cmd.Parameters.AddWithValue("@ID", ID);
            try
            {
                _con.Open();
                return cmd.ExecuteNonQuery();
            }
            finally
            {
                _con.Close();
            }
        }

    }
}
