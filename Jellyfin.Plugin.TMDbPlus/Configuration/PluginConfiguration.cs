using System.Net;
using System.Reflection;
using MediaBrowser.Model.Plugins;

namespace Jellyfin.Plugin.TMDbPlus.Configuration;


/// <summary>
/// Plugin configuration.
/// </summary>
public class PluginConfiguration : BasePluginConfiguration
{
    public const int MAX_CAST_MEMBERS = 15;
    public const int MAX_SEARCH_RESULT = 5;

    /// <summary>
    /// 插件版本
    /// </summary>
    public string Version { get; } = Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? string.Empty;

    /// <summary>
    /// 启用tmdb获取成人内容
    /// </summary>
    public bool EnableTmdbAdult { get; set; } = false;
    /// <summary>
    /// 是否获取tmdb分级信息
    /// </summary>
    public bool EnableTmdbOfficialRating { get; set; } = true;
    /// <summary>
    /// tmdb api key
    /// </summary>
    public string TmdbApiKey { get; set; } = string.Empty;
    /// <summary>
    /// tmdb api host
    /// </summary>
    public string TmdbHost { get; set; } = string.Empty;
    /// <summary>
    /// 代理服务器类型，0-禁用，1-http，2-https，3-socket5
    /// </summary>
    public string TmdbProxyType { get; set; } = string.Empty;
    /// <summary>
    /// 代理服务器host
    /// </summary>
    public string TmdbProxyPort { get; set; } = string.Empty;
    /// <summary>
    /// 代理服务器端口
    /// </summary>
    public string TmdbProxyHost { get; set; } = string.Empty;


    public IWebProxy GetTmdbWebProxy()
    {

        if (!string.IsNullOrEmpty(TmdbProxyType))
        {
            return new WebProxy($"{TmdbProxyType}://{TmdbProxyHost}:{TmdbProxyPort}", true);
        }

        return null;
    }
}
