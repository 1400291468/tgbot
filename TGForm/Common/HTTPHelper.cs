using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace TGForm.Common
{


    public sealed record LicenseStatus(
    string Status,
    bool IsValid,
    long ServerTime,
    long ActivationTime,
    long ExpiresAt,
    long RemainingSeconds);
    public static class LicenseApi
    {
        // 本机授权服务必须直连，避免被系统代理转发后返回502。
        private static readonly HttpClient Http = new(new HttpClientHandler { UseProxy = false })
        {
            BaseAddress = new Uri("http://localhost:5005/"),
            Timeout = TimeSpan.FromSeconds(15)
        };
        private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web);
        public static Task<LicenseStatus> ActivateAsync(
            string activationCode,
            string deviceId,
            CancellationToken cancellationToken = default)
        {
            return RequestAsync(
                "api/tgFormActivationCode/activate",
                activationCode,
                deviceId,
                cancellationToken);
        }
        public static Task<LicenseStatus> GetStatusAsync(
            string activationCode,
            string deviceId,
            CancellationToken cancellationToken = default)
        {
            try
            {
                return RequestAsync(
         "api/tgFormActivationCode/activationStatus",
         activationCode,
         deviceId,
         cancellationToken);
            }
            catch (Exception)
            {
                return default;
            }

        }
        private static async Task<LicenseStatus> RequestAsync(
            string path,
            string activationCode,
            string deviceId,
            CancellationToken cancellationToken)
        {
            using var response = await Http.PostAsJsonAsync(
                path,
                new
                {
                    activationCode = activationCode.Trim(),
                    deviceId
                },
                cancellationToken);
            response.EnsureSuccessStatusCode();
            using var document = await JsonDocument.ParseAsync(
                await response.Content.ReadAsStreamAsync(cancellationToken),
                cancellationToken: cancellationToken);
            var root = document.RootElement;
            // HTTP 200 不代表业务成功，还需要检查 Admin.NET 的业务码。
            if (!root.TryGetProperty("code", out var code)
                || !code.TryGetInt32(out var businessCode)
                || businessCode != 200)
            {
                var message = root.TryGetProperty("message", out var value)
                    ? value.ToString()
                    : "服务器返回了无法识别的响应";
                throw new InvalidOperationException(message);
            }
            if (!root.TryGetProperty("result", out var result)
                || result.ValueKind != JsonValueKind.Object)
            {
                throw new InvalidOperationException("服务器未返回授权状态");
            }
            return result.Deserialize<LicenseStatus>(JsonOptions)
                ?? throw new InvalidOperationException("授权状态解析失败");
        }
    }
}
