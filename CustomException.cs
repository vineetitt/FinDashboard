namespace FinDashboard.API
{
    public class CustomException : Exception
    {
        public int statusCode { get; }

        public CustomException(string message, int statusCode) : base(message)
        {
            this.statusCode = statusCode;
        }
    }
}
