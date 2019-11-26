using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using vegetable.Models;

namespace vegetable.Controllers
{
    public class ItemContext : DbContext
    {
        public ItemContext() : base("name=DefaultConnection")
        {
        }
        public DbSet<Product> Products { get; set; }

        public System.Data.Entity.DbSet<vegetable.Models.ViewModels.ProducetDetil> ProducetDetils { get; set; }
        public System.Data.Entity.DbSet<PicDetail> PicDetails { get; set; }
        public System.Data.Entity.DbSet<Category>  Categories { get; set; }

    }
}