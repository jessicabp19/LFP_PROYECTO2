using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LFP_Proyecto1
{
    class AnalizadorSintactico
    {
        #region PREPARACION
        int controlToken;
        Token tokenActual;
        private LinkedList<Token> listaTok;//Lista de tokens recibida
        private LinkedList<Error> SalidaErrores;
        private LinkedList<Simbolo> ListaSimbolos;
        private LinkedList<String> ListaIDSAux;
        Simbolo.Tipo tipoActual = Simbolo.Tipo.VAR_STRING;
        //private Dictionary<string, string> myDictionary <string, string>();
        String cadenaTraducida = "";
        String cadenaAux = "";
        String valorVAR;
        String idActual = "";
        string varExp = "";
        int noAmbitos = 0;
        LinkedList<String> variableCase;
        int pos = 0;

        public AnalizadorSintactico()
        {
            SalidaErrores = new LinkedList<Error>();
            ListaSimbolos = new LinkedList<Simbolo>();
            ListaIDSAux = new LinkedList<String>();
            variableCase = new LinkedList<String>();
        }

        public void parsear(LinkedList<Token> tokens)
        {
            this.listaTok = tokens;
            controlToken = 0;
            tokenActual = listaTok.ElementAt(controlToken);
            //Llamada al no terminal inicial
            Iniciar();
        }
        #endregion

        #region INICIO, MAIN y CUERPO
        public void Iniciar()//<INICIO>-->class IDENTIFICADOR { <MAIN> }
        {
            emparejar(Token.Tipo.PR_CLASS);
            emparejar(Token.Tipo.IDENTIFICADOR);
            emparejar(Token.Tipo.LLAVE_IZQ);
            Main();
            emparejar(Token.Tipo.LLAVE_DER);
        }

        public void Main()//<MAIN>--> static void Main( <FIRST_PARAM> ) { < CUERPO > }
        {
            emparejar(Token.Tipo.PR_STATIC);
            emparejar(Token.Tipo.PR_VOID);
            emparejar(Token.Tipo.PR_MAIN);
            emparejar(Token.Tipo.PARENTESIS_IZQ);
            FIRST_PARAM();
            emparejar(Token.Tipo.PARENTESIS_DER);
            emparejar(Token.Tipo.LLAVE_IZQ);
            INSTRUCCION();
            emparejar(Token.Tipo.LLAVE_DER);
        }

        public void FIRST_PARAM()
        {
            if (tokenActual.GetTipo() == Token.Tipo.PR_STRING)
            {
                emparejar(Token.Tipo.PR_STRING);
                emparejar(Token.Tipo.CORCHETE_IZQ);
                emparejar(Token.Tipo.CORCHETE_DER);
                //emparejar(Token.Tipo.PR_ARGS);
                emparejar(Token.Tipo.IDENTIFICADOR);
            }
        }

        public void INSTRUCCION()//<INTRUCCION>--><SENTENCIAS> <INTRUCCION>
        {
            if (tokenActual.GetTipo() == Token.Tipo.PR_BOOL || tokenActual.GetTipo() == Token.Tipo.PR_INT ||
                tokenActual.GetTipo() == Token.Tipo.PR_STRING || tokenActual.GetTipo() == Token.Tipo.PR_FLOAT ||
                tokenActual.GetTipo() == Token.Tipo.PR_CHAR
                || tokenActual.GetTipo() == Token.Tipo.IDENTIFICADOR || tokenActual.GetTipo() == Token.Tipo.PR_CONSOLE
                || tokenActual.GetTipo() == Token.Tipo.PR_SWITCH || tokenActual.GetTipo() == Token.Tipo.PR_IF
                || tokenActual.GetTipo() == Token.Tipo.PR_FOR || tokenActual.GetTipo() == Token.Tipo.PR_WHILE
                || tokenActual.GetTipo() == Token.Tipo.COMENTARIO_LINEA || tokenActual.GetTipo() == Token.Tipo.COMENTARIO_MULTI)
            {
                Console.WriteLine("CUMPLIÓ CON SENTENCIA");
                SENTENCIAS();
                INSTRUCCION();
            }
        }
        public void SENTENCIAS()
        {
            if (tokenActual.GetTipo() == Token.Tipo.PR_BOOL || tokenActual.GetTipo() == Token.Tipo.PR_INT ||
                tokenActual.GetTipo() == Token.Tipo.PR_STRING || tokenActual.GetTipo() == Token.Tipo.PR_CHAR)
            {
                DECLARACION();
            }
            else if (tokenActual.GetTipo() == Token.Tipo.PR_FLOAT)
            {
                DECLARACION();
            }
            else if (tokenActual.GetTipo() == Token.Tipo.IDENTIFICADOR)
            {
                ASIGNACION();
            }
            else if (tokenActual.GetTipo() == Token.Tipo.PR_CONSOLE)
            {
                IMPRIMIR();
            }
            else if (tokenActual.GetTipo() == Token.Tipo.PR_IF)
            {
                Console.WriteLine("WAITING");
                SENTENCIA_IF();
            }
            else if (tokenActual.GetTipo() == Token.Tipo.PR_SWITCH)
            {
                SENTENCIA_SWITCH();
            }
            else if (tokenActual.GetTipo() == Token.Tipo.PR_FOR)
            {
                CICLO_FOR();
            }
            else if (tokenActual.GetTipo() == Token.Tipo.PR_WHILE)
            {
                CICLO_WHILE();
            }
            else if (tokenActual.GetTipo() == Token.Tipo.COMENTARIO_LINEA)
            {
                cadenaTraducida += "#" + tokenActual.GetValor() + "\n";
                emparejar(Token.Tipo.COMENTARIO_LINEA);
            }
            else if (tokenActual.GetTipo() == Token.Tipo.COMENTARIO_MULTI)
            {
                cadenaTraducida += "'''" + tokenActual.GetValor() + "'''\n";
                emparejar(Token.Tipo.COMENTARIO_MULTI);
            }
            else
            {
                Console.WriteLine("Error se esperaba el inicio de una Instrucción");
                //controlToken--;
                if (tokenActual.GetTipoE() != Token.Tipo.ULTIMO)
                {
                    controlToken += 1;
                    tokenActual = listaTok.ElementAt(controlToken);
                    String descripcionError = "Error se esperaba el inicio de una Instrucción";
                    SalidaErrores.AddLast(new Error(descripcionError, tokenActual.GetFila(), tokenActual.GetColumna()));
                }
            }
        }
        #endregion

        #region DECLARACION
        public void DECLARACION()//<DECLARACION> -->   <TIPO_DATO> <DECLARACIONPRIM> 
        {
            TIPO_DATO();
            DECLARACIONPRIM();
            emparejar(Token.Tipo.SIGNO_PUNTOYCOMA);
            cadenaTraducida += "\n";
        }

        public void TIPO_DATO()
        {
            if (tokenActual.GetTipo() == Token.Tipo.PR_INT)
            {
                emparejar(Token.Tipo.PR_INT);
                tipoActual = Simbolo.Tipo.VAR_INT;
            }
            else if (tokenActual.GetTipo() == Token.Tipo.PR_FLOAT)
            {
                emparejar(Token.Tipo.PR_FLOAT);
                tipoActual = Simbolo.Tipo.VAR_FLOAT;
            }
            else if (tokenActual.GetTipo() == Token.Tipo.PR_CHAR)
            {
                emparejar(Token.Tipo.PR_CHAR);
                tipoActual = Simbolo.Tipo.VAR_CHAR;
            }
            else if (tokenActual.GetTipo() == Token.Tipo.PR_STRING)
            {
                emparejar(Token.Tipo.PR_STRING);
                tipoActual = Simbolo.Tipo.VAR_STRING;
            }
            else if (tokenActual.GetTipo() == Token.Tipo.PR_BOOL)
            {
                emparejar(Token.Tipo.PR_BOOL);
                tipoActual = Simbolo.Tipo.VAR_BOOL;
            }
        }

        public void DECLARACIONF()
        {
            emparejar(Token.Tipo.PR_FLOAT);
            DECLARACIONPRIM();
            if (tokenActual.GetTipo() == Token.Tipo.IDENTIFICADOR && tokenActual.GetValor().Equals("f"))
            {
                emparejar(Token.Tipo.IDENTIFICADOR);
                //if (tokenActual.GetTipoE() == Token.Tipo.SIGNO_COMA)
                //{
                //    emparejar(Token.Tipo.SIGNO_COMA);
                //    DECLARACIONPRIM();
                //}
            }
            else
            {
                controlToken += 1;
                tokenActual = listaTok.ElementAt(controlToken);
                String descripcionError = "Error se esperaba letra 'f' luego del valor";
                SalidaErrores.AddLast(new Error(descripcionError, tokenActual.GetFila(), tokenActual.GetColumna()));
            }
            emparejar(Token.Tipo.SIGNO_PUNTOYCOMA);
            cadenaTraducida += "\n";
        }
        public void DECLARACIONPRIM()//<DECLARACIONPRIM> -->   IDENTIFICADOR <DEC_VAR> | [] IDENTIFICADOR <DEC_ARRAY> 
        {
            if (tokenActual.GetTipo() == Token.Tipo.IDENTIFICADOR)
            {
                cadenaAux = indentacion() + tokenActual.GetValor();
                idActual = tokenActual.GetValor();
                ListaIDSAux.AddLast(cadenaAux);
                emparejar(Token.Tipo.IDENTIFICADOR);
                DEC_VAR();
            }
            else if (tokenActual.GetTipo() == Token.Tipo.CORCHETE_IZQ)
            {
                emparejar(Token.Tipo.CORCHETE_IZQ);
                emparejar(Token.Tipo.CORCHETE_DER);
                cadenaAux = tokenActual.GetValor();
                ListaIDSAux.AddLast(cadenaAux); idActual = tokenActual.GetValor();
                emparejar(Token.Tipo.IDENTIFICADOR);
                DEC_ARRAY();
            }
            else
            {
                Console.WriteLine("Error se esperaba un Identificador o un [ ");
                if (tokenActual.GetTipoE() != Token.Tipo.ULTIMO)
                {
                    controlToken += 1;
                    tokenActual = listaTok.ElementAt(controlToken);
                    String descripcionError = "Error se esperaba un Identificador o un [ ";
                    SalidaErrores.AddLast(new Error(descripcionError, tokenActual.GetFila(), tokenActual.GetColumna()));
                }
            }
        }

        #region VAR
        public void DEC_VAR()//<DEC_VAR>--> , <DEC_VAR> | = <EXPRESION>  | epsilon
        {
            if (tokenActual.GetTipo() == Token.Tipo.SIGNO_COMA)
            {
                //cadenaAux = tokenActual.GetValor();
                cadenaAux = "";
                emparejar(Token.Tipo.SIGNO_COMA); cadenaTraducida += "\n" + cadenaAux;
                cadenaAux = tokenActual.GetValor();
                ListaIDSAux.AddLast(tokenActual.GetValor());
                emparejar(Token.Tipo.IDENTIFICADOR);
                DEC_VAR();
            }
            else if (tokenActual.GetTipo() == Token.Tipo.SIGNO_IGUAL)
            {
                cadenaTraducida += cadenaAux;//Paso autorizado
                cadenaTraducida += tokenActual.GetValor();
                emparejar(Token.Tipo.SIGNO_IGUAL);
                MEGAEXPRESION();
                if (tokenActual.GetTipo() == Token.Tipo.SIGNO_COMA)
                {
                    DEC_VAR();
                }
            }
        }

        #region MEGAEXPRESION
        public void MEGAEXPRESION()
        {
            EXPRESION();
            COMPARADOR();
        }
        public void COMPARADOR()
        {
            if (tokenActual.GetTipo() == Token.Tipo.SIGNO_MAYORQUE)
            {
                cadenaTraducida += tokenActual.GetValor();
                emparejar(Token.Tipo.SIGNO_MAYORQUE); MEGAEXPRESION();
            }
            else if (tokenActual.GetTipo() == Token.Tipo.SIGNO_MENORQUE)
            {
                cadenaTraducida += tokenActual.GetValor();
                emparejar(Token.Tipo.SIGNO_MENORQUE); EXPRESION();
            }
            if (tokenActual.GetTipo() == Token.Tipo.SIGNO_MAYORIQ)
            {
                cadenaTraducida += tokenActual.GetValor();
                emparejar(Token.Tipo.SIGNO_MAYORIQ); MEGAEXPRESION();
            }
            else if (tokenActual.GetTipo() == Token.Tipo.SIGNO_MENORIQ)
            {
                cadenaTraducida += tokenActual.GetValor();
                emparejar(Token.Tipo.SIGNO_MENORIQ); EXPRESION();
            }
            else if (tokenActual.GetTipo() == Token.Tipo.SIGNO_NEGACION)
            {
                cadenaTraducida += tokenActual.GetValor();
                emparejar(Token.Tipo.SIGNO_NEGACION); EXPRESION();
            }
            else if (tokenActual.GetTipo() == Token.Tipo.SIGNO_IGUALDAD)
            {
                cadenaTraducida += tokenActual.GetValor();
                emparejar(Token.Tipo.SIGNO_IGUALDAD); EXPRESION();
            }
            else if (tokenActual.GetTipo() == Token.Tipo.PR_TRUE)
            {
                cadenaTraducida += tokenActual.GetValor();
                emparejar(Token.Tipo.PR_TRUE); //EXPRESION();
            }
            else if (tokenActual.GetTipo() == Token.Tipo.PR_FALSE)
            {
                cadenaTraducida += tokenActual.GetValor();
                emparejar(Token.Tipo.PR_FALSE); //EXPRESION();
            }

        }
        public void EXPRESION()
        {
            TERMINO();
            EXPRESIONPRIM();
        }
        public void TERMINO()
        {
            FACTOR();
            TERMINOPRIM();
        }
        public void FACTOR()
        {
            if (tokenActual.GetTipo() == Token.Tipo.PARENTESIS_IZQ)
            {
                cadenaAux = tokenActual.GetValor();
                cadenaTraducida += cadenaAux;
                valorVAR = cadenaAux;
                emparejar(Token.Tipo.PARENTESIS_IZQ);
                MEGAEXPRESION();
                cadenaTraducida += tokenActual.GetValor();
                emparejar(Token.Tipo.PARENTESIS_DER);
            }
            else if (tokenActual.GetTipo() == Token.Tipo.NUMERO_ENTERO)
            {
                cadenaAux = tokenActual.GetValor();
                cadenaTraducida += cadenaAux;
                valorVAR = cadenaAux;
                asignarValor();
                emparejar(Token.Tipo.NUMERO_ENTERO);
            }
            else if (tokenActual.GetTipo() == Token.Tipo.NUMERO_REAL)
            {
                cadenaAux = tokenActual.GetValor();
                cadenaTraducida += cadenaAux;
                valorVAR = cadenaAux;
                asignarValor();
                emparejar(Token.Tipo.NUMERO_REAL);
            }
            else if (tokenActual.GetTipo() == Token.Tipo.IDENTIFICADOR)
            {
                cadenaAux = tokenActual.GetValor();
                cadenaTraducida += cadenaAux;
                valorVAR = cadenaAux;
                asignarValor();
                emparejar(Token.Tipo.IDENTIFICADOR);
            }
            else if (tokenActual.GetTipo() == Token.Tipo.CARACTER)
            {
                cadenaAux = tokenActual.GetValor();
                cadenaTraducida += cadenaAux;
                valorVAR = cadenaAux;
                asignarValor();
                emparejar(Token.Tipo.CARACTER);
            }

            else if (tokenActual.GetTipo() == Token.Tipo.CADENA)
            {
                cadenaAux = tokenActual.GetValor();
                cadenaTraducida += cadenaAux;
                valorVAR = cadenaAux;
                asignarValor();
                emparejar(Token.Tipo.CADENA);
            }
            else if (tokenActual.GetTipo() == Token.Tipo.PR_TRUE)
            {
                cadenaAux = tokenActual.GetValor();
                cadenaTraducida += cadenaAux;
                valorVAR = cadenaAux;
                asignarValor();
                emparejar(Token.Tipo.PR_TRUE);
            }
            else if (tokenActual.GetTipo() == Token.Tipo.PR_FALSE)
            {
                cadenaAux = tokenActual.GetValor();
                cadenaTraducida += cadenaAux;
                valorVAR = cadenaAux;
                asignarValor();
                emparejar(Token.Tipo.PR_FALSE);
            }
            else
            {
                Console.WriteLine("Error se esperaba una expresión" + tokenActual.GetValor() + listaTok.ElementAt(controlToken - 1).GetValor());
                if (tokenActual.GetTipoE() != Token.Tipo.ULTIMO)
                {
                    controlToken += 1;
                    tokenActual = listaTok.ElementAt(controlToken);
                    String descripcionError = "Error se esperaba una expresión";
                    SalidaErrores.AddLast(new Error(descripcionError, tokenActual.GetFila(), tokenActual.GetColumna()));
                }
            }
        }
        public void TERMINOPRIM()
        {
            if (tokenActual.GetTipo() == Token.Tipo.SIGNO_POR)
            {
                cadenaTraducida += tokenActual.GetValor();
                emparejar(Token.Tipo.SIGNO_POR);
                FACTOR();
                TERMINOPRIM();
            }
            else if (tokenActual.GetTipo() == Token.Tipo.SIGNO_DIVIDIDO)
            {
                cadenaTraducida += tokenActual.GetValor();
                emparejar(Token.Tipo.SIGNO_DIVIDIDO);
                FACTOR();
                TERMINOPRIM();
            }
        }
        public void EXPRESIONPRIM()
        {
            if (tokenActual.GetTipo() == Token.Tipo.SIGNO_MAS)
            {
                cadenaTraducida += tokenActual.GetValor();
                emparejar(Token.Tipo.SIGNO_MAS);
                TERMINO();
                EXPRESIONPRIM();
            }
            else if (tokenActual.GetTipo() == Token.Tipo.SIGNO_MENOS)
            {
                cadenaTraducida += tokenActual.GetValor();
                emparejar(Token.Tipo.SIGNO_MENOS);
                TERMINO();
                EXPRESIONPRIM();
            }
        }
        #endregion

        #endregion

        public void DEC_ARRAY()
        {
            if (tokenActual.GetTipo() == Token.Tipo.SIGNO_COMA)
            {
                cadenaAux = tokenActual.GetValor();
                emparejar(Token.Tipo.SIGNO_COMA);
                cadenaAux += tokenActual.GetValor();
                ListaIDSAux.AddLast(tokenActual.GetValor());
                emparejar(Token.Tipo.IDENTIFICADOR);
                DEC_ARRAY();
            }
            else if (tokenActual.GetTipo() == Token.Tipo.SIGNO_IGUAL)
            {
                cadenaTraducida += cadenaAux;//Paso autorizado
                cadenaTraducida += tokenActual.GetValor();
                emparejar(Token.Tipo.SIGNO_IGUAL);
                DEC_ARRAY_PRIM();
            }
        }

        public void DEC_ARRAY_PRIM()
        {
            if (tokenActual.GetTipo() == Token.Tipo.LLAVE_IZQ)
            {
                cadenaAux = indentacion() + tokenActual.GetValor();
                //ListaIDSAux.AddLast(cadenaAux);
                emparejar(Token.Tipo.LLAVE_IZQ);cadenaTraducida +="[";
                TIPOACEPTADOCASE();
                LISTAITEMS(); cadenaTraducida += "]";
                emparejar(Token.Tipo.LLAVE_DER);
            }
            else if (tokenActual.GetTipo() == Token.Tipo.PR_NEW)
            {
                emparejar(Token.Tipo.PR_NEW);
                TIPO_DATO();
                emparejar(Token.Tipo.CORCHETE_IZQ);
                emparejar(Token.Tipo.CORCHETE_DER);
                ////cadenaAux = tokenActual.GetValor();
                ///ListaIDSAux.AddLast(cadenaAux);
                //emparejar(Token.Tipo.IDENTIFICADOR);
                //DEC_ARRAY();
                //cadenaTraducida += cadenaAux;
            }
            else
            {
                Console.WriteLine("Error se esperaba un { o la palabra reservada 'new' ");
                if (tokenActual.GetTipoE() != Token.Tipo.ULTIMO)
                {
                    controlToken += 1;
                    tokenActual = listaTok.ElementAt(controlToken);
                    String descripcionError = "Error se esperaba un { o la palabra reservada 'new' ";
                    SalidaErrores.AddLast(new Error(descripcionError, tokenActual.GetFila(), tokenActual.GetColumna()));
                }
            }


        }

        #endregion

        public void LISTAITEMS()
        {
            if (tokenActual.GetTipo() == Token.Tipo.SIGNO_COMA)
            {
                cadenaTraducida += tokenActual.GetValor();
                emparejar(Token.Tipo.SIGNO_COMA);
                //cadenaAux += tokenActual.GetValor();
                ListaIDSAux.AddLast(tokenActual.GetValor());
                //emparejar(Token.Tipo.IDENTIFICADOR);
                TIPOACEPTADOCASE();
                LISTAITEMS();
            }
        }
        #region ASIGNACION
        public void ASIGNACION()
        {
            if (tokenActual.GetTipo() == Token.Tipo.IDENTIFICADOR)
            {
                cadenaAux = indentacion() + tokenActual.GetValor();
                ListaIDSAux.AddLast(cadenaAux); idActual = tokenActual.GetValor();
                emparejar(Token.Tipo.IDENTIFICADOR);
                ASIG_VAR();
                emparejar(Token.Tipo.SIGNO_PUNTOYCOMA);
                cadenaTraducida += "\n";
            }
        }

        public void ASIG_VAR()
        {
            cadenaTraducida += cadenaAux;//ACÁ YA SÉ QUE LO VA IGUALAR A ALGO//indentacion()+
            cadenaAux = tokenActual.GetValor(); cadenaTraducida += cadenaAux;//GUARDO EL IGUAL Y LO AÑADO DE UNA VEZ
            emparejar(Token.Tipo.SIGNO_IGUAL);
            MEGAEXPRESION();
            //cadenaTraducida += cadenaAux;
        }
        #endregion

        //FALTA QUE LO ACEPTE VACIO
        #region IMPRIMIR
        public void IMPRIMIR()
        {
            emparejar(Token.Tipo.PR_CONSOLE);
            emparejar(Token.Tipo.SIGNO_PUNTO);
            emparejar(Token.Tipo.PR_WRITELINE);
            emparejar(Token.Tipo.PARENTESIS_IZQ); cadenaTraducida += indentacion() + "print(";
            CONTENIDOIMPRESION(); cadenaTraducida += ")\n";
            emparejar(Token.Tipo.PARENTESIS_DER);
            emparejar(Token.Tipo.SIGNO_PUNTOYCOMA);
        }

        public void CONTENIDOIMPRESION()
        {
            if (listaTok.ElementAt(controlToken).GetTipo() != Token.Tipo.PARENTESIS_DER) {
                EXPRESION();
                PLUS();
            }
        }

        public void PLUS()
        {
            if (tokenActual.GetTipo() == Token.Tipo.SIGNO_MAS)
            {
                emparejar(Token.Tipo.SIGNO_MAS);
                EXPRESION();
                PLUS();
            }
        }
        #endregion

        #region SENTENCIA IF
        public void SENTENCIA_IF()
        {
            cadenaTraducida += indentacion() + tokenActual.GetValor() + "  ";
            noAmbitos++;
            emparejar(Token.Tipo.PR_IF);
            emparejar(Token.Tipo.PARENTESIS_IZQ);
            MEGAEXPRESION();
            emparejar(Token.Tipo.PARENTESIS_DER);posibleComentario();
            emparejar(Token.Tipo.LLAVE_IZQ);
            cadenaTraducida += "\n";
            INSTRUCCION();
            emparejar(Token.Tipo.LLAVE_DER); noAmbitos--;
            SENTENCIA_ELSE();
        }

        public void posibleComentario(){
            if (tokenActual.GetTipo() == Token.Tipo.COMENTARIO_LINEA)
            {
                cadenaTraducida += "#" + tokenActual.GetValor() + "\n";
                emparejar(Token.Tipo.COMENTARIO_LINEA);
            }
            else if (tokenActual.GetTipo() == Token.Tipo.COMENTARIO_MULTI)
            {
                cadenaTraducida += "'''" + tokenActual.GetValor() + "'''\n";
                emparejar(Token.Tipo.COMENTARIO_MULTI);
            }
        }

        public void SENTENCIA_ELSE()
        {
            if (tokenActual.GetTipo()==Token.Tipo.PR_ELSE)
            {
                cadenaTraducida += indentacion() + tokenActual.GetValor()+":\n";noAmbitos++;
                emparejar(Token.Tipo.PR_ELSE); posibleComentario();
                emparejar(Token.Tipo.LLAVE_IZQ);
                INSTRUCCION();
                emparejar(Token.Tipo.LLAVE_DER);noAmbitos--;cadenaTraducida += "\n";
            }
        }
        #endregion

        #region SENTENCIAS Y CICLOS PENDIENTES

        public void SENTENCIA_SWITCH()
        {
            pos++;
            emparejar(Token.Tipo.PR_SWITCH);
            noAmbitos++;
            emparejar(Token.Tipo.PARENTESIS_IZQ); variableCase.AddLast(tokenActual.GetValor()); 
            emparejar(Token.Tipo.IDENTIFICADOR); //MEGAEXPRESION
            emparejar(Token.Tipo.PARENTESIS_DER);
            emparejar(Token.Tipo.LLAVE_IZQ);
            CASES();
            emparejar(Token.Tipo.LLAVE_DER);pos--;noAmbitos--;
        }

        public void CASES()
        {
            if (tokenActual.GetTipo()==Token.Tipo.PR_CASE)
            {
                CASE();
                LISTACASES();
                CASES();

            }
            else
            {
                CASEDEFAULT();
            }
        }

        public void CASE()
        {
            emparejar(Token.Tipo.PR_CASE);cadenaTraducida += indentacion(1)+"if " +variableCase.ElementAt(pos-1) +" == ";
            TIPOACEPTADOCASE();//MEGAEXPRESION();
            cadenaTraducida += " :\n";
            emparejar(Token.Tipo.SIGNO_DOSPUNTOS);
            INSTRUCCION();
            emparejar(Token.Tipo.PR_BREAK);
            emparejar(Token.Tipo.SIGNO_PUNTOYCOMA);
        }

        public void LISTACASES()
        {
            if (tokenActual.GetTipo() == Token.Tipo.PR_CASE)
            {
                MICASE();
                LISTACASES();
                //CASES();

            }
        }

        public void MICASE()
        {
            emparejar(Token.Tipo.PR_CASE); cadenaTraducida += indentacion(1) + "elif " + variableCase.ElementAt(pos - 1) + " == ";
            TIPOACEPTADOCASE();//MEGAEXPRESION();
            cadenaTraducida += " :\n";
            emparejar(Token.Tipo.SIGNO_DOSPUNTOS);
            INSTRUCCION();
            emparejar(Token.Tipo.PR_BREAK);
            emparejar(Token.Tipo.SIGNO_PUNTOYCOMA);
        }

        public void CASEDEFAULT()
        {
            if (tokenActual.GetTipo()==Token.Tipo.PR_DEFAULT)
            {
                emparejar(Token.Tipo.PR_DEFAULT);
                emparejar(Token.Tipo.SIGNO_DOSPUNTOS); cadenaTraducida += indentacion(1) + "else  :\n";
                INSTRUCCION();
                emparejar(Token.Tipo.PR_BREAK);
                emparejar(Token.Tipo.SIGNO_PUNTOYCOMA);
            }
            
        }

        public void TIPOACEPTADOCASE()
        {
            if (tokenActual.GetTipo() == Token.Tipo.NUMERO_ENTERO)
            {
                cadenaAux = tokenActual.GetValor();
                cadenaTraducida += cadenaAux;
                valorVAR = cadenaAux;
                asignarValor();
                emparejar(Token.Tipo.NUMERO_ENTERO);
            }
            else if (tokenActual.GetTipo() == Token.Tipo.NUMERO_REAL)
            {
                cadenaAux = tokenActual.GetValor();
                cadenaTraducida += cadenaAux;
                valorVAR = cadenaAux;
                asignarValor();
                emparejar(Token.Tipo.NUMERO_REAL);
            }
            else if (tokenActual.GetTipo() == Token.Tipo.IDENTIFICADOR)
            {
                cadenaAux = tokenActual.GetValor();
                cadenaTraducida += cadenaAux;
                valorVAR = cadenaAux;
                asignarValor();
                emparejar(Token.Tipo.IDENTIFICADOR);
            }
            else if (tokenActual.GetTipo() == Token.Tipo.CARACTER)
            {
                cadenaAux = tokenActual.GetValor();
                cadenaTraducida += cadenaAux;
                valorVAR = cadenaAux;
                asignarValor();
                emparejar(Token.Tipo.CARACTER);
            }

            else if (tokenActual.GetTipo() == Token.Tipo.CADENA)
            {
                cadenaAux = tokenActual.GetValor();
                cadenaTraducida += cadenaAux;
                valorVAR = cadenaAux;
                asignarValor();
                emparejar(Token.Tipo.CADENA);
            }
            else if (tokenActual.GetTipo() == Token.Tipo.PR_TRUE)
            {
                cadenaAux = tokenActual.GetValor();
                cadenaTraducida += cadenaAux;
                valorVAR = cadenaAux;
                asignarValor();
                emparejar(Token.Tipo.PR_TRUE);
            }
            else if (tokenActual.GetTipo() == Token.Tipo.PR_FALSE)
            {
                cadenaAux = tokenActual.GetValor();
                cadenaTraducida += cadenaAux;
                valorVAR = cadenaAux;
                asignarValor();
                emparejar(Token.Tipo.PR_FALSE);
            }
            else
            {
                Console.WriteLine("Error se esperaba una expresión" + tokenActual.GetValor() + listaTok.ElementAt(controlToken - 1).GetValor());
                if (tokenActual.GetTipoE() != Token.Tipo.ULTIMO)
                {
                    controlToken += 1;
                    tokenActual = listaTok.ElementAt(controlToken);
                    String descripcionError = "Error se esperaba una expresión";
                    SalidaErrores.AddLast(new Error(descripcionError, tokenActual.GetFila(), tokenActual.GetColumna()));
                }
            }
        }
        #endregion

        #region FOR y WHILE
        public void CICLO_FOR()
        {
            //cadenaTraducida += indentacion() + tokenActual.GetValor();
            emparejar(Token.Tipo.PR_FOR);
            //noAmbitos++;
            emparejar(Token.Tipo.PARENTESIS_IZQ);
            INICIALIZADOR();
            CONDICION();
            emparejar(Token.Tipo.SIGNO_PUNTOYCOMA);
            INCREMENTO();
            emparejar(Token.Tipo.PARENTESIS_DER);
            emparejar(Token.Tipo.LLAVE_IZQ);
            cadenaTraducida += "\n"+indentacion() + "for "+idActual+" in \n";noAmbitos++;
            INSTRUCCION();
            emparejar(Token.Tipo.LLAVE_DER);noAmbitos--;
        }

        public void INICIALIZADOR()
        {
            if (tokenActual.GetTipo() == Token.Tipo.PR_BOOL || tokenActual.GetTipo() == Token.Tipo.PR_INT ||
                tokenActual.GetTipo() == Token.Tipo.PR_STRING || tokenActual.GetTipo() == Token.Tipo.PR_FLOAT ||
                tokenActual.GetTipo() == Token.Tipo.PR_CHAR)
            {
                DECLARACION();
            }
            else if (tokenActual.GetTipo() == Token.Tipo.IDENTIFICADOR)
            {
                ASIGNACION();
            }
            else
            {
                Console.WriteLine("Error se esperaba el inicio de una Instrucción");
                //controlToken--;
                if (tokenActual.GetTipoE() != Token.Tipo.ULTIMO)
                {
                    controlToken += 1;
                    tokenActual = listaTok.ElementAt(controlToken);
                    String descripcionError = "Error se esperaba el inicio de una Instrucción";
                    SalidaErrores.AddLast(new Error(descripcionError, tokenActual.GetFila(), tokenActual.GetColumna()));
                }
            }
        }

        public void CONDICION()
        {
            MEGAEXPRESION();
        }
        public void INCREMENTO()
        {
            if (tokenActual.GetTipo() == Token.Tipo.IDENTIFICADOR) {
                emparejar(Token.Tipo.IDENTIFICADOR);
                if (tokenActual.GetTipo() == Token.Tipo.SIGNO_MAS)
                {
                    emparejar(Token.Tipo.SIGNO_MAS);
                    emparejar(Token.Tipo.SIGNO_MAS);
                }
                else if (tokenActual.GetTipo() == Token.Tipo.SIGNO_MENOS)
                {
                    emparejar(Token.Tipo.SIGNO_MENOS);
                    emparejar(Token.Tipo.SIGNO_MENOS);
                }
            }
            else
            {
                if (tokenActual.GetTipo() == Token.Tipo.SIGNO_MAS)
                {
                    emparejar(Token.Tipo.SIGNO_MAS);
                    emparejar(Token.Tipo.SIGNO_MAS);
                }
                else if (tokenActual.GetTipo() == Token.Tipo.SIGNO_MENOS)
                {
                    emparejar(Token.Tipo.SIGNO_MENOS);
                    emparejar(Token.Tipo.SIGNO_MENOS);
                }
                emparejar(Token.Tipo.IDENTIFICADOR);
            }

        }
        public void CICLO_WHILE()
        {
            cadenaTraducida += indentacion() + tokenActual.GetValor()+"  "; noAmbitos++;
            emparejar(Token.Tipo.PR_WHILE);
            emparejar(Token.Tipo.PARENTESIS_IZQ);
            MEGAEXPRESION(); cadenaTraducida += " :\n";
             emparejar(Token.Tipo.PARENTESIS_DER);
            emparejar(Token.Tipo.LLAVE_IZQ);
            INSTRUCCION();
            emparejar(Token.Tipo.LLAVE_DER);noAmbitos--;cadenaTraducida += "\n";
        }
        #endregion

        #region Métodos Aux
        public void emparejar(Token.Tipo tip)
        {
            if (tokenActual.GetTipoE() != tip)
            {
                Console.WriteLine("Error se esperaba " + getTipoParaError(tip) + tokenActual.GetValor()+ listaTok.ElementAt(controlToken -1).GetValor());
                String descripcionError = "Error se esperaba " + getTipoParaError(tip);
                SalidaErrores.AddLast(new Error(descripcionError, tokenActual.GetFila(), tokenActual.GetColumna()));
            }
            
            if (tokenActual.GetTipoE() != Token.Tipo.ULTIMO)
            {
                controlToken += 1;
                tokenActual = listaTok.ElementAt(controlToken);
            }
        }

        public void asignarValor()
        {
            foreach (string item in ListaIDSAux)
            {
                ListaSimbolos.AddLast(new Simbolo(tipoActual, item, valorVAR));
            }
            ListaIDSAux.Clear();
            valorVAR = "";
        }

        public string indentacion()
        {
            string a = "";
            for (int i = 0; i < noAmbitos; i++)
            {
                a += "    ";
            }
            return a;

        }

        public string indentacion(int reduccion)
        {
            string a = "";
            for (int i = 0; i < noAmbitos-reduccion; i++)
            {
                a += "    ";
            }
            return a;

        }

        public String getTipoParaError(Token.Tipo tip)
        {
            switch (tip)
            {
                case Token.Tipo.PR_CLASS:
                    return "Palabra Reservada 'Class'";
                case Token.Tipo.PR_STATIC:
                    return "Palabra Reservada 'Static'";
                case Token.Tipo.PR_VOID:
                    return "Palabra Reservada 'Void'";
                case Token.Tipo.PR_STRING:
                    return "Palabra Reservada 'String'";
                case Token.Tipo.PR_FLOAT:
                    return "Palabra Reservada 'Float'";
                case Token.Tipo.PR_INT:
                    return "Palabra Reservada 'Int'";
                case Token.Tipo.PR_CHAR:
                    return "Palabra Reservada 'Char'";
                case Token.Tipo.PR_BOOL:
                    return "Palabra Reservada 'Bool'";

                case Token.Tipo.PR_MAIN:
                    return "Palabra Reservada 'Main'";
                case Token.Tipo.PR_ARGS:
                    return "Palabra Reservada 'Args'";
                case Token.Tipo.PR_TRUE:
                    return "Palabra Reservada 'True'";
                case Token.Tipo.PR_FALSE:
                    return "Palabra Reservada 'False'";
                case Token.Tipo.PR_NEW:
                    return "Palabra Reservada 'New'";
                case Token.Tipo.PR_CONSOLE:
                    return "Palabra Reservada 'Console'";
                case Token.Tipo.PR_WRITELINE:
                    return "Palabra Reservada 'WriteLine'";

                case Token.Tipo.PR_IF:
                    return "Palabra Reservada 'If'";
                case Token.Tipo.PR_ELSE:
                    return "Palabra Reservada 'Else'";
                case Token.Tipo.PR_SWITCH:
                    return "Palabra Reservada 'Switch'";
                case Token.Tipo.PR_CASE:
                    return "Palabra Reservada 'Case'";
                case Token.Tipo.PR_BREAK:
                    return "Palabra Reservada 'Break'";
                case Token.Tipo.PR_DEFAULT:
                    return "Palabra Reservada 'Default'";


                case Token.Tipo.PR_FOR:
                    return "Palabra Reservada 'For'";
                case Token.Tipo.PR_WHILE:
                    return "Palabra Reservada 'While'";

                case Token.Tipo.CADENA:
                    return "Cadena";
                case Token.Tipo.CARACTER:
                    return "Caracter";
                case Token.Tipo.LLAVE_IZQ:
                    return "Llave Izquierda";
                case Token.Tipo.LLAVE_DER:
                    return "Llave Derecha";
                case Token.Tipo.PARENTESIS_IZQ:
                    return "Llave Izquierda";
                case Token.Tipo.PARENTESIS_DER:
                    return "Llave Derecha";
                case Token.Tipo.CORCHETE_IZQ:
                    return "Llave Izquierda";
                case Token.Tipo.CORCHETE_DER:
                    return "Llave Derecha";
                case Token.Tipo.NUMERO_ENTERO:
                    return "Numero Entero";
                case Token.Tipo.NUMERO_REAL:
                    return "Numero Real";
                case Token.Tipo.IDENTIFICADOR:
                    return "Identificador";
                case Token.Tipo.COMENTARIO_LINEA:
                    return "Comentario de Linea";
                case Token.Tipo.COMENTARIO_MULTI:
                    return "Comentario Multilinea";

                case Token.Tipo.SIGNO_MENORQUE:
                    return "Signo Menor Que";
                case Token.Tipo.SIGNO_MAYORQUE:
                    return "Signo Dos Puntos";
                case Token.Tipo.SIGNO_IGUAL:
                    return "Signo Punto y Coma";
                case Token.Tipo.SIGNO_NEGACION:
                    return "Signo Menor Que";
                case Token.Tipo.SIGNO_DIVIDIDO:
                    return "Signo Menor Que";
                case Token.Tipo.SIGNO_MENOS:
                    return "Signo Dos Puntos";
                case Token.Tipo.SIGNO_MAS:
                    return "Signo Punto y Coma";
                case Token.Tipo.SIGNO_PUNTO:
                    return "Signo Dos Puntos";
                case Token.Tipo.SIGNO_COMA:
                    return "Signo Punto y Coma";

                case Token.Tipo.SIGNO_POR:
                    return "Signo Menor Que";
                case Token.Tipo.SIGNO_DOSPUNTOS:
                    return "Signo Dos Puntos";
                case Token.Tipo.SIGNO_PUNTOYCOMA:
                    return "Signo Punto y Coma";
                case Token.Tipo.ULTIMO:
                    return "ULTIMO";
                default:
                    return "Desconocido";
            }
        }

        public LinkedList<Simbolo> getListaSimbolos()
        {
            return ListaSimbolos;
        }
        public LinkedList<Error> getListaErrores()
        {
            return SalidaErrores;
        }

        public String getTraduccion()
        {
            return cadenaTraducida;
        }
        #endregion
    }
}
