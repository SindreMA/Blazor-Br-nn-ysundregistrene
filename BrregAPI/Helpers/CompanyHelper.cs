using BrregAPI.Modals;
using BrregAPI.Modals.ApiResponse;
using BrregAPI.Modals.Database;
using BrregAPI.Modals.Response;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BrregAPI.Helpers
{
    public class CompanyHelper
    {
        private BrregContext _context;

        public CompanyHelper(BrregContext context)
        {
            _context = context;
        }

        internal CompanyFullObject GetCompany(int id)
        {
            var company = _context.Firmaer
            .Include(x => x.Forretningsadresse)
            .Include(x => x.Postadresse)
            .Include(x => x.Personer)
            .FirstOrDefault(x => x.Organisasjonsnummer == id);

            if (company == null) {
                var data = FetchCompany(id);
                if (data == null) throw new Exception("Company not found");
                company = new Firma(data);
                _context.Firmaer.Add(company);
                _context.SaveChanges();
            }

            return new CompanyFullObject(company);            
        }

        internal async Task<ListResponse<CompanyBaseObject>> GetCompanies(int start, int amount)
        {
            
            var total = (new BrregContext()).Firmaer.CountAsync(); // Does not scale well
            var companies = _context.Firmaer.Include(x => x.Postadresse).OrderBy(x=> x.Navn).Skip(start).Take(amount).ToList();
            return new ListResponse<CompanyBaseObject>(
                companies.Select(x => new CompanyBaseObject(x)).ToList(), 
                await total, 
                start, 
                amount
            );
        }

        public Firma FetchCompanyAsFirma(int id)
        {
            var data = FetchCompany(id);
            return new Firma(data);
        }

        public FirmaRequest FetchCompany (int OrgNr)
        {
            var data = GeneralHelper.HttpGet($"{Statics.ExtenralApi}/enheter/{OrgNr}");
            var company = Newtonsoft.Json.JsonConvert.DeserializeObject<FirmaRequest>(data);
            return company;            
        }
    }
}
