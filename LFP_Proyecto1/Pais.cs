using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LFP_Proyecto1
{
    class Pais
    {
        int[] idPais;
        string[] nombre;
        int[] poblacion;
        int[] saturacion;
        string[] bandera;
        private int posContinente;
        int cuantosVan;
        int cuantosCaben;

        public Pais(int cantidad)
        {
            this.idPais = new int[cantidad];
            this.nombre = new string[cantidad];
            this.poblacion = new int[cantidad];
            this.saturacion = new int[cantidad];
            this.bandera = new string[cantidad];
            this.cuantosVan = 0;
            this.cuantosCaben = cantidad;
        }

        public void SetPais(string nombre, int poblacion, int saturacion, string bander)
        {
            if (cuantosVan < cuantosCaben)
            {
                this.idPais[cuantosVan] = cuantosVan + 1;
                this.nombre[cuantosVan] = nombre;
                this.poblacion[cuantosVan] = poblacion;
                this.saturacion[cuantosVan] = saturacion;
                this.bandera[cuantosVan] = bander;
                cuantosVan++;
            }
            else
            {
                //MessageBox.Show("Limite Excedido!");
            }
        }

        public int getIdPais(int qpos)
        {
            return idPais[qpos];
        }

        public void setIdPais(int qpos, int idSala)
        {
            this.idPais[qpos] = idSala;
        }

        public string getNombre(int qpos)
        {
            return nombre[qpos];
        }

        public void setNombre(int qpos, string nombre)
        {
            this.nombre[qpos] = nombre;
        }

        public int getPoblacion(int qpos)
        {
            return poblacion[qpos];
        }

        public void setPoblacion(int qpos, int idSala)
        {
            this.poblacion[qpos] = idSala;
        }

        public int getSaturacion(int qpos)
        {
            return saturacion[qpos];
        }

        public void setSaturacion(int qpos, int saturacion)
        {
            this.saturacion[qpos] = saturacion;
        }

        public string getBandera(int qpos)
        {
            return bandera[qpos];
        }

        public void setBandera(int qpos, string bandera)
        {
            this.bandera[qpos] = bandera;
        }

        public string getColor(int qpos)
        {
            if (getSaturacion(qpos)<=15)
            {
                return "white";
            }else if (getSaturacion(qpos) > 15 && getSaturacion(qpos) <= 30)
            {
                return "blue";
            }
            else if (getSaturacion(qpos) > 30 && getSaturacion(qpos) <= 45)
            {
                return "green";
            }
            else if (getSaturacion(qpos) > 45 && getSaturacion(qpos) <= 60)
            {
                return "yellow";
            }
            else if (getSaturacion(qpos) > 60 && getSaturacion(qpos) <= 75)
            {
                return "orange";
            }
            else
            { 
                return "red";
            }
        }

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

    }
}
