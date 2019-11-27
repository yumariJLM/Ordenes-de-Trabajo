using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Seguridad
{
    public class EquiposBL
    {
        Contexto _contexto;
        public BindingList<Equipo> ListaEquipos { get; set; }


        public EquiposBL()
        {
            _contexto = new Contexto();
            ListaEquipos = new BindingList<Equipo>();
        }

        public BindingList<Equipo> ObtenerProductos()
        {
            _contexto.Equipos.Load();

            ListaEquipos = _contexto.Equipos.Local.ToBindingList();
            return ListaEquipos;
        }


        public void CancelarCambios()
        {
            foreach (var item in _contexto.ChangeTracker.Entries())
            {
                item.State = EntityState.Unchanged;
                item.Reload();
            }
        }

        //guardaR
        public Resultado GuardarEquipo(Equipo equipo)
        {
            var resultado = Validar(equipo);
            if (resultado.Exitoso == false)
            {
                return resultado;
            }

            _contexto.SaveChanges();

            resultado.Exitoso = true;
            return resultado;
        }
       


        //agregar
        public  void AgregarEquipo()
        {
            var nuevoEquipo = new Equipo();
            ListaEquipos.Add(nuevoEquipo);
        }

        //elinimar
        public bool EliminarProducto(int id)
        {
            foreach (var equipo in ListaEquipos)
            {
                if (equipo.Id == id)
                {
                    ListaEquipos.Remove( equipo);

                    _contexto.SaveChanges();
                    return true;
                }

            }

            return false;
        }


        //Validaciones
        private Resultado Validar(Equipo equipo)
        {
            var resultado = new Resultado();
            resultado.Exitoso = true;

            if (string.IsNullOrEmpty(equipo.Marca) == true)
            {
                resultado.Mensaje = "Ingrese marca del equipo";
                resultado.Exitoso = false;
            }

            else if (string.IsNullOrEmpty(equipo.Marca) == true)
            {
                resultado.Mensaje = "Por favor complete todos los datos del equipo";
                resultado.Exitoso = false;
            }

            else if (string.IsNullOrEmpty(equipo.NoSerie) == true)
            {
                resultado.Mensaje = "Por favor complete todos los datos del equipo";
                resultado.Exitoso = false;
            }
            else if (string.IsNullOrEmpty(equipo.Fallas) == true)
            {
                resultado.Mensaje = "Por favor complete todos los datos del equipo";
                resultado.Exitoso = false;
            }

            if (equipo.TipoId == 0)
            {
                resultado.Mensaje = "Complete los campos";
                resultado.Exitoso = false;

            }

            return resultado;
        }
}

    public class Equipo
    {

        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public string NoSerie { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Fallas { get; set; }
        public bool Activo { get; set; }
        public int TipoId { get; set; }
        public Tipo Tipo { get; set; }
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }
        public Byte[] Foto { get; set; }
        

    }
}

