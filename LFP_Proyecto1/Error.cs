using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LFP_Proyecto1
{
    class Error
    {
        public enum Tipo
        {
            PALABRA_DESCONOCIDA,
            CARACTER_DESCONOCIDO
        }

        private Tipo tipoError; //QUE TIPO ES
        private String valor; //QUE VALOR TIENE (TANTO NUMERICO O TEXTO)
        private int noFila, noColumna;
        private String descripcion;

        public Error(Tipo tipoDelError, String val, int f, int c)
        {
            this.tipoError = tipoDelError;
            this.valor = val;
            this.noFila = f;
            this.noColumna = c;
        }

        public Error(String descError, int f, int c)
        {
            this.descripcion = descError;
            this.noFila = f;
            this.noColumna = c;
        }

        public String getDescripcion()
        {
            return descripcion;
        }

        public String GetValor()
        {
            return valor;
        }

        public int GetFila()
        {
            return noFila;
        }

        public int GetColumna()
        {
            return noColumna;
        }

        public String GetTipo()
        {
            switch (tipoError)
            {
                case Tipo.PALABRA_DESCONOCIDA:
                    return "Palabra Desconocida";
                case Tipo.CARACTER_DESCONOCIDO:
                    return "Caracter Desconocido";
                default:
                    return "Desconocido";
            }
        }
    }
}
