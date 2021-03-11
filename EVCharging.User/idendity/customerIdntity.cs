using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace EVCharging.User.idendity
{
    public class customerIdntity: IdentityUser
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Nullable<int> BusinessUnitID { get; set; }
        public string EmailAddress { get; set; }
        public string streetAddress { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string InvoiceStreetAddress { get; set; }
        public string InvoicePostalCode { get; set; }
        public string InvoiceCity { get; set; }
        public string InvoiceCountry { get; set; }
        public string Site { get; set; }
        public string Telephone { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<customerIdntity> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}