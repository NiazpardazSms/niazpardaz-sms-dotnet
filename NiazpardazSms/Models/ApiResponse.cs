using Newtonsoft.Json;

namespace NiazpardazSms.Models
{
    /// <summary>
    /// پاسخ عمومی API
    /// </summary>
    /// <typeparam name="T">نوع داده برگشتی</typeparam>
    internal class ApiResponse<T>
    {
        /// <summary>
        /// آیا درخواست موفق بود؟
        /// </summary>
        [JsonProperty("success")]
        public bool Success { get; set; }

        /// <summary>
        /// پیام خطا
        /// </summary>
        [JsonProperty("errorMessage")]
        public string ErrorMessage { get; set; }

        /// <summary>
        /// داده برگشتی
        /// </summary>
        [JsonProperty("result")]
        public T Result { get; set; }
    }
}