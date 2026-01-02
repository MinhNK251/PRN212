using AirConditionerShop_NguyenKhanhMinh.Repo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirConditionerShop_NguyenKhanhMinh.Repo.Repo
{
    public class SupplierCompanyRepo
    {
        private readonly AirConditionerShop2024DbContext _context;

        public SupplierCompanyRepo(AirConditionerShop2024DbContext context)
        {
            _context = context;
        }

        public IEnumerable<SupplierCompany> GetAll()
        {
            return _context.SupplierCompanies.ToList();
        }

        public SupplierCompany GetById(int id)
        {
            return _context.SupplierCompanies.Find(id);
        }

        public void Add(SupplierCompany supplierCompany)
        {
            _context.SupplierCompanies.Add(supplierCompany);
            _context.SaveChanges();
        }

        public void Update(SupplierCompany supplierCompany)
        {
            _context.SupplierCompanies.Update(supplierCompany);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var supplierCompany = _context.SupplierCompanies.Find(id);
            if (supplierCompany != null)
            {
                _context.SupplierCompanies.Remove(supplierCompany);
                _context.SaveChanges();
            }
        }
    }
}
