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

            if row[1] == 'Model.NeuroTagMarkedAsTargetLogEntry':
                row = number + row
                data.append(row)
            elif row[1] == 'Model.NeuroTagHitLogEntry':
                row = number + row
                data.append(row)

    except Exception as e:
        print(e)

# use 'index' to access n++ or n-- element in list
# consider: https://iambipin.medium.com/accessing-next-element-while-iterating-python-tips-44a6fc563490
for index, row in enumerate(data):
    if data[index+1][2] != 'Model.NeuroTagHitLogEntry' and data[index+2][2] != 'Model.NeuroTagHitLogEntry':
        print("Gotcha!")

df = pd.DataFrame(data)
df.columns = ['participant', 'timestamp', 'eventtype', 'value']

print(df)
