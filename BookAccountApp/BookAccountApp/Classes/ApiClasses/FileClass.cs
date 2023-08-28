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
    public class FileClass
    {
        public int fileId { get; set; }
        public string fileName { get; set; }
        public string extention { get; set; }
        public string folderName { get; set; }
        public string notes { get; set; }
        public Nullable<bool> isActive { get; set; }
        public Nullable<int> tableRowId { get; set; }
        public bool canDelete { get; set; }
        /// <summary>
        /// ///////////////////////////////////////
        /// </summary>
        /// <returns></returns>
        /// 
        public async Task<List<FileClass>> GetAll()
        {

            List<FileClass> List = new List<FileClass>();
            bool canDelete = false;
            try
            {
                using (bookdbEntities entity = new bookdbEntities())
                {
                    List = (from S in entity.passengerFiles
                            select new FileClass()
                            {
                                fileId = S.fileId,
                                fileName = S.fileName,
                                extention = S.extention,
                                folderName = S.folderName,
                                notes = S.notes,
                                isActive = S.isActive,
                                tableRowId = S.passengerId,

                                canDelete = false,

                            }).ToList();

                    //if (List.Count > 0)
                    //{
                    //    for (int i = 0; i < List.Count; i++)
                    //    {
                    //        if (List[i].isActive == true)
                    //        {
                    //            int itemId = (int)List[i].fileId;
                    //            var itemsI = entity.serviceData.Where(x => x.fileId == itemId).Select(b => new { b.fileId }).FirstOrDefault();

                    //            if (itemsI is null)
                    //            {
                    //                List[i].canDelete = true;
                    //            }
                    //        }

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

        public async Task<decimal> Save(FileClass newitem)
        {
            passengerFiles newObject = new passengerFiles();
            newObject = JsonConvert.DeserializeObject<passengerFiles>(JsonConvert.SerializeObject(newitem));

            decimal message = 0;
            if (newObject != null)
            {
                if (newObject.passengerId == 0 || newObject.passengerId == null)
                {
                    Nullable<int> id = null;
                    newObject.passengerId = id;
                }
              



                try
                {
                    using (bookdbEntities entity = new bookdbEntities())
                    {
                        var locationEntity = entity.Set<passengerFiles>();
                        if (newObject.fileId == 0)
                        {
                         

                            newObject.isActive = true;
                            locationEntity.Add(newObject);
                            entity.SaveChanges();
                            message = newObject.fileId;
                        }
                        else
                        {
                            var tmpObject = entity.passengerFiles.Where(p => p.fileId == newObject.fileId).FirstOrDefault();

                     

                            tmpObject.fileId = newObject.fileId;
                            tmpObject.fileName = newObject.fileName;
                            tmpObject.extention = newObject.extention;
                            tmpObject.folderName = newObject.folderName;
                            tmpObject.notes = newObject.notes;
                            tmpObject.isActive = newObject.isActive;
                            tmpObject.passengerId = newObject.passengerId;

                            entity.SaveChanges();

                            message = tmpObject.fileId;
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
        public async Task<FileClass> GetByID(int itemId)
        {


            FileClass item = new FileClass();


            FileClass row = new FileClass();
            try
            {
                using (bookdbEntities entity = new bookdbEntities())
                {
                    var list = entity.passengerFiles.ToList();
                    row = list.Where(u => u.fileId == itemId)
                     .Select(S => new FileClass()
                     {
                         fileId = S.fileId,
                         fileName = S.fileName,
                         extention = S.extention,
                         folderName = S.folderName,
                         notes = S.notes,
                         isActive = S.isActive,
                         tableRowId = S.passengerId,

                     }).FirstOrDefault();
                    return row;
                }

            }
            catch (Exception ex)
            {
                row = new FileClass();
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
                        passengerFiles objectDelete = entity.passengerFiles.Find(id);

                        entity.passengerFiles.Remove(objectDelete);
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
                        passengerFiles objectDelete = entity.passengerFiles.Find(id);

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
