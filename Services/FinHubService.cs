using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace FinDashboard.API.Services
{
    public class FinHubService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public FinHubService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiKey = configuration["FinHub:ApiKey"]; // Fetch API Key from config
        }

        public async Task<FinHubQuoteResponse> GetCurrentStockPriceAsync(string stockSymbol)
        {
            // Construct the API URL
            var url = $"https://finnhub.io/api/v1/quote?symbol={stockSymbol}&token={_apiKey}";

            // Make an HTTP GET request
            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                // Parse the JSON response
                var json = await response.Content.ReadAsStringAsync();
                var stockData = JsonSerializer.Deserialize<FinHubQuoteResponse>(json);
                if (stockData != null)
                return stockData; 
            }

            throw new Exception($"Failed to fetch stock price for {stockSymbol}");
        }

        // Class to deserialize FinHub's JSON response
        public class FinHubQuoteResponse
        {
            public decimal c { get; set; }
            public decimal o { get; set; } // Open price
            public decimal h { get; set; } // High price
            public decimal l { get; set; } // Low price
            public decimal pc { get; set; } // Previous close price
        }
    }
}
