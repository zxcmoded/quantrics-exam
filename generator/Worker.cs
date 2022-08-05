using job.Models;

namespace job;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;

    public Worker(ILogger<Worker> logger)
    {
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            GenerateInvoice();
            await Task.Delay(1000, stoppingToken);
        }
    }

    public void GenerateInvoice()
    {
        using (var db = new examContext())
        {
            var invoices = db.Invoices.Where(z => z.TotalAmount == null).ToList();
            foreach (var invoice in invoices)
            {
                var issuingDate = invoice.IssuingDate.Value.Date;

                // Get assets and sum all the prices
                var totalAssetPrice = db.Assets
                    .Where(z => (z.ValidFrom == null && z.ValidTo == null)
                    || (z.ValidFrom.Value.Date.Month == issuingDate.Month && z.ValidFrom.Value.Date.Year == issuingDate.Year)
                    || (z.ValidTo.Value.Date.Month == issuingDate.Month && z.ValidTo.Value.Date.Year == issuingDate.Year))
                    .Sum(z => z.Price);

                invoice.Month = issuingDate.Month;
                invoice.Year = issuingDate.Year;
                invoice.TotalAmount = totalAssetPrice;
                db.SaveChanges();

                NotifyInvoiceChanges();
            }
        }
    }
    public void NotifyInvoiceChanges()
    {
        using (var client = new HttpClient())
        {
            client.BaseAddress = new Uri("http://localhost:5298/api/Invoice/notify");
            //HTTP GET
            var responseTask = client.GetAsync("");
            responseTask.Wait();

            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                // todo log success
            }
        }
    }
}
