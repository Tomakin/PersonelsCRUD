using Project.Core.Data;
using Project.Data.Abstract;
using Project.Data.Concrete.DAL;
using Project.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Data.Concrete
{
    public class EFPersonel : EfEntityRepository<Personel>, IPersonel
    {
        public EFPersonel(EFProjectContext context) : base(context) { }
    }
}
