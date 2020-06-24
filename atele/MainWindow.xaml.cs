using atele.Classes;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace atele
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        public static class Memb
        {
            public static long UserID;
            public static string UserName;
        }

        public MainWindow()
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            txtLogin.Focus();
        }

        public void Enter()
        {
            try
            {
                using (DataContext db = new DataContext(Properties.Settings.Default.DBConnect))
                {
                    string login = txtLogin.Text;
                    string password = passwordBox.Password;

                    Table<User> users = db.GetTable<User>();

                    var query = from a in users
                                where a.Login == login && a.Password == password
                                select new { a.Id_user, a.Name_user, a.Role };

                    if (query != null)
                    {
                        MessageBox.Show("Добро пожаловать, " + query.ToArray()[0].Name_user);
                        Memb.UserID = query.ToArray()[0].Id_user;
                        Memb.UserName = query.ToArray()[0].Name_user;
                        switch (query.ToArray()[0].Role)
                        {
                            case "приемщик": Manager win = new Manager(); win.Show(); this.Visibility = Visibility.Collapsed; break;
                            case "мастер": Manager win1 = new Manager(); win1.Show(); break;
                        }
                    }
                    else MessageBox.Show("Логин или пароль введены неверно.");
                }
            }
            catch
            {
                MessageBox.Show("Нет подключения к БД");
            }
        }

        private void btnEnter_Click(object sender, RoutedEventArgs e)
        {
            Enter();
        }

        private void txtLogin_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtLogin.Text == "" || passwordBox.Password == "")
            {
                btnEnter.IsEnabled = false;
            }
            else
            {
                btnEnter.IsEnabled = true;
            }
        }

        private void passwordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (txtLogin.Text == "" || passwordBox.Password == "")
            {
                btnEnter.IsEnabled = false;
            }
            else
            {
                btnEnter.IsEnabled = true;
            }
        }
    }
}
