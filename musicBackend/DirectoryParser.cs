using musicBackend.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace musicBackend
{
    public class DirectoryParser
    {
        public static bool RefreshDatabase(MusicContext db)
        {
            var baseDir = new DirectoryInfo(Properties.Resources.MusicFolderPath);
            var artists = new List<Artist>();
            var albums = new List<Album>();
            var songs = new List<Song>();

            foreach (var artistDir in baseDir.GetDirectories())
            {
                var artist = new Artist { Name = artistDir.Name };
                artists.Add(artist);
                foreach (var albumOfAristDir in artistDir.GetDirectories())
                {
                    var album = new Album { Name = albumOfAristDir.Name, Artist = artist };
                    artist.Albums.Add(album);
                    albums.Add(album);
                    foreach (var songOfAlbumFile in albumOfAristDir.GetFiles())
                    {
                        var song = new Song { Album = album, FileFormat = songOfAlbumFile.Extension.Substring(1), Name = songOfAlbumFile.Name };
                        album.Songs.Add(song);
                        songs.Add(song);
                    }
                }
            }

            db.Artists.RemoveRange(db.Artists);
            db.Albums.RemoveRange(db.Albums);
            db.Songs.RemoveRange(db.Songs);
            db.Artists.AddRange(artists);
            db.Albums.AddRange(albums);
            db.Songs.AddRange(songs);
            try
            {
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
