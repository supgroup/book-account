using Newtonsoft.Json;
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
using Newtonsoft.Json.Converters;

namespace BookAccountApp.ApiClasses
{
    public class Programs
    {

        public int programId { get; set; }
        public string name { get; set; }
        public string details { get; set; }
        public string notes { get; set; }
        public int isActive { get; set; }
        public Nullable<System.DateTime> createDate { get; set; }
        public Nullable<System.DateTime> updateDate { get; set; }
        public string programCode { get; set; }
        public Nullable<int> createUserId { get; set; }
        public Nullable<int> updateUserId { get; set; }
        public bool canDelete { get; set; }

        private string urimainpath = "programs/";
        /// <summary>
        /// ///////////////////////////////////////
        /// </summary>
        /// <returns></returns>
        /// 

        public async Task<List<Programs>> GetAll()
        {
            List<Programs> list = new List<Programs>();
       
            IEnumerable<Claim> claims = await APIResult.getList(urimainpath + "GetAll");

            foreach (Claim c in claims)
            {
                if (c.Type == "scopes")
                {
                    list.Add(JsonConvert.DeserializeObject<Programs>(c.Value, new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy" }));
                }
            }
            return list;

            //List<Programs> memberships = null;

            //HttpResponseMessage response = new HttpResponseMessage();
            //using (var client = new HttpClient())
            //{
            //    Uri uri = new Uri(Global.APIUri + urimainpath + "GetAll");
            //    response = await ApiConnect.ApiGetConnect(uri);

            //    response = await ApiConnect.ApiGetConnect(uri);
            //    if (response.IsSuccessStatusCode)
            //    {
            //        var jsonString = await response.Content.ReadAsStringAsync();

            //        memberships = JsonConvert.DeserializeObject<List<Programs>>(jsonString);

            //        return memberships;
            //    }
            //    else //web api sent error response 
            //    {
            //        memberships = new List<Programs>();
            //    }
            //    return memberships;
            //}

        }

        public async Task<decimal> Save(Programs obj)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            string method = urimainpath + "Save";

            var myContent = JsonConvert.SerializeObject(obj);
            parameters.Add("Object", myContent);
            return await APIResult.post(method, parameters);

            //var myContent = JsonConvert.SerializeObject(obj);
            //myContent = HttpUtility.UrlEncode(myContent);
            //Uri uri = new Uri(Global.APIUri + urimainpath + "Save?Object=" + myContent);

            //HttpResponseMessage response = new HttpResponseMessage();
            //response = await ApiConnect.ApiPostConnect(uri);
            //using (var client = new HttpClient())
            //{
            //    if (response.IsSuccessStatusCode)
            //    {
            //        var message = await response.Content.ReadAsStringAsync();
            //        message = JsonConvert.DeserializeObject<string>(message);
            //        return message;
            //    }
            //    return "";
            //}
        }

        public async Task<Programs> GetByID(int programId)
        {

            Programs item = new Programs();
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("programId", programId.ToString());
            //#################
            IEnumerable<Claim> claims = await APIResult.getList(urimainpath + "GetByID", parameters);

            foreach (Claim c in claims)
            {
                if (c.Type == "scopes")
                {
                    item = JsonConvert.DeserializeObject<Programs>(c.Value, new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy" });
                    break;
                }
            }


            return item;
            //Programs obj = new Programs();

            //HttpResponseMessage response = new HttpResponseMessage();
            //using (var client = new HttpClient())
            //{

            //    Uri uri = new Uri(Global.APIUri + urimainpath + "GetByID?programId=" + programId);

            //    response = await ApiConnect.ApiGetConnect(uri);
            //    if (response.IsSuccessStatusCode)
            //    {
            //       var jsonString = await response.Content.ReadAsStringAsync();
            //        obj = JsonConvert.DeserializeObject<Programs>(jsonString);
            //        return obj;
            //    }

            //    return obj;
            //}
        }

        public async Task<decimal> Delete(int programId, int userId, bool final)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("programId", programId.ToString());
            parameters.Add("userId", userId.ToString());
            parameters.Add("final", final.ToString());

            string method = urimainpath + "Delete";
            return await APIResult.post(method, parameters);


            //HttpResponseMessage response = new HttpResponseMessage();
            //using (var client = new HttpClient())
            //{
            //    Uri uri = new Uri(Global.APIUri + urimainpath + "Delete?programId=" + programId + "&userId=" + userId + "&final=" + final);
            //    response = await ApiConnect.ApiPostConnect(uri);
            //    if (response.IsSuccessStatusCode)
            //    {
            //        var message = await response.Content.ReadAsStringAsync();
            //        message = JsonConvert.DeserializeObject<string>(message);
            //        return message;
            //    }
            //    return "";
            //}
        }

        public async Task<string> isExistCode(string programCode)
        {



            string item = "";
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("programCode", programCode);
            //#################
            IEnumerable<Claim> claims = await APIResult.getList(urimainpath + "isExistCode", parameters);

            foreach (Claim c in claims)
            {
                if (c.Type == "scopes")
                {
                    item = c.Value;
                    break;
                }
            }

            return item;
            //string obj="" ;

            //  HttpResponseMessage response = new HttpResponseMessage();
            //  using (var client = new HttpClient())
            //  {

            //      Uri uri = new Uri(Global.APIUri + urimainpath + "isExistCode?programCode=" + programCode);

            //      response = await ApiConnect.ApiGetConnect(uri);
            //      if (response.IsSuccessStatusCode)
            //      {
            //          var jsonString = await response.Content.ReadAsStringAsync();
            //          obj = JsonConvert.DeserializeObject<string>(jsonString);
            //          return obj;
            //      }

            //      return obj;
            //  }
        }



    }
}
