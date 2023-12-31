﻿namespace Kata_Bowling;

public class BowlingGame
{
    private int _currentRoll;
    private readonly int[] _rolls = new int[21];

    public void Roll(int i)
    {
        _rolls[_currentRoll] = i;

        var isStrike = i == 10;
        var isStrikeOnLastFrameRoll1 = _currentRoll == 18 && isStrike;
        var isStrikeOnLastFrameRoll2 = _currentRoll == 19 && isStrike;
        var isLastPossibleRoll = _currentRoll == 20;

        _currentRoll += isLastPossibleRoll ? 0 : isStrike && !isStrikeOnLastFrameRoll1 && !isStrikeOnLastFrameRoll2 ? 2 : 1;
    }

    public int Score()
    {
        var output = 0;

        for (var currentRoll = 0; currentRoll < _rolls.Length; currentRoll += 2)
        {
            output += _rolls[currentRoll];
            var isLastPossibleRoll = currentRoll == 20;

            if (isLastPossibleRoll)
                continue;

            output += _rolls[currentRoll + 1];
            output = ApplyBonus(currentRoll, output);
        }

        return output;
    }

    private int ApplyBonus(int currentRoll, int output)
    {
        output += ApplySpareBonus(currentRoll);
        output += ApplyStrikeBonus(currentRoll);

        return output;
    }

    private int ApplySpareBonus(int currentRoll)
    {
        var bonus = 0;

        var isStrike = _rolls[currentRoll] == 10;
        var isSpare = !isStrike && _rolls[currentRoll] + _rolls[currentRoll + 1] == 10;
        var isLastRoll = currentRoll == 18;

        if (isSpare && !isLastRoll)
            bonus = _rolls[currentRoll + 2];

        return bonus;
    }

    private int ApplyStrikeBonus(int currentRoll)
    {
        var isStrike = _rolls[currentRoll] == 10;
        var isLastRoll = currentRoll == 18;

        if (!isStrike || isLastRoll)
            return 0;

        var isTwoStrikesInRow = isStrike && _rolls[currentRoll + 2] == 10;
        var isPenultimateFrame = currentRoll == 18;

        var bonusFirstBall = _rolls[currentRoll + 2];
        var bonusSecondBall =
            isPenultimateFrame ? 0 : (!isTwoStrikesInRow ? _rolls[currentRoll + 3] : _rolls[currentRoll + 4]);

        return bonusFirstBall + bonusSecondBall;
    }
}
