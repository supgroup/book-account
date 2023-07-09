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
    public class PayOp
    {

        public int payOpId { get; set; }
        public decimal Price { get; set; }
        public string code { get; set; }
        public string type { get; set; }
        public Nullable<int> packageUserId { get; set; }
        public Nullable<int> createUserId { get; set; }
        public Nullable<int> updateUserId { get; set; }
        public Nullable<System.DateTime> createDate { get; set; }
        public Nullable<System.DateTime> updateDate { get; set; }
        public string notes { get; set; }
        public bool canDelete { get; set; }
        public decimal discountValue { get; set; }
        public Nullable<int> agentId { get; set; }
        public Nullable<int> customerId { get; set; }
        public Nullable<int> countryPackageId { get; set; }
        public decimal totalnet { get; set; }
        public string packageNumber { get; set; }//bookNum
        public Nullable<System.DateTime> expireDate { get; set; }//expireDate
        public string currency { get; set; }
        public Nullable<int> packageId { get; set; }

        public string packageName { get; set; }

        private string urimainpath = "PayOp/";
        /// <summary>
        /// ///////////////////////////////////////
        /// </summary>
        /// <returns></returns>
        /// 
        public async Task<List<PayOp>> GetAll()
        {
            List<PayOp> list = new List<PayOp>();
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
                    list.Add(JsonConvert.DeserializeObject<PayOp>(c.Value, new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy" }));
                }
            }
            return list;


        }

        public async Task<decimal> Save(PayOp obj)
        {

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            string method = urimainpath + "Save";

            var myContent = JsonConvert.SerializeObject(obj);
            parameters.Add("Object", myContent);
            return await APIResult.post(method, parameters);

  
        }

        public async Task<PayOp> GetByID(int payOpId)
        {
            PayOp item = new PayOp();
       
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("payOpId", payOpId.ToString());
            //#################
            IEnumerable<Claim> claims = await APIResult.getList(urimainpath + "GetByID", parameters);

            foreach (Claim c in claims)
            {
                if (c.Type == "scopes")
                {
                    item = JsonConvert.DeserializeObject<PayOp>(c.Value, new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy" });
                    break;
                }
            }

            return item;

           
        }

        public async Task<PayOp> getLastPayOp(int packageUserId)
        {
            PayOp item = new PayOp();

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("packageUserId", packageUserId.ToString());
            //#################
            IEnumerable<Claim> claims = await APIResult.getList(urimainpath + "getLastPayOp", parameters);

            foreach (Claim c in claims)
            {
                if (c.Type == "scopes")
                {
                    item = JsonConvert.DeserializeObject<PayOp>(c.Value, new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy" });
                    break;
                }
            }

            return item;


        }

        public async Task<List<PayOp>> GetByCustomerId(int customerId)
        {
           List<PayOp> list = new List<PayOp>();
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("customerId", customerId.ToString());
            //#################
            IEnumerable<Claim> claims = await APIResult.getList(urimainpath + "GetByCustomerId", parameters);

            foreach (Claim c in claims)
            {
                if (c.Type == "scopes")
                {
                    list.Add(JsonConvert.DeserializeObject<PayOp>(c.Value, new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy" }));
                }
            }
            return list;

        }

        public async Task<decimal> Delete(int packageId, int userId, bool final)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("packageId", packageId.ToString());
            parameters.Add("userId", userId.ToString());
            parameters.Add("final", final.ToString());

            string method = urimainpath + "Delete";
            return await APIResult.post(method, parameters);
        }

        public async Task<int> GetLastNum(string Code)
        {
            int count = 0;
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("payOpCode", Code);
            //#################
            IEnumerable<Claim> claims = await APIResult.getList("PayOp/GetLastNum", parameters);

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

        public async Task<string> generateNumber(string payOpCode)
        {
            int sequence = await GetLastNum(payOpCode);
            sequence++;
            string strSeq = sequence.ToString();
            if (sequence <= 999999)
                strSeq = sequence.ToString().PadLeft(6, '0');
            string payOpNum = payOpCode + "-" + strSeq;
            return payOpNum;
        }


        /// ////



    }
}
