using System.Text.Json;

namespace BTCh11_Bai2.Services;

/// <summary>Item dùng cho ComboBox: DisplayMember = Name, ValueMember = Code</summary>
public record AddressItem(int Code, string Name);

public class ProvinceService
{
    // Một HttpClient dùng chung cho toàn ứng dụng, Timeout 10 giây
    private static readonly HttpClient _http = new() { Timeout = TimeSpan.FromSeconds(10) };
    private readonly string _baseUrl;

    public ProvinceService(string baseUrl)
        => _baseUrl = baseUrl.EndsWith('/') ? baseUrl : baseUrl + "/";

    /// <summary>GET https://provinces.open-api.vn/api/v2/</summary>
    public async Task<List<AddressItem>> GetProvincesAsync()
    {
        var json = await _http.GetStringAsync(_baseUrl);
        using var doc = JsonDocument.Parse(json);
        return doc.RootElement.EnumerateArray().Select(ToItem).ToList();
    }

    /// <summary>GET .../p/{code}?depth=2 → danh sách phường/xã của tỉnh</summary>
    public async Task<List<AddressItem>> GetWardsAsync(int provinceCode)
    {
        var json = await _http.GetStringAsync($"{_baseUrl}p/{provinceCode}?depth=2");
        using var doc = JsonDocument.Parse(json);
        var root = doc.RootElement;
        if (!root.TryGetProperty("wards", out var wards))
            return new List<AddressItem>();
        return wards.EnumerateArray().Select(ToItem).ToList();
    }

    private static AddressItem ToItem(JsonElement e)
        => new(e.GetProperty("code").GetInt32(), e.GetProperty("name").GetString() ?? "");
}
