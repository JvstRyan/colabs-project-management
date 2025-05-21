namespace Colabs.ProjectManagement.Application.Responses
{
    public class BaseResponse
    {
        public BaseResponse()
        {
            Success = true;
            StatusCode = 200;
        }

        public BaseResponse(string message, int statusCode = 200)
        {
            Success = statusCode >= 200 && statusCode < 300;
            Message = message;
            StatusCode = statusCode;
        }

        public BaseResponse(string message, bool success)
        {
            Success = success;
            Message = message;
        }

        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public List<string>? ValidationErrors { get; set; }
        public int StatusCode {get; set;}
    }
}
