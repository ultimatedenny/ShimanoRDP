using System;

namespace ShimanoRDP.Tools.CustomCollections
{
    public interface INotifyCollectionUpdated<T>
    {
        event EventHandler<CollectionUpdatedEventArgs<T>> CollectionUpdated;
    }
}