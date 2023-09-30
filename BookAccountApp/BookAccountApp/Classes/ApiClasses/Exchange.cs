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
    public class Exchange
    {
        public int exchangeId { get; set; }
        public Nullable<decimal> syValue { get; set; }
        public Nullable<System.DateTime> createDate { get; set; }
        public Nullable<bool> isActive { get; set; }
        public string notes { get; set; }
        public Nullable<int> createUserId { get; set; }
        public bool canDelete { get; set; }
        /// <summary>
        /// ///////////////////////////////////////
        /// </summary>
        /// <returns></returns>
        /// 
        public async Task<List<Exchange>> GetAll()
        {

            List<Exchange> List = new List<Exchange>();
            bool canDelete = false;
            try
            {
                using (bookdbEntities entity = new bookdbEntities())
                {
                    List = (from S in entity.exchange
                            select new Exchange()
                            {
                                exchangeId = S.exchangeId,
                                syValue = S.syValue,
                                createDate = S.createDate,
                                isActive = S.isActive,
                                notes = S.notes,
                                createUserId = S.createUserId,

                            }).ToList();


                    if (List.Count > 0)
                    {
                        for (int i = 0; i < List.Count; i++)
                        {
                            if (List[i].isActive == true)
                            {
                                int itemId = (int)List[i].exchangeId;
                                var itemsI = entity.serviceData.Where(x => x.exchangeId == itemId).Select(b => new { b.exchangeId }).FirstOrDefault();

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
        public async Task<int> check ()
        {
            int count = 0;
            List<Exchange> List = new List<Exchange>();
        
            try
            {
                using (bookdbEntities entity = new bookdbEntities())
                {
                    List = (from S in entity.exchange
                            select new Exchange()
                            {
                                exchangeId = S.exchangeId,
                             

                            }).ToList();
                    if (List!=null)
                    {
                        if (List.Count()>0)
                        {
                            count = List.Count();
                        }

                    }
                  
 
                    return count;
                }

            }
            catch
            {
                return count;
            }
        }
        public async Task<decimal> Save(Exchange newitem)
        {
            exchange newObject = new exchange();
            newObject = JsonConvert.DeserializeObject<exchange>(JsonConvert.SerializeObject(newitem));

            decimal message = 0;
            if (newObject != null)
            {
                
                if (newObject.createUserId == 0 || newObject.createUserId == null)
                {
                    Nullable<int> id = null;
                    newObject.createUserId = id;
                }
           


                try
                {
                    using (bookdbEntities entity = new bookdbEntities())
                    {
                        var locationEntity = entity.Set<exchange>();
                        if (newObject.exchangeId == 0)
                        {
                            newObject.createDate = DateTime.Now;
                          
                            newObject.isActive = true;

                            locationEntity.Add(newObject);
                            entity.SaveChanges();
                            message = newObject.exchangeId;
                        }
                        else
                        {
                            var tmpObject = entity.exchange.Where(p => p.exchangeId == newObject.exchangeId).FirstOrDefault();

                            tmpObject.createDate = DateTime.Now;
                            tmpObject.exchangeId = newObject.exchangeId;
                            tmpObject.syValue = newObject.syValue;
                         
                            tmpObject.isActive = newObject.isActive;
                            tmpObject.notes = newObject.notes;
                            tmpObject.createUserId = newObject.createUserId;

                            entity.SaveChanges();

                            message = tmpObject.exchangeId;
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
        public async Task<Exchange> GetByID(int itemId)
        {


            Exchange item = new Exchange();
           

            Exchange row = new Exchange();
            try
            {
                using (bookdbEntities entity = new bookdbEntities())
                {
                    var list = entity.exchange.ToList();
                    row = list.Where(u => u.exchangeId == itemId)
                     .Select(S => new Exchange()
                     {
                         exchangeId = S.exchangeId,
                         syValue = S.syValue,
                         createDate = S.createDate,
                         isActive = S.isActive,
                         notes = S.notes,
                         createUserId = S.createUserId,

                     }).FirstOrDefault();
                    return row;
                }

            }
            catch (Exception ex)
            {
                row = new Exchange();
                //userrow.name = ex.ToString();
                return row;
            }
        }
        public async Task<Exchange> Getlast()
        {
            Exchange row = new Exchange();
            try
            {
                using (bookdbEntities entity = new bookdbEntities())
                {
                    var list = entity.exchange.ToList();
                    row = list.Where(u => u.exchangeId == entity.exchange.Max(x => x.exchangeId))
                     .Select(S => new Exchange()
                     {
                         exchangeId = S.exchangeId,
                         syValue = S.syValue,
                         createDate = S.createDate,
                         isActive = S.isActive,
                         notes = S.notes,
                         createUserId = S.createUserId,
                     }).FirstOrDefault();
                    return row;
                }

            }
            catch (Exception ex)
            {
                row = new Exchange();
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
                        exchange objectDelete = entity.exchange.Find(id);

                        entity.exchange.Remove(objectDelete);
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
                        exchange objectDelete = entity.exchange.Find(id);

                        objectDelete.isActive = false;
                     
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

        
    }
}
