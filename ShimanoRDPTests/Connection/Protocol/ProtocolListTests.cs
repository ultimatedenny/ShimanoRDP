﻿using System.Collections;
using System.Collections.Specialized;
using ShimanoRDP.Connection.Protocol;
using ShimanoRDP.Connection.Protocol.SSH;
using ShimanoRDP.Connection.Protocol.Telnet;
using ShimanoRDP.Connection.Protocol.VNC;
using NUnit.Framework;


namespace ShimanoRDPTests.Connection.Protocol
{
    public class ProtocolListTests
    {
        private ProtocolList _protocolList;
        private ProtocolBase _protocol1;
        private ProtocolBase _protocol2;
        private ProtocolBase _protocol3;


        [SetUp]
        public void Setup()
        {
            _protocolList = new ProtocolList();
            _protocol1 = new ProtocolTelnet();
            _protocol2 = new ProtocolSSH2();
            _protocol3 = new ProtocolVNC();
        }

        [TearDown]
        public void Teardown()
        {
            _protocolList = null;
            _protocol1 = null;
            _protocol2 = null;
            _protocol3 = null;
        }

        [Test]
        public void EmptyWhenInitialized()
        {
            Assert.That(_protocolList.Count == 0);
        }

        [Test]
        public void AddAddsObjectToList()
        {
            _protocolList.Add(_protocol1);
            Assert.That(_protocolList[0] == _protocol1);
        }

        [Test]
        public void AddRangeAddsAllObjects()
        {
            var protArray = new[] {_protocol1, _protocol2, _protocol3};
            _protocolList.AddRange(protArray);
            Assert.That(_protocolList, Is.EquivalentTo(protArray));
        }

        [Test]
        public void CountUpdatesToReflectCurrentList()
        {
            var protArray = new[] { _protocol1, _protocol2, _protocol3 };
            _protocolList.AddRange(protArray);
            Assert.That(_protocolList.Count == protArray.Length);
        }

        [Test]
        public void RemoveRemovesObjectFromList()
        {
            _protocolList.Add(_protocol1);
            _protocolList.Remove(_protocol1);
            Assert.That(_protocolList.Count == 0);
        }

        [Test]
        public void ClearResetsList()
        {
            var protArray = new[] { _protocol1, _protocol2, _protocol3 };
            _protocolList.AddRange(protArray);
            _protocolList.Clear();
            Assert.That(_protocolList.Count == 0);
        }

        [Test]
        public void IntIndexerReturnsCorrectObject()
        {
            var protArray = new[] { _protocol1, _protocol2, _protocol3 };
            _protocolList.AddRange(protArray);
            Assert.That(_protocolList[1], Is.EqualTo(protArray[1]));
        }

        [Test]
        public void ObjectIndexerReturnsCorrectObject()
        {
            var protArray = new[] { _protocol1, _protocol2, _protocol3 };
            _protocolList.AddRange(protArray);
            Assert.That(_protocolList[_protocol3], Is.EqualTo(_protocol3));
        }

        [Test]
        public void IndexerSafelyHandlesUnknownObjects()
        {
            var protArray = new[] { _protocol1, _protocol2, _protocol3 };
            _protocolList.AddRange(protArray);
            Assert.That(_protocolList["unacceptablevalue"], Is.EqualTo(null));
        }

        [Test]
        public void RemovingNonexistantObjectFromListDoesNothing()
        {
            Assert.DoesNotThrow(()=> _protocolList.Remove(_protocol1));
        }

        [Test]
        public void AddRaisesCollectionChangedEvent()
        {
            var eventWasCalled = false;
            _protocolList.CollectionChanged += (sender, args) => eventWasCalled = true;
            _protocolList.Add(_protocol1);
            Assert.That(eventWasCalled);
        }

        [Test]
        public void AddCollectionChangedEventContainsAddedObject()
        {
            IList nodeListFromEvent = new ArrayList();
            _protocolList.CollectionChanged += (sender, args) => nodeListFromEvent = args.NewItems;
            _protocolList.Add(_protocol1);
            Assert.That(nodeListFromEvent, Is.EquivalentTo(new[] {_protocol1}));
        }

        [Test]
        public void AddRangeCollectionChangedEventContainsAddedObjects()
        {
            var protArray = new[] { _protocol1, _protocol2, _protocol3 };
            IList nodeListFromEvent = new ArrayList();
            _protocolList.CollectionChanged += (sender, args) => nodeListFromEvent = args.NewItems;
            _protocolList.AddRange(protArray);
            Assert.That(nodeListFromEvent, Is.EquivalentTo(protArray));
        }

        [Test]
        public void RemoveCollectionChangedEventContainsRemovedObject()
        {
            IList nodeListFromEvent = new ArrayList();
            _protocolList.Add(_protocol1);
            _protocolList.CollectionChanged += (sender, args) => nodeListFromEvent = args.OldItems;
            _protocolList.Remove(_protocol1);
            Assert.That(nodeListFromEvent, Is.EquivalentTo(new[] { _protocol1 }));
        }

        [Test]
        public void AttemptingToRemoveNonexistantObjectDoesNotRaiseCollectionChangedEvent()
        {
            var eventWasCalled = false;
            _protocolList.CollectionChanged += (sender, args) => eventWasCalled = true;
            _protocolList.Remove(_protocol1);
            Assert.That(eventWasCalled == false);
        }

        [Test]
        public void ClearRaisesCollectionChangedEvent()
        {
            var eventWasCalled = false;
            _protocolList.Add(_protocol1);
            _protocolList.CollectionChanged += (sender, args) => eventWasCalled = true;
            _protocolList.Clear();
            Assert.That(eventWasCalled);
        }

        [Test]
        public void ClearDoesntRaiseCollectionChangedEventWhenNoObjectsRemoved()
        {
            var eventWasCalled = false;
            _protocolList.CollectionChanged += (sender, args) => eventWasCalled = true;
            _protocolList.Clear();
            Assert.That(eventWasCalled == false);
        }

        [Test]
        public void AddCollectionChangedEventHasCorrectAction()
        {
            NotifyCollectionChangedAction collectionChangedAction = NotifyCollectionChangedAction.Move;
            _protocolList.CollectionChanged += (sender, args) => collectionChangedAction = args.Action;
            _protocolList.Add(_protocol1);
            Assert.That(collectionChangedAction, Is.EqualTo(NotifyCollectionChangedAction.Add));
        }

        [Test]
        public void AddRangeCollectionChangedEventHasCorrectAction()
        {
            NotifyCollectionChangedAction collectionChangedAction = NotifyCollectionChangedAction.Move;
            _protocolList.CollectionChanged += (sender, args) => collectionChangedAction = args.Action;
            _protocolList.AddRange(new []{_protocol1});
            Assert.That(collectionChangedAction, Is.EqualTo(NotifyCollectionChangedAction.Add));
        }

        [Test]
        public void RemoveCollectionChangedEventHasCorrectAction()
        {
            NotifyCollectionChangedAction collectionChangedAction = NotifyCollectionChangedAction.Move;
            _protocolList.Add(_protocol1);
            _protocolList.CollectionChanged += (sender, args) => collectionChangedAction = args.Action;
            _protocolList.Remove(_protocol1);
            Assert.That(collectionChangedAction, Is.EqualTo(NotifyCollectionChangedAction.Remove));
        }

        [Test]
        public void ClearCollectionChangedEventHasCorrectAction()
        {
            NotifyCollectionChangedAction collectionChangedAction = NotifyCollectionChangedAction.Move;
            _protocolList.Add(_protocol1);
            _protocolList.CollectionChanged += (sender, args) => collectionChangedAction = args.Action;
            _protocolList.Clear();
            Assert.That(collectionChangedAction, Is.EqualTo(NotifyCollectionChangedAction.Reset));
        }
    }
}