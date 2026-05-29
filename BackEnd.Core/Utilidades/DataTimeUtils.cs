

namespace BackEnd.Core.Utilidades;

public static class DataTimeUtils
{
    public static DateTime GetDateTime()
    {
        return DateTime.UtcNow.AddHours(-5);
    }
}
