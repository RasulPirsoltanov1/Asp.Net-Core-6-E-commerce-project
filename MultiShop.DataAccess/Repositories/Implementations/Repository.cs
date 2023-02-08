﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MultiShop.DataAccess.Contexts;
using MultiShop.DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.DataAccess.Repositories.Implementations
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly AppDbContext _context;

        public Repository(AppDbContext context)
        {
            _context = context;
        }
        private DbSet<T> Table => _context.Set<T>();

     

        public IQueryable<T> GetAll()
        {
            return Table.AsQueryable();
        }
        public async Task CreateAsync(T entity)
        {
            await Table.AddAsync(entity);
        }
        public IQueryable<T> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task SaveAsync()
        {
           await _context.SaveChangesAsync();
        }
    }
}
