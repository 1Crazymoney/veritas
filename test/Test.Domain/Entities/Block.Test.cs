﻿using System.Diagnostics;
using Domain.Common;
using Domain.Entities;

namespace Test.Domain.Entities;

internal class BlockTest
{
    [Test]
    [NonParallelizable]
    public void TestBlockMine()
    {
        // 1 difficulty means 2 leading zeroes
        var block = Block.GenesisBlock().NextBlock();

        for (int i = 0; i < 256; i++)
            block.AddVote(Vote.NewVote(Voter.NewVoter(), 0, 0L.ToUtcDateTime()));

        var stopwatch = Stopwatch.StartNew();
        block.Mine();
        stopwatch.Stop();

        Console.WriteLine(block.Nonce.ToString("N0"));
        Console.WriteLine(block.Hash);
        Console.WriteLine(stopwatch.Elapsed.ToString("c"));
        Assert.That(block.IsHashValid, Is.True);
    }

    [Test]
    [Parallelizable]
    public void TestBlockHash()
    {
        var block = Block.GenesisBlock();
        Console.WriteLine(block.Hash);
    }

    [Test]
    [Parallelizable]
    public void TestCalculateMerkleRootForVotes()
    {
        var block = Block.GenesisBlock();

        block.AddVote(Vote.NewVote(Voter.NewVoter(), 0, 0L.ToUtcDateTime()));
        block.AddVote(Vote.NewVote(Voter.NewVoter(), 0, 0L.ToUtcDateTime()));
        block.AddVote(Vote.NewVote(Voter.NewVoter(), 0, 0L.ToUtcDateTime()));
        block.AddVote(Vote.NewVote(Voter.NewVoter(), 0, 0L.ToUtcDateTime()));

        Console.WriteLine(block.MerkleRoot);
    }

    [Test]
    [Parallelizable]
    public void TestHashBlockWithVotes()
    {
        var block = Block.GenesisBlock();

        block.AddVote(Vote.NewVote(Voter.NewVoter(), 0, 0L.ToUtcDateTime()));
        block.AddVote(Vote.NewVote(Voter.NewVoter(), 0, 0L.ToUtcDateTime()));
        block.AddVote(Vote.NewVote(Voter.NewVoter(), 0, 0L.ToUtcDateTime()));
        block.AddVote(Vote.NewVote(Voter.NewVoter(), 0, 0L.ToUtcDateTime()));

        block.Mine();

        Console.WriteLine(block.MerkleRoot);

        Console.WriteLine(block.Nonce.ToString("N0"));
        Console.WriteLine(block.Hash);

        Assert.That(block.IsHashValid, Is.True);
    }

    [Test]
    [Parallelizable]
    public void TestHashBlockWithMaxVotes()
    {
        var block = Block.GenesisBlock();

        block.AddVote(Vote.NewVote(Voter.NewVoter(), 0, 0L.ToUtcDateTime()));
        block.AddVote(Vote.NewVote(Voter.NewVoter(), 0, 0L.ToUtcDateTime()));
        block.AddVote(Vote.NewVote(Voter.NewVoter(), 0, 0L.ToUtcDateTime()));
        block.AddVote(Vote.NewVote(Voter.NewVoter(), 0, 0L.ToUtcDateTime()));

        Console.WriteLine(block.MerkleRoot);

        // mine operation
        block.Mine();

        Console.WriteLine(block.Nonce.ToString("N0"));
        Console.WriteLine(block.Hash);

        Assert.That(block.IsHashValid, Is.True);
    }
}