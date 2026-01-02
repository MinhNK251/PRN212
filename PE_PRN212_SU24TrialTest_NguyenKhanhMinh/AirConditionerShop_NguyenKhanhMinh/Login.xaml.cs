using AirConditionerShop_NguyenKhanhMinh.Repo.Models;
using AirConditionerShop_NguyenKhanhMinh.Repo.Repo;
using System.Windows;


namespace AirConditionerShop_NguyenKhanhMinh
{
    public partial class Login : Window
    {
        AirConditionerShop2024DbContext _context;
        StaffMemberRepo _staffMemberRepo;

        public Login()
        {
            InitializeComponent();
            _context = new AirConditionerShop2024DbContext();
            _staffMemberRepo = new StaffMemberRepo(_context);
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            var user = _staffMemberRepo.GetAll().Where(s => s.EmailAddress == txtEmail.Text.Trim() && s.Password == txtPassword.Text.Trim()).FirstOrDefault();

            if (user != null)
            {
                if (user.Role == 1 || user.Role == 2)
                {
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();
                    this.Close();
                }
                else
                {
                    System.Windows.MessageBox.Show("You have no permission to access this function!");
                }
            }
            else
            {
                System.Windows.MessageBox.Show("Invalid email or password!");
            }
        }

        private void BtnReset_Click(object sender, RoutedEventArgs e)
        {
            txtEmail.Clear();
            txtPassword.Clear();
        }
    }
}
