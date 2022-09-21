namespace University.DotnetLabs.Lab1.TracerLibrary;

public abstract class MethodTreeNode
{
    public LinkedList<MethodData> InternalMethods { get; } = new();
}
