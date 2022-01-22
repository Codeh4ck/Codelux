namespace Codelux.Common.Models
{
    public class ServiceValidationError
    {
        public ServiceValidationError() { }

        public ServiceValidationError(string field, string message) : this()
        {
            Field = field;
            Message = message;

            if (!string.IsNullOrEmpty(message)) Id = message.GetHashCode();
        }

        public ServiceValidationError(int id, string field, string message) : this()
        {
            Id = id;
            Field = field;
            Message = message;
        }

        public int? Id { get; set; }
        public string Field { get; set; }
        public string Message { get; set; }
    }
}
