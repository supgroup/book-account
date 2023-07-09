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
    
    public class CountryPackageDate
    {
        public int Id { get; set; }
        public Nullable<int> countryId { get; set; }
        public string countryName { get; set; }
        public string currency { get; set; }
        public Nullable<int> packageId { get; set; }
        public int monthCount { get; set; }
        public int yearCount { get; set; }
        public Nullable<decimal> price { get; set; }
        public string notes { get; set; }
        public Nullable<int> createUserId { get; set; }
        public Nullable<int> updateUserId { get; set; }
        public Nullable<System.DateTime> createDate { get; set; }
        public Nullable<System.DateTime> updateDate { get; set; }
        public bool islimitDate { get; set; }
        public Nullable<int> isActive { get; set; }
        public bool canDelete { get; set; }

        private string urimainpath = "countryPackageDate/";
        /// <summary>
        /// ///////////////////////////////////////
        /// </summary>
        /// <returns></returns>
        /// 

        public async Task<List<CountryPackageDate>> GetAll()
        {

            List<CountryPackageDate> list = new List<CountryPackageDate>();
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
                    list.Add(JsonConvert.DeserializeObject<CountryPackageDate>(c.Value, new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy" }));
                }
            }
            return list;



        }

        public async Task<decimal> Save(CountryPackageDate obj)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            string method = urimainpath + "Save";

            var myContent = JsonConvert.SerializeObject(obj);
            parameters.Add("Object", myContent);
            return await APIResult.post(method, parameters);


        }

        public async Task<CountryPackageDate> GetByID(int Id)
        {
            CountryPackageDate item = new CountryPackageDate();
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("Id", Id.ToString());
            //#################
            IEnumerable<Claim> claims = await APIResult.getList(urimainpath + "GetByID", parameters);

            foreach (Claim c in claims)
            {
                if (c.Type == "scopes")
                {
                    item = JsonConvert.DeserializeObject<CountryPackageDate>(c.Value, new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy" });
                    break;
                }
            }

            return item;


        }

        public async Task<decimal> Delete(int Id, int userId, bool final)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("Id", Id.ToString());
            parameters.Add("userId", userId.ToString());
            parameters.Add("final", final.ToString());

            string method = urimainpath + "Delete";
            return await APIResult.post(method, parameters);


        }

        public async Task<List<CountryPackageDate>> GetByCustomerPackId(int customerId,int packageId)
        {
            List<CountryPackageDate> list = new List<CountryPackageDate>();
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            parameters.Add("customerId", customerId.ToString());
            parameters.Add("packageId", packageId.ToString());
            //#################
            IEnumerable<Claim> claims = await APIResult.getList(urimainpath + "GetByCustomerPackId", parameters);

            foreach (Claim c in claims)
            {
                if (c.Type == "scopes")
                {
                    list.Add(JsonConvert.DeserializeObject<CountryPackageDate>(c.Value, new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy" }));
                }
            }
            return list;
        }


    }
}
