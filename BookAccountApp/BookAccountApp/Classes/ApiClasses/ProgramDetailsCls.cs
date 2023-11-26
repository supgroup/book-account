using Newtonsoft.Json;
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
using Newtonsoft.Json.Converters;

namespace BookAccountApp.ApiClasses
{
    public class ActivateModel
    {

        public string officeName { get; set; }
        public string customerHardCode { get; set; }

        public Nullable<System.DateTime> startDate { get; set; }
        public Nullable<System.DateTime> expireDate { get; set; }
        public int yearCount { get; set; }

        public string confirmStat { get; set; }

    }
    public class daysremain
    {
        public Nullable<int> days { get; set; }
        public Nullable<int> hours { get; set; }
        public Nullable<int> minute { get; set; }
        public string expirestate { get; set; }
    }
    public class ProgramDetailsCls
    {
        public int id { get; set; }
        public string activateCode { get; set; }
        public Nullable<int> isCurrent { get; set; }
        public string state { get; set; }
        public  ActivateModel Activatemodel { get; set; }
        public string getHardCode()
        {
            string motherCode = setupConfiguration.GetMotherBoardID();
            string hardCode = setupConfiguration.GetHDDSerialNo();
            string deviceCode = motherCode + "-" + hardCode;
            return deviceCode;
        }

        public async Task<List<ProgramDetailsCls>> GetAll()
        {

            List<ProgramDetailsCls> List = new List<ProgramDetailsCls>();
            bool canDelete = false;
            try
            {
                using (bookdbEntities entity = new bookdbEntities())
                {
                    List = (from S in entity.ProgramDetails
                            select new ProgramDetailsCls()
                            {
                                id = S.id,
                                activateCode = S.activateCode,
                                isCurrent = S.isCurrent,
                                state = S.state,


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

        public async Task<decimal> Save(ProgramDetailsCls newitem)
        {
            ProgramDetails newObject = new ProgramDetails();
            newObject = JsonConvert.DeserializeObject<ProgramDetails>(JsonConvert.SerializeObject(newitem));

            decimal message = 0;
            if (newObject != null)
            {



                try
                {
                    using (bookdbEntities entity = new bookdbEntities())
                    {
                        var locationEntity = entity.Set<ProgramDetails>();
                        if (newObject.id == 0)
                        {



                            locationEntity.Add(newObject);
                            entity.SaveChanges();
                            message = newObject.id;
                        }
                        else
                        {
                            var tmpObject = entity.ProgramDetails.Where(p => p.id == newObject.id).FirstOrDefault();

                            
                            tmpObject.activateCode = newObject.activateCode;
                            tmpObject.isCurrent = newObject.isCurrent;
                            tmpObject.state = newObject.state;
                            entity.SaveChanges();

                            message = tmpObject.id;
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
        public async Task<ProgramDetailsCls> GetByID(int itemId)
        {


           


            ProgramDetailsCls row = new ProgramDetailsCls();
            try
            {
                using (bookdbEntities entity = new bookdbEntities())
                {
                    var list = entity.ProgramDetails.ToList();
                    row = list.Where(u => u.id == itemId)
                     .Select(S => new ProgramDetailsCls()
                     {
                         id = S.id,
                         activateCode = S.activateCode,
                         isCurrent = S.isCurrent,
                         state = S.state,


                     }).FirstOrDefault();
                    return row;
                }

            }
            catch (Exception ex)
            {
                row = new ProgramDetailsCls();
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
                        ProgramDetails objectDelete = entity.ProgramDetails.Find(id);

                        entity.ProgramDetails.Remove(objectDelete);
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
            //            ProgramDetails objectDelete = entity.ProgramDetails.Find(userId);

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
