using BrregAPI.Helpers;
using BrregAPI.Modals;
using BrregAPI.Modals.ApiResponse;
using BrregAPI.Modals.ApiResponses;
using BrregAPI.Modals.Database;
using Hangfire;
using Microsoft.EntityFrameworkCore;

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
                Hangfire.BackgroundJob.Enqueue<FetcherService>(x => x.FetchEmployeesForCompany(firma.Organisasjonsnummer));
            }
            _context.SaveChanges();

            if (current >= count) return;
            Hangfire.BackgroundJob.Schedule<FetcherService>(x => x.FetchCompanies(count, current + 1, data._links.next.href), TimeSpan.FromSeconds(30));
        }

        public void FetchEmployeesForAllCompanies()
        {
            var companies = _context.Firmaer.ToList();

            int index = 0;
            foreach (var company in companies)
            {
                Hangfire.BackgroundJob.Schedule<FetcherService>(x => x.FetchEmployeesForCompany(company.Organisasjonsnummer), TimeSpan.FromSeconds(30 * index));
                index++;
            }
        }

        [AutomaticRetry(Attempts = 0)]
        public void FetchEmployeesForCompany(long orgId)
        {
            var company = _context.Firmaer.Where(x=> x.Organisasjonsnummer == orgId).Include(x=> x.Personer).FirstOrDefault();

            var url = $"{Statics.ExtenralApi}/enheter/{company.Organisasjonsnummer}/roller";
            var data = GeneralHelper.HttpGet(url);
            var request = Newtonsoft.Json.JsonConvert.DeserializeObject<EmployeeSearchRequest>(data);
            
            foreach (var employee in request.rollegrupper)
            {
                foreach (var person in employee.roller.Where(x=> x.person != null))
                {
                    var name = $"{person.person.navn.fornavn} {person.person.navn.mellomnavn} {person.person.navn.etternavn}";
                    if (company.Personer.Any(x=> x.Navn == name)) continue;
                    var personObject = new Modals.Database.Person(person);
                    company.Personer.Add(personObject);
                }                
            }
            _context.SaveChanges();            
        }
    }
}