using Newtonsoft.Json;
using PropertyManagamentDatabase;
using PropertyManagamentDatabase.Interface;
using PropertyManagametTypes;
using PropertyManagametTypes.Enum;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace PropertyManagament.Controllers
{
    public class PropertyController : Controller
    {
        IDatabase<Property> propertyRepository;
        IDatabase<Owner> ownerRepository;

        public PropertyController() : this(new MongoDatabase<Property>(), new MongoDatabase<Owner>()) {

        }

        public PropertyController(MongoDatabase<Property> categoriesRepo, MongoDatabase<Owner> ownerRepo) {
            this.propertyRepository = categoriesRepo;
            this.ownerRepository = ownerRepo;
        }


        [HttpGet]
        public ActionResult Read() {
            var properties = propertyRepository.GetAll;
            return Json(properties, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetEnum() {
            var list = EnumHelper.GetSelectList(typeof(MannerOfPermanentUsage)).Select(x => new NumericValueSelectListItem() {
                Text = x.Text,
                Value = int.Parse(x.Value)
            });

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<JsonResult> GetOwnersByProperty(string property_id) {
            Property property = JsonConvert.DeserializeObject<Property>(property_id);
            property = await propertyRepository.GetByID(property.Id);
            List<Owner> owners = new List<Owner>();
            if (property != null) {
                owners = property.owners;
            }

            return Json(owners, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult CreateProperty() {
            Property property = new Property();
            propertyRepository.Create(property);
            var json = JsonConvert.SerializeObject(property, new CustumStringEnumConverter());

            return new ContentResult { Content = json, ContentType = "application/json" };
        }

        [HttpPost]
        public async Task<JsonResult> CreateMortgage(Property property) {
            Mortgage mortgage = new Mortgage();
            property.mortgage = mortgage;

            await propertyRepository.Update(property);
            return Json(mortgage);
        }

        [HttpPost]
        public async Task<JsonResult> CreateOwner(Property property) {
            Owner owner = new Owner();
            property.owners.Add(owner);

            await propertyRepository.Update(property);
            return Json(owner);
        }

        [HttpPost]
        public JsonResult DeleteProperty(Property property) {
            propertyRepository.Delete(property);
            return Json(true);
        }

        [HttpPost]
        public async Task<JsonResult> UpdateProperty(Property property) {
            await propertyRepository.Update(property);
            return Json(true);
        }
    }

    public class CustumStringEnumConverter : Newtonsoft.Json.Converters.StringEnumConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {
            if (value is MannerOfPermanentUsage) {
                writer.WriteValue(Enum.GetName(typeof(MannerOfPermanentUsage), (MannerOfPermanentUsage)value));// or something else
                return;
            }

            base.WriteJson(writer, value, serializer);
        }
    }

    public class NumericValueSelectListItem
    {
        public string Text { get; set; }
        public int Value { get; set; }
    }
}