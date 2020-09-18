using System.Collections.Generic;
using System.ComponentModel;

namespace ShimanoRDP.Tools.CustomCollections
{
    public interface IFullyNotifiableList<T> : IList<T>, INotifyCollectionUpdated<T>
        where T : INotifyPropertyChanged
    {
    }
}