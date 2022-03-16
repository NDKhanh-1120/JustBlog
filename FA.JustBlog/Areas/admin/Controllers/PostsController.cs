using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FA.JustBlog.Models;
using Core.Repositories;
using Core;

namespace FA.JustBlog.Areas.admin.Controllers
{
    public class PostsController : Controller
    {
        private JustBlogContext db = new JustBlogContext();

        // GET: admin/Posts
        public ActionResult Index(string searchstring, string typepost)
        {
            if (!String.IsNullOrEmpty(searchstring))
            {
                var result = db.Posts.Where
                    (
                        p => p.Desciption.Contains(searchstring)
                        || p.PostContent.Contains(searchstring)
                        || p.Title.Contains(searchstring)
                        || p.Published.Contains(searchstring)
                    );

                return View(result);
            }
            else if (!String.IsNullOrEmpty(typepost))
            {
                switch (typepost)
                {
                    case "lastestposts":
                        {
                            ViewBag.Title = "Lastest Posts";
                            return View(db.Posts.OrderByDescending(p=>p.CreateDate).Take(5));
       
                        }
                    case "mostviewedposts":
                        {
                            return View(PostRepository.GetMostViewedPosts());
                            break;
                        }
                    case "mosterestposts":
                        {
                            return View(PostRepository.GetMostInterestedPosts());
                            break;
                        }
                    case "publishedposts":
                        {
                            return View(PostRepository.GetPublishedPosts());
                            break;
                        }
                    case "unpublishedposts":
                        {
                            return View(PostRepository.GetUnpublishedPosts());
                            break;
                        }
                    default:
                        {
                            return View();
                            break;
                        }
                }
            }
            else
            {
                return View(db.Posts.ToList());
            }
        }

        // GET: admin/Posts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // GET: admin/Posts/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName");
            return View();
        }

        // POST: admin/Posts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Desciption,PostContent,UrlSlug,Published,PostedOn,CreateDate,ModifiedDate,ViewCount,CategoryId")] Post post)
        {
            if (ModelState.IsValid)
            {
                db.Posts.Add(post);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", post.CategoryId);
            return View(post);
        }

        // GET: admin/Posts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", post.CategoryId);
            return View(post);
        }

        // POST: admin/Posts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Desciption,PostContent,UrlSlug,Published,PostedOn,CreateDate,ModifiedDate,ViewCount,CategoryId")] Post post)
        {
            if (ModelState.IsValid)
            {
                db.Entry(post).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", post.CategoryId);
            return View(post);
        }

        // GET: admin/Posts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: admin/Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Post post = db.Posts.Find(id);
            db.Posts.Remove(post);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
