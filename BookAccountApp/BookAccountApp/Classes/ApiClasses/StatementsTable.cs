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
    public class StatementsTable
    {
        public int opStatementId { get; set; }
        public string name { get; set; }
        public Nullable<bool> isActive { get; set; }
        public string notes { get; set; }
        public bool canDelete { get; set; }
      


        /// <summary>
        /// ///////////////////////////////////////
        /// </summary>
        /// <returns></returns>
        /// 
        public async Task<List<StatementsTable>> GetAll()
        {

            List<StatementsTable> List = new List<StatementsTable>();
            bool canDelete = false;
            try
            {
                using (bookdbEntities entity = new bookdbEntities())
                {
                    List = (from S in entity.statementsTable
                            select new StatementsTable()
                            {
                                opStatementId = S.opStatementId,
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

        public async Task<decimal> Save(StatementsTable newitem)
        {
            statementsTable newObject = new statementsTable();
            newObject = JsonConvert.DeserializeObject<statementsTable>(JsonConvert.SerializeObject(newitem));

            decimal message = 0;
            if (newObject != null)
            {
               


                try
                {
                    using (bookdbEntities entity = new bookdbEntities())
                    {
                        var locationEntity = entity.Set<statementsTable>();
                        if (newObject.opStatementId == 0)
                        {
                          


                            locationEntity.Add(newObject);
                            entity.SaveChanges();
                            message = newObject.opStatementId;
                        }
                        else
                        {
                            var tmpObject = entity.statementsTable.Where(p => p.opStatementId == newObject.opStatementId).FirstOrDefault();
                           
                            tmpObject.name = newObject.name;
                            tmpObject.isActive = newObject.isActive;
                            tmpObject.notes = newObject.notes;




                            entity.SaveChanges();

                            message = tmpObject.opStatementId;
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
        public async Task<StatementsTable> GetByID(int itemId)
        {


            StatementsTable item = new StatementsTable();
           

            StatementsTable row = new StatementsTable();
            try
            {
                using (bookdbEntities entity = new bookdbEntities())
                {
                    var list = entity.statementsTable.ToList();
                    row = list.Where(u => u.opStatementId == itemId)
                     .Select(S => new StatementsTable()
                     {
                         opStatementId = S.opStatementId,
                         name = S.name,
                         isActive = S.isActive,
                         notes = S.notes,

                     }).FirstOrDefault();
                    return row;
                }

            }
            catch (Exception ex)
            {
                row = new StatementsTable();
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
                        statementsTable objectDelete = entity.statementsTable.Find(id);

                        entity.statementsTable.Remove(objectDelete);
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
            //            statementsTable objectDelete = entity.statementsTable.Find(userId);

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
