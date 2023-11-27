using System.Text.Json.Serialization;
using BrregAPI.Modals.ApiResponse;

namespace BrregAPI.Modals.Database
{
    public class Adresse
    {
        public int Id { get; set; }
        public string? Land { get; set; }
        public string? Landkode { get; set; }
        public string? Postnummer { get; set; }
        public string? Poststed { get; set; }
        public string? Kommune { get; set; }
        public string? Kommunenummer { get; set; }
        public string? GateAdresse { get; set; }
        
        
        
        public Adresse(AdresseRequest addrese)
        {
            Land = addrese.land;
            Landkode = addrese.landkode;
            Postnummer = addrese.postnummer;
            Poststed = addrese.poststed;
            Kommune = addrese.kommune;
            Kommunenummer = addrese.kommunenummer;
            if (addrese.adresse.Any()) GateAdresse = addrese.adresse.FirstOrDefault();

        }
        
        public Adresse()
        {
            
        }
    }
}
