using DolphinContext.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ApplicationLogic
{
    public class Brand
    {
        private DolServiceDb context = DolServiceDb.GetInstance();
        public List<DolBrand> getBrandById()
        {
            var actual = context.Fetch<DolBrand>().ToList();
            return actual;
        }
    }
}
