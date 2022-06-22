using FinanceNewsStream.Models;
using FinanceNewsStream.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FinanceNewsStream.Pages
{
    public class IndexModel : PageModel
    {

        public FinanceNews news;

        private readonly ILogger<IndexModel> _logger;
        private readonly InterfaceNewsService _newsService;

        public IndexModel(ILogger<IndexModel> logger, InterfaceNewsService newsService)
        {
            _logger = logger;
            _newsService = newsService;
        }

        public void OnGet()
        {
            news = _newsService.GetLatestFinanceNews(0);
        }

        public void OnGetLoadMoreNews(int offset)
        {
            news = _newsService.GetLatestFinanceNews(offset);
        }
    }
}