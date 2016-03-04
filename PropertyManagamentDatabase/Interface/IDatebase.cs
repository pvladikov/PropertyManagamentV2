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
        Task Delete(T item);
        void DeleteAll();
        IQueryable<T> GetAll { get; set; }
        Task Create(T item);
        Task Update(T item);
        T GetByID(string id);
    }
}
