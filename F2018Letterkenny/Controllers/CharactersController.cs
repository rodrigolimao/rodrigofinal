using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using F2018Letterkenny.Models;

namespace F2018Letterkenny.Controllers
{
    [Authorize]
    public class CharactersController : Controller
    {
      
        private IMockCharacter db;

        public CharactersController()
        {
            this.db = new EFCharacter();
        }

        public CharactersController(IMockCharacter db)
        {
            this.db = db;
        }

        // GET: Characters
        public ActionResult Index()
        {
            return View(db.Characters.ToList());
        }

        // GET: Characters/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                return View("Error");
            }
            Character character = db.Characters.SingleOrDefault(c => c.CharacterId ==id);
            if (character == null)
            {
                return HttpNotFound();
            }
            return View(character);
        }

        // GET: Characters/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                return View("Error");
            }
            Character character = db.Characters.SingleOrDefault(c => c.CharacterId == id);
            if (character == null)
            {
                return HttpNotFound();
            }
            return View(character);
        }

        // POST: Characters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CharacterId,Name,Description,Photo,Quote")] Character character)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(character).State = EntityState.Modified;
                db.Save(character);
                return RedirectToAction("Index");
            }
            return View(character);
        }
    }
}
