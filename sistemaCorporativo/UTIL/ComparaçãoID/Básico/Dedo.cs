using System;
using System.Collections.Generic;
using System.Text;
//using SourceAFIS.Dummy;

namespace ComparacaoDPCI.Simples
{
    /// <summary>
    /// Posi��o do dedo na m�o
    /// </summary>
    /// <remarks>
    /// <para>
    /// Finger position � usado para acelerar o matching pulando pares de impressoes digitais
    /// que n�o podem ser a mesma com posi��es incompat�veis. O sourceAfis retornar� zero
    /// no indice de similaridade para pares de Ids incompat�veis
    /// </para>
    /// <para>
    /// Esse recurso � opcional. pode ser desativado usando a posi��o de dedo <see cref="Any"/>
    /// que � o valor padr�o do <see cref="Fingerprint.Dedo"/> para novos objetos <see cref="Fingerprint"/>.
    /// </para>
    /// <para>
    /// Um par de Id compativel consiste de duas Ids com a mesma
    /// posic��o do dedo, e.g. <see cref="PolegarDireito"/> apenas corresponde a outro <see cref="PolegarDireito"/>. Alternativamente,
    /// pares de Ids compat�veis tamb�m podem ser formadas se uma das posi��es da Ids
    /// has <see cref="Any"/> finger position, e.g. <see cref="Any"/> can be matched against all other finger
    /// e todas as outras posi��es de dedos podem ser comparados com <see cref="Any"/>. Duas
    /// Ids com posi��es <see cref="Any"/> s�o completamente compat�veis.
    /// </para>
    /// </remarks>
    /// <seealso cref="Fingerprint.Dedo"/>
    [Serializable]
    public enum Dedo
    {
        /// <summary>
        /// Posi��o de dedo n�o especificada
        /// </summary>
        Any = 0,
        /// <summary>
        /// Dedo polegar da m�o direita
        /// </summary>
        PolegarDireito = 1,
        /// <summary>
        /// Dedo polegar da m�o esquerda
        /// </summary>
        PolegarEsquerdo = 2,
        /// <summary>
        /// Dedo indicador da m�o direita
        /// </summary>
        IndicadorDireito = 3,
        /// <summary>
        /// Dedo indicador da m�o esquerda
        /// </summary>
        IndicadorEsquerdo = 4,
        /// <summary>
        /// Dedo m�dio da m�o direita
        /// </summary>
        M�dioDireito = 5,
        /// <summary>
        /// Dedo m�dio da m�o esquerda
        /// </summary>
        M�dioEsquerdo = 6,
        /// <summary>
        /// Dedo anular da m�o direita
        /// </summary>
        AnularDireito = 7,
        /// <summary>
        /// Dedo anular da m�o esquerda
        /// </summary>
        AnularEsquerdo = 8,
        /// <summary>
        /// Dedo M�nimo da m�o direita
        /// </summary>
        MinimoDireito = 9,
        /// <summary>
        /// Dedo M�nimo da m�o esquerda
        /// </summary>
        MinimoEsquerdo = 10,
    }
}
