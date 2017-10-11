using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using Chinook.Data.Entities;
using ChinookSystem.DAL;
using Chinook.Data.POCOs;
using System.ComponentModel;
#endregion
namespace ChinookSystem.BLL{
    [DataObject]
   public class ArtistController
   {        [DataObjectMethod(DataObjectMethodType.Select,false)]
       public List<Artist> Artists_List()
        {
            using (var context = new ChinookContext())
                       return context.Artists.ToList();
         }
       }
   }

