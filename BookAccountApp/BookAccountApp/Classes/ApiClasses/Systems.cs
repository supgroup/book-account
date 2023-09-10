using BookAccountApp.Classes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
    public class Systems
    {
        public int systemId { get; set; }
        public string name { get; set; }
        public string notes { get; set; }
        public Nullable<bool> isActive { get; set; }
        public Nullable<System.DateTime> createDate { get; set; }
        public Nullable<System.DateTime> updateDate { get; set; }
        public Nullable<int> createUserId { get; set; }
        public Nullable<int> updateUserId { get; set; }
        public Nullable<decimal> company_commission { get; set; }
        public bool canDelete { get; set; }

        /// <summary>
        /// ///////////////////////////////////////
        /// </summary>
        /// <returns></returns>
        /// 
        public async Task<List<Systems>> GetAll()
        {

            List<Systems> List = new List<Systems>();
            bool canDelete = false;
            try
            {
                using (bookdbEntities entity = new bookdbEntities())
                {
                    List = (from S in entity.systems
                            select new Systems()
                            {
                                systemId = S.systemId,
                                name = S.name,
                                notes = S.notes,
                                isActive = S.isActive,
                                createDate = S.createDate,
                                updateDate = S.updateDate,
                                createUserId = S.createUserId,
                                updateUserId = S.updateUserId,
                                company_commission = S.company_commission,

                                canDelete = false,
                              
                            }).ToList();


                    if (List.Count > 0)
                    {
                        for (int i = 0; i < List.Count; i++)
                        {
                            if (List[i].isActive == true)
                            {
                                int itemId = (int)List[i].systemId;
                         var itemsI = entity.serviceData.Where(x => x.systemId == itemId).Select(b => new { b.systemId }).FirstOrDefault();
                           var items2 = entity.serviceData.Where(x => x.officeSystem.systemId == itemId).Select(b => new { b.serviceId }).FirstOrDefault();

                                if (itemsI is null && items2 is null)
                                {                                    
                                    List[i].canDelete = true;
                                }
                                  
                            }
                            
                        }
                    }
                    return List;
                }

            }
            catch
            {
                return List;
            }
        }

        public async Task<decimal> Save(Systems newitem)
        {
            systems newObject = new systems();
            newObject = JsonConvert.DeserializeObject<systems>(JsonConvert.SerializeObject(newitem));

            decimal message = 0;
            if (newObject != null)
            {
                if (newObject.updateUserId == 0 || newObject.updateUserId == null)
                {
                    Nullable<int> id = null;
                    newObject.updateUserId = id;
                }
                if (newObject.createUserId == 0 || newObject.createUserId == null)
                {
                    Nullable<int> id = null;
                    newObject.createUserId = id;
                }
           


                try
                {
                    using (bookdbEntities entity = new bookdbEntities())
                    {
                        var locationEntity = entity.Set<systems>();
                        if (newObject.systemId == 0)
                        {
                            newObject.createDate = DateTime.Now;
                            newObject.updateDate = newObject.createDate;
                            newObject.updateUserId = newObject.createUserId;
                            newObject.isActive = true;

                            locationEntity.Add(newObject);
                            entity.SaveChanges();
                            message = newObject.systemId;
                            OfficeSystem osmodel = new OfficeSystem();
                          decimal   res= await osmodel.AddBySysId(newObject.systemId);

                        }
                        else
                        {
                            var tmpObject = entity.systems.Where(p => p.systemId == newObject.systemId).FirstOrDefault();
                            OfficeSystem osmodel = new OfficeSystem();
                            int res = await osmodel.updateListisActive(tmpObject.systemId,(bool)newObject.isActive, "systems");

                            tmpObject.updateDate = DateTime.Now;

                            //tmpObject.systemId = newObject.systemId;
                            tmpObject.name = newObject.name;
                            tmpObject.notes = newObject.notes;
                            tmpObject.isActive = newObject.isActive;
                            //tmpObject.createDate = newObject.createDate;
                            //tmpObject.updateDate = newObject.updateDate;
                            //tmpObject.createUserId = newObject.createUserId;
                            tmpObject.updateUserId = newObject.updateUserId;
                            tmpObject.company_commission = newObject.company_commission;

                            entity.SaveChanges();

                            message = tmpObject.systemId;
                        }
                    }
                    return message;
                }
                catch
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }
        public async Task<Systems> GetByID(int itemId)
        {


            Systems item = new Systems();
           

            Systems row = new Systems();
            try
            {
                using (bookdbEntities entity = new bookdbEntities())
                {
                    var list = entity.systems.ToList();
                    row = list.Where(u => u.systemId == itemId)
                     .Select(S => new Systems()
                     {
                         systemId = S.systemId,
                         name = S.name,
                         notes = S.notes,
                         isActive = S.isActive,
                         createDate = S.createDate,
                         updateDate = S.updateDate,
                         createUserId = S.createUserId,
                         updateUserId = S.updateUserId,
                         company_commission = S.company_commission,

                     }).FirstOrDefault();
                    return row;
                }

            }
            catch (Exception ex)
            {
                row = new Systems();
                //userrow.name = ex.ToString();
                return row;
            }
        }
        public async Task<decimal> Delete(int id, int signuserId, bool final)
        {

            decimal message = 0;
            if (final)
            {
                try
                {
                    using (bookdbEntities entity = new bookdbEntities())
                    {
                        systems objectDelete = entity.systems.Find(id);

                        entity.systems.Remove(objectDelete);
                        message = entity.SaveChanges();
                        return message;

                    }
                }
                catch
                {
                    return 0;

                }
            }
            else
            {
                try
                {
                    OfficeSystem osmodel = new OfficeSystem();
                    using (bookdbEntities entity = new bookdbEntities())
                    {
                        systems objectDelete = entity.systems.Find(id);
                        int res = await osmodel.updateListisActive(id, false, "systems");
                        objectDelete.isActive = false;
                        objectDelete.updateUserId = signuserId;
                        objectDelete.updateDate = DateTime.Now;
                        message = entity.SaveChanges();
              
                        return message;
                    }
                }
                catch
                {
                    return 0;
                }
            }

        }

        //public async Task<string> generateCodeNumber(string type)
        //{
        //    int sequence = await GetLastNumOfCode(type);
        //    sequence++;
        //    string strSeq = sequence.ToString();
        //    if (sequence <= 999999)
        //        strSeq = sequence.ToString().PadLeft(6, '0');
        //    string transNum = type.ToUpper() + "-" + strSeq;
        //    return transNum;
        //}

        //public async Task<int> GetLastNumOfCode(string type)
        //{

        //    try
        //    {
        //        List<string> numberList;
        //        int lastNum = 0;
        //        using (bookdbEntities entity = new bookdbEntities())
        //        {
        //            numberList = entity.systems.Where(b => b.nu.Contains(type + "-")).Select(b => b.serviceNum).ToList();

        //            for (int i = 0; i < numberList.Count; i++)
        //            {
        //                string code = numberList[i];
        //                string s = code.Substring(code.LastIndexOf("-") + 1);
        //                numberList[i] = s;
        //            }
        //            if (numberList.Count > 0)
        //            {
        //                numberList.Sort();
        //                lastNum = int.Parse(numberList[numberList.Count - 1]);
        //            }
        //        }

        //        return lastNum;
        //    }
        //    catch
        //    {
        //        return 0;
        //    }
        //}

    }
}
