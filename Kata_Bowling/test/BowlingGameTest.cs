using FluentAssertions;
using Xunit;

namespace Kata_Bowling.Test;
    
public class BowlingGameTest
{
    protected BowlingGame Game { get; set; } = new();

    protected void RollMany(int n, int pins)
    {
        for (var i = 0; i < n; i++)
            Game.Roll(pins);
    }

    [Fact]
    public void TestGutterGame()
    {
        RollMany(20, 0);
        Game.Score().Should().Be(0);
    }

    [Fact]
    public void TestAllOnes()
    {
        RollMany(20, 1);
        Game.Score().Should().Be(20);
    }

    [Fact]
    public void TestOneSpare()
    {
        Game.Roll(5);
        Game.Roll(5);
        Game.Roll(3);
        RollMany(17, 0);
        Game.Score().Should().Be(16);
    }

    [Fact]
    public void TestOneStrike()
    {
        Game.Roll(10);
        Game.Roll(3);
        Game.Roll(4);
        RollMany(16, 0);
        Game.Score().Should().Be(24);
    }

    [Fact]
    public void TestPerfectGame()
    {
        RollMany(12, 10);
        Game.Score().Should().Be(300);
    }

    [Fact]
    public void TestNoSpareOrStrikeInLastFrame()
    {
        RollMany(18, 0);
        Game.Roll(3);
        Game.Roll(4);
        Game.Score().Should().Be(7);
    }

    [Fact]
    public void TestSpareInLastFrame()
    {
        RollMany(18, 0);
        Game.Roll(7);
        Game.Roll(3);
        Game.Roll(7);
        Game.Score().Should().Be(17);
    }

    [Fact]
    public void TestStrikeInLastFrame()
    {
        RollMany(18, 0);
        Game.Roll(10);
        Game.Roll(7);
        Game.Roll(1);
        Game.Score().Should().Be(18);
    }
}
