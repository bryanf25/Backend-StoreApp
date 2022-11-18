using StoreApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace StoreApi.Controllers
{
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    public class SaleController : ApiController
    {

        private StoreEntities BD = new StoreEntities();
        private Sale[] sales;
        private Sale sale;
        private string message;
        private ResponseModel response;

        // GET: api/Sale
        public IHttpActionResult Get()
        {
            try
            {
                BD.Configuration.ProxyCreationEnabled = false;
                sales = BD.Sale.ToList().ToArray();
                return Ok(sales);
            }
            catch (Exception ex)
            {
                message = ex.InnerException.InnerException.Message.ToString();
                return BadRequest(message);
            }
        }

        // GET: api/Sale/5
        public IHttpActionResult Get(int id)
        {
            try
            {
                BD.Configuration.ProxyCreationEnabled = false;
                sale = BD.Sale.Find(id);
                return Ok(sale);
            }
            catch (Exception ex)
            {
                message = ex.InnerException.InnerException.Message.ToString();
                return BadRequest(message);
            }
        }

        // POST: api/Sale
        public IHttpActionResult Post(Sale model)
        {
            BD.Configuration.ProxyCreationEnabled = false;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                BD.Sale.Add(model);
                BD.SaveChanges();
                response = new ResponseModel { MessageResponse = "Success, data storage! ", BodyResponse = model };
                return Ok(response);
            }
            catch (Exception ex)
            {
                message = ex.InnerException.InnerException.Message.ToString();
                return BadRequest(message);
            }
        }

        // PUT: api/Sale/5
        public IHttpActionResult Put(int id, Sale model)
        
{
            BD.Configuration.ProxyCreationEnabled = false;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                sale = BD.Sale.Find(id);
                if (sale == null)
                {
                    return BadRequest("Record not found");
                }
                sale.Date = model.Date;
                sale.TotalValue = model.TotalValue;
                sale.Sale_Cedula = model.Sale_Cedula;
                BD.SaveChanges();
                response = new ResponseModel { MessageResponse = "Success, data storage! ", BodyResponse = sale };
                return Ok(response);
            }
            catch (Exception ex)
            {
                message = ex.InnerException.InnerException.Message.ToString();
                return BadRequest(message);
            }
        }

        // DELETE: api/Sale/5
        public IHttpActionResult Delete(int id)
        {
            try
            {
                sale = BD.Sale.Find(id);
                if (sale == null)
                {
                    return BadRequest("Record not found");
                }
                BD.Sale.Remove(sale);
                BD.SaveChanges();
                return Ok("Success, data Eliminated! ");
            }
            catch (Exception ex)
            {
                message = ex.InnerException.InnerException.Message.ToString();
                return BadRequest(message);
            }
        }
    }
}
