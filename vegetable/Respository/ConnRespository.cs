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
            _context = context;
        }

        public void Create(T Enity)
        {
            _context.Entry(Enity).State = EntityState.Added;//新增，entry(泛型的變數)
        }

        public void Delete(T Enity)
        {
            _context.Entry(Enity).State = EntityState.Deleted;//刪除，entry(泛型的變數)
        }

        public IEnumerable<T> GetAll(string sql)
        {
            
           
            using (SqlConnection conn= new SqlConnection(_context.Database.Connection.ConnectionString))/*
                連接字串
                 
                 */ {
                
                var producets = conn.Query<T>(sql);//產生搜尋的IEnumerable，Query(可放自定義的SQL文字)
                return producets;
            }
            
        }

        public void Load(IQueryable<T> Enity)
        {
            Enity.Load();//載入此表格，用於有關聯性的表格無法刪除時
        }

      
        public void Update(T Enity)
        {
            _context.Entry(Enity).State = EntityState.Modified;//修改，entry(泛型的變數)
        }

        public static implicit operator ConnRespository<T>(ConnRespository<ProducetDetil> v)
        {
            throw new NotImplementedException();
        }
    }
}