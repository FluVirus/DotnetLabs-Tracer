using University.DotnetLabs.Lab1.TracerLibrary.Exceptions;

namespace University.DotnetLabs.Lab1.TracerLibrary.Tests;

[TestClass]
public class TracerTests
{
    private ITracer _tracer;
    [TestInitialize]
    public void Initialize()
    {
        _tracer = new Tracer();
    }

    [TestMethod]
    [ExpectedException(typeof(TracerStackException))]
    public void TracerTest_TooManyStops()
    {
        _tracer.StartTrace();
        int x = 5;
        for (int i = 0; i < 10; i++)
        {
            x += x + 2;
        }
        _tracer.StopTrace();
        _tracer.StopTrace();
        _tracer.StopTrace();
        TraceResult result = _tracer.GetTraceResult();
    }
    private void MethodStart() => _tracer.StartTrace();
    private void MethodStop() => _tracer.StopTrace();

    [TestMethod]
    [ExpectedException(typeof(TracerStackException))]
    public void TracerTest_DifferentStartAndStop()
    {
        MethodStart();
        MethodStop();
        TraceResult result = _tracer.GetTraceResult();
    }

    [TestMethod]
    [ExpectedException(typeof(TracerThreadException))]
    public void TracerTest_SuddenStopWithoutStartInThread()
    {
        MethodStop();
        TraceResult result = _tracer.GetTraceResult();
    }
}
