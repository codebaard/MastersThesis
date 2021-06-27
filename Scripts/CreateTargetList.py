import random
import os

def main():
    random.seed(os.urandom(16))
    count = 50
    results = []

    for i in range(50):
        results.append(random.randint(0,9))

    print(results)

if __name__ == '__main__':
    main()