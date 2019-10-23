using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using musicBackend.Models;

namespace musicBackend.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MusicController : ControllerBase
    {
        private MusicContext db;
        public MusicController(MusicContext db)
        {
            this.db = db;
        }
        public IActionResult GetAudioStream(int songId)
        {
            var song = db.Songs
                .Include(x => x.Album)
                .Include(x => x.Album.Artist)
                .FirstOrDefault(x => x.SongId == songId);
            var path = $"{Properties.Resources.MusicFolderPath}{song.GetFilePath()}";
            return File(System.IO.File.OpenRead(path), $"audio/{song.FileFormat.Substring(1)}");
        }

        public bool RefreshDatabase()
        {
            return DirectoryParser.RefreshDatabase(db);
        }
        public List<Artist> GetArtists()
        {
            return db.Artists.ToList();
        }
        public List<Album> GetAlbumsOfArtist(int artistId)
        {
            return db.Albums.Where(x => x.Artist.ArtistId == artistId).ToList();
        }
        public List<Song> GetSongsOfAlbum(int albumId)
        {
            return db.Songs.Where(x => x.Album.AlbumId == albumId).ToList();
        }

    }
}