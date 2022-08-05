using InvoiceService.Models;

public interface IInvoiceRepository
{
    List<Invoice> FetchInvoices();
    void GenerateInvoice();
}