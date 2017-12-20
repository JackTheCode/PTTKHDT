using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DTO;
using Infrastructure.Data;

namespace Infrastructure.Data.Repository
{
    public class NgachRepository
    {
         protected readonly SalaryContext context;
         public NgachRepository(SalaryContext context)
        {
            this.context = context;
        }

        public List<ngach> getNgachList()
        {
            return context.ngach.ToList();
        }
    }
}
