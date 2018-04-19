using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DvdLibrary.Models.Queries
{
    public class DvdSearchParameters
    {
        public int DvdId { get; set; }
        public string Title { get; set; }
        public string RealeaseYear { get; set; }
        public string Director { get; set; }
        public string Rating { get; set; }
    }
}
