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
    /// Métodos e configuraões da ferramenta de comparação de Ids
    /// </summary>
    /// <remarks>
    /// <para>
    /// Depois de definir as propriedades (especialmente <see cref="Threshold"/>), a aplicação
    /// pode chamar um dos três métodos principais (<see cref="Extrair"/>, <see cref="Verificar"/>, <see cref="Identificar"/>)
    /// para realizar a extração do template e a comparação de Ids
    /// </para>
    /// <para>
    /// <see cref="Engine"/> Os objetos são 'thread-safe'. <see cref="Engine"/> é leve,
    /// mas a aplicação é forçada a armazenar de qualquer maneira no cache instancias AfisEngine. Todo
    /// <see cref="Engine"/>método ultiliza multiplos núcleos. Aplicações
    /// que desejam executar vários métodos de<see cref="Engine"/> em paralelo deve
    /// criar multiplos <see cref="Engine"/> objetos, um por Thread talvez.
    /// </para>
    /// </remarks>
    public class Engine
    {
        int DpiValue = 500;
        /// <summary>
        /// Get/set configurações DPI.
        /// </summary>
        /// <value>
        /// DPI da imagem submetida a extração de template. Padrão: 500
        /// </value>
        /// <remarks>
        /// <para>
        /// DPI leitores ópticos de Ids = 500. 
        /// Para outros tipos de leitores assim como para leitores de alta resolução, precisaria mudar essa propriedade
        /// de acordo com a capacidade do leitor. Esse valor é usado apenas durante extração de templates
        /// (<see cref="Extrair"/>). A comparação não é afetada, porque o processo de extração muda todos os
        /// templates para 500dpi altomaticamente.
        /// </para>
        /// <para>
        /// A configuração do DPI faz o extrator ajustar os seus parametros para o DPI.
        /// Ajuda com a precisão. O DPI correto também faz a comparação da IDs vindo de
        /// outros leitores. Quando comparado IDs de crianças, é algumas vezes util
        /// para burlar o extrator com a configuração de DPI menor para lidar com pequenas cristas.
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
        /// Get/set threshold(limiar)[NÃO É O THRESHOLD DE PROCESSAMENTO DE IMAGENS] de similaridade
        /// </summary>
        /// <value>
        /// threshold de similaridade para criar decisões de comparação/incomparação.
        /// O valor padrão é definido como 12
        /// </value>
        /// <remarks>
        /// <para>
        /// O algoritmo de comparação gera um score de similaridade que é uma medida(em double) de semelhança
        /// entre duas Ids. As aplicações porém precisam limpar as decisões de comparação/incomparação.
        /// <see cref="Threshold"/> é usado para setar um limiar de similaridade decisões de comparação/incomparação.
        /// Indice de similaridade igual ou superior<see cref="Threshold"/> é considerado como achado. Um score abaixo é
        /// não foi achado. Essa prop é usada pelos métodos <see cref="Verificar"/> e <see cref="Identificar"/> para fazer decisões de comparação.
        /// </para>
        /// <para>
        /// <see cref="Threshold"/> especiífico é especifico do Aplicativo. O usuario  deve ajustar essa prop
        /// para analizar a diferença em leitores de Ids, populações, e requerimento da aplicação.
        /// Começar com um threshold padrão. Se houver muitos falsos positivos (SourceAFIS
        /// informa o match para Ids de duas pessoas diferentes), aumente o  <see cref="Threshold"/>.
        /// Se houver rejeições falsas (SourceAFIS informa nenhum match para duas Ids
        /// da mesma pessoa), diminua o <see cref="Threshold"/>. Toda aplicação normalmente chega
        /// a alguns equilibrios razoaveis entre TFP (taxa de falso positivo) and TRF (taxa de rejeições falsas).
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
        /// Get/set o valor minímo de IDs que devem corresponder ao match, afim de que um 'person' possa corresponder.
        /// </summary>
        /// <value>
        /// Numeros de Ids que devem corresponder durante a comparação de varios dedos
        /// Valor padrão = 1 (person corresponde se alguns desses Matchs de Ids).
        /// </value>
        /// <remarks>
        /// <para>
        /// Quando há multipas <see cref="Fingerprint"/>s por <see cref="Pessoa"/>, SourceAFIS compara
        /// toda probe <see cref="Fingerprint"/> para todo candidato <see cref="Fingerprint"/> e pega o melhor
        /// match, aquele com maior score. Esse comportamento
        /// melhora TRF (taxa de rejeições falsas), porque a baixa pontuação de similaridade é causada
        /// por impresões danificadas que são ignoradas. Isso acontece quando <see cref="MinMatches"/> é 1 (padrão).
        /// </para>
        /// <para>
        /// Quando <see cref="MinMatches"/> é 2 ou mais, SourceAFIS compara toda probabilidade <see cref="Fingerprint"/>
        /// para todo candidato <see cref="Fingerprint"/> e registra a pontuação para cada comparação. em seguida ele classifica
        /// pontuações parciais recolhidas em ordem decrescente e pega a pntuação que esta na posição especificada pela propriedade
        /// <see cref="MinMatches"/>, e.g. segundo score se <see cref="MinMatches"/> é 2, terceiro score
        /// se <see cref="MinMatches"/> é 3, etc. Quando combinado com <see cref="Threshold"/>, essa prop
        /// especifica quantas pontuações parciais devem estar acima do <see cref="Threshold"/> em ordem para
        /// toda <see cref="Pessoa"/> ao match. Como um caso especial, quando existe poucas pontuações parciais
        /// (menos que o valor de <see cref="MinMatches"/>), SourceAFIS pega o score mais baixo
        /// </para>
        /// <para>
        /// <see cref="MinMatches"/> é util em alguns casos raros onde existe um risco significativo que
        /// algumas Ids podem corresponder aleatoriamente com o score elevado devido a u template defeituoso ou alguma rara
        /// falha no match. Nesses casos, <see cref="MinMatches"/> pode melhorar a TFP
        /// Essa pratica nao é recomendada. Pessoas buscando maneiras para melhorar a TFP
        /// faria de tudo para melhorar <see cref="Threshold"/>. <see cref="Threshold"/> pode ser
        /// pode ser elevada com segurança a níveis onde TFP = zero tanto quanto as IDs são de boa qualidade.
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
        /// <param name="person">objeto Person para usar na extração do template.</param>
        /// <remarks>
        /// <para>
        /// O método <see cref="Extrair"/> pega a <see cref="Fingerprint.Image"/> de todos <see cref="Fingerprint"/>
        /// relacionado a <paramref name="person"/> e controi o template que é guardado na propriedade
        /// <see cref="Fingerprint.Template"/> da respectiva <see cref="Fingerprint"/>. Esse passo deve
        /// ser realizado antes da  <see cref="Pessoa"/> ser usado na  <see cref="Verificar"/> ou no método <see cref="Identificar"/>,
        /// pq o método é feito em templates, não em imagens
        /// </para>
        /// <para>
        /// A imagem da Id pode ser descartada depois da extração, mas é recomendado 
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
        /// <returns> o índice de Similaridade indicando semelhança entre as duas pessoas ou 0 if se não existir match.</returns>
        /// <remarks>
        /// <para>
        /// O método <see cref="Verificar"/> compara duas <see cref="Pessoa"/>s, <see cref="Fingerprint"/> pela <see cref="Fingerprint"/>, e retorna
        /// um [float] indice de similaridade que indica o grau de semelhança entre
        /// as duas <see cref="Pessoa"/>s. Se esse score ficar abaixo do <see cref="Threshold"/>, o método <see cref="Verificar"/> retorna zero matchs.
        /// </para>
        /// <para>
        /// <see cref="Pessoa"/>s que passam por esse método devem ter o <see cref="Fingerprint.Template"/> valido
        /// para toda <see cref="Fingerprint"/>, i.e. devem ser passados pelo método <see cref="Extrair"/> antes.
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
        /// não existir match. Os resultados são classificados por pontuação em ordem decrescente. Se voce precisar apenas do melhor match,
        /// chame o método <see cref="Enumerable.FirstOrDefault{T}(IEnumerable{T})"/> na base de dados retornada.</returns>
        /// <remarks>
        /// <para>
        /// Compare o probe<see cref="Pessoa"/> para todos os candidatos <see cref="Pessoa"/>s e retorne o candidato mais similar.
        /// Chamando <see cref="Identificar"/>é quase a mesma coisa que chamar o <see cref="Verificar"/> em um loop
        /// exceto que o <see cref="Identificar"/> é bem mais rapido que o loop do <see cref="Verificar"/>.
        /// Se não houver candidato ou pontuação igual ou maior que o <see cref="Threshold"/>, <see cref="Identificar"/> retorna
        /// uma base de dados vazia.
        /// </para>
        /// <para>
        /// A maioria das aplicações precisam apenas do melhor match que pode ser obtido chamando o método
        /// <see cref="Enumerable.FirstOrDefault{T}(IEnumerable{T})"/> na base de dados retornada.
        /// Pontuação correspondente para cada<see cref="Pessoa"/> retornado pode ser obtido chamando o
        /// <see cref="Verificar"/> no probe <see cref="Pessoa"/> e o matching <see cref="Pessoa"/>.
        /// </para>
        /// <para>
        /// <see cref="Pessoa"/>s que passar por esse método deve ter o <see cref="Fingerprint.Template"/> valido.
        /// para cada <see cref="Fingerprint"/>, i.e. eles precisam ter passado pelo método <see cref="Extrair"/> antes.
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
