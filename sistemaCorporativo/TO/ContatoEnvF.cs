using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistemaCorporativo.TO.EnvolvidoF
{
    class ContatoEnvF
    {
        //Atributos 
        private string descricao;
        private string tipo;


        //métodos getters e setters
        public string getDescricao()
        {
            return this.descricao;
        }
        public void setDescricao(string descricao)
        {
            this.descricao = descricao;
        }

        public string getTipo()
        {
            return this.tipo;
        }
        public void setTipo(string tipo)
        {
            this.tipo = tipo;
        }
    }
}
