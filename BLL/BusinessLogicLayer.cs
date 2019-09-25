using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using DAL;

namespace BLL
{
    public class BusinessLogicLayer
    {
        readonly DataAccessLayer dal = new DataAccessLayer();

        public string[] GetNameList(string phone)
        {
            var nameList = dal.PopulateNameField(phone.Trim());
            if (nameList.Count == 0)
            {
                return new string[1] { "" };
            }
            else
            {
                return nameList.ToArray();
            }
        }

        public string GetNameNotes(string phone, string name)
        {
            var nameNotes = dal.PopulateNameNotes(phone.Trim(), name.Trim()).Where(x => x.Length > 0).ToArray();
            if (nameNotes.Count() < 1)
            {
                return "";
            }
            else
            {
                return nameNotes[nameNotes.Count() - 1];
            }
        }

        public string GetBusinessNotes(string phone, string code)
        {
            var businessNotes = dal.PopulateBusinessNotes(phone.Trim(), code.Trim()).Where(x => x.Length > 0).ToArray();
            if (businessNotes.Count() < 1)
            {
                return "";
            }
            else
            {
                return businessNotes[businessNotes.Count() - 1];
            }
        }

        public string[] GetCustomerEmail(string phone, string name)
        {
            var emailList = dal.PopulateCustomerEmail(phone.Trim(), name.Trim());
            if (emailList.Count == 0 || phone.Length == 0 || name.Length == 0)
            {
                return new string[1] { "" };
            }
            else
            {
                return emailList.ToArray();
            }
        }

        public string[] GetCustomerCode(string phone, string name)
        {
            var customerCodeList = dal.PopulateCustomerCode(phone.Trim(), name.Trim());
            if (phone.Length == 0 || name.Length == 0 || customerCodeList.Count == 0)
            {
                return new string[1] { "" };
            }
            return customerCodeList.ToArray();
        }

        public string[] GetCompanyName(string code)
        {
            var companyNameList = dal.PopulateCompanyName(code.Trim());
            if(code.Length == 0 || companyNameList.Count == 0)
            {
                return new string[1] { "" };
            }
            return companyNameList.ToArray();
        }

        public string GetCompanyCity(string phone, string code)
        {
            if (phone.Length == 0 || code.Length == 0)
            {
                return "";
            }
            return dal.PopulateCompanyCity(phone.Trim(), code.Trim());
        }

        public string GetCompanyState(string phone, string code)
        {
            if (phone.Length == 0 || code.Length == 0)
            {
                return "";
            }
            return dal.PopulateCompanyState(phone.Trim(), code.Trim());
        }

        public string GetCompanyZip(string phone, string code)
        {
            if (phone.Length == 0 || code.Length == 0)
            {
                return "";
            }
            return dal.PopulateCompanyZip(phone.Trim(), code.Trim());
        }

        public string[] GetCompanyCityStateZip(string code)
        {
            var cityStateZipList = dal.PopulateCompanyCityStateZip(code.Trim());
            if (code.Length == 0 || cityStateZipList.Count == 0)
            {
                return new string[1] { "" };
            }
            return cityStateZipList.ToArray();
        }

        public DataTable GetGridViewData(string phone, string company, string city)
        {
            if (phone.Length == 0 || company.Length == 0 || city.Length == 0)
            {
                return null;
            }
            return dal.PopulateDataGridViewByPhoneCompanyCity(phone.Trim(), company.Trim(), city.Trim());
        }

        public int SaveToDatabase(string phone, string contactName, string email, string code, string companyName, string city, string state, string zip, string reason, string notes, string date, string rep, string contactNotes, string businessNotes)
        {
            return dal.SaveToDatabase(phone.Trim(), contactName.Trim(), email.Trim(), code.Trim(), companyName.Trim(), city.Trim(), state.Trim(), zip.Trim(), reason.Trim(), notes.Trim(), date.Trim(), rep.Trim(), contactNotes.Trim(), businessNotes.Trim());
        }

        public int UpdateCallLogRecord(int ID, string phone, string contactName, string email, string code, string companyName, string city, string state, string zip, string reason, string notes, string date, string rep, string contactnotes, string businessNotes)
        {
            return dal.UpdateCallLogRecord(ID, phone.Trim(), contactName.Trim(), email.Trim(), code.Trim(), companyName.Trim(), city.Trim(), state.Trim(), zip.Trim(), reason.Trim(), notes.Trim(), date.Trim(), rep.Trim(), contactnotes.Trim(), businessNotes.Trim());
        }

        public DataTable GetCallLogDataTableBySearchFromDB(string search)
        {
            return dal.GetCallLogDataTableBySearchFromDB(search.Trim());
        }

        public DataTable GetDataTableOfCallLogByRecordNumber(string recordNumber)
        {
            return dal.GetCallLogDataTableByRecordFromDB(recordNumber.Trim());
        }
    }

}
