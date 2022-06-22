using FinanceNewsStream.Models;
using Newtonsoft.Json;

namespace FinanceNewsStream.Services
{
    public interface InterfaceNewsService
    {
        FinanceNews GetLatestFinanceNews(int offset);
    }

    public class FinanceNewsService : InterfaceNewsService
    {
        private readonly IConfiguration _configuration;
        public FinanceNewsService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public FinanceNews GetLatestFinanceNews(int offset)
        {
            // Here we start fetching data from te API.
            string apikey = _configuration.GetValue<string>("API_KEY");
            string baseUrl = _configuration.GetValue<string>("API_URL");

            using(var httpclient = new HttpClient())
            {
                httpclient.BaseAddress = new Uri(baseUrl);

                var parameters = string.Format("?apikey={0}&offset={1}&date={2}&sort={3}", apikey, offset, "today", "desc");

                HttpResponseMessage response = httpclient.GetAsync(parameters).Result;

                // Check if the response was sucessfull.
                if(response.IsSuccessStatusCode)
                {
                    var responseData = response.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<FinanceNews>(responseData);
                } else
                {
                    return new FinanceNews()
                    {
                        Data = new List<NewsArticle>(),
                        Pagination = new Pagination()
                    };
                }
            }
        }
    }
}
