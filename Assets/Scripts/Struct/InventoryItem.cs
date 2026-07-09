using System;
using Unity.Netcode;

public struct InventoryItem : INetworkSerializable, IEquatable<InventoryItem>
{
    public int ItemId;
    public NetworkObjectReference HomeContainer;


    public bool Equals(InventoryItem other)
    {
        return ItemId == other.ItemId && HomeContainer.Equals(other.HomeContainer);
    }

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue(ref ItemId);
        serializer.SerializeValue(ref HomeContainer);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(ItemId, HomeContainer);
    }

}
