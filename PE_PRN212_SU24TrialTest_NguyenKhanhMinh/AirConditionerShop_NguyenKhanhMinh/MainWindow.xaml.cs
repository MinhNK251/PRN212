using AirConditionerShop_NguyenKhanhMinh.Repo.Models;
using AirConditionerShop_NguyenKhanhMinh.Repo.Repo;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AirConditionerShop_NguyenKhanhMinh
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        AirConditionerShop2024DbContext _context;
        AirConditionerRepo _airConditionerRepo;
        public MainWindow()
        {
            InitializeComponent();
            _context = new AirConditionerShop2024DbContext();
            _airConditionerRepo = new AirConditionerRepo(_context);
            LoadDataToDataGridView();
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }
        private void LoadDataToDataGridView()
        {
            var airConditionerList = _airConditionerRepo.GetAll().ToList();
            var anonymousList = airConditionerList.Select(ac => new AirConditionerViewModel
            {
                AirConditionerId = ac.AirConditionerId,
                AirConditionerName = ac.AirConditionerName,
                Warranty = ac.Warranty,
                SoundPressureLevel = ac.SoundPressureLevel,
                FeatureFunction = ac.FeatureFunction,
                Quantity = ac.Quantity,
                DollarPrice = ac.DollarPrice,
                SupplierName = ac.Supplier?.SupplierName
            }).ToList();
            dtgAirconditioner.AutoGenerateColumns = true;
            dtgAirconditioner.ItemsSource = anonymousList;
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = dtgAirconditioner.SelectedItem as AirConditionerViewModel;
            Console.WriteLine(selectedItem);
            if (selectedItem != null)
            {
                int ACid = selectedItem.AirConditionerId;
                _airConditionerRepo.Delete(ACid);
                LoadDataToDataGridView();
            }
            else
            {
                System.Windows.MessageBox.Show("Please Choose An Item To Delete!");
            }
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            CreateAirConditioner create = new CreateAirConditioner();
            create.ShowDialog();
            LoadDataToDataGridView();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = dtgAirconditioner.SelectedItem as AirConditionerViewModel;
            if (selectedItem != null)
            {
                int ACid = selectedItem.AirConditionerId;
                UpdateAirConditioner update = new UpdateAirConditioner(ACid);
                update.Owner = this; 
                update.ShowDialog();
                LoadDataToDataGridView();
            }
            else
            {
                System.Windows.MessageBox.Show("Please Choose An Item To Update!");
            }
        }
    }
}