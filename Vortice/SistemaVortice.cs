using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vortice
{
    public class SistemaVortice
    {
        private List<Participante> participantes;
        private Dictionary<Evento, EventoInfo> eventos;
        private Random random;

        private string[] nombresHombre = { "Juan Carlos", "Luis Miguel", "José Antonio", "Carlos Alberto",
            "Miguel Ángel", "Roberto Carlos", "Francisco Javier", "Pedro Luis", "Jorge Alberto", "Ricardo Iván",
            "Daniel Alejandro", "Fernando José", "Arturo Ramón", "Eduardo Manuel", "Óscar David" };

        private string[] nombresMujer = { "María Elena", "Ana Laura", "Carmen Rosa", "Laura Patricia",
            "Rosa María", "Patricia Isabel", "Elena Sofía", "Isabel Fernanda", "Sofía Valentina", "Valentina Andrea",
            "Andrea Carolina", "Carolina Daniela", "Daniela Gabriela", "Gabriela Fernanda", "Fernanda Alejandra" };

        private string[] apellidos = { "García", "Rodríguez", "Martínez", "López", "González", "Hernández",
            "Pérez", "Sánchez", "Ramírez", "Torres", "Flores", "Rivera", "Gómez", "Díaz", "Cruz",
            "Morales", "Reyes", "Gutiérrez", "Ortiz", "Mendoza", "Jiménez", "Ruiz", "Álvarez", "Castillo" };

        private string[] carreras = { "DDMI", "Ingeniería Industrial",
            "Ingeniería Mecatrónica", "Ingeniería Civil", "Arquitectura", "Contaduría",
            "Administración de Empresas", "Derecho", "Psicología", "Diseño Gráfico" };

        public SistemaVortice()
        {
            participantes = new List<Participante>();
            random = new Random();

            eventos = new Dictionary<Evento, EventoInfo>
            {
                { Evento.RayandoAndo, new EventoInfo("Rayando-Ando", 120, Evento.RayandoAndo) },
                { Evento.GeekIntrovertido, new EventoInfo("Geek-Introvertido", 230, Evento.GeekIntrovertido) },
                { Evento.SuavecitoPaBajo, new EventoInfo("Suavecito pa'bajo", 180, Evento.SuavecitoPaBajo) }
            };
        }

        public void GenerarParticipantes()
        {
            HashSet<string> matriculasUsadas = new HashSet<string>();
            HashSet<string> nombresUsados = new HashSet<string>();

            foreach (var eventoKvp in eventos)
            {
                EventoInfo ev = eventoKvp.Value;

                for (int i = 0; i < ev.CupoMaximo; i++)
                {
                    string nombre, matricula;
                    Genero genero = random.Next(2) == 0 ? Genero.Masculino : Genero.Femenino;

                    do
                    {
                        string primerNombre = genero == Genero.Masculino ?
                            nombresHombre[random.Next(nombresHombre.Length)] :
                            nombresMujer[random.Next(nombresMujer.Length)];

                        string apellido1 = apellidos[random.Next(apellidos.Length)];
                        string apellido2 = apellidos[random.Next(apellidos.Length)];

                        nombre = $"{primerNombre} {apellido1} {apellido2}";
                    } while (nombresUsados.Contains(nombre));

                    do
                    {
                        matricula = $"A{random.Next(20000000, 20250000)}";
                    } while (matriculasUsadas.Contains(matricula));

                    string carrera = carreras[random.Next(carreras.Length)];
                    int semestre = random.Next(1, 11);

                    Participante p = new Participante(nombre, matricula, carrera, genero, semestre, eventoKvp.Key);

                    participantes.Add(p);
                    matriculasUsadas.Add(matricula);
                    nombresUsados.Add(nombre);
                    ev.Inscritos++;
                }
            }

            Console.WriteLine($"\n✓ Se generaron {participantes.Count} participantes exitosamente");
        }

        public bool RegistrarParticipante(Participante p)
        {
            if (participantes.Any(x => x.Matricula == p.Matricula))
            {
                Console.WriteLine("✗ Error: Matrícula ya registrada");
                return false;
            }

            if (participantes.Any(x => x.Nombre.Equals(p.Nombre, StringComparison.OrdinalIgnoreCase)))
            {
                Console.WriteLine("✗ Error: Nombre ya registrado");
                return false;
            }

            if (!eventos[p.EventoInscrito].TieneCupo())
            {
                Console.WriteLine("✗ Error: Evento sin cupo disponible");
                return false;
            }

            participantes.Add(p);
            eventos[p.EventoInscrito].Inscritos++;
            Console.WriteLine("✓ Participante registrado exitosamente");
            return true;
        }

        public void ConsultarTodosLosParticipantes()
        {
            Console.WriteLine("\n════════════════════════════════════════════════════════════════════════════════════════════════════════════════");
            Console.WriteLine("                                      LISTA COMPLETA DE PARTICIPANTES");
            Console.WriteLine("════════════════════════════════════════════════════════════════════════════════════════════════════════════════");

            foreach (var evento in eventos.Values.OrderBy(e => e.Tipo))
            {
                Console.WriteLine($"\n▼ {evento.Nombre.ToUpper()} - {evento.Inscritos}/{evento.CupoMaximo} inscritos");
                Console.WriteLine("────────────────────────────────────────────────────────────────────────────────────────────────────────────────");

                var participantesEvento = participantes.Where(p => p.EventoInscrito == evento.Tipo).OrderBy(p => p.Nombre);

                foreach (var p in participantesEvento)
                {
                    Console.WriteLine($"  {p}");
                }
            }

            Console.WriteLine($"\n════════════════════════════════════════════════════════════════════════════════════════════════════════════════");
            Console.WriteLine($"TOTAL DE PARTICIPANTES: {participantes.Count}");
            Console.WriteLine("════════════════════════════════════════════════════════════════════════════════════════════════════════════════\n");
        }

        public void ConsultarPorNombre(string busqueda)
        {
            var resultados = participantes.Where(p =>
                p.Nombre.ToLower().Contains(busqueda.ToLower())).ToList();

            Console.WriteLine($"\n╔═══════════════════════════════════════════════════════════════════════════════════════════════════════════════╗");
            Console.WriteLine($"  BÚSQUEDA: '{busqueda}' - {resultados.Count} resultado(s)");
            Console.WriteLine($"╚═══════════════════════════════════════════════════════════════════════════════════════════════════════════════╝");

            if (resultados.Count == 0)
            {
                Console.WriteLine("  No se encontraron participantes con ese nombre/apellido\n");
                return;
            }

            foreach (var p in resultados.OrderBy(p => p.Nombre))
            {
                Console.WriteLine($"  {p}");
            }
            Console.WriteLine();
        }

        public void ConsultarPorCarrera(string carrera)
        {
            var resultados = participantes.Where(p =>
                p.Carrera.ToLower().Contains(carrera.ToLower())).ToList();

            Console.WriteLine($"\n╔═══════════════════════════════════════════════════════════════════════════════════════════════════════════════╗");
            Console.WriteLine($"  CARRERA: '{carrera}' - {resultados.Count} participante(s)");
            Console.WriteLine($"╚═══════════════════════════════════════════════════════════════════════════════════════════════════════════════╝");

            if (resultados.Count == 0)
            {
                Console.WriteLine("  No se encontraron participantes de esa carrera\n");
                return;
            }

            foreach (var p in resultados.OrderBy(p => p.Nombre))
            {
                Console.WriteLine($"  {p}");
            }
            Console.WriteLine();
        }

        public void MostrarPorcentajePorCarrera()
        {
            var porCarrera = participantes.GroupBy(p => p.Carrera)
                .Select(g => new { Carrera = g.Key, Total = g.Count() })
                .OrderByDescending(x => x.Total);

            Console.WriteLine("\n╔═══════════════════════════════════════════════════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("                                   PORCENTAJE DE PARTICIPACIÓN POR CARRERA");
            Console.WriteLine("╚═══════════════════════════════════════════════════════════════════════════════════════════════════════════════╝\n");

            double total = participantes.Count;

            foreach (var item in porCarrera)
            {
                double porcentaje = (item.Total / total) * 100;
                string barra = new string('█', (int)(porcentaje / 2));
                Console.WriteLine($"  {item.Carrera,-40} │ {item.Total,3} │ {porcentaje,6:F2}% {barra}");
            }
            Console.WriteLine();
        }

        public void MostrarPorcentajePorGenero()
        {
            var porGenero = participantes.GroupBy(p => p.Genero)
                .Select(g => new { Genero = g.Key, Total = g.Count() })
                .OrderByDescending(x => x.Total);

            Console.WriteLine("\n╔═══════════════════════════════════════════════════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("                                   PORCENTAJE DE PARTICIPACIÓN POR GÉNERO");
            Console.WriteLine("╚═══════════════════════════════════════════════════════════════════════════════════════════════════════════════╝\n");

            double total = participantes.Count;

            foreach (var item in porGenero)
            {
                double porcentaje = (item.Total / total) * 100;
                string barra = new string('█', (int)(porcentaje / 2));
                Console.WriteLine($"  {item.Genero,-15} │ {item.Total,3} │ {porcentaje,6:F2}% {barra}");
            }
            Console.WriteLine();
        }

        public void GenerarGanadores()
        {
            Console.WriteLine("\n╔═══════════════════════════════════════════════════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("                                        GANADORES VÓRTICE 2025");
            Console.WriteLine("╚═══════════════════════════════════════════════════════════════════════════════════════════════════════════════╝\n");

            foreach (var eventoKvp in eventos)
            {
                var participantesEvento = participantes.Where(p => p.EventoInscrito == eventoKvp.Key).ToList();

                if (participantesEvento.Count > 0)
                {
                    var ganador = participantesEvento[random.Next(participantesEvento.Count)];
                    Console.WriteLine($"     {eventoKvp.Value.Nombre}");
                    Console.WriteLine($"     Ganador: {ganador.Nombre}");
                    Console.WriteLine($"     Matrícula: {ganador.Matricula}");
                    Console.WriteLine($"     Carrera: {ganador.Carrera}\n");
                }
            }
        }

        public void GenerarArchivoParticipantes()
        {
            string nombreArchivo = $"Participantes_Vortice2025_{DateTime.Now:yyyyMMdd_HHmmss}.txt";

            try
            {
                using (StreamWriter sw = new StreamWriter(nombreArchivo))
                {
                    sw.WriteLine("════════════════════════════════════════════════════════════════════════════════════════════════════════════════");
                    sw.WriteLine("                              ENCUENTRO MULTIDISCIPLINARIO VÓRTICE 2025");
                    sw.WriteLine("                                    LISTA DE PARTICIPANTES");
                    sw.WriteLine($"                                Generado: {DateTime.Now:dd/MM/yyyy HH:mm:ss}");
                    sw.WriteLine("════════════════════════════════════════════════════════════════════════════════════════════════════════════════\n");

                    foreach (var evento in eventos.Values.OrderBy(e => e.Tipo))
                    {
                        sw.WriteLine($"\n▼ {evento.Nombre.ToUpper()} - {evento.Inscritos}/{evento.CupoMaximo} inscritos");
                        sw.WriteLine("────────────────────────────────────────────────────────────────────────────────────────────────────────────────");

                        var participantesEvento = participantes.Where(p => p.EventoInscrito == evento.Tipo).OrderBy(p => p.Nombre);

                        foreach (var p in participantesEvento)
                        {
                            sw.WriteLine($"  {p}");
                        }
                    }

                    sw.WriteLine($"\n════════════════════════════════════════════════════════════════════════════════════════════════════════════════");
                    sw.WriteLine($"TOTAL DE PARTICIPANTES: {participantes.Count}");
                    sw.WriteLine("════════════════════════════════════════════════════════════════════════════════════════════════════════════════");

                    sw.WriteLine("\n\nESTADÍSTICAS GENERALES");
                    sw.WriteLine("════════════════════════════════════════════════════════════════════════════════════════════════════════════════\n");

                    sw.WriteLine("Participación por Género:");
                    var porGenero = participantes.GroupBy(p => p.Genero);
                    foreach (var g in porGenero)
                    {
                        double porc = (g.Count() / (double)participantes.Count) * 100;
                        sw.WriteLine($"  {g.Key}: {g.Count()} ({porc:F2}%)");
                    }

                    sw.WriteLine("\nParticipación por Carrera:");
                    var porCarrera = participantes.GroupBy(p => p.Carrera).OrderByDescending(g => g.Count());
                    foreach (var c in porCarrera)
                    {
                        double porc = (c.Count() / (double)participantes.Count) * 100;
                        sw.WriteLine($"  {c.Key}: {c.Count()} ({porc:F2}%)");
                    }
                }

                Console.WriteLine($"\n✓ Archivo generado exitosamente: {nombreArchivo}\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n✗ Error al generar el archivo: {ex.Message}\n");
            }
        }

        public void MostrarEstadoEventos()
        {
            Console.WriteLine("\n╔═══════════════════════════════════════════════════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("                                         ESTADO DE EVENTOS");
            Console.WriteLine("╚═══════════════════════════════════════════════════════════════════════════════════════════════════════════════╝\n");

            foreach (var ev in eventos.Values)
            {
                double porcentaje = (ev.Inscritos / (double)ev.CupoMaximo) * 100;
                string barra = new string('█', (int)(porcentaje / 5));
                Console.WriteLine($"  {ev.Nombre,-25} │ {ev.Inscritos,3}/{ev.CupoMaximo,3} │ {porcentaje,6:F2}% {barra}");
            }
            Console.WriteLine();
        }
    }
}
