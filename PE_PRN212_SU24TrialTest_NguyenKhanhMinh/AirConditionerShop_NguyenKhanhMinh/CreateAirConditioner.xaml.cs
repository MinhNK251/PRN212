using AirConditionerShop_NguyenKhanhMinh.Repo.Models;
using AirConditionerShop_NguyenKhanhMinh.Repo.Repo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AirConditionerShop_NguyenKhanhMinh
{
    public partial class CreateAirConditioner : Window
    {
        AirConditionerShop2024DbContext _context;
        AirConditionerRepo _airConditionerRepo;
        SupplierCompanyRepo _supplierCompanyRepo;
        public CreateAirConditioner()
        {
            InitializeComponent();
            _context = new AirConditionerShop2024DbContext();
            _airConditionerRepo = new AirConditionerRepo(_context);
            _supplierCompanyRepo = new SupplierCompanyRepo(_context);
            LoadSuppliers();
        }

        private bool ValidateInput()
        {
            //check id
            if (txtID.Text.Trim() == "")
            {
                System.Windows.MessageBox.Show("Please input id!");
                return false;
            }
            else
            {
                int id = 0;
                int.TryParse(txtID.Text, out id);
                if (id > 0)
                {
                    //check id exist
                    if (_airConditionerRepo.GetById(id) != null)
                    {
                        System.Windows.MessageBox.Show("ID already exist");
                        return false;
                    }

                }
                else
                {
                    System.Windows.MessageBox.Show("ID have to be a number");
                    return false;
                }
            }
            //check name
            if (txtName.Text.Trim() == "")
            {
                System.Windows.MessageBox.Show("Please input name!");
                return false;
            }
            else
            {
                if (!(txtName.Text.Trim().Length >= 5 && txtName.Text.Trim().Length <= 90))
                {
                    System.Windows.MessageBox.Show("Air Conditioner's Name must be between 5 – 90 characters");
                    return false;
                }
                else if (!(Char.IsUpper(txtName.Text, 0)))
                {
                    System.Windows.MessageBox.Show("Air Conditioner's Name must start with uppercase letter");
                    return false;
                }
            }
            //check warranty
            if (txtWarranty.Text.Trim() == "")
            {
                System.Windows.MessageBox.Show("Please input Warranty!");
                return false;
            }
            //check SoundPressureLevel
            if (txtSoundPressureLevel.Text.Trim() == "")
            {
                System.Windows.MessageBox.Show("Please input Sound Pressure Level!");
                return false;
            }
            //check FeatureFunction
            if (txtFeatureFunction.Text.Trim() == "")
            {
                System.Windows.MessageBox.Show("Please input Feature Function!");
                return false;
            }
            //check Quantity
            if (txtQuantity.Text.Trim() == "")
            {
                System.Windows.MessageBox.Show("Please input quantity!");
                return false;
            }
            else
            {
                int quantity = -1;
                int.TryParse(txtQuantity.Text, out quantity);

                if (quantity < 0)
                {
                    System.Windows.MessageBox.Show("ID have to be a number");
                    return false;
                }
            }
            //check Price
            if (txtPrice.Text.Trim() == "")
            {
                System.Windows.MessageBox.Show("Please input price!");
                return false;
            }
            else
            {
                float quantity = -1;
                float.TryParse(txtID.Text, out quantity);
                if (quantity < 0)
                {
                    System.Windows.MessageBox.Show("dollar price have to be a number");
                    return false;
                }
            }
            return true;
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateInput())
            {
                var airConditioner = new AirConditioner()
                {
                    AirConditionerId = int.Parse(txtID.Text),
                    AirConditionerName = txtName.Text,
                    Warranty = txtWarranty.Text,
                    SoundPressureLevel = txtSoundPressureLevel.Text,
                    FeatureFunction = txtFeatureFunction.Text,
                    Quantity = int.Parse(txtQuantity.Text),
                    DollarPrice = double.Parse(txtPrice.Text),
                    SupplierId = cbbSupplier.SelectedValue.ToString(),
                };
                _airConditionerRepo.Add(airConditioner);
                System.Windows.MessageBox.Show("Air conditioner created successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
        }

        private void cbbSupplier_Loaded(object sender, RoutedEventArgs e)
        {
            LoadSuppliers();
        }

        private void LoadSuppliers()
        {
            var suppliers = _supplierCompanyRepo.GetAll().ToList();
            cbbSupplier.ItemsSource = suppliers;
            cbbSupplier.DisplayMemberPath = nameof(SupplierCompany.SupplierName);
            cbbSupplier.SelectedValuePath = nameof(SupplierCompany.SupplierId);
        }
    }
}
