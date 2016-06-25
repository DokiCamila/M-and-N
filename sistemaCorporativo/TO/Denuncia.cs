using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistemaCorporativo.TO.Denuncia
{
    class Denuncia
    {
        //Atributos 
        private string nome;
        private string email;
        private string tipoDocumento;
        private string numeroDocumento;
        private string emissor;
        private string cpf;
        private string cep;
        private string logradouro;
        private string numero;
        private string complemento;
        private string bairro;
        private string cidade;
        private string estado;
        private string telefone;
        private string celular;
        private string denuncia;

        //Métodos Getters e Setters


        public string getNome()
        {
            return this.nome;
        }
        public void setNome(string nome)
        {
            this.nome = nome;
        }

        public string getEmail()
        {
            return this.email;
        }
        public void setEmail(string email)
        {
            this.email = email;
        }

        public string getTipoDocumento()
        {
            return this.tipoDocumento;
        }
        public void setTipoDocumento(string tipoDocumento)
        {
            this.tipoDocumento = tipoDocumento;
        }

        public string getNumeroDocumento()
        {
            return this.numeroDocumento;
        }
        public void setNumeroDocumento(string numeroDocumento)
        {
            this.numeroDocumento =  numeroDocumento;
        }

        public string getEmissor()
        {
            return this.emissor;
        }
        public void setEmissor(string emissor)
        {
            this.emissor = emissor;
        }

        public string getCpf()
        {
            return this.cpf;
        }
        public void setCpf(string cpf)
        {
            this.cpf = cpf;
        }

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

        public string getEstado()
        {
            return this.estado;
        }
        public void setEstado(string estado)
        {
            this.estado = estado;
        }

        public string getTelefone()
        {
            return this.telefone;
        }
        public void setTelefone(string telefone)
        {
            this.telefone = telefone;
        }

        public string getCelular()
        {
            return this.celular;
        }
        public void setCelular(string celular)
        {
            this.celular = celular;
        }

        public string getDenuncia()
        {
            return this.denuncia;
        }
        public void setDenuncia(string denuncia)
        {
            this.denuncia = denuncia;
        }



    }
}
