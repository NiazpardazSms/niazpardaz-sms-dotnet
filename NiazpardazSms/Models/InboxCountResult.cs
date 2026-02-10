using Newtonsoft.Json;

namespace NiazpardazSms.Models
{
    /// <summary>
    /// نتیجه دریافت تعداد پیامک‌های دریافتی
    /// </summary>
    public class InboxCountResult
    {
        /// <summary>
        /// تعداد پیامک‌های دریافتی
        /// </summary>
        [JsonProperty("inboxCount")]
        public int InboxCount { get; set; }

        /// <summary>
        /// کد نتیجه
        /// </summary>
        [JsonProperty("resultCode")]
        public InboxCountResultCode ResultCode { get; set; }
    }
    
    /// <summary>
    /// کدهای نتیجه تعداد پیامک های دریافتی
    /// </summary>
    public enum InboxCountResultCode
    {
        /// <summary>موفق</summary>
        Success = 0,

        /// <summary>نام کاربری و رمز عبور صحیح نمی باشد</summary>
        InvalidCredentials = -1,

        /// <summary>کاربر غیرفعال می باشد</summary>
        UserDisabled = -2,
        
        /// <summary>Ip موقتا بلاک شده است</summary>
        IpBlocked = -6,
        
        /// <summary>ApiKey نامعتبر</summary>
        InvalidApiKey = -7
    }
}