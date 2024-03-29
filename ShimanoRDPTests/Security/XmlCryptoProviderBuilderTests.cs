﻿using System;
using System.Xml.Linq;
using ShimanoRDP.Security;
using ShimanoRDP.Security.Factories;
using ShimanoRDP.Security.SymmetricEncryption;
using NUnit.Framework;


namespace ShimanoRDPTests.Security
{
    public class XmlCryptoProviderBuilderTests
    {
        [Test]
        public void BuildsCorrectEncryptionEngine()
        {
            var element = BuildValidElement();
            var builder = new CryptoProviderFactoryFromXml(element);
            var cryptoProvider = builder.Build();
            Assert.That(cryptoProvider.CipherEngine, Is.EqualTo(BlockCipherEngines.Serpent));
        }

        [Test]
        public void BuildsCorrectCipherMode()
        {
            var element = BuildValidElement();
            var builder = new CryptoProviderFactoryFromXml(element);
            var cryptoProvider = builder.Build();
            Assert.That(cryptoProvider.CipherMode, Is.EqualTo(BlockCipherModes.EAX));
        }

        [Test]
        public void BuildsCorrectKdfIterations()
        {
            var element = BuildValidElement();
            var builder = new CryptoProviderFactoryFromXml(element);
            var cryptoProvider = builder.Build();
            Assert.That(cryptoProvider.KeyDerivationIterations, Is.EqualTo(1234));
        }

        [Test]
        public void CantPassNullIntoConstructor()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                // ReSharper disable once ObjectCreationAsStatement
                new CryptoProviderFactoryFromXml(null);
            });
        }

        [Test]
        public void ReturnsOldProviderTypeIfXmlIsntValid()
        {
            var badElement = new XElement("BadElement");
            var builder = new CryptoProviderFactoryFromXml(badElement);
            var cryptoProvider = builder.Build();
            Assert.That(cryptoProvider, Is.TypeOf<LegacyRijndaelCryptographyProvider>());
        }

        private static XElement BuildValidElement()
        {
            return new XElement("TestElement",
                new XAttribute("EncryptionEngine", "Serpent"),
                new XAttribute("BlockCipherMode", "EAX"),
                new XAttribute("KdfIterations", "1234"));
        }
    }
}