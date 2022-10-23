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

namespace WpfApp_Test
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void tbLogIn_Click(object sender, RoutedEventArgs e)
        {
            DB.MyContext myContext = new DB.MyContext();
            try
            {
                if(tbLogin != null && tbPassword != null)
                {
                    if(myContext.users.Any(x=> x.Login == tbLogin.Text) && myContext.users.Any(x => x.Password == tbPassword.Text))
                    {
                        MessageBox.Show("Вы Успешно вошли");
                        View.ProductWindow productWindow = new View.ProductWindow();
                        productWindow.Show();
                        Close();

                    }
                    else
                    {
                        MessageBox.Show("Неправильный пароль или логин");
                    }
                }
                else
                {
                    MessageBox.Show("поля пусты");
                    return;
                }
            }
            catch ( Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }
    }
}
