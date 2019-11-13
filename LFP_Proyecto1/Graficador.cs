using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace LFP_Proyecto1
{
    class Graficador
    {
        String ruta;//Donde se almacena la imagen
        StringBuilder grafo;//Texto que se convertirá en grafo
        Pais auxP;
        string direccionIMG="";

        public Graficador()
        {
            ruta = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
        }

        private void generarDot(String rdot, String rpng)
        {
            System.IO.File.WriteAllText(rdot, grafo.ToString());
            String comandoDot = "dot.exe -Tpng "+rdot+" -o "+rpng+" ";
            Console.WriteLine(comandoDot);
            var comando = string.Format(comandoDot);//Variable de entorno
            var procStart = new System.Diagnostics.ProcessStartInfo("cmd", "/C" + comando);//Variable de entorno, EJECUTA EL CMD Y MANDA NUESTRO COMANDO
            var proc = new System.Diagnostics.Process();//Variable de entorno, ES UNA VARIABLE QUE EJECUTA PROCESOS
            proc.StartInfo = procStart;//StartInfo ejecute procStart
            proc.Start();//Empieza
            proc.WaitForExit();//Espera a que finalice
            direccionIMG = rpng;
            //abrirGrafo();
        }

        public void graficar(Continente lista, String nombreGrafo)
        {
            //File.WriteAllText(ruta + "\\GRAFO_PROYECTO1\\imagen.dot", String.Empty);
            grafo = new StringBuilder();
            String rdot = ruta + "\\GRAFO_PROYECTO1\\imagen.dot";
            String rpng = ruta + "\\GRAFO_PROYECTO1\\imagen.png";
            grafo.Append(" digraph G {");
            grafo.Append("start[shape = Mdiamond label =  \""+nombreGrafo+"\"];\n");
            Console.WriteLine("start[shape = Mdiamond label =  \"" + nombreGrafo + "\"];\n");
            for (int i = 0; i < lista.getCuantosVan(); i++)
            {
                Console.WriteLine(lista.getNombre(i));
                grafo.Append("start->"+ lista.getNombre(i) +";\n");
                Console.WriteLine(lista.getNombre(i) + "[shape = record label = \"{ " + lista.getNombre(i) + "|" + lista.GetSaturacion(i) + "}\"style = filled fillcolor = " + lista.GetColor(i));
                grafo.Append(lista.getNombre(i) + "[shape = record label = \"{ " + lista.getNombre(i) + "|" + lista.GetSaturacion(i) + "}\"style = filled fillcolor = " + lista.GetColor(i)+ "];\n");
                auxP = lista.getListaPaises(i);

                for (int j = 0; j < auxP.getCuantosVan(); j++)
                {
                    grafo.Append(lista.getNombre(i)+"->" + auxP.getNombre(j) + ";\n");
                    Console.WriteLine(auxP.getNombre(j) + "[shape = record label = \"{ " + auxP.getNombre(j) + "|" + auxP.getSaturacion(j) + "}\"style = filled fillcolor = " + auxP.getColor(j) + "];\n");

                    grafo.Append(auxP.getNombre(j) + "[shape = record label = \"{ " + auxP.getNombre(j) +"|"+ auxP.getSaturacion(j) + "}\"style = filled fillcolor = " + auxP.getColor(j) + "];\n");
                }
            }
            grafo.Append("}");
            this.generarDot(rdot, rpng);

        }

        public void abrirGrafo()
        {
            //this.graficar(ListaContinentes, nombreGrafica);
            string rutaCompleta = ruta + "\\GRAFO_PROYECTO1\\imagen.png";
                if (File.Exists(rutaCompleta))
                {
                    try
                    {
                            System.Diagnostics.Process.Start(rutaCompleta);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Error1 " +e);
                    }
                }
                else
                {
                    Console.WriteLine("Inexistente");
                }
            //return rutaCompleta;
            direccionIMG = rutaCompleta;
        }

        public string getDireccion()
        {
            return direccionIMG;
        }
    }
}
