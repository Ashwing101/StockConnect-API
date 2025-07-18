using api.Data;
using api.Dtos.Stock;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    class StockRepository : IStockRepository
    {           
        private readonly ApplicationDbContext _context;
            public StockRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Stock> CreateAsync(Stock stockModel)
        {
            await _context.Stock.AddAsync(stockModel);
            await _context.SaveChangesAsync();
            return stockModel;
        }

        public async Task<Stock?> DeleteAsync(int id)
        {
            var stockModel = await _context.Stock.FirstOrDefaultAsync(row => row.Id == id);
           

            if (stockModel == null) 
            {
                return null;
            }
            _context.Stock.Remove(stockModel);
            await _context.SaveChangesAsync();
            return stockModel;

        }

        public async Task<List<Stock>> GetAllAsync()
        {

            var stockModel = await _context.Stock.Include(c=>c.Comments).ToListAsync();
            
           return stockModel;
        }

        public async Task<Stock?> GetByIdAsync(int id)
        {
           return await _context.Stock.Include(c=>c.Comments).FirstOrDefaultAsync(row => row.Id == id);
       
        }

        public Task<bool> StockExist(int id)
        {
            return _context.Stock.AnyAsync(row => row.Id == id);
        }

        public async Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto stockDto)
        {
            var stockModel = await _context.Stock.FirstOrDefaultAsync(row => row.Id == id);

            if (stockModel == null)
            {
                return null;
            }

            stockModel.Symbol = stockDto.Symbol;
            stockModel.CompanyName = stockDto.CompanyName;
            stockModel.Industry = stockDto.Industry;
            stockModel.LastDiv = stockDto.LastDiv;
            stockModel.MarketCap = stockDto.MarketCap;
            stockModel.Purchase = stockDto.Purchase;

            _context.SaveChangesAsync();
            return stockModel;
        }
    }

   
}