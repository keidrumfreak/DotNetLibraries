﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CommonLib.Net.Http
{
    public interface IHtmlDocument
    {
        XNamespace Namespace { get; }

        XDocument Content { get; }

        Uri Uri { get; }

        Task LoadAsync(HttpClient client);
    }
}
