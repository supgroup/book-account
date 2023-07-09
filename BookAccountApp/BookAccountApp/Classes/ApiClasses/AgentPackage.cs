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
  
    public class AgentPackage
    {
        public int agentPackageId { get; set; }
        public Nullable<int> agentId { get; set; }
        public Nullable<int> packageId { get; set; }
        public string notes { get; set; }
        public Nullable<int> createUserId { get; set; }
        public Nullable<int> updateUserId { get; set; }
        public Nullable<System.DateTime> createDate { get; set; }
        public Nullable<System.DateTime> updateDate { get; set; }

        private string urimainpath = "agentPackage/";
        /// <summary>
        /// ///////////////////////////////////////
        /// </summary>
        /// <returns></returns>
        /// 

        public async Task<List<AgentPackage>> GetAll()
        {

            List<AgentPackage> list = new List<AgentPackage>();
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
                    list.Add(JsonConvert.DeserializeObject<AgentPackage>(c.Value, new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy" }));
                }
            }
            return list;

           

        }

        public async Task<decimal> Save(AgentPackage obj)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            string method = urimainpath + "Save";

            var myContent = JsonConvert.SerializeObject(obj);
            parameters.Add("Object", myContent);
            return await APIResult.post(method, parameters);

          
        }

        public async Task<AgentPackage> GetByID(int agentPackageId)
        {
            AgentPackage item = new AgentPackage();
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("agentPackageId", agentPackageId.ToString());
            //#################
            IEnumerable<Claim> claims = await APIResult.getList(urimainpath + "GetByID", parameters);

            foreach (Claim c in claims)
            {
                if (c.Type == "scopes")
                {
                    item = JsonConvert.DeserializeObject<AgentPackage>(c.Value, new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy" });
                    break;
                }
            }

            return item;

           
        }

        public async Task<decimal> Delete(int agentPackageId)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("agentPackageId", agentPackageId.ToString());
            //parameters.Add("userId", userId.ToString());
            //parameters.Add("final", final.ToString());

            string method = urimainpath + "Delete";
            return await APIResult.post(method, parameters);

          
        }


        public async Task<decimal> saveNewList(List<AgentPackage> newList, int agentId)
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
