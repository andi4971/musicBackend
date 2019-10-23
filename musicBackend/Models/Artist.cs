using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace musicBackend.Models
{
    public class Artist
    {
        public int ArtistId { get; set; }
        public string Name { get; set; }
        public List<Album> Albums { get; set; } = new List<Album>();
    }
}
