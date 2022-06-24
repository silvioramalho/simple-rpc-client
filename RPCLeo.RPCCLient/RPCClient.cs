namespace RPCLeo.RPCCLient
{
    public class RPCClient : IDisposable
    {
        private readonly IThreadMessage _pendingMessages;

        public RPCClient(IThreadMessage pendingMessages)
        {
            _pendingMessages = pendingMessages;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public async Task<string> SendAsync(string message)
        {
            var tcs = new TaskCompletionSource<string>();
            // string correlationId = Guid.NewGuid().ToString();

            string correlation = await Publish(message);
            _pendingMessages.TryAdd(correlation, tcs);

            return tcs.Task.Result;           

        }

        private async Task<string> Publish(string message)
        {   
            string correlationId = Guid.NewGuid().ToString();
            Console.WriteLine($"Message: {message} - Correlation Id {correlationId}" );
            return await Task.FromResult(correlationId);
        }
    }
}
