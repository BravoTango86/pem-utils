﻿using System.Linq;
using DerConverter.Asn;
using NUnit.Framework;

namespace DerConverter.Tests.Asn
{
    [TestFixture]
    public class DerAsnBitStringTests
    {
        [Test]
        public void DerAsnBitString_Parse_ShouldDecodeCorrectly()
        {
            var data = new byte[] { 0x03, 0x81, 0x81, 0x05,
                0x47, 0xeb, 0x99, 0x5a, 0xdf, 0x9e, 0x70, 0x0d, 0xfb, 0xa7, 0x31, 0x32, 0xc1, 0x5f, 0x5c, 0x24,
                0xc2, 0xe0, 0xbf, 0xc6, 0x24, 0xaf, 0x15, 0x66, 0x0e, 0xb8, 0x6a, 0x2e, 0xab, 0x2b, 0xc4, 0x97,
                0x1f, 0xe3, 0xcb, 0xdc, 0x63, 0xa5, 0x25, 0xec, 0xc7, 0xb4, 0x28, 0x61, 0x66, 0x36, 0xa1, 0x31,
                0x1b, 0xbf, 0xdd, 0xd0, 0xfc, 0xbf, 0x17, 0x94, 0x90, 0x1d, 0xe5, 0x5e, 0xc7, 0x11, 0x5e, 0xc9,
                0x55, 0x9f, 0xeb, 0xa3, 0x3e, 0x14, 0xc7, 0x99, 0xa6, 0xcb, 0xba, 0xa1, 0x46, 0x0f, 0x39, 0xd4,
                0x44, 0xc4, 0xc8, 0x4b, 0x76, 0x0e, 0x20, 0x5d, 0x6d, 0xa9, 0x34, 0x9e, 0xd4, 0xd5, 0x87, 0x42,
                0xeb, 0x24, 0x26, 0x51, 0x14, 0x90, 0xb4, 0x0f, 0x06, 0x5e, 0x52, 0x88, 0x32, 0x7a, 0x95, 0x20,
                0xa0, 0xfd, 0xf7, 0xe5, 0x7d, 0x60, 0xdd, 0x72, 0x68, 0x9b, 0xf5, 0x7b, 0x05, 0x8f, 0x6d, 0x1e
            };

            var type = DerAsnType.Parse(data);
            Assert.That(type is DerAsnBitString, Is.True);

            var bitString = type as DerAsnBitString;
            Assert.That(bitString.UnusedLowerBitsInLastByte, Is.EqualTo(0x05));
            Assert.That((bitString.Value as byte[]), Is.EqualTo(data.Skip(4).ToArray()));
        }

        [Test]
        public void DerAsnBitString_GetBytes_ShouldEncodeCorrectly()
        {
            var data = new byte[] {
                0x47, 0xeb, 0x99, 0x5a, 0xdf, 0x9e, 0x70, 0x0d, 0xfb, 0xa7, 0x31, 0x32, 0xc1, 0x5f, 0x5c, 0x24,
                0xc2, 0xe0, 0xbf, 0xc6, 0x24, 0xaf, 0x15, 0x66, 0x0e, 0xb8, 0x6a, 0x2e, 0xab, 0x2b, 0xc4, 0x97,
                0x1f, 0xe3, 0xcb, 0xdc, 0x63, 0xa5, 0x25, 0xec, 0xc7, 0xb4, 0x28, 0x61, 0x66, 0x36, 0xa1, 0x31,
                0x1b, 0xbf, 0xdd, 0xd0, 0xfc, 0xbf, 0x17, 0x94, 0x90, 0x1d, 0xe5, 0x5e, 0xc7, 0x11, 0x5e, 0xc9,
                0x55, 0x9f, 0xeb, 0xa3, 0x3e, 0x14, 0xc7, 0x99, 0xa6, 0xcb, 0xba, 0xa1, 0x46, 0x0f, 0x39, 0xd4,
                0x44, 0xc4, 0xc8, 0x4b, 0x76, 0x0e, 0x20, 0x5d, 0x6d, 0xa9, 0x34, 0x9e, 0xd4, 0xd5, 0x87, 0x42,
                0xeb, 0x24, 0x26, 0x51, 0x14, 0x90, 0xb4, 0x0f, 0x06, 0x5e, 0x52, 0x88, 0x32, 0x7a, 0x95, 0x20,
                0xa0, 0xfd, 0xf7, 0xe5, 0x7d, 0x60, 0xdd, 0x72, 0x68, 0x9b, 0xf5, 0x7b, 0x05, 0x8f, 0x6d, 0x1e
            };

            var type = new DerAsnBitString(data, 0x05);

            var bytes = type.GetBytes();
            Assert.That((DerAsnTypeTag)bytes[0], Is.EqualTo(DerAsnTypeTag.BitString));
            Assert.That(bytes[1], Is.EqualTo(0x81));
            Assert.That(bytes[2], Is.EqualTo(0x81));
            Assert.That(bytes[3], Is.EqualTo(0x05));
            Assert.That(bytes.Skip(4).ToArray(), Is.EqualTo(data));
        }
    }
}
