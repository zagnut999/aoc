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
        
        var biggest = FindBiggestTwelveOptimizedNick(input);

        biggest.Sum().ShouldBe(3121910778619L);
    }
    
    [Test]
    [Category("theone")]
    public async Task Part2Actual()
    {
        var input = await Utilities.ReadInputByDay("day03");
        var biggest = FindBiggestTwelveOptimizedNick(input);

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
    
    private static List<long> FindBiggestTwelveOptimized(List<string> input)
    {
        var biggest = new List<long>();

        foreach (var line in input)
        {
            var max = FindMaxSubsequence(line, 12);
            biggest.Add(max);
        }

        return biggest;
    }
    
    private static List<long> FindBiggestTwelveOptimizedInt(List<string> input)
    {
        var biggest = new List<long>();

        foreach (var line in input)
        {
            var max = FindMaxSubsequenceInt(line, 12);
            biggest.Add(max);
        }

        return biggest;
    }
    
    private static List<long> FindBiggestTwelveOptimizedNick(List<string> input)
    {
        var biggest = new List<long>();

        foreach (var line in input)
        {
            var max = FindMaxSubsequenceNick(line, 12);
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
    
    private static long FindMaxSubsequence(string input, int maxLength = 12)
    {
        var n = input.Length;
        var maxValue = 0L;
        
        // Iterate through all possible subsequences using bitmasks
        // Start from 1 to exclude empty subsequence
        var maxMask = 1L << n;
        for (long mask = 1; mask < maxMask; mask++)
        {
            // Count set bits - skip if length exceeds maxLength
            var bitCount = 0;
            var tempMask = mask;
            while (tempMask != 0)
            {
                bitCount++;
                tempMask &= tempMask - 1; // Clear the least significant set bit
            }
            
            if (bitCount > maxLength) continue;
            
            // Build subsequence from bit positions
            var sb = new System.Text.StringBuilder(bitCount);
            for (var i = 0; i < n; i++)
            {
                if ((mask & (1L << i)) != 0)
                {
                    sb.Append(input[i]);
                }
            }
            
            // Parse and track maximum
            if (sb.Length > 0 && long.TryParse(sb.ToString(), out var value))
            {
                if (value > maxValue)
                    maxValue = value;
            }
        }
        
        return maxValue;
    }
    
    
    private static long FindMaxSubsequenceInt(string input, int maxLength = 12)
    {
        // Convert string to integer array upfront
        var digits = input.Select(c => c - '0').ToArray();
        var n = digits.Length;
        var maxValue = 0L;
        
        // Iterate through all possible subsequences using bitmasks
        // Start from 1 to exclude empty subsequence
        var maxMask = 1L << n;
        for (long mask = 1; mask < maxMask; mask++)
        {
            // Count set bits - skip if length exceeds maxLength
            var bitCount = 0;
            var tempMask = mask;
            while (tempMask != 0)
            {
                bitCount++;
                tempMask &= tempMask - 1; // Clear the least significant set bit
            }
            
            if (bitCount > maxLength) continue;
            
            // Build number directly using arithmetic instead of string manipulation
            long value = 0;
            for (var i = 0; i < n; i++)
            {
                if ((mask & (1L << i)) != 0)
                {
                    value = value * 10 + digits[i];
                }
            }
            
            // Track maximum
            if (value > maxValue)
                maxValue = value;
        }
        
        return maxValue;
    }
    
    private static long FindMaxSubsequenceNick(string input, int maxLength = 12)
    {
        var batteries = input[..].Select(b => (int)char.GetNumericValue(b)).ToArray();
        
        
        var bank = new Bank(input, maxLength);
        
        return bank.MaxJoltage;
    }
    
    private static List<string> BuildBitManipulation(List<char> item, int maxLength = 12)
    {
        var results = new List<string>();
        var input = new string(item.ToArray());
        var n = input.Length;
        
        // Iterate through all possible subsequences using bitmasks
        // Start from 1 to exclude empty subsequence
        var maxMask = 1L << n;
        for (long mask = 1; mask < maxMask; mask++)
        {
            // Count set bits - skip if length exceeds maxLength
            var bitCount = 0;
            var tempMask = mask;
            while (tempMask != 0)
            {
                bitCount++;
                tempMask &= tempMask - 1; // Clear the least significant set bit
            }
            
            if (bitCount > maxLength) continue;
            
            // Build subsequence from bit positions
            var sb = new System.Text.StringBuilder(bitCount);
            for (var i = 0; i < n; i++)
            {
                if ((mask & (1L << i)) != 0)
                {
                    sb.Append(input[i]);
                }
            }
            
            if (sb.Length > 0)
            {
                results.Add(sb.ToString());
            }
        }
        
        return results;
    }

    private static List<int> FindBiggestPair(List<string> input)
    {
        var biggest = new List<int>();

        foreach (var item in input)
        {
            var numItem = item.ToCharArray().Select(x => x - '0').ToList();
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
    
    // https://github.com/ngallegos/aoc/blob/main/2025/csharp/AOC2025.Tests/Day03Tests.cs
    class Bank
    {
        public long MaxJoltage { get; } = 0;

        public Bank(string bank, int batteriesToProcess = 2)
        {
            var batteries = bank[..].Select(b => (int)char.GetNumericValue(b)).ToArray();
            
            MaxJoltage = FindMaxJoltage(batteries, batteriesToProcess);
        }
        
        private long FindMaxJoltage(int[] batteries, int batteriesToProcess)
        {
            var max = batteries.Max();
            if (batteriesToProcess == 1)
            {
                return max;
            }
            
            var indexOfMaximum = Array.IndexOf(batteries, max);
            var remainingBatteries = batteriesToProcess - 1;
            var lastValidIndex = batteries.Length - remainingBatteries - 1;
            var multiplier = (long)Math.Pow(10, remainingBatteries);
            if (indexOfMaximum > lastValidIndex)
            {
                var validBatteries = batteries[..^remainingBatteries];
                max = validBatteries.Max();
                indexOfMaximum = Array.IndexOf(validBatteries, max);
                return max * multiplier + FindMaxJoltage(batteries[(indexOfMaximum+1)..], remainingBatteries);
            }
            
            return max * multiplier + FindMaxJoltage(batteries[(indexOfMaximum+1)..], remainingBatteries);
        }
    }
}