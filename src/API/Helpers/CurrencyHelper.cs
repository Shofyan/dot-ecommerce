using System.Globalization;

namespace API.Helpers;

public static class CurrencyHelper
{
    private static readonly CultureInfo IndonesianCulture = new CultureInfo("id-ID");

    public static string ToRupiah(this decimal amount)
    {
        return string.Format(IndonesianCulture, "Rp {0:N0}", amount);
    }

    public static string ToRupiahWithDecimal(this decimal amount)
    {
        return string.Format(IndonesianCulture, "Rp {0:N2}", amount);
    }
}
