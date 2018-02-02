using DataAccessLayer.CustomObjects;
using DolphinContext.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BLL.ApplicationLogic
{
    public class IncidentManagement
    {
        private DolServiceDb context = DolServiceDb.GetInstance();
        public List<DolIncident> GetAllIncident()
        {
            var actual = context.Fetch<DolIncident>().ToList();
            return actual;
        }

        public List<IncidentView> GetAllIncidents()
        {
            string sql = "SELECT A.IncidentId,A.TerminalId,A.UserName,A.IncidentDesc,A.ResponseDateTime,A.IncidentDateTime,A.IncidentStatus,B.TerminalRef,B.TAlias,B.TLocation,C.CustomerName FROM DolIncident A INNER JOIN DolTerminal B ON A.TerminalId=B.TerminalId INNER JOIN DolCompany C ON C.CustomerName=B.CustomerName";
            var actual = context.Fetch<IncidentView>(sql);
            return actual;
        }

        public List<IncidentViewModel> GetCustomerCall(string Username)
        {
            string sql = "SELECT A.IncidentId,A.TerminalId,A.UserName,A.IncidentDesc,A.ResponseDateTime,A.IncidentDateTime,A.IncidentStatus,B.TerminalRef,B.TAlias,B.TLocation,C.CustomerName FROM DolIncident A INNER JOIN DolTerminal B ON A.TerminalId=B.TerminalId INNER JOIN DolCompany C ON C.CustomerId=B.CustomerId Where A.UserName=@0";
            var actual = context.Fetch<IncidentViewModel>(sql,Username);
            return actual;
        }

        public DolIncident GetIncidentById(int? Id)
        {
            string SQL = "SELECT * FROM DolIncident WHERE IncidentID=@0";
            var actual = context.FirstOrDefault<DolIncident>(SQL, Id);
            return actual;
        }

        public DolIncident GetIncidentByUsername(string username)
        {
            string SQL = "SELECT * FROM DolIncident WHERE User_Name=@0";
            var actual = context.FirstOrDefault<DolIncident>(SQL, username);
            return actual;
        }

        public DolIncident GetIncidentByStatus(string Status)
        {
            string SQL = "SELECT * FROM DolIncident WHERE IncidentStatus=@0";
            var actual = context.FirstOrDefault<DolIncident>(SQL, Status);
            return actual;
        }

        //public static int GetIncidentSequence()
        //{
        //    int nextSequence = 0;
        //    string sql = "";
        //}

        //public static string ReturnIncidentSequenceString()
        //{
        //    string nextSequence = "";
        //    int nextSquenceTemp = 0;
        //    string dateTemp = DateTime.Now.ToString("yyMMddHHmmss");
        //    nextSquenceTemp = GetIncidentSequence();
        //    nextSequence = nextSquenceTemp.ToString().PadLeft(18, '0');
        //    nextSequence = dateTemp + nextSequence;
        //    return nextSequence;
        //}
    }
}