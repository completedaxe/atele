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
    /// Логика взаимодействия для AddOrder.xaml
    /// </summary>
    public partial class AddOrder : Window
    {
        public AddOrder()
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            using (DataContext db = new DataContext(Properties.Settings.Default.DBConnect))
            {
                Table<Client> сlients = db.GetTable<Client>();
                cmbKlient.Items.Clear();
                foreach (var a in сlients)
                {
                    cmbKlient.Items.Add(a.Id_client + " " + a.Fio_client);
                }
                Table<Service> services = db.GetTable<Service>();
                cmbUslugas.Items.Clear();
                foreach (var b in services)
                {
                    cmbUslugas.Items.Add(b.Id_service + " " + b.Name_services);
                }
                Table<StatusOrder> statuses = db.GetTable<StatusOrder>();
                cmbRAb.Items.Clear();
                foreach (var c in statuses)
                {
                    cmbRAb.Items.Add(c.Id_status_order + " " + c.Name);
                }
            }
        }

        public string D(string text)
        {
            string[] str = text.Split(' ');
            return str[0];
        }

        private void btnadd_Click(object sender, RoutedEventArgs e)
        {
            using (DataContext db = new DataContext(Properties.Settings.Default.DBConnect))
            {
                Client k = new Client();
                DateTime date = DateTime.Now;
                Order order = new Order { Id_client = int.Parse(D(cmbKlient.Text)), Id_service = int.Parse(D(cmbUslugas.Text)), Id_status_order = int.Parse(D(cmbRAb.Text)), Id_user = MainWindow.Memb.UserID, Date = date };
                db.GetTable<Order>().InsertOnSubmit(order);
                db.SubmitChanges();
                Manager m = new Manager();
                m.Update();
                MessageBox.Show("Заказ добавлен успешно!");
                Close();
            }
        }
    }
}
