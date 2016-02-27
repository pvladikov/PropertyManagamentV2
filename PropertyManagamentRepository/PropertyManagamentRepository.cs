using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PropertyManagamentDatabase.Interface;
using PropertyManagamentDatabase;
using PropertyManagametTypes;

namespace PropertyManagamentRepository
{
    public static class PropertyManagamentRepository
    {
        private static IDatabase<Property> db = new MongoDatabase<Property>("PropertyManagament");


        public static IEnumerable<Property> ToList()
        {
            return db.Query.AsEnumerable();
        }

        public static bool Create(Property property)
        {
            return db.Update(property);
        }

        public static bool Update(Property property)
        {
            return db.Update(property);
        }
    }
}
