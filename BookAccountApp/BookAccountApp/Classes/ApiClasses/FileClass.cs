﻿using BookAccountApp.Classes;
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
    public class FileClass
    {
        public int fileId { get; set; }
        public string fileName { get; set; }
        public string extention { get; set; }
        public string folderName { get; set; }
        public string notes { get; set; }
        public Nullable<bool> isActive { get; set; }
        public Nullable<int> tableRowId { get; set; }

        /// <summary>
        /// ///////////////////////////////////////
        /// </summary>
        /// <returns></returns>
        /// 
        //public async Task<List<Flights>> GetAll()
        //{

        //    List<Flights> List = new List<Flights>();
        //    bool canDelete = false;
        //    try
        //    {
        //        using (bookdbEntities entity = new bookdbEntities())
        //        {
        //            List = (from S in entity.flights
        //                    select new Flights()
        //                    {
        //                        flightId = S.flightId,
        //                        airlineflightTable = S.airline + "/" + S.flightTable.name,
        //                        airline= S.airline,
        //                        flight = S.flightTable.name==null?"": S.flightTable.name,
        //                        flightFrom = S.fromTable.name == null ? "" : S.fromTable.name,
        //                        flightTo = S.toTable.name,
        //                        notes = S.notes,
        //                        createDate = S.createDate,
        //                        updateDate = S.updateDate,
        //                        createUserId = S.createUserId,
        //                        updateUserId = S.updateUserId,
        //                        flightTableId=S.flightTableId==null?0: S.flightTableId,
        //                        fromTableId=S.fromTableId == null ? 0 : S.fromTableId,
        //                        toTableId =S.toTableId == null ? 0 : S.toTableId,

        //                        canDelete = true,

        //                    }).ToList();

        //            //if (List.Count > 0)
        //            //{
        //            //    for (int i = 0; i < List.Count; i++)
        //            //    {
        //            //        if (List[i].isActive == 1)
        //            //        {
        //            //            int userId = (int)List[i].userId;
        //            //            var itemsI = entity.packageUser.Where(x => x.userId == userId).Select(b => new { b.userId }).FirstOrDefault();

        //            //            if ((itemsI is null))
        //            //                canDelete = true;
        //            //        }
        //            //        List[i].canDelete = canDelete;
        //            //    }
        //            //}
        //            return List;
        //        }

        //    }
        //    catch
        //    {
        //        return List;
        //    }
        //}

        //public async Task<decimal> Save(Flights newitem)
        //{
        //    flights newObject = new flights();
        //    newObject = JsonConvert.DeserializeObject<flights>(JsonConvert.SerializeObject(newitem));

        //    decimal message = 0;
        //    if (newObject != null)
        //    {
        //        if (newObject.updateUserId == 0 || newObject.updateUserId == null)
        //        {
        //            Nullable<int> id = null;
        //            newObject.updateUserId = id;
        //        }
        //        if (newObject.createUserId == 0 || newObject.createUserId == null)
        //        {
        //            Nullable<int> id = null;
        //            newObject.createUserId = id;
        //        }
           


        //        try
        //        {
        //            using (bookdbEntities entity = new bookdbEntities())
        //            {
        //                var locationEntity = entity.Set<flights>();
        //                if (newObject.flightId == 0)
        //                {
        //                    newObject.createDate = DateTime.Now;
        //                    newObject.updateDate = newObject.createDate;
        //                    newObject.updateUserId = newObject.createUserId;


        //                    locationEntity.Add(newObject);
        //                    entity.SaveChanges();
        //                    message = newObject.flightId;
        //                }
        //                else
        //                {
        //                    var tmpObject = entity.flights.Where(p => p.flightId == newObject.flightId).FirstOrDefault();

        //                    tmpObject.updateDate = DateTime.Now;

        //                //    tmpObject.flightId = newObject.flightId;
        //                    tmpObject.airline = newObject.airline;
        //                    tmpObject.flightTableId = newObject.flightTableId;
        //                    tmpObject.fromTableId = newObject.fromTableId;
        //                    tmpObject.toTableId = newObject.toTableId;
        //                    tmpObject.notes = newObject.notes;
        //                    tmpObject.createDate = newObject.createDate;
                           
        //                    tmpObject.createUserId = newObject.createUserId;
        //                    tmpObject.updateUserId = newObject.updateUserId;


        //                    entity.SaveChanges();

        //                    message = tmpObject.flightId;
        //                }
        //            }
        //            return message;
        //        }
        //        catch
        //        {
        //            return 0;
        //        }
        //    }
        //    else
        //    {
        //        return 0;
        //    }
        //}
        //public async Task<Flights> GetByID(int itemId)
        //{


        //    Flights item = new Flights();
           

        //    Flights row = new Flights();
        //    try
        //    {
        //        using (bookdbEntities entity = new bookdbEntities())
        //        {
        //            var list = entity.flights.ToList();
        //            row = list.Where(u => u.flightId == itemId)
        //             .Select(S => new Flights()
        //             {
        //                 flightId = S.flightId,
        //                 airline = S.airline,
        //                 flight = S.flightTable.name == null ? "" : S.flightTable.name,
        //                 flightFrom = S.fromTable.name == null ? "" : S.fromTable.name,
        //                 flightTo = S.toTable.name,
        //                 notes = S.notes,
        //                 createDate = S.createDate,
        //                 updateDate = S.updateDate,
        //                 createUserId = S.createUserId,
        //                 updateUserId = S.updateUserId,
        //                 flightTableId = S.flightTableId == null ? 0 : S.flightTableId,
        //                 fromTableId = S.fromTableId == null ? 0 : S.fromTableId,
        //                 toTableId = S.toTableId == null ? 0 : S.toTableId,



        //             }).FirstOrDefault();
        //            return row;
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        row = new Flights();
        //        //userrow.name = ex.ToString();
        //        return row;
        //    }
        //}
        //public async Task<decimal> Delete(int id, int signuserId, bool final)
        //{

        //    decimal message = 0;
        //    if (final)
        //    {
        //        try
        //        {
        //            using (bookdbEntities entity = new bookdbEntities())
        //            {
        //                flights objectDelete = entity.flights.Find(id);

        //                entity.flights.Remove(objectDelete);
        //                message = entity.SaveChanges();
        //                return message;

        //            }
        //        }
        //        catch
        //        {
        //            return 0;

        //        }
        //    }
        //    return message;
        //    //else
        //    //{
        //    //    try
        //    //    {
        //    //        using (bookdbEntities entity = new bookdbEntities())
        //    //        {
        //    //            flights objectDelete = entity.flights.Find(userId);

        //    //            objectDelete.isActive = 0;
        //    //            objectDelete.updateUserId = signuserId;
        //    //        objectDelete.updateDate = DateTime.Now;
        //    //            message = entity.SaveChanges() ;

        //    //            return message;
        //    //        }
        //    //    }
        //    //    catch
        //    //    {
        //    //        return 0;
        //    //    }
        //    //}

        //}


    }
}