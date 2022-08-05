using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class InvoiceController : ControllerBase
{
    private readonly IInvoiceRepository invoiceRepository;
    public InvoiceController(IInvoiceRepository invoiceRepository)
    {
        this.invoiceRepository = invoiceRepository;
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
}