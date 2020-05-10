using System;
using System.Collections.Generic;
using System.Text;

namespace DBLib.Models
{
    public class Album
    {
        public int AlbumID { get; set; }
        public string Title { get; set; }
        public int ArtistId { get; set; }
    }
}
