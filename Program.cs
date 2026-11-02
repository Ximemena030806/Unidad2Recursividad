using System;
using System.IO;
using System.Text;

namespace Unidad2Recursividad
{
    /// <summary>
    /// Menú de consola que ejecuta cada problema del catálogo en sus dos versiones
    /// (iterativa y recursiva) y verifica que ambas den el mismo resultado.
    /// </summary>
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            bool salir = false;

            while (!salir)
            {
                Console.WriteLine("\n=== CATÁLOGO: ITERATIVO vs RECURSIVO ===");
                Console.WriteLine("1. Factorial");
                Console.WriteLine("2. Fibonacci");
                Console.WriteLine("3. Torres de Hanoi");
                Console.WriteLine("4. Suma de arreglo");
                Console.WriteLine("5. Búsqueda lineal");
                Console.WriteLine("6. Ejecutar todas las pruebas automáticas");
                Console.WriteLine("0. Salir");
                Console.Write("Opción: ");
                string opcion = Console.ReadLine();
                Console.WriteLine();

                switch (opcion)
                {
                    case "1": ProbarFactorial(); break;
                    case "2": ProbarFibonacci(); break;
                    case "3": ProbarHanoi(); break;
                    case "4": ProbarSuma(); break;
                    case "5": ProbarBusqueda(); break;
                    case "6": PruebasAutomaticas(); break;
                    case "0": salir = true; break;
                    default: Console.WriteLine("Opción no válida."); break;
                }
            }
        }

        // ------------------------------------------------------------ Pruebas interactivas

        private static void ProbarFactorial()
        {
            // Límite 20: 21! desborda el tipo long.
            int n = LeerEntero("n (0 - 20): ", 0, 20);
            long it = CatalogoIterativo.Factorial(n);
            long rec = CatalogoRecursivo.Factorial(n);
            MostrarComparacion($"Factorial({n})", it, rec);
        }

        private static void ProbarFibonacci()
        {
            // Límite 40: la versión recursiva hace ~2^n llamadas y se vuelve muy lenta.
            int n = LeerEntero("n (0 - 40): ", 0, 40);
            long it = CatalogoIterativo.Fibonacci(n);
            long rec = CatalogoRecursivo.Fibonacci(n);
            MostrarComparacion($"Fibonacci({n})", it, rec);
        }

        private static void ProbarHanoi()
        {
            int n = LeerEntero("Número de discos (1 - 8): ", 1, 8);

            // Se captura la salida de cada versión para poder compararlas.
            string salidaIt = CapturarSalida(() => CatalogoIterativo.ResolverHanoi(n, 'A', 'C', 'B'));
            string salidaRec = CapturarSalida(() => CatalogoRecursivo.ResolverHanoi(n, 'A', 'C', 'B'));

            Console.WriteLine("--- Versión recursiva ---");
            Console.Write(salidaRec);
            Console.WriteLine($"\nMovimientos: {(1 << n) - 1} (2^{n} - 1)");
            Console.WriteLine($"¿La versión iterativa produce la misma secuencia? {salidaIt == salidaRec}");
        }

        private static void ProbarSuma()
        {
            int[] arr = LeerArreglo();
            int it = CatalogoIterativo.SumaArreglo(arr, arr.Length);
            int rec = CatalogoRecursivo.SumaArreglo(arr, arr.Length);
            MostrarComparacion("SumaArreglo", it, rec);
        }

        private static void ProbarBusqueda()
        {
            int[] arr = LeerArreglo();
            int objetivo = LeerEntero("Valor a buscar: ", int.MinValue, int.MaxValue);
            int it = CatalogoIterativo.BusquedaLineal(arr, objetivo);
            int rec = CatalogoRecursivo.BusquedaLineal(arr, objetivo);
            MostrarComparacion($"BusquedaLineal({objetivo}) [índice, -1 = no encontrado]", it, rec);
        }

        // ------------------------------------------------------------ Pruebas automáticas

        private static void PruebasAutomaticas()
        {
            Console.WriteLine("--- Factorial ---");
            foreach (int n in new[] { 0, 1, 5, 10, 20 })
                MostrarComparacion($"Factorial({n})", CatalogoIterativo.Factorial(n), CatalogoRecursivo.Factorial(n));

            Console.WriteLine("\n--- Fibonacci ---");
            foreach (int n in new[] { 0, 1, 2, 10, 20, 30 })
                MostrarComparacion($"Fibonacci({n})", CatalogoIterativo.Fibonacci(n), CatalogoRecursivo.Fibonacci(n));

            Console.WriteLine("\n--- Suma de arreglo ---");
            int[] datos = { 4, 8, 15, 16, 23, 42 };
            MostrarComparacion("SumaArreglo {4,8,15,16,23,42}",
                CatalogoIterativo.SumaArreglo(datos, datos.Length),
                CatalogoRecursivo.SumaArreglo(datos, datos.Length));

            Console.WriteLine("\n--- Búsqueda lineal (mismo arreglo) ---");
            foreach (int objetivo in new[] { 4, 23, 42, 99 })
                MostrarComparacion($"BusquedaLineal({objetivo})",
                    CatalogoIterativo.BusquedaLineal(datos, objetivo),
                    CatalogoRecursivo.BusquedaLineal(datos, objetivo));

            Console.WriteLine("\n--- Torres de Hanoi (n = 1..8) ---");
            for (int n = 1; n <= 8; n++)
            {
                string it = CapturarSalida(() => CatalogoIterativo.ResolverHanoi(n, 'A', 'C', 'B'));
                string rec = CapturarSalida(() => CatalogoRecursivo.ResolverHanoi(n, 'A', 'C', 'B'));
                Console.WriteLine($"n = {n}: {(1 << n) - 1} movimientos | misma secuencia: {it == rec}");
            }
        }

        // ------------------------------------------------------------ Auxiliares

        private static void MostrarComparacion(string nombre, long iterativo, long recursivo)
        {
            Console.WriteLine($"{nombre}");
            Console.WriteLine($"   Iterativo : {iterativo}");
            Console.WriteLine($"   Recursivo : {recursivo}");
            Console.WriteLine($"   ¿Iguales? : {iterativo == recursivo}");
        }

        // Ejecuta una acción que escribe en consola y devuelve lo que escribió como texto.
        private static string CapturarSalida(Action accion)
        {
            TextWriter original = Console.Out;
            StringWriter capturador = new StringWriter();
            Console.SetOut(capturador);
            try { accion(); }
            finally { Console.SetOut(original); }
            return capturador.ToString();
        }

        private static int[] LeerArreglo()
        {
            while (true)
            {
                Console.Write("Elementos separados por espacio (ej: 3 7 1 9): ");
                string linea = Console.ReadLine();
                string[] partes = (linea ?? "").Split(new[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);

                int[] arr = new int[partes.Length];
                bool valido = partes.Length > 0;
                for (int i = 0; i < partes.Length && valido; i++)
                    valido = int.TryParse(partes[i], out arr[i]);

                if (valido) return arr;
                Console.WriteLine("Entrada inválida: ingrese al menos un entero.");
            }
        }

        private static int LeerEntero(string mensaje, int minimo, int maximo)
        {
            while (true)
            {
                Console.Write(mensaje);
                int valor;
                if (int.TryParse(Console.ReadLine(), out valor) && valor >= minimo && valor <= maximo)
                    return valor;
                Console.WriteLine($"Entrada inválida. Ingrese un entero entre {minimo} y {maximo}.");
            }
        }
    }
}
