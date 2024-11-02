using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;
using CommonLib.Net.Http;
using Sgml;

namespace CommonLib.TestHelper.Net.Http
{
    public class MockHtmlDocument : IHtmlDocument
    {
        public XNamespace Namespace { get; private set; }

        public XDocument Content { get; private set; }

        public Uri Uri { get; }

        string content;

        public MockHtmlDocument(string content)
        {
            this.content = content;
        }

        public Task LoadAsync(HttpClient client)
        {
            using var stream = new StringReader(content);
            using var sgml = new SgmlReader { DocType = "HTML", CaseFolding = CaseFolding.ToLower, InputStream = stream };
            Content = XDocument.Load(stream);

            Namespace = Content.Root.Name.Namespace;
            return Task.CompletedTask;
        }
    }
}
