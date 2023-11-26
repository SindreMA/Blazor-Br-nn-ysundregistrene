using BrregAPI.Modals.Database;

namespace BrregAPI.Modals.Response
{
    public class CompanyBaseObject
    {
        public CompanyBaseObject(Firma company)
        {
            OrgNr = company.Organisasjonsnummer;
            Navn = company.Navn;
            OrgKode = company.OrganisasjonsKode;
            RegistreringsdatoEnhetsregisteret = company.RegistreringsdatoEnhetsregisteret;
            AntallAnsatte = company.AntallAnsatte;
            if (company.Postadresse != null) Postadresse = new AdresseObject(company.Postadresse);
            Stiftelsesdato = company.Stiftelsesdato;
        }

        public long OrgNr { get; set; }
        public string? Navn { get; set; }
        public string? OrgKode { get; set; }
        public DateTime RegistreringsdatoEnhetsregisteret { get; set; }
        public long AntallAnsatte { get; set; }
        public AdresseObject? Postadresse { get; set; }
        public DateTime Stiftelsesdato { get; set; }
    }    
}