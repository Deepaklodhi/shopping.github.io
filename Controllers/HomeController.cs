using MvcApplicationEmarketing.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcApplicationEmarketing.Controllers
{
    public class HomeController : Controller
    {
        dbeadmeenEntities4 db = new dbeadmeenEntities4();
        public ActionResult index(int?page)
        {
            
            if (TempData["cart"] != null)
            {
                float x = 0;
                List<cart> li2 = TempData["cart"] as List<cart>;
                foreach (var item in li2)
                {
                    x += item.bill;

                }

                TempData["total"] = x;
            }
            TempData.Keep();
            int pagesize = 20, pageindex = 1;
            pageindex = page.HasValue ? Convert.ToInt32(page) : 1;
            var list = (db.tbl_product.OrderByDescending(x => x.Pro_id).ToList());
            IPagedList<tbl_product> stu = list.ToPagedList(pageindex, pagesize);
            return View(stu);
           
        }

        public ActionResult Adtocart(int? Id)
        {
            

            tbl_product p = db.tbl_product.Where(x => x.Pro_id == Id).SingleOrDefault();
            return View(p);
        }

        List<cart> li = new List<cart>();
        [HttpPost]
        public ActionResult Adtocart(tbl_product pi, string qty, int Id)
        {
            tbl_product p = db.tbl_product.Where(x => x.Pro_id == Id).SingleOrDefault();

            cart c = new cart();
            c.productid = p.Pro_id;
            c.price = (float)p.pro_price;
            c.qty = Convert.ToInt32(qty);
            c.bill = c.price * c.qty;
            c.productname = p.Pro_Name;
            if (TempData["cart"] == null)
            {
                li.Add(c);
                TempData["cart"] = li;

            }
            else
            {
                List<cart> li2 = TempData["cart"] as List<cart>;
                li2.Add(c);
                TempData["cart"] = li2;
            }

            TempData.Keep();




            return RedirectToAction("index");
        }
        public ActionResult About()
        {
            return View();
        }
        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult checkout()
        {
            TempData.Keep();


            return View();
        }
        public ActionResult Registration()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registration(tbl_useer u)
        {
            if (ModelState.IsValid)
            {
                using (dbeadmeenEntities4 db = new dbeadmeenEntities4())
                {
                    db.tbl_useer.Add(u);
                    db.SaveChanges();
                    ModelState.Clear();
                    u= null;
                    ViewBag.message = "Sucessfully Ragistration";
                }
            }
            return View(u);
        }
        public ActionResult login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult login(tbl_useer u)
        {
            tbl_useer ad = db.tbl_useer.Where(x => x.U1_Contact == u.U1_Contact && x.U1_paswword == u.U1_paswword).SingleOrDefault();
            if (ad != null)
            {

                Session["ad_id"] = ad.U1_id.ToString();
                return RedirectToAction("index");

            }
            else
            {
                ViewBag.error = "Invalid username or password";

            }

            return View();
        }

    }
       
}
