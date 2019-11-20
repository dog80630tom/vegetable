﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using vegetable.Controllers;
using vegetable.Models;

namespace vegetable.Services
{
    public class PrductServices
    {
        ItemContext item = new ItemContext();
        //public Product GetProduct( string ProductName, String CategoryName, string ProductDescription, int UnitsInStock,int ProductPrice)
        //{
        //    Product product = new Product() {  CategoryName = ProductName, ProductDescription = CategoryName, ProductName = ProductName, UnitsInStock = UnitsInStock, ProductPrice= ProductPrice };
        //    return product;
        //}
        //public PicDetail GetPicDetail(string PicUrl )
        //{
        //    PicDetail PicDetail = new PicDetail() { PicUrl= PicUrl };
        //    return PicDetail;
        //}
        //public Category GetCategory(string CategoryName,string CategoryPic,string CategoryDescription)
        //{
        //    Category Category = new Category() {  CategoryDescription=CategoryDescription, CategoryName=CategoryName, CategoryPic=CategoryPic };
        //    return Category;
        //}
        public ErrorMessage addProduct(Product product,Category category,PicDetail pic) {
            ErrorMessage error = new ErrorMessage();
            error.IsSuccess = true;
            using (var data = item.Database.BeginTransaction())
            {
                try
                {
                    
                    item.Categories.Add(category);
                    item.SaveChanges();
                    product.CategoryID = item.Categories.FirstOrDefault(x=>x.CategoryPic==category.CategoryPic).CategoryID;
                    item.Products.Add(product);
                    item.SaveChanges();
                    
                     pic.ProductID = product.CategoryID;
                    item.PicDetails.Add(pic);
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
            }
            return error;
        }


    }
}