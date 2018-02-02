using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DolphinContext.Data.Models;

namespace BLL.ApplicationLogic
{
    public class AuditService : BaseService
    {
       private DolServiceDb context = DolServiceDb.GetInstance();
        public void insertAudit(AuditTrail newuser)
        {
            context.Insert<AuditTrail>(newuser);
        }

        public List<AuditTrail> getAuditById()
        {
            var actual = context.Fetch<AuditTrail>().ToList();
            return actual;
        }

    }
}
