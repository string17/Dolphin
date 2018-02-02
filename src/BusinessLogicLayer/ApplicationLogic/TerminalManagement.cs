using DataAccessLayer.CustomObjects;
using DolphinContext.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BLL.ApplicationLogic
{
    public class TerminalManagement
    {
        private DolServiceDb context = DolServiceDb.GetInstance();
        public List<DolTerminal> getTerminalById()
        {
            var actual = context.Fetch<DolTerminal>().ToList();
            return actual;
        }
        


        public List<DolTerminal> getAllTerminal()
        {
            string sql = "select * from DolTerminal";
            var actual = context.Fetch<DolTerminal>(sql);
            return actual;
        }

        public List<TerminalView> getSingleTerminal(int Tid)
        {
            string sql = " SELECT A.TID,A.TERMINALID,A.TSNUM,A.TENG,A.REGID,A.BRANDID,A.CUSTOMERID,A.TERMINALREF,A.TLOCATION,A.TALIAS,B.BRANDNAME,C.CUSTOMERALIAS,D.REGIONNAME,E.FIRSTNAME,E.MIDDLENAME,E.LASTNAME FROM DOLTERMINAL A INNER JOIN DOLBRAND B ON A.BRANDID=B.BRANDID INNER JOIN DOLCOMPANY C ON A.CUSTOMERID=C.CUSTOMERID INNER JOIN DOLREGION D ON A.REGID=D.REGID INNER JOIN DOLUSER E ON A.TENG=E.USERNAME AND A.TID=@0";
            var actual = context.Fetch<TerminalView>(sql,Tid);
            return actual;
        }
        public bool InsertTerminal(DolTerminal TerminalId)
        {
            try
            {
                context.Insert(TerminalId);
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public DolTerminal getTerminalByTID(string TerminalId)
        {
            string SQL = "Select * from DolTerminal where TerminalId =@0";
            var actual = context.FirstOrDefault<DolTerminal>(SQL, TerminalId);
            return actual;
        }

        public DolCompany GetCompanybyId(int id)
        {
            try
            {
                var actual = context.SingleOrDefault<DolCompany>("Where CUstomerId = @0", id);
                return actual;

            }catch(Exception ex)
            {
                return null;
            }
        }


        public TerminalView getTerminalId(int? Tid)
        {
            var actual = context.SingleOrDefault<DolTerminal>("where Tid = @0", Tid);
            var company = context.SingleOrDefault<DolCompany>("where CustomerName=@0", actual.CustomerName);
            var actual1 = context.SingleOrDefault<DolUser>("where Username=@0", actual.TEng);
            UserManagement usermanagement = new UserManagement();
            var userMgt = usermanagement.getUserById(Convert.ToInt32(Tid));
            Region region = new Region();
            var actual2 = region.getRegionByName(actual.RegionName);
            TerminalView term = new TerminalView()
            {
                BrandName = actual.BrandName,
                //BrandName = string.Empty,
                CustomerAlias = company.CustomerAlias,
                CustomerName = actual.CustomerName,
                RegName = actual.RegionName,
                RegionName=actual2.RegionName,
                TerminalId = actual.TerminalId,
                TAlias = actual.TAlias,
                TLocation=actual.TLocation,
                TSNum=actual.TSNum,
                TEng=actual.TEng,
                TerminalStatus=actual.TerminalStatus,
                TerminalRef=actual.TerminalRef,
                FirstName=actual1.FirstName,
                MiddleName=actual1.MiddleName,
                LastName = actual1.LastName,
                PhoneNos =userMgt.PhoneNos
             
            };
            return term;
        }

        public bool UpdateTerminal(string CustomerName, string TerminalId, string TSNum, string TerminalRef, string BrandName, string TLocation, string TAlias, string RegName, string TEng, bool? TerminalStatus, string ModifiedBy, DateTime ModifiedOn, int? Tid)
        {
            try
            {
                var Term = context.SingleOrDefault<DolTerminal>("WHERE Tid=@0", Tid);
                Term.CustomerName = CustomerName;
                Term.TerminalId = TerminalId;
                Term.TSNum = TSNum;
                Term.TerminalRef = TerminalRef;
                Term.BrandName = BrandName;
                Term.TLocation = TLocation;
                Term.TAlias = TAlias;
                Term.RegionName = RegName;
                Term.TEng = TEng;
                Term.TerminalStatus = TerminalStatus;
                Term.ModifiedBy = ModifiedBy;
                Term.ModifiedOn = ModifiedOn;
                context.Update(Term);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<DolState> getAllState()
        {
            var actual = context.Fetch<DolState>().ToList();
            return actual;
        }

        public string DoFileUpload(HttpPostedFileBase terminalFile)
        {
            if (terminalFile == null)
            {
                return " ";
            }
            string result = DateTime.Now.Millisecond + "Terminal.xls";
            terminalFile.SaveAs(HttpContext.Current.Server.MapPath("~/TerminalFile/") + result);
            return result;
        }

    }
}