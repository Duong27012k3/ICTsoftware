using Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using UseCase;

namespace Infrastructure
{
    public class PostgresBlockRepository : IBlockRepository
    {
        private readonly AppDbContext _context;

        public PostgresBlockRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Block> GetByOwner(string ownerType, int ownerId)
        {
            // Ensure a non-null collection is always returned
            return _context.Blocks?
                .Include(b => b.BlockTrans)
                .Where(b => b.OwnerType == ownerType && b.OwnerId == ownerId)
                .OrderBy(b => b.BlockOrder)
                .ToList() ?? new List<Block>();
        }

        public Block GetByID(int id)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return _context.Blocks?
        .Include(b => b.BlockTrans)
        .FirstOrDefault(b => b.BlockId == id);
#pragma warning restore CS8603 // Possible null reference return.
                              //var block = _context.Blocks?
                              //    .Include(b => b.BlockTrans)
                              //    .FirstOrDefault(b => b.BlockId == id);

            //if (block == null)
            //    throw new InvalidOperationException($"Block with ID {id} not found.");

            //return block;
        }

        public void Add(Block block)
        {
            _context.Blocks.Add(block);
            _context.SaveChanges();
        }

        public void Update(Block block)
        {
            _context.Blocks.Update(block);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var block = _context.Blocks.Find(id);
            if (block != null)
            {
                _context.Blocks.Remove(block);
                _context.SaveChanges();
            }
        }

        public void UpdateOrder(int blockId, int newOrder)
        {
            var block = _context.Blocks.Find(blockId);
            if (block != null)
            {
                block.BlockOrder = newOrder;
                _context.SaveChanges();
            }
        }
    }
}
