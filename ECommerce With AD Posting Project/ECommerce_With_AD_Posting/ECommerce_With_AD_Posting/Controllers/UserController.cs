using ECommerce_With_AD_Posting.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList.Mvc;
using PagedList;
using System.IO;

namespace ECommerce_With_AD_Posting.Controllers
{
    public class UserController : Controller
    {
        ECommerce_With_AD_PostingEntities1 db = new ECommerce_With_AD_PostingEntities1();

        //
        // GET: /User/


        //DISPLAY CATEGORY FOR USERS//

        public ActionResult Index(int? page)
        {
            int pagesize = 9, pageindex = 1;

            pageindex = page.HasValue ? Convert.ToInt32(page) : 1;

            var list = db.tbl_cateogry.Where(x => x.cat_status == 1).OrderByDescending(x => x.cat_id).ToList();

            IPagedList<tbl_cateogry> cat = list.ToPagedList(pageindex, pagesize);


            return View(cat);
        }

        //DISPLAY CATEGORY FOR USERS//



        //STUDENT REGISTERATION START//
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]

        public ActionResult Register(tbl_user us, HttpPostedFileBase imgfile)
        {
            tbl_admin ad = new tbl_admin();

            string path = uploadimage(imgfile);

            if (path.Equals("-1"))
            {
                ViewBag.error = "Image Could not be Uploaded";
            }

            else
            {
                tbl_user u = new tbl_user();

                u.u_name = us.u_name;
                u.u_email = us.u_email;
                u.u_password = us.u_password;
                u.u_image = path;
                u.u_contact = us.u_contact;
                u.ad_id_fk = us.ad_id_fk;


                db.tbl_user.Add(u);
                db.SaveChanges();

                return RedirectToAction("Login");
            }

            return View();
        }

        //STUDENT REGISTERATION END//



        //STUDENT LOGIN START//
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]

        public ActionResult Login(tbl_user us)
        {
            tbl_user u = db.tbl_user.Where(x => x.u_email == us.u_email && x.u_password == us.u_password).SingleOrDefault();

            if (u!=null)
            {
                Session["u_id"] = u.u_id;
                Session["u_name"] = u.u_name;

                return RedirectToAction("Index");
            }

            else
            {
                ViewBag.error="Invalid Email or Password";
            }

            return View();
        }


        //STUDENT LOGIN END//



        //LOGOUT//
        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();

            return RedirectToAction("Login");

            return View();
        }
        //LOGOUT END//




        //POST AN ADD METHOD//


        [HttpGet]

        public ActionResult CreateAdd()
        {
            List<tbl_cateogry> li = db.tbl_cateogry.ToList();
            ViewBag.categorylist = new SelectList(li, "cat_id", "cat_name");

            return View();
        }

        [HttpPost]
        public ActionResult CreateAdd(tbl_product p, HttpPostedFileBase imgfile)
        {
            List<tbl_cateogry> li = db.tbl_cateogry.ToList();
            ViewBag.cat_list = new SelectList(li, "cat_id", "cat_name");


            string path = uploadimage(imgfile);

            if (path.Equals("-1"))
            {
                ViewBag.error = "image could not be uploaded";

            }
            else

            {


                tbl_product pr = new tbl_product();
                pr.pro_name = p.pro_name;
                pr.pro_price = p.pro_price;
                pr.pro_image = path;
                pr.cat_id_fk = p.u_id_fk;

                pr.pro_desc = p.pro_desc;
                pr.u_id_fk = Convert.ToInt32(Session["u_id"].ToString());
                db.tbl_product.Add(pr);
                db.SaveChanges();

                Response.Redirect("Index");
            }
            return View();
        }



        public ActionResult DisplayAdd(int? id, int? page)
        {
            int pagesize = 9, pageindex = 1;
            pageindex = page.HasValue ? Convert.ToInt32(page) : 1;
            var list = db.tbl_product.Where(x => x.cat_id_fk == id).OrderByDescending(x => x.pro_id).ToList();
            IPagedList<tbl_product> cate = list.ToPagedList(pageindex, pagesize);

            return View(cate);
        }


        [HttpPost]
        public ActionResult DisplayAdd()
        {

            return View();
        }
        // image upload ............................




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

	}
}