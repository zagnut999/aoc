namespace aoc2025.day03;

public class Tests
{
    [Test]
    public async Task ReadDaysFile()
    {
        var input = await Utilities.ReadInputByDay("day03");
        input.ShouldNotBeEmpty();
    }
    
    [Test]
    public void Part1Example()
    {
        var input = new List<string> 
        { "987654321111111", 
            "811111111111119",
            "234234234234278",
            "818181911112111"};

        var biggest = FindBiggestPair(input);

        biggest.Sum().ShouldBe(357);
    }

    [Test]
    public async Task Part1Actual()
    {
        var input = await Utilities.ReadInputByDay("day03");
        var biggest = FindBiggestPair(input);
        biggest.Sum().ShouldBe(17166);
    }
    
    [Test]
    public void Part2Example()
    {
        var input = new List<string> 
        { "987654321111111", 
            "811111111111119",
            "234234234234278",
            "818181911112111"};
        
        var biggest = FindBiggestTwelve(input);

        biggest.Sum().ShouldBe(3121910778619L);
    }
    
    [Test]
    public async Task Part2Actual()
    {
        var input = await Utilities.ReadInputByDay("day03");
        var biggest = FindBiggestTwelve(input);

        biggest.Sum().ShouldBe(3121910778619L);
    }
    
    private static List<long> FindBiggestTwelve(List<string> input)
    {
        var biggest = new List<long>();

        foreach (var line in input)
        {
            var combos = Build(line.ToList());
            var max = combos.Select(long.Parse).Max();
            biggest.Add(max);
        }

        return biggest;
    }
    
    [Test]
    public void BuildTest()
    {
        var input = new List<string> 
        { 
            "987654321111111", 
            "811111111111119",
            "234234234234278",
            "818181911112111"};

        var list = Build(input.First().ToCharArray().ToList());
        
        list.ShouldNotBeEmpty();
        var max = list.Select(long.Parse).Max();
        max.ShouldBe(987654321111L);
    }

    private static List<string> Build(List<char> item, int depth = 1)
    {
        var results = new List<string>();
        if (item.Count == 1 || depth == 12) return item.Select(x=>x.ToString()).ToList();
        
        for (var x = 0; x < item.Count; x++)
        {
            var first = item[x].ToString();
            
            // shortcut?
            if (results.Any(existing => item[x] < existing.First())) continue;

            var children = Build(item.Skip(x+1).ToList(), depth + 1);
            var temp = children.Select(child => 
                child.Insert(0, first)).ToList();
            results.AddRange(temp);
        }
        return results;
    }
    
    private static List<int> FindBiggestPair(List<string> input)
    {
        var biggest = new List<int>();

        foreach (var item in input)
        {
            var numItem = item.ToCharArray().Select(x=> x - '0').ToList();
            var max = 0;
            for (var x = 0; x < numItem.Count; x++)
            {
                for (var y = x + 1; y < numItem.Count; y++)
                {
                    var num = numItem[x] * 10 + numItem[y];
                    if (num > max)
                        max = num;
                }
            }
            biggest.Add(max);
        }

        return biggest;
    }
}