from FileNames import FileNames
from LoadResults import open as loadCSV
import matplotlib.pyplot as plt
import pandas as pd

def main():
    dataframe = loadCSV(FileNames.userSurvey)

    dataframe.hist(column='age', bins=10)

if __name__ == '__main__':
    main()