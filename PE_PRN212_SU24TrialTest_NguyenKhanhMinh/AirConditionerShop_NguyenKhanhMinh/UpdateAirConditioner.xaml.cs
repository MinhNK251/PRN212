using AirConditionerShop_NguyenKhanhMinh.Repo.Models;
using AirConditionerShop_NguyenKhanhMinh.Repo.Repo;
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
    /// <summary>
    /// Interaction logic for UpdateAirConditioner.xaml
    /// </summary>
    public partial class UpdateAirConditioner : Window
    {
        private AirConditionerShop2024DbContext _context;
        private AirConditionerRepo _airConditionerRepo;
        private SupplierCompanyRepo _supplierCompanyRepo;
        private int _airConditionerId;

        public UpdateAirConditioner(int airConditionerId)
        {
            InitializeComponent();
            _context = new AirConditionerShop2024DbContext();
            _airConditionerRepo = new AirConditionerRepo(_context);
            _supplierCompanyRepo = new SupplierCompanyRepo(_context);
            _airConditionerId = airConditionerId;
            LoadAirConditionerDetails();
            LoadSuppliers();
        }
        private void LoadAirConditionerDetails()
        {
            var airConditioner = _airConditionerRepo.GetById(_airConditionerId);
            if (airConditioner != null)
            {
                txtID.Text = airConditioner.AirConditionerId.ToString();
                txtName.Text = airConditioner.AirConditionerName;
                txtWarranty.Text = airConditioner.Warranty;
                txtSoundPressureLevel.Text = airConditioner.SoundPressureLevel;
                txtFeatureFunction.Text = airConditioner.FeatureFunction;
                txtQuantity.Text = airConditioner.Quantity.ToString();
                txtPrice.Text = airConditioner.DollarPrice.ToString();
                cbbSupplier.SelectedValuePath = nameof(SupplierCompany.SupplierId);
                cbbSupplier.SelectedValue = airConditioner.SupplierId;
            }
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
                if (!(id > 0))
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

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateInput())
            {
                var airConditioner = _airConditionerRepo.GetById(_airConditionerId);
                if (airConditioner != null)
                {
                    airConditioner.AirConditionerName = txtName.Text;
                    airConditioner.Warranty = txtWarranty.Text;
                    airConditioner.SoundPressureLevel = txtSoundPressureLevel.Text;
                    airConditioner.FeatureFunction = txtFeatureFunction.Text;
                    airConditioner.Quantity = int.Parse(txtQuantity.Text);
                    airConditioner.DollarPrice = double.Parse(txtPrice.Text);
                    airConditioner.SupplierId = cbbSupplier.SelectedValue.ToString();
                    _airConditionerRepo.Update(airConditioner);
                    _context.SaveChanges();
                    System.Windows.MessageBox.Show("Air conditioner updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close();
                }
            }
        }

        private void LoadSuppliers()
        {
            cbbSupplier.ItemsSource = _supplierCompanyRepo.GetAll();
            cbbSupplier.DisplayMemberPath = nameof(SupplierCompany.SupplierName);
            cbbSupplier.SelectedValuePath = nameof(SupplierCompany.SupplierId);
        }
    }
}
