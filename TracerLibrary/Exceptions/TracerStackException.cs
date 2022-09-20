namespace University.DotnetLabs.Lab1.TracerLibrary.Exceptions;

public class TracerStackException : Exception
{
    public TracerStackException(string message) : base(message) {}
    public TracerStackException() : base("Tracer Stack Exception raised") { }
}
