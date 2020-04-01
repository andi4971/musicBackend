using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Resources;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using musicBackend.Models;

namespace musicBackend.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MusicController : ControllerBase
    {
        private MusicContext db;
        private IConfiguration config;
        public MusicController(MusicContext db, IConfiguration config)
        {
            this.db = db;
            this.config = config;
        }
        public string GetAudioAsBase64(int songId)
        {
            var song = db.Songs
                .Include(x => x.Album)
                .Include(x => x.Album.Artist)
                .FirstOrDefault(x => x.SongId == songId);
            var path = $"{config.GetValue<string>("musicFolderPath")}{song.GetFilePath()}";
            byte[] bytes = System.IO.File.ReadAllBytes(path);
            return  Convert.ToBase64String(bytes);
        }

        public ActionResult GetAudioDownload(int songId)
        {
            var song = db.Songs
               .Include(x => x.Album)
               .Include(x => x.Album.Artist)
               .FirstOrDefault(x => x.SongId == songId);
            var path = $"{config.GetValue<string>("musicFolderPath")}{song.GetFilePath()}";
            FileContentResult result = new FileContentResult(System.IO.File.ReadAllBytes(path), $"audio/{song.FileFormat}")
            {
                FileDownloadName = $"{song.Name}",
                EnableRangeProcessing = true
            };
            return result;
        }
        public bool RefreshDatabase()
        {
            return DirectoryParser.RefreshDatabase(db, config.GetValue<string>("musicFolderPath"));
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