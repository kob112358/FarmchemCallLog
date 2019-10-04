using CallLog;
using ClassModels.CallClasses;
using DAL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BLL
{
    public class BusinessLogicLayer
    {
        readonly DataAccessLayer dal = new DataAccessLayer();
        private const string _EXCEPTIONFILEPATH = "C:\\Windows\\temp\\CallLogError.txt";

        public List<Address> GetCompanyCityStateZip(string code)
        {
            try
            {
                var cityStateZipList = dal.PopulateCompanyCityStateZip(code.Trim());
                if (code.Length == 0 || cityStateZipList.Count == 0)
                {
                    return new List<Address>() { new Address() { City = "", State = "", Zip = "" } };
                }
                return cityStateZipList;
            }
            catch (Exception ex)
            {
                LogError(ex);
                return new List<Address>();
            }
        }

        public List<DTCall> GetCallLogDataTableBySearchFromDB(string search)
        {
            try
            {
                return dal.GetCallLogDataTableBySearchFromDB(search.Trim());
            }
            catch (Exception ex)
            {
                LogError(ex);
                return new List<DTCall>();
            }
        }

        public int SaveToDatabase(string phone, string contactName, string email, string code, string companyName, string city, string state, string zip, string reason, string notes, DateTime date, string rep, string contactNotes, string businessNotes, bool completed)
        {
            try
            {
                var cal = CreateCallWithInfo(phone, contactName, email, code, companyName, city, state, zip, reason, notes, date, rep, contactNotes, businessNotes, completed);
                return dal.SaveToDatabase(cal);
            }
            catch (Exception ex)
            {
                LogError(ex);
                return -1;
            }
        }

        public int UpdateCallLogRecord(int ID, string phone, string contactName, string email, string code, string companyName, string city, string state, string zip, string reason, string notes, DateTime date, string rep, string contactnotes, string businessNotes, bool completed)
        {
            try
            {
                var cal = CreateCallWithInfo(phone.Trim(), contactName.Trim(), email.Trim(), code.Trim(), companyName.Trim(), city.Trim(), state.Trim(), zip.Trim(), reason.Trim(), notes.Trim(), date, rep.Trim(), contactnotes.Trim(), businessNotes.Trim(), completed);
                cal.CallID = ID;
                return dal.UpdateCallLogRecord(cal);
            }
            catch (Exception ex)
            {
                LogError(ex);
                return -1;
            }
        }

        private Call CreateCallWithInfo(string phone, string contactName, string email, string code, string companyName, string city, string state, string zip, string reason, string notes, DateTime date, string rep, string contactNotes, string businessNotes, bool completed)
        {
            try
            {
                var Add = new Address()
                {
                    City = city.Trim(),
                    State = state.Trim(),
                    Zip = zip.Trim(),
                };
                var Call = new CallInfo()
                {
                    ReasonForCall = reason.Trim(),
                    CallNotes = notes.Trim(),
                    CallResolved = completed,
                    CallDate = date,
                };
                var Bus = new Business()
                {
                    CompanyName = companyName.Trim(),
                    CustomerCode = code.Trim(),
                    CompanyNotes = businessNotes.Trim(),
                    OutsideRep = rep.Trim(),
                    Address = Add
                };
                var Cust = new Customer()
                {
                    ContactName = contactName.Trim(),
                    Email = email.Trim(),
                    Phone = phone.Trim(),
                    CustomerNotes = contactNotes.Trim(),
                    Business = Bus
                };
                return new Call()
                {
                    Cust = Cust,
                    Add = Add,
                    Bus = Bus,
                    CallInformation = Call
                };
            }
            catch (Exception ex)
            {
                LogError(ex);
                return new Call();
            }
        }

        public string GetNameNotes(string phone, string name)
        {
            try
            {
                return dal.PopulateNameNotes(phone.Trim(), name.Trim());
            }
            catch (Exception ex)
            {
                LogError(ex);
                return "";
            }
        }

        public string[] GetNameList(string phone)
        {
            try
            {
                var nameList = dal.PopulateNameField(phone.Trim());
                if (nameList.Count() == 0)
                {
                    return new string[1] { "" };
                }
                else
                {
                    return nameList;
                }
            }
            catch (Exception ex)
            {
                LogError(ex);
                return new string[1] { "" };
            }
        }

        public string GetCustomerEmail(string phone, string name)
        {
            try
            {
                var emailList = dal.PopulateCustomerEmail(phone.Trim(), name.Trim());
                if (String.IsNullOrEmpty(emailList) || phone.Length == 0 || name.Length == 0)
                {
                    return "";
                }
                else
                {
                    return emailList;
                }
            }
            catch (Exception ex)
            {
                LogError(ex);
                return "";
            }
        }
        public string GetBusinessNotes(string code)
        {
            try
            {
                if (String.IsNullOrEmpty(code))
                {
                    return "";
                }
                var businessNotes = dal.PopulateBusinessNote(code.Trim());
                if (String.IsNullOrEmpty(businessNotes))
                {
                    return "";
                }
                else
                {
                    return businessNotes;
                }
            }
            catch (Exception ex)
            {
                LogError(ex);
                return "";
            }
        }
        public string GetCustomerCode(string phone, string name)
        {
            try
            {
                var customerCodeList = dal.PopulateCustomerCode(phone.Trim(), name.Trim());
                if (phone.Length == 0 || name.Length == 0 || String.IsNullOrEmpty(customerCodeList))
                {
                    return "";
                }
                return customerCodeList;
            }
            catch (Exception ex)
            {
                LogError(ex);
                return "";
            }
        }
        public string GetCompanyName(string code)
        {
            try
            {
                if (code.Length == 0)
                {
                    return "";
                }
                return dal.PopulateCompanyName(code);
            }
            catch (Exception ex)
            {
                LogError(ex);
                return "";
            }
        }

        public List<DTCall> GetGridViewDataByPhoneCompany(string phone, string company)
        {
            try
            {
                if (phone.Length == 0 || company.Length == 0)
                {
                    return null;
                }
                return dal.GetGridViewFromPhoneCompany(phone.Trim(), company.Trim());
            }
            catch (Exception ex)
            {
                LogError(ex);
                return new List<DTCall>();
            }
        }
        public Call GetCallByRecordNumber(string recordNumber)
        {
            try
            {
                return dal.GetCallByRecordFromDB(Convert.ToInt32(recordNumber.Trim()));
            }
            catch (Exception ex)
            {
                LogError(ex);
                return new Call();
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
                throw (ex);
            }
        }
    }

}
