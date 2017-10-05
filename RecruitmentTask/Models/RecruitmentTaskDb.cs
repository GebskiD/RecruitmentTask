using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RecruitmentTask.Models
{
    public class RecruitmentTaskDb : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
    }
}