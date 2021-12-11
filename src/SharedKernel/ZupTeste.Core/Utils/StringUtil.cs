using System.Text;

namespace ZupTeste.Core.Utils
{
    public static class StringUtil
    {
        public static string UnMask(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return value;

            return string.Join("", value.Select(x => x).Where(char.IsDigit));
        }
    }
}
