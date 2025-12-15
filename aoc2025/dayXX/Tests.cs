namespace aoc2025.dayXX;

public class Tests
{
    [Test]
    public async Task ReadDaysFile()
    {
        var input = await Utilities.ReadInputByDay("dayXX");
        input.ShouldNotBeEmpty();
    }
    
    [Test]
    public void Part1Example()
    {
        
    }
    
    [Test]
    public async Task Part1Actual()
    {
        var input = await Utilities.ReadInputByDay("dayXX");
    }
    
    [Test]
    public void Part2Example()
    {
        
    }
    
    [Test]
    public async Task Part2Actual()
    {
        var input = await Utilities.ReadInputByDay("dayXX");
    }
}