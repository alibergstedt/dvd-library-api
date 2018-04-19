using DvdLibrary.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DvdLibrary.Models.Queries;
using DvdLibrary.Models.Tables;

namespace DvdLibrary.Data.SampleData
{
    public class DvdRepositoryMock : IDvdRepository
    {
        private static List<DvdItem> _dvds;
        private static List<DvdDetails> _dvdDetails;
        private static List<DvdShortItem> _dvdShortItem;

        static DvdRepositoryMock()
        {
            _dvds = new List<DvdItem>()
            {
                new DvdItem {DvdId=0,Title="A Great Tale",RealeaseYear="2015",Director="Jones",Rating="PG",Notes="This is such a great tale!"},
                new DvdItem {DvdId=0,Title="A Good Tale",RealeaseYear="2012",Director="Smith",Rating="PG-13",Notes=null},
                new DvdItem {DvdId=0,Title="An OK Tale",RealeaseYear="2009",Director="Bryan",Rating="G",Notes="Ehhh. It was ok."},
                new DvdItem {DvdId=0,Title="A Riveting Tale",RealeaseYear="2014",Director="Simpson",Rating="R",Notes="Exceptional and riveting to the core."},
                new DvdItem {DvdId=0,Title="A Rotten Tale",RealeaseYear="2005",Director="Jones",Rating="PG-13",Notes=null},
                new DvdItem {DvdId=0,Title="A Mock Boring Tale",RealeaseYear="2011",Director="Baker",Rating="G",Notes="Underwhelming."},
                new DvdItem {DvdId=0,Title="A Mock Bad Tale",RealeaseYear="2010",Director="Smith",Rating="PG",Notes="So, so bad."},
                new DvdItem {DvdId=0,Title="A Scary Tale",RealeaseYear="2012",Director="Jones",Rating="G",Notes=null},
            };

            _dvdDetails = new List<DvdDetails>()
            {
                new DvdDetails {DvdId=0,Title="A Great Tale",RealeaseYear=2015,Director="Jones",Rating="PG",Notes="This is such a great tale!"},
                new DvdDetails {DvdId=0,Title="A Good Tale",RealeaseYear=2012,Director="Smith",Rating="PG-13",Notes=null},
                new DvdDetails {DvdId=0,Title="An OK Tale",RealeaseYear=2009,Director="Bryan",Rating="G",Notes="Ehhh. It was ok."},
                new DvdDetails {DvdId=0,Title="A Riveting Tale",RealeaseYear=2014,Director="Simpson",Rating="R",Notes="Exceptional and riveting to the core."},
                new DvdDetails {DvdId=0,Title="A Rotten Tale",RealeaseYear=2005,Director="Jones",Rating="PG-13",Notes=null},
                new DvdDetails {DvdId=0,Title="A Mock Boring Tale",RealeaseYear=2011,Director="Baker",Rating="G",Notes="Underwhelming."},
                new DvdDetails {DvdId=0,Title="A Mock Bad Tale",RealeaseYear=2010,Director="Smith",Rating="PG",Notes="So, so bad."},
                new DvdDetails {DvdId=0,Title="A Scary Tale",RealeaseYear=2012,Director="Jones",Rating="G",Notes=null},
            };

            _dvdShortItem = new List<DvdShortItem>()
            {
                new DvdShortItem {DvdId=0,Title="A Great Tale",RealeaseYear="2015",Director="Jones",Rating="PG"},
                new DvdShortItem {DvdId=0,Title="A Good Tale",RealeaseYear="2012",Director="Smith",Rating="PG-13"},
                new DvdShortItem {DvdId=0,Title="An OK Tale",RealeaseYear="2009",Director="Bryan",Rating="G"},
                new DvdShortItem {DvdId=0,Title="A Riveting Tale",RealeaseYear="2014",Director="Simpson",Rating="R"},
                new DvdShortItem {DvdId=0,Title="A Rotten Tale",RealeaseYear="2005",Director="Jones",Rating="PG-13"},
                new DvdShortItem {DvdId=0,Title="A Mock Boring Tale",RealeaseYear="2011",Director="Baker",Rating="G"},
                new DvdShortItem {DvdId=0,Title="A Mock Bad Tale",RealeaseYear="2010",Director="Smith",Rating="PG"},
                new DvdShortItem {DvdId=0,Title="A Scary Tale",RealeaseYear="2012",Director="Jones",Rating="G"},
            };
        }

        public void Delete(int dvdId)
        {
            _dvdDetails.RemoveAll(d => d.DvdId == dvdId);
        }

        public List<DvdShortItem> DvdsSearchByDirector(string director)
        {
            return _dvdShortItem.Where(s => s.Director.Contains(director)).ToList();
        }

        public List<DvdShortItem> DvdsSearchByRating(string rating)
        {
            return _dvdShortItem.Where(s => s.Rating.Contains(rating)).ToList();
        }

        public List<DvdShortItem> DvdsSearchByRealeaseYear(string realeaseYear)
        {
            return _dvdShortItem.Where(s => s.RealeaseYear.Contains(realeaseYear)).ToList();
        }

        public List<DvdShortItem> DvdsSearchByTitle(string title)
        {
            return _dvdShortItem.Where(s => s.Title.Contains(title)).ToList();
        }

        public List<DvdShortItem> GetAll()
        {
            return _dvdShortItem;
        }

        public DvdItem GetById(int dvdId)
        {
            return _dvds.FirstOrDefault(d => d.DvdId == dvdId);
        }

        public void Insert(DvdDetails dvd)
        {
            if (_dvdDetails.Any())
            {
                dvd.DvdId = _dvdDetails.Max(d => d.DvdId) + 1;
            }
            else
            {
                dvd.DvdId = 0;
            }

            _dvdDetails.Add(dvd);
        }

        public void Update(DvdDetails dvd)
        {
            _dvdDetails.RemoveAll(d => d.DvdId == dvd.DvdId);
            _dvdDetails.Add(dvd);
        }
    }
}
