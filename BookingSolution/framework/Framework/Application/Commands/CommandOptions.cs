namespace Framework.Application.Commands
{
    public record CommandOptions
    {
        public string TransactionIsolationLevel { get; set; }
    }
}
