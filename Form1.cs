namespace Nómina_quincenal
{
    public partial class Form1 : Form
    {
        const double RECARGO_EXTRA = 1.35;
        const int HORAS_REGULARES = 88;
        const double INCENTIVO_TRANSPORTE = 1500;
        const double TASA_AFP = 0.0287;
        const double TASA_SFS = 0.0304;
        const int HORAS_MIN = 1;
        const int HORAS_MAX = 88;
        const double TARIFA_AUXILIAR = 180;
        const double TARIFA_TECNICO = 250;
        const double TARIFA_SUPERVISOR = 350;
        const double TARIFA_GERENTE = 500;

        int cantidadEmpleados = 0;
        double totalBruto = 0;
        double totalDeducciones = 0;
        double totalNeto = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cmbCargo.Items.Add("Auxiliar");
            cmbCargo.Items.Add("Técnico");
            cmbCargo.Items.Add("Supervisor");
            cmbCargo.Items.Add("Gerente");

            cmbCargo.DropDownStyle = ComboBoxStyle.DropDownList;

        }



        private void txtEmpleado_TextChanged(object sender, EventArgs e)
        {
            string Nombre = txtEmpleado.Text;
        }

        private void btnResumen_Click(object sender, EventArgs e)
        {
            if (cantidadEmpleados == 0)
            {
                MessageBox.Show("Aún no hay empleados registrados.",
                    "Sin datos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            lblEmplados.Text = cantidadEmpleados.ToString();
            lblBruto.Text = "RD$ " + totalBruto.ToString("N2");
            lblDeducciones.Text = "RD$ " + totalDeducciones.ToString("N2");
            lblNeto.Text = "RD$ " + totalNeto.ToString("N2");

        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            string nombre = txtEmpleado.Text.Trim();
            if (nombre == "")
            {
                MessageBox.Show("Debe escribir el nombre del empleado.",
                    "Dato requerido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmpleado.Focus();
                return;
            }

            if (!rdbFijo.Checked && !rdbTemporal.Checked)
            {
                MessageBox.Show("Debe seleccionar el tipo de contrato.",
                    "Dato requerido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbCargo.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar un cargo.",
                    "Dato requerido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbCargo.Focus();
                return;
            }

            int horas;
            if (!int.TryParse(txtHoras.Text, out horas) || horas < HORAS_MIN || horas > HORAS_MAX)
            {
                MessageBox.Show("Las horas deben ser un número entero entre "
                    + HORAS_MIN + " y " + HORAS_MAX + ".",
                    "Horas inválidas", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHoras.Focus();
                return;
            }

            // ---------- TARIFA SEGÚN EL CARGO ----------
            string cargo = cmbCargo.SelectedItem.ToString();
            double tarifa;

            if (cargo == "Auxiliar")
                tarifa = TARIFA_AUXILIAR;
            else if (cargo == "Técnico")
                tarifa = TARIFA_TECNICO;
            else if (cargo == "Supervisor")
                tarifa = TARIFA_SUPERVISOR;
            else
                tarifa = TARIFA_GERENTE;

            // ---------- HORAS REGULARES Y EXTRA ----------
            int horasRegulares;
            int horasExtra;

            if (horas > HORAS_REGULARES)
            {
                horasRegulares = HORAS_REGULARES;
                horasExtra = horas - HORAS_REGULARES;
            }
            else
            {
                horasRegulares = horas;
                horasExtra = 0;
            }

            // ---------- CÁLCULOS ----------
            double salarioRegular = horasRegulares * tarifa;
            double pagoExtra = horasExtra * tarifa * RECARGO_EXTRA;

            double incentivo = 0;
            if (chkTransporte.Checked)
                incentivo = INCENTIVO_TRANSPORTE;

            double bruto = salarioRegular + pagoExtra + incentivo;

            // Deducciones: solo contrato Fijo, sobre regular + extra (sin incentivo)
            double deducciones = 0;
            if (rdbFijo.Checked)
                deducciones = (salarioRegular + pagoExtra) * (TASA_AFP + TASA_SFS);

            double neto = bruto - deducciones;

            // ---------- AGREGAR A LA LISTA ----------
            lstNomina.Items.Add(nombre + " · " + cargo + " · RD$ " + neto.ToString("N2"));

            // ---------- ACTUALIZAR ACUMULADORES ----------
            cantidadEmpleados++;
            totalBruto += bruto;
            totalDeducciones += deducciones;
            totalNeto += neto;

            // Deja los campos listos para el siguiente empleado
            LimpiarCampos();
            txtEmpleado.Focus();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
            lstNomina.Items.Clear();

            lblEmplados.Text = "";
            lblBruto.Text = "";
            lblDeducciones.Text = "";
            lblNeto.Text = "";

            cantidadEmpleados = 0;
            totalBruto = 0;
            totalDeducciones = 0;
            totalNeto = 0;

            txtEmpleado.Focus();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LimpiarCampos()
        {
            txtEmpleado.Clear();
            txtHoras.Clear();
            rdbFijo.Checked = false;
            rdbTemporal.Checked = false;
            cmbCargo.SelectedIndex = -1;
            chkTransporte.Checked = false;
        }
    }

}

