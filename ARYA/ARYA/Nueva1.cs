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
using static BL.Seguridad.EquiposBL;

namespace ARYA
{
    public partial class Nueva1 : Form
    {
        EquiposBL _equipos;
        TiposBL _tiposBL;
        ClientesBL _clientes;

        public Nueva1()
        {
            InitializeComponent();

            _equipos = new EquiposBL();
            listaEquiposBindingSource.DataSource = _equipos.ObtenerProductos();

            _tiposBL = new TiposBL();
            listaTiposBindingSource.DataSource = _tiposBL.ObtenerTipos();

            _clientes = new ClientesBL();
            listaClientesBindingSource.DataSource = _clientes.ObtenerClientes();

        }



        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            _equipos.AgregarEquipo();
            listaEquiposBindingSource.MoveLast();

            DeshabilitarBotones(false);
        }

        private void DeshabilitarBotones(bool v)
        {
            bindingNavigatorMoveFirstItem.Enabled = v;
            bindingNavigatorMoveLastItem.Enabled = v;
            bindingNavigatorMovePreviousItem.Enabled = v;
            bindingNavigatorMoveNextItem.Enabled = v;
            bindingNavigatorPositionItem.Enabled = v;
            bindingNavigatorAddNewItem.Enabled = v;
            bindingNavigatorDeleteItem.Enabled = v;
            toolStripCancelar.Visible = ! v;


        }


        private void listaEquiposBindingNavigatorSaveItem_Click(object sender, EventArgs e) //FUNCIONGUARDAR
        {

            listaEquiposBindingSource.EndEdit();
            var equipo = (Equipo)listaEquiposBindingSource.Current;

            if (fotoPictureBox.Image != null)
            {
                equipo.Foto = Program.imagetoByteArray(fotoPictureBox.Image);
            }
            else
            {
                equipo.Foto = null;
     
            }

            var resultado = _equipos.GuardarEquipo(equipo);

            if (resultado.Exitoso == true)
            {
               listaEquiposBindingSource.ResetBindings(false);
               DeshabilitarBotones(true);
                MessageBox.Show("Guardado");
            }
            else
            {
                MessageBox.Show(resultado.Mensaje);
            }
        }




        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {

            if (idTextBox.Text != "")
            {
                var resultado = MessageBox.Show("Desea eliminar este registro?", "Eliminar", MessageBoxButtons.YesNo);
                if (resultado == DialogResult.Yes)
                {
                    var id = Convert.ToInt32(idTextBox.Text);


                    //funcionEliminar
                    var resul = _equipos.EliminarProducto(id);
                    if (resul == true)
                    {
                        listaEquiposBindingSource.ResetBindings(false);
                    }
                    else
                    {
                        MessageBox.Show("Ocurrio un error al eliminar la orden");
                    }
                }
            }
        }

        private void toolStripCancelar_Click(object sender, EventArgs e)
        {
            DeshabilitarBotones(true);

            if (idTextBox.Text != "")
            {
                var id = Convert.ToInt32(idTextBox.Text);
                var resultado = _equipos.EliminarProducto(id);

                if (resultado == true)
                {
                    listaEquiposBindingSource.ResetBindings(false);
                }
                else
                {
                    MessageBox.Show("Ocurrio un error al eliminar la orden");
                }
            }
        }

        private void Nueva1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var equipo = (Equipo)listaEquiposBindingSource.Current;
            if (equipo!=null)

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
                MessageBox.Show(" Favor cree un euipo antes de asignar una foto ");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            fotoPictureBox.Image = null;
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }
    }
           
}

