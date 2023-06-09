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
        private String errorMessage = "Error: Could not connect to database Click to try again";

        public Form1()
        {
            InitializeComponent();
            refreshOrdersList(null);
        }

        private void refreshOrdersList(String date)
        {
            ordersList.Items.Clear();
            try
            {
                foreach (var row in database.FetchOrderNumbers(date))
                {
                    var row_dict = (IDictionary<string, object>)row;
                    ordersList.Items.Add(row_dict["order_number"]);
                }
            }
            catch (Exception)
            {
                ordersList.Items.Add(errorMessage);
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
            try
            {
                int orderNumber = (int)ordersList.SelectedItem;
                refreshOrderDetails(orderNumber);
            }
            catch (Exception)
            {
                refreshOrdersList(null);
            }
        }

        private void orderDetailsGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            Debug.WriteLine(dateTimePicker.Value.ToString("yyy-MM-dd"));
            refreshOrdersList(dateTimePicker.Value.ToString("yyy-MM-dd"));

        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            refreshOrdersList(null);
        }
    }
}
