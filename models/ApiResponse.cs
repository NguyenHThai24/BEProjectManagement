using ProjectManagement.Services;

namespace ProjectManagement.Models
{
    public class ApiResponse
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public object? Data { get; set; }

        public ApiResponse(int errorCode, string? message = null, object? data = null)
        {
            Code = errorCode;
            Message = message ?? ErrorCodes.Messages.GetValueOrDefault(errorCode, "unknown_error");
            Data = data;
        }
    }
}
