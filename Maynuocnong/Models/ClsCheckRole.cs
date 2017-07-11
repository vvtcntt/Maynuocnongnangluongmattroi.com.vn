using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Maynuocnong.Models;
namespace Maynuocnong.Models
{
    public class ClsCheckRole
    {
         public static bool  CheckQuyen(int Module,int Role,int idUser)
        {
            MaynuocnongContext db = new MaynuocnongContext();
            var listRight = db.tblRights.Where(p => p.idUser == idUser && p.idModule == Module && p.Role ==Role).ToList();
            if (listRight.Count > 0)
            {
                
                 return true;
            }
            else
                return false;
        }
    }
 
}