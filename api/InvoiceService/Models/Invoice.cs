using System;
using System.Collections.Generic;

namespace InvoiceService.Models
{
    public partial class Invoice
    {
        public decimal Id { get; set; }
        public DateTime? IssuingDate { get; set; }
        public int? Month { get; set; }
        public int? Year { get; set; }
        public decimal? TotalAmount { get; set; }
    }
}
