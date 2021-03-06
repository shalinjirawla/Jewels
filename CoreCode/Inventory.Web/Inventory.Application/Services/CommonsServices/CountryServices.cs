﻿using Inventory.Application.Interface.Common;
using Inventory.Application.ViewModel;
using Inventory.Core.Models;
using Inventory.EntityFrameworkCore.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inventory.Application.Services.CommonsServices
{
    public class CountryServices : ICountry
    {
        private readonly ApplicationDbContext _DbContext;

        public CountryServices(ApplicationDbContext DbContext)
        {
            _DbContext = DbContext;
        }

        public int AddCountryAsyc(CountryVm model, string UserId, long TenantId)
        {
            int CountryId = 0;
            try
            {

                if (model != null)
                {
                    if (model.CountryId == 0)
                    {
                        Country country = new Country();
                        country.CountryName = model.CountryName;
                        country.CountryCode = model.CountryCode;
                        country.CreationTime = DateTime.Now;
                        country.LastModificationTime = DateTime.Now;
                        country.CreatorUserId = UserId;
                        country.LastModifierUserId = UserId;
                        country.TenantsId = TenantId;
                        _DbContext.country.Add(country);
                        _DbContext.SaveChanges();
                        CountryId = int.Parse(country.CountryId.ToString());
                    }
                    else
                    {
                        Country country = new Country();
                        country = _DbContext.country.FirstOrDefault(x => x.CountryId == model.CountryId);
                        if (country != null)
                        {
                            
                            country.CountryName = model.CountryName;
                            country.CountryCode = model.CountryCode;
                            country.LastModificationTime = DateTime.Now;
                            country.LastModifierUserId = UserId;
                            _DbContext.Update(country);
                            _DbContext.SaveChanges();
                            CountryId = int.Parse(country.CountryId.ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return CountryId;
        }

        public int DeleteCountryAsyc(int Id)
        {
            int CountryId = 0;
            try
            {
                var country = _DbContext.country.Where(x => x.CountryId == Id).FirstOrDefault();
                CountryId = int.Parse(country.CountryId.ToString());
                _DbContext.country.Remove(country);
                _DbContext.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return CountryId;
        }

        public CountryVm GetCountryAsyc(int id)
        {
            CountryVm country = new CountryVm();
            try
            {
                var a = _DbContext.country.Where(x => x.CountryId == id).FirstOrDefault();
                country.CountryId = a.CountryId;
                country.CountryName = a.CountryName;
                country.CountryCode = a.CountryCode;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return country;
        }

        public List<CountryVm> GetCountryList()
        {
            List<CountryVm> countryList = new List<CountryVm>();
            try
            {
                var countries = _DbContext.country.ToList();
                foreach (var a in countries)
                {
                    CountryVm country = new CountryVm();
                    country.CountryId = a.CountryId;
                    country.CountryName = a.CountryName;
                    country.CountryCode = a.CountryCode;
                    countryList.Add(country);
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
            return countryList;
        }

    }
}
