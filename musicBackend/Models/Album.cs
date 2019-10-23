using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace musicBackend.Models
{
    public class Album
    {
        public Artist Artist { get; set; }
        public int AlbumId { get; set; }
        public string Name { get; set; }
        public List<Song> Songs { get; set; } = new List<Song>();
    }
}
