using Jellyfin.Plugin.TMDbPlus.Api;
using MediaBrowser.Controller;
using MediaBrowser.Controller.Plugins;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Jellyfin.Plugin.TMDbPlus
{
    /// <inheritdoc />
    public class ServiceRegistrator : IPluginServiceRegistrator
    {
        /// <inheritdoc />
        public void RegisterServices(IServiceCollection serviceCollection, IServerApplicationHost applicationHost)
        {
            serviceCollection.AddSingleton((ctx) =>
            {
                return new TmdbApi(ctx.GetRequiredService<ILoggerFactory>());
            });
        }
    }
}
