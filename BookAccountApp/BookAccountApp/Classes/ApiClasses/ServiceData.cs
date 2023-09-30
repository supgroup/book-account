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
    public class ServiceData
    {
        public int serviceId { get; set; }
        public string serviceNum { get; set; }
        public string type { get; set; }
        public string passenger { get; set; }
        public string ticketNum { get; set; }
        public string airline { get; set; }
        public Nullable<int> officeId { get; set; }
        public Nullable<System.DateTime> serviceDate { get; set; }
        public string pnr { get; set; }
        public Nullable<decimal> ticketvalueSP { get; set; }
        public Nullable<decimal> ticketvalueDollar { get; set; }
        public Nullable<decimal> total { get; set; }
        public Nullable<decimal> priceBeforTax { get; set; }
        public Nullable<decimal> commitionRatio { get; set; }
        public Nullable<decimal> commitionValue { get; set; }
        public Nullable<decimal> cost { get; set; }
        public Nullable<decimal> saleValue { get; set; }
        public Nullable<decimal> paid { get; set; }
        public Nullable<decimal> profit { get; set; }
        public string notes { get; set; }
        public string state { get; set; }
        public Nullable<System.DateTime> createDate { get; set; }
        public Nullable<System.DateTime> updateDate { get; set; }
        public Nullable<int> createUserId { get; set; }
        public Nullable<int> updateUserId { get; set; }
        public Nullable<int> passengerId { get; set; }
        public Nullable<int> flightId { get; set; }
        public string officeName { get; set; }
        public Nullable<int> operationId { get; set; }
        public string strServiceDate { get; set; }
        public string systemType { get; set; }
        public bool canDelete { get; set; }
        public Nullable<bool> isActive { get; set; }
        public Nullable<decimal> system_commission_value { get; set; }
        public Nullable<decimal> system_commission_ratio { get; set; }
        public Nullable<decimal> office_commission_value { get; set; }
        public Nullable<decimal> office_commission_ratio { get; set; }
        public Nullable<decimal> company_commission_value { get; set; }
        public Nullable<decimal> company_commission_ratio { get; set; }
        public Nullable<decimal> totalnet { get; set; }
        public Nullable<decimal> passengerPaid { get; set; }
        public Nullable<decimal> passengerUnpaid { get; set; }
        public Nullable<decimal> officePaid { get; set; }
        public Nullable<decimal> officeUnpaid { get; set; }
        public Nullable<decimal> airlinePaid { get; set; }
        public Nullable<decimal> airlineUnpaid { get; set; }
        public Nullable<decimal> systemPaid { get; set; }
        public Nullable<decimal> systemUnpaid { get; set; }
        public Nullable<int> exchangeId { get; set; }
        public Nullable<int> osId { get; set; }
        public Nullable<int> systemId { get; set; }
        public Nullable<decimal> syValue { get; set; }
        public Nullable<decimal> tax_ratio { get; set; }
        public Nullable<decimal> tax_value { get; set; }
        public string systemName { get; set; }
        public Nullable<int> paysideId { get; set; }
        public string currency { get; set; }
        public string currencyTotal{ get; set; }
        /// <summary>
        /// ///////////////////////////////////////
        /// </summary>
        /// <returns></returns>
        /// 
        public async Task<List<ServiceData>> GetAll()
        {

            List<ServiceData> List = new List<ServiceData>();
            bool canDelete = false;
            try
            {
                using (bookdbEntities entity = new bookdbEntities())
                {
                    List = (from S in entity.serviceData
                            join fl in entity.flights on S.flightId equals fl.flightId into JFL
                            join of in entity.office on S.officeId equals of.officeId into JOF
                            join ps in entity.passengers on S.passengerId equals ps.passengerId into JP
                            from F in JFL.DefaultIfEmpty()
                            from OFF in JOF.DefaultIfEmpty()
                            from P in JP.DefaultIfEmpty()

                            select new ServiceData()
                            {
                                serviceId = S.serviceId,
                                serviceNum = S.serviceNum,
                                type = S.type,
                                passenger = P.name + " " + P.lastName,
                                ticketNum = S.ticketNum,
                                airline = F.systems.name + "/" + F.flightTable.name,
                                officeId = S.officeId,
                                serviceDate = S.serviceDate,
                                pnr = S.pnr,
                                ticketvalueSP = S.ticketvalueSP,
                                ticketvalueDollar = S.ticketvalueDollar,
                                total = S.total,
                                priceBeforTax = S.priceBeforTax,
                                commitionRatio = S.commitionRatio,
                                commitionValue = S.commitionValue,
                                cost = S.cost,
                                saleValue = S.saleValue,
                                paid = S.paid,
                                profit = S.profit,
                                notes = S.notes,
                                state = S.state,
                                createDate = S.createDate,
                                updateDate = S.updateDate,
                                createUserId = S.createUserId,
                                updateUserId = S.updateUserId,
                                passengerId = S.passengerId,
                                flightId = S.flightId,
                                operationId = S.operationId,
                                officeName = OFF.name,
                                isActive = S.isActive,
                                canDelete = false,
                                systemType = S.systemType,
                                system_commission_value = S.system_commission_value,
                                system_commission_ratio = S.system_commission_ratio,
                                office_commission_value = S.office_commission_value,
                                office_commission_ratio = S.office_commission_ratio,
                                company_commission_value = S.company_commission_value,
                                company_commission_ratio = S.company_commission_ratio,
                                totalnet = S.totalnet,
                                passengerPaid = S.passengerPaid,
                                passengerUnpaid = S.passengerUnpaid,
                                officePaid = S.officePaid,
                                officeUnpaid = S.officeUnpaid,
                                airlinePaid = S.airlinePaid,
                                airlineUnpaid = S.airlineUnpaid,
                                systemPaid = S.systemPaid,
                                systemUnpaid = S.systemUnpaid,


                            }).ToList();

                    if (List.Count > 0)
                    {
                        for (int i = 0; i < List.Count; i++)
                        {
                            if (List[i].isActive == true)
                            {
                                int itemId = (int)List[i].serviceId;
                                var itemsI = entity.payOp.Where(x => x.serviceId == itemId).Select(b => new { b.serviceId }).FirstOrDefault();

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

        public async Task<List<ServiceData>> GetBy(string systemType,DateTime? fromDate,DateTime? toDate)
        {

            List<ServiceData> List = new List<ServiceData>();
            bool canDelete = false;
            try
            {
                DateTime now = DateTime.Now;
                string one = MainWindow.resourcemanager.GetString("singleTrip");
                string two = MainWindow.resourcemanager.GetString("roundTrip");
                using (bookdbEntities entity = new bookdbEntities())
                {
                    List = (from S in entity.serviceData
                            join fl in entity.flights on S.flightId equals fl.flightId into JFL
                            join of in entity.office on S.officeId equals of.officeId into JOF
                            join ps in entity.passengers on S.passengerId equals ps.passengerId into JP
                            join syst in entity.systems on S.systemId equals syst.systemId into Syst
                            from F in JFL.DefaultIfEmpty()
                            from OFF in JOF.DefaultIfEmpty()
                            from P in JP.DefaultIfEmpty()
                            from SYS in Syst.DefaultIfEmpty()
                            where (S.systemType == systemType)
                            select new ServiceData()
                            {
                                serviceId = S.serviceId,
                                serviceNum = S.serviceNum,
                                type = S.type,
                                passenger = P.name + " " + P.lastName,
                                ticketNum = S.ticketNum,
                                //airline = F.systems.name + "/" + F.flightTable.name,
                                airline =  F.airlines.name + "/" + F.flightTable.name + " / " + (F.type == 1 ? one : (F.type == 2 ? two : "")),
                                officeId = S.officeId,
                                serviceDate = S.serviceDate,
                                pnr = S.pnr,
                                ticketvalueSP = S.ticketvalueSP,
                                ticketvalueDollar = S.ticketvalueDollar,
                                total = S.total,
                                priceBeforTax = S.priceBeforTax,
                                commitionRatio = S.commitionRatio,
                                commitionValue = S.commitionValue,
                                cost = S.cost,
                                saleValue = S.saleValue,
                                paid = S.paid,
                                profit = S.profit,
                                notes = S.notes,
                                state = S.state,
                                createDate = S.createDate,
                                updateDate = S.updateDate,
                                createUserId = S.createUserId,
                                updateUserId = S.updateUserId,
                                passengerId = S.passengerId,
                                flightId = S.flightId,
                                operationId = S.operationId,
                                officeName = OFF.name,
                                isActive = S.isActive,
                                canDelete = false,
                                systemType = S.systemType,
                                system_commission_value = S.system_commission_value,
                                system_commission_ratio = S.system_commission_ratio,
                                office_commission_value = S.office_commission_value,
                                office_commission_ratio = S.office_commission_ratio,
                                company_commission_value = S.company_commission_value,
                                company_commission_ratio = S.company_commission_ratio,
                                totalnet = S.totalnet,
                                passengerPaid = S.passengerPaid,
                                passengerUnpaid = S.passengerUnpaid,
                                officePaid = S.officePaid,
                                officeUnpaid = S.officeUnpaid,
                                airlinePaid = S.airlinePaid,
                                airlineUnpaid = S.airlineUnpaid,
                                systemPaid = S.systemPaid,
                                systemUnpaid = S.systemUnpaid,
                                exchangeId = S.exchangeId,
                                osId = S.osId,
                                systemId = S.systemId,
                                syValue = S.syValue,
                                tax_ratio = S.tax_ratio,
                                tax_value = S.tax_value,
                                systemName = SYS.name,
                                currency = S.currency,
                                currencyTotal = "usd",
                            }).ToList();
                    List = List.Where(S => ((fromDate == null && toDate == null) ? ((DateTime)S.updateDate).Date == now.Date :
                            ((fromDate != null) ? S.updateDate == null ? false : (S.updateDate.Value.Date >= fromDate.Value.Date) : true)
                            && ((toDate != null) ? S.updateDate == null ? false : (S.updateDate.Value.Date <= toDate.Value.Date) : true)
                           )).ToList();
                    if (List.Count > 0)
                    {
                        for (int i = 0; i < List.Count; i++)
                        {
                            if (List[i].isActive == true)
                            {
                                int itemId = (int)List[i].serviceId;
                                var itemsI = entity.payOp.Where(x => x.serviceId == itemId).Select(b => new { b.serviceId }).FirstOrDefault();

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
            catch(Exception)
            {
                return List;
            }
        }
        public async Task<decimal> Save(ServiceData newitem)
        {
            serviceData newObject = new serviceData();
            newObject = JsonConvert.DeserializeObject<serviceData>(JsonConvert.SerializeObject(newitem));

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
                if (newObject.officeId == 0 || newObject.officeId == null)
                {
                    Nullable<int> id = null;
                    newObject.officeId = id;
                }


                try
                {
                    using (bookdbEntities entity = new bookdbEntities())
                    {
                        var locationEntity = entity.Set<serviceData>();
                        if (newObject.serviceId == 0)
                        {
                            newObject.createDate = DateTime.Now;
                            newObject.updateDate = newObject.createDate;
                            newObject.updateUserId = newObject.createUserId;
                            newObject.isActive = true;

                            locationEntity.Add(newObject);
                            entity.SaveChanges();
                            message = newObject.serviceId;
                        }
                        else
                        {
                            var tmpObject = entity.serviceData.Where(p => p.serviceId == newObject.serviceId).FirstOrDefault();

                            tmpObject.updateDate = DateTime.Now;
                            // tmpObject.serviceId = newObject.serviceId;
                            tmpObject.serviceNum = newObject.serviceNum;
                            tmpObject.type = newObject.type;
                            tmpObject.passenger = newObject.passenger;
                            tmpObject.ticketNum = newObject.ticketNum;
                            tmpObject.airline = newObject.airline;
                            tmpObject.officeId = newObject.officeId;
                            tmpObject.serviceDate = newObject.serviceDate;
                            tmpObject.pnr = newObject.pnr;
                            tmpObject.ticketvalueSP = newObject.ticketvalueSP;
                            tmpObject.ticketvalueDollar = newObject.ticketvalueDollar;
                            tmpObject.total = newObject.total;
                          
                            tmpObject.priceBeforTax = newObject.priceBeforTax;
                            tmpObject.commitionRatio = newObject.commitionRatio;
                            tmpObject.commitionValue = newObject.commitionValue;
                            tmpObject.cost = newObject.cost;
                            tmpObject.saleValue = newObject.saleValue;
                            tmpObject.paid = newObject.paid;
                            tmpObject.profit = newObject.profit;
                            tmpObject.notes = newObject.notes;
                            tmpObject.state = newObject.state;

                            tmpObject.createUserId = newObject.createUserId;
                            tmpObject.updateUserId = newObject.updateUserId;
                            tmpObject.passengerId = newObject.passengerId;
                            tmpObject.flightId = newObject.flightId;
                            tmpObject.operationId = newObject.operationId;
                            tmpObject.systemType = newObject.systemType;
                            tmpObject.isActive = newObject.isActive;
                            tmpObject.system_commission_value = newObject.system_commission_value;
                            tmpObject.system_commission_ratio = newObject.system_commission_ratio;
                            tmpObject.office_commission_value = newObject.office_commission_value;
                            tmpObject.office_commission_ratio = newObject.office_commission_ratio;
                            tmpObject.company_commission_value = newObject.company_commission_value;
                            tmpObject.company_commission_ratio = newObject.company_commission_ratio;
                            tmpObject.totalnet = newObject.totalnet;
                            tmpObject.passengerPaid = newObject.passengerPaid;
                            tmpObject.passengerUnpaid = newObject.passengerUnpaid;
                            tmpObject.officePaid = newObject.officePaid;
                            tmpObject.officeUnpaid = newObject.officeUnpaid;
                            tmpObject.airlinePaid = newObject.airlinePaid;
                            tmpObject.airlineUnpaid = newObject.airlineUnpaid;
                            tmpObject.systemPaid = newObject.systemPaid;
                            tmpObject.systemUnpaid = newObject.systemUnpaid;
                            tmpObject.exchangeId = newObject.exchangeId;
                            tmpObject.osId = newObject.osId;
                            tmpObject.systemId = newObject.systemId;
                            tmpObject.syValue = newObject.syValue;
                            tmpObject.tax_ratio = newObject.tax_ratio;
                            tmpObject.tax_value = newObject.tax_value;
                            tmpObject.currency = newObject.currency;
                            entity.SaveChanges();

                            message = tmpObject.serviceId;
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
        public async Task<ServiceData> GetByID(int itemId)
        {

            ServiceData item = new ServiceData();
            string message = "";

            ServiceData row = new ServiceData();
            try
            {
                using (bookdbEntities entity = new bookdbEntities())
                {
                    item = (from S in entity.serviceData
                            join fl in entity.flights on S.flightId equals fl.flightId into JFL
                            join of in entity.office on S.officeId equals of.officeId into JOF
                            join ps in entity.passengers on S.passengerId equals ps.passengerId into JP
                            join syst in entity.systems on S.systemId equals syst.systemId into Syst
                            from F in JFL.DefaultIfEmpty()
                            from OFF in JOF.DefaultIfEmpty()
                            from P in JP.DefaultIfEmpty()
                            from SYS in Syst.DefaultIfEmpty()
                            where (S.serviceId == itemId)
                            select new ServiceData()
                            {
                                serviceId = S.serviceId,
                                serviceNum = S.serviceNum,
                                type = S.type,
                                passenger = P.name + " " + P.lastName,
                                ticketNum = S.ticketNum,
                                airline = F.systems.name + "/" + F.flightTable.name,
                                officeId = S.officeId,
                                serviceDate = S.serviceDate,
                                pnr = S.pnr,
                                ticketvalueSP = S.ticketvalueSP,
                                ticketvalueDollar = S.ticketvalueDollar,
                                total = S.total,
                                priceBeforTax = S.priceBeforTax,
                                commitionRatio = S.commitionRatio,
                                commitionValue = S.commitionValue,
                                cost = S.cost,
                                saleValue = S.saleValue,
                                paid = S.paid,
                                profit = S.profit,
                                notes = S.notes,
                                state = S.state,
                                createDate = S.createDate,
                                updateDate = S.updateDate,
                                createUserId = S.createUserId,
                                updateUserId = S.updateUserId,
                                passengerId = S.passengerId,
                                flightId = S.flightId,
                                operationId = S.operationId,
                                officeName = OFF.name,
                                isActive = S.isActive,
                                canDelete = false,
                                systemType = S.systemType,
                                system_commission_value = S.system_commission_value,
                                system_commission_ratio = S.system_commission_ratio,
                                office_commission_value = S.office_commission_value,
                                office_commission_ratio = S.office_commission_ratio,
                                company_commission_value = S.company_commission_value,
                                company_commission_ratio = S.company_commission_ratio,
                                totalnet = S.totalnet,
                                passengerPaid = S.passengerPaid,
                                passengerUnpaid = S.passengerUnpaid,
                                officePaid = S.officePaid,
                                officeUnpaid = S.officeUnpaid,
                                airlinePaid = S.airlinePaid,
                                airlineUnpaid = S.airlineUnpaid,
                                systemPaid = S.systemPaid,
                                systemUnpaid = S.systemUnpaid,
                                exchangeId = S.exchangeId,
                                osId = S.osId,
                                systemId = S.systemId,
                                syValue = S.syValue,
                                tax_ratio = S.tax_ratio,
                                tax_value = S.tax_value,
                                systemName = SYS.name,
                                currency=S.currency,
                            }).FirstOrDefault();
                    return row;
                }

            }
            catch (Exception ex)
            {
                row = new ServiceData();
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
                        serviceData objectDelete = entity.serviceData.Find(id);
                        List<serviceDataFiles> fileslist = entity.serviceDataFiles.Where(f => f.serviceId == objectDelete.serviceId).ToList();
                        //remove files first
                        //code here
                        //remove rows from db
                        entity.serviceDataFiles.RemoveRange(fileslist);
                        entity.serviceData.Remove(objectDelete);
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
                        serviceData objectDelete = entity.serviceData.Find(id);

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

        public async Task<string> generateCodeNumber(string type)
        {
            int sequence = await GetLastNumOfCode(type);
            sequence++;
            string strSeq = sequence.ToString();
            if (sequence <= 999999)
                strSeq = sequence.ToString().PadLeft(6, '0');
            string transNum = type.ToUpper() + "-" + strSeq;
            return transNum;
        }

        public async Task<int> GetLastNumOfCode(string type)
        {

            try
            {
                List<string> numberList;
                int lastNum = 0;
                using (bookdbEntities entity = new bookdbEntities())
                {
                    numberList = entity.serviceData.Where(b => b.serviceNum.Contains(type + "-")).Select(b => b.serviceNum).ToList();

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
            catch
            {
                return 0;
            }
        }
    }
}
