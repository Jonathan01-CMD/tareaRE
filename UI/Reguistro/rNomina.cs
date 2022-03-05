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
    public partial class rNomina : Form
    {
        public rNomina()
        {
            InitializeComponent();

        }

        private Nominas nominas = new Nominas();

        public void Limpiar()
        {
            numericUpDown1.Value = 0;
            SalarioMensualTextBox.Clear();
            HorasExtrasTextBox.Clear();
            SFSTextBox.Clear();
            AFPTextBox.Clear();
            ISRTextBox.Clear();
            SueldoTotalSinTextBox.Clear();
            SueldoTotalConTextBox.Clear();
          
        }
        public void LlenarCampo(Nominas nominas)
        {
            numericUpDown1.Value = nominas.EmpleadoId;
            SalarioMensualTextBox.Text = nominas.SalarioMensual.ToString();
            HorasExtrasTextBox.Text = nominas.HorasExtra.ToString();
            SFSTextBox.Text = nominas.SFS.ToString();
            AFPTextBox.Text = nominas.AFP.ToString();
            dateTimePicker1.Value = nominas.Fecha;
            ISRTextBox.Text = nominas.ISR.ToString();
            SueldoTotalConTextBox.Text = nominas.TotalDecuentos.ToString();
            SueldoTotalSinTextBox.Text = nominas.SueldoTotal.ToString();

        }

        public Nominas LlenarClase()
        {
            Nominas nominas = new Nominas();
            nominas.EmpleadoId = (int)numericUpDown1.Value;
            nominas.SalarioMensual = Convert.ToDouble(SalarioMensualTextBox.Text);
            nominas.HorasExtra = Convert.ToDouble(HorasExtrasTextBox.Text);
            nominas.SFS = Convert.ToDouble(SFSTextBox.Text);
            nominas.AFP = Convert.ToDouble(AFPTextBox.Text);
            nominas.Fecha = dateTimePicker1.Value;
            nominas.ISR = Convert.ToDouble(ISRTextBox.Text);
            nominas.TotalDecuentos = Convert.ToDouble(SueldoTotalConTextBox.Text);
            nominas.SueldoTotal = Convert.ToDouble(SueldoTotalSinTextBox.Text);
            return nominas;
        }
        public bool ExisteEnLaBaseDeDatos()
        {
            Nominas nominas = NominasBLL.Buscar((int)numericUpDown1.Value);

            return (nominas != null);
        }

        private bool Validar()
        {
            bool Validado = true;
            if (numericUpDown1.Text.Length == 0)
            {
                Validado = false;
                MessageBox.Show("Transacción Fallida\n\nNo se pudo validar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            return Validado;
        }


        private void limpiarButton_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void BuscarButton_Click(object sender, EventArgs e)
        {
            Nominas nominas;
            int id;
            int.TryParse(numericUpDown1.Text, out id);

            if (numericUpDown1.Value == 0)
            {
                MessageBox.Show("Debes agregar un numero aqui para poder buscar.", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Limpiar();

            nominas = NominasBLL.Buscar(id);

            if (nominas != null)
            {
                MessageBox.Show("Permiso encontrado!", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LlenarCampo(nominas);
            }
            else
                MessageBox.Show("Este Permiso no existe, prueba buscar otro!", "Fallo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

        }

        private void GuardarButton_Click(object sender, EventArgs e)
        {
            if(!Validar())
                return;

            if (numericUpDown1.Text.Trim() == string.Empty)
            {
                MessageBox.Show("El Campo (Contacto Id) está vacío.\n\nPorfavor, Asigne un Id al Contacto.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                numericUpDown1.Text = "0";
                numericUpDown1.Focus();
                return;
            }

            var paso = NominasBLL.Guardar(nominas);
            if (paso)
            {
                Limpiar();
                MessageBox.Show("El Registro se pudo guardar satisfactoriamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("El Registro no se pudo guardar :(", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void SalarioMensualTextBox_TextChanged(object sender, EventArgs e)
        {
            CalcularSalario();
        }



        private void CalcularSalario()
        {
            if (SalarioMensualTextBox.Text == "0")
            {
                HorasExtrasTextBox.Enabled = false;
            }
            else
            {
                HorasExtrasTextBox.Enabled = true;
            }

            nominas.SalarioMensual = double.Parse(SalarioMensualTextBox.Text.ToString());
            nominas.SalarioMensual += nominas.HorasExtra * 200;




            //—————————————[Formulas para Seguro Familiar de Salud o SFS]—————————————

            double SFS = 0.0304;
            //—————————————[Formulas para Administradora de Fondos de Pensiones o AFP]—————————————
            double AFP = 0.0287;

            //—————————————————————————————————————————[Total SFS y AFP]————————————————————————————————————————————
            double T_SFS = (nominas.SalarioMensual * SFS);
            double T_AFP = (nominas.SalarioMensual * AFP);


            //—————————————[Formulas para Impuesto sobre renta o ISR]—————————————
            //double DEDUC = T_SFS + T_AFP;
            // double RDEDUC = SM - DEDUC;
            // double IMPOx12 = RDEDUC * 12;
            //double EXCEDENT = IMPOx12 - 100 ;
            //double APLICA20 = EXCEDENT * 0.20;
            //double PASO_ADICC = APLICA20 + 31216;

            double T_ISR = nominas.SalarioMensual * 0.10;
            //624329.01

            //—————————————————————————————————————————[Total Descuentos]————————————————————————————————————————————
            double T_Descuentos = (T_SFS + T_AFP + T_ISR);

            SFSTextBox.Text = Convert.ToString(Math.Round(T_SFS, 2));
            AFPTextBox.Text = Convert.ToString(Math.Round(T_AFP, 2));
            ISRTextBox.Text = Convert.ToString(Math.Round(T_ISR, 2));

            SueldoTotalSinTextBox.Text = Convert.ToString(Math.Round(nominas.SalarioMensual, 2));
            SueldoTotalConTextBox.Text = Convert.ToString(Math.Round(nominas.SalarioMensual - T_Descuentos, 2));
        }

        private void HorasExtraTextBox_TextChanged(object sender, EventArgs e)
        {
            int HorasExtra = Convert.ToInt32(HorasExtrasTextBox.Text);
            nominas.SalarioMensual += HorasExtra * 200;
            SalarioMensualTextBox.Text = nominas.SalarioMensual.ToString();
            CalcularSalario();

        }
    }
}
