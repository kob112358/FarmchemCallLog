using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CallLog;
using System.Linq;
using ClassModels.CallClasses;

namespace DAL
{
    public class DataAccessLayer
    {
        
        public List<Address> PopulateCompanyCityStateZip(string code)
        {
            try
            {
                using (var context = new CallContext())
                {
                    return context.Calls
                        .Where(s => s.Bus.CustomerCode == code)
                        .Select(s => new Address()
                        {
                            City = s.Add.City,
                            State = s.Add.State,
                            Zip = s.Add.Zip
                        })
                        .OrderBy(s => s.City)
                        .Distinct()
                        .ToList();
                }
            }
            catch(Exception ex)
            {
                throw (ex);
            }
        }
        public List<DTCall> GetCallLogDataTableBySearchFromDB(string search)
        {
            try
            {
                using (var context = new CallContext())
                {
                    return context.Calls
                        .Where(s => s.Add.City.Contains(search) || s.Add.State.Contains(search) || s.Add.Zip.Contains(search) || s.Bus.CompanyName.Contains(search) || s.Bus.CompanyNotes.Contains(search) ||
                          s.Bus.CustomerCode.Contains(search) || s.Bus.OutsideRep.Contains(search) || s.CallInformation.CallNotes.Contains(search) || s.CallInformation.ReasonForCall.Contains(search) ||
                          s.Cust.ContactName.Contains(search) || s.Cust.CustomerNotes.Contains(search) || s.Cust.Phone.Contains(search) || s.Cust.Email.Contains(search))
                        .Select(s => new DTCall()
                        {
                            CallID = s.CallID,
                            CallDate = s.CallInformation.CallDate,
                            ContactName = s.Cust.ContactName,
                            CompanyName = s.Bus.CompanyName,
                            City = s.Add.City,
                            State = s.Add.State,
                            CallNotes = s.CallInformation.CallNotes,
                            CallResolved = s.CallInformation.CallResolved
                        })
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        public Call GetCallByRecordFromDB(int recordNumber)
        {
            try
            {
                using (var context = new CallContext())
                {
                    return context.Calls
                        .Where(s => s.CallID == recordNumber)
                        .Select(s => new Call()
                        {
                            Add = new Address()
                            {
                                City = s.Add.City,
                                State = s.Add.State,
                                Zip = s.Add.Zip
                            },
                            Bus = new Business()
                            {
                                CompanyName = s.Bus.CompanyName,
                                CompanyNotes = s.Bus.CompanyNotes,
                                CustomerCode = s.Bus.CustomerCode,
                                OutsideRep = s.Bus.OutsideRep
                            },
                            CallInformation = new CallInfo()
                            {
                                CallDate = s.CallInformation.CallDate,
                                CallNotes = s.CallInformation.CallNotes,
                                CallResolved = s.CallInformation.CallResolved,
                                ReasonForCall = s.CallInformation.ReasonForCall
                            },
                            Cust = new Customer()
                            {
                                ContactName = s.Cust.ContactName,
                                Email = s.Cust.Email,
                                Phone = s.Cust.Phone
                            }
                        })
                        .Last();
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        public int UpdateCallLogRecord(Call cal)
        {
            try
            {
                using (var context = new CallContext())
                {
                    context.Update<Call>(cal);
                    return context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        public int SaveToDatabase(Call cal)
        {
            try
            {
                using (var context = new CallContext())
                {
                    context.Add(cal);
                    return context.SaveChanges();
                }
            }
            catch
            {
                return 0;
            }
        }
        public string PopulateCompanyZip(string phone, string code)
        {
            try
            {
                using (var context = new CallContext())
                {

                    return context.Calls
                        .Where(s => s.Cust.Phone == phone)
                        .Where(s => s.Bus.CustomerCode == code)
                        .Select(s => s.Add.Zip)
                        .FirstOrDefault();
                };
            }
            catch(Exception ex)
            {
                throw (ex);
            }
        }
        public string[] PopulateNameField(string phone)
        {
            try
            {
                using (var context = new CallContext())
                {
                    return context.Calls
                        .Where(s => s.Cust.Phone == phone)
                        .Select(s => s.Cust.ContactName)
                        .Distinct()
                        .ToArray();
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        public string PopulateNameNotes(string phone, string name)
        {
            try
            {
                using (var context = new CallContext())
                {
                    return context.Calls
                        .Where(s => s.Cust.Phone == phone)
                        .Where(s => s.Cust.ContactName == name)
                        .Select(s => s.Cust.CustomerNotes)
                        .Last();

                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        public string PopulateBusinessNote(string code)
        {
            try
            {
                using (var context = new CallContext())
                {
                    return context.Calls
                        .Where(s => s.Bus.CustomerCode == code)
                        .Select(s => s.Bus.CompanyNotes)
                        .Last();
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        public List<string> PopulateCustomerCode(string phone, string name)
        {
            try
            {
                using (var context = new CallContext())
                {
                    return context.Calls
                        .Where(s => s.Cust.Phone == phone)
                        .Where(s => s.Cust.ContactName == name)
                        .Select(s => s.Bus.CustomerCode)
                        .OrderBy(s => s.Count())
                        .Distinct()
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        public string PopulateCompanyName(string code)
        {
            try
            {
                using (var context = new CallContext())
                {
                    return context.Calls
                        .Where(s => s.Bus.CustomerCode == code)
                        .Select(s => s.Bus.CompanyName)
                        .Last();
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        public List<string> PopulateCustomerEmail(string phone, string name)
        {
            try
            {
                using (var context = new CallContext())
                {
                    return context.Calls
                        .Where(s => s.Cust.Phone == phone)
                        .Where(s => s.Cust.ContactName == name)
                        .OrderBy(s => s.Cust.Email)
                        .Select(s => s.Cust.Email)
                        .Distinct()
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        public List<DTCall> GetGridViewAllAtOnce(string phone, string company, string city)
        {
            try
            {
                using (var context = new CallContext())
                {
                    return context.Calls
                        .Where(s => s.Cust.Phone == phone)
                        .Where(s => s.Bus.CompanyName == company)
                        .Where(s => s.Add.City == city)
                        .Select(s => new DTCall()
                        {
                            CallID = s.CallID,
                            CallDate = s.CallInformation.CallDate,
                            ContactName = s.Cust.ContactName,
                            CompanyName = s.Bus.CompanyName,
                            City = s.Add.City,
                            State = s.Add.State,
                            CallNotes = s.CallInformation.CallNotes,
                            CallResolved = s.CallInformation.CallResolved
                        })
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
    }
}
