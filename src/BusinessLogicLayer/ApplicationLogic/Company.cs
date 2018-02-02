using DolphinContext.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BLL.ApplicationLogic
{
    public class Company
    {
        private DolServiceDb context = DolServiceDb.GetInstance();
        public List<DolCompany> getCompanyById()
        {
            var actual = context.Fetch<DolCompany>().ToList();
            return actual;
        }

        public List<DolCompany> getCompanyByGroup()
        {
            string sql = "Select * from DolCompany where CustomerName <> 'Altaviz' ";
            var actual = context.Fetch<DolCompany>(sql).ToList();
            return actual;
        }

        public DolCompany getCompanyId(int? Id)
        {
            string sql = "Select * from DolCompany where CustomerId =@0";
            var actual = context.FirstOrDefault<DolCompany>(sql, Id);
            return actual;

        }

        public DolCompany getCompanyByName(string CustomerName)
        {
            string SQL = "Select * from DolCompany where CustomerName =@0";
            var actual = context.FirstOrDefault<DolCompany>(SQL, CustomerName);
            return actual;
        }


        public string DoFileUpload(HttpPostedFileBase pic, string filename = "")
        {
            if (pic == null && string.IsNullOrWhiteSpace(filename))
            {
                return "";
            }
            if (!string.IsNullOrWhiteSpace(filename) && pic == null) return filename;
        
            string result = DateTime.Now.Millisecond + "CompanyLogo.jpg";
            pic.SaveAs(HttpContext.Current.Server.MapPath("~/Banner/") + result);
            return result;
        }


        public bool InsertCompany(DolCompany CustomerName)
        {
            try
            {
                context.Insert(CustomerName);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public bool UpdateCompany(string CustomerName, string CustomerAlias, string CustomerBanner, bool? CustomerStatus,string CustomerRespTime,string CustomerRestTime, string CustomerRest1Time, int? CustomerId)
        {
            try
            {
                var clients = context.SingleOrDefault<DolCompany>("WHERE CustomerId=@0", CustomerId);
                clients.CustomerName = CustomerName;
                clients.CustomerAlias = CustomerAlias;
                clients.CustomerStatus = CustomerStatus;
                clients.CustomerBanner = CustomerBanner;
                clients.CustomerRespTime = CustomerRespTime;
                clients.CustomerRest1Time = CustomerRestTime;
                clients.CustomerRest1Time = CustomerRest1Time;
                context.Update(clients);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
