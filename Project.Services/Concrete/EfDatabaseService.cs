using Project.Data.Abstract;
using Project.Data.Concrete;
using Project.Data.Concrete.DAL;
using Project.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Services.Concrete
{
    public class EfDatabaseService : IDatabaseService
    {
        private readonly EFProjectContext context;

        public EfDatabaseService(EFProjectContext _context)
           => context = _context;

        //Field
        private IPersonel _Personel;

        //Property
        public IPersonel Personel
            => _Personel ?? (_Personel = new EFPersonel(context));

        public void Dispose()
           => context.Dispose();

        public int SaveChanges()
            => context.SaveChanges();
    }
}
