using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;
using Sgml;

namespace CommonLib.Net.Http
{
    public class HtmlDocument : IHtmlDocument
    {
        public XNamespace Namespace { get; private set; }

        public XDocument Content { get; private set; }

        public Uri Uri { get; }

        public HtmlDocument(string uri)
        {
            Uri = new Uri(uri);
        }

        public virtual async Task LoadAsync(HttpClient client)
        {
            var content = await client.GetStringAsync(Uri).ConfigureAwait(false);

            using var stream = new StringReader(content);
            using var sgml = new SgmlReader { DocType = "HTML", CaseFolding = CaseFolding.ToLower, InputStream = stream };

            Content = XDocument.Load(sgml);

            Namespace = Content.Root.Name.Namespace;
        }
    }
}
