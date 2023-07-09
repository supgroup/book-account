using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

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
using BookAccountApp.Classes;

namespace BookAccountApp.ApiClasses
{
    public class GroupObject
    {
        public int id { get; set; }
        public Nullable<int> groupId { get; set; }
        public Nullable<int> objectId { get; set; }
        public Nullable<int> parentObjectId { get; set; }
        public string parentObjectName { get; set; }
        public string notes { get; set; }
        public Nullable<byte> addOb { get; set; }
        public Nullable<byte> updateOb { get; set; }
        public Nullable<byte> deleteOb { get; set; }
        public Nullable<byte> showOb { get; set; }
        public Nullable<byte> reportOb { get; set; }
        public Nullable<byte> levelOb { get; set; }
        public string objectName { get; set; }
        public string objectType { get; set; }
        public string desc { get; set; }
        public Nullable<System.DateTime> createDate { get; set; }
        public Nullable<System.DateTime> updateDate { get; set; }
        public Nullable<int> createUserId { get; set; }
        public Nullable<int> updateUserId { get; set; }
        public Nullable<int> isActive { get; set; }
        public Boolean canDelete { get; set; }
        public string GroupName { get; set; }
        private string urimainpath = "GroupObject/";
        public async Task<List<GroupObject>> GetAll()
        {

            List<GroupObject> list = new List<GroupObject>();
            //  Dictionary<string, string> parameters = new Dictionary<string, string>();
            //parameters.Add("mainBranchId", mainBranchId.ToString());
            //parameters.Add("userId", userId.ToString());
            //parameters.Add("date", date.ToString());
            //#################
            IEnumerable<Claim> claims = await APIResult.getList(urimainpath + "Get");

            foreach (Claim c in claims)
            {
                if (c.Type == "scopes")
                {
                    list.Add(JsonConvert.DeserializeObject<GroupObject>(c.Value, new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy" }));
                }
            }
            return list;

        }

   
        public async Task<decimal> Save(GroupObject newObject)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            string method = urimainpath + "Save";

            var myContent = JsonConvert.SerializeObject(newObject);
            parameters.Add("Object", myContent);
           return await APIResult.post(method, parameters);


        }

       
        public async Task<GroupObject> GetByID(int valId)
        {
            GroupObject item = new GroupObject();
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("Id", valId.ToString());
            //#################
            IEnumerable<Claim> claims = await APIResult.getList(urimainpath + "GetByID", parameters);

            foreach (Claim c in claims)
            {
                if (c.Type == "scopes")
                {
                    item = JsonConvert.DeserializeObject<GroupObject>(c.Value, new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy" });
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

        public async Task<decimal> AddGroupObjectList(List<GroupObject> newlist)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            var myContent = JsonConvert.SerializeObject(newlist);
            parameters.Add("Object", myContent);
           

            string method = urimainpath + "AddGroupObjectList";
           return await APIResult.post(method, parameters);


        }

        public async Task<List<GroupObject>> GetByGroupId(int groupId)
        {
            List<GroupObject> list = new List<GroupObject>();
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("groupId", groupId.ToString());
            //#################
            IEnumerable<Claim> claims = await APIResult.getList(urimainpath + "GetByGroupId", parameters);

            foreach (Claim c in claims)
            {
                if (c.Type == "scopes")
                {
                    list.Add(JsonConvert.DeserializeObject<GroupObject>(c.Value, new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy" }));
                }
            }
            return list;


        }

        //
        public async Task<List<GroupObject>> GetUserpermission(int userId)
        {

            List<GroupObject> list = new List<GroupObject>();
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("userId", userId.ToString());
            //#################
            IEnumerable<Claim> claims = await APIResult.getList(urimainpath + "GetUserpermission", parameters);

            foreach (Claim c in claims)
            {
                if (c.Type == "scopes")
                {
                    list.Add(JsonConvert.DeserializeObject<GroupObject>(c.Value, new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy" }));
                }
            }
            return list;

        }


        public bool HasPermission(string objectname, List<GroupObject> GOList)
        {

            List<GroupObject> currlist = new List<GroupObject>();
            currlist = GetObjSons(objectname, GOList);
            currlist = currlist.Where(X => (X.addOb == 1 || X.updateOb == 1 || X.deleteOb == 1 || X.showOb == 1 || X.reportOb == 1)).ToList();
            if (currlist != null)
            {
                if (currlist.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }


        }

        //
        private List<GroupObject> objlist = new List<GroupObject>();
        //

        public List<GroupObject> GetObjSons(string objectName, List<GroupObject> GOList)
        {
            objlist = new List<GroupObject>();
            List<GroupObject> opl = new List<GroupObject>();

            // objlist;
            GroupObject firstelement = new GroupObject();

            firstelement = GOList.Where(X => X.objectName == objectName).FirstOrDefault();
            //  firstelement.objectId = objectId;
            if (firstelement != null)
            {
                objlist.Add(firstelement);
                SonsofObject(objlist, GOList);
                return (objlist);
            }
            else
            {
                return opl;
            }

        }

        private void SonsofObject(List<GroupObject> objlist1, List<GroupObject> mainobjlist)
        {

            List<GroupObject> templist = new List<GroupObject>();
            List<GroupObject> templist2 = new List<GroupObject>();
            if (objlist1.Count > 0)
            {
                foreach (GroupObject row in objlist1.ToList())
                {
                    //   templist = null;
                    if (row != null)
                    {
                        templist = mainobjlist.Where(X => X.parentObjectId == row.objectId).ToList();
                        /*
                                                    templist = (from O in mainobjlist.ToList()
                                                            where O.parentObjectId == row.objectId

                                                            select new GroupObject
                                                            {
                                                                objectId = O.objectId,
                                                                parentObjectId = O.parentObjectId,
                                                            }

                                                            ).ToList();
                                                            */

                    }

                    if (templist.Count > 0)
                    {
                        objlist.AddRange(templist);
                        templist2.AddRange(templist);
                    }


                }
                if (templist2.Count > 0)
                {
                    SonsofObject(templist2, mainobjlist);
                }


            }


        }

        // get groupObject row by objectName

        public GroupObject GetGObjByObjName(string objectName, List<GroupObject> GOList)
        {
       
            GroupObject element = new GroupObject();
   
            element = GOList.Where(X => X.objectName == objectName).FirstOrDefault();

                return element;
        }
        //

        public bool HasPermissionAction(string objectname, List<GroupObject> GOList, string type)
        {
            if (HelpClass.isAdminPermision())
                return true;

            bool hasPermission = false;
           
            GroupObject groupObject =  GetGObjByObjName(objectname, GOList);
            if (groupObject != null)
            if (type == "add" && groupObject.addOb == 1)
                     hasPermission = true;
            else if (type == "update" && groupObject.updateOb == 1)
                hasPermission = true;
            else if (type == "delete" && groupObject.deleteOb == 1)
                hasPermission = true;
            else if (type == "show" && groupObject.showOb == 1)
                hasPermission = true;
            else if (type == "report" && groupObject.reportOb == 1)
                hasPermission = true;
            else if (type == "one" && groupObject.showOb == 1)
                hasPermission = true;

            return hasPermission;
        }


    }
}

