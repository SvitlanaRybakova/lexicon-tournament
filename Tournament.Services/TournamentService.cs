using Microsoft.AspNetCore.JsonPatch;
using Tournament.Contracts;
using Tournament.Core.DTOs.Tournament;
using Tournament.Core.Entities;
using Tournament.Core.Repositories;
using AutoMapper;

namespace Tournament.Services
{
    public class TournamentService : ITournamentService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public TournamentService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task DeleteTournament(int id)
        {
            var tournament = await _uow.TournamentRepository.GetAsync(id);
            if (tournament == null)
            {
                throw new KeyNotFoundException("Tournament not found.");
            }

            _uow.TournamentRepository.Remove(tournament);
            await _uow.SaveAsync();
        }

        public async Task<TournamentDto> GetTournamentAsync(int id, bool trackChanges = false)
        {
            var tournament = await _uow.TournamentRepository.GetAsync(id);
            if (tournament == null)
            {
                throw new KeyNotFoundException("Tournament not found.");
            }

            return _mapper.Map<TournamentDto>(tournament);
        }

        public async Task<IEnumerable<TournamentDto>> GetTournamentsAsync(bool includeGames, bool trackChanges = false)
        {
            var tournaments = await _uow.TournamentRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<TournamentDto>>(tournaments);
        }

        public async Task PatchTournament(int id, JsonPatchDocument<TournamentDto> patchDocument)
        {
            if (patchDocument == null)
            {
                throw new ArgumentNullException(nameof(patchDocument), "Patch document cannot be null.");
            }

            var tournament = await _uow.TournamentRepository.GetAsync(id);
            if (tournament == null)
            {
                throw new KeyNotFoundException("Tournament not found.");
            }

            var tournamentDto = _mapper.Map<TournamentDto>(tournament);
            patchDocument.ApplyTo(tournamentDto);

            if (!ValidateModelState(tournamentDto))
            {
                throw new InvalidOperationException("Invalid patch document.");
            }

            _mapper.Map(tournamentDto, tournament);
            await _uow.SaveAsync();
        }

        public async Task PostTournament(TournamentDto dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto), "Tournament data cannot be null.");
            }

            var tournament = _mapper.Map<TournamentModel>(dto);
            _uow.TournamentRepository.Add(tournament);
            await _uow.SaveAsync();
        }

        public async Task PutTournament(int id, TournamentDto dto)
        {
            var tournament = await _uow.TournamentRepository.GetAsync(id);
            if (tournament == null)
            {
                throw new KeyNotFoundException("Tournament not found.");
            }

            _mapper.Map(dto, tournament);
            _uow.TournamentRepository.Update(tournament);
            await _uow.SaveAsync();
        }

        private bool ValidateModelState<T>(T dto)
        {
          //todo implement validation
            return true;
        }
    }
}
