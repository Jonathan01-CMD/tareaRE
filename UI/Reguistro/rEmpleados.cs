using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using tareaRE.BLL;
using tareaRE.Entidades;

namespace tareaRE.UI.Reguistro
{
    public partial class rEmpleados : Form
    {
        public rEmpleados()
        {
            InitializeComponent();
        }

        public void Limpiar()
        {
            EmpleadoIdNumericUpDown.Value = 0;
            NombreTextBox.Clear();
            TelefonoTextBox.Clear();
            DepartamentoTextBox.Clear();
            PuestosTextBox.Clear();
        }
        public void LlenarCampo(Empleados empleados)
        {
            EmpleadoIdNumericUpDown.Value = empleados.EmpleadoId;
            NombreTextBox.Text = empleados.NombreCompleto;
            TelefonoTextBox.Text = empleados.Telefono;
            DepartamentoTextBox.Text = empleados.Departamento;
            PuestosTextBox.Text = empleados.Puesto;
            dateTimePicker1.Value = empleados.FechaCreacion;
        }

        public Empleados LlenarClase()
        {
            Empleados empleados = new Empleados();
            empleados.EmpleadoId = (int)EmpleadoIdNumericUpDown.Value;
            empleados.NombreCompleto = NombreTextBox.Text;
            empleados.Telefono = TelefonoTextBox.Text;
            empleados.Departamento = DepartamentoTextBox.Text;
            empleados.Puesto = PuestosTextBox.Text;
            empleados.FechaCreacion = dateTimePicker1.Value;
            return empleados;
        }
        public bool ExisteEnLaBaseDeDatos()
        {
            Empleados empleados = EmpleadosBll.Buscar((int)EmpleadoIdNumericUpDown.Value);

            return (empleados != null);
        }

        private void limpiarButton_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void button_Click(object sender, EventArgs e)
        {
            Empleados empleados;
            int id;
            int.TryParse(EmpleadoIdNumericUpDown.Text, out id);

            if (EmpleadoIdNumericUpDown.Value == 0)
            {
                MessageBox.Show("Debes agregar un numero aqui para poder buscar.", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Limpiar();

            empleados = EmpleadosBll.Buscar(id);

            if (empleados != null)
            {
                MessageBox.Show("Permiso encontrado!", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LlenarCampo(empleados);
            }
            else
                MessageBox.Show("Este Permiso no existe, prueba buscar otro!", "Fallo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void GuardarButton_Click(object sender, EventArgs e)
        {
            Empleados empleados;
            empleados = LlenarClase();

            var paso = EmpleadosBll.Guardar(empleados);

            if (paso)
            {
                MessageBox.Show("Empleado guardado correctamente.", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Limpiar();
            }
            else
                MessageBox.Show("No se pudo guardar este permiso, intentalo de nuevo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void eliminarButton_Click(object sender, EventArgs e)
        {
            /*
            int id;
            int.TryParse(EmpleadoIdNumericUpDown.Value, out id);

            if (EmpleadoIdNumericUpDown.value == 0)
            {
                MessageBox.Show("Debes agregar un Id valido para poder eliminar un Rol", "Fallo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (ExisteEnLaBaseDeDatos())
            {
                if (MessageBox.Show("Deseas eliminar este Rol?", "Elije una opcion", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (EmpleadosBll.Eliminar(id))
                    {
                        MessageBox.Show("Rol eliminado!", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Limpiar();
                    }

            }
            else
                MessageBox.Show("Este Rol no existe en la base de datos, prueba a eliminar otro.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);*/
        }
    }
}
