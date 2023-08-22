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
    public class FlightTable
    {
        public int flightTableId { get; set; }
        public string name { get; set; }
        public Nullable<bool> isActive { get; set; }
        public string notes { get; set; }
        public bool canDelete { get; set; }



        /// <summary>
        /// ///////////////////////////////////////
        /// </summary>
        /// <returns></returns>
        /// 
        public async Task<List<FlightTable>> GetAll()
        {

            List<FlightTable> List = new List<FlightTable>();
            bool canDelete = false;
            try
            {
                using (bookdbEntities entity = new bookdbEntities())
                {
                    List = (from S in entity.flightTable
                            select new FlightTable()
                            {
                                flightTableId = S.flightTableId,
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

        public async Task<decimal> Save(FlightTable newitem)
        {
            flightTable newObject = new flightTable();
            newObject = JsonConvert.DeserializeObject<flightTable>(JsonConvert.SerializeObject(newitem));

            decimal message = 0;
            if (newObject != null)
            {
               


                try
                {
                    using (bookdbEntities entity = new bookdbEntities())
                    {
                        var locationEntity = entity.Set<flightTable>();
                        if (newObject.flightTableId == 0)
                        {
                          


                            locationEntity.Add(newObject);
                            entity.SaveChanges();
                            message = newObject.flightTableId;
                        }
                        else
                        {
                            var tmpObject = entity.flightTable.Where(p => p.flightTableId == newObject.flightTableId).FirstOrDefault();
                           
                            tmpObject.name = newObject.name;
                            tmpObject.isActive = newObject.isActive;
                            tmpObject.notes = newObject.notes;




                            entity.SaveChanges();

                            message = tmpObject.flightTableId;
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
        public async Task<FlightTable> GetByID(int itemId)
        {


            FlightTable item = new FlightTable();
           

            FlightTable row = new FlightTable();
            try
            {
                using (bookdbEntities entity = new bookdbEntities())
                {
                    var list = entity.flightTable.ToList();
                    row = list.Where(u => u.flightTableId == itemId)
                     .Select(S => new FlightTable()
                     {
                         flightTableId = S.flightTableId,
                         name = S.name,
                         isActive = S.isActive,
                         notes = S.notes,

                     }).FirstOrDefault();
                    return row;
                }

            }
            catch (Exception ex)
            {
                row = new FlightTable();
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
                        flightTable objectDelete = entity.flightTable.Find(id);

                        entity.flightTable.Remove(objectDelete);
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
            //            flightTable objectDelete = entity.flightTable.Find(userId);

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
