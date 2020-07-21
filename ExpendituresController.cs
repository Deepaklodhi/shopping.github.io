using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Medhurry.Models;

namespace Medhurry.Controllers
{
    public class ExpendituresController : Controller
    {
        MedhurryContext db = new MedhurryContext();

        // GET: Expenditures
        public ActionResult Index()
        {
            var expenditures = db.expenditure.Include(e => e.ExpenseItem).Include(e => e.MasterVendor);
            return View(expenditures.ToList());
        }

        // GET: Expenditures/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Expenditures expenditures = db.expenditure.Find(id);
            if (expenditures == null)
            {
                return HttpNotFound();
            }
            return View(expenditures);
        }

        // GET: Expenditures/Create
        public ActionResult Create()
        {
            ViewBag.SportType1 = new SelectList(db.expenseItems, "ExpenseItemID", "Description");
            ViewBag.SportType2 = new SelectList(db.masterVendors, "VendorID", "Name");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Expenditures cvm)
        {
            Expenditures cat = new Expenditures();

            cat.Status = false;
            if (cvm.statusex == "Yes")
            {
                cat.Status = true;
            }
            try
            {

           
            cat.EntryDate = cvm.EntryDate;
            cat.Purpose = cvm.Purpose;
            cat.Amount = cvm.Amount;
            cat.ModifiedDate = cvm.ModifiedDate;
            cat.CreateDate = cvm.CreateDate;
            cat.ModifiedDateBy = cvm.ModifiedDateBy;
            cat.CreatedBy = cvm.CreatedBy;
            cat.Remarks = cvm.Remarks;
            cat.ExpenseItemID = cvm.ExpenseItemID;
            cat.VendorID = cvm.VendorID;
            cat.VoucherName = cvm.VoucherName;
            db.expenditure.Add(cvm);
            ViewBag.SportType1 = new SelectList(db.expenseItems, "ExpenseItemID", "Description");
            ViewBag.SportType2 = new SelectList(db.masterVendors, "VendorID", "Name");
            db.SaveChanges();

            }







            catch (DbEntityValidationException ex)
            {
                foreach (var eve in ex.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
            }

            catch (Exception ex)
            {

            }
          
            return RedirectToAction("Index");
        }
    

        
            public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Expenditures expenditures = db.expenditure.Find(id);
            if (expenditures == null)
            {
                return HttpNotFound();
            }
            ViewBag.SportType1 = new SelectList(db.expenseItems, "ExpenseItemID", "Description");
            ViewBag.SportType2 = new SelectList(db.masterVendors, "VendorID", "Name");
            return View(expenditures);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Expenditures cvm)
        {
            var cat = db.expenditure.Where(x => x.ExpenditureID == x.ExpenditureID).FirstOrDefault();
            cat.Status = false;
            if (cvm.statusex == "Yes")
            {
                cat.Status = true;
            }
            cat.EntryDate = cvm.EntryDate;
            cat.Purpose = cvm.Purpose;
            cat.Amount = cvm.Amount;
            cat.ModifiedDate = cvm.ModifiedDate;
            cat.CreateDate = cvm.CreateDate;
            cat.ModifiedDateBy = cvm.ModifiedDateBy;
            cat.CreatedBy = cvm.CreatedBy;
            cat.Remarks = cvm.Remarks;
            cat.ExpenseItemID = cvm.ExpenseItemID;
            cat.VendorID = cvm.VendorID;
            cat.VoucherName = cvm.VoucherName;
        
            db.SaveChanges();

            ViewBag.SportType1 = new SelectList(db.expenseItems, "ExpenseItemID", "Description");
            ViewBag.SportType2 = new SelectList(db.masterVendors, "VendorID", "Name");
            return RedirectToAction("Index");
        }

     
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Expenditures expenditures = db.expenditure.Find(id);
            if (expenditures == null)
            {
                return HttpNotFound();
            }
            return View(expenditures);
        }

  
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Expenditures expenditures = db.expenditure.Find(id);
            db.expenditure.Remove(expenditures);
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
