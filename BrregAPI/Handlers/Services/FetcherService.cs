using BrregAPI.Helpers;
using BrregAPI.Modals;
using BrregAPI.Modals.ApiResponse;
using BrregAPI.Modals.Database;

namespace BrregAPI.Handlers.Services
{
    public class FetcherService
    {

        private readonly BrregContext _context;
        public FetcherService(BrregContext context)
        {
            _context = context;
        }

        private CompanySearchRequest FetchCompanyList(string url = null) 
        {
            if (url == null) url = $"{Statics.ExtenralApi}/enheter/";
            var data = GeneralHelper.HttpGet(url);
            var request = Newtonsoft.Json.JsonConvert.DeserializeObject<CompanySearchRequest>(data);
            return request;

        }

        public void FetchCompanies(int count, int current = 0, string url = null)
        {
            var data = FetchCompanyList(url);
            var ids = data._embedded.enheter.Select(x => x.organisasjonsnummer).ToArray();

            var existingCompanies = _context.Firmaer.Select(x => x.Organisasjonsnummer).Where(x=> ids.Contains(x)).ToList();

            foreach (var company in data._embedded.enheter)
            {
                var firma = new Firma(company);
                if (existingCompanies.Contains(firma.Organisasjonsnummer)) continue; // Would later add a update instead of skip
                _context.Firmaer.Add(firma);
            }
            _context.SaveChanges();

            if (current >= count) return;
            Hangfire.BackgroundJob.Schedule<FetcherService>(x => x.FetchCompanies(count, current + 1, data._links.next.href), TimeSpan.FromSeconds(30));
        }
        
    }
}