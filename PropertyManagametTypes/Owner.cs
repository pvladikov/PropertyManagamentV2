using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyManagametTypes
{
    [BsonIgnoreExtraElements]
    public class Owner : EntityBase
    {      
        [BsonRepresentation(BsonType.String)]
        public string name { get; set; }

        [BsonRepresentation(BsonType.String)]
        [BsonElement("last_name")]
        public string lastName { get; set; }

        public string address { get; set; }

        [BsonElement("picture_url")]
        public string pictureUrl { get; set; }
    
    }
}
