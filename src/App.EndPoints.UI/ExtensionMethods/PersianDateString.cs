using System.Globalization;

namespace App.EndPoints.UI.ExtensionMethods
{
    public static class PersianDateString
    {
        public static string[] Days = { "یک شنبه", "دو شنبه", "سه شنبه", "چهار شنبه", "پنج شنبه", "جمعه", "شنبه" };
        public static PersianCalendar pc = new PersianCalendar();

        public static string ToPersianDateOfWeek(this DateTime date)
        {
            return ($"{Days[pc.GetDayOfWeek(date).GetHashCode()]}, {pc.GetDayOfMonth(date)} / {pc.GetMonth(date)} / {pc.GetYear(date)}");
        }
        public static string ToPersianDate(this DateTime date)
        {
            return ($"{pc.GetDayOfMonth(date)} / {pc.GetMonth(date)} / {pc.GetYear(date)} - {pc.GetHour(date)}:{pc.GetMinute(date)} ");
        }
        //ToPersianDateOfWeek : 1401/05/09 , یکشنبه
        //ToPersianDate : 1401/05/09
    }
}
