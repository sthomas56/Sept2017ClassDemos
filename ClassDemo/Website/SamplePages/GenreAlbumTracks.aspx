<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="GenreAlbumTracks.aspx.cs" Inherits="SamplePages_GenreAlbumTracks" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">

    <h1>Genre Albums and Tracks</h1>
<%--        inside a repeater you need a minimum of a ItemTemplate
    other templates include HeaderTemplate, FooterTemplate, AlternatingItemTemplate, SeperatorTemplate

    outer repeater will display the first fields from the DTO which do not repeater
    outer repeater gets its data from an ODS

    nested repeater will display the collection of the DTO file
    nested repeater will get its data source from the collection (List<T> or numerator)
    of the DTO class (either a POCO or another DTO)

        this pattern repeats for all the levels of the data set.

        repeater controls are good for uncommon structures DTOS

    
    --%>

    <asp:Repeater ID="GenreAlbumTrackList" runat="server" DataSourceID="GenreAlbumTrackODS" ItemType="Chinook.Data.DTOs.GenreDTO">
        <ItemTemplate>
            <h2> Genre: <%# Eval("genre") %></h2>
            <asp:Repeater ID="GenreAlbums" runat="server" DataSource='<%# Eval("albums") %>'
                ItemType="Chinook.Data.DTOs.AlbumDTO">
                <ItemTemplate>
                    <h4>Albums: <%# string.Format("{0} ({1}) Tracks: {2}",Eval("title"), Eval("releaseYear"), Eval("numOfTracks")) %></h4><br />

                    <asp:ListView ID="AlbumTracks" runat="server" DataSource="<%# Item.tracks %>"
                     ItemType="Chinook.Data.POCOs.TrackPOCO">
                        <LayoutTemplate>
                            <table>
                                <tr>
                                    <th>Song</th>
                                    <th>Length</th>
                                </tr>
                                <tr id="itemPlaceholder" runat="server"></tr>
                            </table>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <tr>
                                <td style="width:600px"><%# Item.song %></td>
                                <td><%# Item.length %></td>
                            </tr>

                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <tr style="background-color:aqua">
                                <td style="width:600px"><%# Item.song %></td>
                                <td> <%#Item.length %></td>
                            </tr>
                            </AlternatingItemTemplate>
                        <EmptyDataTemplate>
                            <tr>
                                <td colspan="2">
                                    No data available at this time
                                </td>
                            </tr>
                        </EmptyDataTemplate>


                    </asp:ListView>

                    <%-- Grid view example--%>

      <%--              <asp:GridView ID="AlbumTracks" runat="server" DataSource="<%# Item.tracks %>"
                        AutoGenerateColumns="false" ItemType="Chinook.Data.POCOs.TrackPOCO">
                        <Columns>
                            <asp:TemplateField HeaderText="Song">
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text="<%# Item.song %>"
                                        Width="600px"></asp:Label>
                                </ItemTemplate>

                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Length">
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text="<%#Item.length %>"></asp:Label>
                                </ItemTemplate>

                                <ItemStyle HorizontalAlign="right"></ItemStyle>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>--%>


                    <%--repeater example --%>

<%--                    <asp:Repeater ID="AlbumTracks" runat="server" DataSource='<%# Item.tracks  %>' ItemType="Chinook.Data.POCOs.TrackPOCO">
                        <HeaderTemplate>
                            <table>
                                <tr>
                                    <th>Song</th>
                                    <th>Length</th>
                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td style="width:600px">
                                    <%# Item.song %>
                                </td>
                                <td>
                                    <%# Item.length %>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>--%>
                </ItemTemplate>
                <SeparatorTemplate>
                    <hr / style="height:3px;border:none;color:#000;background-color:#000;">
                </SeparatorTemplate>
            </asp:Repeater>


        </ItemTemplate>


    </asp:Repeater>
    <asp:ObjectDataSource ID="GenreAlbumTrackODS" runat="server" OldValuesParameterFormatString="original_{0}"
         SelectMethod="Genre_GenreAlbumTracks" TypeName="ChinookSystem.BLL.GenreController"></asp:ObjectDataSource>

</asp:Content>

