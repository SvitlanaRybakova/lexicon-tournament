using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tournament.Core.DTOs.Game;
using Tournament.Core.Entities;


namespace Tournament.Data
{
    public class AutoMapperProfile : Profile

    {
        public AutoMapperProfile()
        {
            //CreateMap<Game, CreateGameDto>();
            CreateMap<CreateGameDto, Game>();
        }

    }
}
