using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using project2.Models;

namespace project2.Data
{
    public class project2Context : DbContext
    {
        public project2Context (DbContextOptions<project2Context> options)
            : base(options)
        {
        }

        public DbSet<project2.Models.accounts> accounts { get; set; } = default!;

        public DbSet<project2.Models.article> article { get; set; }

        public DbSet<project2.Models.report> report { get; set; }
    }
}
