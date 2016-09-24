using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistemaCorporativo.TO.Unidade
{
    class Unidade
    {
        //Atributos 
        private string unidade;
        private string municipio;


        //métodos getters e setters
        public string getUnidade()
        {
            return this.unidade;
        }
        public void setUnidade(string unidade)
        {
            this.unidade = unidade;
        }

        public string getMunicipio()
        {
            return this.municipio;
        }
        public void setMunicipio(string municipio)
        {
            this.municipio = municipio;
        }
    }
}
