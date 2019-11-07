using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Project.Entities;
using Project.Services.Abstract;
using Project.WebUI.Library.Functions;
using Project.WebUI.Models;

namespace Project.WebUI.Controllers
{
    public class HomeController : Controller
    {
        IDatabaseService _db;
        PersonelFn _PersonelFn;

        public HomeController(IDatabaseService db, PersonelFn personelFn)
        {
            _db = db;
            _PersonelFn = personelFn;
        }

        public IActionResult Index()
        {
            Index_VM model = new Index_VM()
            {
                personels = _db.Personel.GetAll().ToList(),

                ContactTypes = _PersonelFn.GetContactTypes().Select(k => new SelectListItem() { 
                    Text = k,
                    Value = k
                }).ToList(),

                Countries = _PersonelFn.GetCountries().Select(k => new SelectListItem() { 
                Text = k,
                Value = k
                }).ToList()
            };

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add([Bind]Personel personel)
        {
            var s = Request;
            if (ModelState.IsValid)
            {
                _db.Personel.Add(personel);
                _db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public IActionResult Update(int id)
        {
            Index_VM model = new Index_VM()
            {
                personel = _db.Personel.GetByID(id),

                ContactTypes = _PersonelFn.GetContactTypes().Select(k => new SelectListItem()
                {
                    Text = k,
                    Value = k
                }).ToList(),

                Countries = _PersonelFn.GetCountries().Select(k => new SelectListItem()
                {
                    Text = k,
                    Value = k
                }).ToList()
            };

            if (model.personel != null)
            {
                return View(model);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Personel personel)
        {
            if (ModelState.IsValid)
            {
                _db.Personel.Update(personel);
                _db.SaveChanges();
            }
            return RedirectToAction("Update", new { id = personel.ID });
        }

        public IActionResult Delete(int id)
        {
            _db.Personel.DeleteByID(id);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
