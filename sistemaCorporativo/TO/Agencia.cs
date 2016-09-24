using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistemaCorporativo.TO.Agencia
{
    class AgenciaAux
    {
        //Atributos 
        private string nomeAgencia;
        private string sigla;


        //métodos getters e setters
        public string getNomeAgencia()
        {
            return this.nomeAgencia;
        }
        public void setNomeAgencia(string nomeAgencia)
        {
            this.nomeAgencia = nomeAgencia;
        }

        public string getSigla()
        {
            return this.sigla;
        }
        public void setSigla(string sigla)
        {
            this.sigla = sigla;
        }
    }
}
