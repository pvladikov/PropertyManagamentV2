using PropertyManagametTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PropertyManagamentDatabase.Interface
{
    public interface  IDatabase<T> where T : EntityBase
    {
        bool Delete(T item);
        void DeleteAll();
        IQueryable<T> Query { get; set; }
        void Create(T item);
        bool Update(T item);
        T GetByID(string id);
    }
}
