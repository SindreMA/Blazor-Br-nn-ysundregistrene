using BrregAPI.Modals.ApiResponse;

namespace BrregAPI.Modals.ApiResponses
{
    public class EmployeeSearchRequest
    {
        public Rollegrupper[] rollegrupper { get; set; }
        public Links _links { get; set; }
    }

    public class Rollegrupper
    {
        public KodeBeskrivelse type { get; set; }
        public string sistEndret { get; set; }
        public Roller[] roller { get; set; }
    }

    public class Roller
    {
        public KodeBeskrivelse type { get; set; }
        public Person person { get; set; }
        public bool fratraadt { get; set; }
        public int rekkefolge { get; set; }
        public KodeBeskrivelse valgtAv { get; set; }
        public Enhet enhet { get; set; }
    }

    public class Person
    {
        public string fodselsdato { get; set; }
        public Navn navn { get; set; }
        public bool erDoed { get; set; }
    }

    public class Navn
    {
        public string fornavn { get; set; }
        public string etternavn { get; set; }
        public string mellomnavn { get; set; }
    }

    public class Enhet
    {
        public string organisasjonsnummer { get; set; }
        public KodeBeskrivelse organisasjonsform { get; set; }
        public string[] navn { get; set; }
        public bool erSlettet { get; set; }
    }
}
