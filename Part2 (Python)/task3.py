import math


def is_sorted_polyndrom(word: str) -> bool:
    """
  The function checks if the given word is a sorted polyndrom

  Args:
  word (str): The word to check

  Returns:
  bool: True if sorted polyndrom, of False if isn't.
  """
    for i in range(len(word)):  # Loop over the word

        char_start = word[i]
        char_end = word[len(word) - 1 - i]  # The mirrored char from the end

        if char_start != char_end:  # The char is not the same as the mirrored char
            return False

        if i > 0 and word[i] < word[i - 1]:  # The chars aren't sorted
            return False

        if (i == len(word) - 1 - i) or (
                i + 1 > len(word) - i):  # Reached the middle or the next step will switch the chars
            return True
    return True


print(is_sorted_polyndrom('אבגדגבא'))
