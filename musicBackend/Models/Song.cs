using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace musicBackend.Models
{
    public class Song
    {
        public Album Album { get; set; }
        public int SongId { get; set; }
        public string Name { get; set; }
        public string FileFormat { get; set; }
        public string GetFilePath() => $"\\{Album.Artist.Name}\\{Album.Name}\\{Name}";
    }
}
