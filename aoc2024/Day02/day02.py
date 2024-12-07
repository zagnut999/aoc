import unittest


class Day02(unittest.TestCase):

    @staticmethod
    def is_safe(row):
        direction = Day02.find_direction(row[1] - row[0])
        result = True
        for index in range(len(row)):
            if (index == len(row) - 1) and result:
                print('end of line')
                break

            change = row[index + 1] - row[index]
            current_direction = Day02.find_direction(change)

            if direction != current_direction:
                print('direction changed')
                result = False
                break
            if not 1 <= abs(change) <= 3:
                print('out of range')
                result = False
                break
        return result

    @staticmethod
    def find_direction(change):
        if change < 0:
            current_direction = -1
        elif change > 0:
            current_direction = 1
        else:
            current_direction = 0
        return current_direction

    @staticmethod
    def part1(lines):
        safe_count = 0
        for row in lines:
            safe_count += 1 if Day02.is_safe(row) else 0
        return safe_count

    def test_part1_safe(self):
        sample = "7 6 4 2 1"
        raw_row = sample.split()
        row = [int(numeric_string) for numeric_string in raw_row]
        self.assertEqual(5, len(row))
        is_safe = self.is_safe(row)
        self.assertTrue(is_safe)

    def test_part1_unsafe_outofrange_increase(self):
        sample = "1 2 7 8 9"
        raw_row = sample.split()
        row = [int(numeric_string) for numeric_string in raw_row]
        self.assertEqual(5, len(row))
        is_safe = self.is_safe(row)
        self.assertFalse(is_safe)

    def test_part1_unsafe_outofrange_decrease(self):
        sample = "9 7 6 2 1"
        raw_row = sample.split()
        row = [int(numeric_string) for numeric_string in raw_row]
        self.assertEqual(5, len(row))
        is_safe = self.is_safe(row)
        self.assertFalse(is_safe)

    def test_part1_unsafe_directions(self):
        sample = "1 3 2 4 5"
        raw_row = sample.split()
        row = [int(numeric_string) for numeric_string in raw_row]
        self.assertEqual(5, len(row))
        is_safe = self.is_safe(row)
        self.assertFalse(is_safe)

    def test_part1_unsafe_nochange(self):
        sample = "8 6 4 4 1"
        raw_row = sample.split()
        row = [int(numeric_string) for numeric_string in raw_row]
        self.assertEqual(5, len(row))
        is_safe = self.is_safe(row)
        self.assertFalse(is_safe)

    def test_part1_sample(self):
        with open("sample.txt", "r") as file:
            lines = []
            for lines_temp in file.readlines():
                lines.append(list(map(int, lines_temp.strip().split())))

        safe_count = self.part1(lines)
        self.assertEqual(2, safe_count)

    def test_part1_actual(self):
        with open("day02.txt", "r") as file:
            lines = []
            for lines_temp in file.readlines():
                lines.append(list(map(int, lines_temp.strip().split())))

        safe_count = self.part1(lines)
        self.assertEqual(299, safe_count)

    @staticmethod
    def is_safe2(row):
        if Day02.is_safe(row):
            return True

        for index in range(len(row)):
            new_row = row.copy()
            del new_row[index]
            if Day02.is_safe(new_row):
                return True

        return False

    @staticmethod
    def part2(lines):
        safe_count = 0
        for row in lines:
            safe_count += 1 if Day02.is_safe2(row) else 0
        return safe_count

    def test_part2_sample(self):
        with open("sample.txt", "r") as file:
            lines = []
            for lines_temp in file.readlines():
                lines.append(list(map(int, lines_temp.strip().split())))

        safe_count = self.part2(lines)
        self.assertEqual(4, safe_count)

    def test_part2_actual(self):
        with open("day02.txt", "r") as file:
            lines = []
            for lines_temp in file.readlines():
                lines.append(list(map(int, lines_temp.strip().split())))

        safe_count = self.part2(lines)
        self.assertEqual(364, safe_count)


if __name__ == '__main__':
    unittest.main()
