using System.Text.Json.Serialization;

namespace BrregFrontend.DTOs
{
    public class CompanyBaseObject
    {
        [JsonPropertyName("orgNr")]
        public long OrgNr { get; set; }
        [JsonPropertyName("navn")]
        public string? Navn { get; set; }
        [JsonPropertyName("orgKode")]
        public string? OrgKode { get; set; }
        [JsonPropertyName("registreringsdatoEnhetsregisteret")]
        public DateTime RegistreringsdatoEnhetsregisteret { get; set; }
        [JsonPropertyName("antallAnsatte")]
        public long AntallAnsatte { get; set; }
        [JsonPropertyName("postadresse")]
        public AdresseObject? Postadresse { get; set; }
        [JsonPropertyName("stiftelsesdato")]
        public DateTime Stiftelsesdato { get; set; }
    }
}
