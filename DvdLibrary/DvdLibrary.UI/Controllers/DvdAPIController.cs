using DvdLibrary.Data.Factories;
using DvdLibrary.Models.Queries;
using DvdLibrary.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace DvdLibrary.UI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class DvdAPIController : ApiController
    {
        
        [Route("dvds/")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetAll()
        {
            var repo = DvdRepositoryFactory.GetRepository();

            try
            {
                
                var result = repo.GetAll();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [Route("dvd/{dvdId}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetById(int dvdId)
        {
            var repo = DvdRepositoryFactory.GetRepository();

            try
            {

                var result = repo.GetById(dvdId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("dvd")]
        [AcceptVerbs("POST")]
        public IHttpActionResult Add(DvdDetails dvd)
        {
            var repo = DvdRepositoryFactory.GetRepository();

            try
            {

                repo.Insert(dvd);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("dvd/{dvdId}")]
        [AcceptVerbs("PUT")]
        public IHttpActionResult Update(int dvdId, DvdDetails dvd)
        {
            var repo = DvdRepositoryFactory.GetRepository();

            try
            {

                repo.Update(dvd);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [Route("dvd/{dvdId}")]
        [AcceptVerbs("DELETE")]
        public IHttpActionResult RemoveDvd(int dvdId)
        {
            var repo = DvdRepositoryFactory.GetRepository();

            try
            {

                repo.Delete(dvdId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [Route("dvds/year/{realeaseYear}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetDvdByRealeaseYear(string realeaseYear)
        {
            var repo = DvdRepositoryFactory.GetRepository();

            try
            {

                var result = repo.DvdsSearchByRealeaseYear(realeaseYear);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("dvds/title/{title}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetDvdByTitle(string title)
        {
            var repo = DvdRepositoryFactory.GetRepository();

            try
            {

                var result = repo.DvdsSearchByTitle(title);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("dvds/director/{director}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetDvdByDirector(string director)
        {
            var repo = DvdRepositoryFactory.GetRepository();

            try
            {

                var result = repo.DvdsSearchByDirector(director);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("dvds/rating/{rating}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetDvdByRating(string rating)
        {
            var repo = DvdRepositoryFactory.GetRepository();

            try
            {

                var result = repo.DvdsSearchByRating(rating);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}