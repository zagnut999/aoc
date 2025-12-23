namespace aoc2025.day05;

public class Tests
{
    [Test]
    public async Task ReadDaysFile()
    {
        var input = await Utilities.ReadInputByDay("day05");
        input.ShouldNotBeEmpty();
    }
    
    [Test]
    public void Part1Example()
    {
        var input = new List<string>
        {
            "3-5",
            "10-14",
            "16-20",
            "12-18",
            "",
            "1",
            "5",
            "8",
            "11",
            "17",
            "32"
        };
        
        var freshCount = Process(input);
        freshCount.ShouldBe(3);
    }

    [Test]
    public async Task Part1Actual()
    {
        var input = await Utilities.ReadInputByDay("day05");
        var freshCount = Process(input);
        freshCount.ShouldBe(598);
    }
    
    [Test]
    public void Part2Example()
    {
        var input = new List<string>
        {
            "3-5",
            "10-14",
            "16-20",
            "12-18",
            "",
            "1",
            "5",
            "8",
            "11",
            "17",
            "32"
        };
        
        var freshCount = ProcessPart2(input);
        freshCount.ShouldBe(14);
    }
    
    [Test]
    public async Task Part2Actual()
    {
        var input = await Utilities.ReadInputByDay("day05");
        var freshCount = ProcessPart2(input);
        freshCount.ShouldBe(360341832208407L);
    }
    
    private int Process(List<string> input)
    {
        var freshCount = 0;
        // search for freshness
        var freshIds = input.TakeWhile(row => row != "").Select(row => row.Split('-').Select(long.Parse).ToArray()).Select(range => (range[0], range[1])).ToList();

        // find fresh products
        foreach (var row in input)
        {
            if (row.Contains('-') || row=="") continue;
            var item = long.Parse(row);
            foreach(var range in freshIds)
                if (range.Item1 <= item && item <= range.Item2)
                {
                    freshCount++;
                    break;
                }
            
        }
        
        return freshCount;
    }
    
    [Test]
    [TestCase("3-5", "10-14", 8)] // outside
    [TestCase("10-14", "3-5", 8)] // outside 2
    [TestCase("3-6", "4-5", 4)] // new is inside
    [TestCase("4-5", "3-6", 4)] // current is inside
    [TestCase("3-5", "5-8", 6)] // new overlaps current in end
    [TestCase("3-5", "1-3", 5)] // new overlaps current in start
    public void Part2ExampleEdge(string first, string second, long expected)
    {
        var input = new List<string>
        {
            first,
            second
        };
        var result = ProcessPart2(input);
        result.ShouldBe(expected);
    }
    
    [Test]
    [TestCase("3-5", "7-10", "4-8", 8)] // new overlaps two
    public void Part2ExampleEdge3(string first, string second, string third, long expected)
    {
        var input = new List<string>
        {
            first,
            second,
            third
        };
        var result = ProcessPart2(input);
        result.ShouldBe(expected);
    }
    
    private long ProcessPart2(List<string> input)
    {
        var freshIds = input.TakeWhile(row => row != "")
            .Select(row => row.Split('-').Select(long.Parse).ToArray())
            .Select(range => (range[0], range[1])).ToList();

        var changed = true;

        while (changed)
        {
            changed = false;
            for (var i = freshIds.Count - 1; i  >= 0; i--)
            {
                var (currentStart, currentEnd) = freshIds[i];

                for (var j = 0; j < i; j++)
                {
                    var (newStart, newEnd) = freshIds[j];
                    
                    // Outside
                    if (newStart > currentEnd || newEnd < currentStart)
                    {
                        continue;
                    }

                    // new is inside
                    if (currentStart <= newStart && newEnd <= currentEnd)
                    {
                        freshIds[j] = (currentStart, currentEnd);
                        freshIds.RemoveAt(i);
                        changed = true;
                        break;
                    }

                    // current is inside
                    if (newStart <= currentStart && currentEnd <= newEnd)
                    {
                        freshIds.RemoveAt(i);
                        changed = true;
                        break;
                    }

                    // new overlaps current in start
                    if (newStart < currentStart && newEnd <= currentEnd)
                    {
                        freshIds[j] = (newStart, currentEnd);
                        freshIds.RemoveAt(i);
                        changed = true;
                        break;
                    }

                    // new overlaps current in end
                    if (currentStart <= newStart && currentEnd < newEnd)
                    {
                        freshIds[j] = (currentStart, newEnd);
                        freshIds.RemoveAt(i);
                        changed = true;
                        break;
                    }

                    throw new Exception("Should not happen");
                }
            }
        }

        var result = freshIds.Sum(range => range.Item2 - range.Item1 + 1);
        return result;
    }
}