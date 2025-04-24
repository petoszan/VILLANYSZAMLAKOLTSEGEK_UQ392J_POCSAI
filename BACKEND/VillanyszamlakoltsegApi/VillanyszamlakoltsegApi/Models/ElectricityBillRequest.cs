using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace VillanyszamlakoltsegApi.Models
{
    public class ElectricityBillRequest
    {
       
        public decimal UnitPrice { get; set; }
        public string MatrixCsv { get; set; }

        public int[] Years { get; private set; }

        public decimal[,] Consumptions { get; private set; }


        public void ParseMatrix()
        {
            var lines = MatrixCsv
                .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries); //StringSplitOptions.RemoveEmptyEntries space-k törlése

            //if (lines.Length < 2)
            //throw new ArgumentException("Legalább fejléc és egy sor kell legyen a CSV-ben.", nameof(MatrixCsv));

            // Fejléc: évek
            Years = lines[0]
                .Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(s => int.Parse(s))
                .ToArray();

            int yearCount = Years.Length;
            int monthCount = lines.Length - 1;
            Consumptions = new decimal[monthCount, yearCount];

            for (int m = 0; m < monthCount; m++)
            {
                var parts = lines[m + 1].Split(',', StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length != yearCount)
                    throw new ArgumentException($"A(z) {m + 1}. CSV-sor nem megfelelő elemszámú.", nameof(MatrixCsv));

                for (int y = 0; y < yearCount; y++)
                {
                    Consumptions[m, y] = decimal.Parse(parts[y], CultureInfo.InvariantCulture);
                }
            }
        }
    }
}
