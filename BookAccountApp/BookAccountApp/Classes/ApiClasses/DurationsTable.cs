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
    public class DurationsTable
    {
        public int durationId { get; set; }
        public string name { get; set; }
        public Nullable<bool> isActive { get; set; }
        public string notes { get; set; }

        public bool canDelete { get; set; }



        /// <summary>
        /// ///////////////////////////////////////
        /// </summary>
        /// <returns></returns>
        /// 
        public async Task<List<DurationsTable>> GetAll()
        {

            List<DurationsTable> List = new List<DurationsTable>();
            bool canDelete = false;
            try
            {
                using (bookdbEntities entity = new bookdbEntities())
                {
                    List = (from S in entity.durationsTable
                            select new DurationsTable()
                            {
                                durationId = S.durationId,
                                name = S.name,
                                isActive = S.isActive,
                                notes = S.notes,


                                canDelete = true,

                            }).ToList();

                    //if (List.Count > 0)
                    //{
                    //    for (int i = 0; i < List.Count; i++)
                    //    {
                    //        if (List[i].isActive == 1)
                    //        {
                    //            int userId = (int)List[i].userId;
                    //            var itemsI = entity.packageUser.Where(x => x.userId == userId).Select(b => new { b.userId }).FirstOrDefault();

                    //            if ((itemsI is null))
                    //                canDelete = true;
                    //        }
                    //        List[i].canDelete = canDelete;
                    //    }
                    //}
                    return List;
                }

            }
            catch
            {
                return List;
            }
        }

        public async Task<decimal> Save(DurationsTable newitem)
        {
            durationsTable newObject = new durationsTable();
            newObject = JsonConvert.DeserializeObject<durationsTable>(JsonConvert.SerializeObject(newitem));

            decimal message = 0;
            if (newObject != null)
            {
               


                try
                {
                    using (bookdbEntities entity = new bookdbEntities())
                    {
                        var locationEntity = entity.Set<durationsTable>();
                        if (newObject.durationId == 0)
                        {
                          


                            locationEntity.Add(newObject);
                            entity.SaveChanges();
                            message = newObject.durationId;
                        }
                        else
                        {
                            var tmpObject = entity.durationsTable.Where(p => p.durationId == newObject.durationId).FirstOrDefault();
                   
                            tmpObject.name = newObject.name;
                            tmpObject.isActive = newObject.isActive;
                            tmpObject.notes = newObject.notes;



                            entity.SaveChanges();

                            message = tmpObject.durationId;
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
        public async Task<DurationsTable> GetByID(int itemId)
        {


            DurationsTable item = new DurationsTable();
           

            DurationsTable row = new DurationsTable();
            try
            {
                using (bookdbEntities entity = new bookdbEntities())
                {
                    var list = entity.durationsTable.ToList();
                    row = list.Where(u => u.durationId == itemId)
                     .Select(S => new DurationsTable()
                     {
                         durationId = S.durationId,
                         name = S.name,
                         isActive = S.isActive,
                         notes = S.notes,

                     }).FirstOrDefault();
                    return row;
                }

            }
            catch (Exception ex)
            {
                row = new DurationsTable();
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
                        durationsTable objectDelete = entity.durationsTable.Find(id);

                        entity.durationsTable.Remove(objectDelete);
                        message = entity.SaveChanges();
                        return message;

                    }
                }
                catch
                {
                    return 0;

                }
            }
            return message;
            //else
            //{
            //    try
            //    {
            //        using (bookdbEntities entity = new bookdbEntities())
            //        {
            //            durationsTable objectDelete = entity.durationsTable.Find(userId);

            //            objectDelete.isActive = 0;
            //            objectDelete.updateUserId = signuserId;
            //        objectDelete.updateDate = DateTime.Now;
            //            message = entity.SaveChanges() ;

            //            return message;
            //        }
            //    }
            //    catch
            //    {
            //        return 0;
            //    }
            //}

        }

         
    }
}
