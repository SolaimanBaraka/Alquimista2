using System;
using System.Collections.Generic;
using System.IO;

namespace Alquimista
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string rutaArchivo = "../../../alquimista2.txt";
                
                if (!File.Exists(rutaArchivo))
                {
                    Console.WriteLine($"Error: No se encuentra el archivo en: {rutaArchivo}");
                    return;
                }

                string[] lines = File.ReadAllLines(rutaArchivo);
                string textoInicial = lines[0];
                Console.WriteLine($"Texto inicial: {textoInicial}");
                
                Dictionary<string, string> transformaciones = new Dictionary<string, string>();

                for (int i = 1; i < lines.Length; i++)
                {
                    if (lines[i].Length > 0)
                    {
                        string[] parts = lines[i].Split(" -> ");
                        transformaciones[parts[0]] = parts[1];
                    }
                }
                Console.WriteLine($"Número de transformaciones leídas: {transformaciones.Count}");

                Console.WriteLine("Iniciando proceso");
                var proceso = new ProcesoAlquimia(textoInicial, transformaciones);
                long temperatura = proceso.EjecutarProceso(40);
                Console.WriteLine($"La temperatura necesaria es: {temperatura} grados");
            }
            
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}