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
    /// Логика взаимодействия для ProductWindow.xaml
    /// </summary>
    public partial class ProductWindow : Window
    {
        public string[] category = new string[] { "продукты", "напитки", "одежда" };
        public string[] images = new string[] { "продукты.png", "напитки.png", "одежда.png" };
        public ProductWindow()
        {
            InitializeComponent();
            cbCategory.ItemsSource = category;
            cbImage.ItemsSource = images;
        }
        /// <summary>
        /// Кнопка поиска продукта по артиклю
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btSearch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DB.MyContext myContext = new DB.MyContext();
                var product = myContext.product.Single(x => x.Article == tbArticle.Text);
                if (product != null)
                {
                    tbName.Text = product.ProductName.ToString();
                    tbManufacture.Text = product.Manufacture.ToString();
                    tbDescription.Text = product.Description.ToString();
                    tbImage.Text = product.ImagePath.ToString();
                    tbCategory.Text = product.Category.ToString();
                    tbCost.Text = product.Cost.ToString();
                    tbDiscount.Text = product.Discount.ToString();
                    var ProductInStorage = myContext.storage.Where(x => x.ProductId == product.ProductId);
                    if (ProductInStorage.Count() > 0)
                    {
                        tbCount.Text = ProductInStorage.Sum(x => x.Count).ToString();
                    }
                }
                else
                {
                    MessageBox.Show("Not found");
                }

               
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// Кнопка изменения количества товара
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btSaveCount_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DB.MyContext myContext = new DB.MyContext();
                var product = myContext.product.SingleOrDefault(x => x.Article == tbArticle.Text);
                if(product != null)
                {
                    var delivery = new DB.Storage();
                    delivery.ProductId = product.ProductId;
                    delivery.Count = Convert.ToInt32(tbCountIn.Text);
                    myContext.storage.Add(delivery);
                    myContext.SaveChanges();
                    MessageBox.Show("Успешно добавлено");
                }
                else
                {
                    MessageBox.Show("ProductNotFound");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                
            }

        }
        /// <summary>
        /// кнопка сохранения измененых данных о продукте в бд
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btSaveProduct_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DB.MyContext myContext = new DB.MyContext();
                var product = myContext.product.SingleOrDefault(x => x.Article == tbArticle.Text);
                if(product != null)
                {
                    if(Convert.ToDouble(tbDiscount.Text) > 30)
                    {
                        MessageBox.Show("Скидка не может быть больше 30");
                    }
                    product.Manufacture = tbManufacture.Text;
                    product.ProductName = tbName.Text;
                    product.Description = tbDescription.Text;
                    product.Cost = Convert.ToDouble(tbCost.Text);
                    product.Discount = Convert.ToDouble(tbDiscount.Text);
                    if(cbCategory.SelectedItem != null)
                    {
                        product.Category = cbCategory.SelectedItem.ToString();
                    }
                    if(cbImage.SelectedItem != null)
                    {
                        product.ImagePath = cbCategory.SelectedItem.ToString();
                    }
                    myContext.product.Update(product);
                    myContext.SaveChanges();
                    MessageBox.Show("Продукт успешно изменен");
                }
                else
                {
                    MessageBox.Show("Not found");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                
            }
        }
        /// <summary>
        /// кнопка удаления выбраного продукта
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void brDeleteAll_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DB.MyContext myContext = new DB.MyContext();
                var product = myContext.product.SingleOrDefault(x => x.Article == tbArticle.Text);
                if(product != null)
                {
                    var storage = myContext.storage.FirstOrDefault(x=>x.ProductId == product.ProductId);
                    if(storage != null)
                    {
                        myContext.storage.Remove(storage);
                    }
                    myContext.product.Remove(product);
                    tbArticle.Clear();
                    myContext.SaveChanges();
                    MessageBox.Show("Продукт успешно удален");
                }
                else
                {
                    MessageBox.Show("Not found");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }
    }
}
