using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vortice
{
    public class Participante : IParticipante
    {
        public string Nombre { get; set; }
        public string Matricula { get; set; }
        public string Carrera { get; set; }
        public Genero Genero { get; set; }
        public int Semestre { get; set; }
        public Evento EventoInscrito { get; set; }

        public Participante(string nombre, string matricula, string carrera,
                           Genero genero, int semestre, Evento evento)
        {
            Nombre = nombre;
            Matricula = matricula;
            Carrera = carrera;
            Genero = genero;
            Semestre = semestre;
            EventoInscrito = evento;
        }

        public override string ToString()
        {
            return $"{Matricula} | {Nombre,-35} | {Carrera,-25} | {Genero,-10} | Sem: {Semestre} | {ObtenerNombreEvento()}";
        }

        private string ObtenerNombreEvento()
        {
            switch (EventoInscrito)
            {
                case Evento.RayandoAndo:
                    return "Rayando-Ando";
                case Evento.GeekIntrovertido:
                    return "Geek-Introvertido";
                case Evento.SuavecitoPaBajo:
                    return "Suavecito pa'bajo";
                default:
                    return "Desconocido";
            }
        }
    }
}