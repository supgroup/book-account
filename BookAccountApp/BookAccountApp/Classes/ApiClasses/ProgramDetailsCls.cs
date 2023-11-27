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
            deviceCode = Md5Encription.HashString(deviceCode);
            return deviceCode;
        }
        public async Task<string> CheckAvailable()
        {
            List<ProgramDetailsCls> List = new List<ProgramDetailsCls>();
            List = await GetAll();
            string result = "";
            string Nextresult = "";
            DateTime now = DateTime.Now;
            if (List== null|| List.Count()==0)
            {
                result ="no";//1
            } 
            else
            {
                ProgramDetailsCls currentdetail = List.Where(x => x.isCurrent == 1).FirstOrDefault();
                result= await CheckmodelAvailable(currentdetail.Activatemodel);//2
                if (result == "expired")
                {
                    ProgramDetailsCls NextDetail = List.Where(x => x.isCurrent == 2).FirstOrDefault();
                    if (NextDetail!=null)
                    {
                        if (NextDetail.Activatemodel!=null)
                        {
                            //if( NextDetail.Activatemodel.startDate <= currentdetail.Activatemodel.expireDate 
                            //     && NextDetail.Activatemodel.startDate<=now)
                            result = await CheckmodelAvailable(NextDetail.Activatemodel);
                            if(result=="ok")
                            {
                                //update to current
                                await updatetoCurrent(NextDetail);
                                //and ok
                                //result = "ok";
                            }
                        }


                    }
                    else
                    {
                        result = "expired";//3

                    }
                  
                   // result = "no";
                }
                else
                {

                }
                 
            }
            return result;
        }
        public async Task<string> CheckmodelAvailable(ActivateModel Activatemodel)
        {
            string result = "";
            string deviceCode = getHardCode();
            DateTime now = DateTime.Now;
            if (Activatemodel.customerHardCode == deviceCode)
            {
              
                if (Activatemodel.expireDate.Value >= now)
                {
                   
                    if (!(Activatemodel.startDate <= now))
                    {
                      //  MessageBox.Show("تاريخ بدايةالصلاحية لم يبدا بعد");
                        result = "notbegin";
                    }
                    else
                    {
                        result = "ok";
                        // MessageBox.Show("ok");
                       
                    }
                }
                else
                {
                  //  MessageBox.Show("النسخة منتهية");
                    result = "expired";
                }
            }
            else
            {
               // MessageBox.Show("الرمز غير صحيح");
                result = "wrongcode";
            }
            return result;
        }
        public async Task<int> GetCountDetailList(List<ProgramDetailsCls> List)
        {
           
            //List = await GetAll();
            int result = 0;
            if (List == null || List.Count() == 0)
            {
                result = 0;
            }
            else
            {
                result = List.Count();
            }
            return result;
        }
        public async Task<List<ProgramDetailsCls>> GetAll()
        {

            List<ProgramDetailsCls> List = new List<ProgramDetailsCls>();
            bool canDelete = false;
            CodeCls codclass = new CodeCls();
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
                    foreach(ProgramDetailsCls row in List)
                    {
                        row.Activatemodel = codclass.convertToModel(row.activateCode);

                    }
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

        public async Task<decimal> updatetoCurrent(ProgramDetailsCls newitem)
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
                        //locationEntity.ForEach(x =>);
                     
                        if (newObject.id == 0)
                        {

                        }
                        else
                        {
                            //reset all to 0
                            entity.ProgramDetails.ToList().ForEach(x => x.isCurrent = 0);
                            entity.SaveChanges();
                            //update last code to current
                            var tmpObject = entity.ProgramDetails.Where(p => p.id == newObject.id).FirstOrDefault(); 
                            tmpObject.isCurrent = 1;                            
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
        public async Task<decimal> resetallCurrent()
        {
            decimal message = 0;
                try
                {
                using (bookdbEntities entity = new bookdbEntities())
                {
                    var locationEntity = entity.Set<ProgramDetails>();
                    //locationEntity.ForEach(x =>);
                    //reset all to 0
                    entity.ProgramDetails.ToList().ForEach(x => x.isCurrent = 0);
                    message = entity.SaveChanges();

                }
                   
                    return message;
                }
                catch
                {
                    return 0;
                }
            }
        public async Task<decimal> resetallCurrent2()
        {
            decimal message = 0;
            try
            {
                using (bookdbEntities entity = new bookdbEntities())
                {
                    var locationEntity = entity.Set<ProgramDetails>();
                    //locationEntity.ForEach(x =>);
                    //reset all to 0
                    entity.ProgramDetails.ToList().Where(r=>r.isCurrent==2).ToList().ForEach(x => x.isCurrent = 0);
                    message = entity.SaveChanges();

                }

                return message;
            }
            catch
            {
                return 0;
            }
        }

    }
  
}
