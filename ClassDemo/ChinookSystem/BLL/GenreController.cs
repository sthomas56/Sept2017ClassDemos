﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using Chinook.Data.Entities;
using ChinookSystem.DAL;
using Chinook.Data.POCOs;
using Chinook.Data.DTOs;
using System.ComponentModel;
#endregion

namespace ChinookSystem.BLL
{

    [DataObject]
     public class GenreController
    {
        [DataObjectMethod(DataObjectMethodType.Select,false)]
        public List<GenreDTO> Genre_GenreAlbumTracks()
        {
            using (var context = new ChinookContext())
            {
                var results = from x in context.Genres
                              select new GenreDTO
                              {
                                  genre = x.Name,
                                  albums = from y in x.Tracks
                                           group y by y.Album into gResults
                                           select new AlbumDTO
                                           {
                                               title = gResults.Key.Title,
                                               releaseYear = gResults.Key.ReleaseYear,
                                               numOfTracks = gResults.Count(),
                                               tracks = from z in gResults
                                                        select new TrackPOCO
                                                        {
                                                            song = z.Name,
                                                            milliseconds = z.Milliseconds
                                                        }
                                           }
                              };
                return results.ToList();
                    
            }
        }
    }
}
