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
    /// Essa classe � basicamente uma forma de agrupar <see cref="Fingerprint"/>s pertencentes a uma pessoa.
    /// Esse � um recurso muito util quando se h� muitas Ids por pessoas, porque
    /// � poss�vel comparar duas <see cref="Pessoa"/>s diretamente em vez da repeti��es sobre suas <see cref="Fingerprint"/>s.
    /// </para>
    /// <para>
    /// A prop <see cref="Id"/> � um meio simples de ligar a <see cref="Pessoa"/> a sua informa��o
    /// espec�fica. Se voce precisa de mais flexibilidade, herde da classe <see cref="Pessoa"/> e adicione
    /// campos espec�ficos como neces�rio.
    /// </para>
    /// <para>
    /// Essa classe foi projetada para ser facil de serializar a fim de ser armazenada em formato bin�rio (BLOB)
    /// na base de dados da aplica��o, bin�rio ou xml, ou enviar na rede. Voc� tbm pode serializar
    /// o conjunto <see cref="Pessoa"/> ou a <see cref="Fingerprint"/>s indidualmente.
    /// </para>
    /// </remarks>
    /// <seealso cref="Fingerprint"/>
    [Serializable]
    public class Pessoa : ICloneable
    {
        /// <summary>
        /// Id atribu�do pelo app para a <see cref="Pessoa"/>.
        /// </summary>
        /// <remarks>
        /// <para>
        /// SA n�o usa essa prop. ele � fornecido pela aplica��o como meio facil
        /// de vincular a <see cref="Pessoa"/> de volta para os dados espec�ficos do app. As aplica��es podem armazenar qualquer
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
        /// Essa coleta � inicialmente vazia. Adicione <see cref="Fingerprint"/> aqui.
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
        /// Criar uma c�pia avan�ada da<see cref="Pessoa"/>.
        /// </summary>
        /// <returns>C�pia avan�ada da <see cref="Pessoa"/>.</returns>
        /// <remarks>
        /// Esse m�todo tambem clona todos as <see cref="Fingerprint"/> contidas
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
                    throw new ApplicationException("A pessoa cont�m refer�ncias nulas da Impress�o digital");
        }
    }
}
