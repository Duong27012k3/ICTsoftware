using Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace UseCase
{
    public interface IBlockTransRepository
    {
        IEnumerable<BlockTrans> GetByBlockID(int blockId);
        BlockTrans GetByID(int id);
        void Add(BlockTrans blockTrans);
        void Update(BlockTrans blockTrans);
        void Delete(int id);
    }
}
