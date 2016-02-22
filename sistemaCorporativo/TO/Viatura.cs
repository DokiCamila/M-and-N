using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistemaCorporativo.TO.Viatura
{
    class Viatura
    {
        //Atributos 
        private string fabricantemodelo;
        private string placa;
        private string chassi;

        //métodos getters e setters

        public string getFabricanteModelo()
        {
            return this.fabricantemodelo;
        }
        public void setFabricanteModelo(string fabricantemodelo)
        {
            this.fabricantemodelo = fabricantemodelo;
        }

        public string getPlaca()
        {
            return this.placa;
        }
        public void setPlaca(string placa)
        {
            this.placa = placa;
        }

        public string getChassi()
        {
            return this.chassi;
        }
        public void setChassi(string chassi)
        {
            this.chassi = chassi;
        }

    }
}
