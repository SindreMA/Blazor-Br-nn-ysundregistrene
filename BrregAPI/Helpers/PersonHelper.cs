using BrregAPI.Modals;
using BrregAPI.Modals.Response;

namespace BrregAPI.Helpers
{
    public class PersonHelper
    {
        private BrregContext _context;

        public PersonHelper(BrregContext context)
        {
            _context = context;
        }

        internal async Task UpdatePerson(PersonObject personMessage, int id)
        {
            var person = _context.Personer.FirstOrDefault(x => x.Id == id);
            if (person == null) throw new Exception("Person not found");
            
            person.RolleNavn = personMessage.Rolle;
            person.Telefon = personMessage.Telefon;
            person.Epost = personMessage.Epost;

            await _context.SaveChangesAsync();
        }
    }
}
