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
    public class Versions
    {

        public int verId { get; set; }
        public int programId { get; set; }
        public string name { get; set; }
        public string details { get; set; }
        public int isActive { get; set; }
        public string versionscode { get; set; }
        public Nullable<System.DateTime> createDate { get; set; }
        public Nullable<System.DateTime> updateDate { get; set; }
        public Nullable<int> createUserId { get; set; }
        public Nullable<int> updateUserId { get; set; }
        public bool canDelete { get; set; }
        public string programName { get; set; }
        public string notes { get; set; }
        
        private string urimainpath = "versions/";
        /// <summary>
        /// ///////////////////////////////////////
        /// </summary>
        /// <returns></returns>
        /// 

        public async Task<List<Versions>> GetAll()
        {

            List<Versions> list = new List<Versions>();

            IEnumerable<Claim> claims = await APIResult.getList(urimainpath + "GetAll");

            foreach (Claim c in claims)
            {
                if (c.Type == "scopes")
                {
                    list.Add(JsonConvert.DeserializeObject<Versions>(c.Value, new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy" }));
                }
            }
            return list;

            //List<Versions> memberships = null;

            //HttpResponseMessage response = new HttpResponseMessage();
            //using (var client = new HttpClient())
            //{
            //    Uri uri = new Uri(Global.APIUri + urimainpath + "GetAll");
            //    response = await ApiConnect.ApiGetConnect(uri);

            //    response = await ApiConnect.ApiGetConnect(uri);
            //    if (response.IsSuccessStatusCode)
            //    {
            //        var jsonString = await response.Content.ReadAsStringAsync();

            //        memberships = JsonConvert.DeserializeObject<List<Versions>>(jsonString);

            //        return memberships;
            //    }
            //    else //web api sent error response 
            //    {
            //        memberships = new List<Versions>();
            //    }
            //    return memberships;
            //}

        }

        public async Task<decimal> Save(Versions obj)
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

        public async Task<Versions> GetByID(int verId)
        {

            Versions item = new Versions();
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("verId", verId.ToString());
            //#################
            IEnumerable<Claim> claims = await APIResult.getList(urimainpath + "GetByID", parameters);

            foreach (Claim c in claims)
            {
                if (c.Type == "scopes")
                {
                    item = JsonConvert.DeserializeObject<Versions>(c.Value, new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy" });
                    break;
                }
            }


            return item;

            //Versions obj = new Versions();

            //HttpResponseMessage response = new HttpResponseMessage();
            //using (var client = new HttpClient())
            //{

            //    Uri uri = new Uri(Global.APIUri + urimainpath + "GetByID?verId=" + verId);

            //    response = await ApiConnect.ApiGetConnect(uri);
            //    if (response.IsSuccessStatusCode)
            //    {
            //       var jsonString = await response.Content.ReadAsStringAsync();
            //        obj = JsonConvert.DeserializeObject<Versions>(jsonString);
            //        return obj;
            //    }

            //    return obj;
            //}
        }

        public async Task<decimal> Delete(int verId, int userId, bool final)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("verId", verId.ToString());
            parameters.Add("userId", userId.ToString());
            parameters.Add("final", final.ToString());

            string method = urimainpath + "Delete";
            return await APIResult.post(method, parameters);


            //HttpResponseMessage response = new HttpResponseMessage();
            //using (var client = new HttpClient())
            //{
            //    Uri uri = new Uri(Global.APIUri + urimainpath + "Delete?verId=" + verId + "&userId=" + userId + "&final=" + final);
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

        public async Task<string> generateCodeNumber(int programId)
        {

            int sequence = await GetLastNumOfCode(programId);
            sequence++;
            string strSeq = sequence.ToString();
            if (sequence <= 999)
                strSeq = sequence.ToString().PadLeft(3, '0');
            string transNum = "V" + "-" + strSeq;
            return transNum;
        }


        public async Task<int> GetLastNumOfCode(int programId)
        {

            int item = 0;
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("programId", programId.ToString());
            //#################
            IEnumerable<Claim> claims = await APIResult.getList(urimainpath + "GetLastNumOfCode", parameters);

            foreach (Claim c in claims)
            {
                if (c.Type == "scopes")
                {
                    item = int.Parse(c.Value);
                    break;
                }
            }
            return item;

            //HttpResponseMessage response = new HttpResponseMessage();
            //using (var client = new HttpClient())
            //{

            //    Uri uri = new Uri(Global.APIUri + urimainpath + "GetLastNumOfCode?programId=" + programId);

            //    response = await ApiConnect.ApiGetConnect(uri);
            //    if (response.IsSuccessStatusCode)
            //    {
            //        string message = await response.Content.ReadAsStringAsync();
            //        message = JsonConvert.DeserializeObject<string>(message);
            //        return int.Parse(message);
            //    }
            //    return 0;
            //}
        }




    }
}
