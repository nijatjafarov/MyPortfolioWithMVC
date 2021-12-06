using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Portfolio.Models;
using System.Web.UI.WebControls;
using System.IO;

namespace Portfolio.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        NijatPortfolioEntities context = new NijatPortfolioEntities();
        // GET: Admin
        public ActionResult Index()
        {
            ViewBag.AboutMe = context.AboutMes.FirstOrDefault();
            ViewBag.InfoList = context.AdditionalInfoes.ToList<AdditionalInfo>();
            ViewBag.Technologies = context.Technologies.ToList<Technology>();
            ViewBag.Sources = context.Sources.ToList<Source>();
            ViewBag.Tasks = context.Tasks.ToList<Task>();
            ViewBag.Reviews = context.Reviews.ToList<Review>();
            ViewBag.Blogs = context.Blogs.ToList<Blog>();
            ViewBag.Projects = context.Projects.ToList<Project>();
            ViewBag.Photos = context.Photos.ToList<Photo>();
            ViewBag.Works = context.MyWorksOnProjects.ToList<MyWorksOnProject>();
            ViewBag.Contacts = context.Contacts.ToList<Contact>();
            ViewBag.UserMessages = context.UserMessages.ToList<UserMessage>();

            return View();
        }
        public ActionResult UpdateAboutMe()
        {
            AboutMe aboutMe = context.AboutMes.FirstOrDefault();
            ViewBag.Header = "Məlumatda düzəliş et";
            ViewBag.Button = "Düzəliş et";
            return View("AboutMe", aboutMe);
        }

        [HttpPost]
        public ActionResult UpdateAboutMe(AboutMe me)
        {
            if (ModelState.IsValid)
            {
                AboutMe aboutMe = context.AboutMes.FirstOrDefault();
                aboutMe.Header = me.Header;
                aboutMe.Content = me.Content;
                context.SaveChanges();

                return RedirectToAction("Index");
            }
            ViewBag.Header = "Məlumatda düzəliş et";
            ViewBag.Button = "Düzəliş et";

            return View("AboutMe");
        }
        public ActionResult AddInfo()
        {
            ViewBag.Header = "Məlumat əlavə et";
            ViewBag.Button = "Əlavə et";
            ViewBag.Placeholder = "Məlumat əlavə et";
            return View("Info");
        }
        [HttpPost]
        public ActionResult AddInfo(string info)
        {
            if (ModelState.IsValid)
            {
                context.AdditionalInfoes.Add(new AdditionalInfo() { Info = info });
                context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Header = "Məlumat əlavə et";
            ViewBag.Button = "Əlavə et";
            ViewBag.Placeholder = "Məlumat əlavə et";
            return View("Info");
        }
        public ActionResult UpdateInfo(int id)
        {
            AdditionalInfo info = context.AdditionalInfoes.Single(i => i.Id == id);
            ViewBag.Header = "Məlumatda düzəliş et";
            ViewBag.Button = "Düzəliş et";
            return View("Info", info);
        }

        [HttpPost]
        public ActionResult UpdateInfo(string info, int id)
        {
            if (ModelState.IsValid)
            {
                AdditionalInfo inf = context.AdditionalInfoes.Single(i => i.Id == id);
                inf.Info = info;
                context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Header = "Məlumatda düzəliş et";
            ViewBag.Button = "Düzəliş et";
            return View("Info");
        }

        [HttpPost]
        public ActionResult DeleteInfo(int id)
        {
            AdditionalInfo inf = context.AdditionalInfoes.Single(i => i.Id == id);
            context.AdditionalInfoes.Remove(inf);
            context.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult AddTech()
        {
            ViewBag.Header = "Texnologiya əlavə et";
            ViewBag.Button = "Əlavə et";

            return View("Technology");
        }
        [HttpPost]
        public ActionResult AddTech(Technology tech)
        {
            if (ModelState.IsValid)
            {
                context.Technologies.Add(tech);
                context.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.Header = "Texnologiya əlavə et";
            ViewBag.Button = "Əlavə et";
            return View("Technology");
        }
        public ActionResult UpdateTech(int id)
        {
            Technology tech = context.Technologies.Single(i => i.Id == id);
            ViewBag.Header = "Texnologiyada düzəliş et";
            ViewBag.Button = "Düzəliş et";
            return View("Technology", tech);
        }
        [HttpPost]
        public ActionResult UpdateTech(Technology tech)
        {
            if (ModelState.IsValid)
            {
                Technology technology = context.Technologies.Single(i => i.Id == tech.Id);
                technology.Name = tech.Name;
                technology.StartDate = tech.StartDate;
                technology.FinishDate = tech.FinishDate;
                technology.UsageInfo = tech.UsageInfo;
                technology.ParentTechnology = tech.ParentTechnology;
                context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Header = "Texnologiyada düzəliş et";
            ViewBag.Button = "Düzəliş et";
            return View("Technology");
        }
        [HttpPost]
        public ActionResult DeleteTech(int id)
        {
            Technology tech = context.Technologies.Single(i => i.Id == id);
            context.Tasks.RemoveRange(tech.Tasks);
            context.Technologies.Remove(tech);
            context.SaveChanges();

            return RedirectToAction("Index");
        }
        public ActionResult AddSource()
        {
            ViewBag.Header = "Mənbə əlavə et";
            ViewBag.Button = "Əlavə et";
            ViewBag.Technologies = context.Technologies.ToList<Technology>();

            return View("Source");
        }
        [HttpPost]
        public ActionResult AddSource(Source source)
        {
            if (ModelState.IsValid)
            {
                context.Sources.Add(source);
                context.SaveChanges();

                return RedirectToAction("Index");
            }
            ViewBag.Header = "Mənbə əlavə et";
            ViewBag.Button = "Əlavə et";
            ViewBag.Technologies = context.Technologies.ToList<Technology>();

            return View("Source");
        }
        public ActionResult UpdateSource(int id)
        {
            Source source = context.Sources.Single(i => i.Id == id);
            ViewBag.Technologies = context.Technologies.ToList<Technology>();
            ViewBag.Header = "Mənbədə düzəliş et";
            ViewBag.Button = "Düzəliş et";
            return View("Source", source);
        }
        [HttpPost]
        public ActionResult UpdateSource(Source source)
        {
            if (ModelState.IsValid)
            {
                Source src = context.Sources.Single(i => i.Id == source.Id);
            src.Name = source.Name;
            src.TechnologyId = source.TechnologyId;
            context.SaveChanges();

            return RedirectToAction("Index");
            }
            ViewBag.Technologies = context.Technologies.ToList<Technology>();
            ViewBag.Header = "Mənbədə düzəliş et";
            ViewBag.Button = "Düzəliş et";
            return View("Source");
        }
        [HttpPost]
        public ActionResult DeleteSource(int id)
        {
            Source source = context.Sources.Single(i => i.Id == id);
            context.Sources.Remove(source);
            context.SaveChanges();

            return RedirectToAction("Index");
        }
        public ActionResult AddTask()
        {
            ViewBag.Header = "Texnologiya layihəsi əlavə et";
            ViewBag.Button = "Əlavə et";
            ViewBag.Technologies = context.Technologies.ToList<Technology>();

            return View("Task");
        }
        [HttpPost]
        public ActionResult AddTask(Task task)
        {
            if (ModelState.IsValid)
            {
                context.Tasks.Add(task);
                context.SaveChanges();

                return RedirectToAction("Index");
            }
            ViewBag.Header = "Texnologiya layihəsi əlavə et";
            ViewBag.Button = "Əlavə et";
            ViewBag.Technologies = context.Technologies.ToList<Technology>();

            return View("Task");
        }
        public ActionResult UpdateTask(int id)
        {
            Task task = context.Tasks.Single(i => i.Id == id);
            ViewBag.Technologies = context.Technologies.ToList<Technology>();
            ViewBag.Header = "Texnologiya layihəsində düzəliş et";
            ViewBag.Button = "Düzəliş et";
            return View("Task", task);
        }
        [HttpPost]
        public ActionResult UpdateTask(Task task)
        {
            if (ModelState.IsValid)
            {
                Task tsk = context.Tasks.Single(i => i.Id == task.Id);
                tsk.Name = task.Name;
                tsk.Link = task.Link;
                tsk.TechnologyId = task.TechnologyId;
                context.SaveChanges();

                return RedirectToAction("Index");
            }
            ViewBag.Technologies = context.Technologies.ToList<Technology>();
            ViewBag.Header = "Texnologiya layihəsində düzəliş et";
            ViewBag.Button = "Düzəliş et";
            return View("Task");
        }
        [HttpPost]
        public ActionResult DeleteTask(int id)
        {
            Task task = context.Tasks.Single(i => i.Id == id);
            context.Tasks.Remove(task);
            context.SaveChanges();

            return RedirectToAction("Index");
        }
        public ActionResult AddReview()
        {
            ViewBag.Header = "Rəy əlavə et";
            ViewBag.Button = "Əlavə et";

            return View("Review");
        }
        [HttpPost]
        public ActionResult AddReview(Review review)
        {
            if (ModelState.IsValid)
            {
                context.Reviews.Add(review);
                context.SaveChanges();

                return RedirectToAction("Index");
            }
            ViewBag.Header = "Rəy əlavə et";
            ViewBag.Button = "Əlavə et";

            return View("Review");
        }
        public ActionResult UpdateReview(int id)
        {
            Review review = context.Reviews.Single(i => i.Id == id);
            ViewBag.Header = "Rəydə düzəliş et";
            ViewBag.Button = "Düzəliş et";
            return View("Review", review);
        }
        [HttpPost]
        public ActionResult UpdateReview(Review review)
        {
            if (ModelState.IsValid)
            {
                Review rvw = context.Reviews.Single(i => i.Id == review.Id);
                rvw.Review1 = review.Review1;
                rvw.ReviewOwner = review.ReviewOwner;
                rvw.OwnerProfession = review.OwnerProfession;
                context.SaveChanges();

                return RedirectToAction("Index");
            }
            ViewBag.Header = "Rəydə düzəliş et";
            ViewBag.Button = "Düzəliş et";
            return View("Review", review);
        }
        [HttpPost]
        public ActionResult DeleteReview(int id)
        {
            Review review = context.Reviews.Single(i => i.Id == id);
            context.Reviews.Remove(review);
            context.SaveChanges();

            return RedirectToAction("Index");
        }
        public ActionResult AddBlog()
        {
            ViewBag.Header = "Məqalə əlavə et";
            ViewBag.Button = "Əlavə et";

            return View("Blog");
        }
        [HttpPost]
        public ActionResult AddBlog(Blog blog)
        {
            if (ModelState.IsValid)
            {
                context.Blogs.Add(blog);
                context.SaveChanges();

                return RedirectToAction("Index");
            }
            ViewBag.Header = "Məqalə əlavə et";
            ViewBag.Button = "Əlavə et";

            return View("Blog");
        }
        public ActionResult UpdateBlog(int id)
        {
            Blog blog = context.Blogs.Single(i => i.Id == id);
            ViewBag.Header = "Məqalədə düzəliş et";
            ViewBag.Button = "Düzəliş et";
            return View("Blog", blog);
        }
        [HttpPost]
        public ActionResult UpdateBlog(Blog blog)
        {
            if (ModelState.IsValid)
            {
                Blog blg = context.Blogs.Single(i => i.Id == blog.Id);
                blg.Header = blog.Header;
                blg.Content = blog.Content;
                blg.Link = blog.Link;
                blg.UploadDate = blog.UploadDate;
                context.SaveChanges();

                return RedirectToAction("Index");
            }
            ViewBag.Header = "Məqalədə düzəliş et";
            ViewBag.Button = "Düzəliş et";
            return View("Blog");
        }
        [HttpPost]
        public ActionResult DeleteBlog(int id)
        {
            Blog blog = context.Blogs.Single(i => i.Id == id);
            context.Blogs.Remove(blog);
            context.SaveChanges();

            return RedirectToAction("Index");
        }
        public ActionResult AddProject()
        {
            ViewBag.Header = "Layihə əlavə et";
            ViewBag.Button = "Əlavə et";

            return View("Project");
        }
        [HttpPost]
        public ActionResult AddProject(Project project)
        {
            if (ModelState.IsValid)
            {
                context.Projects.Add(project);
                context.SaveChanges();

                return RedirectToAction("Index");
            }
            ViewBag.Header = "Layihə əlavə et";
            ViewBag.Button = "Əlavə et";

            return View("Project");
        }
        public ActionResult UpdateProject(int id)
        {
            Project project = context.Projects.Single(i => i.Id == id);
            ViewBag.Header = "Layihədə düzəliş et";
            ViewBag.Button = "Düzəliş et";
            return View("Project", project);
        }
        [HttpPost]
        public ActionResult UpdateProject(Project project)
        {
            if (ModelState.IsValid)
            {
                Project prj = context.Projects.Single(i => i.Id == project.Id);
                prj.Name = project.Name;
                prj.About = project.About;
                prj.CodeURL = project.CodeURL;
                prj.RealUrl = project.RealUrl;
                prj.StartDate = project.StartDate;
                prj.FinishDate = project.FinishDate;
                context.SaveChanges();

                return RedirectToAction("Index");
            }
            ViewBag.Header = "Layihədə düzəliş et";
            ViewBag.Button = "Düzəliş et";
            return View("Project");
        }
        [HttpPost]
        public ActionResult DeleteProject(int id)
        {
            Project project = context.Projects.Single(i => i.Id == id);
            context.Projects.Remove(project);
            context.SaveChanges();

            return RedirectToAction("Index");
        }
        public ActionResult AddProjAndTech()
        {
            ViewBag.Header = "Layihəyə şəkil əlavə et";
            ViewBag.Button = "Əlavə et";
            ViewBag.Projects = context.Projects.ToList<Project>();
            ViewBag.Technologies = context.Technologies.ToList<Technology>();

            return View("ProjAndTech");
        }
        [HttpPost]
        public ActionResult AddProjAndTech(int ProjId, int TechId)
        {
            Technology tech = context.Technologies.Single(i => i.Id == TechId);
            Project project = context.Projects.Single(i => i.Id == ProjId);

            tech.Projects.Add(project);
            project.Technologies.Add(tech);
            context.SaveChanges();

            return RedirectToAction("Index");
        }
        public ActionResult UpdateProjAndTech(int ProjId, int TechId)
        {
            Technology tech = context.Technologies.Single(i => i.Id == TechId);
            Project project = context.Projects.Single(i => i.Id == ProjId);
            ViewBag.SelectedTech = tech;
            ViewBag.SelectedProject = project;

            context.SaveChanges();

            TempData["projId"] = ProjId;
            TempData["techId"] = TechId;

            ViewBag.Projects = context.Projects.ToList<Project>();
            ViewBag.Technologies = context.Technologies.ToList<Technology>();

            ViewBag.Header = "Layihə və texnologiya uyğunlaşdırmasında düzəliş et";
            ViewBag.Button = "Düzəliş et";
            return View("ProjAndTech");
        }
        [HttpPost]
        [ActionName("UpdateProjAndTech")]
        public ActionResult UpdateProjAndTech_post(int ProjId, int TechId)
        {
            int projId = Int32.Parse(TempData["projId"].ToString());
            int techId = Int32.Parse(TempData["techId"].ToString());

            Technology RemovedTech = context.Technologies.Single(i => 
            i.Id == techId);
            Project RemovedProject = context.Projects.Single(i => 
            i.Id == projId);

            RemovedTech.Projects.Remove(RemovedProject);
            RemovedProject.Technologies.Remove(RemovedTech);

            Technology AddedTech = context.Technologies.Single(i => i.Id == TechId);
            Project AddedProject = context.Projects.Single(i => i.Id == ProjId);

            AddedTech.Projects.Add(AddedProject);
            AddedProject.Technologies.Add(AddedTech);
            context.SaveChanges();

            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult DeleteProjAndTech(int ProjId, int TechId)
        {
            Technology tech = context.Technologies.Single(i => i.Id == TechId);
            Project project = context.Projects.Single(i => i.Id == ProjId);

            tech.Projects.Remove(project);
            project.Technologies.Remove(tech);
            context.SaveChanges();

            return RedirectToAction("Index");
        }
        public ActionResult AddPhoto()
        {
            ViewBag.Header = "Layihəyə şəkil əlavə et";
            ViewBag.Button = "Əlavə et";
            ViewBag.Projects = context.Projects.ToList<Project>();

            return View("Photo");
        }
        [HttpPost]
        public ActionResult AddPhoto(Photo photo, int projectId)
        {
            if (ModelState.IsValid)
            {
                string fileName = Path.GetFileNameWithoutExtension(photo.ImageFile.FileName);
                string extention = Path.GetExtension(photo.ImageFile.FileName);
                fileName += DateTime.Now.ToString("yymmssfff") + extention;
                photo.Name = fileName;
                Project project = context.Projects.Single(p => p.Id == projectId);
                project.Photos.Add(photo);
                fileName = Path.Combine(Server.MapPath("~/Images/"), fileName);
                photo.ImageFile.SaveAs(fileName);
                context.Photos.Add(photo);
                context.SaveChanges();

                return RedirectToAction("Index");
            }
            ViewBag.Header = "Layihəyə şəkil əlavə et";
            ViewBag.Button = "Əlavə et";
            ViewBag.Projects = context.Projects.ToList<Project>();

            return View("Photo");
        }
        [HttpPost]
        public ActionResult DeletePhoto(int id)
        {
            Photo photo = context.Photos.Single(i => i.Id == id);
            string fileName = Path.Combine(Server.MapPath("~/Images/"), photo.Name);
            System.IO.File.Delete(fileName);
            context.Photos.Remove(photo);
            context.SaveChanges();

            return RedirectToAction("Index");
        }
        public ActionResult AddWork()
        {
            ViewBag.Header = "Layihədə görülmüş iş əlavə et";
            ViewBag.Button = "Əlavə et";
            ViewBag.Projects = context.Projects.ToList<Project>();

            return View("Work");
        }
        [HttpPost]
        public ActionResult AddWork(MyWorksOnProject work)
        {
            if (ModelState.IsValid)
            {
                context.MyWorksOnProjects.Add(work);
                context.SaveChanges();

                return RedirectToAction("Index");
            }
            ViewBag.Header = "Layihədə görülmüş iş əlavə et";
            ViewBag.Button = "Əlavə et";
            ViewBag.Projects = context.Projects.ToList<Project>();

            return View("Work");
        }
        public ActionResult UpdateWork(int id)
        {
            MyWorksOnProject work = context.MyWorksOnProjects.Single(i => i.Id == id);
            ViewBag.Projects = context.Projects.ToList<Project>();
            ViewBag.Header = "Layihədə görülmüş işdə düzəliş et";
            ViewBag.Button = "Düzəliş et";
            return View("Work", work);
        }
        [HttpPost]
        public ActionResult UpdateWork(MyWorksOnProject work)
        {
            if (ModelState.IsValid)
            {
                MyWorksOnProject wrk = context.MyWorksOnProjects.Single(i => i.Id == work.Id);
                wrk.Name = work.Name;
                wrk.ProjectId = work.ProjectId;
                context.SaveChanges();

                return RedirectToAction("Index");
            }
            ViewBag.Projects = context.Projects.ToList<Project>();
            ViewBag.Header = "Layihədə görülmüş işdə düzəliş et";
            ViewBag.Button = "Düzəliş et";
            return View("Work");
        }
        [HttpPost]
        public ActionResult DeleteWork(int id)
        {
            MyWorksOnProject work = context.MyWorksOnProjects.Single(i => i.Id == id);
            context.MyWorksOnProjects.Remove(work);
            context.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult AddContact()
        {
            ViewBag.Header = "Kontakt məlumatı əlavə et";
            ViewBag.Button = "Əlavə et";

            return View("Contact");
        }
        [HttpPost]
        public ActionResult AddContact(Contact contact)
        {
            if (ModelState.IsValid)
            {
                context.Contacts.Add(contact);
                context.SaveChanges();

                return RedirectToAction("Index");
            }
            ViewBag.Header = "Kontakt məlumatı əlavə et";
            ViewBag.Button = "Əlavə et";

            return View("Contact");
        }
        public ActionResult UpdateContact(int id)
        {
            Contact contact = context.Contacts.Single(i => i.Id == id);
            ViewBag.Header = "Kontakt məlumatında düzəliş et";
            ViewBag.Button = "Düzəliş et";
            return View("Contact", contact);
        }
        [HttpPost]
        public ActionResult UpdateContact(Contact contact)
        {
            if (ModelState.IsValid)
            {
                Contact cntct = context.Contacts.Single(i => i.Id == contact.Id);
                cntct.Name = contact.Name;
                cntct.Value = contact.Value;
                cntct.ShowOnContactPage = contact.ShowOnContactPage;
                cntct.ShowOnFooter = contact.ShowOnFooter;
                context.SaveChanges();

                return RedirectToAction("Index");
            }
            ViewBag.Header = "Kontakt məlumatında düzəliş et";
            ViewBag.Button = "Düzəliş et";
            return View("Contact");
        }
        [HttpPost]
        public ActionResult DeleteContact(int id)
        {
            Contact contact = context.Contacts.Single(i => i.Id == id);
            context.Contacts.Remove(contact);
            context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}