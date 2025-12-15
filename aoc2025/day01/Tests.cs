namespace aoc2025.day01;

public class Tests
{
    [Test]
    public async Task ReadDaysFile()
    {
        var list = await Utilities.ReadInputByDay("day01");
        list.ShouldNotBeEmpty();
    }
    
    [Test]
    public void Part1Example()
    {
        var list = new List<string>
        {
            "L68",
            "L30",
            "R48",
            "L5",
            "R60",
            "L55",
            "L1",
            "L99",
            "R14",
            "L82"
        };
        
        var tumbler = new Tumbler(50, 100);
        list.ForEach(input => tumbler.Turn(input));
        tumbler.ZeroCount.ShouldBe(3);
    }
    
    [Test]
    public async Task Part1Actual()
    {
        var list = await Utilities.ReadInputByDay("day01");
        var tumbler = new Tumbler(50, 100);
        list.ForEach(input => tumbler.Turn(input));
        tumbler.ZeroCount.ShouldBe(1141);
    }
    
    [Test]
    public void Part2Example()
    {
        var list = new List<string>
        {
            "L68",
            "L30",
            "R48",
            "L5",
            "R60",
            "L55",
            "L1",
            "L99",
            "R14",
            "L82"
        };
        
        var tumbler = new Tumbler(50, 100);
        list.ForEach(input => tumbler.Turn(input));
        tumbler.PastZeroCount.ShouldBe(6);
    }
    
    [Test]
    public async Task Part2Actual()
    {
        var list = await Utilities.ReadInputByDay("day01");
        var tumbler = new Tumbler(50, 100);
        list.ForEach(input => tumbler.Turn(input));
        tumbler.PastZeroCount.ShouldBe(6634);
    }

    private class Tumbler(int start, int size)
    {
        private int _position = start;
        public int ZeroCount { get; private set; }
        public int PastZeroCount { get; private set; }

        public int Turn(string input)
        {
            var direction = input[0];
            var digits = int.Parse(input[1..]);
            
            Turn(digits, direction == 'R');
            
            return _position;
        }

        private void Turn(int clicks, bool rightTurn)
        {
            for (var click = 0; click < clicks; click++)
            {
                _position = rightTurn ? _position + 1 : _position - 1;
                if (_position == -1) _position = 99;
                if (_position == 100) _position = 0;
                
                if (_position == 0) PastZeroCount++;
            }
            
            if (_position == 0) ZeroCount++;
        }
        
        // [Test]
        // [TestCase(150, 50, 1)]
        // [TestCase(50, 50, 0)]
        // [TestCase(-50, 50, 1)]
        // [TestCase(-150, 50, 2)]
        // [TestCase(30, 30, 0)]
        // [TestCase(-30, 70, 1)]
        // [TestCase(-5, 95, 1)]
        // [TestCase(100, 0, 0)]
        // public void Modulus(int input, int outputExpected, int zeroCountExpected)
        // {
        //     var size = 100;
        //     if (input == size) input = 0;
        //     var zeroCount = Math.Abs(input / size);
        //     if (input < 0) zeroCount++;
        //     var offset = input < 0 ? zeroCount * size : 0;
        //     var outputActual = (input + offset) % size;
        //     
        //     zeroCount.ShouldBe(zeroCountExpected);
        //     outputActual.ShouldBe(outputExpected);
        // }
        
        // private void TurnRight(int digits)
        // {
        //     Modulus(_position + digits);
        // }
        //
        // private void TurnLeft(int digits)
        // {
        //     Modulus(_position - digits);
        // }
        //
        // private void Modulus(int position)
        // {
        //     if (position == size) position = 0;
        //     
        //     var zeroCount = Math.Abs(position / size);
        //     
        //     if (position < 0) zeroCount++;
        //     ZeroCount += zeroCount;
        //     var offset = position < 0 ? zeroCount * size : 0;
        //     _position = (position + offset) % size;
        // }
    }
}