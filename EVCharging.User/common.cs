using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace EVCharging.User.Common
{
    public static class ExtensionMethos
    {
        public static string GetUserId(this ClaimsPrincipal user)

        {
            if(!user.Identity.IsAuthenticated)
            
                return null;
            ClaimsPrincipal currentUser = user;
            return currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
            
        }

    }
}