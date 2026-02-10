using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NiazpardazSms.Exceptions;
using NiazpardazSms.Models;

namespace NiazpardazSms
{
    /// <summary>
    /// کلاینت اصلی برای کار با API پیامکی نیازپرداز
    /// </summary>
    public class NiazpardazSmsClient : IDisposable
    {
        private readonly string _apiKey;
        private readonly HttpClient _httpClient;
        private readonly bool _disposeHttpClient;
        private const string DefaultBaseUrl = "https://login.niazpardaz.ir/api/v2/RestWebApi";
        
        /// <summary>
        /// آدرس پایه API
        /// </summary>
        private string BaseUrl { get; set; }

        /// <summary>
        /// سازنده با کلید API
        /// </summary>
        /// <param name="apiKey">کلید API دریافتی از پنل</param>
        public NiazpardazSmsClient(string apiKey)
        {
            if (string.IsNullOrWhiteSpace(apiKey))
                throw new ArgumentNullException(nameof(apiKey), "کلید API نمی‌تواند خالی باشد");

            _apiKey = apiKey;
            _httpClient = new HttpClient();
            _disposeHttpClient = true;
            BaseUrl = DefaultBaseUrl;
        }
        
        /// <summary>
        /// سازنده با کلید API و HttpClient سفارشی
        /// </summary>
        /// <param name="apiKey">کلید API دریافتی از پنل</param>
        /// <param name="httpClient">HttpClient سفارشی</param>
        public NiazpardazSmsClient(string apiKey, HttpClient httpClient)
        {
            if (string.IsNullOrWhiteSpace(apiKey))
                throw new ArgumentNullException(nameof(apiKey), "کلید API نمی‌تواند خالی باشد");

            _apiKey = apiKey;
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _disposeHttpClient = false;
            BaseUrl = DefaultBaseUrl;
        }

        #region Send SMS

        /// <summary>
        /// ارسال پیامک تکی
        /// </summary>
        /// <param name="fromNumber">شماره فرستنده</param>
        /// <param name="toNumber">شماره گیرنده</param>
        /// <param name="message">متن پیامک</param>
        /// <param name="isFlash">آیا فلش باشد؟</param>
        /// <param name="sendDelay">تاخیر ارسال (ثانیه)</param>
        /// <param name="cancellationToken">توکن لغو</param>
        /// <returns>نتیجه ارسال</returns>
        public async Task<SendBatchSmsResult> SendAsync(
            string fromNumber,
            string toNumber,
            string message,
            bool isFlash = false,
            int? sendDelay = null,
            CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(fromNumber))
                throw new ArgumentNullException(nameof(fromNumber));
            if (string.IsNullOrWhiteSpace(toNumber))
                throw new ArgumentNullException(nameof(toNumber));
            if (string.IsNullOrWhiteSpace(message))
                throw new ArgumentNullException(nameof(message));

            var result = await SendBulkAsync(
                fromNumber,
                new[] { toNumber },
                message,
                isFlash,
                sendDelay,
                cancellationToken);

            return result;
        }

        /// <summary>
        /// ارسال پیامک گروهی (یک متن به چند شماره)
        /// </summary>
        /// <param name="fromNumber">شماره فرستنده</param>
        /// <param name="toNumbers">لیست شماره گیرندگان</param>
        /// <param name="message">متن پیامک</param>
        /// <param name="isFlash">آیا فلش باشد؟</param>
        /// <param name="sendDelay">تاخیر ارسال</param>
        /// <param name="cancellationToken">توکن لغو</param>
        /// <returns>لیست نتایج ارسال</returns>
        public async Task<SendBatchSmsResult> SendBulkAsync(
            string fromNumber,
            IEnumerable<string> toNumbers,
            string message,
            bool isFlash = false,
            int? sendDelay = null,
            CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(fromNumber))
                throw new ArgumentNullException(nameof(fromNumber));
            if (toNumbers == null)
                throw new ArgumentNullException(nameof(toNumbers));
            if (string.IsNullOrWhiteSpace(message))
                throw new ArgumentNullException(nameof(message));

            var payload = new
            {
                fromNumber,
                messageContent = message,
                toNumbers = string.Join(",", toNumbers),
                isFlash,
                sendDelay
            };

            return await PostAsync<SendBatchSmsResult>("/SendBatchSms", payload, cancellationToken);
        }

        /// <summary>
        /// ارسال پیامک LikeToLike (هر شماره پیام مخصوص خودش)
        /// </summary>
        /// <param name="fromNumber">شماره فرستنده</param>
        /// <param name="toNumbers">لیست شماره گیرندگان</param>
        /// <param name="messages">لیست پیام‌ها (هم‌طول با گیرندگان)</param>
        /// <param name="isFlash">آیا فلش باشد؟</param>
        /// <param name="cancellationToken">توکن لغو</param>
        /// <returns>لیست نتایج ارسال</returns>
        public async Task<SendLikeToLikeResult> SendSmsLikeToLikeAsync(
            string fromNumber,
            IEnumerable<string> toNumbers,
            IEnumerable<string> messages,
            bool isFlash = false,
            CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(fromNumber))
                throw new ArgumentNullException(nameof(fromNumber));
            if (toNumbers == null)
                throw new ArgumentNullException(nameof(toNumbers));
            if (messages == null)
                throw new ArgumentNullException(nameof(messages));

            var payload = new
            {
                fromNumber,
                messageContents = string.Join(",", messages),
                toNumbers = string.Join(",", toNumbers),
                isFlash
            };

            return await PostAsync<SendLikeToLikeResult>("/SendSmsLikeToLike", payload, cancellationToken);
        }

        /// <summary>
        /// ارسال پیامک صوتی Otp
        /// </summary>
        /// <param name="fromNumber">شماره فرستنده</param>
        /// <param name="toNumber">شماره گیرنده</param>
        /// <param name="otp">متن پیامک Otp</param>
        /// <param name="isFlash">آیا فلش باشد؟</param>
        /// <param name="sendDelay">تاخیر ارسال</param>
        /// <param name="cancellationToken">توکن لغو</param>
        /// <returns>لیست نتایج ارسال</returns>
        public async Task<SendBatchSmsResult> SendVoiceOtpAsync(
            string fromNumber,
            string toNumber,
            string otp,
            bool isFlash = false,
            int? sendDelay = null,
            CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(fromNumber))
                throw new ArgumentNullException(nameof(fromNumber));
            if (string.IsNullOrEmpty(toNumber))
                throw new ArgumentNullException(nameof(toNumber));
            if (string.IsNullOrEmpty(otp))
                throw new ArgumentNullException(nameof(otp));

            var payload = new
            {
                fromNumber,
                messageContent = otp,
                toNumbers = toNumber,
                sendDelay,
                isFlash
            };

            return await PostAsync<SendBatchSmsResult>("/SendVoiceOtp", payload, cancellationToken);
        }
        
        #endregion

        #region Delivery

        /// <summary>
        /// گزارش تحویل پیامک گروهی
        /// </summary>
        /// <param name="batchSmsId">شناسه ارسال گروهی</param>
        /// <param name="pageIndex">شماره صفحه</param>
        /// <param name="pageSize">تعداد رکورد در صفحه</param>
        /// <param name="cancellationToken">توکن لغو</param>
        public async Task<BatchDeliveryResult> GetBatchDeliveryAsync(
            int batchSmsId,
            int pageIndex = 1,
            int pageSize = 100,
            CancellationToken cancellationToken = default)
        {
            var payload = new
            {
                batchSmsId,
                index = pageIndex,
                count = pageSize
            };

            return await PostAsync<BatchDeliveryResult>("/GetBatchDelivery", payload, cancellationToken);
        }

        /// <summary>
        /// گزارش تحویل پیامک LikeToLike
        /// </summary>
        /// <param name="smsId">شناسه پیامک</param>
        /// <param name="cancellationToken">توکن لغو</param>
        public async Task<BatchDeliveryResult> GetDeliveryLikeToLikeAsync(
            int smsId,
            CancellationToken cancellationToken = default)
        {
            var payload = new
            {
                smsId
            };

            return await PostAsync<BatchDeliveryResult>("/GetDeliveryLikeToLike", payload, cancellationToken);
        }

        #endregion

        #region Account

        /// <summary>
        /// دریافت اعتبار حساب
        /// </summary>
        /// <param name="cancellationToken">توکن لغو</param>
        public async Task<CreditResult> GetCreditAsync(CancellationToken cancellationToken = default)
        {
            return await PostAsync<CreditResult>("/GetCredit", new { }, cancellationToken);
        }

        /// <summary>
        /// دریافت لیست شماره‌های فرستنده
        /// </summary>
        /// <param name="cancellationToken">توکن لغو</param>
        public async Task<SenderNumbersResult> GetSenderNumbersAsync(CancellationToken cancellationToken = default)
        {
            return await PostAsync<SenderNumbersResult>("/GetSenderNumbers", new { }, cancellationToken);
        }

        #endregion
        
        #region Inbox

        /// <summary>
        /// دریافت تعداد پیامک‌های دریافتی
        /// </summary>
        /// <param name="isRead">فقط خوانده شده‌ها؟</param>
        /// <param name="cancellationToken">توکن لغو</param>
        public async Task<InboxCountResult> GetInboxCountAsync(
            bool isRead = false,
            CancellationToken cancellationToken = default)
        {
            var payload = new
            {
                isRead
            };

            return await PostAsync<InboxCountResult>("/GetInboxCount", payload, cancellationToken);
        }

        /// <summary>
        /// دریافت پیامک‌ها
        /// </summary>
        /// <param name="messageType">نوع پیامک</param>
        /// <param name="fromNumbers">شماره‌های فرستنده (با کاما جدا شده)</param>
        /// <param name="pageIndex">شماره صفحه</param>
        /// <param name="pageSize">تعداد رکورد در صفحه</param>
        /// <param name="cancellationToken">توکن لغو</param>
        public async Task<MessagesResult> GetMessagesAsync(
            int messageType,
            string fromNumbers,
            int pageIndex = 1,
            int pageSize = 100,
            CancellationToken cancellationToken = default)
        {
            var payload = new
            {
                messageType,
                fromNumbers,
                index = pageIndex,
                count = pageSize
            };

            return await PostAsync<MessagesResult>("/GetMessages", payload, cancellationToken);
        }

        /// <summary>
        /// دریافت پیامک‌ها بر اساس بازه زمانی
        /// </summary>
        /// <param name="messageType">نوع پیامک</param>
        /// <param name="fromNumbers">شماره‌های فرستنده (با کاما جدا شده)</param>
        /// <param name="fromDate">تاریخ شروع</param>
        /// <param name="toDate">تاریخ پایان</param>
        /// <param name="cancellationToken">توکن لغو</param>
        public async Task<MessagesResult> GetMessagesByDateRangeAsync(
            int messageType,
            string fromNumbers,
            DateTime fromDate,
            DateTime toDate,
            CancellationToken cancellationToken = default)
        {
            var payload = new
            {
                messageType,
                fromNumbers,
                fromDate,
                toDate
            };

            return await PostAsync<MessagesResult>("/GetMessagesByDateRange", payload, cancellationToken);
        }

        #endregion
        
        #region Blacklist

        /// <summary>
        /// استخراج شماره‌های لیست سیاه مخابرات
        /// </summary>
        /// <param name="numbers">لیست شماره‌ها</param>
        /// <param name="cancellationToken">توکن لغو</param>
        public async Task<BlacklistNumbersResult> ExtractTelecomBlacklistNumbersAsync(
            string[] numbers,
            CancellationToken cancellationToken = default)
        {
            var payload = new
            {
                numbers = string.Join(",", numbers)
            };

            return await PostAsync<BlacklistNumbersResult>("/ExtractTelecomBlacklistNumbers", payload, cancellationToken);
        }

        /// <summary>
        /// بررسی اینکه آیا شماره در لیست سیاه مخابرات هست؟
        /// </summary>
        /// <param name="number">شماره موبایل</param>
        /// <param name="cancellationToken">توکن لغو</param>
        public async Task<IsBlacklistResult> NumberIsInTelecomBlacklistAsync(
            string number,
            CancellationToken cancellationToken = default)
        {
            var payload = new
            {
                number
            };

            return await PostAsync<IsBlacklistResult>("/NumberIsInTelecomBlacklist", payload, cancellationToken);
        }

        #endregion
        
        #region Validation

        /// <summary>
        /// بررسی محتوای پیامک (فیلتر کلمات)
        /// </summary>
        /// <param name="message">متن پیامک</param>
        /// <param name="cancellationToken">توکن لغو</param>
        public async Task<CheckContentResult> CheckSmsContentAsync(
            string message,
            CancellationToken cancellationToken = default)
        {
            var payload = new
            {
                message
            };

            return await PostAsync<CheckContentResult>("/CheckSmsContent", payload, cancellationToken);
        }

        #endregion
        
        #region Private Methods

        private async Task<T> PostAsync<T>(
            string endpoint,
            object payload,
            CancellationToken cancellationToken)
        {
            var url = BaseUrl.TrimEnd('/') + endpoint;
            var json = JsonConvert.SerializeObject(payload);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            using var request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Content = content;
            request.Headers.Add("X-API-Key", _apiKey);

            using var response = await _httpClient.SendAsync(request, cancellationToken);
            var responseJson = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new NiazpardazApiException(
                    $"خطا در ارتباط با سرور: {response.StatusCode}",
                    (int)response.StatusCode);
            }

            var apiResponse = JsonConvert.DeserializeObject<ApiResponse<T>>(responseJson);

            if (apiResponse == null)
            {
                throw new NiazpardazApiException("پاسخ نامعتبر از سرور");
            }

            if (!apiResponse.Success)
            {
                throw new NiazpardazApiException(
                    apiResponse.ErrorMessage ?? "خطای نامشخص");
            }

            return apiResponse.Result;
        }

        #endregion

        #region IDisposable

        /// <summary>
        /// آزادسازی منابع
        /// </summary>
        public void Dispose()
        {
            if (_disposeHttpClient)
            {
                _httpClient?.Dispose();
            }
        }

        #endregion
    }
}