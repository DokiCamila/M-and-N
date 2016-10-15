using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
#if !COMPACT_FRAMEWORK
using System.Threading.Tasks;
#endif
using ComparacaoDPCI.Geral;
//using SourceAFIS.Dummy;
using ComparacaoDPCI.Extracao;
using ComparacaoDPCI.Templates;
using ComparacaoDPCI.Comparacao;

namespace ComparacaoDPCI.Simples
{
    /// <summary>
    /// M�todos e configura�es da ferramenta de compara��o de Ids
    /// </summary>
    /// <remarks>
    /// <para>
    /// Depois de definir as propriedades (especialmente <see cref="Threshold"/>), a aplica��o
    /// pode chamar um dos tr�s m�todos principais (<see cref="Extrair"/>, <see cref="Verificar"/>, <see cref="Identificar"/>)
    /// para realizar a extra��o do template e a compara��o de Ids
    /// </para>
    /// <para>
    /// <see cref="Engine"/> Os objetos s�o 'thread-safe'. <see cref="Engine"/> � leve,
    /// mas a aplica��o � for�ada a armazenar de qualquer maneira no cache instancias AfisEngine. Todo
    /// <see cref="Engine"/>m�todo ultiliza multiplos n�cleos. Aplica��es
    /// que desejam executar v�rios m�todos de<see cref="Engine"/> em paralelo deve
    /// criar multiplos <see cref="Engine"/> objetos, um por Thread talvez.
    /// </para>
    /// </remarks>
    public class Engine
    {
        int DpiValue = 500;
        /// <summary>
        /// Get/set configura��es DPI.
        /// </summary>
        /// <value>
        /// DPI da imagem submetida a extra��o de template. Padr�o: 500
        /// </value>
        /// <remarks>
        /// <para>
        /// DPI leitores �pticos de Ids = 500. 
        /// Para outros tipos de leitores assim como para leitores de alta resolu��o, precisaria mudar essa propriedade
        /// de acordo com a capacidade do leitor. Esse valor � usado apenas durante extra��o de templates
        /// (<see cref="Extrair"/>). A compara��o n�o � afetada, porque o processo de extra��o muda todos os
        /// templates para 500dpi altomaticamente.
        /// </para>
        /// <para>
        /// A configura��o do DPI faz o extrator ajustar os seus parametros para o DPI.
        /// Ajuda com a precis�o. O DPI correto tamb�m faz a compara��o da IDs vindo de
        /// outros leitores. Quando comparado IDs de crian�as, � algumas vezes util
        /// para burlar o extrator com a configura��o de DPI menor para lidar com pequenas cristas.
        /// </para>
        /// </remarks>
        /// <seealso cref="Extrair"/>
        public int Dpi
        {
            get { lock (this) return DpiValue; }
            set
            {
                lock (this)
                {
                    if (value < 100 || value > 5000)
                        throw new ArgumentOutOfRangeException();
                    DpiValue = value;
                }
            }
        }
        float ThresholdValue = 12;
        /// <summary>
        /// Get/set threshold(limiar)[N�O � O THRESHOLD DE PROCESSAMENTO DE IMAGENS] de similaridade
        /// </summary>
        /// <value>
        /// threshold de similaridade para criar decis�es de compara��o/incompara��o.
        /// O valor padr�o � definido como 12
        /// </value>
        /// <remarks>
        /// <para>
        /// O algoritmo de compara��o gera um score de similaridade que � uma medida(em double) de semelhan�a
        /// entre duas Ids. As aplica��es por�m precisam limpar as decis�es de compara��o/incompara��o.
        /// <see cref="Threshold"/> � usado para setar um limiar de similaridade decis�es de compara��o/incompara��o.
        /// Indice de similaridade igual ou superior<see cref="Threshold"/> � considerado como achado. Um score abaixo �
        /// n�o foi achado. Essa prop � usada pelos m�todos <see cref="Verificar"/> e <see cref="Identificar"/> para fazer decis�es de compara��o.
        /// </para>
        /// <para>
        /// <see cref="Threshold"/> especi�fico � especifico do Aplicativo. O usuario  deve ajustar essa prop
        /// para analizar a diferen�a em leitores de Ids, popula��es, e requerimento da aplica��o.
        /// Come�ar com um threshold padr�o. Se houver muitos falsos positivos (SourceAFIS
        /// informa o match para Ids de duas pessoas diferentes), aumente o  <see cref="Threshold"/>.
        /// Se houver rejei��es falsas (SourceAFIS informa nenhum match para duas Ids
        /// da mesma pessoa), diminua o <see cref="Threshold"/>. Toda aplica��o normalmente chega
        /// a alguns equilibrios razoaveis entre TFP (taxa de falso positivo) and TRF (taxa de rejei��es falsas).
        /// </para>
        /// </remarks>
        /// <seealso cref="Verificar"/>
        /// <seealso cref="Identificar"/>
        public float Threshold
        {
            get { lock (this) return ThresholdValue; }
            set
            {
                lock (this)
                {
                    if (value < 0)
                        throw new ArgumentOutOfRangeException();
                    ThresholdValue = value;
                }
            }
        }
        int MinMatchesValue = 1;
        /// <summary>
        /// Get/set o valor min�mo de IDs que devem corresponder ao match, afim de que um 'person' possa corresponder.
        /// </summary>
        /// <value>
        /// Numeros de Ids que devem corresponder durante a compara��o de varios dedos
        /// Valor padr�o = 1 (person corresponde se alguns desses Matchs de Ids).
        /// </value>
        /// <remarks>
        /// <para>
        /// Quando h� multipas <see cref="Fingerprint"/>s por <see cref="Pessoa"/>, SourceAFIS compara
        /// toda probe <see cref="Fingerprint"/> para todo candidato <see cref="Fingerprint"/> e pega o melhor
        /// match, aquele com maior score. Esse comportamento
        /// melhora TRF (taxa de rejei��es falsas), porque a baixa pontua��o de similaridade � causada
        /// por impres�es danificadas que s�o ignoradas. Isso acontece quando <see cref="MinMatches"/> � 1 (padr�o).
        /// </para>
        /// <para>
        /// Quando <see cref="MinMatches"/> � 2 ou mais, SourceAFIS compara toda probabilidade <see cref="Fingerprint"/>
        /// para todo candidato <see cref="Fingerprint"/> e registra a pontua��o para cada compara��o. em seguida ele classifica
        /// pontua��es parciais recolhidas em ordem decrescente e pega a pntua��o que esta na posi��o especificada pela propriedade
        /// <see cref="MinMatches"/>, e.g. segundo score se <see cref="MinMatches"/> � 2, terceiro score
        /// se <see cref="MinMatches"/> � 3, etc. Quando combinado com <see cref="Threshold"/>, essa prop
        /// especifica quantas pontua��es parciais devem estar acima do <see cref="Threshold"/> em ordem para
        /// toda <see cref="Pessoa"/> ao match. Como um caso especial, quando existe poucas pontua��es parciais
        /// (menos que o valor de <see cref="MinMatches"/>), SourceAFIS pega o score mais baixo
        /// </para>
        /// <para>
        /// <see cref="MinMatches"/> � util em alguns casos raros onde existe um risco significativo que
        /// algumas Ids podem corresponder aleatoriamente com o score elevado devido a u template defeituoso ou alguma rara
        /// falha no match. Nesses casos, <see cref="MinMatches"/> pode melhorar a TFP
        /// Essa pratica nao � recomendada. Pessoas buscando maneiras para melhorar a TFP
        /// faria de tudo para melhorar <see cref="Threshold"/>. <see cref="Threshold"/> pode ser
        /// pode ser elevada com seguran�a a n�veis onde TFP = zero tanto quanto as IDs s�o de boa qualidade.
        /// </para>
        /// </remarks>
        /// <seealso cref="Verificar"/>
        /// <seealso cref="Identificar"/>
        /// <seealso cref="Threshold"/>
        public int MinMatches
        {
            get { lock (this) return MinMatchesValue; }
            set
            {
                lock (this)
                {
                    if (value <= 0)
                        throw new ArgumentOutOfRangeException();
                    MinMatchesValue = value;
                }
            }
        }

        Extrator Extractor = new Extrator();
        ParallelMatcher Matcher = new ParallelMatcher();

        /// <summary>
        /// Criar nova AfisEngine
        /// </summary>
        public Engine()
        {
        }

        /// <summary>
        /// Extrair template(s) de Ids para serem usados durante matching
        /// </summary>
        /// <param name="person">objeto Person para usar na extra��o do template.</param>
        /// <remarks>
        /// <para>
        /// O m�todo <see cref="Extrair"/> pega a <see cref="Fingerprint.Image"/> de todos <see cref="Fingerprint"/>
        /// relacionado a <paramref name="person"/> e controi o template que � guardado na propriedade
        /// <see cref="Fingerprint.Template"/> da respectiva <see cref="Fingerprint"/>. Esse passo deve
        /// ser realizado antes da  <see cref="Pessoa"/> ser usado na  <see cref="Verificar"/> ou no m�todo <see cref="Identificar"/>,
        /// pq o m�todo � feito em templates, n�o em imagens
        /// </para>
        /// <para>
        /// A imagem da Id pode ser descartada depois da extra��o, mas � recomendado 
        /// mante-la en caso do <see cref="Fingerprint.Template"/> precisar ser gerado, atualizado ou qualquer outro motivo novamente.
        /// </para>
        /// </remarks>
        /// <seealso cref="Dpi"/>
        public void Extrair(Pessoa person)
        {
            lock (this)
            {
                foreach (Fingerprint fp in person.Fingerprints)
                {
                    TemplateBuilder builder = Extractor.Extract(fp.Image, Dpi);
                    fp.Decoded = new SerializedFormat().Export(builder);
                }
            }
        }

        /// [1:1]
        /// <summary>
        /// Calcular o score de similaridade entre duas <see cref="Pessoa"/>s.
        /// </summary>
        /// <param name="probe">Primeira das duas pessoas para comparar</param>
        /// <param name="candidate">Segunda das duas pessoas para comparar</param>
        /// <returns> o �ndice de Similaridade indicando semelhan�a entre as duas pessoas ou 0 if se n�o existir match.</returns>
        /// <remarks>
        /// <para>
        /// O m�todo <see cref="Verificar"/> compara duas <see cref="Pessoa"/>s, <see cref="Fingerprint"/> pela <see cref="Fingerprint"/>, e retorna
        /// um [float] indice de similaridade que indica o grau de semelhan�a entre
        /// as duas <see cref="Pessoa"/>s. Se esse score ficar abaixo do <see cref="Threshold"/>, o m�todo <see cref="Verificar"/> retorna zero matchs.
        /// </para>
        /// <para>
        /// <see cref="Pessoa"/>s que passam por esse m�todo devem ter o <see cref="Fingerprint.Template"/> valido
        /// para toda <see cref="Fingerprint"/>, i.e. devem ser passados pelo m�todo <see cref="Extrair"/> antes.
        /// </para>
        /// </remarks>
        /// <seealso cref="Threshold"/>
        /// <seealso cref="MinMatches"/>
        /// <seealso cref="Identificar"/>
        public float Verificar(Pessoa probe, Pessoa candidate)
        {
            lock (this)
            {
                probe.CheckForNulls();
                candidate.CheckForNulls();
                BestMatchSkipper collector = new BestMatchSkipper(1, MinMatches - 1);
                Parallel.ForEach(probe.Fingerprints, probeFp =>
                    {
                        var candidateTemplates = (from candidateFp in candidate.Fingerprints
                                                  where IsCompatibleFinger(probeFp.Dedo, candidateFp.Dedo)
                                                  select candidateFp.Decoded).ToList();

                        ParallelMatcher.PreparedProbe probeIndex = Matcher.Prepare(probeFp.Decoded);
                        float[] scores = Matcher.Match(probeIndex, candidateTemplates);

                        lock (collector)
                            foreach (float score in scores)
                                collector.AddScore(0, score);
                    });

                return ApplyThreshold(collector.GetSkipScore(0));
            }
        }

        /// [1:N]
        /// <summary>
        /// Compara uma <see cref="Pessoa"/> contra um conjunto de outras <see cref="Pessoa"/>s e retorna o(s) melhore(s) match(s)
        /// </summary>
        /// <param name="probe">pessoa para procurar na base de dados</param>
        /// <param name="candidates">base de dados de pessoas a ser pesquisado</param>
        /// <returns>Todo objeto <see cref="Pessoa"/> correspondente na base de dados ou vazio se
        /// n�o existir match. Os resultados s�o classificados por pontua��o em ordem decrescente. Se voce precisar apenas do melhor match,
        /// chame o m�todo <see cref="Enumerable.FirstOrDefault{T}(IEnumerable{T})"/> na base de dados retornada.</returns>
        /// <remarks>
        /// <para>
        /// Compare o probe<see cref="Pessoa"/> para todos os candidatos <see cref="Pessoa"/>s e retorne o candidato mais similar.
        /// Chamando <see cref="Identificar"/>� quase a mesma coisa que chamar o <see cref="Verificar"/> em um loop
        /// exceto que o <see cref="Identificar"/> � bem mais rapido que o loop do <see cref="Verificar"/>.
        /// Se n�o houver candidato ou pontua��o igual ou maior que o <see cref="Threshold"/>, <see cref="Identificar"/> retorna
        /// uma base de dados vazia.
        /// </para>
        /// <para>
        /// A maioria das aplica��es precisam apenas do melhor match que pode ser obtido chamando o m�todo
        /// <see cref="Enumerable.FirstOrDefault{T}(IEnumerable{T})"/> na base de dados retornada.
        /// Pontua��o correspondente para cada<see cref="Pessoa"/> retornado pode ser obtido chamando o
        /// <see cref="Verificar"/> no probe <see cref="Pessoa"/> e o matching <see cref="Pessoa"/>.
        /// </para>
        /// <para>
        /// <see cref="Pessoa"/>s que passar por esse m�todo deve ter o <see cref="Fingerprint.Template"/> valido.
        /// para cada <see cref="Fingerprint"/>, i.e. eles precisam ter passado pelo m�todo <see cref="Extrair"/> antes.
        /// </para>
        /// </remarks>
        /// <seealso cref="Threshold"/>
        /// <seealso cref="MinMatches"/>
        /// <seealso cref="Verificar"/>
        public IEnumerable<Pessoa> Identificar(Pessoa probe, IEnumerable<Pessoa> candidates)
        {
            probe.CheckForNulls();
            Pessoa[] candidateArray = candidates.ToArray();
            BestMatchSkipper.PersonsSkipScore[] results;
            lock (this)
            {
                BestMatchSkipper collector = new BestMatchSkipper(candidateArray.Length, MinMatches - 1);
                Parallel.ForEach(probe.Fingerprints, probeFp =>
                    {
                        List<int> personsByFingerprint = new List<int>();
                        List<Template> candidateTemplates = FlattenHierarchy(candidateArray, probeFp.Dedo, out personsByFingerprint);

                        ParallelMatcher.PreparedProbe probeIndex = Matcher.Prepare(probeFp.Decoded);
                        float[] scores = Matcher.Match(probeIndex, candidateTemplates);

                        lock (collector)
                            for (int i = 0; i < scores.Length; ++i)
                                collector.AddScore(personsByFingerprint[i], scores[i]);
                    });
                results = collector.GetSortedScores();
            }
            return GetMatchingCandidates(candidateArray, results);
        }

        IEnumerable<Pessoa> GetMatchingCandidates(Pessoa[] candidateArray, BestMatchSkipper.PersonsSkipScore[] results)
        {
            foreach (var match in results)
                if (match.Score >= Threshold)
                    yield return candidateArray[match.Person];
        }

        bool IsCompatibleFinger(Dedo first, Dedo second)
        {
            return first == second || first == Dedo.Any || second == Dedo.Any;
        }

        List<Template> FlattenHierarchy(Pessoa[] persons, Dedo finger, out List<int> personIndexes)
        {
            List<Template> templates = new List<Template>();
            personIndexes = new List<int>();
            for (int personIndex = 0; personIndex < persons.Length; ++personIndex)
            {
                Pessoa person = persons[personIndex];
                person.CheckForNulls();
                for (int i = 0; i < person.Fingerprints.Count; ++i)
                {
                    Fingerprint fingerprint = person.Fingerprints[i];
                    if (IsCompatibleFinger(finger, fingerprint.Dedo))
                    {
                        templates.Add(fingerprint.Decoded);
                        personIndexes.Add(personIndex);
                    }
                }
            }
            return templates;
        }

        float ApplyThreshold(float score)
        {
            return score >= Threshold ? score : 0;
        }
    }
}
