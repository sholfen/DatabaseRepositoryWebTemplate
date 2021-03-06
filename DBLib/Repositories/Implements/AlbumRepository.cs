﻿using DBLib.Models;
using DBLib.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace DBLib.Repositories.Implements
{
    public class AlbumRepository : BaseRepository<Album>, IAlbumRepository
    {
        //@"Data Source=C:\sample.sqlite;"
        public AlbumRepository() : base(Tools.Tools.ConnectionString)
        //public AlbumRepository() : base(@"Data Source=C:\sample.sqlite;")
        {

        }

        public Album GetById(int id)
        {
            var items = from a in Query()
                       where a.AlbumID == id
                       select a;

            return items.FirstOrDefault();
        }
    }
}
