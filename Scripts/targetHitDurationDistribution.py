from FileNames import FileNames
from LoadResults import open as loadCSV
import matplotlib.pyplot as plt
import pandas as pd
import os, re

def main():
    print("Im alive")
    # load log file
    # aggregate logfile data into dataframe

    # for filename in os.listdir(FileNames.resultRoot):
    #     try:
    #         regex = re.compile(r'\d+')
    #         number = regex.findall(filename)
    #         df = loadCSV(filename)
    #
    #
    #     except Exception as e:
    #         print(e)


if __name__ == 'main':
    main()