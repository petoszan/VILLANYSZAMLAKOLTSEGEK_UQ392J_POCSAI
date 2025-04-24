using System.ComponentModel.DataAnnotations;

namespace VillanyszamlakoltsegApi.Models
{
    public class ElectricityBillRequest
    {
       
        public decimal UnitPrice { get; set; }
        public string MatrixCsv { get; set; }



    }
}
