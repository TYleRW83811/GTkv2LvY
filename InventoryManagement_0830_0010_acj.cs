// 代码生成时间: 2025-08-30 00:10:25
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// 库存管理类
public class InventoryManagement
{
    // 库存项类
    public class InventoryItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Quantity { get; set; }

        public InventoryItem(int id, string name, double quantity)
        {
            Id = id;
            Name = name;
            Quantity = quantity;
        }
    }

    private List<InventoryItem> inventory = new List<InventoryItem>();

    // 添加库存项
    public void AddItem(int id, string name, double quantity)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Item name cannot be empty.");
        }

        if (quantity < 0)
        {
            throw new ArgumentException("Quantity cannot be negative.");
        }

        var existingItem = inventory.FirstOrDefault(i => i.Id == id);
        if (existingItem != null)
        {
            existingItem.Quantity += quantity;
        }
        else
        {
            inventory.Add(new InventoryItem(id, name, quantity));
        }
    }

    // 移除库存项
    public bool RemoveItem(int id)
    {
        var item = inventory.FirstOrDefault(i => i.Id == id);
        if (item == null)
        {
            return false;
        }

        inventory.Remove(item);
        return true;
    }

    // 更新库存项数量
    public void UpdateQuantity(int id, double quantity)
    {
        if (quantity < 0)
        {
            throw new ArgumentException("Quantity cannot be negative.");
        }

        var item = inventory.FirstOrDefault(i => i.Id == id);
        if (item == null)
        {
            throw new KeyNotFoundException("Item not found.");
        }

        item.Quantity = quantity;
    }

    // 获取库存项信息
    public List<InventoryItem> GetInventoryItems()
    {
        return inventory.ToList();
    }
}
