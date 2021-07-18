from FileNames import FileNames
from LoadResults import open as loadCSV
import matplotlib.pyplot as plt
import pandas as pd

def main():
    df = loadCSV(FileNames.userSurvey)

    plt.hist(df.age, bins=16)

    plt.xlabel('Age groups')
    plt.ylabel('Participants')

    plt.grid()
    plt.show()

if __name__ == '__main__':
    main()