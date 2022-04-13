using DAL.App.EF;
using DAL.App.Repositories;
using DAL.Repositories;
using PublicApi.DTO.v1.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.App.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            Games = new GamesRepository(_context, new GameMapper());
        }

        public IGamesRepository Games { get; set; }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
