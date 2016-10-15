using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.Linq;
//using SourceAFIS.Dummy;

namespace ComparacaoDPCI.Simples
{
    /// <summary>
    /// Coleta da <see cref="Fingerprint"/>s pertencente a uma pessoa.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Essa classe é basicamente uma forma de agrupar <see cref="Fingerprint"/>s pertencentes a uma pessoa.
    /// Esse é um recurso muito util quando se há muitas Ids por pessoas, porque
    /// é possível comparar duas <see cref="Pessoa"/>s diretamente em vez da repetições sobre suas <see cref="Fingerprint"/>s.
    /// </para>
    /// <para>
    /// A prop <see cref="Id"/> é um meio simples de ligar a <see cref="Pessoa"/> a sua informação
    /// específica. Se voce precisa de mais flexibilidade, herde da classe <see cref="Pessoa"/> e adicione
    /// campos específicos como necesário.
    /// </para>
    /// <para>
    /// Essa classe foi projetada para ser facil de serializar a fim de ser armazenada em formato binário (BLOB)
    /// na base de dados da aplicação, binário ou xml, ou enviar na rede. Você tbm pode serializar
    /// o conjunto <see cref="Pessoa"/> ou a <see cref="Fingerprint"/>s indidualmente.
    /// </para>
    /// </remarks>
    /// <seealso cref="Fingerprint"/>
    [Serializable]
    public class Pessoa : ICloneable
    {
        /// <summary>
        /// Id atribuído pelo app para a <see cref="Pessoa"/>.
        /// </summary>
        /// <remarks>
        /// <para>
        /// SA não usa essa prop. ele é fornecido pela aplicação como meio facil
        /// de vincular a <see cref="Pessoa"/> de volta para os dados específicos do app. As aplicações podem armazenar qualquer
        /// Id inteiro nesse campo, por exemplo, uma chave do banco de dados ou um index em um array
        /// </para>
        /// <para>
        /// </para>
        /// </remarks>
        [XmlAttribute]
        public int Id { get; set; }

        List<Fingerprint> FingerprintList = new List<Fingerprint>();

        /// <summary>
        /// Lista de <see cref="Fingerprint"/>s pertencentes a <see cref="Pessoa"/>.
        /// </summary>
        /// <remarks>
        /// Essa coleta é inicialmente vazia. Adicione <see cref="Fingerprint"/> aqui.
        /// Voce tambem pode atribuir toda a coleta 
        /// </remarks>
        public List<Fingerprint> Fingerprints
        {
            get { return FingerprintList; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException();
                FingerprintList = value;
            }
        }

        /// <summary>
        /// Criar <see cref="Pessoa"/> vazia.
        /// </summary>
        public Pessoa()
        {
        }

        /// <summary>
        /// Criar nova <see cref="Pessoa"/> e inicializa-la com
        /// uma lista de  <see cref="Fingerprint"/>s.
        /// </summary>
        /// <param name="fingerprints"><see cref="Fingerprint"/> Para adicionar a nova <see cref="Pessoa"/>.</param>
        public Pessoa(params Fingerprint[] fingerprints)
        {
            Fingerprints = fingerprints.ToList();
        }

        /// <summary>
        /// Criar uma cópia avançada da<see cref="Pessoa"/>.
        /// </summary>
        /// <returns>Cópia avançada da <see cref="Pessoa"/>.</returns>
        /// <remarks>
        /// Esse método tambem clona todos as <see cref="Fingerprint"/> contidas
        /// nessa <see cref="Pessoa"/>.
        /// </remarks>
        public Pessoa Clonar()
        {
            Pessoa clone = new Pessoa();
            clone.Id = Id;
            foreach (Fingerprint fp in Fingerprints)
                clone.Fingerprints.Add(fp.Clone());
            return clone;
        }

        object ICloneable.Clone() { return Clonar(); }

        internal void CheckForNulls()
        {
            foreach (Fingerprint fp in Fingerprints)
                if (fp == null)
                    throw new ApplicationException("A pessoa contém referências nulas da Impressão digital");
        }
    }
}
