using ECommerce_With_AD_Posting.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using PagedList.Mvc;
using PagedList;

namespace ECommerce_With_AD_Posting.Controllers
{
    public class AdminController : Controller
    {
        ECommerce_With_AD_PostingEntities1 db = new ECommerce_With_AD_PostingEntities1();

        //
        // GET: /Admin/
        public ActionResult Index()
        {
            return View();
        }

        //ADMIN SIGNUP START//
        public ActionResult Signup()
        {
            return View();
        }


        [HttpPost]


        public ActionResult Signup(tbl_admin a)
        {
            tbl_admin ad = new tbl_admin();

            ad.ad_name = a.ad_name;
            ad.ad_password = a.ad_password;
            ad.ad_email = a.ad_email;

            db.tbl_admin.Add(ad);

            db.SaveChanges();

            return View("Login");
        }

        //ADMIN SIGNUP END//




        //ADMIN LOGIN START//
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]


        public ActionResult Login(tbl_admin a)
        {
            tbl_admin ad = db.tbl_admin.Where(x => x.ad_email == a.ad_email && x.ad_password == a.ad_password).SingleOrDefault();

            if (ad != null)
            {
                Session["ad_id"] = ad.ad_id;
                Session["ad_name"] = ad.ad_name;

                return RedirectToAction("Category");
            }

            else
            {
                ViewBag.error = "Invalid Email or Password";
            }

            return View();
        }

        //ADMIN LOGIN END//




        //CATEGORY START//
        public ActionResult Category()
        {
            return View();
        }


        [HttpPost]

      
        public ActionResult Category(tbl_cateogry cat, HttpPostedFileBase imgfile)
        {
            tbl_admin ad = new tbl_admin();

            string path = uploadimage(imgfile);

            if (path.Equals("-1"))
            {
                ViewBag.error = "Image Could not Be Uploaded";
            }

            else
            {
                tbl_cateogry ca = new tbl_cateogry();

                ca.cat_name = cat.cat_name;
                ca.cat_image = path;
                ca.cat_status = 1;
                ca.ad_id_fk = Convert.ToInt32(Session["ad_id"].ToString());

                db.tbl_cateogry.Add(ca);
                db.SaveChanges();

                return RedirectToAction("DisplayCategory");
            }
        

            return View();
        }

        //CATEGORY END//




        //UPLOAD IMAGE CODE//
        public string uploadimage(HttpPostedFileBase file)
        {
            Random r = new Random();

            string path = "-1";

            int random = r.Next();

            if (file != null && file.ContentLength > 0)
            {
                string extension = Path.GetExtension(file.FileName);
                if (extension.ToLower().Equals(".jpg") || extension.ToLower().Equals(".jpeg") || extension.ToLower().Equals(".png"))
                {
                    try
                    {
                        path = Path.Combine(Server.MapPath("~/Content/upload"), random + Path.GetFileName(file.FileName));
                        file.SaveAs(path);
                        path = "~/Content/upload/" + random + Path.GetFileName(file.FileName);
                    }
                    catch (Exception ex)
                    {
                        path = "-1";

                        throw;
                    }
                }

                else
                {
                    Response.Write("<script>alert('Only .JPG , .JPEG , .PNG Formats Are Allowed')</script>");
                }

            }

                else
	            {
                    Response.Write("<script>alert('Please Select a File')</script>");

                    path = "-1";
	            }

            return path;
        }
        //UPLOAD IMAGE CODE//



        //CATEGORY DISPLAY START//

        public ActionResult DisplayCategory(int ? page)
        {
            int pagesize = 9, pageindex = 1;

            pageindex = page.HasValue ? Convert.ToInt32(page) : 1;

            var list = db.tbl_cateogry.Where(x => x.cat_status == 1).OrderByDescending(x => x.cat_id).ToList();

            IPagedList<tbl_cateogry> cat = list.ToPagedList(pageindex, pagesize);


            return View(cat);
        }

        //CATEGORY DISPLAY END//
    }
}