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
    /// Логика взаимодействия для Manager.xaml
    /// </summary>
    public partial class Manager : Window
    {
        public Manager()
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        public void Update()
        {
            
            using (DataContext db = new DataContext(Properties.Settings.Default.DBConnect))
            {
                Table<Client> clients = db.GetTable<Client>();
                dgClient.ItemsSource = clients;
                Table<Service> services = db.GetTable<Service>();
                dgService.ItemsSource = services;
                Table<StatusOrder> statuses = db.GetTable<StatusOrder>();
                Table<Order> orders = db.GetTable<Order>();
                var query = from a in orders
                            join b in clients on a.Id_client equals b.Id_client
                            join c in services on a.Id_service equals c.Id_service
                            join d in statuses on a.Id_status_order equals d.Id_status_order
                            select new { a.Id_order, a.Date, d.Name, b.Fio_client, c.Name_services, c.Price_services };

                dgOrder.ItemsSource = query;
            }
            
            using (DataContext db = new DataContext(Properties.Settings.Default.DBConnect))
            {
                Table<Client> clients = db.GetTable<Client>();
                dgClient.ItemsSource = clients;

                var query = from a in clients
                            select a;

                dgClient.ItemsSource = query;
            }

            using (DataContext db = new DataContext(Properties.Settings.Default.DBConnect))
            {
                Table<Service> services = db.GetTable<Service>();
                dgService.ItemsSource = services;

                var query = from a in services
                            select a;

                dgService.ItemsSource = query;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Update();
            lblUser.Content = "Текущий пользователь: " + MainWindow.Memb.UserName;
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void btnSaveClient_Click(object sender, RoutedEventArgs e)
        {
            using (DataContext db = new DataContext(Properties.Settings.Default.DBConnect))
            {
                Client client = new Client { Fio_client = txtFio.Text, Phone_client = txtPhone.Text, Status_client = true };
                db.GetTable<Client>().InsertOnSubmit(client);
                db.SubmitChanges();
                txtFio.Text = null; txtPhone.Text = null;
            }
            Update();
            MessageBox.Show("Клиент добавлен!");
        }

        private void btnAddOrder_Click(object sender, RoutedEventArgs e)
        {
            AddOrder win = new AddOrder();
            win.Show();
        }

        private void dgOrder_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            object item = dgOrder.SelectedItem;
            if (item != null)
            {
                long ID = Convert.ToInt64((dgOrder.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text);
                using (DataContext db = new DataContext(Properties.Settings.Default.DBConnect))
                {
                    Table<Client> clients = db.GetTable<Client>();
                    Table<Order> orders = db.GetTable<Order>();
                    var queryInfo = from a in clients
                                    join b in orders on a.Id_client equals b.Id_client
                                    where b.Id_order == ID
                                    select new { a.Fio_client, a.Phone_client };
                    dgInfo.ItemsSource = queryInfo;

                    Table<StatusOrder> statuses = db.GetTable<StatusOrder>();
                    List<string> nameStatus = new List<string>();
                    foreach(StatusOrder it in statuses)
                    {
                        nameStatus.Add(it.Name);
                    }
                    cmbStatusOrder.ItemsSource = nameStatus;
                }
            }
        }

        private void txtSearchOrder_TextChanged(object sender, TextChangedEventArgs e)
        {
            string str = txtSearchOrder.Text;
            if (str == "") Update();
            else
            {
                using (DataContext db = new DataContext(Properties.Settings.Default.DBConnect))
                {
                    Table<Client> clients = db.GetTable<Client>();
                    Table<Service> services = db.GetTable<Service>();
                    Table<StatusOrder> stasuses = db.GetTable<StatusOrder>();
                    Table<Order> orders = db.GetTable<Order>();
                    var querySearch = from a in orders
                                      join b in clients on a.Id_client equals b.Id_client
                                      join c in services on a.Id_service equals c.Id_service
                                      join d in stasuses on a.Id_status_order equals d.Id_status_order
                                      where b.Fio_client.Contains(str) || Convert.ToString(a.Id_order).Contains(str)
                                      select new { a.Id_order, a.Date, d.Name, b.Fio_client, c.Name_services, c.Price_services };

                    dgOrder.ItemsSource = querySearch;
                }
            }
        }

        private void btnSaveStatus_Click(object sender, RoutedEventArgs e)
        {
            object item = dgOrder.SelectedItem;
            if (item != null)
            {
                using (DataContext db = new DataContext(Properties.Settings.Default.DBConnect))
                {
                    if (dgOrder.SelectedItem != null)
                    {
                        TextBlock text = dgOrder.Columns[0].GetCellContent(item) as TextBlock;
                        Order order = db.GetTable<Order>().FirstOrDefault(dgOrder => dgOrder.Id_order.Equals(text.Text));
                        order.Id_status_order = cmbStatusOrder.SelectedIndex + 1;
                        db.SubmitChanges();
                        Update();
                        MessageBox.Show("Изменения внесены успешно!");
                    }
                }
                Update();
            }
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            Update();
        }

        private void btnSaveChangeClient_Click(object sender, RoutedEventArgs e)
        {
            using (DataContext db = new DataContext(Properties.Settings.Default.DBConnect))
            {
                if (dgClient.SelectedItem != null)
                {
                    TextBlock text = dgClient.Columns[0].GetCellContent(dgClient.SelectedItem) as TextBlock;
                    Client client = db.GetTable<Client>().FirstOrDefault(dgClient => dgClient.Fio_client.Equals(text.Text));
                    client.Phone_client = txtPhoneUp.Text;
                    client.Status_client = cbStatusClient.IsChecked.Value;
                    db.SubmitChanges();
                }
            }
            Update();
            MessageBox.Show("Изменения внесены успешно!");
        }

        private void btnSaveService_Click(object sender, RoutedEventArgs e)
        {
            using (DataContext db = new DataContext(Properties.Settings.Default.DBConnect))
            {
                Service service = new Service { Name_services = txtNameS.Text, Price_services = int.Parse(txtPrice.Text), Status_services = true };
                db.GetTable<Service>().InsertOnSubmit(service);
                db.SubmitChanges();
                txtNameS.Text = null; txtPrice.Text = null;
            }
            Update();
            MessageBox.Show("Услуга добавлена!");
        }

        private void btnSaveChangeService_Click(object sender, RoutedEventArgs e)
        {
            using (DataContext db = new DataContext(Properties.Settings.Default.DBConnect))
            {
                if (dgService.SelectedItem != null)
                {
                    TextBlock text = dgService.Columns[0].GetCellContent(dgService.SelectedItem) as TextBlock;
                    Service service = db.GetTable<Service>().FirstOrDefault(dgService => dgService.Name_services.Equals(text.Text));
                    service.Price_services = int.Parse(txtPriceUp.Text);
                    service.Status_services = cbStatusService.IsChecked.Value;
                    db.SubmitChanges();
                }
            }
            Update();
            MessageBox.Show("Изменения внесены успешно!");
        }

        private void dgClient_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgClient.SelectedItem != null)
            {
                TextBlock text = dgClient.Columns[1].GetCellContent(dgClient.SelectedItem) as TextBlock;
                txtPhoneUp.Text = text.Text;
                using (DataContext db = new DataContext(Properties.Settings.Default.DBConnect))
                {
                    Table<Client> klients = db.GetTable<Client>();
                    cbStatusClient.IsChecked = klients.ToArray()[dgClient.SelectedIndex].Status_client;
                }
            }
        }

        private void dgService_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgService.SelectedItem != null)
            {
                TextBlock text = dgService.Columns[1].GetCellContent(dgService.SelectedItem) as TextBlock;
                txtPriceUp.Text = text.Text;
                using (DataContext db = new DataContext(Properties.Settings.Default.DBConnect))
                {
                    Table<Service> uslugas = db.GetTable<Service>();
                    cbStatusService.IsChecked = uslugas.ToArray()[dgService.SelectedIndex].Status_services;
                }
            }
        }
    }
}
