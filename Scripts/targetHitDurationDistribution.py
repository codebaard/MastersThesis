import copy

from FileNames import FileNames
import matplotlib.pyplot as plt
import numpy as np
import pandas as pd
import os, re
import csv
import seaborn

data = list()

for filename in os.listdir(FileNames.logfiles):
    try:
        regex = re.compile(r'\d+')
        number = regex.findall(filename)
        file_CSV = open(FileNames.logfiles + filename)
        data_CSV = csv.reader(file_CSV, delimiter=';')
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
    if index < (len(data) - 2) and data[index + 1][2] != 'Model.NeuroTagHitLogEntry' and data[index + 2][
        2] != 'Model.NeuroTagHitLogEntry':
        newDataset = copy.deepcopy(data[index + 1])
        newDataset[2] = 'Model.NeuroTagHitLogEntry'
        newDataset[3] = data[index - 1][3]
        data.insert(index + 2, newDataset)

surveyData = pd.read_csv(FileNames.userSurvey, sep=';',
                         header=0, encoding='ascii', engine='python')
combinedData = list()
for row in data:
    set = surveyData.loc[surveyData['participant'] == int(row[0])]
    setList = set.values.tolist()
    row = row + setList[0][1:]
    combinedData.append(row)

logData = pd.DataFrame(combinedData)

# print(combinedData)

hitTimes = list()

for index, row in enumerate(combinedData):
    result = 0
    if row[2] == 'Model.NeuroTagMarkedAsTargetLogEntry':
        if combinedData[index + 1][2] == 'Model.NeuroTagHitLogEntry' and combinedData[index + 1][3] == row[3] and \
                combinedData[index + 1][0] == row[0]:
            result = int(combinedData[index + 1][1]) - int(row[1])
        elif combinedData[index + 2][2] == 'Model.NeuroTagHitLogEntry' and combinedData[index + 2][3] == row[3] and \
                combinedData[index + 2][0] == row[0]:
            result = int(combinedData[index + 2][1]) - int(row[1])
        elif combinedData[index + 3][2] == 'Model.NeuroTagHitLogEntry' and combinedData[index + 3][3] == row[3] and \
                combinedData[index + 3][0] == row[0]:
            result = int(combinedData[index + 3][1]) - int(row[1])

        if result < 120000 and result > 2000:
        # if result > 2000 and result < 10000: # for reduced window
        # if result > 2000 and result < 10000: # for glasses and gender
        # if result > 2000 and result < 20000: # for targets
            hitSet = [row[0], row[3], result / 1000, row[4], row[5], row[6]]
            hitTimes.append(hitSet)

hitTimeData = pd.DataFrame(hitTimes)
hitTimeData.columns = ['participant', 'target', 'time', 'age', 'gender', 'glasses']

## Glasses or not
# withGlasses = list()
# noGlasses = list()
#
# for index, row in enumerate(hitTimes):
#     #row[2] = int(row[2])
#     if row[5]: # true
#         withGlasses.append(row[2])
#     else:
#         noGlasses.append(row[2])
#
# #glasses = [np.array(withGlasses)[:, 2] ,np.array(noGlasses)[:,2]]
# glasses = [withGlasses ,noGlasses]
# plt.boxplot(glasses)
#
# plt.xticks([1, 2],['Glasses', 'No Glasses'])
# plt.ylabel('Time (s)')
#
# plt.show()

## Gender
# male = list()
# female = list()
#
# for index, row in enumerate(hitTimes):
#     #row[2] = int(row[2])
#     if row[4] == 'm': # true
#         male.append(row[2])
#     else:
#         female.append(row[2])
#
# #glasses = [np.array(withGlasses)[:, 2] ,np.array(noGlasses)[:,2]]
# glasses = [male ,female]
# plt.boxplot(glasses)
#
# plt.xticks([1, 2],['Male', 'Female'])
# plt.ylabel('Time (s)')
#
# plt.show()

## Time Distribution for specific numbers

# zero = hitTimeData.loc[hitTimeData['target'] == '0'].time.to_numpy(dtype=float)
# one = hitTimeData.loc[hitTimeData['target'] == '1'].time.to_numpy(dtype=float)
# two = hitTimeData.loc[hitTimeData['target'] == '2'].time.to_numpy(dtype=float)
# three = hitTimeData.loc[hitTimeData['target'] == '3'].time.to_numpy(dtype=float)
# four = hitTimeData.loc[hitTimeData['target'] == '4'].time.to_numpy(dtype=float)
# five = hitTimeData.loc[hitTimeData['target'] == '5'].time.to_numpy(dtype=float)
# six = hitTimeData.loc[hitTimeData['target'] == '6'].time.to_numpy(dtype=float)
# seven = hitTimeData.loc[hitTimeData['target'] == '7'].time.to_numpy(dtype=float)
# eight = hitTimeData.loc[hitTimeData['target'] == '8'].time.to_numpy(dtype=float)
# nine = hitTimeData.loc[hitTimeData['target'] == '9'].time.to_numpy(dtype=float)
#
# plt.boxplot([zero, one, two, three, four, five, six, seven, eight, nine])
#
# #plt.xticks([0, 1, 2, 3, 4, 5, 6, 7, 8, 9],['0','1','2','3','4','5','6','7','8','9'])
# plt.xticks(np.arange(11),['origin', '0','1','2','3','4','5','6','7','8','9'])
# plt.xlabel('Target Number')
# plt.ylabel('Time')
#
# plt.show()

## Speed Increase in later cues
seaborn.set(style = 'whitegrid')

discreteTimings= list()
Timings = np.zeros((26, 51))

for i in range(1,30):
    temp = hitTimeData.loc[hitTimeData['participant'] == str(i)].time.to_numpy(dtype=float).tolist()
    #temp = hitTimeData.loc[hitTimeData['participant'] == str(i)].time.to_numpy(dtype=float)
    if len(temp) == 0:
        continue
    if len(temp) == 50: # data cosmetics
        avg = sum(temp)/len(temp)
        temp.append(avg)
    if len(temp) == 49: # data cosmetics
        avg = sum(temp)/len(temp)
        temp.append(avg)
        temp.append(avg)

    discreteTimings.append(temp)

for index, row in enumerate(discreteTimings):
    Timings[index] = np.array(row)

# tdf = pd.DataFrame(Timings)

# seaborn.violinplot(x ="Targets",
#              y ="Time (s)",
#              data = discreteTimings)

# Create a figure instance
fig = plt.figure()

# Create an axes instance
ax = fig.add_axes([0,0,1,1])

# Create the boxplot
bp = ax.violinplot(Timings)
plt.show()

# plt.errorbar(Timings)
#
# plt.xlabel('Targets 1-50')
# plt.ylabel('Time (s)')
#
# plt.show()

## Time Distribution

# bins = pd.IntervalIndex.from_tuples([(0, 30), (31, 60), (61, 90)])
# #bins = np.arrange(0, 31, 61)
# #TimesByAge = hitTimeData.groupby(['age', pd.cut(hitTimeData.time, bins)])
#
# plt.hist(hitTimeData.time, bins=64, ec='k', stacked=True)
#
# #plt.xlabel('Detection Time (seconds) for 2<t<10')
# plt.xlabel('Detection Time (seconds)')
# plt.ylabel('Occurence')
#
# #plt.xscale('log')
# plt.yscale('log')
#
# plt.grid()
# plt.show()
