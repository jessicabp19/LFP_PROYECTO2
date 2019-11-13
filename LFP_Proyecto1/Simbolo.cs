using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LFP_Proyecto1
{
    class Simbolo
    {
        private string nombreVar;
        private string valorVar;
        private Tipo tipoVar;

        public enum Tipo
        {
            VAR_INT,
            VAR_FLOAT,
            VAR_CHAR,
            VAR_STRING,
            VAR_BOOL
        }
        public Simbolo(Tipo tipoVariable, string nombreVariable, string valorVariable)
        {
            this.tipoVar = tipoVariable;
            this.nombreVar = nombreVariable;
            this.valorVar = valorVariable;
        }

        public string getNombreVar()
        {
            return this.nombreVar;
        }

        public string getValorVar()
        {
            return this.valorVar;
        }

        public string getTipo()
        {
            return this.tipoVar.ToString();
        }

        public void setNombreVar(string nom)
        {
            nombreVar= nom;
        }

        public void setValorVar(string val)
        {
            valorVar = val;
        }

        public void setTipo(Tipo tipo)
        {
            tipoVar = tipo;
        }
    }
}
