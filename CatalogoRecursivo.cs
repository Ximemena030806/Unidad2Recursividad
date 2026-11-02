using System;

namespace Unidad2Recursividad
{
    public class CatalogoRecursivo
    {
        // 1. Factorial
        // Caso Base: n <= 1 -> retorna 1
        // Caso Recursivo: n * Factorial(n - 1)
        public static long Factorial(int n)
        {
            if (n <= 1) return 1;
            return n * Factorial(n - 1);
        }

        // 2. Fibonacci
        // Caso Base: n <= 0 -> 0; n == 1 -> 1
        // Caso Recursivo: Fibonacci(n - 1) + Fibonacci(n - 2)
        public static long Fibonacci(int n)
        {
            if (n <= 0) return 0;
            if (n == 1) return 1;
            return Fibonacci(n - 1) + Fibonacci(n - 2);
        }

        // 3. Suma de Arreglo
        // Caso Base: n == 0 -> retorna 0
        // Caso Recursivo: arr[n-1] + SumaArreglo(arr, n - 1)
        public static int SumaArreglo(int[] arr, int n)
        {
            if (n <= 0) return 0;
            return arr[n - 1] + SumaArreglo(arr, n - 1);
        }

        // 4. Búsqueda Lineal Recursiva
        // Caso Base: indice >= arr.Length -> -1 (no encontrado); arr[indice] == objetivo -> indice
        // Caso Recursivo: BusquedaLineal(arr, objetivo, indice + 1)
        public static int BusquedaLineal(int[] arr, int objetivo, int indice = 0)
        {
            if (indice >= arr.Length) return -1;
            if (arr[indice] == objetivo) return indice;
            return BusquedaLineal(arr, objetivo, indice + 1);
        }

        // 5. Torres de Hanoi
        // Caso Base: n == 1 -> mover disco directamente de origen a destino
        // Caso Recursivo: Mover n-1 a auxiliar, mover 1 a destino, mover n-1 de auxiliar a destino
        public static void ResolverHanoi(int n, char origen, char destino, char auxiliar)
        {
            if (n == 1)
            {
                Console.WriteLine($"Mover disco 1 de {origen} a {destino}");
                return;
            }
            ResolverHanoi(n - 1, origen, auxiliar, destino);
            Console.WriteLine($"Mover disco {n} de {origen} a {destino}");
            ResolverHanoi(n - 1, auxiliar, destino, origen);
        }
    }
}
