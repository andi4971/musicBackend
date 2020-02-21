using System;
using System.Collections.Generic;
using System.IO;
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
        public string GetFilePath() => $"{Path.DirectorySeparatorChar}{Album.Artist.Name}{Path.DirectorySeparatorChar}{Album.Name}{Path.DirectorySeparatorChar}{Name}";
    }
}
