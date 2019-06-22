using MvcApplicationEmarketing.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcApplicationEmarketing.Controllers
{
    public class PurchaseController : Controller
    {
        //
        // GET: /Purchase/
        dbeadmeenEntities5 db = new dbeadmeenEntities5();

        public ActionResult Details()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Details(tbl_purchase p)
        {
            if(ModelState.IsValid)
            {
                using(dbeadmeenEntities5 db = new dbeadmeenEntities5())
                {
                    db.tbl_purchase.Add(p);
                    db.SaveChanges();
                    ModelState.Clear();
                    p = null;
                    //ViewBag.message = "sucess fully order";
                    TempData["message"] = "sucess fully order";
                    
                   
                }
            }
            //return View(p);
            return RedirectToAction("Details");
        }

    }
}
