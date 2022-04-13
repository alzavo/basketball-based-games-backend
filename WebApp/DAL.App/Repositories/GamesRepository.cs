﻿using DAL.App.EF;
using DAL.Base;
using Mapping;
using Microsoft.EntityFrameworkCore;
using PublicApi.DTO.v1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity = Domain.App.Game;
using Dto = PublicApi.DTO.v1.Game;
using DAL.Repositories;

namespace DAL.App.Repositories
{
    public class GamesRepository : BaseRepository<int, Entity, Dto>, IGamesRepository
    {
        public GamesRepository(AppDbContext dbContext, IMapper<Entity, Dto> mapper) : base(dbContext, mapper)
        {
        }
    }
}