namespace Gob.Web.Models;

public class ErrorViewModel
{
    public string RequestId { get; set; } = "Error";

    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
}