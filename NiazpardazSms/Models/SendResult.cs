using Newtonsoft.Json;

namespace NiazpardazSms.Models
{
    /// <summary>
    /// نتیجه ارسال پیامک
    /// </summary>
    public class SendBatchSmsResult
    {
        /// <summary>
        /// شناسه یکتای ارسال گروهی
        /// </summary>
        [JsonProperty("batchSmsId")]
        public int BatchSmsId { get; set; }

        /// <summary>
        /// کد نتیجه
        /// </summary>
        [JsonProperty("resultCode")]
        public SendResultCode ResultCode { get; set; }
    }

    /// <summary>
    /// نتیجه ارسال پیامک LikeToLike
    /// </summary>
    public class SendLikeToLikeResult
    {
        /// <summary>
        /// شناسه پیامک
        /// </summary>
        [JsonProperty("smsId")]
        public int SmsId { get; set; }

        /// <summary>
        /// کد نتیجه
        /// </summary>
        [JsonProperty("resultCode")]
        public SendResultCode ResultCode { get; set; }
    }

    /// <summary>
    /// کدهای نتیجه ارسال پیامک
    /// </summary>
    public enum SendResultCode
    {
        /// <summary>ارسال با موفقیت انجام شد</summary>
        SendWasSuccessful = 0,

        /// <summary>نام کاربر یا کلمه عبور نامعتبر می باشد</summary>
        InvalidUserNameOrPassword = 1,

        /// <summary>کاربر مسدود شده است</summary>
        UserBlocked = 2,

        /// <summary>شماره فرستنده نامعتبر است</summary>
        InvalidSenderNumber = 3,

        /// <summary>محدودیت در ارسال روزانه</summary>
        LimitationInDailySend = 4,

        /// <summary>تعداد گیرندگان حداکثر 1000 شماره می باشد</summary>
        LimitationInRecieverCount = 5,

        /// <summary>خط فرسنتده غیرفعال است</summary>
        SenderLineIsInactive = 6,

        /// <summary>متن پیامک شامل کلمات فیلتر شده است</summary>
        SmsContentFilteredWordsIsIncluded = 7,

        /// <summary>اعتبار کافی نیست</summary>
        NoCredit = 8,

        /// <summary>سامانه در حال بروز رسانی است</summary>
        SystemBeingUpdated = 9,

        /// <summary>وب سرویس غیرفعال است</summary>
        WebServiceNoActive = 10,

        /// <summary>پیاده سازی نشده است</summary>
        NotImplemented = 11,

        /// <summary>تعداد پیامها و شماره ها باید یکسان باشد</summary>
        LikeToLikeSendReceiverAndMessageCountShouldEqual = 12,

        /// <summary>تعداد پیامها حداکثر 100 پیام می باشد</summary>
        LimitationInMesssageContentCount = 13,

        /// <summary>هیچ مقداری برای تعرفه جاری کاربر تعریف نشده است</summary>
        UserTariffNotDetermined = 14,

        /// <summary>ارسال تکراری متن مشابه به شماره مشابه در مدت زمان مشخص</summary>
        DuplicateSendSms = 15,

        /// <summary>ﺷﻤﺎر ﻣﻮﺑﺎﻳﻞ گیرنده ﻳﺎﻓﺖ ﻧﺸﺪ(گیرنده خالی، اشتباه یا بلاک لیست است)</summary>
        InvalidNumberEmptyOrBlackList = 16,

        /// <summary>متن وارد نشده است</summary>
        TextNotFound = 17,

        /// <summary>متن طبق الگوی تعریف شده نیست(وب سرویس شما محدود به قالب تعرفه شده، هست)</summary>
        NotValidTemplateFound = 18,

        /// <summary>کاربر منقضی شده است</summary>
        UserExpired = 19,

        /// <summary>وضعیت کاربر فعال نیست</summary>
        UserIsNotActive = 20,

        /// <summary>مقدار یکی یا بیشتر از پارامترهای وروردی معتبر نیست</summary>
        InvalidParameters = 21,

        /// <summary>آی پی موقت بلاک شده است</summary>
        IpBlocked = 22,

        /// <summary>عملیات با خطا مواجه شد. لطفاً دقایقی دیگر مجدداً تلاش نمایید</summary>
        EnqueueFailed = 23,

        /// <summary>درخواست کاملا تکراری در بازه کوتاه (چند ثانیه)</summary>
        DuplicateRequestThreshold = 24,

        /// <summary>ApiKey نامعتبر</summary>
        InvalidApiKey = 25,

        /// <summary>خطا در ساخت فایل صوتی</summary>
        ErrorCreateVoiceFile = 26
    }
}