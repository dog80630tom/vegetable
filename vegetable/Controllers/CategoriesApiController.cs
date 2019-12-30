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
using vegetable.Models;

namespace vegetable.Controllers
{
    public class CategoriesApiController : ApiController
    {
        private ItemContext db = new ItemContext();

        // GET: api/CategoriesApi
        public IQueryable GetCategories()
        {
            //撈出三層分類
            var rtnCategory = db.Categories.Where(c1 => c1.ParentID== null)
                                            .Select(c1 => new 
                                                { 
                                                  id = c1.CategoryID,
                                                  label = c1.CategoryName,
                                                  listname=c1.CategoryName.Replace(" ","-"),
                                                  children = db.Categories.Where(c2 => c2.ParentID != null && c2.ParentID == c1.CategoryID)
                                                  .Select(c2 => new
                                                  {
                                                      id = c2.CategoryID,
                                                      label = c2.CategoryName,
                                                      listname = c2.CategoryName.Replace(" ","-").Replace("&","and"),
                                                      children = db.Categories.Where(c3 => c3.ParentID != null && c3.ParentID == c2.CategoryID)
                                                      .Select(c3=>new { 
                                                            id=c3.CategoryID,
                                                            label=c3.CategoryName
                                                      })
                                                  })
                                            }
                                            );
            return rtnCategory;
        }

        // GET: api/CategoriesApi/5
        [ResponseType(typeof(Category))]
        public IHttpActionResult GetCategory(int id)
        {
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        // PUT: api/CategoriesApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCategory(int id, Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != category.CategoryID)
            {
                return BadRequest();
            }

            db.Entry(category).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(id))
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

        // POST: api/CategoriesApi
        [ResponseType(typeof(Category))]
        public IHttpActionResult PostCategory(Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Categories.Add(category);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = category.CategoryID }, category);
        }

        // DELETE: api/CategoriesApi/5
        [ResponseType(typeof(Category))]
        public IHttpActionResult DeleteCategory(int id)
        {
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }

            db.Categories.Remove(category);
            db.SaveChanges();

            return Ok(category);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CategoryExists(int id)
        {
            return db.Categories.Count(e => e.CategoryID == id) > 0;
        }
    }
}