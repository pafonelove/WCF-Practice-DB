using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();
        // View customers.
        protected void Page_Load(object sender, EventArgs e)
        {
            GridView1.DataSource = client.GetCustomers();
            GridView1.DataBind();
        }

        // View selected customer orders.
        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridView2.DataSource = client.GetOrders(Convert.ToInt32(GridView1.SelectedRow.Cells[1].Text));
            GridView2.DataBind();
        }
    }
}