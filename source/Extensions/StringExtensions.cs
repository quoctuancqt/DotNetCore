using System.Text;
using System.Text.RegularExpressions;

namespace DotNetCore.Extensions
{
    public static class StringExtensions
    {
        public static string CamelCase(this string value)
        {
            return string.IsNullOrWhiteSpace(value) ? value : value[0].ToString().ToLowerInvariant() + value.Substring(1);
        }

        public static string NonSpecialCharacters(this string value)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            var bytes = Encoding.GetEncoding("ISO-8859-8").GetBytes(value);

            value = Encoding.Default.GetString(bytes);

            return new Regex("[^0-9a-zA-Z._ ]+?").Replace(value, string.Empty);
        }

        public static string NumericCharacters(this string value)
        {
            return Regex.Replace(value, "[^0-9]", string.Empty);
        }
    }
}
