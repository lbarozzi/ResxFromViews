using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;

namespace ResxFromViews {
    internal class Replacer {

        // Trova tutte le etichette nel file CSHTML
        public static void ProcessFile(string fname) {
            List<string> etichetteTrovate = TrovaEtichette(fname);

            // Percorso del file RESX di output
            string fileResxPath = fname.Replace("cshtml", "it.resx");

            // Scrivi le etichette nel file RESX
            ScriviSuResx(etichetteTrovate, fileResxPath);

            Console.WriteLine($"Etichette trovate nel file CSHTML e scritte in {fileResxPath}.");
        }

        static List<string> TrovaEtichette(string filePath) {
            List<string> etichette = new List<string>();
            string pattern = @"@Localizer\[(.*?)\]";

            string content = File.ReadAllText(filePath);
            MatchCollection matches = Regex.Matches(content, pattern);

            foreach (Match match in matches) {
                etichette.Add(match.Groups[1].Value);
            }

            return etichette;
        }

        static void ScriviSuResx(List<string> etichette, string filePath) {
            using (XmlWriter writer = XmlWriter.Create(filePath)) {
                writer.WriteStartDocument();
                writer.WriteStartElement("root");

                foreach (string etichetta in etichette) {
                    writer.WriteStartElement("data");
                    writer.WriteAttributeString("name", etichetta);
                    writer.WriteAttributeString("xml:space", "preserve");
                    writer.WriteElementString("value", etichetta);
                    writer.WriteEndElement();
                }

                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
        }
    }
}
