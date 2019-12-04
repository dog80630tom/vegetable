using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using vegetable.Controllers;
using vegetable.Models;

namespace vegetable.Services
{
    public class MemberServices
    {
        ItemContext item = new ItemContext();
        public ErrorMessage EditMember(Member Member)
        {
            ErrorMessage error = new ErrorMessage();
            error.IsSuccess = true;
            try
            {
                item.Entry(Member).State = EntityState.Modified;
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

        public ErrorMessage DeleteMember(Member Member)
        {
            ErrorMessage error = new ErrorMessage();
            error.IsSuccess = true;
            try
            {
                Member temps = item.Members.Find(Member.MemberID);
                item.Members.Remove(temps);
                //item.Member.Remove(temp);
                //item.Entry(Member).State = EntityState.Deleted;
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

        public ErrorMessage CreateMember(Member Member)
        {
            ErrorMessage error = new ErrorMessage();
            error.IsSuccess = true;
            using (var data = item.Database.BeginTransaction())
            {
                try
                {
                    item.Members.Add(Member);
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