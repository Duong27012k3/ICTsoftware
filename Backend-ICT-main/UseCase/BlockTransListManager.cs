using Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace UseCase
{
    public class BlockTransListManager
    {
        private readonly IBlockTransRepository _blockTransRepository;

        public BlockTransListManager(IBlockTransRepository blockTransRepository)
        {
            _blockTransRepository = blockTransRepository;
        }

        public IEnumerable<BlockTrans> GetTransByBlockID(int blockId)
        {
            return _blockTransRepository.GetByBlockID(blockId);
        }

        public BlockTrans GetTransByID(int id)
        {
            return _blockTransRepository.GetByID(id);
        }

        public void AddTrans(BlockTrans blockTrans)
        {
            if (blockTrans.BlockId <= 0)
                throw new ArgumentException("BlockId khong hop le.");
            if (string.IsNullOrWhiteSpace(blockTrans.LangCode))
                throw new ArgumentException("Ma ngon ngu khong duoc de trong.");

            _blockTransRepository.Add(blockTrans);
        }

        public void UpdateTrans(BlockTrans blockTrans)
        {
            var existing = _blockTransRepository.GetByID(blockTrans.BlockTransId);
            if (existing == null)
                throw new Exception("Khong tim thay ban dich block.");

            existing.Title = blockTrans.Title;
            existing.Content = blockTrans.Content;
            existing.LangCode = blockTrans.LangCode;
            _blockTransRepository.Update(existing);
        }

        public void DeleteTrans(int id)
        {
            var existing = _blockTransRepository.GetByID(id);
            if (existing == null)
                throw new Exception("Khong tim thay ban dich block.");

            _blockTransRepository.Delete(id);
        }
    }
}
