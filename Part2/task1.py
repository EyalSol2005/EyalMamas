import math

def num_len(num):
    return 1 if num < 10 else math.floor(math.log10(num)) + 1

print(num_len(123413))