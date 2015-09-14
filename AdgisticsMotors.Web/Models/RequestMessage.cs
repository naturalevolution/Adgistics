namespace AdgisticsMotors.Web.Models
{
    public abstract class RequestMessage<T>
    {
        public bool IsError { get; set; }
        public string Message { get; set; }
        public abstract void AddSuccess(T data);
        public abstract void AddError(string identifier, string exceptionMessage);
    }
}