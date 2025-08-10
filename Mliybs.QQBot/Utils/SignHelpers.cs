using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto.Signers;
using Org.BouncyCastle.Security;

namespace Mliybs.QQBot.Utils
{
    public class SignHelpers
    {
        public static (Ed25519PublicKeyParameters PublicKey, Ed25519PrivateKeyParameters PrivateKey) GenerateKey(string secret)
        {
#if NETSTANDARD2_1
            var seedBytes = ArrayPool<byte>.Shared.Rent(32);

            while (secret.Length < 32) secret += secret;

            Encoding.UTF8.GetBytes(secret.AsSpan()[..32], seedBytes);

            var privateKey = new Ed25519PrivateKeyParameters(seedBytes);
#else
            Span<byte> seedBytes = stackalloc byte[32];

            while (secret.Length < 32) secret += secret;

            Encoding.UTF8.GetBytes(secret.AsSpan()[..32], seedBytes);

            var privateKey = new Ed25519PrivateKeyParameters(seedBytes);
#endif
            var publicKey = privateKey.GeneratePublicKey();

            return (publicKey, privateKey);
        }

        public static bool VerifySignature(Ed25519PublicKeyParameters publicKey, byte[] signature, byte[] timestamp, byte[] message)
        {
            var verifier = new Ed25519Signer();
            verifier.Init(false, publicKey); // false 表示用于验证
            verifier.BlockUpdate(timestamp, 0, timestamp.Length);
            verifier.BlockUpdate(message, 0, message.Length);
            return verifier.VerifySignature(signature);
        }

        public static Ed25519Signer NewSigner(Ed25519PublicKeyParameters publicKey, byte[] data)
        {
            var verifier = new Ed25519Signer();
            verifier.Init(false, publicKey); // false 表示用于验证
            verifier.BlockUpdate(data, 0, data.Length);
            return verifier;
        }

        public static Ed25519Signer NewSigner(Ed25519PrivateKeyParameters privateKey, byte[] data)
        {
            var verifier = new Ed25519Signer();
            verifier.Init(true, privateKey);
            verifier.BlockUpdate(data, 0, data.Length);
            return verifier;
        }
#if !NETSTANDARD2_1
        public static bool VerifySignature(Ed25519PublicKeyParameters publicKey, byte[] signature, ReadOnlySpan<byte> timestamp, ReadOnlySpan<byte> message)
        {
            var verifier = new Ed25519Signer();
            verifier.Init(false, publicKey); // false 表示用于验证
            verifier.BlockUpdate(timestamp);
            verifier.BlockUpdate(message);
            return verifier.VerifySignature(signature);
        }

        public static Ed25519Signer NewSigner(Ed25519PublicKeyParameters publicKey, ReadOnlySpan<byte> data)
        {
            var verifier = new Ed25519Signer();
            verifier.Init(false, publicKey); // false 表示用于验证
            verifier.BlockUpdate(data);
            return verifier;
        }

        public static Ed25519Signer NewSigner(Ed25519PrivateKeyParameters privateKey, ReadOnlySpan<byte> data)
        {
            var verifier = new Ed25519Signer();
            verifier.Init(true, privateKey);
            verifier.BlockUpdate(data);
            return verifier;
        }
#endif
    }
}
