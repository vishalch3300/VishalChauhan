using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MachineTest1.Models;
using System.Data.Entity;
using Microsoft.SqlServer.Server;
using System.Data.SqlTypes;

namespace MachineTest1.Controllers
{
    public class EmployeeController : Controller
    {
        MachineTestDBEntities db = new MachineTestDBEntities();

        [HttpGet]
        public ActionResult Index()
        {
            var data = db.EmployeeTbls.ToList();
            return View(data);
        }

        [HttpGet]
        public ActionResult Create()
        {
            List<CountryTbl> countryList = db.CountryTbls.ToList();
            ViewBag.CountryList = new SelectList(countryList, "CountryId", "CountryName");
            return View();
        }

        [HttpPost]
        public ActionResult Create(EmployeeTbl emp, bool IsMarathi, bool IsHindi, bool IsEnglish)
        {
            if (emp.CountryId == null)
            {
                ViewBag.Country = "The Country field is required.";
            }
            if (IsMarathi == false && IsHindi == false && IsEnglish == false)
            {
                ViewBag.languages = "The Languages field is required.";
            }
            if (ModelState.IsValid == true)
            {
                if (IsMarathi == true || IsHindi == true || IsEnglish == true)
                {
                    if (emp.CountryId != null)
                    {
                        emp.IsMarathi = (IsMarathi == true) ? true : false;
                        emp.IsHindi = (IsHindi == true) ? true : false;
                        emp.IsEnglish = (IsEnglish == true) ? true : false;

                        db.Entry(emp).State = EntityState.Added;
                        int a = db.SaveChanges();
                        if (a > 0)
                        {
                            TempData["InsertMessage"] = "<script>alert('Inserted !!')</script>";
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            TempData["InsertMessage"] = "<script>alert('Not Inserted !!')</script>";
                        }
                    }
                }  
            }
            List<CountryTbl> countryList = db.CountryTbls.ToList();
            ViewBag.CountryList = new SelectList(countryList, "CountryId", "CountryName");
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var user = db.EmployeeTbls.Where(model => model.EmpId == id).FirstOrDefault();

            ViewBag.age = user.Age;

            List<CountryTbl> countryList = db.CountryTbls.ToList();
            ViewBag.CountryList = new SelectList(countryList, "CountryId", "CountryName");
            return View(user);
        }

        [HttpPost]
        public ActionResult Edit(EmployeeTbl emp, bool IsMarathi, bool IsHindi, bool IsEnglish)
        {
            if (emp.CountryId == null)
            {
                ViewBag.Country = "The Country field is required.";
            }
            if (IsMarathi == false && IsHindi == false && IsEnglish == false)
            {
                ViewBag.languages = "The Languages field is required.";
            }
            if (ModelState.IsValid == true)
            {
                if (IsMarathi == true || IsHindi == true || IsEnglish == true)
                {
                    if (emp.CountryId != null)
                    {
                        emp.IsMarathi = (IsMarathi == true) ? true : false;
                        emp.IsHindi = (IsHindi == true) ? true : false;
                        emp.IsEnglish = (IsEnglish == true) ? true : false;

                        db.Entry(emp).State = EntityState.Modified;
                        int a = db.SaveChanges();
                        if (a > 0)
                        {
                            TempData["InsertMessage"] = "<script>alert('Updated !!')</script>";
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            TempData["InsertMessage"] = "<script>alert('Not Updated !!')</script>";
                        }
                    }
                }

            }
            List<CountryTbl> countryList = db.CountryTbls.ToList();
            ViewBag.CountryList = new SelectList(countryList, "CountryId", "CountryName");
            return View(emp);
        }

        [HttpPost]
        public ActionResult Clear(EmployeeTbl emp)
        {
            return RedirectToAction("Create");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            if (id > 0)
            {
                var row = db.EmployeeTbls.Where(model => model.EmpId == id).FirstOrDefault();
                if (row != null)
                {
                    db.Entry(row).State = System.Data.Entity.EntityState.Deleted;
                    int a = db.SaveChanges();
                    if (a > 0)
                    {
                        TempData["DeleteMessage"] = "<script>alert('Deleted !!')</script>";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["DeleteMessage"] = "<script>alert('Not Deleted !!')</script>";
                    }
                }
            }
            return View();
        }
    }
}