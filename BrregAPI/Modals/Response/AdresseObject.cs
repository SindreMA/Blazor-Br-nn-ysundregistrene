using BrregAPI.Modals.Database;

namespace BrregAPI.Modals.Response
{
    public class AdresseObject
    {
        public string Land { get; set; }
        public string Landkode { get; set; }
        public string Postnummer { get; set; }
        public string Poststed { get; set; }
        public string Kommune { get; set; }
        public string Kommunenummer { get; set; }

        public AdresseObject(Adresse adresse)
        {
            Land = adresse.Land;
            Landkode = adresse.Landkode;
            Postnummer = adresse.Postnummer;
            Poststed = adresse.Poststed;
            Kommune = adresse.Kommune;
            Kommunenummer = adresse.Kommunenummer;
        }
    }
}