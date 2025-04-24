namespace VillanyszamlakoltsegApi.Models
{
    public class ElectricityBillResponse
    {
        // Kulcs: év → érték: összes éves költség
        public Dictionary<int, decimal> YearlyCosts { get; set; }

        // Kulcs: (év, hónap) → havi költség
        public Dictionary<string, decimal> MonthlyCosts { get; set; }

        public decimal TotalCost { get; set; }
    }
}
