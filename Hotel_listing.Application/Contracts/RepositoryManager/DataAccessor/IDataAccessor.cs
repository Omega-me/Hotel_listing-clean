﻿using Hotel_listing.Application.Common.Features;

namespace Hotel_listing.Application.Contracts.RepositoryManager.DataAccessor;

public interface IDataAccessor
{
    Task<IEnumerable<T>> Query<T,TParams>(DataAccessorOptions<TParams> options);
    Task Command<TPramas>(DataAccessorOptions<TPramas> options);
}