using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LFP_Proyecto1
{
    class Token
    {
        public enum Tipo
        {
            PR_CLASS,
            PR_STATIC,
            PR_VOID,
            PR_STRING,
            PR_INT,
            PR_FLOAT,
            PR_CHAR,
            PR_BOOL,
            //Nuevos
            PR_MAIN,
            PR_ARGS,//Puede ser cualquier cosa
            PR_TRUE,
            PR_FALSE,
            PR_NEW,
            PR_CONSOLE,
            PR_WRITELINE,
            PR_IF,
            PR_ELSE,
            PR_SWITCH,
            PR_CASE,
            PR_BREAK,
            PR_DEFAULT,
            PR_FOR,
            PR_WHILE,
            //Estos 3 si
            CADENA,
            CARACTER,//Nou
            LLAVE_IZQ,
            LLAVE_DER,//<--
            PARENTESIS_IZQ,
            PARENTESIS_DER,
            CORCHETE_IZQ,
            CORCHETE_DER,
            NUMERO_ENTERO,
            NUMERO_REAL,
            IDENTIFICADOR,
            COMENTARIO_LINEA, 
            COMENTARIO_MULTI,
            SIGNO_MENORQUE,
            SIGNO_MAYORQUE,
            SIGNO_IGUAL,
            SIGNO_NEGACION,//   !
            SIGNO_PUNTO,
            SIGNO_COMA,
            SIGNO_DIVIDIDO,
            SIGNO_MENOS,
            SIGNO_MAS,
            SIGNO_IGUALDAD,
            SIGNO_MAYORIQ,
            SIGNO_MENORIQ,
            //
            SIGNO_POR,
            SIGNO_DOSPUNTOS,
            SIGNO_PUNTOYCOMA, 
            ULTIMO//NO HE HECHO NADA MÁS
        }

        #region AUX
        private Tipo tipoToken; //QUE TIPO ES
        private String valorToken; //QUE VALOR TIENE (TANTO NUMERICO O TEXTO)
        private int noFila, noColumna;

        public Token(Tipo tipoDelToken, String val, int f, int c)
        {
            this.tipoToken = tipoDelToken;
            this.valorToken = val;
            this.noFila = f;
            this.noColumna = c;
        }

        public String GetValor()
        {
            return valorToken;
        }

        public int GetFila()
        {
            return noFila;
        }

        public int GetColumna()
        {
            return noColumna;
        }
        #endregion

        public Tipo GetTipoE()
        {
            return tipoToken;
        }

        public String GetTipoString(){
            switch (tipoToken){
                case Tipo.PR_CLASS:
                    return "Palabra Reservada 'Class'";
                case Tipo.PR_STATIC:
                    return "Palabra Reservada 'Static'";
                case Tipo.PR_VOID:
                    return "Palabra Reservada 'Void'";
                case Tipo.PR_STRING:
                    return "Palabra Reservada 'String'";
                case Tipo.PR_FLOAT:
                    return "Palabra Reservada 'Float'";
                case Tipo.PR_INT:
                    return "Palabra Reservada 'Int'";
                case Tipo.PR_CHAR:
                    return "Palabra Reservada 'Char'";
                case Tipo.PR_BOOL:
                    return "Palabra Reservada 'Bool'";

                case Tipo.PR_MAIN:
                    return "Palabra Reservada 'Main'";
                case Tipo.PR_ARGS:
                    return "Palabra Reservada 'Args'";
                case Tipo.PR_TRUE:
                    return "Palabra Reservada 'True'";
                case Tipo.PR_FALSE:
                    return "Palabra Reservada 'False'";
                case Tipo.PR_NEW:
                    return "Palabra Reservada 'New'";
                case Tipo.PR_CONSOLE:
                    return "Palabra Reservada 'Console'";
                case Tipo.PR_WRITELINE:
                    return "Palabra Reservada 'WriteLine'";

                case Tipo.PR_IF:
                    return "Palabra Reservada 'If'";
                case Tipo.PR_ELSE:
                    return "Palabra Reservada 'Else'";
                case Tipo.PR_SWITCH:
                    return "Palabra Reservada 'Switch'";
                case Tipo.PR_CASE:
                    return "Palabra Reservada 'Case'";
                case Tipo.PR_BREAK:
                    return "Palabra Reservada 'Break'";
                case Tipo.PR_DEFAULT:
                    return "Palabra Reservada 'Default'";


                case Tipo.PR_FOR:
                    return "Palabra Reservada 'For'";
                case Tipo.PR_WHILE:
                    return "Palabra Reservada 'While'";

                case Tipo.CADENA:
                    return "Cadena";
                case Tipo.CARACTER:
                    return "Caracter";
                case Tipo.LLAVE_IZQ:
                    return "Llave Izquierda";
                case Tipo.LLAVE_DER:
                    return "Llave Derecha";
                case Tipo.PARENTESIS_IZQ:
                    return "Llave Izquierda";
                case Tipo.PARENTESIS_DER:
                    return "Llave Derecha";
                case Tipo.CORCHETE_IZQ:
                    return "Llave Izquierda";
                case Tipo.CORCHETE_DER:
                    return "Llave Derecha";
                case Tipo.NUMERO_ENTERO:
                    return "Numero Entero";
                case Tipo.NUMERO_REAL:
                    return "Numero Real";
                case Tipo.IDENTIFICADOR:
                    return "Identificador";
                case Tipo.COMENTARIO_LINEA:
                    return "Comentario de Linea";
                case Tipo.COMENTARIO_MULTI:
                    return "Comentario Multilinea";

                case Tipo.SIGNO_MENORQUE:
                    return "Signo Menor Que";
                case Tipo.SIGNO_MAYORQUE:
                    return "Signo Mayor Que";
                case Tipo.SIGNO_IGUAL:
                    return "Signo Igual";
                case Tipo.SIGNO_NEGACION:
                    return "Signo Negación";
                case Tipo.SIGNO_DIVIDIDO:
                    return "Signo Dividido";
                case Tipo.SIGNO_MENOS:
                    return "Signo Menos";
                case Tipo.SIGNO_MAS:
                    return "Signo Mas";
                case Tipo.SIGNO_PUNTO:
                    return "Signo Punto";
                case Tipo.SIGNO_COMA:
                    return "Signo Coma";
                case Tipo.SIGNO_IGUALDAD:
                    return "Signo de Igualdad";
                case Tipo.SIGNO_MAYORIQ:
                    return "Signo MayorIgualQue";
                case Tipo.SIGNO_MENORIQ:
                    return "Signo MenorIgualQue";

                case Tipo.SIGNO_POR:
                    return "Signo Por";
                case Tipo.SIGNO_DOSPUNTOS:
                    return "Signo Dos Puntos";
                case Tipo.SIGNO_PUNTOYCOMA:
                    return "Signo Punto y Coma";
                case Tipo.ULTIMO:
                    return "ULTIMO";
                default:
                    return "Desconocido";
            }
        }

        public Tipo GetTipoEnum()
        {
            switch (tipoToken)
            {
                case Tipo.PR_CLASS:
                    return Tipo.PR_CLASS;
                case Tipo.PR_STATIC:
                    return Tipo.PR_STATIC;
                case Tipo.PR_VOID:
                    return Tipo.PR_VOID;
                case Tipo.PR_STRING:
                    return Tipo.PR_STRING;
                case Tipo.PR_FLOAT:
                    return Tipo.PR_FLOAT;
                case Tipo.PR_INT:
                    return Tipo.PR_INT;
                case Tipo.PR_CHAR:
                    return Tipo.PR_CHAR;
                case Tipo.CADENA:
                    return Tipo.CADENA;
                case Tipo.PR_BOOL:
                    return Tipo.PR_BOOL;
                default:
                    return Tipo.CADENA;
            }
        }

        public Tipo GetTipo()
        {
            return tipoToken;
        }
    }
}
