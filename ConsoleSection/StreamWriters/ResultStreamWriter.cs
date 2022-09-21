using University.DotnetLabs.Lab1.TracerLibrary;

namespace University.DotnetLabs.Lab1.ConsoleSection.StreamWriters;

public abstract class ResultStreamWriter : IDisposable
{   
    public abstract void Dispose();
    public abstract void Write(TraceResult result);
}
