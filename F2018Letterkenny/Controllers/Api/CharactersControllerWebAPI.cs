using F2018Letterkenny.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace F2018Letterkenny.Controllers.Api
{
    public class CharactersControllerWebAPI : ApiController
    {
        private DatabaseModel db = new DatabaseModel();

        // GET: api/characters
        public IQueryable<Character> Getcharacters()
        {
            return db.Characters;
        }

        // GET: api/Characters/5
        [ResponseType(typeof(Character))]
        public IHttpActionResult GetCharacter(int id)
        {
            Character character = db.Characters.Find(id);
            if (character == null)
            {
                return NotFound();
            }

            return Ok(character);
        }

        // PUT: api/Characters/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCharacter(int id, Character character)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != character.CharacterId)
            {
                return BadRequest();
            }

            db.Entry(character).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CharacterExists(id))
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

        // POST: api/Characters
        [ResponseType(typeof(Character))]
        public IHttpActionResult PostCharacter(Character character)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Characters.Add(character);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = character.CharacterId }, character);
        }

        // DELETE: api/Characters/5
        [ResponseType(typeof(Character))]
        public IHttpActionResult DeleteCharacter(int id)
        {
            Character Character = db.Characters.Find(id);
            if (Character == null)
            {
                return NotFound();
            }

            db.Characters.Remove(Character);
            db.SaveChanges();

            return Ok(Character);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CharacterExists(int id)
        {
            return db.Characters.Count(e => e.CharacterId == id) > 0;
        }

    }
}
