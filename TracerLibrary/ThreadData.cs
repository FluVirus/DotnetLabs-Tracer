namespace University.DotnetLabs.Lab1.TracerLibrary;

public class ThreadData : MethodTreeNode
{
    public int ID { get; }
    public TimeSpan Duration { get; internal set; } = new();
    public ThreadData(int iD)
    {
        ID = iD;
    }
}
