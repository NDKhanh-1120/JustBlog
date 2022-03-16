using FA.JustBlog.Models;
using Core.Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FA.JustBlog.Controllers
{
    public class PostController : Controller
    {
        JustBlogContext db = new JustBlogContext();
        // GET: Post
        public ActionResult Index(string searchstring)
        {
            if (String.IsNullOrEmpty(searchstring))
            {
                var posts = db.Posts;
                return View(PostRepository.GetAllPosts());
            }
            else
            {
                var result = db.Posts.Where
                    (
                        p =>  p.Title.Contains(searchstring)
                        || p.Desciption.Contains(searchstring)
                    );
                return View(result);
            }
        }
        public ActionResult _LastestPosts()
        {
            ViewBag.Title = "LastestPosts";
            return PartialView("_ListPosts",db.Posts.OrderByDescending(p => p.CreateDate).Take(5));
        }
        public PartialViewResult _MostViewPosts()
        {
            ViewBag.Title = "MostViewPosts";
            return PartialView("_ListPosts",db.Posts.OrderByDescending(p => p.ViewCount).Take(5));
        }
        public ActionResult Detail(int iD)
        {
            return View(db.Posts.FirstOrDefault(p => p.Id == iD));
        }

        public ActionResult CreateNewPost()
        {
            ViewData["CategoryId"] = new SelectList(db.Categories, "CategoryId", "CategoryName");
            return View();
        }
        [HttpPost]
        public ActionResult CreateNewPost([Bind(Include = "Id,Title,Desciption,PostContent,UrlSlug,Published,PostedOn,CreateDate,ModifiedDate,CategoryId")] Post post)
        {
            Debug.WriteLine(post);
            post.CreateDate = DateTime.Now;
            db.Posts.Add(post);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public PartialViewResult _MenuCategories()
        {
            var categories = db.Categories.ToList();
            return PartialView(categories);
        }
        public ActionResult PostCategory(int iD)
        {
            ViewBag.Title = "CategoryName";
            return View("_ListPosts",db.Posts.Where(p => p.CategoryId == iD));   
        }
    }
}