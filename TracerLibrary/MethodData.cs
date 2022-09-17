using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University.DotnetLabs.Lab1.TracerLibrary;

public class MethodData : MethodTreeNode
{
    public string MethodName { get; }
    public string ClassName { get; }
    public Stopwatch Duration { get; } = new();
    public MethodData(string methodName, string className)
    {
        MethodName = methodName;
        ClassName = className;
    }
}
