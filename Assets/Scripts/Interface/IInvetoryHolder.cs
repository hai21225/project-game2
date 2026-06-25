
// use for all inventory holders, like chests, players, etc.
public interface IInventoryHolder
{
    public Inventory GetInventory();

    public string DisplayName { get; }
}