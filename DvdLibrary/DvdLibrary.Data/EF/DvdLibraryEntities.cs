using DvdLibrary.Models.Tables;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DvdLibrary.Data.EF
{
    public class DvdLibraryEntities : DbContext
    {
        public DvdLibraryEntities()
            : base("DvdLibrary")
        {

        }

        public DbSet<DvdDetails> DvdDetails { get; set; }
    }
}
