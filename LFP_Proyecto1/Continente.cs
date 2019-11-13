using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LFP_Proyecto1
{
    class Continente
    {
        public int[] idContinente;
        private String[] nombreC;
        private int[] saturacion;
        private Pais[] ListaPaises;
        private Pais auxPais;
        private int cuantosVan;
        private int cuantosCaben;

        public Continente(int cantidad)
        {
            this.idContinente = new int[cantidad];
            this.nombreC = new String[cantidad];
            this.saturacion = new int[cantidad];
            this.ListaPaises = new Pais[cantidad];
            this.cuantosVan = 0;
            this.cuantosCaben = cantidad;
        }

        public void setContinente(String nombre)
        {
            if (cuantosVan < cuantosCaben)
            {
                    this.idContinente[cuantosVan] = cuantosVan + 1;
                    this.nombreC[cuantosVan] = nombre;
                    this.saturacion[cuantosVan] = 0;//HAY QUE CALCULARLO
                    //this.cantidadPaises[cuantosVan] = cantidadPaises;
                    this.ListaPaises[cuantosVan] = new Pais(25);
                    cuantosVan++;
            }else{
                    //JOptionPane.showMessageDialog(null, "Excede el límite de ingresos");
             
            }
        }

        #region setters y getters
        public int getIdContinente(int qpos)
        {
            return idContinente[qpos];
        }

        public void setIdContinente(int qpos, int idSucursal)
        {
            this.idContinente[qpos] = idSucursal;
        }

        public String getNombre(int qpos)
        {
            return nombreC[qpos];
        }

        public void setNombre(int qpos, String nombre)
        {
            this.nombreC[qpos] = nombre;
        }
        #endregion

        public int GetSaturacion(int qpos)
        {
            auxPais = getListaPaises(qpos);
            int suma = 0;
            for (int i=0; i<auxPais.getCuantosVan();i++)
            {
                suma = suma + auxPais.getSaturacion(i);
            }
            int satContinente = suma / auxPais.getCuantosVan();
            return satContinente;
        }

        public string GetColor(int qpos)
        {
            if (GetSaturacion(qpos) <= 15)
            {
                return "white";
            }
            else if (GetSaturacion(qpos) > 15 && GetSaturacion(qpos) <= 30)
            {
                return "blue";
            }
            else if (GetSaturacion(qpos) > 30 && GetSaturacion(qpos) <= 45)
            {
                return "green";
            }
            else if (GetSaturacion(qpos) > 45 && GetSaturacion(qpos) <= 60)
            {
                return "yellow";
            }
            else if (GetSaturacion(qpos) > 60 && GetSaturacion(qpos) <= 75)
            {
                return "orange";
            }
            else
            {
                return "red";
            }
        }

        public Pais getListaPaises(int qpos)//POR QUE DEVUELVE EL ARREGLO DE PAISES EN ESE CONTINTENTE
        {
            return ListaPaises[qpos];
        }

        public int posPaisMenosSaturado(int qpos)
        {
            auxPais = getListaPaises(qpos);
            int min = auxPais.getSaturacion(0);
            int pos = 0;
            for (int j = 0; j < auxPais.getCuantosVan(); j++)
            {
                if (auxPais.getSaturacion(j) < min)
                {
                    min = auxPais.getSaturacion(j);
                    pos = j;
                }
            }
            return pos;
        }

        public int MenosSaturado(int qpos)
        {
            auxPais = getListaPaises(qpos);
            int min = auxPais.getSaturacion(0);
            int pos = 0;
            for (int j = 0; j < auxPais.getCuantosVan(); j++)
            {
                if (auxPais.getSaturacion(j) < min)
                {
                    min = auxPais.getSaturacion(j);
                    pos = j;
                }
            }
            return min;
        }
        #region 
        public int getCuantosVan()
        {
            return cuantosVan;
        }

        public void setCuantosVan(int cuantosVan)
        {
            this.cuantosVan = cuantosVan;
        }

        public int getCuantosCaben()
        {
            return cuantosCaben;
        }

        public void setCuantosCaben(int cuantosCaben)
        {
            this.cuantosCaben = cuantosCaben;
        }
        #endregion
    }
}
