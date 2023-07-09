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

    public class PosSerialsUpdate
    {

        public int serialId { get; set; }
    
        public int isActive { get; set; }

   

    }
        public class PosSerials
    {

        public int serialId { get; set; }
        public string serial { get; set; }
        public string posDeviceCode { get; set; }
        public Nullable<int> packageUserId { get; set; }
        public Nullable<bool> isBooked { get; set; }
        public int isActive { get; set; }
        public Nullable<System.DateTime> createDate { get; set; }
        public Nullable<System.DateTime> updateDate { get; set; }
      
        public Nullable<int> createUserId { get; set; }
        public Nullable<int> updateUserId { get; set; }
        public bool canDelete { get; set; }
        public string notes { get; set; }
        public string packageSaleCode { get; set; }

        //info
        public Nullable<int> posInfoId { get; set; }
        public string posName { get; set; }
        public string branchName { get; set; }
        public Nullable<System.DateTime> updateDateinfo { get; set; }


        private string urimainpath = "posSerials/";

        /// <summary>
        /// ///////////////////////////////////////
        /// </summary>
        /// <returns></returns>
        /// 

        public async Task<List<PosSerials>> GetAll()
        {
            List<PosSerials> list = new List<PosSerials>();
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
                  
                     list.Add(JsonConvert.DeserializeObject<PosSerials>(c.Value, new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy" }));
                }
            }
            return list;

            //List<PosSerials> memberships = null;

            //HttpResponseMessage response = new HttpResponseMessage();
            //using (var client = new HttpClient())
            //{
            //    Uri uri = new Uri(Global.APIUri + urimainpath + "GetAll");
            //    response = await ApiConnect.ApiGetConnect(uri);

            //    response = await ApiConnect.ApiGetConnect(uri);
            //    if (response.IsSuccessStatusCode)
            //    {
            //        var jsonString = await response.Content.ReadAsStringAsync();

            //        memberships = JsonConvert.DeserializeObject<List<PosSerials>>(jsonString);

            //        return memberships;
            //    }
            //    else //web api sent error response 
            //    {
            //        memberships = new List<PosSerials>();
            //    }
            //    return memberships;
            //}

        }

        public async Task<decimal> Save(PosSerials obj)
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

        public async Task<PosSerials> GetByID(int serialId)
        {

            PosSerials item = new PosSerials();
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("serialId", serialId.ToString());
            //#################
            IEnumerable<Claim> claims = await APIResult.getList(urimainpath + "GetByID", parameters);

            foreach (Claim c in claims)
            {
                if (c.Type == "scopes")
                {
                    item = JsonConvert.DeserializeObject<PosSerials>(c.Value, new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy" });
                    break;
                }
            }

            return item;

            //PosSerials obj = new PosSerials();

            //HttpResponseMessage response = new HttpResponseMessage();
            //using (var client = new HttpClient())
            //{

            //    Uri uri = new Uri(Global.APIUri + urimainpath + "GetByID?serialId=" + serialId);

            //    response = await ApiConnect.ApiGetConnect(uri);
            //    if (response.IsSuccessStatusCode)
            //    {
            //       var jsonString = await response.Content.ReadAsStringAsync();
            //        obj = JsonConvert.DeserializeObject<PosSerials>(jsonString);
            //        return obj;
            //    }

            //    return obj;
            //}
        }
        //GetBySID

        //public async Task<PosSerials> GetBySID(int serialId)
        //{

        //    PosSerials item = new PosSerials();
        //    Dictionary<string, string> parameters = new Dictionary<string, string>();
        //    parameters.Add("serialId", serialId.ToString());
        //    //#################
        //    IEnumerable<Claim> claims = await APIResult.getList(urimainpath + "GetBySID", parameters);

        //    foreach (Claim c in claims)
        //    {
        //        if (c.Type == "scopes")
        //        {
        //            item = JsonConvert.DeserializeObject<PosSerials>(c.Value, new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy" });
        //            break;
        //        }
        //    }

        //    return item;

          
        //}
        public async Task<decimal> Delete(int serialId, int userId, bool final)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("serialId", serialId.ToString());
            parameters.Add("userId", userId.ToString());
            parameters.Add("final", final.ToString());

            string method = urimainpath + "Delete";
            return await APIResult.post(method, parameters);

            //HttpResponseMessage response = new HttpResponseMessage();
            //using (var client = new HttpClient())
            //{
            //    Uri uri = new Uri(Global.APIUri + urimainpath + "Delete?serialId=" + serialId + "&userId=" + userId + "&final=" + final);
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


        public async Task<List<PosSerials>> GetByPackUserId(int packageUserId)
        {

            List<PosSerials> list = new List<PosSerials>();
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("packageUserId", packageUserId.ToString());
            //#################
            IEnumerable<Claim> claims = await APIResult.getList(urimainpath + "GetByPackUserId", parameters);

            foreach (Claim c in claims)
            {
                if (c.Type == "scopes")
                {

                    list.Add(JsonConvert.DeserializeObject<PosSerials>(c.Value, new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy" }));
                }
            }

            return list;

        }

        public async Task<List<PosSerials>> GetSerialAndPosInfo(int packageUserId)
        {

            List<PosSerials> list = new List<PosSerials>();
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("packageUserId", packageUserId.ToString());
            //#################
            IEnumerable<Claim> claims = await APIResult.getList(urimainpath + "GetSerialAndPosInfo", parameters);

            foreach (Claim c in claims)
            {
                if (c.Type == "scopes")
                {

                    list.Add(JsonConvert.DeserializeObject<PosSerials>(c.Value, new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy" }));
                }
            }

            return list;

        }

        public async Task<int> UpdateList(List<PosSerialsUpdate> newList, int userId, int packageUserId)
        {

            //Dictionary<string, string> parameters = new Dictionary<string, string>();
            //parameters.Add("userId", userId.ToString());

            //var list = JsonConvert.SerializeObject(newList);
            //parameters.Add("newlistobject", list);
            ////#################
            //string method = urimainpath + "UpdateList";
            //int res = await APIResult.post(method, parameters);
            
            //return res;


            int res = 0;
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("userId", userId.ToString());
            parameters.Add("packageUserId", packageUserId.ToString());
            var list = JsonConvert.SerializeObject(newList);
            parameters.Add("newlistobject", list);
            //#################
            IEnumerable<Claim> claims = await APIResult.getList(urimainpath + "UpdateList", parameters);

            foreach (Claim c in claims)
            {
                if (c.Type == "scopes")
                {
                    res = int.Parse(c.Value);

                }
            }

            return res;

        }

    }
}
