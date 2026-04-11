using System;
using System.Collections.Generic;
using System.Globalization;
using Jellyfin.Plugin.TMDbPlus.Configuration;
using MediaBrowser.Common.Configuration;
using MediaBrowser.Common.Plugins;
using MediaBrowser.Controller;
using MediaBrowser.Model.Plugins;
using MediaBrowser.Model.Serialization;
using Microsoft.AspNetCore.Http;

namespace Jellyfin.Plugin.TMDbPlus;

/// <summary>
/// The main plugin.
/// </summary>
public class Plugin : BasePlugin<PluginConfiguration>, IHasWebPages
{
    /// <summary>
    /// Gets the provider name.
    /// </summary>
    public const string PluginName = "TMDbPlus";


    private readonly IServerApplicationHost _appHost;

    /// <summary>
    /// Initializes a new instance of the <see cref="Plugin"/> class.
    /// </summary>
    /// <param name="applicationPaths">Instance of the <see cref="IApplicationPaths"/> interface.</param>
    /// <param name="xmlSerializer">Instance of the <see cref="IXmlSerializer"/> interface.</param>
    public Plugin(IServerApplicationHost appHost, IApplicationPaths applicationPaths, IXmlSerializer xmlSerializer)
        : base(applicationPaths, xmlSerializer)
    {
        this._appHost = appHost;
        Plugin.Instance = this;
    }

    /// <inheritdoc />
    public override string Name => PluginName;

    /// <inheritdoc />
    public override Guid Id => Guid.Parse("2792BCF5-FF66-4CA2-A7F3-00B84563F69E");

    /// <summary>
    /// Gets the current plugin instance.
    /// </summary>
    public static Plugin? Instance { get; private set; }

    /// <inheritdoc />
    public IEnumerable<PluginPageInfo> GetPages()
    {
        return new[]
        {
            new PluginPageInfo
            {
                Name = this.Name,
                EmbeddedResourcePath = string.Format(CultureInfo.InvariantCulture, "{0}.Configuration.configPage.html", GetType().Namespace),
            },
        };
    }

    public string GetLocalApiBaseUrl()
    {
        return this._appHost.GetLocalApiUrl("127.0.0.1", "http");
    }

    public string GetApiBaseUrl(HttpRequest request)
    {
        int? requestPort = request.Host.Port;
        if (requestPort == null
            || (requestPort == 80 && string.Equals(request.Scheme, "http", StringComparison.OrdinalIgnoreCase))
            || (requestPort == 443 && string.Equals(request.Scheme, "https", StringComparison.OrdinalIgnoreCase)))
        {
            requestPort = -1;
        }

        return this._appHost.GetLocalApiUrl(request.Host.Host, request.Scheme, requestPort);
    }
}
