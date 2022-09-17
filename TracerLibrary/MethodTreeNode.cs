using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University.DotnetLabs.Lab1.TracerLibrary;

public abstract class MethodTreeNode
{
    public LinkedList<MethodTreeNode> InternalMethods { get; } = new();
}
