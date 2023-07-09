using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using BookAccountApp;
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
    class Group
    {
        public int groupId { get; set; }
        public string name { get; set; }
        public string notes { get; set; }
        public Nullable<System.DateTime> createDate { get; set; }
        public Nullable<System.DateTime> updateDate { get; set; }
        public Nullable<int> createUserId { get; set; }
        public Nullable<int> updateUserId { get; set; }
        public Nullable<int> isActive { get; set; }
        public Boolean canDelete { get; set; }

        private string urimainpath = "Group/";

        public async Task<List<Group>> GetAll()
        {
            List<Group> list = new List<Group>();
            IEnumerable<Claim> claims = await APIResult.getList(urimainpath + "Get");

            foreach (Claim c in claims)
            {
                if (c.Type == "scopes")
                {
                    list.Add(JsonConvert.DeserializeObject<Group>(c.Value, new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy" }));
                }
            }
            return list;
        }
         

        // get users by groupId
        public async Task<List<Users>> GetUsersByGroupId(int groupId)
        {

            List<Users> list = new List<Users>();
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("groupId", groupId.ToString());
            //#################
            IEnumerable<Claim> claims = await APIResult.getList(urimainpath + "GetUsersByGroupId", parameters);

            foreach (Claim c in claims)
            {
                if (c.Type == "scopes")
                {
                    list.Add(JsonConvert.DeserializeObject<Users>(c.Value, new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy" }));
                }
            }
            return list;

            
        }
        public async Task<decimal> Save(Group newObject)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            string method = urimainpath + "Save";

            var myContent = JsonConvert.SerializeObject(newObject);
            parameters.Add("Object", myContent);
           return await APIResult.post(method, parameters);

            
        }

     

        public async Task<decimal> Delete(int groupId, int userId, bool final)
        {

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("groupId", groupId.ToString());
            parameters.Add("userId", userId.ToString());
            parameters.Add("final", final.ToString());
            string method = urimainpath + "Delete";
           return await APIResult.post(method, parameters);
        }

        public async Task<decimal> UpdateGroupIdInUsers(int groupId, List<int> newList, int userId)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            var myContent = JsonConvert.SerializeObject(newList);
            parameters.Add("newList", myContent);
            parameters.Add("groupId", groupId.ToString());
            parameters.Add("userId", userId.ToString());

            string method = urimainpath + "UpdateGroupIdInUsers";
           return await APIResult.post(method, parameters);
        }
    }
}

