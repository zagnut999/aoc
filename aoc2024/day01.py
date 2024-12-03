import unittest


class Day01(unittest.TestCase):
    def test_part1_line_split(self):
        sample = "3   4"
        (a, b) = sample.split()
        value1 = int(a)
        value2 = int(b)
        self.assertEqual(3, value1)
        self.assertEqual(4, value2)

    def test_part1_doc_split(self):
        sample = """3   4
4   3
2   5
1   3
3   9
3   3"""
        array1 = []
        array2 = []

        for line in sample.splitlines():
            (a, b) = line.split()
            array1.append(int(a))
            array2.append(int(b))

        self.assertEqual(6, len(array1))
        self.assertEqual(6, len(array2))

    @staticmethod
    def part1(lines):
        array1 = []
        array2 = []

        for line in lines:
            (a, b) = line.split()
            array1.append(int(a))
            array2.append(int(b))

        array1.sort()
        array2.sort()

        distance = 0
        for i in range(0, len(array1)):
            distance += abs(array1[i] - array2[i])

        return distance

    def test_part1_math(self):
        sample = """3   4
        4   3
        2   5
        1   3
        3   9
        3   3"""

        distance = self.part1(sample.splitlines())

        self.assertEqual(11, distance)

    def test_part1_actual(self):
        with open("day01.txt", "r") as file:
            lines = file.readlines()

        distance = self.part1(lines)

        self.assertEqual(2815556, distance)

    @staticmethod
    def part2(lines):
        array1 = []
        array2 = []

        for line in lines:
            (a, b) = line.split()
            array1.append(int(a))
            array2.append(int(b))

        array1.sort()
        array2.sort()

        distance = 0
        for i in range(0, len(array1)):
            distance += array1[i] * array2.count(array1[i])

        return distance

    def test_part2_math(self):
        sample = """3   4
        4   3
        2   5
        1   3
        3   9
        3   3"""

        distance = self.part2(sample.splitlines())

        self.assertEqual(31, distance)

    def test_part2_actual(self):
        with open("day01.txt", "r") as file:
            lines = file.readlines()

        distance = self.part2(lines)

        self.assertEqual(23927637, distance)

if __name__ == '__main__':
    unittest.main()
