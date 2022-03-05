using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using tareaRE.UI.Consultas;
using tareaRE.UI.Reguistro;

namespace tareaRE.UI
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
            this.empleadosToolStripMenuItem.Click += new EventHandler(this.empleadosToolStripMenuItem_ItemClicked);
            this.nominaToolStripMenuItem.Click += new EventHandler(this.empleadosToolStripMenuItem_ItemClicked);
            this.empleadosToolStripMenuItem1.Click += new EventHandler(this.empleadosToolStripMenuItem1_ItemClicked);
            this.nominaToolStripMenuItem1.Click += new EventHandler(this.empleadosToolStripMenuItem1_ItemClicked);

        }


        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            Close();

        }


        private void empleadosToolStripMenuItem_ItemClicked(object sender, EventArgs e)
        {
            rEmpleados empleados = new rEmpleados();
            empleados.Show();
        }

        private void nominaToolStripMenuItem_ItemClicked(object sender, EventArgs e)
        {
            rNomina Nomina = new rNomina();
            Nomina.Show();
        }

        private void empleadosToolStripMenuItem1_ItemClicked(object sender, EventArgs e)
        {
            cEmpleados empleados = new cEmpleados();
            empleados.Show();

        }

        private void nominaToolStripMenuItem1_ItemClicked(object sender, EventArgs e)
        {
            cNomina nomina = new cNomina();
            nomina.Show();

        }
    }
}
