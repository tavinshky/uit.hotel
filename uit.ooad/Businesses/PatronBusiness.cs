using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Realms;
using uit.ooad.DataAccesses;
using uit.ooad.Models;

namespace uit.ooad.Businesses
{
    public class PatronBusiness
    {
        public static Task<Patron> Add(Patron patron)
        {
            var patronInDatabase = Get(patron.Identification);
            if (patronInDatabase != null) return null;

            patron.PatronKind = patron.PatronKind.GetManaged();
            return PatronDataAccess.Add(patron);
        }

        public static Patron Get(string patronId) => PatronDataAccess.Get(patronId);
        public static IEnumerable<Patron> Get() => PatronDataAccess.Get();
        public static IEnumerable<Patron> Query(string query)
        {
            return Get()
            .Where(p => p.Identification.Contains(query, StringComparison.OrdinalIgnoreCase))
            .ToList();
        }
    }
}
