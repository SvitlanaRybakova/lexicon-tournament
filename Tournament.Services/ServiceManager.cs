using System;
using System.Collections.Generic;
using System.Linq;

using Tournament.Contracts;


namespace Tournament.Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<ITournamentService> _tournamentService;
        private readonly Lazy<IGameService> _gameService;
        public ITournamentService TournamentService => _tournamentService.Value;
        public IGameService GameService => _gameService.Value;
        public ServiceManager(Lazy<ITournamentService> tournamentService, Lazy<IGameService> gameService)
        {
            _tournamentService = tournamentService;
            _gameService = gameService;
        }
    }
}
