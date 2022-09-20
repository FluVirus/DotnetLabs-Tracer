using System.Text.Json;
using University.DotnetLabs.Lab1.TracerLibrary;

namespace University.DotnetLabs.Lab1.ConsoleSection.StreamWriters;

public sealed class ResultJsonWriter : ResultStreamWriter, IDisposable
{
    private readonly Utf8JsonWriter _jsonWriter;

    private const string threadIDProperty= "id";
    private const string threadDurationProperty = "time";
    private const string rootProperty = "threads";
    private const string methodInternalMethodsProperty = "methods";
    private const string methodNameProperty = "name";
    private const string methodClassProperty = "class";
    private const string methodDurationProperty = "time";


    public ResultJsonWriter(Stream stream, JsonWriterOptions options)
    { 
        _jsonWriter = new Utf8JsonWriter(stream, options);
    }

    public override void Dispose()
    {
        _jsonWriter.Dispose();
    }
    private void WriteMethods(ICollection<MethodData> methods) 
    {
        _jsonWriter.WriteStartArray(methodInternalMethodsProperty);
        foreach (MethodData method in methods)
        {
            _jsonWriter.WriteStartObject();
            _jsonWriter.WriteString(methodNameProperty, method.MethodName);
            _jsonWriter.WriteString(methodClassProperty, method.ClassName);
            _jsonWriter.WriteString(methodDurationProperty, $"{method.Duration.ElapsedMilliseconds} ms");
            if (method.InternalMethods.Count > 0) WriteMethods(method.InternalMethods);
            _jsonWriter.WriteEndObject();
        }
        _jsonWriter.WriteEndArray();
    }
    public override void Write(TraceResult result)
    {
        _jsonWriter.WriteStartObject();
        _jsonWriter.WriteStartArray(rootProperty);
        foreach (ThreadData threadData in result.Threads)
        {
            _jsonWriter.WriteStartObject();
            _jsonWriter.WriteString(threadIDProperty, $"{threadData.ID}");
            _jsonWriter.WriteString(threadDurationProperty, $"{threadData.Duration.TotalMilliseconds} ms");
            if (threadData.InternalMethods.Count > 0) WriteMethods(threadData.InternalMethods);
            _jsonWriter.WriteEndObject();
        }
        _jsonWriter.WriteEndArray();
        _jsonWriter.WriteEndObject();
    }
}
