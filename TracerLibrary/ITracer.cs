namespace University.DotnetLabs.Lab1.TracerLibrary;

public interface ITracer
{
    void StartTrace();
    void StopTrace();
    TraceResult GetTraceResult();
}
