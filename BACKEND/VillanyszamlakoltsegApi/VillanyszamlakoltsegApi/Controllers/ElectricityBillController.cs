using Microsoft.AspNetCore.Mvc;
using VillanyszamlakoltsegApi.Models;

namespace VillanyszamlakoltsegApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class ElectricityBillController : ControllerBase
    {
        private const decimal SystemFeePerKwh = 23.4m;
        private const decimal DiscountRate = 0.13m;
        private const decimal DiscountThreshold = 350_000m;
        [HttpPost("calculate")]        

        public ActionResult<ElectricityBillResponse> Calculate([FromBody] ElectricityBillRequest req)
        {
            // Parse-oljuk a mátrixot
            req.ParseMatrix();

            // Kiszámoljuk a díjakat rendszerhasználati díjjal és kedvezménnyel
            var years = req.Years;
            var consumptions = req.Consumptions;
            int yc = years.Length;
            int mc = consumptions.GetLength(0);

            var baseMonthly = new decimal[yc, mc];
            var yearlySum = new decimal[yc];
            for (int y = 0; y < yc; y++)
            {
                for (int m = 0; m < mc; m++)
                {
                    decimal cost = consumptions[m, y]
                                 * (req.UnitPrice + SystemFeePerKwh);
                    baseMonthly[y, m] = cost;
                    yearlySum[y] += cost;
                }
            }

            
        }
    }
}
