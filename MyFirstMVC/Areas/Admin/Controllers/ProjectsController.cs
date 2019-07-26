using MyFirstMVC.Data;
using MyFirstMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyFirstMVC.Areas.Admin.Controllers
{
    [RouteArea("Admin")]
    public class ProjectsController : Controller
    {
        // GET: Admin/Projects
        public ActionResult Index()
        {
            using(var db=new ApplicationDbContext())
            {
                var projects = db.Projects.Include("Category").ToList();
                return View(projects);
            }
            
        }

        public ActionResult Create()
        {
            var project = new Project();
            using(var db=new ApplicationDbContext())
            {
                ViewBag.Categories = new SelectList( db.Categories.ToList(),"Id","Name");
            }
            
            return View(project);
        }
        [HttpPost]
        [ValidateInput(false)] //bu actiona html/script etiketleri artık gönderilebilir.
        public ActionResult Create(Project project)
        {
            if (ModelState.IsValid)
            {
                using(var db =new ApplicationDbContext())
                {
                    db.Projects.Add(project);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            using (var db = new ApplicationDbContext())
            {
                ViewBag.Categories = new SelectList(db.Categories.ToList(), "Id", "Name");
            }

            return View(project);
        }

        public ActionResult Edit(int id)
        {
            using(var db= new ApplicationDbContext())
            {
                var project = db.Projects.Where(x => x.Id == id).FirstOrDefault();
                if (project != null)
                {
                   
                   ViewBag.Categories = new SelectList(db.Categories.ToList(), "Id", "Name");
                   
                    return View(project);
                }
                else
                {
                    return HttpNotFound();
                }
            }
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Project project)
        {
            if (ModelState.IsValid)
            {
                using(var db=new ApplicationDbContext())
                {
                    var oldproject = db.Projects.Where(x => x.Id == project.Id).FirstOrDefault();
                    if (oldproject != null)
                    {
                        oldproject.Title = project.Title;
                        oldproject.Description = project.Description;
                        oldproject.Body = project.Body;
                        oldproject.Photo = project.Photo;
                        oldproject.CategoryId = project.CategoryId;
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }
            }


            using (var db = new ApplicationDbContext())
            {
                ViewBag.Categories = new SelectList(db.Categories.ToList(), "Id", "Name");
            }


            return View(project);
        }
        public ActionResult Delete(int id)
        {
            using(var db= new ApplicationDbContext())
            {
                var project = db.Projects.Where(x => x.Id == id).FirstOrDefault();
                if (project != null)
                {
                    db.Projects.Remove(project);
                    db.SaveChanges();
                    return RedirectToAction("Index");

                } else
                {
                    return HttpNotFound();
                }

            }
        }

    }
}