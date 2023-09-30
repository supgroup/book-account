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
    public class Passengers
    {
        public int passengerId { get; set; }
        public string name { get; set; }
        public string lastName { get; set; }
        public string father { get; set; }
        public string mother { get; set; }
        public string notes { get; set; }
        public Nullable<System.DateTime> createDate { get; set; }
        public Nullable<System.DateTime> updateDate { get; set; }
        public Nullable<int> createUserId { get; set; }
        public Nullable<int> updateUserId { get; set; }
        public string fullName { get; set; }
        public bool canDelete { get; set; }
        public Nullable<decimal> balance { get; set; }
        public Nullable<bool> isActive { get; set; }
        public string code { get; set; }

        /// <summary>
        /// ///////////////////////////////////////
        /// </summary>
        /// <returns></returns>
        /// 
        public async Task<List<Passengers>> GetAll()
        {

            List<Passengers> List = new List<Passengers>();
            bool canDelete = false;
            try
            {
                using (bookdbEntities entity = new bookdbEntities())
                {
                    List = (from S in entity.passengers
                            select new Passengers()
                            {
                                passengerId = S.passengerId,
                                name = S.name,
                                lastName = S.lastName,
                                father = S.father,
                                mother = S.mother,
                                notes = S.notes,
                                createDate = S.createDate,
                                updateDate = S.updateDate,
                                createUserId = S.createUserId,
                                updateUserId = S.updateUserId,
                                fullName= S.name+" "+S.lastName,
                                isActive = S.isActive,
                                canDelete = false,
                                code=S.code,
                            }).ToList();

                    if (List.Count > 0)
                    {
                        for (int i = 0; i < List.Count; i++)
                        {
                            if (List[i].isActive == true)
                            {
                                int itemId = (int)List[i].passengerId;
                                var itemsI = entity.serviceData.Where(x => x.passengerId == itemId).Select(b => new { b.passengerId }).FirstOrDefault();
                                var items2 = entity.payOp.Where(x => x.passengerId == itemId).Select(b => new { b.passengerId }).FirstOrDefault();

                                if ((itemsI is null) && (items2 is null))
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

        public async Task<decimal> Save(Passengers newitem)
        {
            passengers newObject = new passengers();
            newObject = JsonConvert.DeserializeObject<passengers>(JsonConvert.SerializeObject(newitem));

            decimal message = 0;
            if (newObject != null)
            {
                if (newObject.updateUserId == 0 || newObject.updateUserId == null)
                {
                    Nullable<int> id = null;
                    newObject.updateUserId = id;
                }
                if (newObject.createUserId == 0 || newObject.createUserId == null)
                {
                    Nullable<int> id = null;
                    newObject.createUserId = id;
                }
                try
                {
                    using (bookdbEntities entity = new bookdbEntities())
                    {
                        var locationEntity = entity.Set<passengers>();
                        if (newObject.passengerId == 0)
                        {
                            newObject.code =  generateCodeNumber();
                            newObject.createDate = DateTime.Now;
                            newObject.updateDate = newObject.createDate;
                            newObject.updateUserId = newObject.createUserId;
                            newObject.isActive = true;
                            newObject.balance = 0;
                            locationEntity.Add(newObject);
                            entity.SaveChanges();
                            message = newObject.passengerId;
                        }
                        else
                        {
                            var tmpObject = entity.passengers.Where(p => p.passengerId == newObject.passengerId).FirstOrDefault();

                            tmpObject.updateDate = DateTime.Now;
                            tmpObject.passengerId = newObject.passengerId;
                            tmpObject.name = newObject.name;
                            tmpObject.lastName = newObject.lastName;
                            tmpObject.father = newObject.father;
                            tmpObject.mother = newObject.mother;
                            tmpObject.notes = newObject.notes;
                           // tmpObject.createDate = newObject.createDate;
                          //  tmpObject.updateDate = newObject.updateDate;
                           // tmpObject.createUserId = newObject.createUserId;
                            tmpObject.updateUserId = newObject.updateUserId;
                            tmpObject.isActive = newObject.isActive;

                            entity.SaveChanges();

                            message = tmpObject.passengerId;
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
        public async Task<Passengers> GetByID(int itemId)
        {


            Passengers item = new Passengers();
           

            Passengers row = new Passengers();
            try
            {
                using (bookdbEntities entity = new bookdbEntities())
                {
                    var list = entity.passengers.ToList();
                    row = list.Where(u => u.passengerId == itemId)
                     .Select(S => new Passengers()
                     {
                         passengerId = S.passengerId,
                         name = S.name,
                         lastName = S.lastName,
                         father = S.father,
                         mother = S.mother,
                         notes = S.notes,
                         createDate = S.createDate,
                         updateDate = S.updateDate,
                         createUserId = S.createUserId,
                         updateUserId = S.updateUserId,
                         isActive = S.isActive,
                         code = S.code,
                     }).FirstOrDefault();
                    return row;
                }

            }
            catch (Exception ex)
            {
                row = new Passengers();
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
                        passengers objectDelete = entity.passengers.Find(id);
                        List<passengerFiles> fileslist = entity.passengerFiles.Where(f => f.passengerId == objectDelete.passengerId).ToList();
                        //remove files first
                        //code here
                        //remove rows from db
                        entity.passengerFiles.RemoveRange(fileslist);
                        entity.passengers.Remove(objectDelete);
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
                        passengers objectDelete = entity.passengers.Find(id);

                        objectDelete.isActive = false;
                        objectDelete.updateUserId = signuserId;
                        objectDelete.updateDate = DateTime.Now;
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
        public string generateCodeNumber( )
        {
            int sequence =  GetLastNumOfCode();
            sequence++;
            string strSeq = sequence.ToString();
            //if (sequence <= 999999)
            //    strSeq = sequence.ToString().PadLeft(6, '0');
            //string transNum = type.ToUpper() + "-" + strSeq;
            return strSeq;
        }
        public int GetLastNumOfCode()
        {

            try
            {
                List<string> numberList;
                int lastNum = 0;
                using (bookdbEntities entity = new bookdbEntities())
                {
                    numberList = entity.passengers.Select(b => b.code).ToList();

                    //for (int i = 0; i < numberList.Count; i++)
                    //{
                    //    string code = numberList[i];
                    //    string s = code.Substring(code.LastIndexOf("-") + 1);
                    //    numberList[i] = s;
                    //}
                    if (numberList.Count > 0)
                    {
                        numberList.Sort();
                        lastNum = int.Parse(numberList[numberList.Count - 1]);
                    }
                }

                return lastNum;
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
        //            numberList = entity.passengers.Where(b => b.nu.Contains(type + "-")).Select(b => b.serviceNum).ToList();

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

    }
}
