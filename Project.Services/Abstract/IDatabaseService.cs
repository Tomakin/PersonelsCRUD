using Project.Data.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Services.Abstract
{
    public interface IDatabaseService : IDisposable
    {
        IPersonel Personel { get; }
        int SaveChanges();
    }
}

