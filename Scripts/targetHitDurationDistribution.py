import copy

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

print(combinedData)

hitTimes = list()

for index, row in enumerate(combinedData):
    result = 0
    if row[2] == 'Model.NeuroTagMarkedAsTargetLogEntry':
        if combinedData[index+1][2] == 'Model.NeuroTagHitLogEntry' and combinedData[index+1][3] == row[3]:
            result = int(combinedData[index+1][1]) - int(row[1])
        if combinedData[index+2][2] == 'Model.NeuroTagHitLogEntry' and combinedData[index+2][3] == row[3]:
            result = int(combinedData[index+2][1]) - int(row[1])
        if combinedData[index+3][2] == 'Model.NeuroTagHitLogEntry' and combinedData[index+3][3] == row[3]:
            result = int(combinedData[index+3][1]) - int(row[1])

    hitSet = [row[0], row[3], result, row[4], row[5], row[6]]

    hitTimes.append(hitSet)

print(hitTimes)




