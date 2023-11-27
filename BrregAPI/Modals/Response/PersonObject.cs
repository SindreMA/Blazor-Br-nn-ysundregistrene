using BrregAPI.Modals.Database;

namespace BrregAPI.Modals.Response
{
    public class PersonObject
    {
        public int Id { get; set; }
        public string? Navn { get; set; }
        public string? Epost { get; set; }
        public string? Telefon { get; set; }
        public string? Rolle { get; set; }        
        
        public PersonObject(Person person)
        {
            Id = person.Id;
            Navn = person.Navn;
            Epost = person.Epost;
            Telefon = person.Telefon;
            Rolle = person.RolleNavn;
        }
        public PersonObject()
        {
        }
    }    
}