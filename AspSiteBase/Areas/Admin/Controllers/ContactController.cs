using AspSiteBase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AspSiteBase.Areas.Admin.Controllers
{
    [Authorize]
    public class ContactController : Controller
    {
        private DatabaseContext DbCon = new DatabaseContext();

        // GET: Admin/Contact
        public ActionResult Index()
        {
            ViewBag.Message = "Your contact page.";

            var contacts = DbCon.Contacts;
            return View(contacts.ToList());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="")] Contactus contact)
        {
            if (ModelState.IsValid)
            {
                DbCon.Contacts.Add(contact);
                await DbCon.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(contact);
        }

        public async Task<ActionResult> Details (int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Contactus contactus = await DbCon.Contacts.FindAsync(id);

            if (contactus == null)
            {
                return HttpNotFound();
            }

            return View(contactus);
        }

        public async Task<ActionResult> Edit (int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Contactus contact = await DbCon.Contacts.FindAsync(id);
            if (contact == null)
            {
                return HttpNotFound();
            }

            return View(contact);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit ([Bind(Include="ContactusId,Name,Telephone,Created,Email,Msg,Ip")] Contactus contact)
        {
            //contact.Created = DateTime.Now;
            if (ModelState.IsValid)
            {
                DbCon.Entry(contact).State = System.Data.Entity.EntityState.Modified;
                await DbCon.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            
            return View(contact);
        }



        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Contactus contactus = await DbCon.Contacts.FindAsync(id);

            if (contactus == null)
            {
                return HttpNotFound();
            }
            return View(contactus);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Contactus contact = await DbCon.Contacts.FindAsync(id);
            DbCon.Contacts.Remove(contact);
            await DbCon.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if(disposing)
            {
                DbCon.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}