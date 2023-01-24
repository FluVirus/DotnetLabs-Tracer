using System.Xml;
using University.DotnetLabs.Lab1.TracerLibrary;

namespace University.DotnetLabs.Lab1.ConsoleSection.StreamWriters;

public sealed class ResultXmlWriter : ResultStreamWriter, IDisposable
{ 
    private readonly XmlWriter _xmlWriter;

    private const string threadTag = "thread";
    private const string threadAttributeID = "id";
    private const string threadDurationAttribute = "time";
    private const string methodTag = "method";
    private const string methodNameAttribute = "name";
    private const string methodClassAttribute = "class";
    private const string methodDurationAttribute = "time";
    private const string rootTag = "root";
    public ResultXmlWriter(Stream stream, XmlWriterSettings settings) 
    {
        _xmlWriter = XmlWriter.Create(stream, settings);
    }

    ~ResultXmlWriter()
    {
        if (!_disposed) 
            Dispose(false);
    }
    protected override void Dispose(bool disposing)
    {
        if (_disposed)
            return;

        if (disposing) 
        {
            _xmlWriter.Dispose();
        }

        _disposed = true;
    }

    private void WriteMethods(ICollection<MethodData> methods)
    {    
        foreach (MethodData method in methods)
        {
            _xmlWriter.WriteStartElement(methodTag);
            _xmlWriter.WriteAttributeString(methodNameAttribute, method.MethodName);
            _xmlWriter.WriteAttributeString(methodClassAttribute, method.ClassName);
            _xmlWriter.WriteAttributeString(methodDurationAttribute, $"{method.Duration.ElapsedMilliseconds} ms");
            if (method.InternalMethods.Count > 0) WriteMethods(method.InternalMethods);
            _xmlWriter.WriteEndElement();
        }
    }

    public override void Write(TraceResult result)
    {
        if (_disposed)
            throw new ObjectDisposedException("This object has already been disposed");

        if (_xmlWriter.Settings?.ConformanceLevel == ConformanceLevel.Document) _xmlWriter.WriteStartDocument();
        _xmlWriter.WriteStartElement(rootTag);
        foreach (ThreadData threadData in result.Threads)
        {
            _xmlWriter.WriteStartElement(threadTag);
            _xmlWriter.WriteAttributeString(threadAttributeID, $"{threadData.ID}");
            _xmlWriter.WriteAttributeString(threadDurationAttribute, $"{threadData.Duration.TotalMilliseconds} ms");
            if (threadData.InternalMethods.Count > 0) WriteMethods(threadData.InternalMethods);
            _xmlWriter.WriteEndElement();
        }
        _xmlWriter.WriteEndElement();
        if (_xmlWriter.Settings?.ConformanceLevel == ConformanceLevel.Document)  _xmlWriter.WriteEndDocument();
    }
}
