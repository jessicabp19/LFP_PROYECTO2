using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LFP_Proyecto1
{
    class Analizador
    {
        private LinkedList<Token> SalidaTokens; //LISTA DE TOKENS
        private LinkedList<Error> SalidaErrores; //LISTA DE ERRORES
        private int estado; //INDICA EN QUE ESTADO DE MI AFD ESTOY
        private String auxlex; //REPRESENTA EL LEXEMA QUE SE ESTA LEYEDO (PALABRA Y/O CARACTER)
        private int fila, columna;

        public Analizador()
        {
            SalidaTokens = new LinkedList<Token>();
            SalidaErrores = new LinkedList<Error>();
            fila = 1;
            columna = 0;
        }

        public LinkedList<Token> ListaTokens(String entrada)
        {
            #region inicializaciones
            entrada = entrada + "#"; //NOS INDICA CUANDO LLEGAMOS AL FINAL DE A CADENA
            estado = 0;
            auxlex = "";
            Char c;
            int llave1 = 0;
            #endregion

            for (int i = 0; i <= entrada.Length - 1; i++)
            {
                columna += 1;
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
                            Console.WriteLine("ENTRO A ISDIGIT " + auxlex);
                        }
                        else if (c.CompareTo('"') == 0)//FALTA EL CARACTER!
                        {
                            estado = 3; auxlex += c;
                        }
                        else if (c.CompareTo('\'') == 0)//FALTA EL CARACTER!
                        {
                            estado = 10; auxlex += c;
                        }
                        #region Comparaciones directas
                        else if (c.CompareTo('{') == 0)
                        {
                            Console.WriteLine("------------COMPARÓ {----------");
                            auxlex += c;
                            AgregarToken(Token.Tipo.LLAVE_IZQ);
                        }
                        else if (c.CompareTo('}') == 0)
                        {
                            auxlex += c;
                            AgregarToken(Token.Tipo.LLAVE_DER);
                        }
                        else if (c.CompareTo('[') == 0)
                        {
                            auxlex += c;
                            AgregarToken(Token.Tipo.CORCHETE_IZQ);
                        }
                        else if (c.CompareTo(']') == 0)
                        {
                            auxlex += c;
                            AgregarToken(Token.Tipo.CORCHETE_DER);
                        }
                        else if (c.CompareTo('(') == 0)
                        {
                            auxlex += c;
                            AgregarToken(Token.Tipo.PARENTESIS_IZQ);
                        }
                        else if (c.CompareTo(')') == 0)
                        {
                            auxlex += c; Console.WriteLine("------------)))))))))))))----------");
                            AgregarToken(Token.Tipo.PARENTESIS_DER);
                        }
                        else if (c.CompareTo('<') == 0)
                        {
                            auxlex += c;
                            estado = 11;//AgregarToken(Token.Tipo.SIGNO_MENORQUE);
                        }
                        else if (c.CompareTo('>') == 0)
                        {
                            auxlex += c;
                            estado = 12;//AgregarToken(Token.Tipo.SIGNO_MAYORQUE);
                        }
                        else if (c.CompareTo('=') == 0)
                        {
                            auxlex += c; estado = 9;
                            //AgregarToken(Token.Tipo.SIGNO_IGUAL);
                        }
                        else if (c.CompareTo('!') == 0)
                        {
                            auxlex += c;
                            estado = 8;//AgregarToken(Token.Tipo.SIGNO_NEGACION);
                        }
                        else if (c.CompareTo('/') == 0)
                        {
                            estado = 5;
                        }
                        else if (c.CompareTo('-') == 0)
                        {
                            auxlex += c;
                            AgregarToken(Token.Tipo.SIGNO_MENOS);
                        }
                        else if (c.CompareTo('+') == 0)
                        {
                            auxlex += c;
                            AgregarToken(Token.Tipo.SIGNO_MAS);
                        }
                        else if (c.CompareTo('.') == 0)
                        {
                            auxlex += c;
                            AgregarToken(Token.Tipo.SIGNO_PUNTO);
                        }
                        else if (c.CompareTo(',') == 0)
                        {
                            auxlex += c;
                            AgregarToken(Token.Tipo.SIGNO_COMA);
                        }
                        else if (c.CompareTo('*') == 0)
                        {
                            auxlex += c;
                            AgregarToken(Token.Tipo.SIGNO_POR);
                        }
                        else if (c.CompareTo(':') == 0)
                        {
                            auxlex += c;
                            AgregarToken(Token.Tipo.SIGNO_DOSPUNTOS);
                        }
                        else if (c.CompareTo(';') == 0)
                        {
                            auxlex += c;
                            AgregarToken(Token.Tipo.SIGNO_PUNTOYCOMA);
                        }
                        #endregion

                        else if (c.CompareTo(' ') == 0 )
                        {
                            auxlex = "";
                            estado = 0; 
                        }
                        else if (c.CompareTo('\n') == 0 || c.Equals("\r\n") || c.CompareTo('\r') == 0 || c.CompareTo('\t') == 0)
                        {
                            fila += 1;
                            columna = 0;Console.WriteLine("COLUMNA "+columna);
                            auxlex = "";
                            estado = 0;
                        }else{
                            if (c.CompareTo('#') == 0 && i == entrada.Length - 1)
                            {
                                Console.WriteLine("HEMOS CONCLUIDO EL ANALISIS");
                            }
                            else
                            {
                                Console.WriteLine("Error lexico con " + c);
                                AgregarError(Error.Tipo.CARACTER_DESCONOCIDO, c);
                            }
                        }
                        break;
                    #endregion

                    case 1:
                        #region PALABRAS RESERVADAS
                        if (Char.IsLetter(c) || Char.IsDigit(c) ||c.CompareTo('_')==0)
                        {
                            estado = 1;
                            auxlex += c;
                        }else{
                            columna--;
                            i -= 1;
                            if (auxlex.Equals("class"))
                            {
                                AgregarToken(Token.Tipo.PR_CLASS);
                            }
                            else if (auxlex.Equals("static"))
                            {
                                AgregarToken(Token.Tipo.PR_STATIC);
                            }
                            else if (auxlex.Equals("void"))
                            {
                                AgregarToken(Token.Tipo.PR_VOID);
                            }
                            else if (auxlex.ToUpper().Equals("STRING"))//PUEDE SER DE AMBAS MANERAS
                            {
                                AgregarToken(Token.Tipo.PR_STRING);
                            }
                            else if (auxlex.Equals("int"))
                            {
                                AgregarToken(Token.Tipo.PR_INT);
                            }
                            else if (auxlex.Equals("float"))
                            {
                                AgregarToken(Token.Tipo.PR_FLOAT);
                            }
                            else if (auxlex.Equals("char"))
                            {
                                AgregarToken(Token.Tipo.PR_CHAR);
                            }
                            else if (auxlex.Equals("bool"))
                            {
                                AgregarToken(Token.Tipo.PR_BOOL);
                            }
                            else if (auxlex.Equals("Main"))
                            {
                                AgregarToken(Token.Tipo.PR_MAIN);
                            }
                            //else if (auxlex.ToUpper().Equals("ARGS"))//Al final de cuentas puede venir lo que sea
                            //{
                            //    AgregarToken(Token.Tipo.PR_ARGS);
                            //}
                            else if (auxlex.Equals("true"))
                            {
                                AgregarToken(Token.Tipo.PR_TRUE);
                            }
                            else if (auxlex.Equals("false"))
                            {
                                AgregarToken(Token.Tipo.PR_FALSE);
                            }
                            else if (auxlex.Equals("new"))
                            {
                                AgregarToken(Token.Tipo.PR_NEW);
                            }
                            else if (auxlex.Equals("Console"))
                            {
                                AgregarToken(Token.Tipo.PR_CONSOLE);
                            }
                            else if (auxlex.Equals("WriteLine"))
                            {
                                AgregarToken(Token.Tipo.PR_WRITELINE);
                            }
                            else if (auxlex.Equals("if"))
                            {
                                AgregarToken(Token.Tipo.PR_IF);
                            }
                            else if (auxlex.Equals("else"))
                            {
                                AgregarToken(Token.Tipo.PR_ELSE);
                            }
                            else if (auxlex.Equals("switch"))
                            {
                                AgregarToken(Token.Tipo.PR_SWITCH);
                            }

                            else if (auxlex.Equals("case"))
                            {
                                AgregarToken(Token.Tipo.PR_CASE);
                            }
                            else if (auxlex.Equals("break"))
                            {
                                AgregarToken(Token.Tipo.PR_BREAK);
                            }
                            else if (auxlex.Equals("default"))
                            {
                                AgregarToken(Token.Tipo.PR_DEFAULT);
                            }
                            else if (auxlex.Equals("for"))
                            {
                                AgregarToken(Token.Tipo.PR_FOR);
                            }
                            else if (auxlex.Equals("while"))
                            {
                                AgregarToken(Token.Tipo.PR_WHILE);
                            }
                            else
                            {
                                //if (llave1==1)
                                //{
                                    AgregarToken(Token.Tipo.IDENTIFICADOR);
                                //}
                                //else { 
                                //Console.WriteLine("Error lexico con: " + auxlex);
                                //AgregarGranError(Error.Tipo.PALABRA_DESCONOCIDA);
                                //}
                            }
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
                        else if(c.CompareTo('.')==0)
                        {
                            estado = 4;
                            auxlex +=c;
                        }
                        else
                        {
                            --columna;
                            i = i-1;//ANTES HABIAN COMPARACIONES
                            AgregarToken(Token.Tipo.NUMERO_ENTERO);
                            Console.WriteLine("despues de agregar el tokencito");
                        }
                        break;
                    #endregion
                    case 3:
                        #region CADENAS
                        if (c.CompareTo('"') != 0)
                        {
                            estado = 3;
                            auxlex += c;
                        }else{
                            auxlex += c;
                            AgregarToken(Token.Tipo.CADENA);
                        }
                        break;
                    #endregion
                    case 4:
                        #region NUMEROS
                        if (Char.IsDigit(c))
                        {
                            estado = 4;
                            auxlex += c;
                        }
                        else
                        {
                            --columna;
                            i -= 1;//ANTES HABIAN COMPARACIONES
                            AgregarToken(Token.Tipo.NUMERO_REAL);
                        }
                        break;
                    #endregion
                    case 5:
                        #region Comentarios
                        if (c.CompareTo('/') == 0)
                        {
                            estado = 6;
                        }
                        else if (c.CompareTo('*') == 0)
                        {
                            estado = 7;
                        }
                        else
                        {
                            auxlex = "/";columna--;i = i-1;
                            AgregarToken(Token.Tipo.SIGNO_DIVIDIDO);
                        }
                        break;
                    #endregion
                    case 6:
                        #region Comentarios
                        if (c.CompareTo('\n') != 0)
                        {
                            auxlex += c;
                        }
                        else
                        {
                            columna = columna - auxlex.Length;
                            AgregarToken(Token.Tipo.COMENTARIO_LINEA);
                        }
                        break;
                    #endregion
                    case 7:
                        #region Comentarios
                        if (c.CompareTo('*') != 0)
                        {
                            auxlex += c;
                        }
                        else if (c.CompareTo('*') == 0 && entrada.ElementAt(i+1).CompareTo('/') == 0) 
                        {
                            i = i + 1;
                            columna = columna + 1 - auxlex.Length;
                            AgregarToken(Token.Tipo.COMENTARIO_MULTI);
                        }
                        break;
                        #endregion
                    case 8:
                        #region NEGACION
                        auxlex += c;
                        if (c.CompareTo('=')==0)
                        {
                            AgregarToken(Token.Tipo.SIGNO_NEGACION);
                        }
                        else
                        {
                            Console.WriteLine("Error lexico con: " + auxlex);
                            AgregarGranError(Error.Tipo.PALABRA_DESCONOCIDA);
                        }
                        break;
                        #endregion
                    case 9:
                        #region IGUALES
                        if (c.CompareTo('=') == 0)
                        {
                            auxlex += c;
                            AgregarToken(Token.Tipo.SIGNO_IGUALDAD);
                        }
                        else
                        {
                            columna--;
                            i--;
                            AgregarToken(Token.Tipo.SIGNO_IGUAL);
                        }
                        break;
                    #endregion
                    case 10:
                        #region CHAR
                        if (c.CompareTo('\'') != 0)
                        {
                            estado = 10;
                            auxlex += c;
                        }
                        else
                        {
                            auxlex += c;
                            AgregarToken(Token.Tipo.CARACTER);
                        }
                        break;
                    #endregion
                    case 11:
                        #region menorque
                        if (c.CompareTo('=') == 0)
                        {
                            auxlex += c;
                            AgregarToken(Token.Tipo.SIGNO_MENORIQ);
                        }
                        else
                        {
                            columna--;
                            i--;
                            AgregarToken(Token.Tipo.SIGNO_MENORQUE);
                        }
                        break;
                    #endregion
                    case 12:
                        #region menorque
                        if (c.CompareTo('=') == 0)
                        {
                            auxlex += c;
                            AgregarToken(Token.Tipo.SIGNO_MAYORIQ);
                        }
                        else
                        {
                            columna--;
                            i--;
                            AgregarToken(Token.Tipo.SIGNO_MAYORQUE);
                        }
                        break;
                        #endregion
                }
            }
            return SalidaTokens;
        }

        public LinkedList<Error> ListaErrores()
        {
            return SalidaErrores;
        }
        public void AgregarToken(Token.Tipo tipo)
        {
            SalidaTokens.AddLast(new Token(tipo, auxlex, fila, columna));
            auxlex = "";
            estado = 0;
        }

        public void AgregarError(Error.Tipo tipo, Char error)
        {
            SalidaErrores.AddLast(new Error(tipo, "" + error, fila, columna));
            auxlex = "";
            estado = 0;
        }

        public void AgregarGranError(Error.Tipo tipo)
        {
            SalidaErrores.AddLast(new Error(tipo, auxlex, fila, columna));
            auxlex = "";
            estado = 0;
        }

        public void Imprimir(LinkedList<Token> lista)
        {
            Console.WriteLine("--------------");
            foreach (Token item in lista)
            {
                Console.WriteLine(item.GetFila() + "\t<--->\t" + item.GetColumna() + "\t<--->\t" + item.GetTipoString() + "\t<--->\t" + item.GetValor());
            }
        }

        public void ImprimirE(LinkedList<Error> lista)
        {
            Console.WriteLine("--------------");
            foreach (Error item in lista)
            {
                Console.WriteLine(item.GetValor() + " <---> " + item.GetTipo());
            }
        }
    }
}
