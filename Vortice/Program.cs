using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vortice
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("╔═══════════════════════════════════════════════════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                                                                                                               ║");
            Console.WriteLine("║                            SISTEMA DE REGISTRO VÓRTICE 2025                                                   ║");
            Console.WriteLine("║                              Encuentro Multidisciplinario                                                     ║");
            Console.WriteLine("║                                                                                                               ║");
            Console.WriteLine("╚═══════════════════════════════════════════════════════════════════════════════════════════════════════════════╝");

            SistemaVortice sistema = new SistemaVortice();
            bool salir = false;

            while (!salir)
            {
                Console.WriteLine("\n┌─────────────────────────────────────────────────────────────────────────────────────────────────────────────┐");
                Console.WriteLine("│                                            MENÚ PRINCIPAL                                                   │");
                Console.WriteLine("├─────────────────────────────────────────────────────────────────────────────────────────────────────────────┤");
                Console.WriteLine("│  1. Generar participantes automáticamente                                                                   │");
                Console.WriteLine("│  2. Consultar todos los participantes                                                                       │");
                Console.WriteLine("│  3. Buscar participantes por nombre/apellido                                                                │");
                Console.WriteLine("│  4. Consultar participantes por carrera                                                                     │");
                Console.WriteLine("│  5. Ver porcentaje de participación por carrera                                                             │");
                Console.WriteLine("│  6. Ver porcentaje de participación por género                                                              │");
                Console.WriteLine("│  7. Generar ganadores de cada evento                                                                        │");
                Console.WriteLine("│  8. Ver estado de eventos                                                                                   │");
                Console.WriteLine("│  9. Generar archivo con lista de participantes                                                              │");
                Console.WriteLine("│  0. Salir                                                                                                   │");
                Console.WriteLine("└─────────────────────────────────────────────────────────────────────────────────────────────────────────────┘");
                Console.Write("\n  Seleccione una opción: ");

                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        sistema.GenerarParticipantes();
                        break;
                    case "2":
                        sistema.ConsultarTodosLosParticipantes();
                        break;
                    case "3":
                        Console.Write("\n  Ingrese nombre o apellido a buscar: ");
                        string nombre = Console.ReadLine();
                        sistema.ConsultarPorNombre(nombre);
                        break;
                    case "4":
                        Console.Write("\n  Ingrese la carrera a buscar: ");
                        string carrera = Console.ReadLine();
                        sistema.ConsultarPorCarrera(carrera);
                        break;
                    case "5":
                        sistema.MostrarPorcentajePorCarrera();
                        break;
                    case "6":
                        sistema.MostrarPorcentajePorGenero();
                        break;
                    case "7":
                        sistema.GenerarGanadores();
                        break;
                    case "8":
                        sistema.MostrarEstadoEventos();
                        break;
                    case "9":
                        sistema.GenerarArchivoParticipantes();
                        break;
                    case "0":
                        Console.WriteLine("\n  ¡Gracias por usar el Sistema de Registro Vórtice 2025!");
                        salir = true;
                        break;
                    default:
                        Console.WriteLine("\n  ✗ Opción no válida. Intente de nuevo.");
                        break;
                }

                if (!salir)
                {
                    Console.WriteLine("\n  Presione cualquier tecla para continuar...");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
        }
    }
}
