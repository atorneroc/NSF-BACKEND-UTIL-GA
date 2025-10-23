using Scharff.Domain.Utils.Exceptions;

namespace Scharff.Application.Helpers
{
    public static class DateHelper
    {
        public static DateTime ConvertToLimaTimeZone(DateTime dateTime)
        {
            try
            {
                string timeZoneId;

                if (OperatingSystem.IsWindows())
                {
                    timeZoneId = "SA Pacific Standard Time"; // Windows
                }
                else
                {
                    timeZoneId = "America/Lima"; // Linux/Mac (IANA)
                }

                TimeZoneInfo limaTimeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
                return TimeZoneInfo.ConvertTime(dateTime, limaTimeZone);
            }
            catch (TimeZoneNotFoundException)
            {
                return dateTime.ToUniversalTime().AddHours(-5);
                //throw new NotFoundException("La zona horaria de Lima no fue encontrada en el sistema.");
            }
            catch (InvalidTimeZoneException)
            {
                return dateTime.ToUniversalTime().AddHours(-5);
                //throw new NotFoundException("La zona horaria de Lima no es válida.");
            }
        }
    }
}
