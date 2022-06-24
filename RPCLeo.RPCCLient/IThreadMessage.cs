namespace RPCLeo.RPCCLient
{
    public interface IThreadMessage
    {

        bool TryAdd(string correlationId, TaskCompletionSource<string> tcs);

        TaskCompletionSource<string> TryRemove(string correlationId);
    }
}