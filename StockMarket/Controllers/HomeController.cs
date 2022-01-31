using Microsoft.AspNetCore.Mvc;
using StockMarket.Custom;
using StockMarket.Models;
using StockMarket.Data;
using System.Diagnostics;
using System.Net;


namespace StockMarket.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        StockData _stockData;
        private readonly ApplicationDbContext _context;

        public HomeController( ILogger<HomeController> logger , ApplicationDbContext context)
        {
            _stockData = new StockData();
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View( );
        }

        [HttpGet]
        public async Task<IActionResult> DisplayStock()
        {
            return View( );
        }

        [HttpPost]
        public async Task<ActionResult> DisplayStock( string symbol, DateTime startDate, DateTime EndDate )
        {
            startDate = DateTime.MinValue;
            EndDate = DateTime.Now;
            /*            string data = await _stockData.getStockData(symbol, startDate, EndDate);
                        List<StockDisplay> Data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<StockDisplay>>(data);
                        _context.stocks.AddRange(Data);
                        _context.SaveChanges( );*/
            string data = Newtonsoft.Json.JsonConvert.SerializeObject(_context.stocks.ToList());
            return Ok(data);
        }


        [HttpPost]
        public async Task<ActionResult> Last14DaysVolumeAvg( )
        {
            var Data = _context.stocks.OrderByDescending(t => t.Id).Take(14).ToList().Average(x=> x.Volume);
            var sampleData = new StockPrediction.ModelInput()
            {
                Id = _context.stocks.Count() + 1,
                Date = DateTime.Now.ToString(),
                Volume = (float) Data,
            };

            var result = StockPrediction.Predict(sampleData);
            
            string data = result.Score.ToString();
            return Ok(data);
        }


        public IActionResult Privacy()
        {
            return View( );
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}