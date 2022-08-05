public class ResponsePayload<T>
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public T Data { get; set; }

    public ResponsePayload(bool success, string message, T data)
    {
        this.Success = success;
        this.Message = message;
        this.Data = data;
    }
}