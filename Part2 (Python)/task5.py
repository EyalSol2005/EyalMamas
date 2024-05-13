import mpmath


def reverse_n_pi_digits(n: int) -> int:  # Calculates the 'n' digits of pie and reverses them
    mpmath.mp.dps = n
    pi = str(mpmath.pi)
    return pi[::-1]


print(reverse_n_pi_digits(20))
