using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LFP_Proyecto1
{
    class Escogido
    {
        int[] posPais;
        int[] saturacionPais;
        int[] posContinente;
        int[] saturacionCont;
        int cuantosVan;
        int cuantosCaben;

        public Escogido(int cantidad)
        {
            this.posPais = new int[cantidad];
            this.saturacionPais = new int[cantidad];
            this.posContinente = new int[cantidad];
            this.saturacionCont = new int[cantidad];
            this.cuantosVan = 0;
            this.cuantosCaben = cantidad;
        }

        public void SetEscogido(int posPais, int satP, int posCont, int satC)
        {
            if (cuantosVan < cuantosCaben)
            {
                this.posPais[cuantosVan] = posPais;//cuantosVan + 1
                this.saturacionPais[cuantosVan] = satP;
                this.posContinente[cuantosVan] = posCont;
                this.saturacionCont[cuantosVan] = satC;
                cuantosVan++;
            }
            else
            {
                //MessageBox.Show("Limite Excedido!");
            }
        }

        public int getPosPais(int qpos)
        {
            return posPais[qpos];
        }

        public void setPosPais(int qpos, int idSala)
        {
            this.posPais[qpos] = idSala;
        }

        public int getSaturacionPais(int qpos)
        {
            return saturacionPais[qpos];
        }

        public void setSaturacionPais(int qpos, int nombre)
        {
            this.saturacionPais[qpos] = nombre;
        }

        public int getposContinente(int qpos)
        {
            return posContinente[qpos];
        }

        public void setposContinente(int qpos, int idSala)
        {
            this.posContinente[qpos] = idSala;
        }

        public int getSaturacionCont(int qpos)
        {
            return saturacionCont[qpos];
        }

        public void setSaturacionCont(int qpos, int saturacion)
        {
            this.saturacionCont[qpos] = saturacion;
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
