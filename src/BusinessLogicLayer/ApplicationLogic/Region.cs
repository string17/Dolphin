using DolphinContext.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BLL.ApplicationLogic
{
    public class Region
    {
        private DolServiceDb context = DolServiceDb.GetInstance();

        public List<DolRegion> getRegionById()
        {
            var actual = context.Fetch<DolRegion>().ToList();
            return actual;
        }


        public DolRegion getRegionId(int? Id)
        {
            string sql = "Select * from DolRegion where RegId =@0";
            var actual = context.FirstOrDefault<DolRegion>(sql, Id);
            return actual;

        }

        public DolRegion getRegionByName(string RegionName)
        {
            string SQL = "Select * from DolRegion where RegionName =@0";
            var actual = context.FirstOrDefault<DolRegion>(SQL, RegionName);
            return actual;
        }

    }
}