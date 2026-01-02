using AirConditionerShop_NguyenKhanhMinh.Repo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirConditionerShop_NguyenKhanhMinh
{
    public class AirConditionerViewModel
    {
        public int AirConditionerId { get; set; }

        public string AirConditionerName { get; set; } = null!;

        public string? Warranty { get; set; }

        public string? SoundPressureLevel { get; set; }

        public string? FeatureFunction { get; set; }

        public int? Quantity { get; set; }

        public double? DollarPrice { get; set; }

        public virtual string SupplierName { get; set; }
    }
}
