using StoreApi.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using System.Web.Http.Cors;

namespace StoreApi.Controllers
{
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    public class ProductController : ApiController
    {
        private StoreEntities BD = new StoreEntities();
        private Product[] products;
        private Product product;
        private string message;
        private ResponseModel response;

        

        // GET: api/Product
        public IHttpActionResult Get()
        {
            try
            {
                BD.Configuration.ProxyCreationEnabled = false;
                products = BD.Product.ToList().ToArray();
                return Ok(products);
            }
            catch (Exception ex)
            {
                message = ex.InnerException.InnerException.Message.ToString();
                return BadRequest(message);
            }
        }

        // GET: api/Product/5
        public IHttpActionResult Get(int id)
        {
            try
            {
                BD.Configuration.ProxyCreationEnabled = false;
                product = BD.Product.Find(id);
                return Ok(product);
            }
            catch (Exception ex)
            {
                message = ex.InnerException.InnerException.Message.ToString();
                return BadRequest(message);
            }
        }

        // POST: api/Product
        public IHttpActionResult Post(Product model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                BD.Product.Add(model);
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

        // PUT: api/Product/5
        public IHttpActionResult Put( int id,Product model)
        {
            BD.Configuration.ProxyCreationEnabled = false;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                product = BD.Product.Find(id);
                if (product == null)
                {
                    return BadRequest("Record not found");
                }
                product.Name = model.Name;
                product.UnitValue = model.UnitValue;
                product.DetailSale = model.DetailSale;
                product.Description = model.Description;
                product.Image = model.Image;
                BD.SaveChanges();
                response = new ResponseModel { MessageResponse = "Success, data storage! ", BodyResponse = product };
                return Ok(response);
            }
            catch (Exception ex)
            {
                message = ex.InnerException.InnerException.Message.ToString();
                return BadRequest(message);
            }
        }

        // DELETE: api/Product/5
        public IHttpActionResult Delete(int id)
        {
            try
            {
                product = BD.Product.Find(id);
                if (product == null)
                {
                    return BadRequest("Record not found");
                }
                BD.Product.Remove(product);
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
