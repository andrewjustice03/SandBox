using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Common;

namespace HRSystem_Core.Data
{
    public class HRSystem_CoreContext : DbContext
    {
        public HRSystem_CoreContext (DbContextOptions<HRSystem_CoreContext> options)
            : base(options)
        {
        }

        public DbSet<Common.EmployeeModel> EmployeeModel { get; set; }
    }
}
