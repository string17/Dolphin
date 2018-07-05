using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;

namespace DolphinServices.ApplicationLogic
{
    public class UploadAttachment
    {
        public string UploadBanner(HttpPostedFileBase pic, string filename = "")
        {
            string path = WebConfigurationManager.AppSettings["Banner"].ToString();
            if (pic == null && string.IsNullOrWhiteSpace(filename))
            {
                return "";
            }
            if (!string.IsNullOrWhiteSpace(filename) && pic == null) return filename;

            string result = DateTime.Now.Millisecond + "Banner.jpg";
            pic.SaveAs(HttpContext.Current.Server.MapPath("~/assets/Banner/") + result);

            return result;
        }


        public string UploadImage(HttpPostedFileBase pic, string filename = "")
        {
            if (pic == null && string.IsNullOrWhiteSpace(filename))
            {
                return "";
            }
            if (!string.IsNullOrWhiteSpace(filename) && pic == null) return filename;

            string result = DateTime.Now.Millisecond + "User.jpg";
            pic.SaveAs(HttpContext.Current.Server.MapPath("~/assets/UserImg/") + result);

            return result;
        }


        public string UploadTerminal(HttpPostedFileBase pic, string filename = "")
        {
            if (pic == null && string.IsNullOrWhiteSpace(filename))
            {
                return "";
            }
            if (!string.IsNullOrWhiteSpace(filename) && pic == null) return filename;

            string result = DateTime.Now.Millisecond + "Terminal.xlxs";
            pic.SaveAs(HttpContext.Current.Server.MapPath("~/assets/Terminal/") + result);

            return result;
        }
    }
}
