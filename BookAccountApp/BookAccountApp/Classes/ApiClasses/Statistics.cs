﻿using Newtonsoft.Json;
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
    public class BookSts 
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
        public string recipient { get; set; }
        public string recivedFrom { get; set; }
        public string sideAr { get; set; }
        public string side { get; set; }
        public string strCreateDate { get; set; }
        

    }
    public class PaymentsSts 
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
        public string serviceNum { get; set; }
        public string ticketNum { get; set; }
        public Nullable<decimal> deserved { get; set; }
        public string purpose { get; set; }
        public string stropDate { get; set; }

      
  
  
        public Nullable<decimal> priceBeforTax { get; set; }
    }
    public class Statistics
    {
        public async Task<List<BookSts>> GetBookProfit(DateTime? fromDate, DateTime? toDate)
        {

            List<BookSts> List = new List<BookSts>();
            string syr = MainWindow.resourcemanager.GetString("syr");
            string soto = MainWindow.resourcemanager.GetString("soto");
            try
            {
                DateTime now = DateTime.Now;
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
                           where (S.state == "confirmd")
                            select new BookSts()
                            {
                                serviceId = S.serviceId,
                                serviceNum = S.serviceNum,
                                type = S.type,
                                passenger = P.name + " " + P.lastName,
                                ticketNum = S.ticketNum,
                                airline = SYS.name + "/" + F.flightTable.name + "/" +(S.systemType=="syr"?syr:soto),
                                officeId = S.officeId,
                                serviceDate = S.serviceDate,
                                pnr = S.pnr,
                                ticketvalueSP = S.ticketvalueSP,
                                ticketvalueDollar = S.ticketvalueDollar,
                                total = S.total,
                                //priceBeforTax =S.systemType=="soto"? (S.priceBeforTax*S.syValue) : S.priceBeforTax,
                                 priceBeforTax =S.priceBeforTax,
                                commitionRatio = S.commitionRatio,
                                commitionValue = S.commitionValue,
                                cost = S.cost,
                                saleValue = S.saleValue,
                                paid = S.paid,
                                profit = S.profit,//
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
                                office_commission_value =  S.office_commission_value,//
                                office_commission_ratio = S.office_commission_ratio,
                                company_commission_value =  S.company_commission_value,//
                                company_commission_ratio = S.company_commission_ratio,
                                totalnet =S.totalnet,//
                                passengerPaid = S.passengerPaid,
                                passengerUnpaid = S.passengerUnpaid,
                                officePaid = S.officePaid,//
                                officeUnpaid =  S.officeUnpaid,//
                                airlinePaid = S.airlinePaid,
                                airlineUnpaid = S.airlineUnpaid,
                                systemPaid = S.systemPaid,//
                                systemUnpaid = S.systemUnpaid,//
                                exchangeId = S.exchangeId,
                                osId = S.osId,
                                systemId = S.systemId,
                                syValue = S.syValue,
                                tax_ratio = S.tax_ratio,
                                tax_value = S.tax_value,//
                                systemName = SYS.name,

                            }).ToList();
                    List = List.Where(S => ((fromDate == null && toDate == null) ? true :
                            ((fromDate != null) ? S.createDate == null ? false : (S.createDate.Value.Date >= fromDate.Value.Date) : true)
                            && ((toDate != null) ? S.createDate == null ? false : (S.createDate.Value.Date <= toDate.Value.Date) : true)
                           )).ToList();
                  
                    return List;
                }

            }
            catch (Exception)
            {
                return List;
            }
        }
        public async Task<List<PaymentsSts>> GetPayments(DateTime? fromDate, DateTime? toDate)
        {

            List<PaymentsSts> List = new List<PaymentsSts>();
            
            try
            {
                using (bookdbEntities entity = new bookdbEntities())
                {
                    List = (from S in entity.payOp
                           // where S.opType == opType
                            select new PaymentsSts()
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
                            }).ToList();
                    List = List.Where(S => ((fromDate == null && toDate == null) ? true :
                           ((fromDate != null) ? S.createDate == null ? false : (S.createDate.Value.Date >= fromDate.Value.Date) : true)
                           && ((toDate != null) ? S.createDate == null ? false : (S.createDate.Value.Date <= toDate.Value.Date) : true)
                          )).ToList();
                    return List;
                }

            }
            catch
            {
                return List;
            }

        }
        public List<PaymentsSts> GetPaySystemsTransfer(DateTime? fromDate, DateTime? toDate)
        {

            List<PaymentsSts> List = new List<PaymentsSts>();

            try
            {
                using (bookdbEntities entity = new bookdbEntities())
                {
                    List = (from S in entity.payOp
                            where ((S.opType == "p" && (S.side == "soto" || S.side == "syr")) ||
                            (S.opType == "p" && S.side == "system" && (S.fromSide == "soto" || S.fromSide == "syr") && S.processType == "book"))
                            select new PaymentsSts()
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
                                sideAr = S.side == "system" ? S.fromSide : S.side,
                                flightId = S.flightId,
                                opName = S.opName,
                                systemName = S.systems.name,
                                airline =S.processType=="book"? S.systems.name + "/" + S.flights.flightTable.name + "/" + S.paySides.sideAr : S.paySides.sideAr,
                                officeName= S.officeId==null?"":S.office.name,
                                currency=S.currency,
                            }).ToList();
                    List = List.Where(S => ((fromDate == null && toDate == null) ? true :
                           ((fromDate != null) ? S.createDate == null ? false : (S.createDate.Value.Date >= fromDate.Value.Date) : true)
                           && ((toDate != null) ? S.createDate == null ? false : (S.createDate.Value.Date <= toDate.Value.Date) : true)
                          )).ToList();
                    return List;
                }

            }
            catch
            {
                return List;
            }

        }
        //private string urimainpath = "Statistics/";
        ///// <summary>
        ///// ///////////////////////////////////////
        ///// </summary>
        ///// <returns></returns>
        ///// 


        //public async Task<List<BookSts>> GetByAgentId(int agentId)
        //{
        //   List<BookSts> list = new List<BookSts>();
        //    Dictionary<string, string> parameters = new Dictionary<string, string>();
        //    parameters.Add("agentId", agentId.ToString());
        //    //#################
        //    IEnumerable<Claim> claims = await APIResult.getList(urimainpath + "GetByAgentId", parameters);

        //    foreach (Claim c in claims)
        //    {
        //        if (c.Type == "scopes")
        //        {
        //            list.Add(JsonConvert.DeserializeObject<BookSts>(c.Value, new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy" }));
        //        }
        //    }
        //    return list;

        //}

        //public async Task<List<BookSts>> GetAllBooks()
        //{
        //    List<BookSts> list = new List<BookSts>();
        //    Dictionary<string, string> parameters = new Dictionary<string, string>();
        //   // parameters.Add("agentId", agentId.ToString());
        //    //#################
        //    IEnumerable<Claim> claims = await APIResult.getList(urimainpath + "GetAllBooks");

        //    foreach (Claim c in claims)
        //    {
        //        if (c.Type == "scopes")
        //        {
        //            list.Add(JsonConvert.DeserializeObject<BookSts>(c.Value, new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy" }));
        //        }
        //    }
        //    return list;

        //}

        ////payments
        //public async Task<List<PaymentsSts>> GetPaymentsByAgentId(int agentId)
        //{
        //    List<PaymentsSts> list = new List<PaymentsSts>();
        //    Dictionary<string, string> parameters = new Dictionary<string, string>();
        //    parameters.Add("agentId", agentId.ToString());
        //    //#################
        //    IEnumerable<Claim> claims = await APIResult.getList(urimainpath + "GetPaymentsByAgentId", parameters);

        //    foreach (Claim c in claims)
        //    {
        //        if (c.Type == "scopes")
        //        {
        //            list.Add(JsonConvert.DeserializeObject<PaymentsSts>(c.Value, new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy" }));
        //        }
        //    }
        //    return list;

        //}

        //public async Task<List<PaymentsSts>> GetAllPayments()
        //{
        //    List<PaymentsSts> list = new List<PaymentsSts>();
        //    Dictionary<string, string> parameters = new Dictionary<string, string>();
        //    // parameters.Add("agentId", agentId.ToString());
        //    //#################
        //    IEnumerable<Claim> claims = await APIResult.getList(urimainpath + "GetAllPayments");

        //    foreach (Claim c in claims)
        //    {
        //        if (c.Type == "scopes")
        //        {
        //            list.Add(JsonConvert.DeserializeObject<PaymentsSts>(c.Value, new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy" }));
        //        }
        //    }
        //    return list;

        //}

        ///// ////



    }
}
