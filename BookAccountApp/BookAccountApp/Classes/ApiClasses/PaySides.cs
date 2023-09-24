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
    public class PaySides
    {
        public int paysideId { get; set; }
        public string side { get; set; }
        public string sideAr { get; set; }
        public string notes { get; set; }
        public Nullable<bool> isActive { get; set; }
        public string code { get; set; }
        public Nullable<decimal> balance { get; set; }
        public bool canDelete { get; set; }



        /// <summary>
        /// ///////////////////////////////////////
        /// </summary>
        /// <returns></returns>
        /// 
        public async Task<List<PaySides>> GetAll()
        {

            List<PaySides> List = new List<PaySides>();
            bool canDelete = false;
            try
            {
                using (bookdbEntities entity = new bookdbEntities())
                {
                    List = (from S in entity.paySides
                            select new PaySides()
                            {
                                paysideId = S.paysideId,
                                side = S.side,
                                sideAr = S.sideAr,
                                notes = S.notes,
                                isActive = S.isActive,
                                code = S.code,
                                balance=S.balance,

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

        public async Task<decimal> Save(PaySides newitem)
        {
            paySides newObject = new paySides();
            newObject = JsonConvert.DeserializeObject<paySides>(JsonConvert.SerializeObject(newitem));

            decimal message = 0;
            if (newObject != null)
            {
               


                try
                {
                    using (bookdbEntities entity = new bookdbEntities())
                    {
                        var locationEntity = entity.Set<paySides>();
                        if (newObject.paysideId == 0)
                        {

                            locationEntity.Add(newObject);
                            entity.SaveChanges();
                            message = newObject.paysideId;
                        }
                        else
                        {
                            var tmpObject = entity.paySides.Where(p => p.paysideId == newObject.paysideId).FirstOrDefault();
                           // tmpObject.paysideId = newObject.paysideId;
                            tmpObject.side = newObject.side;
                            tmpObject.sideAr = newObject.sideAr;
                            tmpObject.notes = newObject.notes;
                            tmpObject.isActive = newObject.isActive;
                            tmpObject.code = newObject.code;



                            entity.SaveChanges();

                            message = tmpObject.paysideId;
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
        public async Task<PaySides> GetByID(int itemId)
        {


            PaySides item = new PaySides();
           

            PaySides row = new PaySides();
            try
            {
                using (bookdbEntities entity = new bookdbEntities())
                {
                    var list = entity.paySides.ToList();
                    row = list.Where(u => u.paysideId == itemId)
                     .Select(S => new PaySides()
                     {
                         paysideId = S.paysideId,
                         side = S.side,
                         sideAr = S.sideAr,
                         notes = S.notes,
                         isActive = S.isActive,
                         code = S.code,

                     }).FirstOrDefault();
                    return row;
                }

            }
            catch (Exception ex)
            {
                row = new PaySides();
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
                        paySides objectDelete = entity.paySides.Find(id);

                        entity.paySides.Remove(objectDelete);
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
            //            paySides objectDelete = entity.paySides.Find(userId);

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

        public async Task<int> updateSideBalance(string  side,decimal cash)
        {

            paySides newObject = new paySides();

            int message = 0;
            try
                {
                    using (bookdbEntities entity = new bookdbEntities())
                    {
                        var locationEntity = entity.Set<paySides>();
                        
                            var tmpObject = entity.paySides.Where(p => p.code == side).FirstOrDefault();
                    decimal sum= (decimal)tmpObject.balance + cash;
                    if (sum<0)
                    {
                        return  -1;
                    }
                    else
                    {
                        tmpObject.balance = sum;
                        entity.SaveChanges();
                        message = tmpObject.paysideId;
                    }                       
                    }
                    return message;
                }
                catch
                {
                    return 0;
                }
           
        }
        public async Task<PaySides> GetByCode(string code)
        {
            PaySides row = new PaySides();
            try
            {
                using (bookdbEntities entity = new bookdbEntities())
                {
                    var list = entity.paySides.ToList();
                    row = list.Where(u => u.code == code)
                     .Select(S => new PaySides()
                     {
                         paysideId = S.paysideId,
                         side = S.side,
                         sideAr = S.sideAr,
                         notes = S.notes,
                         isActive = S.isActive,
                         code = S.code,
                        balance=S.balance,
                     }).FirstOrDefault();
                    return row;
                }

            }
            catch (Exception ex)
            {
                row = new PaySides();
                //userrow.name = ex.ToString();
                return row;
            }
        }
    }
}
