using Newtonsoft.Json;

namespace NiazpardazSms.Models
{
    /// <summary>
    /// نتیجه بررسی محتوای پیامک
    /// </summary>
    public class CheckContentResult
    {
        /// <summary>
        /// آیا متن معتبر است؟
        /// </summary>
        [JsonProperty("isValid")]
        public bool IsValid { get; set; }

        /// <summary>
        /// کد نتیجه
        /// </summary>
        [JsonProperty("resultCode")]
        public CheckContentResultCode ResultCode { get; set; }
    }
    
    /// <summary>
    /// کدهای نتیجه بررسی محتوای پیامک
    /// </summary>
    public enum CheckContentResultCode
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