using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jellyfin.Plugin.TMDbPlus.Model
{
    public class GuessInfo
    {
        public int? episodeNumber { get; set; }

        public int? seasonNumber { get; set; }

        public string? Name { get; set; }
    }
}