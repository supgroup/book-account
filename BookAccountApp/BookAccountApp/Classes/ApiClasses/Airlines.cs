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
    public class Airlines
    {
        public int airlineId { get; set; }
        public string name { get; set; }
        public string notes { get; set; }
        public Nullable<bool> isActive { get; set; }
        public bool canDelete { get; set; }



        /// <summary>
        /// ///////////////////////////////////////
        /// </summary>
        /// <returns></returns>
        /// 
        public async Task<List<Airlines>> GetAll()
        {

            List<Airlines> List = new List<Airlines>();
            bool canDelete = false;
            try
            {
                using (bookdbEntities entity = new bookdbEntities())
                {
                    List = (from S in entity.airlines
                            select new Airlines()
                            {
                                airlineId = S.airlineId,
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

        public async Task<decimal> Save(Airlines newitem)
        {
            airlines newObject = new airlines();
            newObject = JsonConvert.DeserializeObject<airlines>(JsonConvert.SerializeObject(newitem));

            decimal message = 0;
            if (newObject != null)
            {
               


                try
                {
                    using (bookdbEntities entity = new bookdbEntities())
                    {
                        var locationEntity = entity.Set<airlines>();
                        if (newObject.airlineId == 0)
                        {
                          


                            locationEntity.Add(newObject);
                            entity.SaveChanges();
                            message = newObject.airlineId;
                        }
                        else
                        {
                            var tmpObject = entity.airlines.Where(p => p.airlineId == newObject.airlineId).FirstOrDefault();
                           
                            tmpObject.name = newObject.name;
                            tmpObject.isActive = newObject.isActive;
                            tmpObject.notes = newObject.notes;




                            entity.SaveChanges();

                            message = tmpObject.airlineId;
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
        public async Task<Airlines> GetByID(int itemId)
        {


            Airlines item = new Airlines();
           

            Airlines row = new Airlines();
            try
            {
                using (bookdbEntities entity = new bookdbEntities())
                {
                    var list = entity.airlines.ToList();
                    row = list.Where(u => u.airlineId == itemId)
                     .Select(S => new Airlines()
                     {
                         airlineId = S.airlineId,
                         name = S.name,
                         isActive = S.isActive,
                         notes = S.notes,

                     }).FirstOrDefault();
                    return row;
                }

            }
            catch (Exception ex)
            {
                row = new Airlines();
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
                        airlines objectDelete = entity.airlines.Find(id);

                        entity.airlines.Remove(objectDelete);
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
            //            airlines objectDelete = entity.airlines.Find(userId);

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
