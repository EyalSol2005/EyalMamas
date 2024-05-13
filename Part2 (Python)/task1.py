import math


def num_len(num: int) -> int:
    """
      The function finds the length of the given number

      Args:
      sum (int): The number to find its size

      Returns:
      int: The length of the given number
      """
    return 1 if num < 10 else math.floor(math.log10(num)) + 1


print(num_len(150))