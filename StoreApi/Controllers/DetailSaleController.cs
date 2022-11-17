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
    public class DetailSaleController : ApiController
    {
        private StoreEntities BD = new StoreEntities();
        private DetailSale[] detailSales;
        private DetailSale detailSale;
        private string message;
        private ResponseModel response;


        // GET: api/DetailSale
        public IHttpActionResult Get()
        {
            try
            {
                detailSales = BD.DetailSale.ToList().ToArray();
                return Ok(detailSales);
            }
            catch (Exception ex)
            {
                message = ex.InnerException.InnerException.Message.ToString();
                return BadRequest(message);
            }
        }

        // GET: api/DetailSale/5
        public IHttpActionResult Get(int id)
        {
            try
            {
                detailSale = BD.DetailSale.Find(id);
                return Ok(detailSale);
            }
            catch (Exception ex)
            {
                message = ex.InnerException.InnerException.Message.ToString();
                return BadRequest(message);
            }
        }

        // POST: api/DetailSale
        public IHttpActionResult Post(DetailSale model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                BD.DetailSale.Add(model);
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

        // PUT: api/DetailSale/5
        public IHttpActionResult Put(int id, DetailSale model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                detailSale = BD.DetailSale.Find(id);
                if (detailSale == null)
                {
                    return BadRequest("Record not found");
                }
                detailSale.IdProduct = model.IdProduct;
                detailSale.Count = model.Count;
                detailSale.DetailSale_IdSale = model.DetailSale_IdSale;
                BD.SaveChanges();
                response = new ResponseModel { MessageResponse = "Success, data storage! ", BodyResponse = detailSale };
                return Ok(response);
            }
            catch (Exception ex)
            {
                message = ex.InnerException.InnerException.Message.ToString();
                return BadRequest(message);
            }
        }

        // DELETE: api/DetailSale/5
        public IHttpActionResult Delete(int id)
        {
            try
            {
                detailSale = BD.DetailSale.Find(id);
                if (detailSale == null)
                {
                    return BadRequest("Record not found");
                }
                BD.DetailSale.Remove(detailSale);
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
