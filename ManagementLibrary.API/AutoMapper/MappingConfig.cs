using AutoMapper;
using GetWay.API.Models;
using GetWay.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GetWay.API.AutoMapper
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMappingFromEntitiesToViewModels();
            CreateMappingFromViewModelsToEntities();

        }

        private void CreateMappingFromViewModelsToEntities()
        {
            CreateMap<CarCreateViewModel, Car>();
            CreateMap<CarUpdateViewModel, Car>();
        }

        private void CreateMappingFromEntitiesToViewModels()
        {
            // YOUR code
        }
    }
}