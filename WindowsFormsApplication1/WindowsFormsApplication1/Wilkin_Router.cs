using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Wilkin_Router : Form
    {
        public Wilkin_Router()
        {
            InitializeComponent();
        }

        private void Wilkin_Router_Load(object sender, EventArgs e)
        {
            Form f1 = new Form();
            f1.Text = "Wilkin ROUTER";
        }
    }
}
