using InvoiceService.Models;

public class InvoiceRepository : IInvoiceRepository
{
    private readonly examContext db;
    public InvoiceRepository(examContext db)
    {
        this.db = db;
    }
    public List<Invoice> FetchInvoices()
    {
        return db.Invoices.Where(z => z.TotalAmount != null).GroupBy(z => new { z.Month, z.Year })
        .Select(z => new Invoice
        {
            Id = z.OrderBy(z=>z.IssuingDate).Select(x => x.Id).LastOrDefault(),
            IssuingDate = z.OrderBy(z=>z.IssuingDate).Select(x => x.IssuingDate).LastOrDefault(),
            Month = z.OrderBy(z=>z.IssuingDate).Select(x => x.Month).LastOrDefault(),
            Year = z.OrderBy(z=>z.IssuingDate).Select(x => x.Year).LastOrDefault(),
            TotalAmount = z.OrderBy(z=>z.IssuingDate).Select(x => x.TotalAmount).LastOrDefault(),
        }).ToList();
    }

    public void GenerateInvoice()
    {
        Invoice invoice = new Invoice();
        invoice.IssuingDate = DateTime.Now.Date;
        db.Invoices.Add(invoice);
        db.SaveChanges();
    }
}
