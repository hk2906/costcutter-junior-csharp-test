using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using MySql.Data.MySqlClient;
using ui.Properties;

namespace ui
{
    public class Database
    {
        private readonly string _ConnectionString = Settings.Default.DbConnectionString;

        public MySqlConnection GetConnection => new MySqlConnection(_ConnectionString);

        private IEnumerable<dynamic> SQLSelect(String sql)
        {
            var dbConnection = new Database().GetConnection;
            dbConnection.Open();

            var result = dbConnection.Query(sql);
            dbConnection.Close();
            return result;
        }

        public IEnumerable<dynamic> FetchAllBranches()
        {
            return SQLSelect("SELECT * from branches");
        }

        public IEnumerable<dynamic> FetchAllOrderNumbers()
        {
            return SQLSelect("SELECT order_number from orders ORDER BY order_number");
        }

        public IEnumerable<dynamic> FetchOrderDetails(int orderNumber)
        {
            String sql = "SELECT "+
                             "order_number, order_date, sale_price, deposit, "+
	                         "customers.customer_number, customers.forename, customers.surname, customers.telephone_number, "+
	                         "branches.branch_name, branches.postcode "+
                         "FROM orders "+
                         "INNER JOIN customers ON orders.customer_number = customers.customer_number "+
                         "INNER JOIN employees ON orders.employee_number = employees.employee_number "+
                         "INNER JOIN branches ON employees.branch_name = branches.branch_name "+
                         "AND order_number = " + orderNumber;

            return SQLSelect(sql);
        }

    }
}
