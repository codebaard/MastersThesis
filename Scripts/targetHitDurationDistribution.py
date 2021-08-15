from FileNames import FileNames
import matplotlib.pyplot as plt
import pandas as pd
import os, re
import csv

data = list()

for filename in os.listdir(FileNames.logfiles):
    try:
        regex = re.compile(r'\d+')
        number = regex.findall(filename)
        file_CSV = open(FileNames.logfiles + filename)
        data_CSV = csv.reader(file_CSV, delimiter = ';')
        list_CSV = list(data_CSV)

        for index, row in enumerate(list_CSV):
            # use 'index' to access n++ or n-- element in list
            # consider: https://iambipin.medium.com/accessing-next-element-while-iterating-python-tips-44a6fc563490
            if row[1] == 'Model.NeuroTagMarkedAsTargetLogEntry':
                row = number + row
                data.append(row)
            elif row[1] == 'Model.NeuroTagHitLogEntry':
                row = number + row
                data.append(row)

    except Exception as e:
        print(e)

df = pd.DataFrame(data)
df.columns = ['participant', 'timestamp', 'eventtype', 'value']

print(df)
