using BL.Seguridad;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ARYA
{
    public partial class Tecnicos : Form
    {
        NuevoTBL _tecnico;
        TiposBL _tiposBL;
        public Tecnicos()
        {
            InitializeComponent();
            _tecnico = new NuevoTBL();
            listaTecnicosBindingSource.DataSource = _tecnico.SeleccionarTecnico();

            _tiposBL = new TiposBL();
            listaTiposBindingSource.DataSource = _tiposBL.ObtenerTipos();
        }

        private void Tecnicos_Load(object sender, EventArgs e)
        {

        }

        private void listaTecnicosBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            listaTecnicosBindingSource.EndEdit();
            var tecnico = (Tecnico)listaTecnicosBindingSource.Current;

            if (fotoPictureBox.Image != null)
            {
                tecnico.Foto = Program.imagetoByteArray(fotoPictureBox.Image);
            }
            else
            {
                tecnico.Foto = null;
            }

            var resultado = _tecnico.GuardarTecnico(tecnico);

            if (resultado.Exitoso == true)
            {
                listaTecnicosBindingSource.ResetBindings(false);
                DeshabilitarHabilitarBotones(true);
                MessageBox.Show("Tecnico guardado");
            }
            else
            {
                MessageBox.Show(resultado.Mensaje);
            }
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            _tecnico.AgregarTecnico();
            listaTecnicosBindingSource.MoveLast();

            DeshabilitarHabilitarBotones(false);
        }

        private void DeshabilitarHabilitarBotones(bool v)
        {
            bindingNavigatorMoveFirstItem.Enabled = v;
            bindingNavigatorMoveLastItem.Enabled = v;
            bindingNavigatorMovePreviousItem.Enabled = v;
            bindingNavigatorMoveNextItem.Enabled = v;
            bindingNavigatorPositionItem.Enabled = v;
            bindingNavigatorAddNewItem.Enabled = v;
            bindingNavigatorDeleteItem.Enabled = v;

            toolStripButtonCancelar.Visible = !v;

        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            if (idTextBox.Text != "")
            {
                var resultado = MessageBox.Show("Desea eliminar este registro?", "Eliminar", MessageBoxButtons.YesNo);
                if (resultado == DialogResult.Yes)

                {
                    var id = Convert.ToInt32(idTextBox.Text);
                    Eliminar(id);
                }
            }
        }

        private void Eliminar(int id)
        {

            var resultado = _tecnico.EliminarTecnico(id);

            if (resultado == true)
            {
                listaTecnicosBindingSource.ResetBindings(false);
            }
            else
            {
                MessageBox.Show("Ocurrio un error al eliminar el producto");
            }
        }

        private void toolStripButtonCancelar_Click(object sender, EventArgs e)
        {
            
            DeshabilitarHabilitarBotones(true);
            
        }

        private void listaTecnicosDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void fotoLabel_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {



            var tecnico = (Tecnico)listaTecnicosBindingSource.Current;

            if (tecnico != null)
            {

                openFileDialog1.ShowDialog();
                var archivo = openFileDialog1.FileName;

                if (archivo != "")
                {
                    var fileInfo = new FileInfo(archivo);
                    var fileStream = fileInfo.OpenRead();

                    fotoPictureBox.Image = Image.FromStream(fileStream);
                }
            }
            else
            {
                MessageBox.Show(" Favor cree un tecnico antes de asignar una foto ");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            fotoPictureBox.Image = null;

        }

        private void tipoIdLabel_Click(object sender, EventArgs e)
        {

        }

        private void tipoIdComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tipoIdLabel_Click_1(object sender, EventArgs e)
        {

        }

        private void tipoIdComboBox_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void fotoPictureBox_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            listaTecnicosBindingSource.EndEdit();
            var tecnico = (Tecnico)listaTecnicosBindingSource.Current;

            if (fotoPictureBox.Image != null)
            {
                tecnico.Foto = Program.imagetoByteArray(fotoPictureBox.Image);
            }
            else
            {
                tecnico.Foto = null;
            }

            var resultado = _tecnico.GuardarTecnico(tecnico);

            if (resultado.Exitoso == true)
            {
                listaTecnicosBindingSource.ResetBindings(false);
                DeshabilitarHabilitarBotones(true);
                MessageBox.Show("Tecnico guardado");
            }
            else
            {
                MessageBox.Show(resultado.Mensaje);
            }
        }
    }
}

