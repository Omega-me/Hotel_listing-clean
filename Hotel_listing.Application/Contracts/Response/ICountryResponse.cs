﻿namespace Hotel_listing.Application.Contracts.Response;
public interface ICountryResponse:IBaseResponse<object,object>
{
    string? Token { get; set; }
}