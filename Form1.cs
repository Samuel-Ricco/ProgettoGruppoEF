using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProgettoGruppoEF
{
    public partial class Form1 : Form
    {
        public academynetEntities1 ctx;
        public Form1()
        {
            ctx = new academynetEntities1();
            InitializeComponent();

            comboBox1.DataSource = GetTables();
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }


        private List<string> GetTables()
        {
            var result = new List<string>();
            var metadata = ((IObjectContextAdapter)ctx).ObjectContext.MetadataWorkspace;
            var tables = metadata.GetItemCollection(DataSpace.SSpace)
            .GetItems<EntityContainer>()
            .Single()
            .BaseEntitySets
            .OfType<EntitySet>()
            .Where(s => !s.MetadataProperties.Contains("Type") || s.MetadataProperties["Type"].ToString() == "Tables"); foreach (var table in tables)
            {
                var tableName = table.MetadataProperties.Contains("Table") && table.MetadataProperties["Table"].Value != null
                ? table.MetadataProperties["Table"].Value.ToString()
                : table.Name; var tableSchema = table.MetadataProperties["Schema"].Value.ToString(); result.Add(tableSchema + "," + tableName);
            }
            return result;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            switch (comboBox1.Text)
            {
                case "production,brands":
                    dataGridView1.DataSource = ctx.brands.ToList();
                    break;
                case "production,categories":
                    dataGridView1.DataSource = ctx.categories.ToList();
                    break;
                case "production,products":
                    dataGridView1.DataSource = ctx.products.ToList();
                    break;
                case "production,stocks":
                    dataGridView1.DataSource = ctx.stocks.ToList();
                    break;
                case "sales,customers":
                    dataGridView1.DataSource = ctx.customers.ToList();
                    break;
                case "sales,order_items":
                    dataGridView1.DataSource = ctx.order_items.ToList();
                    break;
                case "sales,orders":
                    dataGridView1.DataSource = ctx.orders.ToList();
                    break;
                case "sales,staffs":
                    dataGridView1.DataSource = ctx.staffs.ToList();
                    break;
                case "sales,stocks":
                    dataGridView1.DataSource = ctx.stocks.ToList();
                    break;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ctx.SaveChanges();
        }
    }
}
