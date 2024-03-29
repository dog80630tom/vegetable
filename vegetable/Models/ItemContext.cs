﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Web;
using vegetable.Models;
using vegetable.Models.ViewModels;
using System.Data.Entity;

namespace vegetable.Controllers
{
    public class ItemContext : DbContext
    {
        public ItemContext() : base("name=DefaultConnection")
        {
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProducetDetail> ProducetDetils { get; set; }
        public DbSet<PicDetail> PicDetails { get; set; }
        public DbSet<Category>  Categories { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Customer_Question> Customer_Questions { get; set; }
        public DbSet<HomePageAD> HomePageADs { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<AdminRespond> AdminResponds { get; set; }
        public DbSet<CartDetail> CartDetails { get; set; }
        public DbSet<Customer_Review> Customer_Reviews { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<WishList> WishLists { get; set; }

        public System.Data.Entity.DbSet<vegetable.Models.ViewModels.NewProductDetail> NewProductDetails { get; set; }
    }
}