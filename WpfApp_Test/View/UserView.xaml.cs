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

namespace WpfApp_Test.View
{
    /// <summary>
    /// Логика взаимодействия для UserView.xaml
    /// </summary>
    public partial class UserView : Window
    {
        public UserView()
        {
            InitializeComponent();
            Loaded += UserView_Loaded;
        }

        private void UserView_Loaded(object sender, RoutedEventArgs e)
        {
            DB.MyContext myContext = new DB.MyContext();
            var product = myContext.product;
            ImageUser.Source = GetSource();
            tbName.Text = App.UserSessions.Name;
            tbStatus.Text = App.UserSessions.Status;
            cbProductView.ItemsSource = product.Select(x => x.ProductName).Distinct().ToList();

        }
        private BitmapImage GetSource()
        {
            string path = App.UserSessions.ImagePath;
            Uri uri = new Uri(path, UriKind.Relative);
            BitmapImage bitmapImage = new BitmapImage(uri);
            return bitmapImage;

        }

        private void btExit_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }
    }
}
