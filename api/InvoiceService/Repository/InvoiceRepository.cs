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
        return db.Invoices.Where(z => z.TotalAmount != null).ToList();
    }

    public void GenerateInvoice()
    {
        Invoice invoice = new Invoice();
        invoice.IssuingDate = DateTime.Now.Date;
        db.Invoices.Add(invoice);
        db.SaveChanges();
    }
}
