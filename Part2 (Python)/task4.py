from typing import List

import matplotlib.pyplot as plt
import numpy as np


def get_numbers_from_user() -> List[int]:
    """
    The function gets all the numbers from the user

    Args:
    None

    Returns:
    List[int]: List of all the numbers that the user entered
  """

    numbers = []
    user_input: str = ""
    while user_input != "-1":

        user_input = input("Enter number: ")
        if user_input != "-1":
            try:
                numbers.append(float(user_input))
            except ValueError:
                print("Not a valid number.")

    return numbers


def print_avg(numbers: List[int]) -> None:  # Prints the numbers' average
    print(f"Average: {sum(numbers) / len(numbers)}")


def print_amount_positive_numbers(numbers: List[int]) -> None:  # Prints the amount of positive numbers
    positive_numbers = filter(lambda x: x > 0, numbers)

    print(f"Amount Positive: {len(list(positive_numbers))}")


def print_sorted_numbers(numbers: List[int]) -> None:  # Prints the numbers from lowest to largest
    sorted_numbers = numbers
    sorted_numbers.sort()
    print(f"Sorted Numbers: {sorted_numbers}")


def show_graph(x_points: List[int], y_points: List[int]) -> None:  # Shows the numbers graph
    plt.plot(x_points, y_points)
    plt.show()


def print_pearson(x_points: List[int], y_points: List[int]) -> None:  # Prints the pearson correlation coefficient
    pearson = np.corrcoef(x_points, y_points)[0, 1]
    print(f"Pearson correlation coefficient: {pearson}")


def main() -> None:
    numbers = get_numbers_from_user()
    print(f"Numbers: {numbers}")
    print_avg(numbers)
    print_amount_positive_numbers(numbers)
    print_sorted_numbers(numbers)

    x_points = np.array([i for i in range(0, len(numbers))])
    y_points = np.array(numbers)

    print_pearson(x_points, y_points)
    show_graph(x_points, y_points)


if __name__ == "__main__":
    main()
