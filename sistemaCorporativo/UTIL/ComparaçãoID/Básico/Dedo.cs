using System;
using System.Collections.Generic;
using System.Text;
//using SourceAFIS.Dummy;

namespace ComparacaoDPCI.Simples
{
    /// <summary>
    /// Posição do dedo na mão
    /// </summary>
    /// <remarks>
    /// <para>
    /// Finger position é usado para acelerar o matching pulando pares de impressoes digitais
    /// que não podem ser a mesma com posições incompatíveis. O sourceAfis retornará zero
    /// no indice de similaridade para pares de Ids incompatíveis
    /// </para>
    /// <para>
    /// Esse recurso é opcional. pode ser desativado usando a posição de dedo <see cref="Any"/>
    /// que é o valor padrão do <see cref="Fingerprint.Dedo"/> para novos objetos <see cref="Fingerprint"/>.
    /// </para>
    /// <para>
    /// Um par de Id compativel consiste de duas Ids com a mesma
    /// posicção do dedo, e.g. <see cref="PolegarDireito"/> apenas corresponde a outro <see cref="PolegarDireito"/>. Alternativamente,
    /// pares de Ids compatíveis também podem ser formadas se uma das posições da Ids
    /// has <see cref="Any"/> finger position, e.g. <see cref="Any"/> can be matched against all other finger
    /// e todas as outras posições de dedos podem ser comparados com <see cref="Any"/>. Duas
    /// Ids com posições <see cref="Any"/> são completamente compatíveis.
    /// </para>
    /// </remarks>
    /// <seealso cref="Fingerprint.Dedo"/>
    [Serializable]
    public enum Dedo
    {
        /// <summary>
        /// Posição de dedo não especificada
        /// </summary>
        Any = 0,
        /// <summary>
        /// Dedo polegar da mão direita
        /// </summary>
        PolegarDireito = 1,
        /// <summary>
        /// Dedo polegar da mão esquerda
        /// </summary>
        PolegarEsquerdo = 2,
        /// <summary>
        /// Dedo indicador da mão direita
        /// </summary>
        IndicadorDireito = 3,
        /// <summary>
        /// Dedo indicador da mão esquerda
        /// </summary>
        IndicadorEsquerdo = 4,
        /// <summary>
        /// Dedo médio da mão direita
        /// </summary>
        MédioDireito = 5,
        /// <summary>
        /// Dedo médio da mão esquerda
        /// </summary>
        MédioEsquerdo = 6,
        /// <summary>
        /// Dedo anular da mão direita
        /// </summary>
        AnularDireito = 7,
        /// <summary>
        /// Dedo anular da mão esquerda
        /// </summary>
        AnularEsquerdo = 8,
        /// <summary>
        /// Dedo Mínimo da mão direita
        /// </summary>
        MinimoDireito = 9,
        /// <summary>
        /// Dedo Mínimo da mão esquerda
        /// </summary>
        MinimoEsquerdo = 10,
    }
}
