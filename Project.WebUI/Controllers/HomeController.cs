using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
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
        IWebHostEnvironment _env;

        public HomeController(IDatabaseService db, PersonelFn personelFn, IWebHostEnvironment env)
        {
            _db = db;
            _PersonelFn = personelFn;
            _env = env;
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

        public IActionResult LoadPersonelsByFilter(string ContactType = null, string CountryName = null)
        {
            Index_VM model = _PersonelFn.GetFilteredModel(ContactType, CountryName);

            return PartialView("Views/Partials/ListPersonels.cshtml", model.personels);
        }

        public async Task<IActionResult> OnPostExport()
        {
            List<Personel> personeller = _db.Personel.GetAll().ToList();

            string sWebRootFolder = _env.WebRootPath;
            string sFileName = $"Personeller-{DateTime.Now.ToShortDateString()}.xlsx";
            string URL = string.Format("{0}://{1}/{2}", Request.Scheme, Request.Host, sFileName);
            FileInfo file = new FileInfo(Path.Combine(sWebRootFolder, sFileName));
            var memory = new MemoryStream();
            using (var fs = new FileStream(Path.Combine(sWebRootFolder, sFileName), FileMode.Create, FileAccess.Write))
            {
                IWorkbook workbook;
                workbook = new XSSFWorkbook();
                ISheet excelSheet = workbook.CreateSheet("Personel");


                IRow row = excelSheet.CreateRow(0);
                row.CreateCell(0).SetCellValue("#");
                row.CreateCell(1).SetCellValue("Adı");
                row.CreateCell(2).SetCellValue("Soyadı");
                row.CreateCell(3).SetCellValue("TC No");
                row.CreateCell(4).SetCellValue("Şirket Adı");
                row.CreateCell(5).SetCellValue("İletişim Türü");
                row.CreateCell(6).SetCellValue("Ülke");
                row.CreateCell(7).SetCellValue("İl");
                row.CreateCell(8).SetCellValue("İlçe");
                row.CreateCell(9).SetCellValue("Açık Adres");
                row.CreateCell(10).SetCellValue("Posta Kodu");
                row.CreateCell(11).SetCellValue("E-Mail");
                row.CreateCell(12).SetCellValue("Cep Telefonu");

                int i = 1;
                foreach (var personel in personeller)
                {
                    row = excelSheet.CreateRow(i);
                    row.CreateCell(0).SetCellValue(i);
                    row.CreateCell(1).SetCellValue(personel.Name);
                    row.CreateCell(2).SetCellValue(personel.SurName);
                    row.CreateCell(3).SetCellValue(personel.TCNo);
                    row.CreateCell(4).SetCellValue(personel.CompanyName);
                    row.CreateCell(5).SetCellValue(personel.ContactType);
                    row.CreateCell(6).SetCellValue(personel.Country);
                    row.CreateCell(7).SetCellValue(personel.City);
                    row.CreateCell(8).SetCellValue(personel.District);
                    row.CreateCell(9).SetCellValue(personel.Address);
                    row.CreateCell(10).SetCellValue(personel.PostCode);
                    row.CreateCell(11).SetCellValue(personel.EMail);
                    row.CreateCell(12).SetCellValue(personel.Phone);
                    i++;
                }

                workbook.Write(fs);
            }
            using (var stream = new FileStream(Path.Combine(sWebRootFolder, sFileName), FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", sFileName);
        }
    }
}
