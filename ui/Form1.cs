using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace ui
{
    public partial class Form1 : Form
    {
        private Database database = new Database();

        public Form1()
        {
            InitializeComponent();
            refreshOrdersList();

        }

        private void refreshOrdersList()
        {
            ordersList.Items.Clear();
            
            foreach (var row in database.FetchAllOrderNumbers())
            {
                var row_dict = (IDictionary<string, object>)row;
                ordersList.Items.Add(row_dict["order_number"]);
            }
        }

        private void refreshOrderDetails(int orderNumber)
        {
            orderDetailsGrid.Rows.Clear();
            foreach (var row in database.FetchOrderDetails(orderNumber))
            {
                var row_dict = (IDictionary<string, object>)row;
                foreach (var key in row_dict.Keys)
                {
                    orderDetailsGrid.Rows.Add(key, row_dict[key]);
                }
            }
        }

        private void ordersList_SelectedIndexChanged(object sender, EventArgs e)
        {
            int orderNumber = (int)ordersList.SelectedItem;
            refreshOrderDetails(orderNumber);
        }

        private void orderDetailsGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
