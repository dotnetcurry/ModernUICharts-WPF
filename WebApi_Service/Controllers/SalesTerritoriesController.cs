using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebApi_Service.Models;

namespace WebApi_Service.Controllers
{
    public class SalesTerritoriesController : ApiController
    {
        private AdventureWorks2012Entities db = new AdventureWorks2012Entities();

        // GET: api/SalesTerritories
        public IQueryable<SalesTerritory> GetSalesTerritories()
        {
            return db.SalesTerritories;
        }

        // GET: api/SalesTerritories/5
        [ResponseType(typeof(SalesTerritory))]
        public IHttpActionResult GetSalesTerritory(int id)
        {
            SalesTerritory salesTerritory = db.SalesTerritories.Find(id);
            if (salesTerritory == null)
            {
                return NotFound();
            }

            return Ok(salesTerritory);
        }

        // PUT: api/SalesTerritories/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSalesTerritory(int id, SalesTerritory salesTerritory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != salesTerritory.TerritoryID)
            {
                return BadRequest();
            }

            db.Entry(salesTerritory).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SalesTerritoryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/SalesTerritories
        [ResponseType(typeof(SalesTerritory))]
        public IHttpActionResult PostSalesTerritory(SalesTerritory salesTerritory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.SalesTerritories.Add(salesTerritory);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = salesTerritory.TerritoryID }, salesTerritory);
        }

        // DELETE: api/SalesTerritories/5
        [ResponseType(typeof(SalesTerritory))]
        public IHttpActionResult DeleteSalesTerritory(int id)
        {
            SalesTerritory salesTerritory = db.SalesTerritories.Find(id);
            if (salesTerritory == null)
            {
                return NotFound();
            }

            db.SalesTerritories.Remove(salesTerritory);
            db.SaveChanges();

            return Ok(salesTerritory);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SalesTerritoryExists(int id)
        {
            return db.SalesTerritories.Count(e => e.TerritoryID == id) > 0;
        }
    }
}