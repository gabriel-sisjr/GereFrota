﻿using Data.Repositories.BaseRepository;
using Domain.Abstract.Repositories;
using Domain.Entities;
using Domain.Entities.Context;

namespace Data.Repositories
{
    public class TiposManutencaoRepository : BaseRepository<TiposManutencao>, ITiposManutencaoRepository
    {
        public TiposManutencaoRepository(ContextDB context) : base(context) { }
    }
}
