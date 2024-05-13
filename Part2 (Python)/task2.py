import math


def pythogorean_triplet_by_sum(sum: int) -> None:
    """
  The function finds Pythagorean triplets whose their sum is equal to the given sum

  Args:
  sum (int): The given goal sum for the Pythagorean triplets.

  Returns:
  None: No return value. It prints the Pythagorean triplets
        whose sum is equal to the given sum.
  """
    for a in range(1, sum - 1):
        for b in range(a, sum - 1):  # Start from a to not repeat same triplets
            c = math.hypot(a, b)

            if c % 1 != 0:  # Not an int, so skip
                continue

            c = int(c)

            if a + b + c == sum:  # Equal to the goal sum
                print(f"{a} + {b} + {c} = {sum}")
                break
            elif a + b + c > sum:  # Skip if greater than the goal sum
                break


pythogorean_triplet_by_sum(180)
