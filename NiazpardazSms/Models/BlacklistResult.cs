using Newtonsoft.Json;

namespace NiazpardazSms.Models
{
    /// <summary>
    /// نتیجه استخراج شماره‌های لیست سیاه
    /// </summary>
    public class BlacklistNumbersResult
    {
        /// <summary>
        /// لیست شماره‌های لیست سیاه
        /// </summary>
        [JsonProperty("blackListNumbers")]
        public string[] BlackListNumbers { get; set; }

        /// <summary>
        /// کد نتیجه
        /// </summary>
        [JsonProperty("resultCode")]
        public BlacklistResultCode ResultCode { get; set; }
    }

    /// <summary>
    /// نتیجه بررسی شماره در لیست سیاه
    /// </summary>
    public class IsBlacklistResult
    {
        /// <summary>
        /// آیا شماره در لیست سیاه است؟
        /// </summary>
        [JsonProperty("isBlack")]
        public bool IsBlack { get; set; }

        /// <summary>
        /// کد نتیجه
        /// </summary>
        [JsonProperty("resultCode")]
        public BlacklistResultCode ResultCode { get; set; }
    }
    
    /// <summary>
    /// کدهای نتیجه استخراج شماره‌های لیست سیاه
    /// </summary>
    public enum BlacklistResultCode
    {
        /// <summary>عملیات با موفقیت انجام شد</summary>
        Success = 0,

        /// <summary>نام کاربری و رمز عبور صحیح نمی باشد</summary>
        InvalidCredentials = -1,

        /// <summary>کاربر غیرفعال می باشد</summary>
        UserDisabled = -2,

        /// <summary>آرایه شماره های همراه خالی می باشد</summary>
        EmptyNumbersArray = -3,

        /// <summary>تعداد شماره ها حداکثر 1000 شماره می باشد</summary>
        MaxNumbersExceeded = -4,
        
        /// <summary>Ip موقتا بلاک شده است</summary>
        IpBlocked = -6,
        
        /// <summary>ApiKey نامعتبر</summary>
        InvalidApiKey = -7
    }
}