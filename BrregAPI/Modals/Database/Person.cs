using BrregAPI.Modals.ApiResponses;

namespace BrregAPI.Modals.Database
{
    public class Person {

        public Person(Roller person)
        {
            Navn = $"{person.person.navn.fornavn} {person.person.navn.mellomnavn} {person.person.navn.etternavn}";
            //Epost = ""; // API does not provide this data
            //Telefon = "";  // API does not provide this data
            Rolle = person.type.kode;
            RolleNavn = person.type.beskrivelse;
        }

        public Person() { }

        public int Id { get; set; }
        public string Navn { get; set; }
        public string? Epost { get; set; }
        public string? Telefon { get; set; }
        public string Rolle { get; set; }
        public string RolleNavn { get; set; }
    }
    
}