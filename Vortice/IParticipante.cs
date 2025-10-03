using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vortice
{
    public interface IParticipante
    {
        string Nombre { get; set; }
        string Matricula { get; set; }
        string Carrera { get; set; }
        Genero Genero { get; set; }
        int Semestre { get; set; }
        Evento EventoInscrito { get; set; }
    }
}
