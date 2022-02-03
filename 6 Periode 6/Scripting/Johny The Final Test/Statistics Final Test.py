#
# In this final test assignment about data and statistics in Python, we
# will plot a set of Bitcoin prices (from a csv file) as well as the mean
# and median price of the data set.
#
# To do this assignment, you have to open the script in PyCharm as a project
# and create a configuration to run it.
# Make sure you have copied the file 'bitcoin_price.csv' to the same folder
# as this script!
#
# If you see the text 'TO DO', then you have to complete the code. Usually
# a part of the solution is already given. You have to uncomment the
# corresponding code and fix the code where '?' is given.
#

# We start by importing the libraries that we need.
# You must also have the corresponding packages installed. In PyCharm, you can
# do this via View -> Tool Windows -> Python Packages and then find each package
# and install it.
#
# Here we import matplotlib
from matplotlib import pyplot as plt
# TODO: on the next line, import the numpy library. Use the shorthand name 'np' for the library:
import numpy as np
# Let's load the last 100 Bitcoin prices (data until 24-1-2022) into an array:
#   Have a look at the .csv file, and see how it is just a list of numbers
#   that we load into an array.
# TODO: specify the file name of the csv file in the statement below:
data = np.loadtxt('bitcoin_price.csv')

# TODO: Create an array from the loaded data and name it price_array:
price_array = np.array(data)

# Compute the mean and median price of all prices in the period
# TODO: Compute mean price and store it in the variable 'mean_price':
mean_price = np.mean(price_array)

# TODO: Compute median price and store it in the variable 'median_price':
median_price = np.median(price_array)

# We need to plot the prices of our array, but also the mean and median of the array.
# Therefore, we will create two new arrays (one for the mean and one for the median)
# These must have the same length as our original array. We must set all items in the
# first array to the mean price and all items in the second array to the median price.
# TODO: Complete the statement below to create an array with the mean price. The second
#  argument must be the length of our price array.
mean_array = np.repeat(mean_price, price_array.size)

# TODO: Do the same for the median array:
median_array = np.repeat(median_price, price_array.size)

# In the next part, we will plot the data (we will create a line chart)
# the chart is not visible until we call plt.show() (see last line of code).

# TODO: Plot the array that contains the prices:
plt.plot(price_array)

# Plot the the mean, with a dashed line style
# TODO: Plot the mean. Give the line a dashed line style
#  (search for help online for matplotlib plot)
plt.plot(mean_array, linestyle='dashed')

# Plot the median, with a dash/dot line style
# TODO: Plot the median. Give the line a dash/dot line style
plt.plot(median_array, linestyle='dashdot')

# Let's also show a legend on the chart, which explains the meaning of
# the lines.
# TODO: Show a legend with the following labels: 'USD', 'mean' and 'median'
plt.legend(["USD", "mean", "median"])

# Finally, we show the plotted chart
plt.show()
