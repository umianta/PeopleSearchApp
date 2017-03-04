using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PeopleSearchApp.DataContext;
using PeopleSearchApp.Models;
using PeopleSearchApp.Repository;
namespace PeopleSearchApp.Controllers
{
    public class UserController : Controller
    {
        private IRepository<User> _user = null;
        public UserController()
        {
            this._user = new Repository<User>();
        }

        // GET: User
        public ActionResult PeopleList()
        {
            return View(_user.GetAll());
        }

        public ActionResult UserSearchScreen()
        {
            return View();
        }
        public JsonResult UserSearch(string searchString)
        {
            var users = from u in _user.GetAll() select u;
            if (!String.IsNullOrEmpty(searchString))
            {
                users = users.Where(s => s.LastName.ToUpper().Contains(searchString.ToUpper())
                                       || s.FirstName.ToUpper().Contains(searchString.ToUpper())).OrderBy(s=>s.LastName);
            }
            System.Threading.Thread.Sleep(5000);
            return Json(users, JsonRequestBehavior.AllowGet);
        }

        // GET: User/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = _user.GetById(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,Address,Interest,Age,PicPath")] User user)
        {
            if (ModelState.IsValid)
            {
                _user.Insert(user);
                _user.Save();
                return RedirectToAction("PeopleList");
            }

            return View(user);
        }

        // GET: User/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = _user.GetById(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: User/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastNameAddress,Interest,Age,PicPath")] User user)
        {
            if (ModelState.IsValid)
            {
                _user.Update(user);
                _user.Save();
                return RedirectToAction("PeopleList");
            }
            return View(user);
        }

        // GET: User/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = _user.GetById(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //User user = _user.GetById(id);
           _user.Delete(id);
           _user.Save();
            return RedirectToAction("PeopleList");
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        _user.di();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
