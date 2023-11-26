namespace BrregAPI.Modals.ApiResponse
{
    public class CompanySearchRequest
    {
        public Embedded _embedded { get; set; }
        public Links _links { get; set; }
        public Page page { get; set; }
    }

    public class Embedded
    {
        public FirmaRequest[] enheter { get; set; }
    }

    public class FirmaRequest
    {
        public long organisasjonsnummer { get; set; }
        public string navn { get; set; }
        public KodeBeskrivelse organisasjonsform { get; set; }
        public DateTime registreringsdatoEnhetsregisteret { get; set; }
        public bool registrertIMvaregisteret { get; set; }
        public KodeBeskrivelse naeringskode1 { get; set; }
        public int antallAnsatte { get; set; }
        public AdresseRequest forretningsadresse { get; set; }
        public KodeBeskrivelse institusjonellSektorkode { get; set; }
        public bool registrertIForetaksregisteret { get; set; }
        public bool registrertIStiftelsesregisteret { get; set; }
        public bool registrertIFrivillighetsregisteret { get; set; }
        public bool konkurs { get; set; }
        public bool underAvvikling { get; set; }
        public bool underTvangsavviklingEllerTvangsopplosning { get; set; }
        public string maalform { get; set; }
        public Link _links { get; set; }
        public string hjemmeside { get; set; }
        public AdresseRequest postadresse { get; set; }
        public DateTime stiftelsesdato { get; set; }
        public KodeBeskrivelse naeringskode2 { get; set; }
        public int sisteInnsendteAarsregnskap { get; set; }
    }

    public class KodeBeskrivelse
    {
        public string kode { get; set; }
        public string beskrivelse { get; set; }
        public Links _links { get; set; }
        public bool hjelpeenhetskode { get; set; }
    }


    public class AdresseRequest
    {
        public string land { get; set; }
        public string landkode { get; set; }
        public string postnummer { get; set; }
        public string poststed { get; set; }
        public string[] adresse { get; set; }
        public string kommune { get; set; }
        public string kommunenummer { get; set; }
    }

    public class Links
    {
        public Link first { get; set; }
        public Link self { get; set; }
        public Link next { get; set; }
        public Link last { get; set; }
    }

    public class Link
    {
        public string href { get; set; }
    }


    public class Page
    {
        public int size { get; set; }
        public int totalElements { get; set; }
        public int totalPages { get; set; }
        public int number { get; set; }
    }
}
