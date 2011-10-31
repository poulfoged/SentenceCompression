using System.Linq;
using System.Text;

namespace Devchamp.SentenceCompression
{
    public class SentenceCompressor
    {
        private const char SingleVerbatimCharMarker = (char)254;
        private const char MultiVerbatimCharMarker = (char)255;

        public static string Decompress(string input)
        {
            var outputBuilder = new StringBuilder();
            var inputBuilder = new StringBuilder(input);
            
            while(inputBuilder.Length > 0)
            {
                switch (inputBuilder.FirstChar())
                {
                    case MultiVerbatimCharMarker:
                        inputBuilder.PopChar();
                        var length = inputBuilder.PopChar();
                        outputBuilder.AppendAndPopCharFrom(inputBuilder, length);
                        break;
                    case SingleVerbatimCharMarker:
                        inputBuilder.PopChar();
                        outputBuilder.AppendAndPopCharFrom(inputBuilder);
                        break;
                    default:
                        outputBuilder.Append(PartDictionary[inputBuilder.PopChar()]);
                        break;
                }
            }

            return outputBuilder.ToString();
        }

        public static string Compress(string input)
        {
            var outputBuilder = new StringBuilder();
            var inputBuilder = new StringBuilder(input);
            var verbatimBuilder = new StringBuilder();

            while (inputBuilder.Length > 0)
            {
                var possibleParts = PartDictionary
                    .Where(p => inputBuilder.ToString().StartsWith(p))
                    .ToList();
                
                if (verbatimBuilder.Length == char.MaxValue)
                    FlushVerbatim(outputBuilder, verbatimBuilder);

                if (!possibleParts.Any())
                {
                    verbatimBuilder.AppendAndPopCharFrom(inputBuilder);
                    continue;
                } 
                
                FlushVerbatim(outputBuilder, verbatimBuilder);
                var bestFit = possibleParts.OrderByDescending(p => p.Length).First();
                outputBuilder.Append((char)PartDictionary.ToList().IndexOf(bestFit));
                inputBuilder.Remove(0, bestFit.Length);
            }

            FlushVerbatim(outputBuilder, verbatimBuilder);
            return outputBuilder.ToString();
        }

        private static void FlushVerbatim(StringBuilder outputBuilder, StringBuilder verbatimBuilder)
        {
            if (verbatimBuilder.Length == 1)
            {
                outputBuilder
                    .Append(SingleVerbatimCharMarker)
                    .AppendAndPopCharFrom(verbatimBuilder);
                
                return;
            }

            if (verbatimBuilder.Length > 1)
            {            
                outputBuilder
                    .Append(MultiVerbatimCharMarker)
                    .Append((char)verbatimBuilder.Length)
                    .AppendAndPopCharFrom(verbatimBuilder, verbatimBuilder.Length);
            } 
        }


        private static readonly string[] PartDictionary = 
        {
            " ", "the", "e", "t", "a", "of", "o", "and", "i", "n", "s", "e ", "r", " th",
            " t", "in", "he", "th", "h", "he ", "to", "\r\n", "l", "s ", "d", " a", "an",
            "er", "c", " o", "d ", "on", " of", "re", "of ", "t ", ", ", "is", "u", "at",
            "   ", "n ", "or", "which", "f", "m", "as", "it", "that", "\n", "was", "en",
            "  ", " w", "es", " an", " i", "\r", "f ", "g", "p", "nd", " s", "nd ", "ed ",
            "w", "ed", "http://", "for", "te", "ing", "y ", "The", " c", "ti", "r ", "his",
            "st", " in", "ar", "nt", ",", " to", "y", "ng", " h", "with", "le", "al", "to ",
            "b", "ou", "be", "were", " b", "se", "o ", "ent", "ha", "ng ", "their", "\"",
            "hi", "from", " f", "in ", "de", "ion", "me", "v", ".", "ve", "all", "re ",
            "ri", "ro", "is ", "co", "f t", "are", "ea", ". ", "her", " m", "er ", " p",
            "es ", "by", "they", "di", "ra", "ic", "not", "s, ", "d t", "at ", "ce", "la",
            "h ", "ne", "as ", "tio", "on ", "n t", "io", "we", " a ", "om", ", a", "s o",
            "ur", "li", "ll", "ch", "had", "this", "e t", "g ", "e\r\n", " wh", "ere",
            " co", "e o", "a ", "us", " d", "ss", "\n\r\n", "\r\n\r", "=\"", " be", " e",
            "s a", "ma", "one", "t t", "or ", "but", "el", "so", "l ", "e s", "s,", "no",
            "ter", " wa", "iv", "ho", "e a", " r", "hat", "s t", "ns", "ch ", "wh", "tr",
            "ut", "/", "have", "ly ", "ta", " ha", " on", "tha", "-", " l", "ati", "en ",
            "pe", " re", "there", "ass", "si", " fo", "wa", "ec", "our", "who", "its", "z",
            "fo", "rs", ">", "ot", "un", "<", "im", "th ", "nc", "ate", "><", "ver", "ad",
            " we", "ly", "ee", " n", "id", " cl", "ac", "il", "</", "rt", " wi", "div",
            "e, ", " it", "whi", " ma", "ge", "x", "e c", "men", ".com" 
        };
    }
}
