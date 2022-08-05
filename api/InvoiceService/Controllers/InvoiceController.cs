using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

[ApiController]
[Route("api/[controller]")]
public class InvoiceController : ControllerBase
{
    private readonly IInvoiceRepository invoiceRepository;
    private readonly IHubContext<InvoiceHub> hubContext;
    public InvoiceController(IInvoiceRepository invoiceRepository, IHubContext<InvoiceHub> hubContext)
    {
        this.invoiceRepository = invoiceRepository;
        this.hubContext = hubContext;
    }
    [HttpGet]
    public IActionResult GetInvoices()
    {
        try
        {
            return Ok(this.invoiceRepository.FetchInvoices());
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [HttpPost]
    public IActionResult GenerateInvoice()
    {
        try
        {
            invoiceRepository.GenerateInvoice();
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    // web socket push all changes in invoice
    [HttpGet]
    [Route("notify")]
    public IActionResult NotifyAll()
    {
        this.hubContext.Clients.All.SendAsync("RefreshInvoiceList");

        return Ok();
    }
}