using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyManagametTypes
{
    public class Mortgage : EntityBase
    {
        [BsonElement("end_date")]
        public DateTime endDate { get; set; }

        public decimal amount { get; set; }
    }
}
