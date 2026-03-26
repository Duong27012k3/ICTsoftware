using System;
using System.Collections.Generic;
using System.Text;
using Entity;

namespace UseCase
{
    public interface IBlockRepository
    {
        IEnumerable<Block> GetByOwner(string ownerType, int ownerID);
        Block GetByID(int id);
        void Add(Block block);
        void Update(Block block);
        void Delete(int id);
        void UpdateOrder(int blockId, int newOrder);
    }
}
