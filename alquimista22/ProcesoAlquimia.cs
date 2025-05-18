using System;
using System.Collections.Generic;

namespace Alquimista
{
    public class ProcesoAlquimia
    {
        private readonly string textoInicial;
        private readonly Dictionary<string, string> reglasTransformacion;
        public ProcesoAlquimia(string textoInicial, Dictionary<string, string> reglasTransformacion)
        {
            this.textoInicial = textoInicial;
            this.reglasTransformacion = reglasTransformacion;
        }
        public long EjecutarProceso(int numeroPasos)
        {
            var frecuenciaPares = new Dictionary<string, long>();
            for (int posicion = 0; posicion < textoInicial.Length - 1; posicion++)
            {
                string parActual = textoInicial[posicion].ToString() + textoInicial[posicion + 1].ToString();
                if (!frecuenciaPares.ContainsKey(parActual))
                    frecuenciaPares[parActual] = 0;
                frecuenciaPares[parActual]++;
            }

            var frecuenciaCaracteres = new Dictionary<string, long>();
            for (int posicion = 0; posicion < textoInicial.Length; posicion++)
            {
                string caracter = textoInicial[posicion].ToString();
                if (!frecuenciaCaracteres.ContainsKey(caracter))
                    frecuenciaCaracteres[caracter] = 0;
                frecuenciaCaracteres[caracter]++;
            }

            Console.WriteLine("\nConteo inicial de caracteres:");
            var caracteresOrdenados = new List<string>(frecuenciaCaracteres.Keys);
            caracteresOrdenados.Sort();
            foreach (string caracter in caracteresOrdenados)
            {
                Console.WriteLine($"{caracter}: {frecuenciaCaracteres[caracter]}");
            }

            for (int paso = 0; paso < numeroPasos; paso++)
            {
                var nuevosPares = new Dictionary<string, long>();
                foreach (var par in frecuenciaPares.Keys.ToList())
                {
                    if (reglasTransformacion.ContainsKey(par))
                    {
                        string nuevoCaracter = reglasTransformacion[par];
                        string parIzquierdo = par[0] + nuevoCaracter;
                        string parDerecho = nuevoCaracter + par[1];

                        if (!frecuenciaCaracteres.ContainsKey(nuevoCaracter))
                            frecuenciaCaracteres[nuevoCaracter] = 0;
                        frecuenciaCaracteres[nuevoCaracter] += frecuenciaPares[par];

                        if (!nuevosPares.ContainsKey(parIzquierdo))
                            nuevosPares[parIzquierdo] = 0;
                        if (!nuevosPares.ContainsKey(parDerecho))
                            nuevosPares[parDerecho] = 0;

                        nuevosPares[parIzquierdo] += frecuenciaPares[par];
                        nuevosPares[parDerecho] += frecuenciaPares[par];
                    }
                    else
                    {
                        if (!nuevosPares.ContainsKey(par))
                            nuevosPares[par] = 0;
                        nuevosPares[par] += frecuenciaPares[par];
                    }
                }
                frecuenciaPares = nuevosPares;

                if (paso == numeroPasos - 1)
                {
                    Console.WriteLine("\nConteo final de caracteres:");
                    caracteresOrdenados = new List<string>(frecuenciaCaracteres.Keys);
                    caracteresOrdenados.Sort();
                    foreach (string caracter in caracteresOrdenados)
                    {
                        Console.WriteLine($"{caracter}: {frecuenciaCaracteres[caracter]}");
                    }
                }
            }
            
            long maximo = 0;
            long minimo = long.MaxValue;
            foreach (long valor in frecuenciaCaracteres.Values)
            {
                if (valor > maximo) maximo = valor;
                if (valor < minimo) minimo = valor;
            }

            Console.WriteLine($"\nMáximo: {maximo}");
            Console.WriteLine($"Mínimo: {minimo}");

            return maximo - minimo;
        }
    }
} 