import unittest
import re
from operator import truediv


class Day04(unittest.TestCase):
    def get_all_combinations(self, lines: list[str]):
        all_lines = self.horizontal_slice(lines)

        all_lines += self.vertical_slice(lines)

        all_lines += self.diag_up_slice(lines)

        all_lines += self.diag_down_slice(lines)

        return all_lines

    def horizontal_slice(self, lines):
        all_lines = lines.copy()
        for line in lines:
            all_lines.append(line[::-1])
        return all_lines

    def vertical_slice(self, lines):
        all_lines : list[str] = []
        for i in range(len(lines[0])):
            line = ""
            for j in range(len(lines)):
                line += lines[j][i]
            all_lines.append(line)
            all_lines.append(line[::-1])
        return all_lines

    def diag_up_slice(self, lines):
        all_lines : list[str] = []
        #down
        for j in range(len(lines)):
            line = ""
            i = 0
            for jj in range(j, -1, -1):
                line += lines[jj][i]
                i += 1

            all_lines.append(line)
            all_lines.append(line[::-1])

        #across
        for i in range(1, len(lines[0])): #zero was done above
            line = ""
            j = len(lines) - 1
            for ii in range(i, len(lines[0])):
                line += lines[j][ii]
                j -= 1

            all_lines.append(line)
            all_lines.append(line[::-1])

        return all_lines

    def diag_down_slice(self, lines):
        all_lines: list[str] = []
        # up
        for j in range(len(lines) -1, -1, -1):
            line = ""
            i = 0
            for jj in range(j, len(lines)):
                line += lines[jj][i]
                i += 1
                if i >= len(lines[0]):
                    break

            all_lines.append(line)
            all_lines.append(line[::-1])

        # across
        for i in range(1, len(lines[0])):  # zero was done above
            line = ""
            j = 0
            for ii in range(i, len(lines[0])):
                line += lines[j][ii]
                j += 1

            all_lines.append(line)
            all_lines.append(line[::-1])

        return all_lines

    def test_horizontal(self):
        with open("sample.txt", "r") as file:
            lines = file.readlines()
        all_lines = self.horizontal_slice(lines)

        self.assertEqual(2*len(lines), len(all_lines))

    def test_vertical(self):
        with open("sample.txt", "r") as file:
            lines = file.readlines()
        all_lines = self.vertical_slice(lines)

        self.assertEqual(2*len(lines[0]), len(all_lines))

    def test_diag_up(self):
        with open("sample.txt", "r") as file:
            lines = file.readlines()
        all_lines = self.diag_up_slice(lines)

        self.assertEqual(2*20, len(all_lines))

    def test_diag_down(self):
        with open("sample.txt", "r") as file:
            lines = file.readlines()
        all_lines = self.diag_down_slice(lines)

        self.assertEqual(2*20, len(all_lines))

    def test_part1_sample(self):
        with open("sample.txt", "r") as file:
            lines = file.readlines()
        all_lines = self.get_all_combinations(lines)
        count = 0
        for line in all_lines:
            x = re.findall(r"XMAS", line)
            count += len(x)
        self.assertEqual(18, count)

    def test_part1_actual(self):
        with open("day04.txt", "r") as file:
            lines = file.readlines()

        all_lines = self.get_all_combinations(lines)
        count = 0
        for line in all_lines:
            x = re.findall(r"XMAS", line)
            count += len(x)
        self.assertEqual(2613, count)

    def test_part2_sample(self):
        with open("sample.txt", "r") as file:
            lines = file.readlines()

        self.assertEqual(True, True)

    def test_part2_actual(self):
        with open("day04.txt", "r") as file:
            lines = file.readlines()

        self.assertEqual(True, True)


if __name__ == '__main__':
    unittest.main()
