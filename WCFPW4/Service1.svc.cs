using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WCFPW4
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "Service1" в коде, SVC-файле и файле конфигурации.
    // ПРИМЕЧАНИЕ. Чтобы запустить клиент проверки WCF для тестирования службы, выберите элементы Service1.svc или Service1.svc.cs в обозревателе решений и начните отладку.
    public class Service1 : IService1
    {
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        public List<Customer> GetCustomers()
        {
            List<Customer> list = new List<Customer>();

            // DB connection string.
            string str = System.Configuration.ConfigurationManager.ConnectionStrings["myDB"].ConnectionString;
            using (SqlConnection con = new SqlConnection(str))
            {
                SqlCommand com = new SqlCommand("Select * from Customers", con);    // select data
                con.Open();
                SqlDataReader r = com.ExecuteReader();
                while (r.Read())
                {
                    // Create new Customer object.
                    Customer c = new Customer();
                    c.Id = (int)r["Id"];
                    c.Name = r["Name"].ToString();
                    c.Surname = r["Surname"].ToString();
                    c.YearOfBirth = (int) r["YearOfBirth"];
                    
                    list.Add(c);
                }
            }
            return list;
        }

        public List<Order> GetOrders(int id)
        {
            List<Order> list = new List<Order>();

            // DB connection string.
            string str = System.Configuration.ConfigurationManager.ConnectionStrings["myDB"].ConnectionString;
            using (SqlConnection con = new SqlConnection(str))
            {
                SqlCommand com = new SqlCommand("Select * from Orders where IdCust=@id", con);    // select data
                com.Parameters.Add(new SqlParameter("@id", id));    // add @id parameter for select command
                con.Open();
                SqlDataReader r = com.ExecuteReader();
                while (r.Read())
                {
                    // Create new Order object.
                    Order or = new Order();
                    or.Id = (int)r["Id"];
                    or.Title = r["Title"].ToString();
                    or.IdCust = (int)r["IdCust"];
                    or.Price = (int)r["Price"];
                    or.Quant = (int)r["Quant"];

                    list.Add(or);
                }
            }
            return list;
        }
    }
}
