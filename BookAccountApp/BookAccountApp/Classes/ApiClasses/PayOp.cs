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
using Newtonsoft.Json.Converters;

namespace BookAccountApp.ApiClasses
{
    public class PayOp
    {
        public int payOpId { get; set; }
        public string code { get; set; }
        public Nullable<decimal> cash { get; set; }
        public string opType { get; set; }
        public string side { get; set; }
        public Nullable<int> serviceId { get; set; }
        public string opStatus { get; set; }
        public Nullable<System.DateTime> opDate { get; set; }
        public string notes { get; set; }
        public Nullable<int> createUserId { get; set; }
        public Nullable<int> updateUserId { get; set; }
        public Nullable<System.DateTime> createDate { get; set; }
        public Nullable<System.DateTime> updateDate { get; set; }
        public Nullable<int> officeId { get; set; }
        public Nullable<int> passengerId { get; set; }
        public Nullable<int> userId { get; set; }
        public string recipient { get; set; }
        public string recivedFrom { get; set; }
        public Nullable<int> paysideId { get; set; }
        public string sideAr { get; set; }
        public Nullable<int> flightId { get; set; }
        public string opName { get; set; }
        //    
        public string passenger { get; set; }
        public string airline { get; set; }

        public string officeName { get; set; }
        public string systemType { get; set; }
        public Nullable<int> systemId { get; set; }
        public string systemName { get; set; }
        public string currency { get; set; }
        public Nullable<decimal> syValue { get; set; }
        public Nullable<int> exchangeId { get; set; }
        public string fromSide { get; set; }
        public string processType { get; set; }

        public Nullable<int> sourceId { get; set; }
        public Nullable<decimal> paid { get; set; }
        public Nullable<bool> isPaid { get; set; }
        public bool canDelete { get; set; }
        /// <summary>
        /// ///////////////////////////////////////
        /// </summary>
        /// <returns></returns>
        /// 
        public async Task<List<PayOp>> GetAll()
        {

            List<PayOp> List = new List<PayOp>();
            bool canDelete = false;
            try
            {
                using (bookdbEntities entity = new bookdbEntities())
                {
                    List = (from S in entity.payOp
                            select new PayOp()
                            {
                                payOpId = S.payOpId,
                                code = S.code,
                                cash = S.cash,
                                opType = S.opType,
                                side = S.paySides.code,
                                serviceId = S.serviceId,
                                opStatus = S.opStatus,
                                opDate = S.opDate,
                                notes = S.notes,
                                createUserId = S.createUserId,
                                updateUserId = S.updateUserId,
                                createDate = S.createDate,
                                updateDate = S.updateDate,
                                officeId = S.officeId,
                                passengerId = S.passengerId,
                                userId = S.userId,
                                recipient = S.recipient,
                                recivedFrom = S.recivedFrom,
                                paysideId = S.paysideId,
                                flightId = S.flightId,
                                opName = S.opName,
                                sideAr = S.paySides.sideAr,
                            }).ToList();

                    return List;
                }

            }
            catch
            {
                return List;
            }

        }
        public async Task<List<PayOp>> GetbyType(string opType)
        {

            List<PayOp> List = new List<PayOp>();
            bool canDelete = false;
            try
            {
                using (bookdbEntities entity = new bookdbEntities())
                {
                    List = (from S in entity.payOp
                            where S.opType == opType
                            select new PayOp()
                            {
                                payOpId = S.payOpId,
                                code = S.code,
                                cash = S.cash,
                                opType = S.opType,
                                side = S.paySides.code,
                                serviceId = S.serviceId,
                                opStatus = S.opStatus,
                                opDate = S.opDate,
                                notes = S.notes,
                                createUserId = S.createUserId,
                                updateUserId = S.updateUserId,
                                createDate = S.createDate,
                                updateDate = S.updateDate,
                                officeId = S.officeId,
                                passengerId = S.passengerId,
                                userId = S.userId,
                                recipient = S.recipient,
                                recivedFrom = S.recivedFrom,
                                paysideId = S.paysideId,
                                sideAr = S.paySides.sideAr,
                                flightId = S.flightId,
                                opName = S.opName,
                                passenger = S.passengers.name + " " + S.passengers.lastName,
                                airline = S.systems.name + "/" + S.flights.flightTable.name,
                                officeName = S.office.name,
                                systemType = S.systemType,
                                systemId = S.systemId,
                                systemName = S.systems.name,
                                syValue = S.syValue,
                                exchangeId = S.exchangeId,
                                currency = S.currency,
                                fromSide = S.fromSide,
                                processType = S.processType,
                                sourceId = S.sourceId,
                                paid = S.paid,
                                isPaid = S.isPaid,

                            }).ToList();

                    return List;
                }

            }
            catch
            {
                return List;
            }

        }

        public async Task<decimal> Save(PayOp newitem)
        {
            payOp newObject = new payOp();
            newObject = JsonConvert.DeserializeObject<payOp>(JsonConvert.SerializeObject(newitem));

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
                if (newObject.serviceId == 0 || newObject.serviceId == null)
                {
                    Nullable<int> id = null;
                    newObject.serviceId = id;
                }
                if (newObject.paysideId == 0 || newObject.paysideId == null)
                {
                    Nullable<int> id = null;
                    newObject.paysideId = id;
                }
                if (newObject.passengerId == 0 || newObject.passengerId == null)
                {
                    Nullable<int> id = null;
                    newObject.passengerId = id;
                }
                if (newObject.officeId == 0 || newObject.officeId == null)
                {
                    Nullable<int> id = null;
                    newObject.officeId = id;
                }
                if (newObject.userId == 0 || newObject.userId == null)
                {
                    Nullable<int> id = null;
                    newObject.userId = id;
                }
                try
                {
                    using (bookdbEntities entity = new bookdbEntities())
                    {
                        var locationEntity = entity.Set<payOp>();
                        if (newObject.payOpId == 0)
                        {
                            newObject.createDate = DateTime.Now;
                            newObject.updateDate = newObject.createDate;
                            newObject.updateUserId = newObject.createUserId;


                            locationEntity.Add(newObject);
                            entity.SaveChanges();
                            message = newObject.payOpId;
                        }
                        else
                        {
                            var tmpObject = entity.payOp.Where(p => p.payOpId == newObject.payOpId).FirstOrDefault();

                            //tmpObject.payOpId = newObject.payOpId;
                            tmpObject.code = newObject.code;
                            tmpObject.cash = newObject.cash;
                            tmpObject.opType = newObject.opType;
                            tmpObject.side = newObject.side;
                            tmpObject.serviceId = newObject.serviceId;
                            tmpObject.opStatus = newObject.opStatus;
                            tmpObject.opDate = newObject.opDate;
                            tmpObject.notes = newObject.notes;
                            //tmpObject.createUserId = newObject.createUserId;
                            tmpObject.updateUserId = newObject.updateUserId;
                            //tmpObject.createDate = newObject.createDate;
                            tmpObject.updateDate = DateTime.Now;
                            tmpObject.officeId = newObject.officeId;
                            tmpObject.passengerId = newObject.passengerId;
                            tmpObject.userId = newObject.userId;
                            tmpObject.recipient = newObject.recipient;
                            tmpObject.recivedFrom = newObject.recivedFrom;
                            tmpObject.paysideId = newObject.paysideId;
                            tmpObject.flightId = newObject.flightId;
                            tmpObject.opName = newObject.opName;
                            tmpObject.systemType = newObject.systemType;
                            tmpObject.systemId = newObject.systemId;
                            tmpObject.syValue = newObject.syValue;
                            tmpObject.exchangeId = newObject.exchangeId;
                            tmpObject.currency = newObject.currency;
                            tmpObject.fromSide = newObject.fromSide;
                            tmpObject.processType = newObject.processType;
                            tmpObject.sourceId = newObject.sourceId;
                               tmpObject. paid = newObject.paid;
                                tmpObject.isPaid = newObject.isPaid;
                            entity.SaveChanges();

                            message = tmpObject.payOpId;
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

        public async Task<PayOp> GetByID(int payOpId)
        {

            string message = "";

            PayOp row = new PayOp();
            try
            {
                using (bookdbEntities entity = new bookdbEntities())
                {
                    var list = entity.payOp.ToList();
                    row = list.Where(u => u.payOpId == payOpId)
                      .Select(S => new PayOp()
                      {
                          payOpId = S.payOpId,
                          code = S.code,
                          cash = S.cash,
                          opType = S.opType,
                          side = S.side,
                          serviceId = S.serviceId,
                          opStatus = S.opStatus,
                          opDate = S.opDate,
                          notes = S.notes,
                          createUserId = S.createUserId,
                          updateUserId = S.updateUserId,
                          createDate = S.createDate,
                          updateDate = S.updateDate,
                          officeId = S.officeId,
                          passengerId = S.passengerId,
                          userId = S.userId,
                          recipient = S.recipient,
                          recivedFrom = S.recivedFrom,
                          paysideId = S.paysideId,
                          flightId = S.flightId,
                          opName = S.opName,
                          processType = S.processType,
                          sourceId = S.sourceId,
                          paid = S.paid,
                          isPaid = S.isPaid,
                      }).FirstOrDefault();
                    return row;
                }

            }
            catch (Exception ex)
            {
                row = new PayOp();
                //userrow.name = ex.ToString();
                return row;
            }

        }

        //public async Task<PayOp> getLastPayOp(int packageUserId)
        //{
        //    PayOp item = new PayOp();

        //    //Dictionary<string, string> parameters = new Dictionary<string, string>();
        //    //parameters.Add("packageUserId", packageUserId.ToString());
        //    ////#################
        //    //IEnumerable<Claim> claims = await APIResult.getList(urimainpath + "getLastPayOp", parameters);

        //    //foreach (Claim c in claims)
        //    //{
        //    //    if (c.Type == "scopes")
        //    //    {
        //    //        item = JsonConvert.DeserializeObject<PayOp>(c.Value, new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy" });
        //    //        break;
        //    //    }
        //    //}

        //    return item;


        //}

        //public async Task<List<PayOp>> GetByCustomerId(int customerId)
        //{
        //    List<PayOp> list = new List<PayOp>();
        //    //Dictionary<string, string> parameters = new Dictionary<string, string>();
        //    //parameters.Add("customerId", customerId.ToString());
        //    ////#################
        //    //IEnumerable<Claim> claims = await APIResult.getList(urimainpath + "GetByCustomerId", parameters);

        //    //foreach (Claim c in claims)
        //    //{
        //    //    if (c.Type == "scopes")
        //    //    {
        //    //        list.Add(JsonConvert.DeserializeObject<PayOp>(c.Value, new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy" }));
        //    //    }
        //    //}
        //    return list;

        //}
        //public async Task<List<PayOp>> GetByOfficeId(int officeId)
        //{
        //    List<PayOp> list = new List<PayOp>();
        //    using (bookdbEntities entity = new bookdbEntities())
        //    {
        //        list = (from S in entity.payOp
        //                where (S.serviceData.officeId == officeId)
        //                select new PayOp()
        //                {
        //                    payOpId = S.payOpId,
        //                    code = S.code,
        //                    cash = S.cash,
        //                    opType = S.opType,
        //                    side = S.side,
        //                    serviceId = S.serviceId,
        //                    opStatus = S.opStatus,
        //                    opDate = S.opDate,
        //                    notes = S.notes,
        //                    createUserId = S.createUserId,
        //                    updateUserId = S.updateUserId,
        //                    createDate = S.createDate,
        //                    updateDate = S.updateDate,


        //                }).ToList();
        //    }
        //    return list;

        //}

        public async Task<decimal> Delete(int id, int userId, bool final)
        {
            decimal message = 0;
            if (final)
            {
                try
                {
                    using (bookdbEntities entity = new bookdbEntities())
                    {
                        payOp objectDelete = entity.payOp.Find(id);

                        entity.payOp.Remove(objectDelete);
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
            //            payOp objectDelete = entity.payOp.Find(userId);

            //            objectDelete.isActive = 0;
            //            objectDelete.updateUserId = signuserId;
            //            objectDelete.updateDate = DateTime.Now;
            //            message = entity.SaveChanges();

            //            return message;
            //        }
            //    }
            //    catch
            //    {
            //        return 0;
            //    }
            //}

        }

        public async Task<int> GetLastNum(string payOpCode)
        {


            List<string> numberList;
            int lastNum = 0;
            using (bookdbEntities entity = new bookdbEntities())
            {
                numberList = entity.payOp.Where(b => b.code.Contains(payOpCode + "-")).Select(b => b.code).ToList();

                for (int i = 0; i < numberList.Count; i++)
                {
                    string code = numberList[i];
                    string s = code.Substring(code.LastIndexOf("-") + 1);
                    numberList[i] = s;
                }
                if (numberList.Count > 0)
                {
                    numberList.Sort();
                    lastNum = int.Parse(numberList[numberList.Count - 1]);
                }
            }
            return lastNum;
        }




        public async Task<string> generateNumber(string payOpCode)
        {
            int sequence = await GetLastNum(payOpCode);
            sequence++;
            string strSeq = sequence.ToString();
            if (sequence <= 999999)
                strSeq = sequence.ToString().PadLeft(6, '0');
            string payOpNum = payOpCode + "-" + strSeq;
            return payOpNum;
        }

        public async Task<int> AddSideRecord(ServiceData serviceModel, PaySides paysideModel)
        {
            //   paySides newObject = new paySides();
            PayOp payOpModel = new PayOp();

            int message = 0;
            try
            {
                payOpModel.code = await generateNumber("P" + "BK");
                payOpModel.cash = serviceModel.total;
                payOpModel.opType = "p";
                payOpModel.side = "system";
                payOpModel.serviceId = serviceModel.serviceId;
                payOpModel.opStatus = "draft";
                payOpModel.opDate = DateTime.Now;
                payOpModel.notes = serviceModel.notes;
                payOpModel.createUserId = serviceModel.updateUserId;
                payOpModel.updateUserId = serviceModel.updateUserId;
                // //payOpModel.createDate = serviceModel.createDate;
                //  payOpModel.updateDate = DateTime.Now;
                payOpModel.officeId = serviceModel.officeId;
                payOpModel.passengerId = serviceModel.passengerId;
                // payOpModel.userId = serviceModel.updateUserId;
                payOpModel.recipient = "";
                payOpModel.recivedFrom = "";
                payOpModel.paysideId = paysideModel.paysideId;
                payOpModel.flightId = serviceModel.flightId;
                payOpModel.opName = "";
                payOpModel.systemType = serviceModel.systemType;
                payOpModel.systemId = serviceModel.systemId;
                payOpModel.syValue = serviceModel.syValue;
                payOpModel.exchangeId = serviceModel.exchangeId;
                payOpModel.currency = serviceModel.currency;
                payOpModel.fromSide = serviceModel.systemType;
                payOpModel.processType = "book";
                payOpModel.isPaid = true;
                await Save(payOpModel);
                return message;
            }
            catch
            {
                return 0;
            }
        }
        public async Task<int> AddCompanyCommRecord(ServiceData serviceModel)
        {
            //   paySides newObject = new paySides();
            PayOp payOpModel = new PayOp();

            int message = 0;
            try
            {
                payOpModel.code = await generateNumber("P" + "CC");//company_commission
                payOpModel.cash = serviceModel.company_commission_value;
                payOpModel.opType = "p";
                payOpModel.side = "system";
                payOpModel.serviceId = serviceModel.serviceId;
                payOpModel.opStatus = "draft";
                payOpModel.opDate = DateTime.Now;
                payOpModel.notes = serviceModel.notes;
                payOpModel.createUserId = serviceModel.updateUserId;
                payOpModel.updateUserId = serviceModel.updateUserId;
                // //payOpModel.createDate = serviceModel.createDate;
                //  payOpModel.updateDate = DateTime.Now;
                payOpModel.officeId = null;
                payOpModel.passengerId = null;
                // payOpModel.userId = serviceModel.updateUserId;
                payOpModel.recipient = "";
                payOpModel.recivedFrom = "";
                payOpModel.paysideId = null;
                payOpModel.flightId = null;
                payOpModel.opName = "";
                payOpModel.systemType = serviceModel.systemType;
                payOpModel.systemId = serviceModel.systemId;
                payOpModel.syValue = serviceModel.syValue;
                payOpModel.exchangeId = serviceModel.exchangeId;
                payOpModel.currency = serviceModel.currency;
                payOpModel.fromSide = serviceModel.systemType;
                payOpModel.processType = "company_commission";
                // payOpModel.sourceId = null,
                payOpModel.paid = 0;
                payOpModel.isPaid = false;
              
               
                decimal res = await Save(payOpModel);
                message = Convert.ToInt32(res);
                return message;
          
            }
            catch
            {
                return 0;
            }
        }
        public async Task<int> AddOfficeCommRecord(ServiceData serviceModel)
        {
            //   paySides newObject = new paySides();
            PayOp payOpModel = new PayOp();

            int message = 0;
            try
            {
                if (serviceModel.officeId>0)
                {

              
                payOpModel.code = await generateNumber("D" + "OC");//company_commission
                payOpModel.cash = serviceModel.office_commission_value;
                payOpModel.opType = "d";
                payOpModel.side = "office";
                payOpModel.serviceId = serviceModel.serviceId;
                payOpModel.opStatus = "draft";
                payOpModel.opDate = DateTime.Now;
                payOpModel.notes = serviceModel.notes;
                payOpModel.createUserId = serviceModel.updateUserId;
                payOpModel.updateUserId = serviceModel.updateUserId;
                // //payOpModel.createDate = serviceModel.createDate;
                //  payOpModel.updateDate = DateTime.Now;
                payOpModel.officeId = serviceModel.officeId;
                payOpModel.passengerId = null;
                // payOpModel.userId = serviceModel.updateUserId;
                payOpModel.recipient = "";
                payOpModel.recivedFrom = "";
                payOpModel.paysideId = null;
                payOpModel.flightId = null;
                payOpModel.opName = "";
                payOpModel.systemType = serviceModel.systemType;
                payOpModel.systemId = serviceModel.systemId;
                payOpModel.syValue = serviceModel.syValue;
                payOpModel.exchangeId = serviceModel.exchangeId;
                payOpModel.currency = serviceModel.currency;
                payOpModel.fromSide = serviceModel.systemType;
                payOpModel.processType = "office_commission";
                // payOpModel.sourceId = null,
                payOpModel.paid = 0;
                payOpModel.isPaid = false;
                 decimal   res= await Save(payOpModel);
                    message = Convert.ToInt32(res);
                }
                return message;
            }
            catch
            {
                return 0;
            }
        }
        public async Task<int> updateSideBalance(string side, decimal cash)
        {

            //   paySides newObject = new paySides();

            int message = 0;
            try
            {
                using (bookdbEntities entity = new bookdbEntities())
                {
                    var locationEntity = entity.Set<paySides>();

                    var tmpObject = entity.paySides.Where(p => p.code == side).FirstOrDefault();
                    decimal sum = (decimal)tmpObject.balance + cash;
                    if (sum < 0)
                    {
                        return -1;
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

    }
}
