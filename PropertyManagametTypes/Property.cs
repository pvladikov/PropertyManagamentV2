using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;
using System.ComponentModel;
using PropertyManagametTypes.Enum;

namespace PropertyManagametTypes
{
    [BsonIgnoreExtraElements]
    public class Property : EntityBase
    {
       
        public string upi { get; set; }

        public List<Owner> owners { get; set; }

        public decimal area { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        [BsonRepresentation(BsonType.String)]
        [BsonElement("manner_of_permanent_usage")]        
        public MannerOfPermanentUsage mannerOfPermanentUsage { get; set; }

        [BsonElement("picture_url")]
        public string pictureUrl { get; set; }

        public Mortgage mortgage { get; set; }

        public Property()
        {
            owners = new List<Owner>();
        }
    } 
}
