using Microsoft.AspNetCore.Mvc;
using VillanyszamlakoltsegApi.Models;

[ApiController]
[Route("api/[controller]")]
public class ElectricityBillController : ControllerBase
{
    private const decimal SystemFee = 23.4m;
    private const decimal DiscountRate = 0.13m;
    private const decimal DiscountThreshold = 350_000m;

    [HttpPost("calculate")]
    public ActionResult<ElectricityBillResponse> Calculate([FromBody] ElectricityBillRequest req)
    {
        // 1. Parse
        req.ParseMatrix();

        var years = req.Years;
        var consumptions = req.Consumptions;
        int yc = years.Length;
        int mc = consumptions.GetLength(0);

        // 2. Alap havi díjak
        var baseMonthly = new decimal[yc, mc];
        var yearlySum = new decimal[yc];
        for (int y = 0; y < yc; y++)
            for (int m = 0; m < mc; m++)
            {
                var cost = consumptions[m, y] * req.UnitPrice + SystemFee;
                baseMonthly[y, m] = cost;
                yearlySum[y] += cost;
            }

        // 3. Kedvezmény
        var finalMonthly = new Dictionary<(int Year, int Month), decimal>();
        for (int y = 0; y < yc; y++)
        {
            bool hasDiscount = y >= 2  && yearlySum[y - 1] > DiscountThreshold && yearlySum[y - 2] > DiscountThreshold;

            for (int m = 0; m < mc; m++)
            {
                var cost = baseMonthly[y, m];
                if (hasDiscount)
                    cost = Math.Round(cost * (1 - DiscountRate), 2);

                finalMonthly[(years[y], m + 1)] = cost;
            }
        }

        // 4. Éves összesítés
        var yearlyCosts = finalMonthly
            .GroupBy(kv => kv.Key.Year)
            .ToDictionary(g => g.Key, g => g.Sum(kv => kv.Value));

        // 5. Válasz összeállítása
        var resp = new ElectricityBillResponse
        {
            YearlyCosts = yearlyCosts,
            MonthlyCosts = finalMonthly.ToDictionary(
                               kvp => $"{kvp.Key.Year}-{kvp.Key.Month:D2}",
                               kvp => kvp.Value),
            TotalCost = yearlyCosts.Values.Sum()
        };

        return Ok(resp);
    }
}
