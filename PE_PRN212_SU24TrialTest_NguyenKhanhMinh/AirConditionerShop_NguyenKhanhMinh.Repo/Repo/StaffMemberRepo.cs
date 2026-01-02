using AirConditionerShop_NguyenKhanhMinh.Repo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirConditionerShop_NguyenKhanhMinh.Repo.Repo
{
    public class StaffMemberRepo
    {
        private readonly AirConditionerShop2024DbContext _context;

        public StaffMemberRepo(AirConditionerShop2024DbContext context)
        {
            _context = context;
        }

        public IEnumerable<StaffMember> GetAll()
        {
            return _context.StaffMembers.ToList();
        }

        public StaffMember GetById(int id)
        {
            return _context.StaffMembers.Find(id);
        }

        public void Add(StaffMember staffMember)
        {
            _context.StaffMembers.Add(staffMember);
            _context.SaveChanges();
        }

        public void Update(StaffMember staffMember)
        {
            _context.StaffMembers.Update(staffMember);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var staffMember = _context.StaffMembers.Find(id);
            if (staffMember != null)
            {
                _context.StaffMembers.Remove(staffMember);
                _context.SaveChanges();
            }
        }
    }
}
