using MyFirstMVC.Data;
using MyFirstMVC.Models;
using System;
using System.Collections.Generic;
using System.IO;
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
        public ActionResult Create(Project project,HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                
                using(var db =new ApplicationDbContext())
                { //dosyayı upload etmeyi dene
                    try
                    {
                      //yüklenen dosyanın adını entity'deki alana ata.
                      project.Photo = UploadFile(upload);
                    } catch(Exception ex)
                    {
                        //upload sırasında hata oluşursa view da görüntüülemek üzere hatayı modelstate ekle
                        ViewBag.Error = ex.Message;
                        //hata oluştuğu için projeyi veritabanına eklemek yerine view'ı tekrar göster ve metottan çık
                        ViewBag.Categories = new SelectList(db.Categories.ToList(), "Id", "Name");
                        return View(project);
                    }
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

        public string UploadFile(HttpPostedFileBase upload)
        {
            //yüklenmek istenen dosya var mı?
            if (upload != null && upload.ContentLength > 0)
            {
                //dosyanın uzantısını kontrol et
                var extension = Path.GetExtension(upload.FileName).ToLower();
                if (extension == ".jpg" || extension == ".jpeg" || extension == ".gif" || extension == ".png")
                {
                    //uzantı doğuysa dosyanın yükleneceği uploads dizini var mı kontrol et
                    if (Directory.Exists(Server.MapPath("~/Uploads")))
                     {
                     //dosya adındaki geçersiz karakterleri düzelt
                        string fileName = upload.FileName.ToLower();
                        fileName = fileName.Replace("İ", "i");
                        fileName = fileName.Replace("Ş", "s");
                        fileName = fileName.Replace("Ğ", "g");
                        fileName = fileName.Replace("ğ", "g");
                        fileName = fileName.Replace("ı", "i");
                        fileName = fileName.Replace("(", "");
                        fileName = fileName.Replace(")", "");
                        fileName = fileName.Replace(" ", "-");
                        fileName = fileName.Replace(",", "");
                        fileName = fileName.Replace("ö", "o");
                        fileName = fileName.Replace("ü", "u");
                        fileName = fileName.Replace("`", "");
                        //aynı isimde dosya olabilir diye dosya adının önüne zaman pulu ekliyoruz.
                        fileName = DateTime.Now.Ticks.ToString() + fileName;
                        //dosyayı Uploads dizinine yükle 
                        upload.SaveAs(Path.Combine(Server.MapPath("~/Uploads"), fileName));
                        //yüklenen dosyanın adını geri döndür.
                        return fileName;
                    } else
                    {
                        throw new Exception("Upload dizini mevcut değil");
                    }
                }
                else
                {
                    throw new Exception("Dosya uzantısı .jpg,.gif, ya da .png olmalıdır.");
                }
            }
            return null;
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
        public ActionResult Edit(Project project, HttpPostedFileBase upload, string deletePhoto)
        {
            if (ModelState.IsValid)
            {
                using(var db=new ApplicationDbContext())
                {
                    try
                    {
                        //yüklenen dosyanın adını entity'deki alana ata.
                        project.Photo = UploadFile(upload);
                    }
                    catch (Exception ex)
                    {
                        //upload sırasında hata oluşursa view da görüntüülemek üzere hatayı modelstate ekle
                        ViewBag.Error = ex.Message;
                        //hata oluştuğu için projeyi veritabanına eklemek yerine view'ı tekrar göster ve metottan çık
                        ViewBag.Categories = new SelectList(db.Categories.ToList(), "Id", "Name");
                        return View(project);
                    }
                    var oldproject = db.Projects.Where(x => x.Id == project.Id).FirstOrDefault();
                    if (oldproject != null)
                    {
                        oldproject.Title = project.Title;
                        oldproject.Description = project.Description;
                        oldproject.Body = project.Body;
                        if (!string.IsNullOrEmpty(deletePhoto))
                        {
                            oldproject.Photo = null;
                        }
                        if (!string.IsNullOrEmpty(project.Photo))
                        {
                            oldproject.Photo = project.Photo;
                        }
                        
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