namespace BrregFrontend.DTOs
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
    }
}
