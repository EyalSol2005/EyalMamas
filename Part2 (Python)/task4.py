def get_numbers_from_user():
  numbers = []

  user_input: str = ""
  while (user_input != "-1"):

    user_input = input("Enter number: ")
    if user_input != "-1":
      try:
        numbers.append(float(user_input))
      except ValueError:
        print("Not a valid number.")

  return numbers


def print_avg(numbers):
  print(f"Average: {sum(numbers) / len(numbers)}")


def print_amount_positive_numbers(numbers):
  positive_numbers = filter(lambda x: x > 0, numbers)

  print(f"Amount Positive: {len(list(positive_numbers))}")


def print_sorted_numbers(numbers):
  sorted_numbers = numbers
  sorted_numbers.sort()
  print(f"Sorted Numbers: {sorted_numbers}")


def main():

  numbers = get_numbers_from_user()
  print(numbers)
  print_avg(numbers)
  print_amount_positive_numbers(numbers)
  print_sorted_numbers(numbers)


if __name__ == "__main__":
  main()
