using System.Collections.Concurrent;

namespace RPCLeo.RPCCLient
{
    public class ThreadMessage : IThreadMessage
    {
        private readonly ConcurrentDictionary<string, TaskCompletionSource<string>> _pendingMessages;

        public ThreadMessage()
        {
            _pendingMessages = new ConcurrentDictionary<string, TaskCompletionSource<string>>();
        }

        public bool TryAdd(string correlationId, TaskCompletionSource<string> tcs)
        {
            return _pendingMessages.TryAdd(correlationId, tcs);
        }

        public TaskCompletionSource<string> TryRemove(string correlationId)
        {
            _pendingMessages.TryRemove(correlationId, out var tsc);
            return tsc;
        }



    }
}
