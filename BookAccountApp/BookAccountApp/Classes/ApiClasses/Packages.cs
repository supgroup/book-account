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
    public class Packages
    {

        public int packageId { get; set; }
        public string packageCode { get; set; }
        public string packageName { get; set; }
        public string details { get; set; }
        public int branchCount { get; set; }
        public int posCount { get; set; }
        public int userCount { get; set; }
        public int vendorCount { get; set; }
        public int customerCount { get; set; }
        public int itemCount { get; set; }
        public int salesInvCount { get; set; }
        public int storeCount { get; set; }
        public Nullable<int> programId { get; set; }
        public string programName { get; set; }
        public Nullable<int> verId { get; set; }
        public string verName { get; set; }
        public int isActive { get; set; }
        public Nullable<System.DateTime> createDate { get; set; }
        public Nullable<System.DateTime> updateDate { get; set; }
        public Nullable<int> createUserId { get; set; }
        public Nullable<int> updateUserId { get; set; }
        public bool canDelete { get; set; }
        public string notes { get; set; }
        public string isDemo { get; set; }

        private string urimainpath = "packages/";
        /// <summary>
        /// ///////////////////////////////////////
        /// </summary>
        /// <returns></returns>
        /// 
        public async Task<List<Packages>> GetAll()
        {
            List<Packages> list = new List<Packages>();
            //  Dictionary<string, string> parameters = new Dictionary<string, string>();
            //parameters.Add("mainBranchId", mainBranchId.ToString());
            //parameters.Add("userId", userId.ToString());
            //parameters.Add("date", date.ToString());
            //#################
            IEnumerable<Claim> claims = await APIResult.getList(urimainpath + "GetAll");

            foreach (Claim c in claims)
            {
                if (c.Type == "scopes")
                {
                    list.Add(JsonConvert.DeserializeObject<Packages>(c.Value, new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy" }));
                }
            }
            return list;

            //List<Packages> memberships = null;

            //HttpResponseMessage response = new HttpResponseMessage();
            //using (var client = new HttpClient())
            //{
            //    Uri uri = new Uri(Global.APIUri + urimainpath + "GetAll");
            //    response = await ApiConnect.ApiGetConnect(uri);

            //    response = await ApiConnect.ApiGetConnect(uri);
            //    if (response.IsSuccessStatusCode)
            //    {
            //        var jsonString = await response.Content.ReadAsStringAsync();

            //        memberships = JsonConvert.DeserializeObject<List<Packages>>(jsonString);

            //        return memberships;
            //    }
            //    else //web api sent error response 
            //    {
            //        memberships = new List<Packages>();
            //    }
            //    return memberships;
            //}

        }

        public async Task<decimal> Save(Packages obj)
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

        public async Task<Packages> GetByID(int packageId)
        {
            Packages item = new Packages();
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("packageId", packageId.ToString());
            //#################
            IEnumerable<Claim> claims = await APIResult.getList(urimainpath + "GetByID", parameters);

            foreach (Claim c in claims)
            {
                if (c.Type == "scopes")
                {
                    item = JsonConvert.DeserializeObject<Packages>(c.Value, new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy" });
                    break;
                }
            }

            return item;

            //Packages obj = new Packages();

            //HttpResponseMessage response = new HttpResponseMessage();
            //using (var client = new HttpClient())
            //{

            //    Uri uri = new Uri(Global.APIUri + urimainpath + "GetByID?packageId=" + packageId);

            //    response = await ApiConnect.ApiGetConnect(uri);
            //    if (response.IsSuccessStatusCode)
            //    {
            //       var jsonString = await response.Content.ReadAsStringAsync();
            //        obj = JsonConvert.DeserializeObject<Packages>(jsonString);
            //        return obj;
            //    }

            //    return obj;
            //}
        }

        public async Task<decimal> Delete(int packageId, int userId, bool final)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("packageId", packageId.ToString());
            parameters.Add("userId", userId.ToString());
            parameters.Add("final", final.ToString());

            string method = urimainpath + "Delete";
            return await APIResult.post(method, parameters);

            //HttpResponseMessage response = new HttpResponseMessage();
            //using (var client = new HttpClient())
            //{
            //    Uri uri = new Uri(Global.APIUri + urimainpath + "Delete?packageId=" + packageId + "&userId=" + userId + "&final=" + final);
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

        public async Task<string> isExistCode(string packageCode)
        {

            string  item ="";
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("packageCode", packageCode);
            //#################
            IEnumerable<Claim> claims = await APIResult.getList(urimainpath + "isExistCode", parameters);

            foreach (Claim c in claims)
            {
                if (c.Type == "scopes")
                {
                    item =c.Value;
                    break;
                }
            }

            return item;

            //string obj = "";

            //HttpResponseMessage response = new HttpResponseMessage();
            //using (var client = new HttpClient())
            //{

            //    Uri uri = new Uri(Global.APIUri + urimainpath + "isExistCode?packageCode=" + packageCode);

            //    response = await ApiConnect.ApiGetConnect(uri);
            //    if (response.IsSuccessStatusCode)
            //    {
            //        var jsonString = await response.Content.ReadAsStringAsync();
            //        obj = JsonConvert.DeserializeObject<string>(jsonString);
            //        return obj;
            //    }

            //    return obj;
            //}
        }
      
        /// ////
   
        public async Task<List<Packages>> GetPackagesByAgent(int agentId)
        {
            List<Packages> list = new List<Packages>();
            Dictionary<string, string> parameters = new Dictionary<string, string>();
           
            parameters.Add("userId", agentId.ToString());
       
            //#################
            IEnumerable<Claim> claims = await APIResult.getList(urimainpath + "GetPackagesByAgent", parameters);

            foreach (Claim c in claims)
            {
                if (c.Type == "scopes")
                {
                    list.Add(JsonConvert.DeserializeObject<Packages>(c.Value, new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy" }));
                }
            }
            return list;

         

        }

        public async Task<List<Packages>> GetByAgentCountryId(int agentId)
        {
            List<Packages> list = new List<Packages>();
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            parameters.Add("userId", agentId.ToString());

            //#################
            IEnumerable<Claim> claims = await APIResult.getList(urimainpath + "GetByAgentCountryId", parameters);

            foreach (Claim c in claims)
            {
                if (c.Type == "scopes")
                {
                    list.Add(JsonConvert.DeserializeObject<Packages>(c.Value, new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy" }));
                }
            }
            return list;



        }

        public async Task<List<Packages>> GetByCustomerCountry(int customerId)
        {
            List<Packages> list = new List<Packages>();
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            parameters.Add("customerId", customerId.ToString());

            //#################
            IEnumerable<Claim> claims = await APIResult.getList(urimainpath + "GetByCustomerCountry", parameters);

            foreach (Claim c in claims)
            {
                if (c.Type == "scopes")
                {
                    list.Add(JsonConvert.DeserializeObject<Packages>(c.Value, new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy" }));
                }
            }
            return list;



        }

    }
}
