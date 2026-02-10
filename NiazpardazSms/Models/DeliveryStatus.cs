using System;
using Newtonsoft.Json;

namespace NiazpardazSms.Models
{
    /// <summary>
    /// نتیجه گزارش تحویل
    /// </summary>
    public class BatchDeliveryResult
    {
        /// <summary>
        /// کد نتیجه
        /// </summary>
        [JsonProperty("resultCode")]
        public DeliveryResultCode ResultCode { get; set; }

        /// <summary>
        /// لیست شماره‌ها
        /// </summary>
        [JsonProperty("numbers")]
        public string[] Numbers { get; set; }

        /// <summary>
        /// وضعیت تحویل هر شماره
        /// </summary>
        [JsonProperty("deliveryStatus")]
        public SmsDeliveryStatus[] DeliveryStatus { get; set; }
    }
    
    /// <summary>
    /// کدهای نتیجه گزارش تحویل
    /// </summary>
    public enum DeliveryResultCode
    {
        /// <summary>موفق</summary>
        Success = 0,

        /// <summary>خطا در ارتباط با سرویس دهنده</summary>
        ServiceConnectionError = -1,

        /// <summary>پیام با این کد وجود ندارد (batchSmsId نامعتبر است)</summary>
        InvalidBatchSmsId = -2,

        /// <summary>مهلت یک هفته ای گرفتن گزارش پایان یافته است</summary>
        ReportExpired = -3,

        /// <summary>پیام در صف ارسال مخابرات است، امکان گرفتن گزارش وجود ندارد</summary>
        MessageInQueue = -4,

        /// <summary>حداقل یک دقیقه بعد از ارسال اقدام نمایید</summary>
        TooEarly = -5,
        
        /// <summary>Ip موقتا بلاک شده است</summary>
        IpBlocked = -6,
        
        /// <summary>ApiKey نامعتبر</summary>
        InvalidApiKey = -7
    }
    
    /// <summary>
    /// وضعیت تحویل پیامک
    /// </summary>
    public enum SmsDeliveryStatus
    {
        /// <summary>نامشخص</summary>
        None = -10,

        /// <summary>ارسال شده به مخابرات</summary>
        SentToTelecom = 0,

        /// <summary>رسیده به گوشی</summary>
        Delivered = 1,

        /// <summary>نرسیده به گوشی</summary>
        NotDelivered = 2,

        /// <summary>خطای مخابراتی</summary>
        SmsFailed = 3,

        /// <summary>خطای نامشخص</summary>
        UnknownError = 4,

        /// <summary>رسیده به مخابرات</summary>
        ReceivedByTelecom = 5,

        /// <summary>نرسیده به مخابرات</summary>
        NotReceivedByTelecom = 6,

        /// <summary>مسدود شده توسط مقصد</summary>
        BlackListed = 7,

        /// <summary>نامشخص</summary>
        Unknown = 8,

        /// <summary>مخابرات پیام را مردود اعلام کرد</summary>
        RejectedByTelecom = 9,

        /// <summary>کنسل شده توسط اپراتور</summary>
        Canceled = 10,

        /// <summary>ارسال نشده</summary>
        NotSent = 11,

        /// <summary>تلگرام ندارد</summary>
        NoTelegram = 12,

        /// <summary>در صف ارسال</summary>
        InQueue = 13
    }
}