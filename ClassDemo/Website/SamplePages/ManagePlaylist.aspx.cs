using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

#region Additional Namespaces
using ChinookSystem.BLL;
using Chinook.Data.POCOs;

#endregion
public partial class SamplePages_ManagePlaylist : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Request.IsAuthenticated)
        {
            Response.Redirect("~/Account/Login.aspx");
        }
        else
        {
            TracksSelectionList.DataSource = null;
        }
    }

    protected void CheckForException(object sender, ObjectDataSourceStatusEventArgs e)
    {
        MessageUserControl.HandleDataBoundException(e);
    }

    protected void Page_PreRenderComplete(object sender, EventArgs e)
    {
        //PreRenderComplete occurs just after databinding page events
        //load a pointer to point to your DataPager control
        DataPager thePager = TracksSelectionList.FindControl("DataPager1") as DataPager;
        if (thePager != null)
        {
            //this code will check the StartRowIndex to see if it is greater that the
            //total count of the collection
            if (thePager.StartRowIndex > thePager.TotalRowCount)
            {
                thePager.SetPageProperties(0, thePager.MaximumRows, true);
            }
        }
    }

    protected void ArtistFetch_Click(object sender, EventArgs e)
    {
        TracksBy.Text = "Artist";
        SearchArgID.Text = ArtistDDL.SelectedValue;
        TracksSelectionList.DataBind();
    }

    protected void MediaTypeFetch_Click(object sender, EventArgs e)
    {
        TracksBy.Text = "MediaType";
        SearchArgID.Text = MediaTypeDDL.SelectedValue;
        TracksSelectionList.DataBind();
    }

    protected void GenreFetch_Click(object sender, EventArgs e)
    {
        TracksBy.Text = "Genre";
        SearchArgID.Text = GenreDDL.SelectedValue;
        TracksSelectionList.DataBind();
    }

    protected void AlbumFetch_Click(object sender, EventArgs e)
    {
        TracksBy.Text = "Album";
        SearchArgID.Text = AlbumDDL.SelectedValue;
        TracksSelectionList.DataBind();
    }

    protected void PlayListFetch_Click(object sender, EventArgs e)
    {
        //standard query
        if (string.IsNullOrEmpty(PlaylistName.Text))
        {
            //put out error message
            //this for muses a user control called MessageuserControl
            MessageUserControl.ShowInfo("Warning", "Playlist Name is required");
        }
        else
        {
            //MessageUserControl has Try Catch coding embedded in the control
            MessageUserControl.TryRun(() =>
            {
                //this is the process coding block to be executed under the "watchful eye" of the MessageUSerControl

                //obtain the username from the security part of the application
                string username = User.Identity.Name;
                PlaylistTracksController sysmgr = new PlaylistTracksController();
                List<UserPlaylistTrack> info = sysmgr.List_TracksForPlaylist(PlaylistName.Text, username);
                PlayList.DataSource = info;
                PlayList.DataBind();

            }, "", "Here is your current playlist");

        }

    }

    protected void TracksSelectionList_ItemCommand(object sender,
        ListViewCommandEventArgs e)
    {
        //ListViewCommandEventArgs paramter e contains the CommandArg value (TrackID from button )
        if (string.IsNullOrEmpty(PlaylistName.Text))
        {

            MessageUserControl.ShowInfo("Warning", "Playlist Name is required");
        }
        else
        {
            string username = User.Identity.Name;
            //TrackID is going to come from e.CommandArgument
            //e.commandArgument is an object, therfore convert to string

            int trackid = int.Parse(e.CommandArgument.ToString());

            // the following code calls a BLL method to add to the database

            MessageUserControl.TryRun(() =>
            {
                PlaylistTracksController sysmgr = new PlaylistTracksController();
                List<UserPlaylistTrack> refreshresults = sysmgr.Add_TrackToPLaylist(PlaylistName.Text, username, trackid);
                PlayList.DataSource = refreshresults;
                PlayList.DataBind();

            }, "Success", "track added to playlist");

        }
    }

    protected void MoveUp_Click(object sender, EventArgs e)
    {
        //code to go here
    }

    protected void MoveDown_Click(object sender, EventArgs e)
    {
        //code to go here
    }
    protected void MoveTrack(int trackid, int tracknumber, string direction)
    {
        //code to go here
    }
    protected void DeleteTrack_Click(object sender, EventArgs e)
    {
        //code to go here
    }
}
