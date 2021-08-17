using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using RosterService.Models;

namespace RosterService.Controllers
{
    public class tblServicesController : ApiController
    {
        private ServiceEntities1 db = new ServiceEntities1();

        // GET: api/tblServices
        [Route("api/GetServices")]
        public IQueryable<tblService> GettblServices()
        {
            return db.tblServices;
        }

        // GET: api/tblServices/5
        [ResponseType(typeof(tblService))]
        [Route("api/GetServiceByID")]
        public IHttpActionResult GettblService(int id)
        {
            tblService tblService = db.tblServices.Find(id);
            if (tblService == null)
            {
                return NotFound();
            }

            return Ok(tblService);
        }

        // PUT: api/tblServices/5
        [ResponseType(typeof(void))]
        [Route("api/EditService")]
        public IHttpActionResult PuttblService(int id, tblService tblService)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tblService.ID)
            {
                return BadRequest();
            }

            db.Entry(tblService).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblServiceExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/tblServices
        [ResponseType(typeof(tblService))]
        [Route("api/AddService")]
        public IHttpActionResult PosttblService(tblService tblService)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tblServices.Add(tblService);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tblService.ID }, tblService);
        }

        // DELETE: api/tblServices/5
        [ResponseType(typeof(tblService))]
        [Route("api/DeleteService")]
        public IHttpActionResult DeletetblService(int id)
        {
            tblService tblService = db.tblServices.Find(id);
            if (tblService == null)
            {
                return NotFound();
            }

            db.tblServices.Remove(tblService);
            db.SaveChanges();

            return Ok(tblService);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tblServiceExists(int id)
        {
            return db.tblServices.Count(e => e.ID == id) > 0;
        }
    }
}