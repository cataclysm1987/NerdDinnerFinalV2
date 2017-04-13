using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using NerdDinnerVersion2.Models;
using PagedList;

namespace NerdDinnerVersion2.Controllers
{
    public class HomeController : Controller
    {
        private NerdDinners db = new NerdDinners();
        // GET: Home
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var dinners = from s in db.Dinners
                          select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                dinners = dinners.Where(s => s.Title.Contains(searchString)
                                       || s.HostedBy.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    dinners = dinners.OrderByDescending(s => s.Title);
                    break;
                case "Date":
                    dinners = dinners.OrderBy(s => s.EventDate);
                    break;
                case "date_desc":
                    dinners = dinners.OrderByDescending(s => s.EventDate);
                    break;
                default:  // Name ascending 
                    dinners = dinners.OrderBy(s => s.Title);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(dinners.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Create()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "DinnerId,Title,EventDate,Address,HostedBy")] Dinner dinner)
        {
            if (ModelState.IsValid)
            {
                db.Dinners.Add(dinner);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(dinner);
        }

        public ActionResult RSVP(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dinner dinner = db.Dinners.Find(id);
            if (dinner == null)
            {
                return HttpNotFound();
            }
            RSVP rsvp = new RSVP();
            rsvp.DinnerId = dinner.DinnerId;
            return View(rsvp);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RSVP([Bind(Include = "RsvpId,DinnerId,AttendeeEmail")] RSVP rsvp)
        {
            if (ModelState.IsValid)
            {
                db.RSVPs.Add(rsvp);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(rsvp);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dinner dinner = db.Dinners.Find(id);
            if (dinner == null)
            {
                return HttpNotFound();
            }
            DinnerDetailsViewModel dm = new DinnerDetailsViewModel();
            var rsvps =  db.RSVPs.Where(u => u.DinnerId == id).OrderBy(u=>u.DinnerId).ToPagedList(1, 10);
            dm.RSVPs = (PagedList<RSVP>) rsvps;
            dm.RSVPCount = dm.RSVPs.Count();
            dm.Dinner = db.Dinners.FirstOrDefault(u => u.DinnerId == id);
            return View(dm);
        }
    }
}