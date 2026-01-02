using AirConditionerShop_NguyenKhanhMinh.Repo.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirConditionerShop_NguyenKhanhMinh.Repo.Repo
{
    public class AirConditionerRepo
    {
        private readonly AirConditionerShop2024DbContext _context;

        public AirConditionerRepo(AirConditionerShop2024DbContext context)
        {
            _context = context;
        }

        public IEnumerable<AirConditioner> GetAll()
        {
            //return _context.AirConditioners.Include(a => a.Supplier).ToList();
            return _context.AirConditioners.Include(ac => ac.Supplier).ToList();
        }

        public AirConditioner GetById(int id)
        {
            return _context.AirConditioners.Find(id);
        }

        public void Add(AirConditioner airConditioner)
        {
            _context.AirConditioners.Add(airConditioner);
            _context.SaveChanges();
        }

        public void Update(AirConditioner airConditioner)
        {
            _context.AirConditioners.Update(airConditioner);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var airConditioner = _context.AirConditioners.Find(id);
            if (airConditioner != null)
            {
                _context.AirConditioners.Remove(airConditioner);
                _context.SaveChanges();
            }
        }
    }
}