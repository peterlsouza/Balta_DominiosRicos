using Payment.Shared.Commands;

namespace Payment.Domain.Commands
{
    public class CommanResult : ICommandResult
    {
        public CommanResult()
        {

        }
        public CommanResult(bool success, string message)
        {
            Success = success;
            Message = message;
        }

        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
