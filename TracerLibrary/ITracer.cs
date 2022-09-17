using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University.DotnetLabs.Lab1.TracerLibrary;

public interface ITracer
{
    void StartTrace();
    void StopTrace();
    TraceResult GetTraceResult();
}
