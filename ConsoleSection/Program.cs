using University.DotnetLabs.Lab1.TracerLibrary;
using University.DotnetLabs.Lab1.ConsoleSection.StreamWriters;
using System.Xml;
using System.Text.Json;

namespace University.DotnetLabs.Lab1.ConsoleSection;

class Program
{
    static private string XmlFilePath = "../../../../output.xml";
    static private string JsonFilePath = "../../../../output.json"; 

    static private int _x;
    static private void Recursive()
    {
        _tracer.StartTrace();
        for (int i = 0; i < 100; i++) 
        {
            Console.WriteLine("Hi From Recursive");
        }
        if (_x++ < 9) 
        {
            Recursive();
        }
        _tracer.StopTrace();
    }

    static private void Method1()
    {
        _tracer.StartTrace();
        for (int i = 0; i < 10; i++)
        {
            Console.WriteLine("Hi From Method1");
        }
        _tracer.StopTrace();
    }

    static private void Method2()
    {
        _tracer.StartTrace();
        for (int i = 0; i < 10; i++)
        {
            Console.WriteLine("Hi From Method2");
        }
        _tracer.StopTrace();
    }


    static private readonly ITracer _tracer = new Tracer();
    static void Main(string[] args)
    {
        _tracer.StartTrace();
        Thread thread = new Thread(() =>
        {
            _tracer.StartTrace();
            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine("HI FROM ANOTHER THREAD");
            }
            _tracer.StopTrace();
        });
        Thread thread2 = new Thread(Recursive);
        thread2.Start();
        thread.Start();
        for (int i = 0; i < 100; i++)
        {
            Console.WriteLine("Hi from main thread");
        }
        Method1();
        Method2();
        thread.Join();
        thread2.Join();
        _tracer.StopTrace();

        TraceResult result = _tracer.GetTraceResult();
       
        FileStream XmlFileStream = new FileStream(XmlFilePath, FileMode.OpenOrCreate);
        FileStream JsonFileStream = new FileStream(JsonFilePath, FileMode.OpenOrCreate);
        XmlWriterSettings settings = new XmlWriterSettings();
        settings.ConformanceLevel = ConformanceLevel.Document;
        settings.Indent = true;
        ResultStreamWriter fXmlWriter = new ResultXmlWriter(XmlFileStream, settings);
        ResultStreamWriter cXmlWriter = new ResultXmlWriter(Console.OpenStandardOutput(), settings);
        JsonWriterOptions options = new JsonWriterOptions();
        options.Indented = true;
        ResultStreamWriter fJsonWriter = new ResultJsonWriter(JsonFileStream, options);
        ResultStreamWriter cJsonWriter = new ResultJsonWriter(Console.OpenStandardOutput(), options);
        using (fXmlWriter)
        {
            fXmlWriter.Write(result);
        }
        using (fJsonWriter)
        {
            fJsonWriter.Write(result);
        }
        Console.WriteLine();
        Console.WriteLine("----------------------------XML-----------------------------");
        using (cXmlWriter)
        {
            cXmlWriter.Write(result);
        }
        Console.WriteLine();
        Console.WriteLine("----------------------------JSON-----------------------------");
        using (cJsonWriter)
        {
            cJsonWriter.Write(result);
        }
        XmlFileStream.Dispose();
        JsonFileStream.Dispose();
        Console.ReadLine();
    }
}
