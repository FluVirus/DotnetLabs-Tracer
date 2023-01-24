using University.DotnetLabs.Lab1.TracerLibrary;

namespace University.DotnetLabs.Lab1.ConsoleSection.StreamWriters;

public abstract class ResultStreamWriter : IDisposable
{   
    protected bool _disposed;
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    protected abstract void Dispose(bool disposing);
    public abstract void Write(TraceResult result);
}
