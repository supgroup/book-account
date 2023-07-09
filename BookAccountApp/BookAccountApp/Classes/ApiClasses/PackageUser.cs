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
    public class SendDetail
    {
        public List<PosSerialSend> PosSerialSendList;

        public packagesSend packageSend;
    }

    public class PosSerialSend
    {

        public string serial { get; set; }
        public string posDeviceCode { get; set; }

        public Nullable<bool> isBooked { get; set; }
        public int isActive { get; set; }
        public string posName { get; set; }
        public string branchName { get; set; }
        public Nullable<int> posSettingId { get; set; }
        public Nullable<int> posId { get; set; }
        public bool unLimited { get; set; }
    }



    public class packagesSend
    {
        public Nullable<int> packageUserId { get; set; }

        public string packageName { get; set; }

        public int branchCount { get; set; }
        public int posCount { get; set; }
        public int userCount { get; set; }
        public int vendorCount { get; set; }
        public int customerCount { get; set; }
        public int itemCount { get; set; }
        public int salesInvCount { get; set; }
        public string programName { get; set; }
        public string verName { get; set; }
        public int isActive { get; set; }
        public string packageCode { get; set; }
        public int storeCount { get; set; }
        public Nullable<System.DateTime> endDate { get; set; }
        public bool islimitDate { get; set; }
        public Nullable<bool> isOnlineServer { get; set; }
        public string customerServerCode { get; set; }
        public string packageSaleCode { get; set; }
        public int monthCount { get; set; }
        public bool canRenew { get; set; }
        public bool isBooked { get; set; }

        public string isDemo { get; set; }
        public Nullable<System.DateTime> bookDate { get; set; }

        public Nullable<System.DateTime> expireDate { get; set; }


        public string type { get; set; }
        public bool isPayed { get; set; }

        public Nullable<System.DateTime> activatedate { get; set; }
        public bool isServerActivated { get; set; }
        public int totalsalesInvCount { get; set; }
        public int result { get; set; }

        public string packageNumber { get; set; }



        public Nullable<int> pId { get; set; }
        public Nullable<int> pcdId { get; set; }//package country id

        public string activeState { get; set; }
        public string activeres { get; set; }

        public string customerName { get; set; }// 6- customer Name
        public string customerLastName { get; set; }// 6- customer LastName
        public string agentName { get; set; }// 5- Agent name 
        public string agentAccountName { get; set; }//5- Agent AccountName
        public string agentLastName { get; set; }//5- Agent LastName

        public Nullable<System.DateTime> pocrDate { get; set; }
        public Nullable<int> poId { get; set; }
        public string notes { get; set; }
        public string upnum { get; set; }
        public string packuserType { get; set; }
        public string activeApp { get; set; }

    }

    public class PackageUser
    {
        public int packageUserId { get; set; }
        public Nullable<int> packageId { get; set; }
        public Nullable<int> userId { get; set; }
        public string packageSaleCode { get; set; }
        public string packageNumber { get; set; }
        public Nullable<int> customerId { get; set; }
        public string customerServerCode { get; set; }
        public bool isBooked { get; set; }
        public string notes { get; set; }
        public Nullable<System.DateTime> createDate { get; set; }
        public Nullable<System.DateTime> updateDate { get; set; }
        public Nullable<int> createUserId { get; set; }
        public Nullable<int> updateUserId { get; set; }
        public Nullable<System.DateTime> bookDate { get; set; }
        public Nullable<int> isActive { get; set; }
        public Nullable<System.DateTime> expireDate { get; set; }
        public string userName { get; set; }
        public string userLastName { get; set; }
        public string customerName { get; set; }
        public string customerLastName { get; set; }
        public bool canDelete { get; set; }
        public Nullable<int> countryPackageId { get; set; }
        public bool canRenew { get; set; }
        public Nullable<bool> isOnlineServer { get; set; }
        public int oldPackageId { get; set; }
        public string type { get; set; }
        public bool isPayed { get; set; }
        public int salesInvCount { get; set; }
        public int monthCount { get; set; }
        public Nullable<System.DateTime> activatedate { get; set; }
        public bool isServerActivated { get; set; }
        public Nullable<int> oldCountryPackageId { get; set; }
        public string packageName { get; set; }
        public bool islimitDate { get; set; }
        public Nullable<decimal> price { get; set; }
        public string currency { get; set; }
        public string programName { get; set; }
        public string verName { get; set; }
        public int branchCount { get; set; }
        public int programId { get; set; }
        public int verId { get; set; }
        public string confirmStat { get; set; }
        public Nullable<int> canChngSer { get; set; }
        public string isDemo { get; set; }

        private string urimainpath = "packageUser/";
        /// <summary>
        /// ///////////////////////////////////////
        /// </summary>
        /// <returns></returns>
        /// 

        public async Task<List<PackageUser>> GetAll()
        {

            List<PackageUser> list = new List<PackageUser>();
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
                    list.Add(JsonConvert.DeserializeObject<PackageUser>(c.Value, new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy" }));
                }
            }
            return list;

            //List<PackageUser> memberships = null;

            //HttpResponseMessage response = new HttpResponseMessage();
            //using (var client = new HttpClient())
            //{
            //    Uri uri = new Uri(Global.APIUri + urimainpath + "GetAll");
            //    response = await ApiConnect.ApiGetConnect(uri);

            //    response = await ApiConnect.ApiGetConnect(uri);
            //    if (response.IsSuccessStatusCode)
            //    {
            //        var jsonString = await response.Content.ReadAsStringAsync();

            //        memberships = JsonConvert.DeserializeObject<List<PackageUser>>(jsonString);

            //        return memberships;
            //    }
            //    else //web api sent error response 
            //    {
            //        memberships = new List<PackageUser>();
            //    }
            //    return memberships;
            //}

        }

        public async Task<decimal> Save(PackageUser obj)
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

        public async Task<decimal> MultiSave(PackageUser obj, int count)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            string method = urimainpath + "MultiSave";

            var myContent = JsonConvert.SerializeObject(obj);
            parameters.Add("count", count.ToString());
            parameters.Add("Object", myContent);

            return await APIResult.post(method, parameters);

            //var myContent = JsonConvert.SerializeObject(obj);
            //myContent = HttpUtility.UrlEncode(myContent);
            //Uri uri = new Uri(Global.APIUri + urimainpath + "MultiSave?Object=" + myContent+ "&count="+ count);

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

        public async Task<PackageUser> GetByID(int packageUserId)
        {
            PackageUser item = new PackageUser();
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("packageUserId", packageUserId.ToString());
            //#################
            IEnumerable<Claim> claims = await APIResult.getList(urimainpath + "GetByID", parameters);

            foreach (Claim c in claims)
            {
                if (c.Type == "scopes")
                {
                    item = JsonConvert.DeserializeObject<PackageUser>(c.Value, new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy" });
                    break;
                }
            }

            return item;

            //PackageUser obj = new PackageUser();

            //HttpResponseMessage response = new HttpResponseMessage();
            //using (var client = new HttpClient())
            //{

            //    Uri uri = new Uri(Global.APIUri + urimainpath + "GetByID?packageUserId=" + packageUserId);

            //    response = await ApiConnect.ApiGetConnect(uri);
            //    if (response.IsSuccessStatusCode)
            //    {
            //       var jsonString = await response.Content.ReadAsStringAsync();
            //        obj = JsonConvert.DeserializeObject<PackageUser>(jsonString);
            //        return obj;
            //    }

            //    return obj;
            //}
        }

        public async Task<decimal> Delete(int packageUserId, int userId, bool final)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("packageUserId", packageUserId.ToString());
            parameters.Add("userId", userId.ToString());
            parameters.Add("final", final.ToString());

            string method = urimainpath + "Delete";
            return await APIResult.post(method, parameters);

            //HttpResponseMessage response = new HttpResponseMessage();
            //using (var client = new HttpClient())
            //{
            //    Uri uri = new Uri(Global.APIUri + urimainpath + "Delete?packageUserId=" + packageUserId + "&userId=" + userId + "&final=" + final);
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


        public async Task<SendDetail> Activateserver(string packageSaleCode, string customerServerCode)
        {
            SendDetail item = new SendDetail();
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("packageSaleCode", packageSaleCode);
            parameters.Add("customerServerCode", customerServerCode);

            //#################
            IEnumerable<Claim> claims = await APIResult.getList(urimainpath + "ActivateServer", parameters);

            foreach (Claim c in claims)
            {
                if (c.Type == "scopes")
                {
                   item = JsonConvert.DeserializeObject<SendDetail>(c.Value, new JsonSerializerSettings { DateParseHandling = DateParseHandling.None });
               
                }
            }
            return item;

        }
        public async Task<int> GetLastNum(string Code, int agentId)
        {
            int count = 0;
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("Code", Code);
            parameters.Add("agentId", agentId.ToString());
            //#################
            IEnumerable<Claim> claims = await APIResult.getList("packageUser/GetLastNum", parameters);

            foreach (Claim c in claims)
            {
                if (c.Type == "scopes")
                {
                    count = int.Parse(c.Value);
                    break;
                }
            }
            return count;
        }
        public async Task<string> generateNumber(string packageCode, string agentCode, int agentId)
        {
            int sequence = await GetLastNum(packageCode, agentId);
            sequence++;
            string strSeq = sequence.ToString();
            if (sequence <= 999999)
                strSeq = sequence.ToString().PadLeft(6, '0');
            string invoiceNum = packageCode + "-" + agentCode + "-" + strSeq;
            return invoiceNum;
        }


        public async Task<decimal> packageBook(PackageUser obj)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            string method = urimainpath + "packageBook";

            var myContent = JsonConvert.SerializeObject(obj);
            parameters.Add("Object", myContent);

            return await APIResult.post(method, parameters);

      
        }
        public async Task<decimal> packagePay(PackageUser obj, PayOp payobj)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            string method = urimainpath + "packagePay";

            var myContent = JsonConvert.SerializeObject(obj);
            parameters.Add("Object", myContent);
            var myContentPay = JsonConvert.SerializeObject(payobj);
            parameters.Add("PayObject", myContentPay);

            return await APIResult.post(method, parameters);


        }

        public async Task<List<PackageUser>> GetByCustomerId(int customerId)
        {
            List<PackageUser> list = new List<PackageUser>();
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            parameters.Add("customerId", customerId.ToString());

            //#################
            IEnumerable<Claim> claims = await APIResult.getList(urimainpath + "GetByCustomerId", parameters);

            foreach (Claim c in claims)
            {
                if (c.Type == "scopes")
                {
                    list.Add(JsonConvert.DeserializeObject<PackageUser>(c.Value, new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy" }));
                }
            }
            return list;

        }


        public async Task<SendDetail> ActivateServerOffline(int packageUserId, string activeState)
        {
            SendDetail item = new SendDetail();
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("packageUserId", packageUserId.ToString());
            parameters.Add("activeState", activeState);

            //#################
            IEnumerable<Claim> claims = await APIResult.getList(urimainpath + "ActivateServerOffline", parameters);

            foreach (Claim c in claims)
            {
                if (c.Type == "scopes")
                {
                    item = JsonConvert.DeserializeObject<SendDetail>(c.Value, new JsonSerializerSettings { DateParseHandling = DateParseHandling.None });

                }
            }
            return item;

        }


        public async Task<int> updatecustomerdata(SendDetail SendDetaildata, string activeState)
        {
            int item = 0;
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            var myContent3 = JsonConvert.SerializeObject(SendDetaildata);
            parameters.Add("object", myContent3);
            parameters.Add("activeState", activeState);
            //#################
            IEnumerable<Claim> claims = await APIResult.getList(urimainpath + "updatecustomerdata", parameters);

            foreach (Claim c in claims)
            {
                if (c.Type == "scopes")
                {
                    item = int.Parse(c.Value);
                }
            }
            return item;
        }


    }
}
