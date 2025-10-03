using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vortice
{
    public class EventoInfo
    {
        public string Nombre { get; set; }
        public int CupoMaximo { get; set; }
        public int Inscritos { get; set; }
        public Evento Tipo { get; set; }

        public EventoInfo(string nombre, int cupo, Evento tipo)
        {
            Nombre = nombre;
            CupoMaximo = cupo;
            Tipo = tipo;
            Inscritos = 0;
        }

        public bool TieneCupo() => Inscritos < CupoMaximo;
    }
}
