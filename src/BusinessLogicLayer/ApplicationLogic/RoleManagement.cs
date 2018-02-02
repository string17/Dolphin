using DolphinContext.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BLL.ApplicationLogic
{
    public class RoleManagement
    {
        private DolServiceDb context = DolServiceDb.GetInstance();
        public List<DolRole> getAllRoles()
        {
            var actual = context.Fetch<DolRole>();
            return actual;
        }

        public DolRole getRoleById(decimal Id)
        {
            var actual = context.SingleById<DolRole>(Id);
            return actual;
        }

        public DolRole getRoleByRoleName(string RoleName)
        {
            string SQL = "Select * from DolRole where RoleName =@0";
            var actual = context.FirstOrDefault<DolRole>(SQL, RoleName);
            return actual;
        }
        public List<DolRole> getRoleByRoleId()
        {
            var actual = context.Fetch<DolRole>().ToList();
            return actual;
        }

        public List<DolRoleMenu> getRoleMenuByRoleId(decimal Id)
        {
            var actual = context.Fetch<DolRoleMenu>("Where ID = " + Id);
            return actual;
        }

        public bool InsertRole(DolRole RoleName)
        {
            try
            {
                context.Insert(RoleName);
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public bool UpdateRole(string RoleName,string RoleDesc,bool RoleStatus,int RoleId)
        {
            try
            {
                var roles = context.SingleOrDefault<DolRole>("WHERE RoleId=@0", RoleId);
                roles.RoleName = RoleName;
                roles.RoleDesc = RoleDesc;
                roles.RoleStatus = RoleStatus;
                context.Update(roles);
                return true;

            }
            catch(Exception ex)
            {
                return false;
            }
        }
    }
}