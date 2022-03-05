using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using tareaRE.Entidades;
using tareaRE.BLL;


namespace tareaRE.UI.Consultas
{
    public partial class cEmpleados : Form
    {
        public cEmpleados()
        {
            InitializeComponent();
        }

        private void ConsultaButton_Click(object sender, EventArgs e)
        {
            var listado = new List<Empleados>();

            if (CriteriosTextBox.Text.Trim().Length > 0)
            {
                switch (FiltroComboBox.SelectedIndex)
                {
                    case 0:
                        listado = EmpleadosBll.GetList(e => e.EmpleadoId == Utilidades.ToInt(CriteriosTextBox.Text));
                        break;

                    case 1:
                        listado = EmpleadosBll.GetList(e => e.NombreCompleto.Contains(CriteriosTextBox.Text, StringComparison.OrdinalIgnoreCase));
                        break;
                    case 2:
                        listado = EmpleadosBll.GetList(e => e.Telefono.Contains(CriteriosTextBox.Text));
                        break;
                    case 3:
                        listado = EmpleadosBll.GetList(e => e.Departamento.Contains(CriteriosTextBox.Text));
                        break;
                    case 4:
                        listado = EmpleadosBll.GetList(e => e.Puesto.Contains(CriteriosTextBox.Text));
                        break;

                }
            }
            else
            {
                listado = EmpleadosBll.GetList(c => true);
            }
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = listado;
        }
    }
}
