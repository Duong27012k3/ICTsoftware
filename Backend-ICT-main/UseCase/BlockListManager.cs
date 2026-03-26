using System;
using System.Collections.Generic;
using System.Text;
using Entity;
namespace UseCase
{
    public class BlockListManager
    {
        private readonly IBlockRepository _blockRepository;
        public BlockListManager(IBlockRepository blockRepository)
        {
            _blockRepository = blockRepository;
        }
        public IEnumerable<Block> GetBlocksByOwnerType(string ownerType, int ownerId)
        {
            return _blockRepository.GetByOwner(ownerType, ownerId);
        }
        public IEnumerable<Block> GetBlocksByService(int serviceID)
        {
            return _blockRepository.GetByOwner("service", serviceID);
        }
        public IEnumerable<Block> GetBlocksByProject(int projectID)
        {
            return _blockRepository.GetByOwner("project", projectID);
        }
        public Block GetBlockByID(int id)
        {
            return _blockRepository.GetByID(id);
        }

        public void AddBlock(Block block)
        {
            if (string.IsNullOrWhiteSpace(block.OwnerType))
                throw new ArgumentException("OwnerType khong duoc de trong.");
            if (block.OwnerId <= 0)
                throw new ArgumentException("OwnerId khong hop le.");
            if (string.IsNullOrWhiteSpace(block.BlockType))
                throw new ArgumentException("BlockType khong duoc de trong.");

            _blockRepository.Add(block);
        }

        public void UpdateBlock(Block block)
        {
            var existing = _blockRepository.GetByID(block.BlockId);
            if (existing == null)
                throw new Exception("Khong tim thay block.");

            existing.BlockType = block.BlockType;
            existing.BlockOrder = block.BlockOrder;
            existing.ImageUrl = block.ImageUrl;
            _blockRepository.Update(existing);
        }

        public void DeleteBlock(int id)
        {
            var existing = _blockRepository.GetByID(id);
            if (existing == null)
                throw new Exception("Khong tim thay block.");

            _blockRepository.Delete(id);
        }

        public void MoveBlockUp(int id)
        {
            var block = _blockRepository.GetByID(id);
            if (block == null) throw new Exception("Khong tim thay block.");
            if (block.BlockOrder <= 1) return;

            _blockRepository.UpdateOrder(id, block.BlockOrder - 1);
        }

        public void MoveBlockDown(int id)
        {
            var block = _blockRepository.GetByID(id);
            if (block == null) throw new Exception("Khong tim thay block.");

            _blockRepository.UpdateOrder(id, block.BlockOrder + 1);
        }
    }
}
