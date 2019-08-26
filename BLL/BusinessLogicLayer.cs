using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;
using DAL;

namespace BLL
{
    public class BusinessLogicLayer
    {
        DataAccessLayer dal = new DataAccessLayer();
        public string[] GetNameList(string phone)
        {
            return dal.PopulateNameField(phone.Trim()).ToArray();
        }

        public string GetCustomerEmail(string phone, string name)
        {
            return dal.PopulateCustomerEmail(phone.Trim(), name.Trim());
        }

        public string GetCustomerCode(string phone, string name)
        {
            return dal.PopulateCustomerCode(phone.Trim(), name.Trim());
        }

        public string GetCompanyName(string code)
        {
            return dal.PopulateCompanyName(code.Trim());
        }

        public string GetCompanyCity(string phone, string code)
        {
            return dal.PopulateCompanyCity(phone.Trim(), code.Trim());
        }

        public string GetCompanyState(string phone, string code)
        {
            return dal.PopulateCompanyState(phone.Trim(), code.Trim());
        }

        public string GetCompanyZip(string phone, string code)
        {
            return dal.PopulateCompanyZip(phone.Trim(), code.Trim());
        }

        public DataTable GetGridViewData(string phone, string company, string city)
        {
            return dal.PopulateDataGridViewByPhoneCompanyCity(phone.Trim(), company.Trim(), city.Trim());
        }

        public int SaveToDatabase(string phone, string contactName, string email, string code, string companyName, string city, string state, string zip, string so, string part, string reason, string notes, string date)
        {
            return dal.SaveToDatabase(phone, contactName, email, code, companyName, city, state, zip, so, part, reason, notes, date);
        }

        public string GetRGATextBasedOnReturn(string returnType, string rga)
        {
            if (returnType == "a")
            {
                return "You will not be needing to return any items - you may discard or do with them as you please.";
            }
            if (returnType == "b")
            {
                return "Please print the attached RGA form and place it on the outside of the box(es). Use UPS or FedEx to return it to us.";
            }
            if(returnType == "c" || returnType == "d")
            {
                return "You will be receiving an e-mail with a return shipping label. Please print it off and place it on the outside of your box(es).";
            }
            else
            {
                return $"If you have any questions regarding your {rga}, please let me know.";
            }
            

            
        }

        public int SaveRGAToDatabase(string CF,string rgaDate, string Order_owner, string Order_status, string RGA_Type, string Energy_or_Chem, string Reason_RGA_Open, string Complaint, string vendor_rga,
string SALES_ORDER, string CUSTOMER, string Notes_and_Activity, string Final_Outcome, string Testing_Notes, string Invoice_Number, string Customer_Number, string Contact_Name, string Contact_Phone,
string Contact_Email, string Return_Qty_A, string Return_Item_A, string Return_Qty_B, string Return_Item_B,
string Return_Qty_C, string Return_Item_C, string Return_Qty_D, string Return_Item_D, string Return_Credit_A, string Return_Credit_B, string Return_Credit_C, string Return_Credit_D,
string Return_Ship_Method, string Replace_Qty_A, string Replace_Qty_B, string Replace_Qty_C, string Replace_Item_A, string Replace_Item_B, string Replace_Item_C, string Replace_SO_A,
string Replace_SO_B, string Replace_SO_C, string Energy_Price_Level)
        {
            return dal.SaveRGAToDatabase(CF.Trim(), rgaDate.Trim(), Order_owner.Trim(), Order_status.Trim(), RGA_Type.Trim(), Energy_or_Chem.Trim(), Reason_RGA_Open.Trim(), Complaint.Trim(), vendor_rga.Trim(),
                SALES_ORDER.Trim(), CUSTOMER.Trim(), Notes_and_Activity.Trim(), Final_Outcome.Trim(), Testing_Notes.Trim(), Invoice_Number.Trim(), Customer_Number.Trim(), Contact_Name.Trim(), Contact_Phone.Trim(),
                Contact_Email.Trim(), Return_Qty_A.Trim(), Return_Item_A.Trim(), Return_Qty_B.Trim(), Return_Item_B.Trim(), Return_Qty_C.Trim(), Return_Item_C.Trim(), Return_Qty_D.Trim(), Return_Item_D.Trim(),
                Return_Credit_A.Trim(), Return_Credit_B.Trim(), Return_Credit_C.Trim(), Return_Credit_D.Trim(), Return_Ship_Method.Trim(), Replace_Qty_A.Trim(), Replace_Item_B.Trim(), Replace_Item_C.Trim(), 
                Replace_Item_A.Trim(), Replace_Item_B.Trim(), Replace_Item_C.Trim(), Replace_SO_A.Trim(), Replace_SO_B.Trim(), Replace_SO_C.Trim(), Energy_Price_Level.Trim());
        }

        public DataTable GetDataTableOfRGAByRecordNumber(string recordNum)
        {
            return dal.GetDataTableOfRecordNumberFromDB(recordNum.Trim());
        }


        public List<string> LoadRGAVersionsFromRecord(string rgaNumber)
        {
            DataTable dt = dal.GetDataTableOfRGAVersions(rgaNumber.Trim());
            List<string> versions = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                versions.Add(dt.Rows[i]["ID"].ToString() + " V" + (i + 1));
            }
            return versions;
        }
    }

    
}
