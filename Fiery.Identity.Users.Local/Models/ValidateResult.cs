namespace Fiery.Identity.Users.Local.Models
{
    public class ValidateResult
    {
        public bool IsSuccess { get; protected set; }

        public string Subject { get; protected set; }
        public string Name { get; protected set; }
        public string Error { get; protected set; }

        public ValidateResult(string subject, string name)
        {
            IsSuccess = true;
            Subject = subject;
            Name = name;
            Error = null;
        }

        public ValidateResult(string error)
        {
            IsSuccess = false;
            Error = error;
        }
    }
}
