using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistemaCorporativo.TO.Caso
{
    class Caso
    {
        //Atributos 
        private string numerocaso;
        private string titulocaso;
        private string tipocaso;
        private string deploc;
        private string categoriacaso;
        private string agente1;
        private string agente2;
        private string agente3;
        private string agente4;
        private DateTime dataabertura;
        private string status;
        private DateTime datafechamento;
        private string tipolavagem;
        private string forcatarefa;
        private string municio;
        private string estado;
        private string refoutraagencia;
        private string grandejuri;
        private string intervencaojuri;
        private string relatoriojuri;

        //métodos getters e setters

        public string getNumeroCaso()
        {
            return this.numerocaso;
        }
        public void setNumeroCaso(string numerocaso)
        {
            this.numerocaso = numerocaso;
        }

        public string getTituloCaso()
        {
            return this.titulocaso;
        }
        public void setTituloCaso(string titulocaso)
        {
            this.titulocaso = titulocaso;
        }

        public string getTipoCaso()
        {
            return this.tipocaso;
        }
        public void setTipoCaso(string tipocaso)
        {
            this.tipocaso = tipocaso;
        }

        public string getDepLoc()
        {
            return this.deploc;
        }
        public void setDepLoc(string deploc)
        {
            this.deploc = deploc;
        }

        public string getCategoriaCaso()
        {
            return this.categoriacaso;
        }
        public void setCategoriaCaso(string categoriacaso)
        {
            this.categoriacaso = categoriacaso;
        }

        public string getAgente1()
        {
            return this.agente1;
        }
        public void setAgente1(string agente1)
        {
            this.agente1 = agente1;
        }

        public string getAgente2()
        {
            return this.agente2;
        }
        public void setAgente2(string agente2)
        {
            this.agente2 = agente2;
        }

        public string getAgente3()
        {
            return this.agente3;
        }
        public void setAgente3(string agente3)
        {
            this.agente3 = agente3;
        }

        public string getAgente4()
        {
            return this.agente4;
        }
        public void setAgente4(string agente4)
        {
            this.agente4 = agente4;

        }

        public DateTime getDataAbertura()
        {
            return this.dataabertura;
        }
        public void setDataAbertura(DateTime dataabertura)
        {
            this.dataabertura = dataabertura;
        }

        public string getStatus()
        {
            return this.status;
        }
        public void setStatus(string status)
        {
            this.status = status;

        }

        public DateTime getDataFechamento()
        {
            return this.datafechamento;
        }
        public void setDataFechamento(DateTime datafechamento)
        {
            this.datafechamento = datafechamento;
        }

        public string getTipoLavagem()
        {
            return this.tipolavagem;
        }
        public void setTipoLavagem(string tipolavagem)
        {
            this.tipolavagem = tipolavagem;
        }

        public string getForcaTarefa()
        {
            return this.forcatarefa;
        }
        public void setForcaTarefa(string forcatarefa)
        {
            this.forcatarefa = forcatarefa;
        }

        public string getMunicipio()
        {
            return this.municio;
        }
        public void setMunicipio(string municio)
        {
            this.municio = municio;
        }

        public string getEstado()
        {
            return this.estado;
        }
        public void setEstado(string estado)
        {
            this.estado = estado;
        }

        public string getRefOutraAgencia()
        {
            return this.refoutraagencia;
        }
        public void setRefOutraAgencia(string refoutraagencia)
        {
            this.refoutraagencia = refoutraagencia;
        }

        public string getGrandeJuri()
        {
            return this.grandejuri;
        }
        public void setGrandeJuri(string grandejuri)
        {
            this.grandejuri = grandejuri;
        }

        public string getIntervencaoJuri()
        {
            return this.intervencaojuri;
        }
        public void setIntervencaoJuri(string intervencaojuri)
        {
            this.intervencaojuri = intervencaojuri;
        }

        public string getRelJuri()
        {
            return this.relatoriojuri;
        }
        public void setRelJuri(string relatoriojuri)
        {
            this.relatoriojuri = relatoriojuri;
        }

    }
}
