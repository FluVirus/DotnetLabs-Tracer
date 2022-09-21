namespace University.DotnetLabs.Lab1.TracerLibrary;

public class TraceResult
{
    public ICollection<ThreadData> Threads { get; internal set; }

    public TraceResult(ICollection<ThreadData> threads) 
    { 
        Threads = threads;
    }
}
