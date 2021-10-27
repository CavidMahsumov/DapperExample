using Dapper;
using DapperExample.Entites;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
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

namespace DapperExample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Product> GetAll()
        {
            List<Product> products = new List<Product>();
            using (var connection=new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnString"].ConnectionString))
            {
                connection.Open();
                products = connection.Query<Product>("select*from Products").ToList();
            }
            return products;
        }

        private void InsertProduct(Product product)
        {
            using (var connection=new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnString"].ConnectionString))
            {
                connection.Open();
                connection.Execute("insert into Products(Name,Price)values(@ProductName,@ProductPrice)", new { @ProductName = product.Name, @ProductPrice = product.Price });
            }
        }
        private void UpdateProduct(int id,Product product)
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnString"].ConnectionString))
            {
                connection.Open();
                connection.Execute("Update Products Set Name=@PName,Price=@PPrice where Id=@PId", new { @PName = product.Name, @PPrice = product.Price, @PId = id });
            }
        }
        public MainWindow()
        {
            InitializeComponent();
            UpdateProduct(1, new Product { Name = "RangeRover", Price = 43300 });
            MyDataGrid.ItemsSource = GetAll();

        }
    }
}
