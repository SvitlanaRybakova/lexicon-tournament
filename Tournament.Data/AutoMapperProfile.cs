﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tournament.Core.DTOs.Game;
using Tournament.Core.DTOs.Tournament;
using Tournament.Core.Entities;


namespace Tournament.Data
{
    public class AutoMapperProfile : Profile

    {
        public AutoMapperProfile()
        {
            CreateMap<Game, CreateGameDto>();
            CreateMap<CreateGameDto, Game>();

            CreateMap<Game, GameDto>();
            CreateMap<GameDto, Game>();


            // model => dto (+ end date property)
            CreateMap<TournamentModel, TournamentDto>()
                .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.StartDate.AddMonths(3)));

            // dto => model
            CreateMap<CreateTournamentDto, TournamentModel>();

            CreateMap<TournamentModel, CreateTournamentDto>()
            .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate));

            CreateMap<CreateTournamentDto, TournamentModel>()
            .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate));
    }

    }
}
