using System.Xml;
using System.Xml.Serialization;

public static class XmlFormat
{
    public static void Write(AzureResourceResult result)
    {
        var serializer = new XmlSerializer(result.GetType());
        var writer = new StringWriter();
        serializer.Serialize(XmlWriter.Create(writer, new XmlWriterSettings { Indent = true }), result);
        Console.WriteLine(writer.ToString());
    }
}
