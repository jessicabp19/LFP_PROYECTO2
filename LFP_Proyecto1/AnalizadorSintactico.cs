using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LFP_Proyecto1
{
    class AnalizadorSintactico
    {
        //5+6>8+3!=true;

        #region PREPARACION
        int controlToken;
        Token tokenActual;
        private LinkedList<Token> listaTok;//Lista de tokens que el parser recibe del analizador lexico
        private LinkedList<Error> SalidaErrores;
        private LinkedList<Simbolo> ListaSimbolos;
        private LinkedList<String> ListaIDSAux;
        Simbolo.Tipo tipoActual = Simbolo.Tipo.VAR_STRING;
        //private Dictionary<String, string> myDictionary<String, String>();
        String cadenaTraducida = "";
        String cadenaAux = "";
        String valorVAR;
        int noAmbitos = 0;
        
        public AnalizadorSintactico()
        {
            SalidaErrores = new LinkedList<Error>();
            ListaSimbolos = new LinkedList<Simbolo>();
            ListaIDSAux = new LinkedList<String>();
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
            emparejar(Token.Tipo.PARENTESIS_DER);
            emparejar(Token.Tipo.LLAVE_IZQ);
            INSTRUCCION();
            emparejar(Token.Tipo.LLAVE_DER);
        }

        public void INSTRUCCION()//<INTRUCCION>--><SENTENCIAS> <INTRUCCION>
        {
            if (tokenActual.GetTipo() == Token.Tipo.PR_BOOL || tokenActual.GetTipo() == Token.Tipo.PR_INT ||
                tokenActual.GetTipo() == Token.Tipo.PR_STRING || tokenActual.GetTipo() == Token.Tipo.PR_FLOAT ||
                tokenActual.GetTipo() == Token.Tipo.PR_CHAR 
                || tokenActual.GetTipo() == Token.Tipo.IDENTIFICADOR || tokenActual.GetTipo() == Token.Tipo.PR_CONSOLE 
                || tokenActual.GetTipo() == Token.Tipo.PR_SWITCH || tokenActual.GetTipo() == Token.Tipo.PR_IF
                || tokenActual.GetTipo() == Token.Tipo.PR_FOR || tokenActual.GetTipo() == Token.Tipo.PR_WHILE) {
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
                DECLARACIONF();
            }
            else if(tokenActual.GetTipo()== Token.Tipo.IDENTIFICADOR)
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

        public void DECLARACIONF()
        {
            emparejar(Token.Tipo.PR_FLOAT);
            DECLARACIONPRIM();
            //Console.WriteLine("Error se esperaba el inicio de una Instrucción");
            //controlToken--;
            if (tokenActual.GetTipoE() == Token.Tipo.IDENTIFICADOR && tokenActual.GetValor().Equals("f"))
            {
                //if (tokenActual.GetValor().CompareTo('f')==0)
                //{
                //  emparejar(Token.Tipo.IDENTIFICADOR);
                ///}
                //else { 
                emparejar(Token.Tipo.IDENTIFICADOR);
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
                ListaIDSAux.AddLast(cadenaAux);
                emparejar(Token.Tipo.IDENTIFICADOR);
                DEC_VAR();
                //emparejar(Token.Tipo.SIGNO_PUNTOYCOMA);
                //cadenaTraducida += "\n";
            }
            else if (tokenActual.GetTipo() == Token.Tipo.CORCHETE_IZQ)
            {
                emparejar(Token.Tipo.CORCHETE_IZQ);
                emparejar(Token.Tipo.CORCHETE_DER);
                cadenaAux = tokenActual.GetValor();
                ListaIDSAux.AddLast(cadenaAux);
                emparejar(Token.Tipo.IDENTIFICADOR);
                DEC_ARRAY();
                cadenaTraducida += cadenaAux;
                //emparejar(Token.Tipo.SIGNO_PUNTOYCOMA);
                //cadenaTraducida += "\n";
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
                cadenaAux += tokenActual.GetValor();
                emparejar(Token.Tipo.SIGNO_COMA);
                cadenaAux += tokenActual.GetValor();
                ListaIDSAux.AddLast(tokenActual.GetValor());
                emparejar(Token.Tipo.IDENTIFICADOR);
                DEC_VAR();
            } 
            else if (tokenActual.GetTipo() == Token.Tipo.SIGNO_IGUAL)
            {
                cadenaTraducida += cadenaAux;//ACÁ YA SÉ QUE LO VA IGUALAR A ALGO
                cadenaAux = tokenActual.GetValor(); cadenaTraducida += cadenaAux;//GUARDO EL IGUAL Y LO AÑADO DE UNA VEZ
                emparejar(Token.Tipo.SIGNO_IGUAL);
                MEGAEXPRESION();
                if (tokenActual.GetTipo() == Token.Tipo.SIGNO_COMA)
                {
                    DEC_VAR();
                }
                //cadenaTraducida += cadenaAux;   
            }
        }

        public void MEGAEXPRESION()
        {
            EXPRESION();
            COMPARADOR();
        }

        public void COMPARADOR()
        {
            if (tokenActual.GetTipo() == Token.Tipo.SIGNO_MAYORQUE)
            {
                cadenaAux = tokenActual.GetValor();
                emparejar(Token.Tipo.SIGNO_MAYORQUE);MEGAEXPRESION();
            }
            else if (tokenActual.GetTipo() == Token.Tipo.SIGNO_MENORQUE)
            {
                cadenaAux = tokenActual.GetValor();
                emparejar(Token.Tipo.SIGNO_MENORQUE);EXPRESION();
            }
            else if (tokenActual.GetTipo() == Token.Tipo.SIGNO_NEGACION)
            {
                cadenaAux = tokenActual.GetValor();
                emparejar(Token.Tipo.SIGNO_NEGACION); EXPRESION();
            }
            else if (tokenActual.GetTipo() == Token.Tipo.SIGNO_IGUALDAD)
            {
                cadenaAux = tokenActual.GetValor();
                emparejar(Token.Tipo.SIGNO_IGUALDAD); EXPRESION();
            }
            else if (tokenActual.GetTipo() == Token.Tipo.PR_TRUE)
            {
                cadenaAux = tokenActual.GetValor();
                emparejar(Token.Tipo.PR_TRUE); //EXPRESION();
            }
            else if (tokenActual.GetTipo() == Token.Tipo.PR_FALSE)
            {
                cadenaAux = tokenActual.GetValor();
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
        public void TERMINOPRIM()
        {
            if (tokenActual.GetTipo() == Token.Tipo.SIGNO_POR)
            {
                cadenaTraducida += tokenActual.GetValor();
                emparejar(Token.Tipo.SIGNO_POR);
                //TERMINO();
                //EXPRESIONPRIM();
                FACTOR();
                TERMINOPRIM();
            }
            else if (tokenActual.GetTipo() == Token.Tipo.SIGNO_DIVIDIDO)
            {
                cadenaTraducida += tokenActual.GetValor();
                emparejar(Token.Tipo.SIGNO_DIVIDIDO);
                //TERMINO();
                //EXPRESIONPRIM();
                FACTOR();
                TERMINOPRIM();
            }
        }
        public void FACTOR()
        {
            if (tokenActual.GetTipo() == Token.Tipo.PARENTESIS_IZQ)
            {
                cadenaAux = tokenActual.GetValor();
                cadenaTraducida += cadenaAux;
                valorVAR = cadenaAux;
                //asignarValor();
                emparejar(Token.Tipo.PARENTESIS_IZQ); MEGAEXPRESION(); emparejar(Token.Tipo.PARENTESIS_DER);
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
                valorVAR = cadenaAux;
                asignarValor();
                emparejar(Token.Tipo.IDENTIFICADOR);
            }
            else if (tokenActual.GetTipo() == Token.Tipo.CADENA)
            {
                cadenaAux = tokenActual.GetValor();
                valorVAR = cadenaAux;
                asignarValor();
                emparejar(Token.Tipo.CADENA);
            }
            else if (tokenActual.GetTipo() == Token.Tipo.PR_TRUE)
            {
                cadenaAux = tokenActual.GetValor();
                valorVAR = cadenaAux;
                asignarValor();
                emparejar(Token.Tipo.PR_TRUE);
            }
            else if (tokenActual.GetTipo() == Token.Tipo.PR_FALSE)
            {
                cadenaAux = tokenActual.GetValor();
                valorVAR = cadenaAux;
                asignarValor();
                emparejar(Token.Tipo.PR_FALSE);
            }
            else 
            { 
                Console.WriteLine("Error se esperaba una expresión");
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

        public void DEC_ARRAY()
        {
            if (tokenActual.GetTipo() == Token.Tipo.LLAVE_IZQ)
            {
                emparejar(Token.Tipo.LLAVE_IZQ);
                emparejar(Token.Tipo.IDENTIFICADOR);
                DEC_VAR();
            }
            else if (tokenActual.GetTipo() == Token.Tipo.SIGNO_IGUAL)
            {
                emparejar(Token.Tipo.SIGNO_IGUAL);
                EXPRESION();
            }
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
            else
            {
                //EPSILON NO SE HACE NADA   O   REPORTA ERROR
            }
        }
        #endregion

        #region ASIGNACION
        public void ASIGNACION()
        {
            if (tokenActual.GetTipo() == Token.Tipo.IDENTIFICADOR)
            {
                cadenaAux = tokenActual.GetValor();
                ListaIDSAux.AddLast(cadenaAux);
                emparejar(Token.Tipo.IDENTIFICADOR);
                ASIG_VAR();
                emparejar(Token.Tipo.SIGNO_PUNTOYCOMA);
                cadenaTraducida += "\n";
            }
        }

        public void ASIG_VAR()
        {
            cadenaTraducida += indentacion()+ cadenaAux;//ACÁ YA SÉ QUE LO VA IGUALAR A ALGO
            cadenaAux = tokenActual.GetValor(); cadenaTraducida += cadenaAux;//GUARDO EL IGUAL Y LO AÑADO DE UNA VEZ
            emparejar(Token.Tipo.SIGNO_IGUAL);
            EXPRESION();
            cadenaTraducida += cadenaAux;
        }
        #endregion

        #region IMPRIMIR
        public void IMPRIMIR()
        {
            emparejar(Token.Tipo.PR_CONSOLE);
            emparejar(Token.Tipo.SIGNO_PUNTO);
            emparejar(Token.Tipo.PR_WRITELINE);
            emparejar(Token.Tipo.PARENTESIS_IZQ);
            CONTENIDOIMPRESION();
            emparejar(Token.Tipo.PARENTESIS_DER);
            emparejar(Token.Tipo.SIGNO_PUNTOYCOMA);
        }

        public void CONTENIDOIMPRESION()
        {
            Console.WriteLine();
            EXPRESION();
            PLUS();
        }

        public void PLUS()
        {
            if (tokenActual.GetTipo()==Token.Tipo.SIGNO_MAS)
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
            cadenaTraducida += indentacion()+tokenActual.GetValor();
            emparejar(Token.Tipo.PR_IF);
            noAmbitos++;
            emparejar(Token.Tipo.PARENTESIS_IZQ);
            MEGAEXPRESION();
            emparejar(Token.Tipo.PARENTESIS_DER);
            emparejar(Token.Tipo.LLAVE_IZQ);
            cadenaTraducida += "\n";
            INSTRUCCION();
            emparejar(Token.Tipo.LLAVE_DER);noAmbitos--;
            SENTENCIA_ELSE();
        }

        public void SENTENCIA_ELSE()
        {
            if (tokenActual.GetTipo()==Token.Tipo.PR_ELSE)
            {
                cadenaTraducida += indentacion() + tokenActual.GetValor()+":";
                emparejar(Token.Tipo.PR_ELSE);
                emparejar(Token.Tipo.LLAVE_IZQ);
                cadenaTraducida += "\n";
                INSTRUCCION();
                emparejar(Token.Tipo.LLAVE_DER);noAmbitos--;
            }
        }
        #endregion

        #region SENTENCIAS Y CICLOS PENDIENTES

        public void SENTENCIA_SWITCH()
        {
            //cadenaTraducida += indentacion() + tokenActual.GetValor();
            emparejar(Token.Tipo.PR_SWITCH);
            noAmbitos++;
            emparejar(Token.Tipo.PARENTESIS_IZQ);
            MEGAEXPRESION();
            emparejar(Token.Tipo.PARENTESIS_DER);
            emparejar(Token.Tipo.LLAVE_IZQ);
            //cadenaTraducida += "\n";
            CASES();
            //CASEDEFAULT();
            emparejar(Token.Tipo.LLAVE_DER);
        }

        public void CASES()
        {
            if (tokenActual.GetTipo()==Token.Tipo.PR_CASE)
            {
                CASE();
                CASES();
            }
            else
            {
                CASEDEFAULT();
            }
        }

        public void CASE()
        {
            emparejar(Token.Tipo.PR_CASE);
            MEGAEXPRESION();
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
                emparejar(Token.Tipo.SIGNO_DOSPUNTOS);
                INSTRUCCION();
                emparejar(Token.Tipo.PR_BREAK);
                emparejar(Token.Tipo.SIGNO_PUNTOYCOMA);
            }
            
        }

        public void CICLO_FOR()
        {
            cadenaTraducida += indentacion() + tokenActual.GetValor();
            emparejar(Token.Tipo.PR_FOR);
            noAmbitos++;
            emparejar(Token.Tipo.PARENTESIS_IZQ);
            INICIALIZADOR();
            emparejar(Token.Tipo.SIGNO_PUNTOYCOMA);
            //CONDICION
            emparejar(Token.Tipo.SIGNO_PUNTOYCOMA);
            //INICIALIZACION
            emparejar(Token.Tipo.PARENTESIS_DER);
            emparejar(Token.Tipo.LLAVE_IZQ);
            cadenaTraducida += "\n";
            INSTRUCCION();
            emparejar(Token.Tipo.LLAVE_DER);
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

        public void CICLO_WHILE()
        {
            cadenaTraducida += indentacion() + tokenActual.GetValor();
            emparejar(Token.Tipo.PR_WHILE);
            noAmbitos++;
            emparejar(Token.Tipo.PARENTESIS_IZQ);
            MEGAEXPRESION();
            emparejar(Token.Tipo.PARENTESIS_DER);
            emparejar(Token.Tipo.LLAVE_IZQ);
            cadenaTraducida += "\n";
            INSTRUCCION();
            emparejar(Token.Tipo.LLAVE_DER);noAmbitos--;
        }
        #endregion

        #region Métodos Aux
        public void emparejar(Token.Tipo tip)
        {
            if (tokenActual.GetTipoE() != tip)
            {
                Console.WriteLine("Error se esperaba " + getTipoParaError(tip));
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
