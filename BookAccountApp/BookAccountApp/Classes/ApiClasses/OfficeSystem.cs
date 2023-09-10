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
using System.Data.Entity.Migrations;

using System.Security.Claims;
using Newtonsoft.Json.Converters;

namespace BookAccountApp.ApiClasses
{
    public class OfficeSystem
    {
        public int osId { get; set; }
        public Nullable<int> officeId { get; set; }
        public Nullable<int> systemId { get; set; }
        public Nullable<decimal> office_commission { get; set; }
        public Nullable<bool> isActive { get; set; }
        public string notes { get; set; }
        public bool canDelete { get; set; }
        public string systemName { get; set; }
        public string officeName { get; set; }


        /// <summary>
        /// ///////////////////////////////////////
        /// </summary>
        /// <returns></returns>
        /// 
        public async Task<List<OfficeSystem>> GetAll()
        {

            List<OfficeSystem> List = new List<OfficeSystem>();
            bool canDelete = false;
            try
            {
                using (bookdbEntities entity = new bookdbEntities())
                {
                    List = (from S in entity.officeSystem
                            select new OfficeSystem()
                            {
                                osId = S.osId,
                                officeId = S.officeId,
                                systemId = S.systemId,
                                office_commission = S.office_commission,
                                isActive = S.isActive,
                                notes = S.notes,

                                canDelete = false,

                            }).ToList();


                    if (List.Count > 0)
                    {
                        for (int i = 0; i < List.Count; i++)
                        {
                            if (List[i].isActive == true)
                            {
                                int itemId = (int)List[i].osId;
                                var itemsI = entity.serviceData.Where(x => x.osId == itemId).Select(b => new { b.osId }).FirstOrDefault();

                                if (itemsI is null)
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

        public async Task<int> Save(OfficeSystem newitem)
        {
            officeSystem newObject = new officeSystem();
            newObject = JsonConvert.DeserializeObject<officeSystem>(JsonConvert.SerializeObject(newitem));

            int message = 0;
            if (newObject != null)
            {
                if (newObject.officeId == 0 || newObject.officeId == null)
                {
                    Nullable<int> id = null;
                    newObject.officeId = id;
                }
                if (newObject.systemId == 0 || newObject.systemId == null)
                {
                    Nullable<int> id = null;
                    newObject.systemId = id;
                }



                try
                {
                    using (bookdbEntities entity = new bookdbEntities())
                    {
                        var locationEntity = entity.Set<officeSystem>();
                        if (newObject.osId == 0)
                        {

                            newObject.isActive = true;
                            locationEntity.Add(newObject);
                            entity.SaveChanges();
                            message = newObject.osId;
                        }
                        else
                        {
                            var tmpObject = entity.officeSystem.Where(p => p.osId == newObject.osId).FirstOrDefault();


                            //  tmpObject.osId = newObject.osId;
                            tmpObject.officeId = newObject.officeId;
                            tmpObject.systemId = newObject.systemId;
                            tmpObject.office_commission = newObject.office_commission;
                            tmpObject.isActive = newObject.isActive;
                            tmpObject.notes = newObject.notes;

                            entity.SaveChanges();

                            message = tmpObject.osId;
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


        public async Task<OfficeSystem> GetByID(int itemId)
        {


            OfficeSystem item = new OfficeSystem();


            OfficeSystem row = new OfficeSystem();
            try
            {
                using (bookdbEntities entity = new bookdbEntities())
                {
                    var list = entity.officeSystem.ToList();
                    row = list.Where(u => u.osId == itemId)
                     .Select(S => new OfficeSystem()
                     {
                         osId = S.osId,
                         officeId = S.officeId,
                         systemId = S.systemId,
                         office_commission = S.office_commission,
                         isActive = S.isActive,
                         notes = S.notes,

                     }).FirstOrDefault();
                    return row;
                }

            }
            catch (Exception ex)
            {
                row = new OfficeSystem();
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
                        officeSystem objectDelete = entity.officeSystem.Find(id);

                        entity.officeSystem.Remove(objectDelete);
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
                    using (bookdbEntities entity = new bookdbEntities())
                    {
                        officeSystem objectDelete = entity.officeSystem.Find(id);

                        objectDelete.isActive = false;
                        //objectDelete.updateUserId = signuserId;
                        //objectDelete.updateDate = DateTime.Now;
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
        public async Task<decimal> DeletebySysId(int systemId)
        {

            decimal message = 0;

            try
            {
                List<OfficeSystem> oslist = new List<OfficeSystem>();
                oslist = await GetbySysId(systemId);

                using (bookdbEntities entity = new bookdbEntities())
                {
                    if (oslist.Count > 0)
                    {
                        for (int i = 0; i < oslist.Count; i++)
                        {
                            if (oslist[i].isActive == true)
                            {
                                int itemId = (int)oslist[i].osId;
                                var itemsI = entity.serviceData.Where(x => x.osId == itemId).Select(b => new { b.osId }).FirstOrDefault();

                                if (itemsI is null)
                                {
                                    var itemsos = entity.officeSystem.Find(itemId);

                                    entity.officeSystem.Remove(itemsos);
                                    message = entity.SaveChanges();

                                }

                            }

                        }

                    }

                    return message;

                }
            }
            catch
            {
                return 0;

            }



        }
        public async Task<decimal> DeletebyOfficeId(int officeId)
        {

            decimal message = 0;

            try
            {
                List<OfficeSystem> oslist = new List<OfficeSystem>();
                oslist = await GetbyOfficeId(officeId);

                using (bookdbEntities entity = new bookdbEntities())
                {
                    if (oslist.Count > 0)
                    {
                        for (int i = 0; i < oslist.Count; i++)
                        {
                            if (oslist[i].isActive == true)
                            {
                                int itemId = (int)oslist[i].osId;
                                var itemsI = entity.serviceData.Where(x => x.osId == itemId).Select(b => new { b.osId }).FirstOrDefault();

                                if (itemsI is null)
                                {
                                    var itemsos = entity.officeSystem.Find(itemId);

                                    entity.officeSystem.Remove(itemsos);
                                    message = entity.SaveChanges();

                                }

                            }

                        }

                    }

                    return message;

                }
            }
            catch
            {
                return 0;

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
        //            numberList = entity.officeSystem.Where(b => b.nu.Contains(type + "-")).Select(b => b.serviceNum).ToList();

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

        public async Task<decimal> AddBySysId(int systemId)
        {


            decimal message = 0;
            if (systemId > 0)
            {



                try
                {
                    using (bookdbEntities entity = new bookdbEntities())
                    {
                        var officeIdList = entity.office.Select(o => o.officeId).ToList();
                        var tableEntity = entity.Set<officeSystem>();
                        if (officeIdList.Count > 0)
                        {

                            officeSystem officeSystemtemp = new officeSystem();
                            foreach (int officeId in officeIdList)
                            {
                                officeSystemtemp = new officeSystem();
                                officeSystemtemp.osId = 0;
                                officeSystemtemp.isActive = true;
                                // officeSystemtemp.notes = "";
                                officeSystemtemp.officeId = officeId;
                                officeSystemtemp.office_commission = 0;
                                officeSystemtemp.systemId = systemId;
                                tableEntity.Add(officeSystemtemp);
                                entity.SaveChanges();
                                message = officeSystemtemp.osId;
                            }

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

        public async Task<decimal> AddByOfficeId(int officeId)
        {
            decimal message = 0;
            if (officeId > 0)
            {

                try
                {
                    using (bookdbEntities entity = new bookdbEntities())
                    {
                        var systemIdList = entity.systems.Select(o => o.systemId).ToList();
                        var tableEntity = entity.Set<officeSystem>();
                        if (systemIdList.Count > 0)
                        {

                            officeSystem officeSystemtemp = new officeSystem();
                            foreach (int systemId in systemIdList)
                            {
                                officeSystemtemp = new officeSystem();
                                officeSystemtemp.osId = 0;
                                officeSystemtemp.isActive = true;
                                // officeSystemtemp.notes = "";
                                officeSystemtemp.officeId = officeId;
                                officeSystemtemp.office_commission = 0;
                                officeSystemtemp.systemId = systemId;
                                tableEntity.Add(officeSystemtemp);
                                entity.SaveChanges();
                                message = officeSystemtemp.osId;
                            }

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

        public async Task<List<OfficeSystem>> GetbySysId(int systemId)
        {

            List<OfficeSystem> List = new List<OfficeSystem>();
            bool canDelete = false;
            try
            {
                if (systemId > 0)
                {
                    using (bookdbEntities entity = new bookdbEntities())
                    {
                        List = (from S in entity.officeSystem
                                where S.systemId == systemId
                                select new OfficeSystem()
                                {
                                    osId = S.osId,
                                    officeId = S.officeId,
                                    systemId = S.systemId,
                                    office_commission = S.office_commission,
                                    systemName = S.systems.name,
                                    officeName = S.office.name,
                                    isActive = S.isActive,
                                    notes = S.notes,

                                    canDelete = false,

                                }).ToList();


                        //if (List.Count > 0)
                        //{
                        //    for (int i = 0; i < List.Count; i++)
                        //    {
                        //        if (List[i].isActive == true)
                        //        {
                        //            int itemId = (int)List[i].osId;
                        //            var itemsI = entity.serviceData.Where(x => x.osId == itemId).Select(b => new { b.osId }).FirstOrDefault();

                        //            if (itemsI is null)
                        //            {
                        //                List[i].canDelete = true;
                        //            }

                        //        }

                        //    }
                        //}
                        return List;
                    }
                }
                else
                {
                    return List;
                }


            }
            catch
            {
                return List;
            }


        }
        public async Task<List<OfficeSystem>> GetbyOfficeId(int officeId)
        {

            List<OfficeSystem> List = new List<OfficeSystem>();
            bool canDelete = false;
            try
            {
                if (officeId > 0)
                {
                    using (bookdbEntities entity = new bookdbEntities())
                    {
                        List = (from S in entity.officeSystem
                                where S.officeId == officeId
                                select new OfficeSystem()
                                {
                                    osId = S.osId,
                                    officeId = S.officeId,
                                    systemId = S.systemId,
                                    office_commission = S.office_commission,
                                    systemName = S.systems.name,
                                    officeName = S.office.name,
                                    isActive = S.isActive,
                                    notes = S.notes,

                                    canDelete = false,

                                }).ToList();


                        //if (List.Count > 0)
                        //{
                        //    for (int i = 0; i < List.Count; i++)
                        //    {
                        //        if (List[i].isActive == true)
                        //        {
                        //            int itemId = (int)List[i].osId;
                        //            var itemsI = entity.serviceData.Where(x => x.osId == itemId).Select(b => new { b.osId }).FirstOrDefault();

                        //            if (itemsI is null)
                        //            {
                        //                List[i].canDelete = true;
                        //            }

                        //        }

                        //    }
                        //}
                        return List;
                    }
                }
                else
                {
                    return List;
                }


            }
            catch
            {
                return List;
            }
        }

        public async Task<int> updateList(List<OfficeSystem> newList)
        {

            int message = 0;

            try
            {
                if (officeId > 0)
                {
                    foreach (OfficeSystem row in newList)
                    {
                        message = await Save(row);
                    }
                    return message;

                }
                else
                {
                    return 0;
                }


            }
            catch (Exception ex)
            {
                return message;
            }
        }
        public async Task<int> updateListisActive(int id,bool isActive,string table)
        {

            int message = 0;

            try
            {
                if (id > 0)
                {
                    using (bookdbEntities entity = new bookdbEntities())
                    {                  
                        if (table == "systems")
                        {
                            entity.officeSystem.Where(s => s.systemId == id).ToList().ForEach(x => x.isActive = isActive);
                            message= entity.SaveChanges();
                        }else if (table == "office")
                        {                            
                            entity.officeSystem.Where(s => s.officeId == id).ToList().ForEach(x => x.isActive = isActive);
                            message = entity.SaveChanges();                             
                        }
                    }
                 
                    return message;

                }
                else
                {
                    return 0;
                }


            }
            catch (Exception ex)
            {
                return message;
            }
        }
    }
}
