﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using Chinook.Data.Entities;
using Chinook.Data.DTOs;
using Chinook.Data.POCOs;
using ChinookSystem.DAL;
using System.ComponentModel;
#endregion

namespace ChinookSystem.BLL
{
    public class PlaylistTracksController
    {
        public List<UserPlaylistTrack> List_TracksForPlaylist(
            string playlistname, string username)
        {
            using (var context = new ChinookContext())
            {
                //to ensure results end up with a valid value use the .FirstOrDefault()
                //default is null


                var results = (from x in context.Playlists
                              where x.Name.Equals(username)
                              && x.Name.Equals(playlistname)
                              select x).FirstOrDefault();

                var theTracks = from x in context.PlaylistTracks
                                where x.PlaylistId.Equals(results.PlaylistId)
                                orderby x.TrackNumber
                                select new UserPlaylistTrack
                                {
                                    TrackID = x.TrackId,
                                    TrackNumber = x.TrackNumber,
                                    TrackName = x.Track.Name,
                                    Milliseconds = x.Track.Milliseconds,
                                    UnitPrice = x.Track.UnitPrice
                                };

                return theTracks.ToList();
            }
        }//eom
        public List<UserPlaylistTrack> Add_TrackToPLaylist(string playlistname, string username, int trackid)
        {
            using (var context = new ChinookContext())
            {
                //Part One: handle playlist record
                //query to get playlist ID
                var exists = (from x in context.Playlists
                               where x.Name.Equals(username)
                               && x.Name.Equals(playlistname)
                               select x).FirstOrDefault();
                //intialize the track number for the track going into PLaylistTracks
                int tracknumber = 0;

                //will need to create an instance of PLaylistTrack
                PlaylistTrack newtrack = null;

                //determine if this is an exisintg list or if a new list needs to be created
                if (exists == null)
                {
                    //this is a new playlist
                    //create the playlist

                    exists = new Playlist();
                    exists.Name = playlistname;
                    exists.UserName = username;
                    exists = context.Playlists.Add(exists);
                    tracknumber = 1;

                }
                else
                {
                    //the playlist already exists, we need to knwo the number of tracks on the list
                    //tracknumber = count + 1
                    tracknumber = exists.PlaylistTracks.Count() + 1;

                    //in our example, tracks exist only once on each playlist
                    newtrack = exists.PlaylistTracks.SingleOrDefault(x => x.TrackId == trackid);
                    //this will be null if the track is NOT on the playlist tracks

                    if(newtrack != null)
                    {
                        throw new Exception("Playlist already has requested track");
                    }
                   
                }

                //Part Two: handle the track for PLaylistTrack
                //use navigation to .Add the new track to PlaylistTrack
                newtrack = new PlaylistTrack();
                newtrack.TrackId = trackid;
                newtrack.TrackNumber = tracknumber;


                //NOTE: the Pkey for PlaylistId may not yet exist,using navigation one can let HashSet handle the PlaylistId Pkey

                exists.PlaylistTracks.Add(newtrack);

                //physically commit your work to the DB
                context.SaveChanges();

                //refresh the list
                return List_TracksForPlaylist(playlistname, username);


            }
        }//eom
        public void MoveTrack(string username, string playlistname, int trackid, int tracknumber, string direction)
        {
            using (var context = new ChinookContext())
            {
                //code to go here 

            }
        }//eom


        public void DeleteTracks(string username, string playlistname, List<int> trackstodelete)
        {
            using (var context = new ChinookContext())
            {
               //code to go here


            }
        }//eom
    }
}
