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
    public class Office
    {
        public int officeId { get; set; }
        public string name { get; set; }
        public string agentName { get; set; }
        public Nullable<System.DateTime> joinDate { get; set; }
        public string mobile { get; set; }
        public string address { get; set; }
        public string userName { get; set; }
        public string passwordSY { get; set; }
        public string PasswordSoto { get; set; }
        public string notes { get; set; }
        public Nullable<System.DateTime> createDate { get; set; }
        public Nullable<System.DateTime> updateDate { get; set; }
        public Nullable<int> createUserId { get; set; }
        public Nullable<int> updateUserId { get; set; }
        public string strjoinDate { get; set; }
        public bool canDelete { get; set; }
        public Nullable<decimal> balance { get; set; }
        public Nullable<decimal> commission_ratio { get; set; }
        public Nullable<bool> isActive { get; set; }

        /// <summary>
        /// ///////////////////////////////////////
        /// </summary>
        /// <returns></returns>
        /// 
        public async Task<List<Office>> GetAll()
        {

            List<Office> List = new List<Office>();
            bool canDelete = false;
            try
            {
                using (bookdbEntities entity = new bookdbEntities())
                {
                    List = (from S in entity.office
                            select new Office()
                            {
                                officeId = S.officeId,
                                name = S.name,
                                agentName = S.agentName,
                                joinDate = S.joinDate,
                                mobile = S.mobile,
                                address = S.address,
                                userName = S.userName,
                                passwordSY = S.passwordSY,
                                PasswordSoto = S.PasswordSoto,
                                notes = S.notes,
                                createDate = S.createDate,
                                updateDate = S.updateDate,
                                createUserId = S.createUserId,
                                updateUserId = S.updateUserId,
                                isActive=S.isActive,
                                canDelete = false,
                                commission_ratio=S.commission_ratio,
                            }).ToList();

                    if (List.Count > 0)
                    {
                        for (int i = 0; i < List.Count; i++)
                        {
                            if (List[i].isActive == true)
                            {
                                int itemId = (int)List[i].officeId;
                                var itemsI = entity.serviceData.Where(x => x.officeId == itemId).Select(b => new { b.officeId }).FirstOrDefault();
                                var items2 = entity.payOp.Where(x => x.officeId == itemId).Select(b => new { b.officeId }).FirstOrDefault();
                                var items3 = entity.serviceData.Where(x => x.officeSystem.officeId == itemId).Select(b => new { b.serviceId }).FirstOrDefault();

                                if ((itemsI is null)&& (items2 is null) && (items3 is null))
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

        public async Task<decimal> Save(Office newitem)
        {
            office newObject = new office();
            newObject = JsonConvert.DeserializeObject<office>(JsonConvert.SerializeObject(newitem));

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
                        var locationEntity = entity.Set<office>();
                        if (newObject.officeId == 0)
                        {
                            newObject.createDate = DateTime.Now;
                            newObject.updateDate = newObject.createDate;
                            newObject.updateUserId = newObject.createUserId;
                            newObject.isActive = true;

                            locationEntity.Add(newObject);
                            entity.SaveChanges();
                            message = newObject.officeId;
                            OfficeSystem osmodel = new OfficeSystem();
                            decimal res = await osmodel.AddByOfficeId(newObject.officeId);
                        }
                        else
                        {
                            var tmpObject = entity.office.Where(p => p.officeId == newObject.officeId).FirstOrDefault();
                            // update is active in os list
                            OfficeSystem osmodel = new OfficeSystem();
                            int res = await osmodel.updateListisActive(tmpObject.officeId,(bool)newObject.isActive, "office");

                            tmpObject.updateDate = DateTime.Now;
                         
                            tmpObject.name = newObject.name;
                            tmpObject.agentName = newObject.agentName;
                            tmpObject.joinDate = newObject.joinDate;
                            tmpObject.mobile = newObject.mobile;
                            tmpObject.address = newObject.address;
                            tmpObject.userName = newObject.userName;
                            tmpObject.passwordSY = newObject.passwordSY;
                            tmpObject.PasswordSoto = newObject.PasswordSoto;
                            tmpObject.notes = newObject.notes;
                            tmpObject.isActive = newObject.isActive;
                            tmpObject.createUserId = newObject.createUserId;
                            tmpObject.updateUserId = newObject.updateUserId;
                            tmpObject.commission_ratio = newObject.commission_ratio;
                            entity.SaveChanges();

                            message = tmpObject.officeId;
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
        public async Task<Office> GetByID(int itemId)
        {


            Office item = new Office();
           

            Office row = new Office();
            try
            {
                using (bookdbEntities entity = new bookdbEntities())
                {
                    var list = entity.office.ToList();
                    row = list.Where(u => u.officeId == itemId)
                     .Select(S => new Office()
                     {
                         officeId = S.officeId,
                         name = S.name,
                         agentName = S.agentName,
                         joinDate = S.joinDate,
                         mobile = S.mobile,
                         address = S.address,
                         userName = S.userName,
                         passwordSY = S.passwordSY,
                         PasswordSoto = S.PasswordSoto,
                         notes = S.notes,
                         createDate = S.createDate,
                         updateDate = S.updateDate,
                         createUserId = S.createUserId,
                         updateUserId = S.updateUserId,
                         isActive = S.isActive,
                         commission_ratio = S.commission_ratio,
                     }).FirstOrDefault();
                    return row;
                }

            }
            catch (Exception ex)
            {
                row = new Office();
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
                        office objectDelete = entity.office.Find(id);
                        List<officeFiles> fileslist = entity.officeFiles.Where(f => f.officeId == objectDelete.officeId).ToList();
                      
                        //remove files first
                        //code here
                        //remove rows from db
                        entity.officeFiles.RemoveRange(fileslist);
                        //delete related OfficeSystem
                        OfficeSystem OfficeSystemModel = new OfficeSystem();
                        decimal res=   await OfficeSystemModel.DeletebyOfficeId(id);

                        entity.office.Remove(objectDelete);
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
                    OfficeSystem osmodel = new OfficeSystem();
                    using (bookdbEntities entity = new bookdbEntities())
                    {
                        office objectDelete = entity.office.Find(id);
                        int res = await osmodel.updateListisActive(id, false, "office");
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
        //            numberList = entity.office.Where(b => b.nu.Contains(type + "-")).Select(b => b.serviceNum).ToList();

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
