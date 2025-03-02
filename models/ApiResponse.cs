using ProjectManagement.services;

namespace ProjectManagement.Models
{
    public class ApiResponse
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public object? Data { get; set; }

        public ApiResponse(int errorCode, string message = "", object? data = null)
        {
            Code = errorCode;
            Message = string.IsNullOrEmpty(message) ? ErrorCodes.Messages.GetValueOrDefault(errorCode, "unknown_error") : message;
            Data = data;
        }
    }
}
