using System;
using System.Collections.Generic;
using System.Text;
using ComparacaoDPCI.Geral;
//using SourceAFIS.Dummy;

namespace ComparacaoDPCI.Templates
{
    [Serializable]
    public sealed class Template : ICloneable
    {
        public enum MinutiaType : byte
        {
            Ending = 0,
            Bifurcation = 1
        }

        [Serializable]
        public struct Minutia
        {
            public readonly PointS Position;
            public readonly byte Direction;
            public readonly MinutiaType Type;

            public Minutia(TemplateBuilder.Minutia builderMinutia)
            {
                Position = new PointS(builderMinutia.Position);
                Direction = builderMinutia.Direction;
                if (builderMinutia.Type == TemplateBuilder.MinutiaType.Extremidade)
                    Type = MinutiaType.Ending;
                else
                    Type = MinutiaType.Bifurcation;
            }

            public TemplateBuilder.Minutia ToBuilderMinutia()
            {
                TemplateBuilder.Minutia builderMinutia = new TemplateBuilder.Minutia();
                builderMinutia.Position = Position;
                builderMinutia.Direction = Direction;
                if (Type == MinutiaType.Ending)
                    builderMinutia.Type = TemplateBuilder.MinutiaType.Extremidade;
                else
                    builderMinutia.Type = TemplateBuilder.MinutiaType.Bifurcação;
                return builderMinutia;
            }
        }

        public readonly int OriginalDpi;
        public readonly int OriginalWidth;
        public readonly int OriginalHeight;
        public readonly int StandardDpiWidth;
        public readonly int StandardDpiHeight;

        public readonly Minutia[] Minutiae;

        [NonSerialized]
        internal object MatcherCache;

        public Template(TemplateBuilder builder)
        {
            OriginalDpi = builder.OriginalDpi;
            OriginalWidth = builder.OriginalWidth;
            OriginalHeight = builder.OriginalHeight;
            StandardDpiWidth = builder.StandardDpiWidth;
            StandardDpiHeight = builder.StandardDpiHeight;

            Minutiae = new Minutia[builder.Minutiae.Count];
            for (int i = 0; i < builder.Minutiae.Count; ++i)
                Minutiae[i] = new Minutia(builder.Minutiae[i]);
        }

        public TemplateBuilder ToTemplateBuilder()
        {
            TemplateBuilder builder = new TemplateBuilder();

            builder.OriginalDpi = OriginalDpi;
            builder.OriginalWidth = OriginalWidth;
            builder.OriginalHeight = OriginalHeight;

            foreach (Minutia minutia in Minutiae)
                builder.Minutiae.Add(minutia.ToBuilderMinutia());
            return builder;
        }

        public Template Clone()
        {
            return new Template(ToTemplateBuilder());
        }

        object ICloneable.Clone() { return Clone(); }
    }
}
