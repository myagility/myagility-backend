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
using MyAgility_API.Models.myagility;

namespace MyAgility_API.Controllers
{
    public class LapsController : ApiController
    {
        private myagilityEntities2 db = new myagilityEntities2();

        // GET: api/Laps
        public IQueryable<Lap> GetLap()
        {
            return db.Lap;
        }

        // GET: api/Laps/5
        [ResponseType(typeof(Lap))]
        public IHttpActionResult GetLap(int id)
        {
            Lap lap = db.Lap.Find(id);
            if (lap == null)
            {
                return NotFound();
            }

            return Ok(lap);
        }

        // PUT: api/Laps/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutLap(int id, Lap lap)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != lap.ID)
            {
                return BadRequest();
            }

            db.Entry(lap).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LapExists(id))
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

        // POST: api/Laps
        [ResponseType(typeof(Lap))]
        public IHttpActionResult PostLap(Lap lap)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Lap.Add(lap);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = lap.ID }, lap);
        }

        // DELETE: api/Laps/5
        [ResponseType(typeof(Lap))]
        public IHttpActionResult DeleteLap(int id)
        {
            Lap lap = db.Lap.Find(id);
            if (lap == null)
            {
                return NotFound();
            }

            db.Lap.Remove(lap);
            db.SaveChanges();

            return Ok(lap);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LapExists(int id)
        {
            return db.Lap.Count(e => e.ID == id) > 0;
        }
    }
}