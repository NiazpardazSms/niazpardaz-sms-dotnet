using Newtonsoft.Json;

namespace NiazpardazSms.Models
{
    /// <summary>
    /// نتیجه دریافت شماره‌های ارسال‌کننده
    /// </summary>
    public class SenderNumbersResult
    {
        /// <summary>
        /// لیست شماره‌های فرستنده
        /// </summary>
        [JsonProperty("senders")]
        public string[] Senders { get; set; }

        /// <summary>
        /// کد نتیجه
        /// </summary>
        [JsonProperty("resultCode")]
        public SenderNumbersResultCode ResultCode { get; set; }
    }
    
    /// <summary>
    /// کدهای نتیجه شماره های ارسال کننده
    /// </summary>
    public enum SenderNumbersResultCode
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