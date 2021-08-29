import copy

from FileNames import FileNames
import matplotlib.pyplot as plt
import numpy as np
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

for index, row in enumerate(data):
    if index < (len(data)-2) and data[index+1][2] != 'Model.NeuroTagHitLogEntry' and data[index+2][2] != 'Model.NeuroTagHitLogEntry':
        newDataset = copy.deepcopy(data[index+1])
        newDataset[2] = 'Model.NeuroTagHitLogEntry'
        newDataset[3] = data[index-1][3]
        data.insert(index+2, newDataset)


surveyData = pd.read_csv(FileNames.userSurvey, sep=';',
                           header=0, encoding='ascii', engine='python')
combinedData = list()
for row in data:
    set = surveyData.loc[surveyData['participant'] == int(row[0]) ]
    setList = set.values.tolist()
    row = row + setList[0][1:]
    combinedData.append(row)

logData = pd.DataFrame(combinedData)

#print(combinedData)

hitTimes = list()

for index, row in enumerate(combinedData):
    result = 0
    if row[2] == 'Model.NeuroTagMarkedAsTargetLogEntry':
        if combinedData[index+1][2] == 'Model.NeuroTagHitLogEntry' and combinedData[index+1][3] == row[3] and combinedData[index+1][0] == row[0]:
            result = int(combinedData[index+1][1]) - int(row[1])
        elif combinedData[index+2][2] == 'Model.NeuroTagHitLogEntry' and combinedData[index+2][3] == row[3] and combinedData[index+2][0] == row[0]:
            result = int(combinedData[index+2][1]) - int(row[1])
        elif combinedData[index+3][2] == 'Model.NeuroTagHitLogEntry' and combinedData[index+3][3] == row[3] and combinedData[index+3][0] == row[0]:
            result = int(combinedData[index+3][1]) - int(row[1])

        if result < 120000 and result > 2000:
        #if result > 2000 and result < 10000:
            hitSet = [row[0], row[3], result/1000, row[4], row[5], row[6]]
            hitTimes.append(hitSet)

hitTimeData = pd.DataFrame(hitTimes)
hitTimeData.columns = ['participant', 'target', 'time', 'age', 'gender', 'glasses']

bins = pd.IntervalIndex.from_tuples([(0, 30), (31, 60), (61, 90)])
#bins = np.arrange(0, 31, 61)
#TimesByAge = hitTimeData.groupby(['age', pd.cut(hitTimeData.time, bins)])

plt.hist(hitTimeData.time, bins=64, ec='k', stacked=True)

#plt.xlabel('Detection Time (seconds) for 2<t<10')
plt.xlabel('Detection Time (seconds)')
plt.ylabel('Occurence')

#plt.xscale('log')
plt.yscale('log')

plt.grid()
plt.show()




