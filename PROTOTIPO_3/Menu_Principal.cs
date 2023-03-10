using System;
using System.Windows.Forms;

namespace PROTOTIPO_3
{
    public partial class Menu_Principal : Form
    {
        public Menu_Principal()
        {
            InitializeComponent();
        }

        private Form Formulario_Activo = null;
        private void AbrirFormularioHijo(Form FormularioHijo)
        {
            if (Formulario_Activo != null) 
            { 
                Formulario_Activo.Close();
            }
            Formulario_Activo = FormularioHijo;
            FormularioHijo.TopLevel = false;
            FormularioHijo.FormBorderStyle = FormBorderStyle.None;
            FormularioHijo.Dock = DockStyle.Fill; 
            PanelFormularioHijo.Controls.Add(FormularioHijo);
            PanelFormularioHijo.Tag = FormularioHijo;
            FormularioHijo.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AbrirFormularioHijo(new Form1());
        }
    }
}
