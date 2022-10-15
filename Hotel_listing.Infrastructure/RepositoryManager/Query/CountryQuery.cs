using AutoMapper;
using Hotel_listing.Application.Common.Features;
using Hotel_listing.Application.Common.RepositoryOptions;
using Hotel_listing.Application.Contracts.RepositoryManager.DataAccessor;
using Hotel_listing.Application.Contracts.RepositoryManager.Query;
using Hotel_listing.Application.DTO.Country;
using Hotel_listing.Domain.Entitites;
using Hotel_listing.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Hotel_listing.Infrastructure.RepositoryManager.Query;

public class CountryQuery:BaseQuery<Country>, ICountryQuery
{
    public CountryQuery(DatabaseContext context,IDataAccessor db, IMapper mapper) : base(context,db,mapper)
    { }
}