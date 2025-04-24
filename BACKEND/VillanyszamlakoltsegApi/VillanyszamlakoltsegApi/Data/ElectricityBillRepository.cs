using VillanyszamlakoltsegApi.Models;

namespace VillanyszamlakoltsegApi.Data
{
    public class ElectricityBillRepository
    {
        ElectricityBillRequest bill;
        public ElectricityBillRepository() 
        {
            bill = new ElectricityBillRequest();
        }

        public void Create(ElectricityBillRequest bill)
        {
            this.bill = bill;
        }

        public ElectricityBillRequest Read()
        {
            return this.bill;
        }

        
    }
}
