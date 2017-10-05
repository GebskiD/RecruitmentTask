using RecruitmentTask.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RecruitmentTask.Controllers
{
    public class CustomerController : Controller
    {
        private RecruitmentTaskDb _recruitmentTaskDbCtx { get; set; }

        public CustomerController()
        {
            _recruitmentTaskDbCtx = new RecruitmentTaskDb();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Customer model)
        {
            if (ModelState.IsValid && !CheckIfTelephoneExistInDb(model.Telephone))
            {
                Customer customer = new Customer
                {
                    Name = model.Name,
                    Surname = model.Surname,
                    Address = model.Address,
                    Telephone = model.Telephone
                };
                _recruitmentTaskDbCtx.Customers.Add(customer);
                _recruitmentTaskDbCtx.SaveChanges();

                ModelState.Clear();
                TempData["message"] = string.Format("Customer {0} {1} created", customer.Name, customer.Surname);
                return View();
            }
            if(CheckIfTelephoneExistInDb(model.Telephone)) TempData["message"] = "Telephone allready exsist!";
            return View(model);
        }

        public ActionResult CustomersData()
        {
            List<Customer> CustomersList =_recruitmentTaskDbCtx.Customers.Take(10).ToList();
            return View(CustomersList);
        }

        public ActionResult RemoveCustomer(int Id)
        {
            Customer RemovedCustomer = _recruitmentTaskDbCtx.Customers.Find(Id);
            _recruitmentTaskDbCtx.Customers.Remove(RemovedCustomer);
            _recruitmentTaskDbCtx.SaveChanges();
            return RedirectToAction("CustomersData");
        }

        public ActionResult Update(int Id)
        {
            var editedCustomer = _recruitmentTaskDbCtx.Customers.Where(m => m.Id == Id).FirstOrDefault();
            return View(editedCustomer);
        }


        [HttpPost]
        public ActionResult Update(Customer editedCustomer)
        {
            string erroMessage = string.Empty;
            var model = _recruitmentTaskDbCtx.Customers.Where(m => m.Id == editedCustomer.Id).FirstOrDefault();

            if (ModelState.IsValid)
            {
                if (editedCustomer.Telephone == model.Telephone)
                {
                    var SavedData = SaveEditedChanges(model, editedCustomer);
                    return SavedData;
                }
                else
                {
                    if (!CheckIfTelephoneExistInDb(editedCustomer.Telephone))
                    {
                        var SavedData = SaveEditedChanges(model, editedCustomer);
                        return SavedData;
                    }
                    erroMessage = "Telephone allready exsist!<br />";
                }
            }

            var errors = ModelState.Values.SelectMany(v => v.Errors).ToList();
            foreach (var item in errors)
            {
                erroMessage = erroMessage + item.ErrorMessage + "<br />";
            }

            TempData["message"] = erroMessage;
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }






        private bool CheckIfTelephoneExistInDb(string Telephone)
        {
            bool existInDb= _recruitmentTaskDbCtx.Customers.Where(m => m.Telephone == Telephone).Any();
            return existInDb;
        }

        private JsonResult SaveEditedChanges(Customer model, Customer customer)
        {
            model.Name = customer.Name;
            model.Surname = customer.Surname;
            model.Address = customer.Address;
            model.Telephone = customer.Telephone;

            _recruitmentTaskDbCtx.SaveChanges();
            return Json(new { success = false }, JsonRequestBehavior.AllowGet);
        }
    }
}