using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace vegetable.Respository
{
    //CRUD的介面
   public interface ICRUD<T>
    {
        void Create(T Enity);
        void Update(T Enity);
        void Delete(T Enity);
     
        IQueryable<T> GetAll();
        void Load(IQueryable<T> Enity);

    }
}