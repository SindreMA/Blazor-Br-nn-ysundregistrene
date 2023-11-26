using BrregAPI.Modals.Database;

namespace BrregAPI.Modals.Response
{
    public class CompanyFullObject : CompanyBaseObject
    {
        public bool RegistrertIMvaregisteret { get; set; }
        public AdresseObject? Forretningsadresse { get; set; }
        public int? InstitusjonellSektorkode { get; set; }
        public bool RegistrertIForetaksregisteret { get; set; }
        public bool RegistrertIStiftelsesregisteret { get; set; }
        public bool RegistrertIFrivillighetsregisteret { get; set; }
        public bool Konkurs { get; set; }
        public bool UnderAvvikling { get; set; }
        public bool UnderTvangsavviklingEllerTvangsopplosning { get; set; }
        public string? Maalform { get; set; }
        public string? Hjemmeside { get; set; }
        public long? SisteInnsendteAarsregnskap { get; set; }
        public List<PersonObject>? Personer { get; set; }

        public CompanyFullObject(Firma company) : base(company)
        {
            RegistrertIMvaregisteret = company.RegistrertIMvaregisteret;
            if (company.Forretningsadresse != null) Forretningsadresse = new AdresseObject(company.Forretningsadresse);
            InstitusjonellSektorkode = company.InstitusjonellSektorkode;
            RegistrertIForetaksregisteret = company.RegistrertIForetaksregisteret;
            RegistrertIStiftelsesregisteret = company.RegistrertIStiftelsesregisteret;
            RegistrertIFrivillighetsregisteret = company.RegistrertIFrivillighetsregisteret;
            Konkurs = company.Konkurs;
            UnderAvvikling = company.UnderAvvikling;
            UnderTvangsavviklingEllerTvangsopplosning = company.UnderTvangsavviklingEllerTvangsopplosning;
            Maalform = company.Maalform;
            Hjemmeside = company.Hjemmeside;
            SisteInnsendteAarsregnskap = company.SisteInnsendteAarsregnskap;
            Personer = company.Personer?.Select(person => new PersonObject(person)).ToList();            
        }
    }
    
}