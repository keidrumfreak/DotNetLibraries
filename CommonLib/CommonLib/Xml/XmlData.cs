using System.IO;
using System.IO.Abstractions;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using CommonLib.IO;
using CommonLib.Logging;

namespace CommonLib.Xml
{
    public abstract class XmlData<T> where T : new()
    {
        protected T Data { get; set; }

        protected string DefaultNamespace { get; set; }

        protected XmlSerializerNamespaces Namespaces { get; set; }

        protected string Xsd { get; set; }

        IFileSystem fileSystem;

        IFile File => fileSystem.File;

        public XmlData(IFileSystem fileSystem)
        {
            this.fileSystem = fileSystem;
        }

        public XmlData() : this(new FileSystem())
        { }

        public virtual void Load(string path)
        {
            var serializer = new XmlSerializer(typeof(T));

            using (var file = File.OpenRead(path))
            {
                Data = (T)serializer.Deserialize(file);
            }
        }

        public virtual void Save(string path)
        {
            using (var file = File.OpenWrite(path))
            using (var writer = new StreamWriter(file))
            {
                Save(writer);
            }
        }

        protected void Save(string path, Encoding encoding)
        {
            using (var file = File.OpenWrite(path))
            using (var writer = new StreamWriter(file, encoding))
            {
                Save(writer);
            }
        }

        private void Save(TextWriter writer)
        {
            var serializer = string.IsNullOrEmpty(DefaultNamespace)
                ? new XmlSerializer(typeof(T))
                : new XmlSerializer(typeof(T), DefaultNamespace);

            if (Namespaces == default)
                serializer.Serialize(writer, Data);
            else
                serializer.Serialize(writer, Data, Namespaces);
        }

        public bool Validate(string path, ILogger logger = null)
        {
            if (Xsd == default)
                return true;

            var doc = new XmlDocument();
            var schemaset = new XmlSchemaSet();
            schemaset.Add(DefaultNamespace, Xsd);
            doc.Schemas.Add(schemaset.Schemas().Cast<XmlSchema>().First());
            doc.Load(path);
            bool isValid = true;
            doc.Validate((sender, args) =>
            {
                if (args.Severity == XmlSeverityType.Error)
                {
                    isValid = false;
                    logger?.TraceErrorLog($"Error: {args.Message}");
                }
                else if (args.Severity == XmlSeverityType.Warning)
                {
                    logger?.TraceWarningLog($"Warning: {args.Message}");
                }
            });
            return isValid;
        }
    }
}
