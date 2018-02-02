using BLL.ApplicationLogic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BusinessLogicLayer.ApplicationLogic
{
    public class LogManager
    {
        public static void ErrorLoging(Exception ex)
        {
            string result = DateTime.Now + "ErrorLog.txt";
            string strPath = @"C:\Dolphin\" + result;
            if (!File.Exists(strPath))
            {
                File.Create(strPath).Dispose();
            }
            using (StreamWriter sw = File.AppendText(strPath))
            {
              
                sw.WriteLine("Error Message: " + ex.Message + DateTime.Now);
                sw.WriteLine("Stack Trace: " + ex.StackTrace + DateTime.Now);
               

            }
           
        }

        public static void ReadError()
        {
            string strPath = @"D:\Rekha\Log.txt";
            using (StreamReader sr = new StreamReader(strPath))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    Console.WriteLine(line);
                }
            }
        }

        
        public static void InsertLog(Constants.AuditActionType action_type, string action, string performed_by)
        {
            //string result = DateTime.Now + "Log.txt";
            string strPath = @"C:\Dolphin\Log.txt";
            if (!File.Exists(strPath))
            {
                File.Create(strPath).Dispose();
            }
            using (StreamWriter sw = File.AppendText(strPath))
            {
          
                sw.WriteLine("Summary: " + action_type.ToString() +", "+ action+", "+performed_by + ", "+ DateTime.Now);
               

            }
        }
    }
}
