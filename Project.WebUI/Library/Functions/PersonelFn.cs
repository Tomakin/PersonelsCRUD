using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Project.Services.Abstract;
using Project.WebUI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Project.WebUI.Library.Functions
{
    public class PersonelFn
    {
        IDatabaseService _db;

        IWebHostEnvironment _env;
        public PersonelFn(IWebHostEnvironment env, IDatabaseService db)
        {
            _env = env;
            _db = db;
        }
        public List<string> GetContactTypes()
        {

            string path = _env.ContentRootPath + "/Library/Json/ContactTypes.json";
            string read = File.ReadAllText(path);

            return JsonSerializer.Deserialize<List<string>>(read);
        }

        public List<string> GetCountries()
        {

            string path = _env.ContentRootPath + "/Library/Json/Countries.json";
            string read = File.ReadAllText(path);

            return JsonSerializer.Deserialize<List<string>>(read);
        }

        public Index_VM GetFilteredModel(string ContactType, string CountryName)
        {
            Index_VM model = new Index_VM()
            {
                personels = _db.Personel.GetAll().ToList()
            };
            if (ContactType != null)
                model.personels = model.personels.Where(k => k.ContactType == ContactType).ToList();
            if (CountryName != null)
                model.personels = model.personels.Where(k => k.Country == CountryName).ToList();
            return model;
        }


        
    }
}
