using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LFP_Proyecto1
{
    class Country
    {

         private int idPais;
         private string nombreP;
         private int poblacion;
         private int saturacion;
         private string imagen;
         private string continente;

        public Country(int idPais, string nombreP, int poblacion, int saturacion, string imagen, string continente)
        {
            this.idPais = idPais;
            this.nombreP = nombreP;
            this.poblacion = poblacion;
            this.saturacion = saturacion;
            this.imagen = imagen;
            this.continente = continente;
        }

        public int GetId()
        {
            return idPais;
        }
        public string getNombreP()
            {
                return nombreP;
        }
        public int getPoblacion()
        {
            return poblacion;
        }
        public int getSaturacion()
        {
            return saturacion;
        }
        public string getImagen()
            {
                return imagen;
            }
        public string getContinente()
            {
                return continente;
            }
        
    }
}
