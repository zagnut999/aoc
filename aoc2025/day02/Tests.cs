using System.Text.RegularExpressions;

namespace aoc2025.day02;

public class Tests
{
    [Test]
    public async Task ReadDaysFile()
    {
        var input = await Utilities.ReadInputByDayRaw("day02");
        input.ShouldNotBeEmpty();
    }
    
    [Test]
    public void Part1Example()
    {
        var input =
            "11-22,95-115,998-1012,1188511880-1188511890,222220-222224,1698522-1698528,446443-446449,38593856-38593862,565653-565659,824824821-824824827,2121212118-2121212124";
        
        var invalid = ExtractInvalidNumbers(input);

        var sum = invalid.Sum();
        sum.ShouldBe(1227775554);
    }

    [Test]
    [TestCase(11, true)]
    [TestCase(12, false)]
    [TestCase(1010, true)]
    public void InvalidNumber(int number, bool expected)
    {
        var asString = number.ToString();
        var invalid = asString[..(asString.Length/2)] == asString[(asString.Length/2)..];
        invalid.ShouldBe(expected);
    }
    
    [Test]
    [TestCase(11, true)]
    [TestCase(12, false)]
    [TestCase(1010, true)]
    [TestCase(111, true)]
    [TestCase(112112, true)]
    [TestCase(112, false)]
    public void InvalidNumberMany(int number, bool expected)
    {
        var asString = number.ToString();
        var asStringLength = asString.Length;
        var actual = false;
        for (var factor = 2; factor <= asStringLength; factor++)
        {
            if (actual) break;
            if (asStringLength % factor != 0) continue;
            
            var partLength = asStringLength / factor;
            var parts = new List<string>();
            for(var i=0; i<factor; i++)
                parts.Add(asString.Substring(i*partLength, partLength));
            actual = parts.Distinct().Count() == 1;
        }
        
        actual.ShouldBe(expected);
    }
    
    // [Test]
    // [TestCase(11, true)]
    // [TestCase(12, false)]
    // [TestCase(1010, true)]
    // public void InvalidNumberRegex(int number, bool expected)
    // {
    //     var regex = new Regex(@"(\d+){2,}");
    //     var asString = number.ToString();
    //     var invalid = regex.IsMatch(asString);
    //     invalid.ShouldBe(expected);
    // }
    
    [Test]
    public async Task Part1Actual()
    {
        var input = await Utilities.ReadInputByDayRaw("day02");
        
        var invalid = ExtractInvalidNumbers(input);

        var sum = invalid.Sum();
        sum.ShouldBe(56660955519L);
    }

    private static List<long> ExtractInvalidNumbers(string input)
    {
        var ranges = input.Split(',');
        var rangesNumber = ranges.Select(x=> x.Split('-').Select(long.Parse).ToArray()).ToList();
        var invalid = new List<long>();
        foreach (var range in rangesNumber)
        {
            for (var number = range[0]; number <= range[1]; number++)
            {
                var asString = number.ToString();
                if( asString[..(asString.Length/2)] == asString[(asString.Length/2)..])
                    invalid.Add(number);
            }
        }

        return invalid;
    }
    
    private static List<long> ExtractInvalidNumbersPart2(string input)
    {
        var ranges = input.Split(',');
        var rangesNumber = ranges.Select(x=> x.Split('-').Select(long.Parse).ToArray()).ToList();
        var invalid = new List<long>();
        foreach (var range in rangesNumber)
        {
            for (var number = range[0]; number <= range[1]; number++)
            {
                var asString = number.ToString();
                var asStringLength = asString.Length;
                
                for (var factor = 2; factor <= asStringLength; factor++)
                {
                    if (asStringLength % factor != 0) continue;
                    
                    var partLength = asStringLength / factor;
                    var parts = new List<string>();
                    for(var i=0; i<factor; i++)
                        parts.Add(asString.Substring(i*partLength, partLength));
                    if (parts.Distinct().Count() == 1)
                    {
                        invalid.Add(number);
                        break;
                    }
                }
            }
        }

        return invalid;
    }

    [Test]
    public void Part2Example()
    {
        var input =
            "11-22,95-115,998-1012,1188511880-1188511890,222220-222224,1698522-1698528,446443-446449,38593856-38593862,565653-565659,824824821-824824827,2121212118-2121212124";
        
        var invalid = ExtractInvalidNumbersPart2(input);

        var sum = invalid.Sum();
        sum.ShouldBe(4174379265);
    }
    
    [Test]
    public async Task Part2Actual()
    {
        var input = await Utilities.ReadInputByDayRaw("day02");
        
        var invalid = ExtractInvalidNumbersPart2(input);

        var sum = invalid.Sum();
        sum.ShouldBe(79183223243L);
    }
}