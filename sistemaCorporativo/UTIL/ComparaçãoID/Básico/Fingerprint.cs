using System;
using System.Collections.Generic;
using System.Text;
#if !COMPACT_FRAMEWORK
using System.Drawing;
#endif
using System.Xml.Serialization;
using System.IO;
#if !MONO
//using System.Windows.Media.Imaging;
#endif
using System.Xml.Linq;
using ComparacaoDPCI.Geral;
//using SourceAFIS.Dummy;
using ComparacaoDPCI.Templates;

namespace ComparacaoDPCI.Simples
{
    /// <summary>
    /// Coleta de informa��es relacionadas a impress�o digital
    /// </summary>
    /// <remarks>
    /// <para>
    /// Essa classe contem informa��es b�sicas (<see cref="Image"/>, <see cref="Template"/>) sobre a Id que 
    /// � usada pelo SourceAFIS para realizar a extra��o do template e compara��o de Ids
    /// se voce precisa anexar informa��es especificas para o objeto <see cref="Fingerprint"/>,
    /// herdar dessa classe e adicionar campos necessarios como objetos da <see cref="Fingerprint"/> pode ser
    /// agrupado nos objetos <see cref="Pessoa"/>.
    /// </para>
    /// <para>
    /// Essa classe foi projetada  para ser facil de serializar ao mesmo tempo que possa ser guardada em formato bin�rio (BLOB)
    /// no banco de dados da aplica��o,bin�rio ou xml, ou enviar atrav�s da internet. Voc� pode serializar
    /// o objeto inteiro ou apenas propriedades individuais. Tamb�m pode definir algumas props como <see langword="null"/>
    /// para excuir eles da serializa��o
    /// </para>
    /// </remarks>
    /// <seealso cref="Pessoa"/>
    [Serializable]
    public class Fingerprint : ICloneable
    {
        /// <summary>
        /// Criar objeto <see cref="Fingerprint"/> vazio.
        /// </summary>
        public Fingerprint() { }

        byte[,] ImageData;

        /// <summary>
        /// Imagem da Id
        /// </summary>
        /// <value>
        /// A imagem da Id foi usada para gerar o <see cref="Template"/> ou outra imagem
        /// inserida ap�s a extra��o. Essa prop � <see langword="null"/> PADR�O.
        /// </value>
        /// <remarks>
        /// <para>
        /// Essa � a imagem da Id. Essa prop dever ser definida antes de chamar o <see cref="AfisEngine.Extrair"/>
        /// em ordem para gerar<see cref="Template"/> validos. Uma vez que o <see cref="Template"/>seja gerado, a prop <see cref="Image"/> tem apenas
        /// um sentido informativo e pode ser definido como <see langword="null"/> para poupar espa�o. No entanto � recomendado que
        /// mantenha a imagem original caso seja necess�rio gerar novos  <see cref="Template"/> futuramente.
        /// </para>
        /// <para>
        /// O formato dessa imagem � um array simples de <see langword="byte"/>s. todo byte
        /// representa tons de cinza do preto (0) ao branco (255). Quando gerado o array em 2D, o eixo Y
        /// vai primeiro, o X vai em segundo, e.g. <c>Image[y, x]</c>. para converter para/de objeto<see cref="Bitmap"/>,
        /// use a prop <see cref="AsBitmap"/>. Para coverter para/de objeto<see cref="BitmapSource"/>,
        /// use a prop <see cref="AsBitmapSource"/.
        /// </para>
        /// <para>
        /// Acessores da classe n�o clonam a imahem. para evitar o compartilhamento indesejado de array <see langword="byte"/>
        /// chame o <see cref="ICloneable.Clone"/> na <see cref="Image"/>.
        /// </para>
        /// </remarks>
        /// <seealso cref="Template"/>
        /// <seealso cref="AsBitmap"/>
        /// <seealso cref="AsBitmapSource"/>
        /// <seealso cref="AsImageData"/>
        /// <seealso cref="AfisEngine.Extrair"/>
        [XmlIgnore]
        public byte[,] Image
        {
            get { return ImageData; }
            set
            {
                if (value == null)
                    ImageData = null;
                else
                {
                    if (value.GetLength(0) < 100 || value.GetLength(1) < 100)
                        throw new ApplicationException("Imagem da Impress�o Digital � muito pequena.");
                    ImageData = value;
                }
            }
        }

        /// <summary>
        /// Imagem da Id como um array em 2d
        /// </summary>
        /// <value>
        /// Imagem da Id da propriedade<see cref="Image"/> convertida para 
        /// (byte array uni-dimensional) ou <see langword="null"/> se <see cref="Image"/>
        /// � <see langword="null"/>.
        /// </value>
        /// <seealso cref="Image"/>
        /// <seealso cref="AsBitmap"/>
        /// <seealso cref="AsBitmapSource"/>
        /// <seealso cref="Template"/>
        /// <seealso cref="Engine.Extrair"/>
        public byte[] AsImageData
        {
            get
            {
                byte[,] image = Image;
                if (image == null)
                    return null;
                else
                {
                    int height = image.GetLength(0);
                    int width = image.GetLength(1);

                    byte[] packed = new byte[8 + image.Length];
                    BitConverter.GetBytes(height).CopyTo(packed, 0);
                    BitConverter.GetBytes(width).CopyTo(packed, 4);

                    for (int y = 0; y < height; ++y)
                        for (int x = 0; x < width; ++x)
                            packed[8 + y * width + x] = image[y, x];

                    return packed;
                }
            }
            set
            {
                if (value == null)
                    Image = null;
                else
                {
                    if (value.Length <= 8)
                        throw new ApplicationException("Matriz da imagem � muito curta.");
                    
                    int height = BitConverter.ToInt32(value, 0);
                    int width = BitConverter.ToInt32(value, 4);

                    if (height <= 0 || width <= 0)
                        throw new ApplicationException("Dimens�es da imagem inv�lidos na matriz da imagem.");
                    if (8 + width * height != value.Length)
                        throw new ApplicationException("Comprimento incorreto da matriz da imagem.");

                    byte[,] unpacked = new byte[height, width];
                    for (int y = 0; y < height; ++y)
                        for (int x = 0; x < width; ++x)
                            unpacked[y, x] = value[8 + y * width + x];

                    Image = unpacked;
                }
            }
        }

#if !COMPACT_FRAMEWORK && !MONO
        /// <summary>
        /// Imagem da Id como objeto <see cref="BitmapSource"/>.
        /// </summary>
        /// <value>
        /// Imagem da Id da prop <see cref="Image"/> convertida para objeto <see cref="BitmapSource"/>
        /// ou <see langword="null"/> se <see cref="Image"/> � <see langword="null"/>.
        /// </value>
        /// <remarks>
        /// Propriedades para aplica��es em WPF
        /// </remarks>
        /// <seealso cref="Image"/>
        /// <seealso cref="AsImageData"/>
        /// <seealso cref="AsBitmap"/>
        /// <seealso cref="Template"/>
        /// <seealso cref="Engine.Extrair"/>
        /*
        [XmlIgnore]
        public BitmapSource AsBitmapSource
        {
            get { return Image != null ? WpfIO.GetBitmapSource(Image) : null; }
            set { Image = value != null ? WpfIO.GetPixels(value) : null; }
        }
         */

        /// <summary>
        /// Imagem da Id como objeto <see cref="Bitmap"/>.
        /// </summary>
        /// <value>
        /// Imagem da Id da prop <see cref="Image"/> convertida para objeto<see cref="Bitmap"/>
        /// ou <see langword="null"/> se <see cref="Image"/> � <see langword="null"/>.
        /// </value>
        /// <remarks>
        /// Usar essa propriedades em aplica��es em Windows Forms
        /// </remarks>
        /// <seealso cref="Image"/>
        /// <seealso cref="AsImageData"/>
        /// <seealso cref="AsBitmapSource"/>
        /// <seealso cref="Template"/>
        /// <seealso cref="Engine.Extrair"/>
        [XmlIgnore]
        public Bitmap AsBitmap
        {
            get { return Image != null ? GdiIO.GetBitmap(Image) : null; }
            set { Image = value != null ? GdiIO.GetPixels(value) : null; }
        }
#endif

        static readonly CompactFormat CompactFormat = new CompactFormat();
        static readonly SerializedFormat SerializedFormat = new SerializedFormat();
        static readonly IsoFormat IsoFormat = new IsoFormat();
        static readonly XmlFormat XmlFormat = new XmlFormat();

        /// <summary>
        /// Template da Impress�o Digital
        /// </summary>
        /// <value>
        /// Template da Id gerado pelo <see cref="Engine.Extrair"/> ou outro modelo atr�buido
        /// por exemplo depois da Serializa��o. Essa prop � <see langword="null"/> por padr�o.
        /// </value>
        /// <remarks>
        /// <para>
        /// O template da Id � um modelo abstrato of da Id que � serializada
        /// em um formato bin�rio compactado (at� KB). Templates s�o melhores que a imagem da Id,
        /// porque exigem menos espa�o e eles s�o muito faceis para comparar que as imagens. Para gerar o
        /// <see cref="Template"/>, passe o objeto <see cref="Fingerprint"/> com uma <see cref="Image"/> valida para <see cref="Engine.Extrair"/>.
        /// <see cref="Template"/> � exigido pelo <see cref="Engine.Verificar"/> e <see cref="Engine.Identificar"/>.
        /// </para>
        /// <para>
        /// O formato dos templates podem mudar em algumas vers�es do SourceAfis
        /// � recomendado manter a <see cref="Image"/> original para ser capaz de
        /// gerar o <see cref="Template"/>.
        /// </para>
        /// <para>
        /// Se voc� precisa acessar a estrutura interna do Template, use
        /// <see cref="ComparacaoDPCI.Templates.CompactFormat"/> para converter para
        /// <see cref="ComparacaoDPCI.Templates.TemplateBuilder"/>.
        /// </para>
        /// </remarks>
        /// <seealso cref="Image"/>
        /// <seealso cref="Engine.Extrair"/>
        /// <seealso cref="AsIsoTemplate"/>
        /// <seealso cref="ComparacaoDPCI.Templates.CompactFormat"/>
        /// <seealso cref="ComparacaoDPCI.Templates.TemplateBuilder"/>
        public byte[] Template
        {
            get { return Decoded != null ? CompactFormat.Export(SerializedFormat.Import(Decoded)) : null; }
            set { Decoded = value != null ? SerializedFormat.Export(CompactFormat.Import(value)) : null; }
        }

        /// <summary>
        /// Template da impress�o digital em formato ISO avan�ado
        /// </summary>
        /// <value>
        /// Valor do <see cref="Template"/> convertido para formato ISO avan�ado ISO/IEC 19794-2 (2005).
        /// Essa propriedade pe <see langword="null"/> se <see cref="Template"/> � <see langword="null"/>.
        /// </value>
        /// <remarks>
        /// <para>
        /// Use essa prop para a troca bidireccional de modelos de Ids com outros sistemas
        /// biom�tricos. Em geral o SourceAfis, usa a <see cref="Template"/> propriedade que 
        /// cont�m o template nativo para aperfei�oa-lo para melhor precis�o e desempenho.
        /// </para>
        /// <para>
        /// SourceAfis contem implementa��o parcial do ISO/IEC 19794-2 avan�ado.
        /// Templates ISO Multi-Ids devem ser divididos em Ids individuais antes 
        /// de serem usados. O valor da prop <see cref="Fingerprint.Dedo"/> n�o �
        /// automaticamente guardada no template ISO. Ela precisa ser decodificada (separadamente).
        /// </para>
        /// </remarks>
        /// <seealso cref="Template"/>
        /// <seealso cref="Engine.Extrair"/>
        /// <seealso cref="ComparacaoDPCI.Templates.IsoFormat"/>
        /// <seealso cref="ComparacaoDPCI.Templates.TemplateBuilder"/>
        [XmlIgnore]
        public byte[] AsIsoTemplate
        {
            get { return Decoded != null ? IsoFormat.Export(SerializedFormat.Import(Decoded)) : null; }
            set { Decoded = value != null ? SerializedFormat.Export(IsoFormat.Import(value)) : null; }
        }

        /// <summary>
        /// Template da Impress�o Digital em formato XML(legivel)
        /// </summary>
        /// <value>
        /// O valor do <see cref="Template"/> convertido para formato XML.
        /// Essa prop � <see langword="null"/> se <see cref="Template"/> � <see langword="null"/>.
        /// </value>
        /// <remarks>
        /// Usar um formato XML onde o formato de dado � limpo � melhor do que uma codifica��o compacta e rapida
        /// Modelos XML s�o adequados para troca de dados em XML, codificando varias Ids junto
        /// com o acompanhamento de dados em XML, e para fins de debug e log.
        /// </remarks>
        /// <seealso cref="Template"/>
        /// <seealso cref="Engine.Extrair"/>
        /// <seealso cref="ComparacaoDPCI.Templates.XmlFormat"/>
        /// <seealso cref="ComparacaoDPCI.Templates.TemplateBuilder"/>
        [XmlIgnore]
        public XElement AsXmlTemplate
        {
            get { return Decoded != null ? XmlFormat.Export(SerializedFormat.Import(Decoded)) : null; }
            set { Decoded = value != null ? SerializedFormat.Export(XmlFormat.Import(value)) : null; }
        }

        Dedo FingerPosition;

        /// <summary>
        /// Posi��o do dedo na m�o
        /// </summary>
        /// <value>
        /// Dedo (do Polegar ao Minimo) e m�os (direita e esquerda) que foram usadas para criar essa Id.
        /// Valor padr�o = <see cref="F:SourceAFIS.Simple.Finger.Any"/> ou seja, posi��o n�o especificada.
        /// </value>
        /// <remarks>
        /// A posi��o do dedo � usada para acelerar a compara��o pulando pares de Id.
        /// </remarks>
        /// <seealso cref="ComparacaoDPCI.Simples.Dedo"/>
        [XmlAttribute]
        public Dedo Dedo
        {
            get { return FingerPosition; }
            set
            {
                if (!Enum.IsDefined(typeof(Dedo), value))
                    throw new ApplicationException("Posi��o de dedo inv�lida.");
                FingerPosition = value;
            }
        }

#if !COMPACT_FRAMEWORK
        internal
#else
        public
#endif
        Template Decoded;

        /// <summary>
        /// Criar uma c�pia acan�ada da <see cref="Fingerprint"/>.
        /// </summary>
        /// <returns>c�pia avan�ada dessa <see cref="Fingerprint"/>.</returns>
        public Fingerprint Clone()
        {
            Fingerprint clone = new Fingerprint();
            clone.Image = Image != null ? (byte[,])Image.Clone() : null;
            clone.Decoded = Decoded != null ? Decoded.Clone() : null;
            clone.Dedo = Dedo;
            return clone;
        }

        object ICloneable.Clone() { return Clone(); }
    }
}
