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
using BookAccountApp.Classes;

namespace BookAccountApp.ApiClasses
{
  
    public class AgentCustomer
    {
        public int agentCustomerId { get; set; }
        public Nullable<int> agentId { get; set; }
        public Nullable<int> customerId { get; set; }
        public string notes { get; set; }
        public Nullable<int> createUserId { get; set; }
        public Nullable<int> updateUserId { get; set; }
        public Nullable<System.DateTime> createDate { get; set; }
        public Nullable<System.DateTime> updateDate { get; set; }

        private string urimainpath = "agentCustomer/";
        /// <summary>
        /// ///////////////////////////////////////
        /// </summary>
        /// <returns></returns>
        /// 

        public async Task<List<AgentCustomer>> GetAll()
        {

            List<AgentCustomer> list = new List<AgentCustomer>();
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
                    list.Add(JsonConvert.DeserializeObject<AgentCustomer>(c.Value, new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy" }));
                }
            }
            return list;

           

        }

        public async Task<decimal> Save(AgentCustomer obj)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            string method = urimainpath + "Save";

            var myContent = JsonConvert.SerializeObject(obj);
            parameters.Add("Object", myContent);
            return await APIResult.post(method, parameters);

          
        }

        public async Task<AgentCustomer> GetByID(int agentCustomerId)
        {
            AgentCustomer item = new AgentCustomer();
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("agentCustomerId", agentCustomerId.ToString());
            //#################
            IEnumerable<Claim> claims = await APIResult.getList(urimainpath + "GetByID", parameters);

            foreach (Claim c in claims)
            {
                if (c.Type == "scopes")
                {
                    item = JsonConvert.DeserializeObject<AgentCustomer>(c.Value, new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy" });
                    break;
                }
            }

            return item;

           
        }

        public async Task<decimal> Delete(int agentCustomerId)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("agentCustomerId", agentCustomerId.ToString());
            //parameters.Add("userId", userId.ToString());
            //parameters.Add("final", final.ToString());

            string method = urimainpath + "Delete";
            return await APIResult.post(method, parameters);

          
        }


        public async Task<decimal> saveNewList(List<AgentCustomer> newList, int agentId)
        {

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("userId", agentId.ToString());


            var list = JsonConvert.SerializeObject(newList);
            parameters.Add("newlistobject", list);
            //#################
            string method = urimainpath + "saveNewList";
            decimal res = await APIResult.post(method, parameters);
            //  return await APIResult.post(method, parameters);
            return res;



        }


    }
}
