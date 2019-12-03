using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using vegetable.Controllers;
using vegetable.Models;

namespace vegetable.Services
{
    public class CategoryServices
    {
        MyDBContext item = new MyDBContext();
        public ErrorMessage EditCategory(Category category)
        {
            ErrorMessage error = new ErrorMessage();
            error.IsSuccess = true;
            try
            {
                item.Entry(category).State = EntityState.Modified;
                item.SaveChanges();
            }
            catch (Exception ex)
            {
                error.IsSuccess = false;
                error.Message = ex.Message;
                return error;
            }
            return error;
        }

        public ErrorMessage DeleteCategory(Category category)
        {
            ErrorMessage error = new ErrorMessage();
            error.IsSuccess = true;
            try
            {
                Category temp = item.Categories.Find(category.CategoryID);
                item.Categories.Remove(temp);   
                //item.Entry(category).State = EntityState.Deleted;
                item.SaveChanges();
            }
            catch (Exception ex)
            {
                error.IsSuccess = false;
                error.Message = ex.Message;
                return error;
            }
            return error;
        }

        public ErrorMessage CreateCategory(Category category)
        {
            ErrorMessage error = new ErrorMessage();
            error.IsSuccess = true;
            using (var data = item.Database.BeginTransaction())
            {
                try
                {
                    item.Categories.Add(category);
                    item.SaveChanges();
                    data.Commit();
                }
                catch (Exception ex)
                {
                    error.Message = ex.Message;
                    error.IsSuccess = false;
                    data.Rollback();

                    return error;
                }
                return error;
            }
                
        }
    }
}