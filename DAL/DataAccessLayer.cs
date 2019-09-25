using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class DataAccessLayer
    {
        private readonly SqlConnection _con = new SqlConnection("data source=kobpc\\sqlexpress;initial catalog=modifycalllog;integrated security=true;Connect Timeout=60");
        private DataTable _dt;


        public List<string> PopulateNameField(string phone)
        {
            var adapter = new SqlDataAdapter("SELECT contactName, COUNT(contactName) AS MOST_FREQUENT FROM modify_data_calllog WHERE contactPhone LIKE '%' + @contactPhone + '%' GROUP BY contactName ORDER BY MOST_FREQUENT DESC", _con);
            adapter.SelectCommand.Parameters.AddWithValue("contactPhone", phone);
            var returnList = GenerateListForComboboxFromDB(adapter, "contactName");
            adapter.Dispose();
            return returnList;
        }

        public List<string> PopulateNameNotes(string phone, string name)
        {
            var adapter = new SqlDataAdapter("SELECT customerNotes FROM modify_data_calllog WHERE contactPhone LIKE '%' + @contactPhone + '%' AND contactName LIKE '%' + @contactName + '%'", _con);
            adapter.SelectCommand.Parameters.AddWithValue("contactPhone", phone);
            adapter.SelectCommand.Parameters.AddWithValue("contactName", name);
            var returnList = GenerateListForComboboxFromDB(adapter, "customerNotes");
            adapter.Dispose();
            return returnList;
        }

        public List<string> PopulateBusinessNotes(string phone, string code)
        {
            var adapter = new SqlDataAdapter("SELECT businessNotes FROM modify_data_calllog WHERE contactPhone LIKE '%' + @contactPhone + '%' AND customerCode LIKE '%' + @customerCode + '%'", _con);
            adapter.SelectCommand.Parameters.AddWithValue("contactPhone", phone);
            adapter.SelectCommand.Parameters.AddWithValue("customerCode", code);
            var returnList = GenerateListForComboboxFromDB(adapter, "businessNotes");
            adapter.Dispose();
            return returnList;
        }


        public List<string> PopulateCustomerEmail(string phone, string name)
        {
            var adapter = new SqlDataAdapter("SELECT contactEmail, COUNT(contactEmail) AS MOST_FREQUENT FROM modify_data_calllog WHERE contactPhone LIKE '%' + @contactPhone + '%' AND contactName LIKE '%' + @contactName + '%' GROUP BY contactEmail ORDER BY MOST_FREQUENT DESC", _con);
            adapter.SelectCommand.Parameters.AddWithValue("contactPhone", phone);
            adapter.SelectCommand.Parameters.AddWithValue("contactName", name);
            var returnList = GenerateListForComboboxFromDB(adapter, "contactEmail");
            adapter.Dispose();
            return returnList;
        }

        public List<string> PopulateCustomerCode(string phone, string name)
        {
            var adapter = new SqlDataAdapter("SELECT customerCode, COUNT(customerCode) AS MOST_FREQUENT FROM modify_data_calllog WHERE contactPhone LIKE '%' + @contactPhone + '%' AND contactName LIKE '%' + @contactName + '%' GROUP BY customerCode ORDER BY MOST_FREQUENT DESC", _con);
            adapter.SelectCommand.Parameters.AddWithValue("contactPhone", phone);
            adapter.SelectCommand.Parameters.AddWithValue("contactName", name);
            var returnList = GenerateListForComboboxFromDB(adapter, "customerCode");
            adapter.Dispose();
            return returnList;
        }

        public List<string> PopulateCompanyName(string code)
        {
            var adapter = new SqlDataAdapter("SELECT TOP (1) companyName FROM modify_data_calllog WHERE customerCode LIKE '%' + @customerCode + '%' GROUP BY companyName ORDER BY companyName DESC", _con);
            adapter.SelectCommand.Parameters.AddWithValue("customerCode", code);
            var returnList = GenerateListForComboboxFromDB(adapter, "companyName");
            adapter.Dispose();
            return returnList;
        }

        public List<string> PopulateCompanyCityStateZip(string code)
        {
            var adapter = new SqlDataAdapter("SELECT companyCityStateZip, COUNT(companyCityStateZip) AS MOST_FREQUENT FROM modify_data_calllog WHERE customerCode LIKE '%' + @customerCode + '%' GROUP BY companyCityStatezip ORDER BY MOST_FREQUENT DESC", _con);
            adapter.SelectCommand.Parameters.AddWithValue("customerCode", code);
            var returnList = GenerateListForComboboxFromDB(adapter, "companyCityStateZip");
            adapter.Dispose();
            return returnList;
        }


        public string PopulateCompanyCity(string phone, string code)
        {
            var adapter = new SqlDataAdapter("SELECT TOP (3) companyCity FROM modify_data_calllog WHERE contactPhone LIKE '%' + @contactPhone + '%' AND customerCode LIKE '%' + @customerCode + '%' GROUP BY companyCity ORDER BY COUNT(companyCity) DESC", _con);
            adapter.SelectCommand.Parameters.AddWithValue("customerCode", code);
            adapter.SelectCommand.Parameters.AddWithValue("contactPhone", phone);
            var returnString = GenerateListForComboboxFromDB(adapter, "companyCity")[0];
            adapter.Dispose();
            return returnString;
        }

        public string PopulateCompanyState(string phone, string code)
        {
            var adapter = new SqlDataAdapter("SELECT TOP (3) companyState FROM modify_data_calllog WHERE contactPhone LIKE '%' + @contactPhone + '%' AND customerCode LIKE '%' + @customerCode + '%' GROUP BY companyState ORDER BY COUNT(companyState) DESC", _con);
            adapter.SelectCommand.Parameters.AddWithValue("customerCode", code);
            adapter.SelectCommand.Parameters.AddWithValue("contactPhone", phone);
            var returnString = GenerateListForComboboxFromDB(adapter, "companyState")[0];
            adapter.Dispose();
            return returnString;
        }

        public string PopulateCompanyZip(string phone, string code)
        {
            var adapter = new SqlDataAdapter("SELECT TOP (3) companyZip FROM modify_data_calllog WHERE contactPhone LIKE '%' + @contactPhone + '%' AND customerCode LIKE '%' + @customerCode + '%' GROUP BY companyZip ORDER BY COUNT(companyZip) DESC", _con);
            adapter.SelectCommand.Parameters.AddWithValue("customerCode", code);
            adapter.SelectCommand.Parameters.AddWithValue("contactPhone", phone);
            var returnString = GenerateListForComboboxFromDB(adapter, "companyZip")[0];
            adapter.Dispose();
            return returnString;
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
                adapter.Dispose();
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

        public DataTable GetCallLogDataTableByRecordFromDB(string recordNumber)
        {
            try
            { 
                string searchID = "SELECT * FROM modify_data_CallLog WHERE ID LIKE '%' + @ID + '%'";
                var adapter = new SqlDataAdapter(searchID, _con);
                adapter.SelectCommand.Parameters.AddWithValue("ID", recordNumber);
                _dt = new DataTable();
                adapter.Fill(_dt);
                adapter.Dispose();
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
                adapter.Dispose();
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
