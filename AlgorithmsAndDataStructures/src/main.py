'''Module to calculate out a Fibonacci sequence.'''
import time


def fibonacci(num):
    '''Gets the next Fibonacci number.'''
    if num == 0:
        return 0
    elif num in (1, 2):
        return 1
    else:
        return fibonacci(num - 1) + fibonacci(num - 2)


def get_fibonacci(num):
    '''Gets a sequence of Fibonacci numbers.'''
    sequence = []
    for i in range(num):
        sequence.append(fibonacci(i))
    return sequence


start_time = time.time()
fib = get_fibonacci(35)
end_time = time.time() - start_time

for i in fib:
    print("Fib sequence: "+str(i))

print("\n--- completed in: %s seconds ---\n" % end_time)
