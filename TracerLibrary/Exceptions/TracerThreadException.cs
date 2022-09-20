namespace University.DotnetLabs.Lab1.TracerLibrary.Exceptions;

public class TracerThreadException : Exception
{
    public TracerThreadException(string message) : base(message) {}
    public TracerThreadException() : base("Tracer Thread Exception raised") {}
}
