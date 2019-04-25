//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.Entity;
//using System.Data.Entity.Infrastructure;
//using System.Linq;
//using System.Net;
//using System.Net.Http;
//using System.Web.Http;
//using System.Web.Http.Description;
//using WebApplication1.Models;

//namespace WebApplication1.Controllers
//{
//    public class ProductsController : ApiController
//    {
//        private ApplicationDbContext db = new ApplicationDbContext();

//        // GET: api/Products
//        public IQueryable<Products> GetProducts()
//        {
//            return db.Products;
//        }

//        // GET: api/Products/5
//        [ResponseType(typeof(Products))]
//        public IHttpActionResult GetProducts(int id)
//        {
//            Products products = db.Products.Find(id);
//            if (products == null)
//            {
//                return NotFound();
//            }

//            return Ok(products);
//        }

//        // PUT: api/Products/5
//        [ResponseType(typeof(void))]
//        public IHttpActionResult PutProducts([FromBody]Products products)
//        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }

//            //if (id != products.ProductId)
//            //{
//            //    return BadRequest();
//            //}

//            db.Entry(products).State = EntityState.Modified;

//            try
//            {
//                db.SaveChanges();
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                if (!ProductsExists(products.ProductId))
//                {
//                    return NotFound();
//                }
//                else
//                {
//                    throw;
//                }
//            }

//            return StatusCode(HttpStatusCode.NoContent);
//        }

//        // POST: api/Products
//        [ResponseType(typeof(Products))]
//        public IHttpActionResult PostProducts([FromBody]Products products)
//        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }

//            db.Products.Add(products);
//            db.SaveChanges();

//            return CreatedAtRoute("DefaultApi", new { id = products.ProductId }, products);
//        }

//        // DELETE: api/Products/5
//        [ResponseType(typeof(Products))]
//        public IHttpActionResult DeleteProducts(int id)
//        {
//            Products products = db.Products.Find(id);
//            if (products == null)
//            {
//                return NotFound();
//            }

//            db.Products.Remove(products);
//            db.SaveChanges();

//            return Ok(products);
//        }

//        protected override void Dispose(bool disposing)
//        {
//            if (disposing)
//            {
//                db.Dispose();
//            }
//            base.Dispose(disposing);
//        }

//        private bool ProductsExists(int id)
//        {
//            //return db.Products.Count(e => e.ProductId == id) > 0;
//            return 1;
//        }
//    }
//}