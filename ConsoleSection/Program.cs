using University.DotnetLabs.Lab1.TracerLibrary;
using University.DotnetLabs.Lab1.ConsoleSection.StreamWriters;
using System.Xml;
using System.Text.Json;

namespace University.DotnetLabs.Lab1.ConsoleSection;

class Program
{
    static private string s_xmlFilePath = "../../../../output.xml";
    static private string s_jsonFilePath = "../../../../output.json"; 

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

        using (FileStream xmlFileStream = new FileStream(s_xmlFilePath, FileMode.Create))
        { 
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.ConformanceLevel = ConformanceLevel.Auto;
            settings.WriteEndDocumentOnClose = true;
            settings.Indent = true;
            using (ResultStreamWriter fXmlWriter = new ResultXmlWriter(xmlFileStream, settings))
            {
                fXmlWriter.Write(result);
            }
            using (ResultStreamWriter cXmlWriter = new ResultXmlWriter(Console.OpenStandardOutput(), settings))
            {
                Console.WriteLine();
                Console.WriteLine("----------------------------XML-----------------------------");
                cXmlWriter.Write(result);
            }
        }

        using (FileStream JsonFileStream = new FileStream(s_jsonFilePath, FileMode.Create))
        {
            JsonWriterOptions options = new JsonWriterOptions();
            options.SkipValidation = false;
            options.Indented = true;
            using (ResultStreamWriter fJsonWriter = new ResultJsonWriter(JsonFileStream, options)) 
            {
                fJsonWriter.Write(result);
            }
            using (ResultStreamWriter cJsonWriter = new ResultJsonWriter(Console.OpenStandardOutput(), options)) 
            {
                Console.WriteLine();
                Console.WriteLine("----------------------------JSON-----------------------------");
                cJsonWriter.Write(result);
            }
        }
        Console.ReadLine();
    }
}
