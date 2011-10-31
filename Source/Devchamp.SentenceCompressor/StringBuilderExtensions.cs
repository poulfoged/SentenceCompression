using System.Text;

namespace Devchamp.SentenceCompression
{
    public static class StringBuilderExtensions
    {
        public static char FirstChar(this StringBuilder stringBuilder)
        {
            return stringBuilder.ToString()[0];
        }

        public static char PopChar(this StringBuilder stringBuilder)
        {
            var result = stringBuilder.FirstChar();
            stringBuilder.Remove(0, 1); 
            return result;
        }

        public static StringBuilder AppendAndPopCharFrom(this StringBuilder stringBuilder, StringBuilder sourceBuilder)
        {
            return stringBuilder.AppendAndPopCharFrom(sourceBuilder, 1);
        }


        public static StringBuilder AppendAndPopCharFrom(this StringBuilder stringBuilder, StringBuilder sourceBuilder, int length)
        {
            stringBuilder.Append(sourceBuilder.ToString().Substring(0, length));
            sourceBuilder.Remove(0, length);
            return stringBuilder;
        }
    }
}
