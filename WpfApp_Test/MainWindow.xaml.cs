using System;
using System.Collections.Generic;
using System.Diagnostics.PerformanceData;
using System.Linq;
using System.Text;
using System.Threading;
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
using WpfApp_Test.DB;

namespace WpfApp_Test
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// путь для капчи
        /// </summary>
        private string directore = @"/Captcha/";
        /// <summary>
        /// массив с названиями картинок с капчами
        /// </summary>
        private string[] ImPath = new string[]
        {
            "cp1.png",
            "cp2.png",
            "cp3.png"
        };
        /// <summary>
        /// Массив с правильными ответами на капчу
        /// </summary>
        private string[] Answers = new string[]
        {
            "A1B2C",
            "Ab48Hji",
            "b7aD9"
        };
        private int CountIn = 0;
        public int RnNumber = 0;
        
        public MainWindow()
        {
            InitializeComponent();
            ImageContent.Source = GetSource();
        }

        private void tbLogIn_Click(object sender, RoutedEventArgs e)
        {
            if(CountIn >= 2)
            {
                stCaptcha.Visibility = Visibility.Visible;
                stImage.Visibility = Visibility.Visible;
                MessageBox.Show("введите капчу");
                return;
            }
            DB.MyContext myContext = new DB.MyContext();
            try
            {
                if(tbLogin != null && tbPassword != null)
                {
                    if(myContext.users.Any(x=> x.Login == tbLogin.Text) && myContext.users.Any(x => x.Password == tbPassword.Text))
                    {
                        CountIn = 0;
                        MessageBox.Show("Вы Успешно вошли");
                        var user = myContext.users.Single(x=> x.Login == tbLogin.Text);
                        App.UserSessions = user;
                        switch (user.Status)
                        {
                            case "Admin":
                                View.ProductWindow productWindow = new View.ProductWindow();
                                productWindow.Show();
                                Close();
                                break;
                            case "User":
                                View.UserView userView = new View.UserView();
                                userView.Show();
                                Close();
                                break;

                        }
                    }
                    else
                    {
                        MessageBox.Show("Неправильный пароль или логин");
                        CountIn++;
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
        /// <summary>
        /// Метод для загрузки картинки капчи
        /// </summary>
        /// <returns></returns>
        private BitmapImage GetSource()
        {
            string path = directore + ImPath[RnNumber];
            Uri uri = new Uri(path, UriKind.Relative);
            BitmapImage image = new BitmapImage(uri);
            return image;
        }
        /// <summary>
        /// кнопка проверки капчи
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btCaptchaIn_Click(object sender, RoutedEventArgs e)
        {
            if(tbCaptchaIn.Text == Answers[RnNumber])
            {
                MessageBox.Show("Успешно");
                CountIn = 0;
                return;
            }
            MessageBox.Show("Неправильная капча вы заблокированы на 15 секунд");
            Thread.Sleep(15000);
            
        }
        /// <summary>
        /// кнопка для обновления капчи
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btReloadeCaptcha_Click(object sender, RoutedEventArgs e)
        {
            Random random = new Random();
            int RndNumber = random.Next(0, 3);
            RnNumber = RndNumber;
            ImageContent.Source = GetSource();

        }

        private void btGuestIn_Click(object sender, RoutedEventArgs e)
        {
            View.GuestView guestView = new View.GuestView();
            guestView.Show();
            Close();
        }
    }
}
