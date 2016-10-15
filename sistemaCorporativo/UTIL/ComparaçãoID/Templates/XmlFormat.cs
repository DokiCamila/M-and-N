using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;
using ComparacaoDPCI.Geral;
//using SourceAFIS.Dummy;

namespace ComparacaoDPCI.Templates
{
    public sealed class XmlFormat : TemplateFormatBase<XElement>
    {
        public override XElement Export(TemplateBuilder builder)
        {
            return new XElement("FingerprintTemplate",
                new XAttribute("Version", "2"),
                new XAttribute("OriginalDpi", builder.OriginalDpi),
                new XAttribute("OriginalWidth", builder.OriginalWidth),
                new XAttribute("OriginalHeight", builder.OriginalHeight),
                from minutia in builder.Minutiae
                select new XElement("Minuncia",
                    new XAttribute("X", minutia.Position.X),
                    new XAttribute("Y", minutia.Position.Y),
                    new XAttribute("Direção", minutia.Direction),
                    new XAttribute("Tipo", minutia.Type.ToString())));
        }

        public override TemplateBuilder Import(XElement template)
        {
            int version = (int)template.Attribute("Version");
            if (version < 1 || version > 2)
                throw new ApplicationException("Versão de template desconhecido");
            TemplateBuilder builder = new TemplateBuilder()
            {
                Minutiae = (from minutia in template.Elements("Minuncia")
                            select new TemplateBuilder.Minutia()
                            {
                                Position = new Point(
                                    (int)minutia.Attribute("X"),
                                    (int)minutia.Attribute("Y")),
                                Direction = (byte)(uint)minutia.Attribute("Direção"),
                                Type = (TemplateBuilder.MinutiaType)Enum.Parse(
                                    typeof(TemplateBuilder.MinutiaType),
                                    (string)minutia.Attribute("Tipo"),
                                    false)
                            }).ToList()
            };
            if (version >= 2)
            {
                builder.OriginalDpi = (int)template.Attribute("OriginalDpi");
                builder.OriginalWidth = (int)template.Attribute("OriginalWidth");
                builder.OriginalHeight = (int)template.Attribute("OriginalHeight");
            }
            else
            {
                builder.OriginalDpi = 500;
                builder.StandardDpiWidth = template.Elements("Minuncia").Max(e => (int)e.Attribute("X")) + 1;
                builder.StandardDpiHeight = template.Elements("Minuncia").Max(e => (int)e.Attribute("Y")) + 1;
            }
            return builder;
        }

        public override void Serialize(Stream stream, XElement template)
        {
            template.Save(stream);
        }

        public override XElement Deserialize(Stream stream)
        {
            return XElement.Load(stream);
        }
    }
}
