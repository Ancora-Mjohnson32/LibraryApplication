using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LibraryApplication.Models
{
    public class ClaimsStore
    {
        public static List<Claim> AllClaims = new List<Claim>()
    {
            new Claim("Create Role", "Create Role"),
            new Claim("Edit Role","Edit Role"),
            new Claim("Delete Role","Delete Role"),

            new Claim("Delete Book", "Delete Book"),
            new Claim("Edit Book", "Edit Book"),
            new Claim("Create Book", "Create Book")
    };
    }
}
