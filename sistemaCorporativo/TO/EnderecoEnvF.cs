using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistemaCorporativo.TO.EnvolvidoF
{
    class EnderecoEnvF
    {
        //Atributos 
        private string cep;
        private string logradouro;
        private string numero;
        private string complemento;
        private string bairro;
        private string cidade;
        private string uf;

        //métodos getters e setters
        public string getCep()
        {
            return this.cep;
        }
        public void setCep(string cep)
        {
            this.cep = cep;
        }

        public string getLogradouro()
        {
            return this.logradouro;
        }
        public void setLogradouro(string logradouro)
        {
            this.logradouro = logradouro;
        }

        public string getNumero()
        {
            return this.numero;
        }
        public void setNumero(string numero)
        {
            this.numero = numero;
        }

        public string getComplemento()
        {
            return this.complemento;
        }
        public void setComplemento(string complemento)
        {
            this.complemento = complemento;
        }

        public string getBairro()
        {
            return this.bairro;
        }
        public void setBairro(string bairro)
        {
            this.bairro = bairro;
        }

        public string getCidade()
        {
            return this.cidade;
        }
        public void setCidade(string cidade)
        {
            this.cidade = cidade;
        }

        public string getUf()
        {
            return this.uf;
        }
        public void setUf(string uf)
        {
            this.uf = uf;
        }
    }
}
