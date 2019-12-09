using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using vegetable.Controllers;
using vegetable.Models;

namespace vegetable.Respository.MemberResp
{
    public class initMember
    {
        public List<Member> initMemberData()
        {
            List<Member> MemberData = new List<Member>();
            ItemContext item = new ItemContext();
            try
            {
                MemberData = (from m in item.Members select m).ToList();
            }
            catch (Exception)
            {
                throw;
            }
            return (MemberData);
        }
    }
}