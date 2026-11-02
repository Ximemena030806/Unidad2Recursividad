using System;
using System.Collections.Generic;

namespace Unidad2Recursividad
{
    /// <summary>
    /// Versiones ITERATIVAS (con ciclos) de cada problema del catálogo.
    /// Cada método tiene su equivalente recursivo en CatalogoRecursivo
    /// y ambos deben producir exactamente el mismo resultado.
    /// </summary>
    public class CatalogoIterativo
    {
        // 1. Factorial
        // Ciclo: acumulador = 1; para i desde 2 hasta n -> acumulador *= i
        // Equivale al caso recursivo n * Factorial(n - 1), pero multiplicando de abajo hacia arriba.
        public static long Factorial(int n)
        {
            long resultado = 1;
            for (int i = 2; i <= n; i++)
                resultado *= i;
            return resultado; // Si n <= 1 el ciclo no corre y retorna 1 (caso base).
        }

        // 2. Fibonacci
        // Ciclo: se mantienen los dos últimos valores (anterior, actual) y se avanza n veces.
        // Evita la explosión exponencial de llamadas de la versión recursiva: O(n) vs O(2^n).
        public static long Fibonacci(int n)
        {
            if (n <= 0) return 0;
            if (n == 1) return 1;

            long anterior = 0, actual = 1;
            for (int i = 2; i <= n; i++)
            {
                long siguiente = anterior + actual;
                anterior = actual;
                actual = siguiente;
            }
            return actual;
        }

        // 3. Suma de Arreglo
        // Ciclo: recorre los primeros n elementos acumulando su valor.
        // El parámetro n cumple el mismo papel que en la versión recursiva.
        public static int SumaArreglo(int[] arr, int n)
        {
            int suma = 0;
            for (int i = 0; i < n; i++)
                suma += arr[i];
            return suma; // Si n <= 0 el ciclo no corre y retorna 0 (caso base).
        }

        // 4. Búsqueda Lineal
        // Ciclo: revisa cada posición desde 'indice'; retorna al primer acierto.
        // Si termina el ciclo sin encontrarlo retorna -1 (caso base "no encontrado").
        public static int BusquedaLineal(int[] arr, int objetivo, int indice = 0)
        {
            for (int i = indice; i < arr.Length; i++)
            {
                if (arr[i] == objetivo)
                    return i;
            }
            return -1;
        }

        // 5. Torres de Hanoi
        // Versión iterativa clásica (sin recursión) usando una pila por cada torre.
        //  - Se realizan 2^n - 1 movimientos.
        //  - Si n es par, se intercambian los roles de destino y auxiliar.
        //  - En el movimiento i se opera entre un par de torres según i % 3:
        //        i % 3 == 1 -> origen  <-> destino
        //        i % 3 == 2 -> origen  <-> auxiliar
        //        i % 3 == 0 -> auxiliar <-> destino
        //  - Entre ese par, siempre se mueve el disco más pequeño hacia la otra torre
        //    (o el único disponible si una está vacía), lo que garantiza movimientos legales.
        // Produce la misma secuencia de movimientos que ResolverHanoi (recursivo).
        public static void ResolverHanoi(int n, char origen, char destino, char auxiliar)
        {
            // Índices: 0 = origen, 1 = destino, 2 = auxiliar
            char[] nombres = { origen, destino, auxiliar };
            Stack<int>[] torres =
            {
                new Stack<int>(), new Stack<int>(), new Stack<int>()
            };

            // Todos los discos empiezan en el origen: el más grande abajo, el 1 arriba.
            for (int disco = n; disco >= 1; disco--)
                torres[0].Push(disco);

            int indiceDestino = (n % 2 == 1) ? 1 : 2;
            int indiceAuxiliar = (n % 2 == 1) ? 2 : 1;

            long totalMovimientos = (1L << n) - 1; // 2^n - 1

            for (long i = 1; i <= totalMovimientos; i++)
            {
                int a, b;
                switch (i % 3)
                {
                    case 1:  a = 0;               b = indiceDestino; break;
                    case 2:  a = 0;               b = indiceAuxiliar; break;
                    default: a = indiceAuxiliar;  b = indiceDestino; break;
                }

                // Se decide el sentido del movimiento entre las torres a y b.
                if (torres[a].Count == 0 ||
                    (torres[b].Count > 0 && torres[b].Peek() < torres[a].Peek()))
                {
                    int temporal = a; a = b; b = temporal; // mover de b hacia a
                }

                int discoMovido = torres[a].Pop();
                torres[b].Push(discoMovido);
                Console.WriteLine($"Mover disco {discoMovido} de {nombres[a]} a {nombres[b]}");
            }
        }
    }
}
