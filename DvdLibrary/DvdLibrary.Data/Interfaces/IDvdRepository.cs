using DvdLibrary.Models.Queries;
using DvdLibrary.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DvdLibrary.Data.Interfaces
{
    public interface IDvdRepository
    {
        List<DvdShortItem> GetAll();
        DvdItem GetById(int dvdId);
        void Insert(DvdDetails dvd);
        void Update(DvdDetails dvd);
        void Delete(int dvdId);

        List<DvdShortItem> DvdsSearchByDirector(string director);
        List<DvdShortItem> DvdsSearchByRating(string rating);
        List<DvdShortItem> DvdsSearchByTitle(string title);
        List<DvdShortItem> DvdsSearchByRealeaseYear(string realeaseYear);


    }
}
