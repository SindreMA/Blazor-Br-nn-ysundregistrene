using System.Text.Json.Serialization;
using BrregAPI.Modals.ApiResponse;

namespace BrregAPI.Modals.Database
{
    public partial class Firma
    {        
        public long Organisasjonsnummer { get; set; }
        public string Navn { get; set; }
        public string OrganisasjonsKode { get; set; }
        public DateTime RegistreringsdatoEnhetsregisteret { get; set; }
        public bool RegistrertIMvaregisteret { get; set; }
        public long AntallAnsatte { get; set; }
        public Adresse? Forretningsadresse { get; set; }
        public int? InstitusjonellSektorkode { get; set; }
        public bool RegistrertIForetaksregisteret { get; set; }
        public bool RegistrertIStiftelsesregisteret { get; set; }
        public bool RegistrertIFrivillighetsregisteret { get; set; }
        public bool Konkurs { get; set; }
        public bool UnderAvvikling { get; set; }
        public bool UnderTvangsavviklingEllerTvangsopplosning { get; set; }
        public string? Maalform { get; set; }
        public string? Hjemmeside { get; set; }
        public Adresse? Postadresse { get; set; }
        public DateTime Stiftelsesdato { get; set; }
        public long? SisteInnsendteAarsregnskap { get; set; }
        public List<Person> Personer { get; set; }

        public Firma(FirmaRequest data)
        {
            Organisasjonsnummer = data.organisasjonsnummer;
            Navn = data.navn;
            if (data.organisasjonsform != null) OrganisasjonsKode = data.organisasjonsform.kode;
            RegistreringsdatoEnhetsregisteret = data.registreringsdatoEnhetsregisteret;
            RegistrertIMvaregisteret = data.registrertIMvaregisteret;
            AntallAnsatte = data.antallAnsatte;
            if (data.forretningsadresse != null) Forretningsadresse = new Adresse(data.forretningsadresse);
            if (data.institusjonellSektorkode != null) InstitusjonellSektorkode = Convert.ToInt32(data.institusjonellSektorkode.kode);
            RegistrertIForetaksregisteret = data.registrertIForetaksregisteret;
            RegistrertIStiftelsesregisteret = data.registrertIStiftelsesregisteret;
            RegistrertIFrivillighetsregisteret = data.registrertIFrivillighetsregisteret;
            Konkurs = data.konkurs;
            UnderAvvikling = data.underAvvikling;
            UnderTvangsavviklingEllerTvangsopplosning = data.underTvangsavviklingEllerTvangsopplosning;
            Maalform = data.maalform;
            Hjemmeside = data.hjemmeside;
            if (data.postadresse != null) Postadresse = new Adresse(data.postadresse);
            Stiftelsesdato = data.stiftelsesdato;
            SisteInnsendteAarsregnskap = data.sisteInnsendteAarsregnskap;
            Personer = new List<Person>();            
        }

        public Firma()
        {
        }       
    }
    
}