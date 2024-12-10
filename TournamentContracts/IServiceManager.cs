﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tournament.Contracts
{
    public interface IServiceManager
    {
        ITournamentService TournamentService { get; }
        IGameService GameService { get; }
    }
}