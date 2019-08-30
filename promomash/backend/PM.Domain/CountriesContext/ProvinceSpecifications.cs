using PM.Models;
using SilentNotary.Specifications;

namespace PM.Domain.CountriesContext
{
    public class ProvinceSpecifications
    {
        public static Specification<Province> WithCountryId(long id)
        {
            return new QuerySpecification<Province>(wallet => wallet.Country.Id == id);
        }
    }
}