using System;

namespace Log.Extensions
{
    public static class TimeSpanExtension
    {
        public static string ToReadableString(this TimeSpan span)
        {
            string formatted = string.Format("{0}{1}{2}",
                span.Duration().Hours > 0 ? string.Format("{0:0}h", span.Hours) : string.Empty,
                span.Duration().Minutes > 0 ? string.Format("{0:0}m", span.Minutes) : string.Empty,
                span.Duration().Seconds > 0 ? string.Format("{0:0}s", span.Seconds) : string.Empty);
            if (string.IsNullOrEmpty(formatted)) formatted = "0 s";
            return formatted;
        }
    }
}
