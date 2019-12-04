using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using vegetable.Controllers;
using vegetable.Models.ViewModels;
using Dapper;

namespace vegetable.Respository
{
    public class ConnRespository<T> where T:class
    {
        private ItemContext _context ;
        //static string connstring = @"data source=vegetable.database.windows.net;initial catalog=vegetableDB;user id=sean200365;password=800824arcARC;MultipleActiveResultSets=True;App=EntityFramework";
        public ConnRespository(ItemContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException();

            }
            context =_context;
        }

        public void Create(T Enity)
        {
            _context.Entry(Enity).State = EntityState.Added;
        }

        public void Delete(T Enity)
        {
            _context.Entry(Enity).State = EntityState.Deleted;
        }

        public IEnumerable<T> GetAll(T Enity,string sql)
        {
           
            using (SqlConnection conn= new SqlConnection(_context.Database.Connection.ConnectionString)) {
                
                var producets = conn.Query<T>(sql);
                return producets;
            }
            
        }

        public void Load(IQueryable<T> Enity)
        {
            Enity.Load();
        }

      
        public void Update(T Enity)
        {
            _context.Entry(Enity).State = EntityState.Modified;
        }

        public static implicit operator ConnRespository<T>(ConnRespository<ProducetDetil> v)
        {
            throw new NotImplementedException();
        }
    }
}