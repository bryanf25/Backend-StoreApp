using StoreApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace StoreApi.Controllers
{
    public class CustomerController : ApiController
    {

        private StoreEntities BD = new StoreEntities();
        private Customer[] customers;
        private Customer customer;
        private string message;
        private ResponseModel response;

        // GET: api/Customer
        public IHttpActionResult Get()
        {
            try
            {
                customers = BD.Customer.ToList().ToArray();
                return Ok(customers);
            }
            catch (Exception ex)
            {
                message = ex.InnerException.InnerException.Message.ToString();
                return BadRequest(message);
            }
        }

        // GET: api/Customer/5
        public IHttpActionResult Get(int id)
        {
            try
            {
                customer = BD.Customer.Find(id);
                return Ok(customer);
            }
            catch (Exception ex)
            {
                message = ex.InnerException.InnerException.Message.ToString();
                return BadRequest(message);
            }
        }

        // POST: api/Customer
        public IHttpActionResult Post(Customer model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                BD.Customer.Add(model);
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

        // PUT: api/Customer/5
        public IHttpActionResult Put(int id, Customer model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                customer = BD.Customer.Find(id);
                if (customer == null)
                {
                    return BadRequest("Record not found");
                }
                customer.Name = model.Name;
                customer.LastName = model.LastName;
                customer.Phone = model.Phone;
                BD.SaveChanges();
                response = new ResponseModel { MessageResponse = "Success, data storage! ", BodyResponse = customer };
                return Ok(response);
            }
            catch (Exception ex)
            {
                message = ex.InnerException.InnerException.Message.ToString();
                return BadRequest(message);
            }
        }

        // DELETE: api/Customer/5
        public IHttpActionResult Delete(int id)
        {
            try
            {
                customer = BD.Customer.Find(id);
                if (customer == null)
                {
                    return BadRequest("Record not found");
                }
                BD.Customer.Remove(customer);
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
