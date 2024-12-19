using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace FinDashboard.API.Services
{
    public class FinHubService
    {
        private readonly HttpClient _httpClient;
        private readonly string ?_apiKey;

        public FinHubService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiKey = configuration["FinHub:ApiKey"]; 
        }

        public async Task<FinHubQuoteResponse> GetCurrentStockPriceAsync(string stockSymbol)
        {
            var url = $"https://finnhub.io/api/v1/quote?symbol={stockSymbol}&token={_apiKey}";

            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var stockData = JsonSerializer.Deserialize<FinHubQuoteResponse>(json);
                if (stockData != null)
                return stockData; 
            }

            throw new Exception($"Failed to fetch stock price for {stockSymbol}");
        }

        public class FinHubQuoteResponse
        {
            public decimal c { get; set; }
            public decimal o { get; set; } 
            public decimal h { get; set; } 
            public decimal l { get; set; } 
            public decimal pc { get; set; } 
        }
    }
}
