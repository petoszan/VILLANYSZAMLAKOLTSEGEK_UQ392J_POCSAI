// Models/CalculateRequest.cs
using System;
using System.Linq;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace VillanyszamlakoltsegApi.Models
{
    public class ElectricityBillRequest
    {
        public decimal UnitPrice { get; set; }

       
        public string[] MatrixRows { get; set; }

     
        public int[]? Years { get; private set; }

       
        public decimal[,]? Consumptions { get; private set; }

        public void ParseMatrix()
        {
            if (MatrixRows == null || MatrixRows.Length < 2)
                throw new ArgumentException("Legalább fejléc és egy sor kell legyen a MatrixRows-ban.");

            // Fejléc
            Years = MatrixRows[0]
                .Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            int yc = Years.Length;
            int mc = MatrixRows.Length - 1;
            Consumptions = new decimal[mc, yc];

            for (int m = 0; m < mc; m++)
            {
                var parts = MatrixRows[m + 1]
                    .Split(',', StringSplitOptions.RemoveEmptyEntries);

                if (parts.Length != yc)
                    throw new ArgumentException($"A {m + 1}. sor nem megfelelő elemszámú.");

                for (int y = 0; y < yc; y++)
                    Consumptions[m, y] = decimal.Parse(parts[y], System.Globalization.CultureInfo.InvariantCulture);
            }
        }
    }
}
