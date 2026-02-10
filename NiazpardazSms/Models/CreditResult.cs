using Newtonsoft.Json;

namespace NiazpardazSms.Models
{
    /// <summary>
    /// نتیجه دریافت اعتبار
    /// </summary>
    public class CreditResult
    {
        /// <summary>
        /// اعتبار باقیمانده
        /// </summary>
        [JsonProperty("credit")]
        public decimal Credit { get; set; }

        /// <summary>
        /// کد نتیجه
        /// </summary>
        [JsonProperty("resultCode")]
        public CreditResultCode ResultCode { get; set; }
    }
    
    /// <summary>
    /// کدهای نتیجه اعتبار
    /// </summary>
    public enum CreditResultCode
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