
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace BTCh11_Bai5.Services
{
    public sealed class TyGiaService
    {
        private static readonly HttpClient Http = new HttpClient
        {
            Timeout = TimeSpan.FromSeconds(8)
        };

        private readonly ConcurrentDictionary<string,
            (decimal Rate, DateTime Expiry)> cache = new();

        private readonly ConcurrentDictionary<string, SemaphoreSlim>
            locks = new();

        public async Task<decimal> LayTyGiaAsync(string ngoaiTe)
        {
            if (string.IsNullOrWhiteSpace(ngoaiTe))
                throw new ArgumentException("Chưa chọn ngoại tệ.");

            string code = ngoaiTe.Trim().ToLowerInvariant();

            if (code == "vnd")
                return 1m;

            if (code != "thb" && code != "krw" &&
                code != "jpy" && code != "usd")
            {
                throw new ArgumentException(
                    "Ngoại tệ không được hỗ trợ.");
            }

            // Kiểm tra cache 10 phút
            if (cache.TryGetValue(code, out var saved) &&
                saved.Expiry > DateTime.UtcNow)
            {
                return saved.Rate;
            }

            var gate = locks.GetOrAdd(
                code, _ => new SemaphoreSlim(1, 1));

            await gate.WaitAsync();

            try
            {
                // Kiểm tra lại cache sau khi chờ
                if (cache.TryGetValue(code, out saved) &&
                    saved.Expiry > DateTime.UtcNow)
                {
                    return saved.Rate;
                }

                string[] urls =
                {
                    "https://cdn.jsdelivr.net/npm/@fawazahmed0/currency-api@latest/v1/currencies/" + code + ".json",
                    "https://latest.currency-api.pages.dev/v1/currencies/" + code + ".json"
                };

                Exception lastError = null;

                foreach (string url in urls)
                {
                    try
                    {
                        using var response = await Http.GetAsync(url);
                        response.EnsureSuccessStatusCode();

                        string json =
                            await response.Content.ReadAsStringAsync();

                        using var doc = JsonDocument.Parse(json);

                        decimal rate = doc.RootElement
                            .GetProperty(code)
                            .GetProperty("vnd")
                            .GetDecimal();

                        if (rate <= 0)
                        {
                            throw new FormatException(
                                "Tỷ giá API không hợp lệ.");
                        }

                        // Lưu cache trong 10 phút
                        cache[code] = (
                            rate,
                            DateTime.UtcNow.AddMinutes(10)
                        );

                        return rate;
                    }
                    catch (Exception ex) when (
                        ex is HttpRequestException ||
                        ex is TaskCanceledException ||
                        ex is JsonException ||
                        ex is KeyNotFoundException ||
                        ex is FormatException ||
                        ex is InvalidOperationException ||
                        ex is OverflowException)
                    {
                        lastError = ex;
                        // Thử API tiếp theo
                    }
                }

                throw new InvalidOperationException(
                    "Cả API chính và API dự phòng đều lỗi. " +
                    "Vui lòng kiểm tra kết nối Internet.",
                    lastError);
            }
            finally
            {
                gate.Release();
            }
        }
    }
}
