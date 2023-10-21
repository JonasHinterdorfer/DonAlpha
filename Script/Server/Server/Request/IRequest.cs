namespace Server.Request
{
    public interface IRequest
    {
        public bool Error { get; set; }
        public string ErrorMessage { get; set; }
    }
}
