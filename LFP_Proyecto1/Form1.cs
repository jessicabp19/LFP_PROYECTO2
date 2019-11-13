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
        Boolean hayErrores = true;

        string pdfPath = "";
        string imgLoadingPath = "";

        private int estado;
        private String auxlex;
        private string nombreGrafica="";
        private string selec = "";
        private string nombre = "";
        private int poblacion=0;
        private int satEscogida = 0;
        Pais miPais;
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
                    foreach (object line in richTextBox1.Lines)
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
            Console.WriteLine("SALIÓ ERR");
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
            Console.WriteLine("SALIÓ ERR");
        }

        #region EN PAUSA
        void ListaTokensPintados(String entrada, Continente ListaContinentes)
        {
            entrada = entrada + "#"; //NOS INDICA CUANDO LLEGAMOS AL FINAL DE A CADENA
            estado = 0;
            auxlex = "";
            Char c;
            for (int i = 0; i <= entrada.Length - 1; i++)
            {
                c = entrada.ElementAt(i);
                switch (estado)
                {
                    #region ESTADO 0
                    case 0:
                        if (Char.IsLetter(c))
                        {
                            estado = 1;
                            auxlex += c;
                        }
                        else if (Char.IsDigit(c))
                        {
                            estado = 2;
                            auxlex += c;
                        }
                        else if (c.CompareTo('"') == 0)
                        {
                            estado = 3;
                            auxlex += c;
                        }
                        #region Comparaciones directas
                        else if (c.CompareTo('{') == 0)
                        {
                            auxlex += c;
                            Pintar3(i, auxlex, 4);
                            estado = 0;
                            auxlex = "";
                        }
                        else if (c.CompareTo('}') == 0)
                        {

                            auxlex += c;
                            Pintar3(i, auxlex, 4);
                            estado = 0;
                            auxlex = "";
                        }
                        else if (c.CompareTo('%') == 0 || c.CompareTo(':') == 0)
                        {
                            auxlex += c;
                            estado = 0;
                            auxlex = "";
                        }
                        else if (c.CompareTo(';') == 0)
                        {
                            auxlex += c;
                            Pintar3(i, auxlex, 5);
                            estado = 0;
                            auxlex = "";
                        }
                        #endregion
                        else if (c.CompareTo(' ') == 0 || c.CompareTo('\t') == 0 || c.CompareTo('\n') == 0)
                        {
                            auxlex = "";
                            estado = 0;
                        }
                        break;
                    #endregion

                    case 1:
                        #region PALABRAS RESERVADAS
                        if (Char.IsLetter(c))
                        {
                            estado = 1;
                            auxlex += c;
                        }
                        else
                        {
                            i -= 1;
                            Pintar3(i - auxlex.Length + 1, auxlex, 1);//ESA i ME GENERA DUDA
                            estado = 0;
                            auxlex = "";
                        }
                        break;
                    #endregion
                    case 2:
                        #region DIGITOS
                        if (Char.IsDigit(c))
                        {
                            estado = 2;
                            auxlex += c;
                        }
                        else
                        {
                            i -= 1;
                            Pintar3(i - auxlex.Length, auxlex, 2);
                            estado = 0;
                            auxlex = "";
                        }
                        break;
                    #endregion
                    case 3:
                        #region CADENAS
                        if (c.CompareTo('"') != 0)
                        {
                            estado = 3;
                            auxlex += c;

                        }
                        else
                        {
                            auxlex += c;
                            Pintar3(i - auxlex.Length, auxlex, 3);
                            estado = 0;
                            auxlex = "";
                        }
                        break;
                        #endregion
                }
            }
        }

        void ListaTokensPintados2(String entrada, Continente ListaContinentes)
        {
            int posContinenteAct = 0;
        #region inicializaciones
        entrada = entrada + "#"; //NOS INDICA CUANDO LLEGAMOS AL FINAL DE A CADENA
            estado = 0;
            auxlex = "";
            Char c;
            string auxContinente = "";
            string auxPais = "";
            int auxPoblacion = 0;
            int auxSaturacion = 0;
            string auxBandera = "";
            int llave=0, opcion=0;
            Boolean paisCreandose=false;

            #endregion
            for (int i = 0; i <= entrada.Length - 1; i++)
            {
                c = entrada.ElementAt(i);
                switch (estado)
                {
                    #region ESTADO 0
                    case 0:
                        if (Char.IsLetter(c))
                        {
                            estado = 1;
                            auxlex += c;
                        }
                        else if (Char.IsDigit(c))
                        {
                            estado = 2;
                            auxlex += c;
                        }
                        else if (c.CompareTo('"') == 0)
                        {
                            estado = 3;
                        }
                        #region Comparaciones directas
                        else if (c.CompareTo('{') == 0)
                        {
                            auxlex += c;
                            estado = 0;
                            auxlex = "";
                        }
                        else if (c.CompareTo('}') == 0)
                        {
                            
                            auxlex += c;
                            if (llave==3 && paisCreandose==true)
                            {
                                miPais =ListaContinentes.getListaPaises(posContinenteAct-1);
                                miPais.SetPais(auxPais, auxPoblacion, auxSaturacion, auxBandera);
                            }
                            estado = 0;
                            auxlex = "";
                            paisCreandose = false;
                        }
                        else if (c.CompareTo('%') == 0 || c.CompareTo(':') == 0)
                        {
                            auxlex += c;
                            estado = 0;
                            auxlex = "";
                        }
                        else if (c.CompareTo(';') == 0)
                        {
                            auxlex += c;
                            estado = 0;
                            auxlex = "";
                        }
                        #endregion
                        else if (c.CompareTo(' ') == 0 || c.CompareTo('\t') == 0 || c.CompareTo('\n') == 0)
                        {
                            auxlex = "";
                            estado = 0;
                        }
                        break;
                    #endregion

                    case 1:
                        #region PALABRAS RESERVADAS
                        if (Char.IsLetter(c))
                        {
                            estado = 1;
                            auxlex += c;
                        }
                        else
                        {
                            i -= 1;
                            if (auxlex.ToUpper().Equals("GRAFICA"))
                            {
                                llave = 1;
                            }
                            else if (auxlex.ToUpper().Equals("NOMBRE"))
                            {
                                opcion=1;
                            }
                            else if (auxlex.ToUpper().Equals("CONTINENTE"))
                            {
                                llave = 2;
                            }
                            else if (auxlex.ToUpper().Equals("PAIS") || auxlex.ToUpper().Equals("PAÍS"))
                            {
                                llave = 3;paisCreandose = true;
                            }
                            else if (auxlex.ToUpper().Equals("POBLACION") || auxlex.ToUpper().Equals("POBLACIÓN"))
                            {
                                opcion = 2;
                            }
                            else if (auxlex.ToUpper().Equals("SATURACION") || auxlex.ToUpper().Equals("SATURACIÓN"))
                            {
                                opcion = 3;
                            }
                            else if (auxlex.ToUpper().Equals("BANDERA"))
                            {
                                opcion = 4;
                            }
                            else
                            {
                                Console.WriteLine("Error lexico con: " + auxlex);//SE SUPONE QUE YA NO HAY ERRORES
                            }
                            estado = 0;
                            auxlex = "";
                        }
                        break;
                    #endregion
                    case 2:
                        #region DIGITOS
                        if (Char.IsDigit(c))
                        {
                            estado = 2;
                            auxlex += c;
                        }
                        else
                        {
                            i -= 1;
                            Pintar3(i-auxlex.Length+1, auxlex, 2);
                            if (llave == 3 && opcion == 2)
                            {
                                auxPoblacion = Int32.Parse(auxlex);
                            }
                            else if (llave == 3 && opcion == 3)
                            {
                                auxSaturacion = Int32.Parse(auxlex);
                            }
                            estado = 0;
                            auxlex = "";
                        }
                        break;
                    #endregion
                    case 3:
                        #region CADENAS
                        if (c.CompareTo('"') != 0)
                        {
                            estado = 3;
                            auxlex += c;
                            
                        }
                        else
                        {
                            if (llave==1)
                            {
                                nombreGrafica = auxlex;
                            }else if (llave==2)
                            {
                                auxContinente = auxlex;
                                ListaContinentes.setContinente(auxContinente);//OJO!!!
                                posContinenteAct++;
                            }
                            else if (llave==3 && opcion==1)
                            {
                                auxPais = auxlex;
                            }
                            else if (llave == 3 && opcion == 4)
                            {
                                auxBandera = auxlex;
                            }
                            estado = 0;
                            auxlex = "";
                        }
                        break;
                        #endregion
                }
            }
        }

        private void Pintar3(int INDEX, string cadena, int tipo)
        {
        //Console.WriteLine("ENTRÓ, pos: " +INDEX+"/"+cadena+"/"+tipo);
        var control = tabControl1.SelectedTab.Controls[0];
        RichTextBox nuevo = (RichTextBox)control;
        nuevo.Find(cadena, INDEX, nuevo.TextLength, RichTextBoxFinds.WholeWord);
        Console.WriteLine("--> INICIO: " + INDEX);
            Console.WriteLine("--> CADENA: " + cadena + " Length: " + cadena.Length );
            if (tipo==1) {
                nuevo.SelectionColor = Color.Blue;
            }else if(tipo==2){
                nuevo.SelectionColor = Color.Green;
            }else if (tipo==3){
                nuevo.SelectionColor = Color.Yellow;
            }else if (tipo==4){
                nuevo.SelectionColor = Color.Red;
            }else if (tipo == 5){
                nuevo.SelectionColor = Color.Orange;
            }

            //INDEX = richTextBox1.Text.IndexOf(cadena, INDEX) + 1;
            nuevo.SelectionStart = nuevo.TextLength;//AANTES ESTABA R1
            nuevo.SelectionColor = Color.Black;
        }

        private void mostrarGrafico(string imgpath)
        {    
            try
            {
                pictureBox1.Load(imgpath);
                //pictureBox1.Image = Image.FromFile(imgpath);
            }
            catch (Exception)
            {
                MessageBox.Show("Inconvenientes con la ruta de la imagen");
                pictureBox1.Image = null;
            }
        }

        private void button2_Click(object sender, EventArgs e)//    PDF
        {
            try
            {
                if (hayErrores)
                {
                    MessageBox.Show("¡Corrija los errores existentes, Realice el Analisis y Vuelva a Intentar!");
                }
                else
                {
                    Document doc = new Document(iTextSharp.text.PageSize.LETTER, 10, 10, 22, 25);
                    iTextSharp.text.pdf.PdfWriter wri = iTextSharp.text.pdf.PdfWriter.GetInstance(doc, new FileStream("ResultadoAnalisis.pdf", FileMode.Create));
                    doc.Open();

                    #region PrimeraParte
                    iTextSharp.text.Font myFont = FontFactory.GetFont(iTextSharp.text.Font.FontFamily.HELVETICA.ToString(), 32, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.ORANGE);
                    Paragraph p = new Paragraph("RESULTADO DEL ANÁLISIS", myFont);
                    p.Alignment = Element.ALIGN_CENTER;
                    doc.Add(p);

                    iTextSharp.text.Image PNG = iTextSharp.text.Image.GetInstance(@"C:\Users\jessi\Desktop\GRAFO_PROYECTO1\imagen.png");
                    PNG.Alignment = Element.ALIGN_CENTER;
                    PNG.ScalePercent(120f);
                    doc.Add(PNG);
                    #endregion

                    #region SegundaParte
                    Paragraph espacio = new Paragraph("----------------------------------------------------", myFont);
                    espacio.Alignment = Element.ALIGN_CENTER;
                    doc.Add(espacio);
                    iTextSharp.text.Font myFont2 = FontFactory.GetFont(iTextSharp.text.Font.FontFamily.TIMES_ROMAN.ToString(), 20, iTextSharp.text.Font.ITALIC, iTextSharp.text.BaseColor.BLACK);
                    Paragraph p2 = new Paragraph("P A Í S     S E L E C C I O N A D O", myFont2);
                    p2.Alignment = Element.ALIGN_CENTER;
                    doc.Add(p2);

                    iTextSharp.text.Image PNG2 = iTextSharp.text.Image.GetInstance(selec);
                    PNG2.Alignment = Element.ALIGN_CENTER;
                    PNG2.ScalePercent(115f);
                    doc.Add(PNG2);

                    List list = new List(List.UNORDERED);
                    list.Add(new ListItem(" P A I S :     " + nombre + "\n\n"));
                    list.Add(" POBLACIÓN:     " + poblacion + "\n\n");
                    list.Add(" SATURACIÓN:     " + satEscogida + "\n");
                    list.IndentationLeft = 206f;
                    doc.Add(list);
                    #endregion

                    doc.Close();
                    //MessageBox.Show("---   PDF creado con Éxito   ---");

                    pdfPath = Path.Combine(Application.StartupPath, "ResultadoAnalisis.pdf");
                    Process.Start(pdfPath);
                }
                
            

            }
            catch
            {
                MessageBox.Show("-----  No se pudo crear el PDF  -----");
            }
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
                String entrada = richTextBox1.Text;
                Analizador lexico = new Analizador();
                LinkedList<Token> lTokens = lexico.ListaTokens(entrada);
                LinkedList<Error> lErrores = lexico.ListaErrores();
                lexico.Imprimir(lTokens);
                lexico.ImprimirE(lErrores);
                File.WriteAllText(htmlPathTokens, String.Empty);
                nombreA_Salida = "";
                #endregion

                if (lErrores.Count != 0 || entrada.CompareTo("") == 0)
                {
                    HtmlErrores(lErrores);
                    hayErrores = true;
                    MessageBox.Show("Corrija lo errores existentes para poder proceder");
                }
                else
                {
                    MessageBox.Show("Analisis Lexico Exitoso");
                    hayErrores = false;
                    //imgLoadingPath = Path.Combine(Application.StartupPath, "gifLoading.gif");
                    //mostrarGrafico(imgLoadingPath);
                    HtmlTokens(lTokens);
                    lTokens.AddLast(new Token(Token.Tipo.ULTIMO, "#", 0, 0));
                    AnalizadorSintactico parser = new AnalizadorSintactico();
                    parser.parsear(lTokens);
                    LinkedList<Error> SErrores = parser.getListaErrores();
                    HtmlErrores2(SErrores);
                    if (SErrores.Count != 0)
                    {
                        //HtmlErrores(lErrores);
                        //hayErrores = true;
                        MessageBox.Show("Corrija lo errores existentes para poder proceder");
                    }
                    else
                    {
                        Console.WriteLine("FIN!!!");
                        richTextBox2.Text = parser.getTraduccion();Console.WriteLine("TRADUCCION");
                        LinkedList<Simbolo> LSimbolos =parser.getListaSimbolos();
                        Console.WriteLine("TRAJO LA LISTA");
                        foreach (Simbolo item in LSimbolos)
                        {
                            Console.WriteLine("YEI");
                        }
                        Console.WriteLine("SALIÓ DEL FOR");
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