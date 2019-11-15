using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace LFP_Proyecto1
{
    public partial class Form1 : Form
    {
        #region Variables Globales
        int contadorArchivos = 1;
        string nombreA_Entrada = "";
        string nombreA_Salida = "Sin Salida";
        string htmlPathTokens = "";
        string htmlPathSimbolos = "";
        string htmlPathErrores = "";
        Boolean hayErrores = true, hayMasErrores=true;

        //string pdfPath = "";
        //string imgLoadingPath = "";

        #endregion

        #region Cosas que no son de mi interés
        public Form1()
        {
            InitializeComponent();
        }

        private void abrirArchivoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog selector = new OpenFileDialog();
                selector.Filter = "Documento|*.cs"; selector.Title = "CARGANDO CONTENIDO";
                if (selector.ShowDialog() == DialogResult.OK)
                {
                    nombreA_Entrada = selector.FileName;
                    if (nombreA_Entrada.EndsWith(".cs")  || nombreA_Entrada.EndsWith(".CS"))
                    {
                        string nombre = nombreA_Entrada.Substring(nombreA_Entrada.LastIndexOf(@"\") + 1);
                        StreamReader lector = new StreamReader(nombreA_Entrada);
                        //fastColoredTextBox1.Text = lector.ReadToEnd();
                        richTextBox1.Text = lector.ReadToEnd();

                        lector.Close();
                    }
                    else
                    {
                        MessageBox.Show("ATENCIÓN", "Tipo de Extensión No Compatible", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Mensaje informativo", "Ha ocurrido un error, intente de nuevo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        
        private void guardarComoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog gselector = new SaveFileDialog();
                gselector.Filter = "Documento|*.cs"; gselector.Title = "GUARDANDO CONTENIDO";
                gselector.FileName = "Sin Titulo " + (contadorArchivos++) + ".cs";

                if (gselector.ShowDialog() == DialogResult.OK)
                {
                    nombreA_Entrada = gselector.FileName;
                    StreamWriter escritor = new StreamWriter(nombreA_Entrada);
                    foreach (object line in  richTextBox1.Lines) //fastColoredTextBox1.Lines)//
                    { escritor.WriteLine(line); }
                    escritor.Close();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Mensaje informativo", "Ha ocurrido un error, intente de nuevo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void acercaDeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DatosDesarrollador misDatos = new DatosDesarrollador();
            misDatos.ShowDialog();
        }

        private void salirToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        #endregion

        private void HtmlTokens(LinkedList<Token> miLista)
        {
            int correlativoTokens = 1;
            string fechaHora = DateTime.Now.ToString("G");
            int pos = nombreA_Entrada.LastIndexOf(@"\");
            try
            {
                StreamWriter escritor = new StreamWriter(htmlPathTokens);
                escritor.WriteLine("<!DOCTYPE html>\n<html>\n<head>\n<title>Salida Analizador Lexico</title>\n</head>\n<body bgcolor=\"#FAEBD7\"> <center>\n<h1> <center> -- SALIDA DEL ANALIZADOR LEXICO --</h1>\n");
                escritor.WriteLine("\n<h2> <center> FECHA Y HORA : "+fechaHora+"</h2>\n");
                escritor.WriteLine("<table border='1'>\n" +
                "<caption>TABLA DE TOKENS</caption>\n" +
                "<tr>\n" +
                "<th>Correlativo</th>\n<th>     #Fila    </th>\n<th>    #Columna    </th>\n<th>     Lexema      </th>\n<th>     Token       </th>\n" +
                "</tr>\n");
                foreach (Token item in miLista)
                {
                    escritor.WriteLine("<tr>\n<td>" + (correlativoTokens++) + "</td><td>" + item.GetFila() + "</td><td>" + item.GetColumna() + "</td><td>" + item.GetValor() + "</td><td>" + item.GetTipoString() + "</td>\n</tr>");
                }
                escritor.WriteLine("</table>\n<br>\n<br>\n");
                escritor.WriteLine("\n<h2> <center> RUTA ARCHIVO : " + htmlPathTokens + "</h2>\n");
                escritor.WriteLine("\n<h2> <center> Archivo Entrada : " + nombreA_Entrada.Substring(pos + 1) + "</h2>\n");
                escritor.WriteLine("\n<h2> <center> Archivo Salida : " + nombreA_Entrada.Substring(pos + 1) + "</h2>\n");
                escritor.WriteLine("</body>\n</html>");
                escritor.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Mensaje informativo", "Ha ocurrido un error, intente de nuevo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void HtmlErrores(LinkedList<Error> miListaE)
        {
            int correlativoErrores = 1; 
            string fechaHora = DateTime.Now.ToString("G");
            int pos = nombreA_Entrada.LastIndexOf(@"\");
            try
            {
                StreamWriter escritor = new StreamWriter(htmlPathErrores);

                escritor.WriteLine("<!DOCTYPE html>\n<html>\n<head>\n<title>Salidas Analizador Lexico</title>\n</head>\n<body bgcolor=\"#FAEBD7\"> <center>\n<h1> <center> -- SALIDAS DEL ANALIZADOR LEXICO --</h1>\n");
                escritor.WriteLine("\n<h2> <center> FECHA Y HORA : " + fechaHora + "</h2>\n");
                escritor.WriteLine("<table border='1'>\n" +
                "<caption>TABLA DE TOKENS</caption>\n" +
                "<tr>\n" +
                "<th>Correlativo</th>\n<th>     #Fila    </th>\n<th>    #Columna    </th>\n<th>     Objeto      </th>\n<th>     Descripción       </th>\n" +
                "</tr>\n");
                foreach (Error item in miListaE)
                {
                    escritor.WriteLine("<tr>\n<td>" + (correlativoErrores++) + "</td><td>" + item.GetFila() + "</td><td>" + item.GetColumna() + "</td><td>" + item.GetValor() + "</td><td>" + item.GetTipo() + "</td>\n</tr>");
                }
                escritor.WriteLine("</table>\n<br>\n<br>\n");
                escritor.WriteLine("\n<h2> <center> RUTA ARCHIVO : " + htmlPathErrores + "</h2>\n");
                escritor.WriteLine("\n<h2> <center> Archivo Entrada : " + nombreA_Entrada.Substring(pos + 1) + "</h2>\n");
                escritor.WriteLine("\n<h2> <center> Archivo Salida : " + nombreA_Entrada.Substring(pos + 1) + "</h2>\n");
                escritor.WriteLine("</body>\n</html>");
                escritor.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Mensaje informativo", "Ha ocurrido un error, intente de nuevo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
        }

        private void HtmlErrores2(LinkedList<Error> miListaE)
        {
            int correlativoErrores = 1;
            string fechaHora = DateTime.Now.ToString("G");
            int pos = nombreA_Entrada.LastIndexOf(@"\");
            try
            {
                StreamWriter escritor = new StreamWriter(htmlPathErrores);

                escritor.WriteLine("<!DOCTYPE html>\n<html>\n<head>\n<title>Salidas Analizador Lexico</title>\n</head>\n<body bgcolor=\"#FAEBD7\"> <center>\n<h1> <center> -- SALIDAS DEL ANALIZADOR SINTÁCTICO --</h1>\n");
                escritor.WriteLine("\n<h2> <center> FECHA Y HORA : " + fechaHora + "</h2>\n");
                escritor.WriteLine("<table border='1'>\n" +
                "<caption>TABLA DE TOKENS</caption>\n" +
                "<tr>\n" +
                "<th>Correlativo</th>\n<th>     #Fila    </th>\n<th>    #Columna    </th>\n<th>     Descripción       </th>\n" +
                "</tr>\n");
                foreach (Error item in miListaE)
                {
                    escritor.WriteLine("<tr>\n<td>" + (correlativoErrores++) + "</td><td>" + item.GetFila() + "</td><td>" + item.GetColumna() + "</td><td>" + item.getDescripcion() + "</td>\n</tr>");
                }
                escritor.WriteLine("</table>\n<br>\n<br>\n");
                escritor.WriteLine("\n<h2> <center> RUTA ARCHIVO : " + htmlPathErrores + "</h2>\n");
                escritor.WriteLine("\n<h2> <center> Archivo Entrada : " + nombreA_Entrada.Substring(pos + 1) + "</h2>\n");
                escritor.WriteLine("\n<h2> <center> Archivo Salida : " + nombreA_Entrada.Substring(pos + 1) + "</h2>\n");
                escritor.WriteLine("</body>\n</html>");
                escritor.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Mensaje informativo", "Ha ocurrido un error, intente de nuevo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
        }

        private void HtmlSimbolos(LinkedList<Simbolo> miListaS)
        {
            int correlativoErrores = 1;
            string fechaHora = DateTime.Now.ToString("G");
            int pos = nombreA_Entrada.LastIndexOf(@"\");
            try
            {
                StreamWriter escritor = new StreamWriter(htmlPathSimbolos);

                escritor.WriteLine("<!DOCTYPE html>\n<html>\n<head>\n<title>-----Salida-----</title>\n</head>\n<body bgcolor=\"#FAEBD7\"> <center>\n<h1> <center> -- SALIDAS DEL ANALIZADOR SINTÁCTICO --</h1>\n");
                escritor.WriteLine("\n<h2> <center> FECHA Y HORA : " + fechaHora + "</h2>\n");
                escritor.WriteLine("<table border='1'>\n" +
                "<caption>TABLA DE TOKENS</caption>\n" +
                "<tr>\n" +
                "<th>Correlativo</th>\n<th>     TIPO    </th>\n<th>    NOMBRE    </th>\n<th>     VALOR       </th>\n" +
                "</tr>\n");
                foreach (Simbolo item in miListaS)
                {
                    escritor.WriteLine("<tr>\n<td>" + (correlativoErrores++) + "</td><td>" + item.getTipo() + "</td><td>" + item.getNombreVar() + "</td><td>" + item.getValorVar() + "</td>\n</tr>");
                }
                escritor.WriteLine("</table>\n<br>\n<br>\n");
                escritor.WriteLine("\n<h2> <center> RUTA ARCHIVO : " + htmlPathSimbolos + "</h2>\n");
                escritor.WriteLine("\n<h2> <center> Archivo Entrada : " + nombreA_Entrada.Substring(pos + 1) + "</h2>\n");
                escritor.WriteLine("\n<h2> <center> Archivo Salida : " + nombreA_Entrada.Substring(pos + 1) + "</h2>\n");
                escritor.WriteLine("</body>\n</html>");
                escritor.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Mensaje informativo", "Ha ocurrido un error, intente de nuevo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
        }

        #region EN PAUSA
        

        private void button2_Click(object sender, EventArgs e)//    PDF
        {
            
        }
        #endregion


        private void generarTraducciónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                #region SiempreSeEjecutaEsteCodigo
                htmlPathTokens = Path.Combine(Application.StartupPath, "LFP_PaginaProyecto2.html");
                htmlPathErrores = Path.Combine(Application.StartupPath, "LFP_PaginaErroresP2.html");
                htmlPathSimbolos = Path.Combine(Application.StartupPath, "LFP_PaginaSimbolosP2.html");
                String entrada = richTextBox1.Text;//fastColoredTextBox1.Text;//richTextBox1.Text;
                Analizador lexico = new Analizador();
                LinkedList<Token> lTokens = lexico.ListaTokens(entrada);
                LinkedList<Error> lErrores = lexico.ListaErrores();
                lexico.Imprimir(lTokens);
                lexico.ImprimirE(lErrores);
                File.WriteAllText(htmlPathTokens, String.Empty);
                File.WriteAllText(htmlPathErrores, String.Empty);
                File.WriteAllText(htmlPathSimbolos, String.Empty);
                nombreA_Salida = "";
                #endregion

                if (lErrores.Count != 0 || entrada.CompareTo("") == 0)
                {
                    HtmlErrores(lErrores);
                    hayErrores = true;
                    MessageBox.Show("Corrija los ERRORES LÉXICOS existentes para poder proceder");
                    richTextBox2.Clear();richTextBox3.Clear();
                }
                else
                {
                    MessageBox.Show("Analisis Lexico Exitoso!");
                    hayErrores = false;
                    HtmlTokens(lTokens);
                    lTokens.AddLast(new Token(Token.Tipo.ULTIMO, "#", 0, 0));
                    AnalizadorSintactico parser = new AnalizadorSintactico();
                    parser.parsear(lTokens);
                    LinkedList<Error> SErrores = parser.getListaErrores();
                    HtmlErrores2(SErrores);
                    if (SErrores.Count != 0)
                    {
                        //HtmlErrores2(SErrores);
                        //hayMasErrores = true;
                        MessageBox.Show("Corrija lo ERRORES SINTACTICOS existentes para poder proceder");
                        richTextBox2.Clear(); richTextBox3.Clear();
                    }
                    else
                    {
                        Console.WriteLine("FIN!!!");
                        richTextBox2.Text = parser.getTraduccion();
                        LinkedList<Simbolo> LSimbolos =parser.getListaSimbolos();
                        HtmlSimbolos(LSimbolos);
                        foreach (Simbolo item in LSimbolos)
                        {
                            Console.WriteLine("YEI");
                        }
                        string nombre = nombreA_Entrada.Substring(0,nombreA_Entrada.LastIndexOf('.'));
                        nombreA_Salida = nombre+".py";
                        StreamWriter escritor = new StreamWriter(nombreA_Salida);
                        foreach (object line in richTextBox1.Lines)//fastColoredTextBox1.Lines)//
                        { escritor.WriteLine(line); }
                        escritor.Close();
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Revise la estructura de la entrada Porfavor!");
            }
        }

        #region REPORTES y Limpieza
        private void tablaDeTokensReconocidosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(htmlPathTokens);
        }

        private void tablaDeSímbolosOVariablesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(htmlPathSimbolos);
        }

        private void tablaDeErroresLéxicosYSintácticosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(htmlPathErrores);
        }

        private void limpiarDocsRecientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            limpiando(htmlPathTokens);
            limpiando(htmlPathSimbolos);
            limpiando(htmlPathErrores);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void lbPais_Click(object sender, EventArgs e)
        {

        }

        private void limpiando(string actualPath)
        {
            try
            {
                string fechaHora = DateTime.Now.ToString("G");
                StreamWriter escritor = new StreamWriter(actualPath);
                escritor.WriteLine("<!DOCTYPE html>\n<html>\n<head>\n<title>    SALIDA  </title>\n</head>\n<body bgcolor=\"#FAEBD7\"> <center>\n<h1> <center> -- SALIDA LIMPIA --</h1>\n");
                escritor.WriteLine("\n<h2> <center> FECHA Y HORA : " + fechaHora + "</h2>\n");
                escritor.WriteLine("<table border='1'>\n" +
                "<caption>TABLA</caption>\n");
                escritor.WriteLine("</table>\n<br>\n<br>\n");
                escritor.WriteLine("\n<h2> <center> RUTA ARCHIVO :  S/N </h2>\n");
                escritor.WriteLine("\n<h2> <center> Archivo Entrada : S/N </h2>\n");
                escritor.WriteLine("\n<h2> <center> Archivo Salida : S/N </h2>\n");
                escritor.WriteLine("</body>\n</html>");
                escritor.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Mensaje informativo", "Ha ocurrido un error, intente de nuevo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        #endregion
    }

}