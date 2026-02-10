using System;

namespace NiazpardazSms.Exceptions
{
    /// <summary>
    /// استثنای مربوط به خطاهای API نیازپرداز
    /// </summary>
    public class NiazpardazApiException : Exception
    {
        /// <summary>
        /// کد خطا
        /// </summary>
        public int ErrorCode { get; }

        /// <summary>
        /// سازنده با پیام خطا
        /// </summary>
        public NiazpardazApiException(string message) : base(message)
        {
        }

        /// <summary>
        /// سازنده با پیام و کد خطا
        /// </summary>
        public NiazpardazApiException(string message, int errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }

        /// <summary>
        /// سازنده با پیام و استثنای داخلی
        /// </summary>
        public NiazpardazApiException(string message, Exception innerException) 
            : base(message, innerException)
        {
        }
    }
}