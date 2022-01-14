import csv
import matplotlib.pyplot as plt
import numpy as np

with open('zonsopkomsten-2022.csv', mode ='r')as file:
    csv_file = csv.reader(file)
    #array for storing sunrise and sunset in minutes after midnight
    minute_array = []
    #convert all values from database into minutes and store in array
    for lines in csv_file:
        temp = []
        for numbers in lines:
            temp.append(int(numbers[0] + numbers[1]) * 60 + int(numbers[3] + numbers[4]))
        minute_array.append(temp)
    #array for storing all sunrises in minutes after midnight
    zonsopkomsten = []
    #array for storing all sunsets in minutes after midnight
    zonsondergangen = []
    temp = []
    temp2 = []
    #append an empty array to both temps, one for each month
    for i in range(0, 12):
        temp.append([])
        temp2.append([])
    #devide all sunsets and sunrises among the temp arrays
    for lines in minute_array:
        for i in range(0, len(lines)):
            if i % 2 == 0:
                zonsopkomsten.append(lines[i])
            else:
                zonsondergangen.append(lines[i])
    for i in range(0, len(zonsopkomsten)):
        if zonsopkomsten[i] != 0:
            temp[i % 12].append(zonsopkomsten[i])
    for i in range(0, len(zonsondergangen)):
        if zonsopkomsten[i] != 0:
            temp2[i % 12].append(zonsondergangen[i])
    zonsopkomsten = []
    zonsondergangen = []
    for lines in temp:
        for numbers in lines:
            zonsopkomsten.append(numbers)
    for lines in temp2:
        for numbers in lines:
            zonsondergangen.append(numbers)
                        
                
            
        
xpoints = np.array(list(range(0, len(zonsopkomsten))))
zonsopkomsten = np.array(zonsopkomsten)
zonsondergangen = np.array(zonsondergangen)
plt.plot(xpoints, zonsopkomsten)
plt.plot(xpoints, zonsondergangen)
plt.show()
