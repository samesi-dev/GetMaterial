﻿using IStock.UI_forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IStock.Reports
{
    public partial class supporder : Form
    {
        public supporder()
        {
            InitializeComponent();
        }

        private void supporder_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'sorder.s_ord_pay' table. You can move, or remove it, as needed.
            this.s_ord_payTableAdapter.Fill(this.sorder.s_ord_pay);

            this.reportViewer1.RefreshReport();
        }

        private void CrossButton_Click(object sender, EventArgs e)
        {
            PurchaseOrders p = new PurchaseOrders();
            p.ShowDialog();
            this.Hide();
        }
    }
}
