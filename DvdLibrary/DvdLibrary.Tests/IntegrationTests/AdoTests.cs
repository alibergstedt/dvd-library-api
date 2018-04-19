using DvdLibrary.Data.ADO;
using DvdLibrary.Models.Queries;
using DvdLibrary.Models.Tables;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DvdLibrary.Tests.IntegrationTests
{
    [TestFixture]
    public class AdoTests
    {
        [SetUp]
        public void Init()
        {
            using (var cn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                var cmd = new SqlCommand();
                cmd.CommandText = "DbReset";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Connection = cn;
                cn.Open();

                cmd.ExecuteNonQuery();
            }
        }

        [Test]
        public void CanLoadDvds()
        {
            var repo = new DvdRepositoryADO();

            var dvds = repo.GetAll();

            Assert.AreEqual(8, dvds.Count);
            Assert.AreEqual("Jones", dvds[0].Director);
            Assert.AreEqual("2014", dvds[3].RealeaseYear);
        }

        [Test]
        public void CanLoadDvd()
        {
            var repo = new DvdRepositoryADO();
            var dvd = repo.GetById(0);

            Assert.IsNotNull(dvd);

            Assert.AreEqual(0, dvd.DvdId);
            Assert.AreEqual("A Great Tale", dvd.Title);
            Assert.AreEqual("2015", dvd.RealeaseYear);
            Assert.AreEqual("Jones", dvd.Director);
            Assert.AreEqual("PG", dvd.Rating);
            Assert.AreEqual("This is such a great tale!", dvd.Notes);
                        
        }

        [Test]
        public void NotFoundDvdReturnsNull()
        {
            var repo = new DvdRepositoryADO();
            var dvd = repo.GetById(100);

            Assert.IsNull(dvd);
        }


        [Test]
        public void CanAddDvd()
        {
            DvdDetails dvdToAdd = new DvdDetails();
            var repo = new DvdRepositoryADO();

            dvdToAdd.Title = "A Test Tale";
            dvdToAdd.RealeaseYear = 2018;
            dvdToAdd.Director = "Bergstedt";
            dvdToAdd.Rating = "PG";
            dvdToAdd.Notes = "This is to test if we can add a dvd";

            repo.Insert(dvdToAdd);

            Assert.AreEqual(8, dvdToAdd.DvdId);
        }


        [Test]
        public void CanUpdateDvd()
        {
            DvdDetails dvdToAdd = new DvdDetails();
            var repo = new DvdRepositoryADO();

            dvdToAdd.Title = "A Test Tale";
            dvdToAdd.RealeaseYear = 2018;
            dvdToAdd.Director = "Bergstedt";
            dvdToAdd.Rating = "PG";
            dvdToAdd.Notes = "This is to test if we can add a dvd";

            repo.Insert(dvdToAdd);

            dvdToAdd.Title = "An Updated Tale";
            dvdToAdd.RealeaseYear = 2018;
            dvdToAdd.Director = "Bergstedt2";
            dvdToAdd.Rating = "PG-13";
            dvdToAdd.Notes = "This is to see if we can update a dvd";

            repo.Update(dvdToAdd);

            var updatedDvd = repo.GetById(8);

            Assert.AreEqual("An Updated Tale", updatedDvd.Title);
            Assert.AreEqual("2018", updatedDvd.RealeaseYear);
            Assert.AreEqual("Bergstedt2", updatedDvd.Director);
            Assert.AreEqual("PG-13", updatedDvd.Rating);
            Assert.AreEqual("This is to see if we can update a dvd", updatedDvd.Notes);
        }

        [Test]
        public void CanDeleteListing()
        {
            DvdDetails dvdToAdd = new DvdDetails();
            var repo = new DvdRepositoryADO();

            dvdToAdd.Title = "A Test Tale";
            dvdToAdd.RealeaseYear = 2018;
            dvdToAdd.Director = "Bergstedt";
            dvdToAdd.Rating = "PG";
            dvdToAdd.Notes = "This is to test if we can add a dvd";

            repo.Insert(dvdToAdd);

            var loaded = repo.GetById(8);
            Assert.IsNotNull(loaded);

            repo.Delete(8);
            loaded = repo.GetById(8);

            Assert.IsNull(loaded);
        }

    }
}
