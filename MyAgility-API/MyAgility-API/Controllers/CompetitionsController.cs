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
    public class CompetitionsController : ApiController
    {
        private myagilityEntities2 db = new myagilityEntities2();

        // GET: api/Competitions
        public IQueryable<Competition> GetCompetition()
        {
            return db.Competition;
        }

        // GET: api/Competitions/5
        [ResponseType(typeof(Competition))]
        public IHttpActionResult GetCompetition(int id)
        {
            Competition competition = db.Competition.Find(id);
            if (competition == null)
            {
                return NotFound();
            }

            return Ok(competition);
        }

        // PUT: api/Competitions/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCompetition(int id, Competition competition)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != competition.ID)
            {
                return BadRequest();
            }

            db.Entry(competition).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompetitionExists(id))
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

        // POST: api/Competitions
        [ResponseType(typeof(Competition))]
        public IHttpActionResult PostCompetition(Competition competition)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Competition.Add(competition);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = competition.ID }, competition);
        }

        // DELETE: api/Competitions/5
        [ResponseType(typeof(Competition))]
        public IHttpActionResult DeleteCompetition(int id)
        {
            Competition competition = db.Competition.Find(id);
            if (competition == null)
            {
                return NotFound();
            }

            db.Competition.Remove(competition);
            db.SaveChanges();

            return Ok(competition);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CompetitionExists(int id)
        {
            return db.Competition.Count(e => e.ID == id) > 0;
        }
    }
}