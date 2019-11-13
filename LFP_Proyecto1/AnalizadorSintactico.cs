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
        String cadenaTraducida = "";
        String cadenaAux = "";
        String valorVAR;
        
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

        public void INSTRUCCION()//INTRUCCION
        {
            if (tokenActual.GetTipo() == Token.Tipo.PR_BOOL || tokenActual.GetTipo() == Token.Tipo.PR_INT ||
                tokenActual.GetTipo() == Token.Tipo.PR_STRING || tokenActual.GetTipo() == Token.Tipo.PR_FLOAT ||
                tokenActual.GetTipo() == Token.Tipo.PR_CHAR 
                || tokenActual.GetTipo() == Token.Tipo.IDENTIFICADOR || tokenActual.GetTipo() == Token.Tipo.PR_CONSOLE 
                || tokenActual.GetTipo() == Token.Tipo.PR_SWITCH || tokenActual.GetTipo() == Token.Tipo.PR_FOR 
                || tokenActual.GetTipo() == Token.Tipo.PR_WHILE) {
                Console.WriteLine("CUMPLIÓ CON SENTENCIA");
                SENTENCIAS();
                INSTRUCCION();
            }
        }
        public void SENTENCIAS()
        {
            if (tokenActual.GetTipo() == Token.Tipo.PR_BOOL || tokenActual.GetTipo() == Token.Tipo.PR_INT ||
                tokenActual.GetTipo() == Token.Tipo.PR_STRING || tokenActual.GetTipo() == Token.Tipo.PR_FLOAT ||
                tokenActual.GetTipo() == Token.Tipo.PR_CHAR)
            {
                DECLARACION();
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
        public void DECLARACION()//<DECLARACION_VAR> -->   <TIPO_DATO> IDENTIFICADOR <DEC_VAR> ;
        {                       //<DECLARACION_VAR> -->   <TIPO_DATO> [] IDENTIFICADOR <Algo> ;
            TIPO_DATO();

            if (tokenActual.GetTipo() == Token.Tipo.IDENTIFICADOR)
            {
                cadenaAux = tokenActual.GetValor();//cadenaTraducida += cadenaAux;
                ListaIDSAux.AddLast(cadenaAux);//---------------------------------
                emparejar(Token.Tipo.IDENTIFICADOR);
                DEC_VAR();
                cadenaTraducida += cadenaAux;
                emparejar(Token.Tipo.SIGNO_PUNTOYCOMA);
                cadenaTraducida +="\n";
            }
            else if (tokenActual.GetTipo() == Token.Tipo.CORCHETE_IZQ)
            {
                emparejar(Token.Tipo.CORCHETE_IZQ);
                emparejar(Token.Tipo.CORCHETE_DER);
                emparejar(Token.Tipo.IDENTIFICADOR);
                DEC_ARRAY();
                //cadenaTraducida.Append(cadenaAux);
                cadenaTraducida += cadenaAux;
                cadenaAux = tokenActual.GetValor();
                emparejar(Token.Tipo.SIGNO_PUNTOYCOMA);
            }
        }

        #region var
        public void DEC_VAR()
        {
            if (tokenActual.GetTipo() == Token.Tipo.SIGNO_COMA)
            {
                emparejar(Token.Tipo.SIGNO_COMA);
                ListaIDSAux.AddLast(tokenActual.GetValor());//--------------------------
                emparejar(Token.Tipo.IDENTIFICADOR);
                DEC_VAR();
            } else if (tokenActual.GetTipo() == Token.Tipo.SIGNO_IGUAL)
            {
                cadenaTraducida += cadenaAux;
                cadenaAux = tokenActual.GetValor();
                emparejar(Token.Tipo.SIGNO_IGUAL);
                EXPRESION();
            }
        }

        public void EXPRESION()
        {
            TERMINO();
            EXPRESIONP();
        }

        public void TERMINO()
        {
            FACTOR();
            TERMINOPRIM();
        }

        public void EXPRESIONP()
        {
            if (tokenActual.GetTipo() == Token.Tipo.SIGNO_MAS)
            {
                emparejar(Token.Tipo.SIGNO_MAS);
                TERMINO();
                EXPRESIONP();
            }
            else if (tokenActual.GetTipo() == Token.Tipo.SIGNO_MENOS)
            {
                emparejar(Token.Tipo.SIGNO_MENOS);
                TERMINO();
                EXPRESIONP();
            }
        } 

        public void TERMINOPRIM()
        {
            if (tokenActual.GetTipo() == Token.Tipo.SIGNO_POR)
            {
                emparejar(Token.Tipo.SIGNO_POR);
                FACTOR();
                TERMINOPRIM();
            }
            else if (tokenActual.GetTipo() == Token.Tipo.SIGNO_DIVIDIDO)
            {
                emparejar(Token.Tipo.SIGNO_DIVIDIDO);
                FACTOR();
                TERMINOPRIM();
            }
        }

        public void FACTOR()
        {
            if (tokenActual.GetTipo() == Token.Tipo.PARENTESIS_IZQ)
            {
                //cadenaTraducida.Append(cadenaAux);
                cadenaTraducida+=cadenaAux;
                cadenaAux = tokenActual.GetValor();
               
                emparejar(Token.Tipo.PARENTESIS_IZQ);
            }
            else if (tokenActual.GetTipo() == Token.Tipo.NUMERO_ENTERO)
            {
                //cadenaTraducida.Append(cadenaAux);
                cadenaTraducida+=cadenaAux;
                cadenaAux = tokenActual.GetValor();
                valorVAR = cadenaAux;
                asignarValor();
                emparejar(Token.Tipo.NUMERO_ENTERO);
            }
            else if (tokenActual.GetTipo() == Token.Tipo.NUMERO_REAL)
            {
                emparejar(Token.Tipo.NUMERO_REAL);
            }
            else if (tokenActual.GetTipo() == Token.Tipo.IDENTIFICADOR)
            {
                emparejar(Token.Tipo.IDENTIFICADOR);
            }
            else if (tokenActual.GetTipo() == Token.Tipo.CADENA)
            {
                emparejar(Token.Tipo.CADENA);
            }
            else if (tokenActual.GetTipo() == Token.Tipo.PR_TRUE)
            {
                emparejar(Token.Tipo.PR_TRUE);
            }
            else if (tokenActual.GetTipo() == Token.Tipo.PR_FALSE)
            {
                emparejar(Token.Tipo.PR_FALSE);
            }
            else 
            { 
                Console.WriteLine("Error se esperaba una expresión");
                controlToken--;
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
        
        public void ASIGNACION()
        {
            Console.WriteLine("ASIGNO ALGO");
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

        #region IMPRIMIR
        public void IMPRIMIR()
        {
            if (tokenActual.GetTipo() == Token.Tipo.PR_CONSOLE)
            {
                emparejar(Token.Tipo.PR_CONSOLE);
                emparejar(Token.Tipo.SIGNO_PUNTO);
                emparejar(Token.Tipo.PR_WRITELINE);
                emparejar(Token.Tipo.PARENTESIS_IZQ);
                CONTENIDOIMPRESION();
                emparejar(Token.Tipo.PARENTESIS_DER);

            }
        }

        public void CONTENIDOIMPRESION()
        {
            Console.WriteLine("CONTENIDO");
        }
        #endregion

#region SENTENCIAS Y CICLOS
        public void SENTENCIA_IF()
        {

        }

        public void SENTENCIA_SWITCH()
        {

        }

        public void CICLO_FOR()
        {

        }

        public void CICLO_WHILE()
        {

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
