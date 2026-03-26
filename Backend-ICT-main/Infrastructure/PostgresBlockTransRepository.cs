using Entity;
using System;
using System.Collections.Generic;
using System.Text;
using UseCase;

namespace Infrastructure
{
    public class PostgresBlockTransRepository : IBlockTransRepository
    {
        private readonly AppDbContext _context;

        public PostgresBlockTransRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<BlockTrans> GetByBlockID(int blockId)
        {
            return _context.BlockTrans
                .Where(bt => bt.BlockId == blockId)
                .ToList();
        }

        public BlockTrans GetByID(int id)
        {
            return _context.BlockTrans.Find(id);
        }

        public void Add(BlockTrans blockTrans)
        {
            _context.BlockTrans.Add(blockTrans);
            _context.SaveChanges();
        }

        public void Update(BlockTrans blockTrans)
        {
            _context.BlockTrans.Update(blockTrans);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var trans = _context.BlockTrans.Find(id);
            if (trans != null)
            {
                _context.BlockTrans.Remove(trans);
                _context.SaveChanges();
            }
        }
    }
}
