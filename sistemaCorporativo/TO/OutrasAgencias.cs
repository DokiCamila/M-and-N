using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistemaCorporativo.TO.AgenciaAux
{
    class AgenciaAux
    {

        //Atributos 
        private string nomeAgencia;
        private string nomeAgente;


        //métodos getters e setters
        public string getNomeAgencia()
        {
            return this.nomeAgencia;
        }
        public void setNomeAgencia(string nomeAgencia)
        {
            this.nomeAgencia = nomeAgencia;
        }

        public string getNomeAgente()
        {
            return this.nomeAgente;
        }
        public void setNomeAgente(string nomeAgente)
        {
            this.nomeAgente = nomeAgente;
        }

    }
}
