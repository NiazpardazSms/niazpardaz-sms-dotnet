using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace NiazpardazSms.Models
{
    /// <summary>
    /// نتیجه دریافت پیامک‌ها
    /// </summary>
    public class MessagesResult
    {
        /// <summary>
        /// لیست پیامک‌ها
        /// </summary>
        [JsonProperty("messages")]
        public List<MessageInfo> Messages { get; set; }

        /// <summary>
        /// کد نتیجه
        /// </summary>
        [JsonProperty("resultCode")]
        public int ResultCode { get; set; }
    }

    /// <summary>
    /// اطلاعات پیامک
    /// </summary>
    public class MessageInfo
    {
        /// <summary>
        /// شناسه پیامک
        /// </summary>
        [JsonProperty("messageId")]
        public int MessageId { get; set; }
        
        /// <summary>
        /// شناسه کاربر
        /// </summary>
        [JsonProperty("userId")]
        public int UserId { get; set; }
        
        /// <summary>
        /// تعرفه
        /// </summary>
        [JsonProperty("tariff")]
        public decimal Tariff { get; set; }
        
        /// <summary>
        /// متن پیامک
        /// </summary>
        [JsonProperty("content")]
        public string Content { get; set; }
        
        /// <summary>
        /// زمان انجام
        /// </summary>
        [JsonProperty("actionDateTime")]
        public DateTime ActionDateTime { get; set; }
        
        /// <summary>
        /// مسیر پیام
        /// </summary>
        [JsonProperty("messageType")]
        public SmsDirectionType MessageType { get; set; }
        
        /// <summary>
        /// فرستنده
        /// </summary>
        [JsonProperty("sender")]
        public string Sender { get; set; }
        
        /// <summary>
        /// دریافت کننده
        /// </summary>
        [JsonProperty("receiver")]
        public string Receiver { get; set; }
        
        /// <summary>
        /// فلش
        /// </summary>
        [JsonProperty("flash")]
        public bool Flash { get; set; }
        
        /// <summary>
        /// تعداد صفحه
        /// </summary>
        [JsonProperty("pages")]
        public int Pages { get; set; }
        
        /// <summary>
        /// زبان
        /// </summary>
        [JsonProperty("lang")]
        public SmsLangType Lang { get; set; }
        
        /// <summary>
        /// وضعیت پیامک
        /// </summary>
        [JsonProperty("status")]
        public SmsStatusType Status { get; set; }
        
        /// <summary>
        /// وضعیت ارسال پیامک
        /// </summary>
        [JsonProperty("sendStatus")]
        public SmsSendStatusType SendStatus { get; set; }
        
        /// <summary>
        /// وضعیت ارسال پیامک
        /// </summary>
        [JsonProperty("sendMethod")]
        public SmsSendMethodType SendMethod { get; set; }
        
        /// <summary>
        /// هزینه
        /// </summary>
        [JsonProperty("cost")]
        public decimal Cost { get; set; }
        
        /// <summary>
        /// عنوان
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; set; }
        
        /// <summary>
        /// تعداد کل
        /// </summary>
        [JsonProperty("count")]
        public int Count { get; set; }
        
        /// <summary>
        /// تعداد ارسال شده
        /// </summary>
        [JsonProperty("sent")]
        public int Sent { get; set; }
        
        /// <summary>
        /// توضیحات
        /// </summary>
        [JsonProperty("desc")]
        public string Desc { get; set; }
        
        /// <summary>
        /// تعداد ارسال نشده
        /// </summary>
        [JsonProperty("notSent")]
        public int NotSent { get; set; }
        
        /// <summary>
        /// هزینه برگشت داده شده
        /// </summary>
        [JsonProperty("moneyIsRefunded")]
        public bool MoneyIsRefunded { get; set; }
        
        /// <summary>
        /// خوانده شده
        /// </summary>
        [JsonProperty("isRead")]
        public bool IsRead { get; set; }
    }
    
    /// <summary>
    /// زبان پیامک
    /// </summary>
    public enum SmsLangType
    {
        /// <summary>نامشخص</summary>
        None = -10,

        /// <summary>فارسی</summary>
        Fa = 1,

        /// <summary>انگلیسی</summary>
        En = 2
    }
    
    /// <summary>
    /// وضعیت پیامک
    /// </summary>
    public enum SmsStatusType
    {
        /// <summary>نامشخص</summary>
        None = -10,

        /// <summary>منتظر تایید</summary>
        Pending = 1,

        /// <summary>غیر مجاز</summary>
        Illegal = 2,

        /// <summary>در حال ارسال</summary>
        Sending = 3,

        /// <summary>تایید نشده</summary>
        NotApproved = 4,

        /// <summary>ارسال شده</summary>
        Sent = 5,

        /// <summary>انصراف</summary>
        Canceled = 6,

        /// <summary>توقف ارسال</summary>
        Error = 7,

        /// <summary>مبلغ شارژ جهت ارسال کافی نیست</summary>
        NoCredit = 8,

        /// <summary>ارسال نشده</summary>
        NotSent = 9,

        /// <summary>منتظر ارسال</summary>
        WaitForSend = 10,

        /// <summary>در حال کسر هزینه</summary>
        PendingDecCost = 11,

        /// <summary>در صف منتطر ارسال</summary>
        InQueue = 12,

        /// <summary>در حال کسر هزینه چند به چند</summary>
        ManyToManyCalcCost = 13
    }
    
    /// <summary>
    /// وضعیت ارسال پیامک
    /// </summary>
    public enum SmsSendStatusType
    {
        /// <summary>نامشخص</summary>
        None = -10,

        /// <summary>ارسال شده</summary>
        Sent = 1,

        /// <summary>خطای مخابراتی</summary>
        Error = 2,
        
        /// <summary>خط مقصد دریافت از خطوط پیامک را مسدود کرده است</summary>
        BlockList = 3
    }
    
    /// <summary>
    /// روش ارسال
    /// </summary>
    public enum SmsSendMethodType
    {
        /// <summary>نامشخص</summary>
        None = -10,

        /// <summary>سریع</summary>
        Quick = 1,

        /// <summary>تست</summary>
        Test = 2,

        /// <summary>منطقه ای</summary>
        Regional = 3,

        /// <summary>گروهی</summary>
        Group = 4,

        /// <summary>وب سرویس</summary>
        WebService = 5,
        
        /// <summary>اطلاع رسانی</summary>
        Announcement = 6,

        /// <summary>منشی</summary>
        Secretary = 7,
        
        /// <summary>ارسال هوشمند</summary>
        IntelligentSend = 8,
        
        /// <summary>ارسال متناظر</summary>
        CorrespondingSend = 9,

        /// <summary>کد خوان</summary>
        CodeReader = 10,

        /// <summary>نظرسنجی</summary>
        Poll = 11,

        /// <summary>انتقال پیامک</summary>
        Transfer = 12,

        /// <summary>پاسخ</summary>
        Reply = 13,
        
        /// <summary>ارسال از دفتر تلفن</summary>
        PhoneBook = 14,

        /// <summary>ارسال کد پستی</summary>
        PostalCode = 15,

        /// <summary>پیامک مناسبت</summary>
        SmsEvent = 16,

        /// <summary>منشی هوشمند</summary>
        IntelligentSecretary = 17,

        /// <summary>اضافه به لیست</summary>
        AddToPhoneBook = 18,

        /// <summary>پیامک فوری</summary>
        InstantSms = 19,

        /// <summary>پیامک زماندار</summary>
        ScheduleSms = 20,

        /// <summary>پیامک سررسید</summary>
        UsanceSms = 21,
        
        /// <summary>سن و جنسیت</summary>
        AgeAndGender = 22,
        
        /// <summary>سیستمی</summary>
        System = 23,
        
        /// <summary>ارسال تولد</summary>
        BirthdaySms = 24,
        
        /// <summary>کیوسک</summary>
        KioskSms = 25,
        
        /// <summary>لغو11</summary>
        CancellationBy11 = 26
    }
    
    /// <summary>
    /// نوع مسیر پیامک
    /// </summary>
    public enum SmsDirectionType
    {
        /// <summary>نامشخص</summary>
        None = -10,

        /// <summary>دریافتی</summary>
        In = 1,

        /// <summary>ارسالی</summary>
        Out = 2,
        
        /// <summary>زماندار</summary>
        Schedule = 3,
        
        /// <summary>سررسید</summary>
        Usance = 4
    }
}