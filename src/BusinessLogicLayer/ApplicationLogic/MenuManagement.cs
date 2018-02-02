using DolphinContext.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BLL.ApplicationLogic
{
   
    public class MenuManagement
    {
        private DolServiceDb context = DolServiceDb.GetInstance();
      

        public List<DolMenu> getMenuByRoleId(decimal RoleId)
        {
            string SQL = "select DolMenu.MenuId,DolMenu.MenuName,DolMenu.MenuURL,DolMenu.LinkIcon,DolMenu.ExternalUrl, DolRole_Menu.MenuId,DolMenu.ParentId,RoleId from DolMenu INNER JOIN DolRole_Menu ON DolMenu.MenuId=DolRole_Menu.MenuId WHERE DolRole_Menu.RoleId=" + RoleId;
            var actual = context.Fetch<DolMenu>(SQL).ToList();
            return (actual);
        }

       
        public List<DolMenu> getMenuByMenuId()
        {
            var actual = context.Fetch<DolMenu>().ToList();
            return actual;
        }

        public DolMenu getMenuByName(string MenuName)
        {
            string SQL = "Select * from DolMenu where MenuName =@0";
            var actual = context.FirstOrDefault<DolMenu>(SQL, MenuName);
            return actual;
        }

        public List<DolMenu> getMenuByUsername(string Username)
        {
            try
            {
                string SQL = "select A.* from DolMenu A inner join DolRole_Menu B on A.MenuId = B.MenuId inner join DolUser c on c.RoleId = B.RoleId where c.UserName =@0";
                var actual = context.Fetch<DolMenu>(SQL, Username).ToList();
                return actual;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<DolMenu> getMenuById()
        {
            var actual = context.Fetch<DolMenu>().ToList();
            return actual;
        }

        public bool InsertMenu(DolMenu MenuName)
        {
            try
            {
                context.Insert(MenuName);
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
            
        }
    }
}