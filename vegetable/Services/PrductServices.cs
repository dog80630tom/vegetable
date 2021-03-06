﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using vegetable.Controllers;
using vegetable.Models;
using vegetable.Models.ViewModels;

namespace vegetable.Services
{
    public class PrductServices
    {
        ItemContext item = new ItemContext();
        ItemContext item2 = new ItemContext();
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
        public ErrorMessage addProduct(Product product,PicDetail pic)
        {
            ErrorMessage error = new ErrorMessage();
            error.IsSuccess = true;
            item.Products.Add(product);
            item.SaveChanges();
            item.Database.ExecuteSqlCommand($"insert into PicDetails values({product.ProductID},'{pic.PicUrl}')");
            //using (var data = item.Database.BeginTransaction())
            //{
            //    try
            //    {
            //        data.Commit();
            //    }
            //    catch (Exception ex)
            //    {
            //        error.Message = ex.Message;
            //        error.IsSuccess = false;
            //        data.Rollback();

            //        return error;
            //    }
            //}
            return error;
        }

        public ErrorMessage addPic(PicDetail pic,int id)
        {
            ErrorMessage error = new ErrorMessage();
            error.IsSuccess = true;
            //using (var data = item.Database.BeginTransaction())
            //{
            //    try
            //    {
            //        pic.ProductID = id;
            //        item.PicDetails.Add(pic);
            //        item.SaveChanges();
            //        data.Commit();
            //    }
            //    catch (Exception ex)
            //    {
            //        error.Message = ex.Message;
            //        error.IsSuccess = false;
            //        data.Rollback();

            //        return error;
            //    }
            //}
            return error;
        }
        public ErrorMessage EditProduct(Product product, PicDetail pic)
        {
            ErrorMessage error = new ErrorMessage();
            error.IsSuccess = true;
            try
            {
                item.Entry(product).State = EntityState.Modified;
                item.Entry(pic).State = EntityState.Modified;
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

       
        public ErrorMessage DeleteProduct(IQueryable<Product> a, IQueryable<PicDetail> picDetail)
        {
            ErrorMessage error = new ErrorMessage();

            using (var data = item.Database.BeginTransaction())
            {
                error.IsSuccess = true;
                try
                {
                    item.Entry(picDetail.FirstOrDefault()).State = EntityState.Deleted;
                    item.SaveChanges();

                    item.Entry(a.FirstOrDefault()).State = EntityState.Deleted;
                    item.SaveChanges();

                    data.Commit();
                }
                catch (Exception ex)
                {

                    error.IsSuccess = false;
                    error.Message = ex.Message;
                    data.Rollback();
                    return error;
                }
            }
            return error;
        }
    }
}