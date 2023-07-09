using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Security.Claims;

namespace BookAccountApp.ApiClasses
{
   public class Object
    {
        public int objectId { get; set; }
        public string name { get; set; }
        public Nullable<int> parentObjectId { get; set; }
        public string objectType { get; set; }
        public string notes { get; set; }
        public Nullable<System.DateTime> createDate { get; set; }
        public Nullable<System.DateTime> updateDate { get; set; }
        public Nullable<int> createUserId { get; set; }
        public Nullable<int> updateUserId { get; set; }

        private string urimainpath = "Object/";
        public async Task<List<Object>> GetAll()
        {


            List<Object> list = new List<Object>();
            //  Dictionary<string, string> parameters = new Dictionary<string, string>();
            //parameters.Add("mainBranchId", mainBranchId.ToString());
            //parameters.Add("userId", userId.ToString());
            //parameters.Add("date", date.ToString());
            //#################
            IEnumerable<Claim> claims = await APIResult.getList(urimainpath+"Get");

            foreach (Claim c in claims)
            {
                if (c.Type == "scopes")
                {
                    list.Add(JsonConvert.DeserializeObject<Object>(c.Value, new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy" }));
                }
            }
            return list;


        }

   
        public async Task<decimal> Save(Object newObject)
        {

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            string method = urimainpath + "Save";

            var myContent = JsonConvert.SerializeObject(newObject);
            parameters.Add("Object", myContent);
           return await APIResult.post(method, parameters);


        }

       
        public async Task<Object> GetByID(int valId)
        {

            Object item = new Object();
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("Id", valId.ToString());
            //#################
            IEnumerable<Claim> claims = await APIResult.getList(urimainpath + "GetByID", parameters);

            foreach (Claim c in claims)
            {
                if (c.Type == "scopes")
                {
                    item = JsonConvert.DeserializeObject<Object>(c.Value, new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy" });
                    break;
                }
            }
            return item;

        }

      



        public async Task<decimal> Delete(int objectId, int userId, bool final)
        {
            //int objectId, int userId, bool final
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("objectId", objectId.ToString());
            parameters.Add("userId", userId.ToString());
            parameters.Add("final", final.ToString());
            string method = urimainpath + "Delete";
           return await APIResult.post(method, parameters);

        }


     


    }
}

