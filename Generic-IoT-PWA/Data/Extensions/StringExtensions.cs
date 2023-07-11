using System.Text;
using System.Web;

namespace Generic_IoT_PWA.Data.Extensions
{
    public static class StringExtensions
    {
        public static string ToPascal(this string text) =>
        string.Concat(text[0].ToString().ToUpper(), text.AsSpan(1));

        public static string EncodeUTF8(this string text) =>
        HttpUtility.UrlEncode(text, Encoding.UTF8);

        public static string DecodeUTF8(this string text) =>
            HttpUtility.UrlDecode(text, Encoding.UTF8);
    }
}
