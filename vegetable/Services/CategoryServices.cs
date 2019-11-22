using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using vegetable.Controllers;
using vegetable.Models;

namespace vegetable.Services
{
    public class CategoryServices
    {
        ItemContext item = new ItemContext();
        public ErrorMessage EditProduct(Category category)
        {
            ErrorMessage error = new ErrorMessage();
            error.IsSuccess = true;
            try
            {
                item.Entry(category).State = System.Data.Entity.EntityState.Modified;
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
    }
}