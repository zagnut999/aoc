namespace aoc2025.day04;

public class Tests
{
    [Test]
    public async Task ReadDaysFile()
    {
        var input = await Utilities.ReadInputByDay("day04");
        input.ShouldNotBeEmpty();
    }
    
    [Test]
    public void Part1Example()
    {
        var input = new List<string>
        {
            "..@@.@@@@.",
            "@@@.@.@.@@",
            "@@@@@.@.@@",
            "@.@@@@..@.",
            "@@.@@@@.@@",
            ".@@@@@@@.@",
            ".@.@.@.@@@",
            "@.@@@.@@@@",
            ".@@@@@@@@.",
            "@.@.@@@.@."
        };

        var rolls = Process(input);
        rolls.countOfRollsToRemove.ShouldBe(13);
    }

    [Test]
    public async Task Part1Actual()
    {
        var input = await Utilities.ReadInputByDay("day04");
        var rolls = Process(input);
        rolls.countOfRollsToRemove.ShouldBe(1489);
    }
    
    [Test]
    public void Part2Example()
    {
        var input = new List<string>
        {
            "..@@.@@@@.",
            "@@@.@.@.@@",
            "@@@@@.@.@@",
            "@.@@@@..@.",
            "@@.@@@@.@@",
            ".@@@@@@@.@",
            ".@.@.@.@@@",
            "@.@@@.@@@@",
            ".@@@@@@@@.",
            "@.@.@@@.@."
        };

        var rolls = ProcessPart2(input);
        rolls.countOfRollsToRemove.ShouldBe(43);
    }
    
    [Test]
    public async Task Part2Actual()
    {
        var input = await Utilities.ReadInputByDay("day04");
        var rolls = ProcessPart2(input);
        rolls.countOfRollsToRemove.ShouldBe(8890);
    }
    
    private readonly List<Point> _neighbors = [new Point(-1, -1), new Point(-1, 0), new Point(-1, 1), 
        new Point(0, -1),                    new Point(0, 1), 
        new Point(1, -1),   new Point(1, 0),  new Point(1, 1)];

    private bool Valid(Point point, int rowCount, int columnCount)
    {
        return point.X >= 0 && point.X < columnCount && point.Y >= 0 && point.Y < rowCount;
    }

    private (int countOfRollsToRemove, List<string> remaining) Process(List<string> input)
    {
        var maxRows = input.Count;
        var maxColumns = input[0].Length;
        var rollsCount = new Dictionary<Point, int>();
        for (var y = 0; y < maxRows; y++)
        {
            for (var x = 0; x < maxColumns; x++)
            {
                var currentEvaluation = new Point(x, y);
                if (input[currentEvaluation.Y][currentEvaluation.X] != '@') continue;
                
                var count = 0;
                foreach (var neighbor in _neighbors)
                {
                    var eval = new Point(currentEvaluation.X + neighbor.X, currentEvaluation.Y + neighbor.Y);
                    
                    if (!Valid(eval, maxRows, maxColumns)) continue;

                    if (input[eval.Y][eval.X] == '@')
                    {
                        count++;
                        
                    }
                    
                }
                rollsCount[currentEvaluation] = count;
            }
        }

        var subset = rollsCount.Where(x => x.Value is >= 0 and < 4).ToList();
        var countOfRollsToRemove = subset.Count();
        foreach (var (point, _) in subset)
            input[point.Y] = input[point.Y].Remove(point.X, 1).Insert(point.X, "-");
        return (countOfRollsToRemove, input);
    }

    private (int countOfRollsToRemove, List<string> remaining) ProcessPart2(List<string> input)
    {
        var totalRolls = 0;
        var (count, remaining) = Process(input);
        totalRolls += count;
        while (count > 0)
        {
            (count, remaining) = Process(remaining);
            totalRolls += count;
        }

        return (totalRolls, remaining);
    }
}