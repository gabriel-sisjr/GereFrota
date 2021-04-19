﻿using Data.Repositories.BaseRepository;
using Domain.Abstract.Repositories;
using Domain.Entities;
using Domain.Entities.Context;

namespace Data.Repositories
{
    public class FrotaRepository : BaseRepository<Frota>, IFrotaRepository
    {
        public FrotaRepository(ContextDB context) : base(context) { }
    }
}
