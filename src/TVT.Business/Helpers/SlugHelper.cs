using System.Text;
using System.Text.RegularExpressions;

namespace TVT.Business.Helpers;

public static class SlugHelper
{
    public static string Generate(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            return string.Empty;

        text = text.Trim().ToLowerInvariant();

        text = text
            .Replace("ə", "e")
            .Replace("ü", "u")
            .Replace("ö", "o")
            .Replace("ğ", "g")
            .Replace("ş", "s")
            .Replace("ç", "c")
            .Replace("ı", "i")
            .Replace("İ", "i");

        text = RemoveDiacritics(text);

        text = Regex.Replace(text, @"[^a-z0-9\s-]", "");

        text = Regex.Replace(text, @"\s+", "-");

        text = Regex.Replace(text, @"-+", "-");

        return text.Trim('-');
    }

    private static string RemoveDiacritics(string text)
    {
        var normalized = text.Normalize(NormalizationForm.FormD);

        var builder = new StringBuilder();

        foreach (var c in normalized)
        {
            if (System.Globalization.CharUnicodeInfo.GetUnicodeCategory(c)
                != System.Globalization.UnicodeCategory.NonSpacingMark)
            {
                builder.Append(c);
            }
        }

        return builder.ToString().Normalize(NormalizationForm.FormC);
    }
}
