import unittest
import re
from operator import truediv


class Day03(unittest.TestCase):

    def test_regex(self):
        test = "xmul(2,4)%&mul[3,7]!@^do_not_mul(5,5)+mul(32,64]then(mul(11,8)mul(8,5))"
        x = re.findall(r"mul\((\d{1,3}),(\d{1,3})\)", test)
        self.assertEqual(4, len(x))

        total = 0
        for i in range(len(x)):
            (a,b) = x[i]
            total += int(a) * int(b)

        self.assertEqual(161, total)


    def test_part1_sample(self):
        with open("sample.txt", "r") as file:
            lines = file.readlines()

        total = 0
        for line in lines:
            x = re.findall(r"mul\((\d{1,3}),(\d{1,3})\)", line)
            for i in range(len(x)):
                (a, b) = x[i]
                total += int(a) * int(b)

        self.assertEqual(161, total)

    def test_part1_actual(self):
        with open("day03.txt", "r") as file:
            lines = file.readlines()

        total = 0
        for line in lines:
            x = re.findall(r"mul\((\d{1,3}),(\d{1,3})\)", line)
            for i in range(len(x)):
                (a, b) = x[i]
                total += int(a) * int(b)

        self.assertEqual(173517243, total)

    def test_part2_regex(self):
        line = "xmul(2,4)&mul[3,7]!^don't()_mul(5,5)+mul(32,64](mul(11,8)undo()?mul(8,5))"
        x = re.findall(r"mul\(\d{1,3},\d{1,3}\)|do\(\)|don't\(\)", line)
        self.assertEqual(6, len(x))

        total = 0
        dont = False
        for i in range(len(x)):
            if x[i] == "don't()":
                dont = True
            elif x[i] == "do()":
                dont = False
            else:
                if not dont:
                    (a,b) = re.findall("mul\((\d{1,3}),(\d{1,3})\)", x[i])[0]
                    total += int(a) * int(b)

        self.assertEqual(48, total)

    # def test_part2_sample(self):
    #     with open("sample.txt", "r") as file:
    #         lines = []
    #         for lines_temp in file.readlines():
    #             lines.append(list(map(int, lines_temp.strip().split())))
    #
    #     safe_count = self.part2(lines)
    #     self.assertEqual(4, safe_count)

    def test_part2_actual(self):
        with open("day03.txt", "r") as file:
            lines = file.readlines()

        total = 0
        dont = False
        for line in lines:
            x = re.findall(r"mul\(\d{1,3},\d{1,3}\)|do\(\)|don't\(\)", line)
            for i in range(len(x)):
                if x[i] == "don't()":
                    dont = True
                elif x[i] == "do()":
                    dont = False
                else:
                    if not dont:
                        (a, b) = re.findall("mul\((\d{1,3}),(\d{1,3})\)", x[i])[0]
                        total += int(a) * int(b)

        self.assertEqual(100450138, total)


if __name__ == '__main__':
    unittest.main()
