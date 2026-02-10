# NiazpardazSms SDK for .NET

کتابخانه رسمی .NET برای کار با API پیامکی نیازپرداز

[![NuGet](https://img.shields.io/nuget/v/NiazpardazSms.svg)](https://www.nuget.org/packages/NiazpardazSms)
[![License](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE)

## نصب

### Package Manager
```
Install-Package NiazpardazSms
```

### .NET CLI
```bash
dotnet add package NiazpardazSms
```

## شروع سریع

```csharp
using NiazpardazSms;

// ساخت کلاینت با API Key
var client = new NiazpardazSmsClient("YOUR_API_KEY");

// ارسال پیامک
var result = await client.SendAsync(
    fromNumber: "10001234",
    toNumber: "09123456789",
    message: "سلام از نیازپرداز!"
);

if (result.ResultCode == SendResultCode.SendWasSuccessful)
{
    Console.WriteLine($"BatchSmsId: {result.BatchSmsId}");
}
```

## امکانات

### ارسال پیامک تکی

```csharp
using NiazpardazSms;

var result = await client.SendAsync(
    fromNumber: "10001234",
    toNumber: "09123456789",
    message: "متن پیامک",
    isFlash: false,
    sendDelay: null
);

Console.WriteLine($"شناسه ارسال: {result.BatchSmsId}");
Console.WriteLine($"وضعیت: {result.ResultCode}");
```

### ارسال گروهی (یک متن به چند شماره)

```csharp
using NiazpardazSms;

var result = await client.SendBulkAsync(
    fromNumber: "10001234",
    toNumbers: new[] { "09123456789", "09198765432" },
    message: "پیام گروهی",
    isFlash: false
);
```

### ارسال LikeToLike (هر شماره پیام مخصوص خودش)

```csharp
using NiazpardazSms;

var result = await client.SendSmsLikeToLikeAsync(
    fromNumber: "10001234",
    toNumbers: new[] { "09123456789", "09198765432" },
    messages: new[] { "سلام علی جان", "سلام رضا جان" }
);

Console.WriteLine($"SmsId: {result.SmsId}");
```

### ارسال پیامک صوتی OTP

```csharp
using NiazpardazSms;

var result = await client.SendVoiceOtpAsync(
    fromNumber: "10001234",
    toNumber: "09123456789",
    otp: "12345"
);
```

### گزارش تحویل

```csharp
using NiazpardazSms;

// گزارش تحویل ارسال گروهی
var delivery = await client.GetBatchDeliveryAsync(
    batchSmsId: 123456,
    index: 0,
    count: 100
);

if (delivery.ResultCode == DeliveryResultCode.Success)
{
    for (int i = 0; i < delivery.Numbers.Length; i++)
    {
        Console.WriteLine($"{delivery.Numbers[i]}: {delivery.DeliveryStatus[i]}");
    }
}

// گزارش تحویل LikeToLike
var delivery2 = await client.GetDeliveryLikeToLikeAsync(smsId: 789);
```

### اعتبار و اطلاعات حساب

```csharp
using NiazpardazSms;

// دریافت اعتبار
var credit = await client.GetCreditAsync();
if (credit.ResultCode == CreditResultCode.Success)
{
    Console.WriteLine($"اعتبار: {credit.Credit}");
}

// دریافت شماره‌های فرستنده
var senders = await client.GetSenderNumbersAsync();
foreach (var sender in senders.Senders)
{
    Console.WriteLine(sender);
}
```

### دریافت پیامک‌ها

```csharp
using NiazpardazSms;

// تعداد پیامک‌های دریافتی
var inboxCount = await client.GetInboxCountAsync(isRead: false);
Console.WriteLine($"تعداد: {inboxCount.InboxCount}");

// لیست پیامک‌ها
var messages = await client.GetMessagesAsync(
    messageType: 1,
    fromNumbers: "10001234",
    index: 0,
    count: 50
);

// پیامک‌ها بر اساس تاریخ
var messages2 = await client.GetMessagesByDateRangeAsync(
    messageType: 1,
    fromNumbers: "10001234",
    fromDate: "1403/01/01",
    toDate: "1403/01/31"
);
```

### لیست سیاه مخابرات

```csharp
using NiazpardazSms;

// بررسی یک شماره
var isBlack = await client.NumberIsInTelecomBlacklistAsync("09123456789");
Console.WriteLine($"در لیست سیاه: {isBlack.IsBlack}");

// استخراج شماره‌های لیست سیاه از یک لیست
var blacklist = await client.ExtractTelecomBlacklistNumbersAsync(
    new[] { "09123456789", "09198765432", "09351234567" }
);
```

### بررسی محتوای پیامک

```csharp
using NiazpardazSms;

var check = await client.CheckSmsContentAsync("متن پیامک تست");
Console.WriteLine($"متن معتبر است: {check.IsValid}");
```

---

## کدهای نتیجه

### کدهای نتیجه ارسال (SendResultCode)

| کد | مقدار | توضیحات |
|---:|-------|--------:|
| 0 | SendWasSuccessful | ارسال موفق ✅ |
| 1 | InvalidUserNameOrPassword | نام کاربر یا رمز نامعتبر |
| 2 | UserBlocked | کاربر مسدود |
| 3 | InvalidSenderNumber | شماره فرستنده نامعتبر |
| 4 | LimitationInDailySend | محدودیت روزانه |
| 5 | LimitationInRecieverCount | حداکثر 1000 گیرنده |
| 6 | SenderLineIsInactive | خط غیرفعال |
| 7 | SmsContentFilteredWordsIsIncluded | کلمات فیلتر شده |
| 8 | NoCredit | اعتبار ناکافی |
| 9 | SystemBeingUpdated | در حال بروزرسانی |
| 10 | WebServiceNoActive | وب سرویس غیرفعال |
| 11 | NotImplemented | پیاده سازی نشده |
| 12 | LikeToLikeSendReceiverAndMessageCountShouldEqual | تعداد پیام و شماره نابرابر |
| 13 | LimitationInMesssageContentCount | حداکثر 100 پیام |
| 14 | UserTariffNotDetermined | تعرفه تعریف نشده |
| 15 | DuplicateSendSms | ارسال تکراری |
| 16 | InvalidNumberEmptyOrBlackList | شماره نامعتبر یا بلاک لیست |
| 17 | TextNotFound | متن خالی |
| 18 | NotValidTemplateFound | مغایرت با قالب |
| 19 | UserExpired | کاربر منقضی |
| 20 | UserIsNotActive | کاربر غیرفعال |
| 21 | InvalidParameters | پارامتر نامعتبر |
| 22 | IpBlocked | آی پی بلاک شده |
| 23 | EnqueueFailed | خطا در صف ارسال |
| 24 | DuplicateRequestThreshold | درخواست تکراری |
| 25 | InvalidApiKey | ApiKey نامعتبر |
| 26 | ErrorCreateVoiceFile | خطا در ساخت فایل صوتی |

### کدهای نتیجه گزارش تحویل (DeliveryResultCode)

| کد | مقدار | توضیحات |
|---:|-------|--------:|
| 0 | Success | موفق ✅ |
| -1 | ServiceConnectionError | خطا در ارتباط با سرویس دهنده |
| -2 | InvalidBatchSmsId | پیام با این کد وجود ندارد |
| -3 | ReportExpired | مهلت یک هفته ای گزارش پایان یافته |
| -4 | MessageInQueue | پیام در صف ارسال مخابرات است |
| -5 | TooEarly | حداقل یک دقیقه بعد از ارسال اقدام نمایید |
| -6 | IpBlocked | آی پی بلاک شده |
| -7 | InvalidApiKey | ApiKey نامعتبر |

### وضعیت تحویل پیامک (SmsDeliveryStatus)

| کد | مقدار | توضیحات |
|---:|-------|--------:|
| 0 | SentToTelecom | ارسال شده به مخابرات |
| 1 | Delivered | رسیده به گوشی ✅ |
| 2 | NotDelivered | نرسیده به گوشی |
| 3 | SmsFailed | خطای مخابراتی |
| 4 | UnknownError | خطای نامشخص |
| 5 | ReceivedByTelecom | رسیده به مخابرات |
| 6 | NotReceivedByTelecom | نرسیده به مخابرات |
| 7 | BlackListed | مسدود شده توسط مقصد |
| 8 | Unknown | نامشخص |
| 9 | RejectedByTelecom | مخابرات پیام را مردود اعلام کرد |
| 10 | Canceled | کنسل شده توسط اپراتور |
| 11 | NotSent | ارسال نشده |
| 12 | NoTelegram | تلگرام ندارد |
| 13 | InQueue | در صف ارسال |

### کدهای نتیجه اعتبار (CreditResultCode)

| کد | مقدار | توضیحات |
|---:|-------|--------:|
| 0 | Success | موفق ✅ |
| -1 | InvalidCredentials | نام کاربری و رمز عبور صحیح نمی باشد |
| -2 | UserDisabled | کاربر غیرفعال می باشد |
| -6 | IpBlocked | آی پی بلاک شده |
| -7 | InvalidApiKey | ApiKey نامعتبر |

### کدهای نتیجه لیست سیاه (BlacklistResultCode)

| کد | مقدار | توضیحات |
|---:|-------|--------:|
| 0 | Success | موفق ✅ |
| -1 | InvalidCredentials | نام کاربری و رمز عبور صحیح نمی باشد |
| -2 | UserDisabled | کاربر غیرفعال می باشد |
| -3 | EmptyNumbersArray | آرایه شماره ها خالی می باشد |
| -4 | MaxNumbersExceeded | حداکثر 1000 شماره |
| -6 | IpBlocked | آی پی بلاک شده |
| -7 | InvalidApiKey | ApiKey نامعتبر |

---

## مدیریت خطا

```csharp
using NiazpardazSms.Exceptions;

try
{
    var result = await client.SendAsync("10001234", "09123456789", "تست");
    
    if (result.ResultCode != SendResultCode.SendWasSuccessful)
    {
        Console.WriteLine($"خطا: {result.ResultCode}");
    }
}
catch (NiazpardazApiException ex)
{
    Console.WriteLine($"خطای API: {ex.Message}");
    Console.WriteLine($"کد خطا: {ex.ErrorCode}");
}
catch (HttpRequestException ex)
{
    Console.WriteLine($"خطای شبکه: {ex.Message}");
}
```

---

## تنظیمات پیشرفته

### استفاده از HttpClient سفارشی

```csharp
var httpClient = new HttpClient();
httpClient.Timeout = TimeSpan.FromSeconds(30);

var client = new NiazpardazSmsClient("YOUR_API_KEY", httpClient);
```

### Dependency Injection

```csharp
// Program.cs
services.AddHttpClient<NiazpardazSmsClient>();
services.AddSingleton(sp => 
{
    var httpClient = sp.GetRequiredService<IHttpClientFactory>()
        .CreateClient(nameof(NiazpardazSmsClient));
    return new NiazpardazSmsClient("YOUR_API_KEY", httpClient);
});
```

---

## سازگاری

- .NET Framework 4.6.1+
- .NET Core 2.0+
- .NET 5/6/7/8/9/10+
- Xamarin
- Unity

## مجوز

MIT License

## پشتیبانی

- 📚 مستندات: https://niazpardaz-sms.com/webservice
- 🐛 گزارش باگ: [GitHub Issues](https://github.com/NiazpardazSms/niazpardaz-sms-dotnet/issues)