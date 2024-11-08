﻿using ArbitraX.Core.Repositories;
using Solution.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Solution.Core.Entities;

namespace ArbitraX.Infrastructure.Persistence.Repositories;

public class CoinRepository : ICoinRepository
{
    private readonly ApplicationDbContext _context;

    public CoinRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Crypto>> GetAll()
    {
        return await _context.Cryptos
            .Where(c => c.Active == true)
            .ToListAsync();
    }
}

