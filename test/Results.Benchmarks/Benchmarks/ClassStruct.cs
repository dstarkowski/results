using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Results;
using static Results.Result;

namespace Results.Benchmarks;

public sealed class ClassStruct
{


	[Benchmark]
	public Byte[] StructResult()
	{
		var salt = new Byte[16];
		RandomNumberGenerator.Fill(salt);

		var secretHash = KeyDerivation
			.Pbkdf2(_secret, salt, KeyDerivationPrf.HMACSHA256, iterationCount: 10000, numBytesRequested: 32);

		var hash = new Byte[16 + 32];
		salt.CopyTo(hash, 0);
		secretHash.CopyTo(hash, 16);

		return hash;
	}

	[Benchmark]
	public Byte[] ClassResult()
	{
		var salt = new Byte[16];
		RandomNumberGenerator.Fill(salt);

		var secretHash = KeyDerivation
			.Pbkdf2(_secret, salt, KeyDerivationPrf.HMACSHA256, iterationCount: 10000, numBytesRequested: 32);

		var hash = new Byte[16 + 32];
		salt.CopyTo(hash, 0);
		secretHash.CopyTo(hash, 16);

		return hash;
	}
}
