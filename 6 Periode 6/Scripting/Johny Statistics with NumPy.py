# -*- coding: utf-8 -*-
"""

@author: m.heeren (feat. m.delaat)

To do this assignment (for which you need to run this script), you must have Python installed as well as a Python
IDE such as PyCharm.
Python can be downloaded from   https://www.python.org/downloads/
PyCharm can be downloaded from  https://www.jetbrains.com/pycharm/download/ (choose "Community Edition"!)

How can I open and run a .py file in PyCharm? This is explained in the video "Open Python script in PyCharm and run
it.mp4" which can be found in the folder "Instruction videos about Python" on our portal site:
https://sintlucasedunl.sharepoint.com/sites/T2337

"""

# Welcome to statistics with NumPy: Numerical Python!
# We will use this module to learn about the basics of statistics
# We will be discussing the following topics:
    # Mean
    # Median
    # Percentiles
    # Interquartile Range
    # Standard Deviation
    # Outliers (read-only)

# When you stumble upon the text 'PLEASE, TRY IT YOURSELF', this means that you have to add some
# Python code yourself.

""" Importing NumPy """

# First thing we need to do is import the module
# Or else, nothing you do will work...

import numpy as np

# First, we'll have a look at the beginnings of statistics
# If you really want to know, statistics is defined as:
  # Statistics is the discipline that concerns
    # the collection,
    # organization,
    # analysis,
    # interpretation,
    # and presentation of data.
# So, that's kind of right on the nose for this course!
# Statistics is a broad and deep field
  # Like a vast ocean of knowledge, if you want to get all poetical
# So we'll just get our toes wet


""" The normal distribution """


#                         ###
#                       ##|||##
#                      #|||||||#
#                     #|||||||||#
#                   ##|||||||||||##
#                 ##|||||||||||||||##
#              ###|||||||||||||||||||###
#          ####|||||||||||||||||||||||||####
#   #######|||||||||||||||||||||||||||||||||#######
# SD-3    -2     -1       0       1      2      3


# One of the most important principles in statistics is the NORMAL DISTRIBUTION
# It's also called the bell curve (if you want to judge it by its looks)
# The normal distribution is a distribution that occurs naturally in many situations
# A good example would be the distribution of height in humans
  # Some are very tall, some very short, but most would be somewhere in the middle
# There are other distributions, for example SKEWED distributions (either to the left or right)
  # An example would be 'natural deaths per age category'
    # It would be really weird if that would be normally distributed!
    # Because that would mean the most natural deaths would occur among middle aged people
    # And it would stand to reason that really old people die more of natural causes than younger people
    # So, if you would be visualising deaths per age group
      # It would look much more like the left half of the bell curve
# OK, back to the normal distribution
# The reason we're still talking about this, is because it's important for understanding statistics
  # It's all about the PROBABILITY of a given value occurring in a given data set
  # For example, if I measure 10 random people, the chance that they are all very tall is quite small
    # But maybe at least one of them is quite tall
    # The more people I measure, the more extreme values I will find
    # But still, *most* people I measure would be somewhere in the middle
    # So, while extreme values do happen, the chance that 1 random person has that value is very small
  # Another example would be the lottery
    # Most people never win big
    # A small amount of people win a nice sum once in their life
    # A very very small amount of people actually win
    # And somewhere, out there, one extremely lucky person wins twice
    # So, while the chances are 100% that SOMEONE wins the lottery
      # The chance of you being that someone are smaller than getting struck by lighting
      # You are very much more likely to be somewhere in the middle
    # Although, think about this:
      # Would the distribution of lottery winners look like the normal distribution?
      # Or would it be a SKEWED distribution?
      # Why?
# Here is a nice visualization I found on the internet:
  # https://phet.colorado.edu/sims/html/plinko-probability/latest/plinko-probability_en.html
    # Play around with it for a bit!
  # It illustrates an import concept
    # With a small SAMPLE SIZE (say 10 people) a distribution can look like anything
    # But with every unit added, the distribution starts to look more like the normal distribution


""" Mean """
print('\n************************************** MEAN **************************************')
# Let's start with the MEAN
# The MEAN is the MATHEMATICAL AVERAGE of a SET of NUMBERS
# If your data is normally distributed, the mean would be right in the middle of the bell curve
# You've probably seen loads of examples in your life
  # The average life expectancy of a country for example
  # It's nothing more than the average of the highest ages all people have reached
    # Divided by the number of people
  # And now you have the MEAN life expectancy!

# Let's try!
# Say we have 2 tables, and on each table there are six plates of cookies
  # (cookies are both less ghastly and more tasty than average life expectancy)
# Now say that we would like the average number of cookies on each table
# First, let's make some arrays to represent the tables with their 6 plates of cookies
table1 = np.array([16, 20, 4, 8, 19, 58])
table2 = np.array([17, 56, 8, 25, 14, 9])

# Now, let's use NumPy to calculate the mean (average number of cookies) for each table
table1_avg = np.mean(table1)
table2_avg = np.mean(table2)

if table1_avg > table2_avg:
  print("The plates on table 1 have more cookies on them, averaging "
        + str(table1_avg) + " cookies per plate")
else:
  print("The plates on table 2 have more cookies on them, averaging "
        + str(table2_avg) + " cookies per plate")

# You can also use logical operations on the mean function
# Say you have an array filled with student grades
# And you want to know what percentage of students received a grade higher than 7
# You can use a logical operator for that
# Adding that logical operator does the following:
  # It evaluates, in our example, which grades are higher than 7
  # It then assigns a value of 1 to all grades higher than 7
  # After that, it adds all of the ones and divides it by the total number of grades
  # Resulting in a floating point number which corresponds directly to a percentage
# Let's dance

grades = np.array([6, 8, 8, 2, 4, 6, 8, 4, 9, 10, 5, 6, 7, 3, 10,
                   8, 9, 2, 6, 4, 8, 6, 4, 7, 6, 5, 9, 10, 3, 4,
                   6, 6, 6, 7, 7, 7, 8, 8, 8, 9, 9, 9, 8, 2, 3])

high_marks = np.mean(grades > 7)
print('The following marks are higher than 7:')
print(high_marks)
print()

# PLEASE, TRY IT YOURSELF
  # We've found a value of 0.4 for grades higher than 7, what percentage is that?
# 40%
  # Calculate the mean grade for the class and print it to the console
print('The mean grade for the class is:')
print(np.mean(grades))
print()

""" Median """
print('\n************************************* MEDIAN *************************************')
# We use MEAN to calculate the average of a set of numbers
# But, weirdly, the average doesn't always give us the best indication of what's in the middle
# For example, what if you we're looking at the average rainfall in a month in a given area
# Normally, MEAN would give you a good indication of that
# But what if there were three days where a freak storm hit, resulting in massive rainfall?
  # What do you think would happen to our data?
  # Would the mean still be representative of the *normal* average rainfall in that area?
    # Remember, it was a freak storm, not a regular occurence.

# In this case, the MEDIAN might give us a better indication of how much rain normally falls
# But what is this MEDIAN?
# In a dataset that's ordered from lowest through highest, the MEDIAN is simply the middle value
  # Or, in the case that the dataset has an even amount of numbers, the average between the middle 2

# So, the MEDIAN is useful in cases where some extreme values are messing up our average
# Such extreme values are called OUTLIERS
  # We'll get to them later
# For now, let's take a quick look at how MEDIAN works

rainfall = np.array([10, 15, 0, 4, 3, 0, 12,
                     0, 0, 1, 2, 15, 11, 3,
                     0, 0, 5, 5, 6, 3, 1,
                     4, 120, 148, 0, 0, 0, 2])
print('Rain fall during last month:')
print(rainfall)
print()

# First we need to sort this array, since this is very much not ordered
rain_sorted = np.sort(rainfall)
print('Sorted rain fall:')
print(rain_sorted)
print()

# PLEASE, TRY IT YOURSELF
  # calculate the mean rainfall and print it to the console
print('The mean rain fall during last month:')
print(np.mean(rainfall))
print()
  # Now run the following code:
median_rain_fall = np.median(rain_sorted)
print(median_rain_fall)
  # Take a look at both numbers
    # Are they very different?
#Yes.
    # Read back what we said earlier about outliers, why are the mean and median so different here?
#Because there were 2 days that had way more rainful than the others.

# There are more reasons for using MEDIAN as an indication of the CENTRAL TENDENCY of a set of numbers
  # But that's a problem for a different day

""" Percentiles """
print('********************************** PERCENTILES **********************************')
# So, now we now the MEDIAN is the middle number in a data set
# Being the middle number, we can fairly say the following:
  # 50% of numbers lie above the median
  # 50% of numbers lie below the median
# But, what if we wanted to find the point on which the following is true:
  # 30% of numbers lie above this point
  # 70% of numbers lie below this point
# Such a point is called a PERCENTILE
  # It could be useful for any number of reasons!
  # Discuss this with your classmates, try to find at least two reasons
  # Now I hear you asking the question: is the median a percentile as well?
  # Yes it is, it's just a fancy name for the middle one
# Example time

# We're selling tires now!!

monthly_tire_sales = np.array([6, 8, 7, 2, 3, 4, 7, 9, 5, 0,
                               5, 7, 0, 1, 2, 6, 4, 9, 8, 5,
                               3, 6, 1, 1, 9, 4, 3, 2, 1, 5])

# First, some sorting
np.sort(monthly_tire_sales)

# Now, let's find the 30th percentile
# Remember, this is the point where:
  # 30% of the data is below this value
  # and 70% is above this value
thirtieth_percentile = np.percentile(monthly_tire_sales, 30)
print('30th percentile:')
print(thirtieth_percentile)
print()

# PLEASE, TRY IT YOURSELF
  # Find the 80% percentile, save it in a variable and print it to the console
eightieth_percentile = np.percentile(monthly_tire_sales, 80)
print('80th percentile:')
print(eightieth_percentile)
print()
  # Find the 50% percentile, save it in a variable and print it to the console
fiftieth_percentile = np.percentile(monthly_tire_sales, 50)
print('50th percentile:')
print(fiftieth_percentile)
print()


""" Interquartile range """
print('************************************** IQR **************************************')
# The interquartile range is the range between the 25th and 75th percentile
# RANGE is a measure of the beginning and end of a data set
  # Useful for seeing how spread out the data is
    # For example, while looking at student grades
      # How close together are the results?
      # That tells you something about the test and the students
        # Big spread? Maybe not all students understood well enough
        # Small spread? Maybe the test was way to easy (or way too hard, if most grades are low)
# The interquartile range is useful if you want to see how the 'middle group' performed
  # Due to the tendency of things to be NORMALLY DISTRIBUTED, the bulk of the values lies in this middle group
# It's also super nice for data visualization using Box Plots
  # But we'll get to that later

# Let's calculate an interquartile range
  # Let's call that IQR from now on
# Since were still selling tires for a living, let's use the array of monthly tire sales
# First we calculate the 25th percentile
first_quarter = np.percentile(monthly_tire_sales, 25)
# Then we calculate the 75th percentile
third_quarter = np.percentile(monthly_tire_sales, 75)
# Now we can calculate the IQR!
IQR = third_quarter - first_quarter
print('Interquartile range (IQR) of tire sales:')
print(IQR)
print()

# Hmmm... You could probably write a function for that!
# Maybe you should!
# PLEASE TRY THAT YOURSELF
def calculate_iqr(dataset):
  first_quarter = np.percentile(dataset, 25)
  third_quarter = np.percentile(dataset, 75)
  return third_quarter - first_quarter

""" Standard Deviation """
print('\n******************************* STANDARD DEVIATION ******************************')
# Another important concept in statistics is the STANDARD DEVIATION
# This can be a bit of a brain twister at first
# I'll try to explain it really, really well

# OK, remember the MEAN?
  # The mean is the average 'score' in a given data set
# The STANDARD DEVIATION is a measure for how much the data is spread
# What again do we mean by spread? Let's take a look:
array1 = np.array([3, 4, 7, 4, 7, 4, 8, 4, 4, 5])
print("Mean array1 = " + str(np.mean(array1)))
array2 = np.array([3, 1, 12, 9, 14, 7, 1, 0, 2, 1])
print("Mean array2 = " + str(np.mean(array2)))

# We can see that both arrays have the same mean (50)
# But the range of array1 lies between 3 and 8
# The range of array2 lies between 0 and 14
# That's quite a difference!
# You could say the data in array2 has a larger SPREAD even though the MEAN is the same

# The STANDARD DEVIATION can be used as a measure for how spread out the data is
# If you want an explanation on how it's calculated, take a look over here:
  # https://www.mathsisfun.com/data/standard-deviation.html
  # NOTE: We don't expect you to remember this, but it may help with understanding
# For now, let's just remember it can be used as a measure of how spread the data is

# Let's see it in action for both our arrays
std_array1 = np.std(array1)
print("The standard deviation for array 1 is " + str(std_array1))
std_array2 = np.std(array2)
print("The standard deviation for array 2 is " + str(std_array2))

# We can clearly see that array 2's STANDARD DEVIATION is a lot larger
# Which makes sense, because the data is much more spread out
# We can also see that just by looking at the arrays
  # But that's because they don't contain a lot of data
  # It would be different for larger datasets

# Now that we now what the STANDARD DEVIATION can be used as a measure of SPREAD
# We can take a look at what it means for individual data points
# Because, when data is normally distributed
# The standard deviation tells us something about how unusual a given data point is
# Take a look at this normal distribution again:

#                         ###
#                       ##|||##
#                      #|||||||#
#                     #|||||||||#
#                   ##|||||||||||##
#                 ##|||||||||||||||##
#              ###|||||||||||||||||||###
#          ####|||||||||||||||||||||||||####
#   #######|||||||||||||||||||||||||||||||||#######
# SD -3    -2     -1       0       1      2      3

# The numbers on the bottom mean how many STANDARD DEVIATIONS a data point is from the center of the bell curve
# As a rule of thumb, you can follow these guidelines:
  # 68% of data in a normally distributed dataset lies between -1 and 1 standard deviations from the center
  # 95% of data in a normally distributed dataset lies between -2 and 2 standard deviations from the center
  # 99.7% of data in a normally distributed dataset lies between -3 and 3 standard deviations from the center

# That means if you have a data point that's 3 STANDARD DEVIATIONS from the center...
  # That's a very unusual data point!
  # Because then only 0.3% of all data will be larger (or smaller)!

# Of course, we can calculate how many standard deviations a given point lies from the center
# Let us try
# We'll need a cool new array first
hamster_size = np.random.normal(loc=4, scale=2, size=100)
print('Randomly generated hamster sizes:')
print(hamster_size)

# Did you see that? I just casually created a normally distributed array of random numbers
  # Here's how you do that: https://www.sharpsightlabs.com/blog/numpy-random-normal/
  # Useful for practicing purposes, or checking stuff
  # Anyway...

# First let's see the MEAN
print("The average hamster measures " + str(np.mean(hamster_size)) + " centimeters")

# Now let's sort the array to easily find the smallest and largest hamsters
hamsters_sorted = np.sort(hamster_size)
print('Sorted hamster sizes:')
print(hamsters_sorted)

# So, how small is the smallest hamster? And the largest?
# Let's see how many STANDARD DEVIATIONS they deviate from the mean

# Step 1: calculate the difference between the hamster's length and the mean
  # I used the index instead of a hard coded number because:
    # This array is newly generated every time this code runs, so hard coding would be pointless
    # More importantly, you could make a mistake looking up the largest and smallest number
      # This way you can't make that mistake
largest_hamster_difference = hamsters_sorted[-1] - np.mean(hamster_size)
smallest_hamster_difference = hamsters_sorted[0] - np.mean(hamster_size)

# Step 2: Use the difference between that point and the mean
  # This is how you find how many STANDARD DEVIATIONS the hamster's length is away from the mean
print("standard deviation :" + str(np.std(hamster_size)))
print("mean: " + str(np.mean(hamster_size)))

largest_hamster_deviations = largest_hamster_difference / np.std(hamster_size)
print("STDs largest hamster removed from mean: " + str(largest_hamster_deviations))

smallest_hamster_deviations = smallest_hamster_difference / np.std(hamster_size)
print("STDs smalles hamster removed from mean: " + str(smallest_hamster_deviations))

# So now we know how many STANDARD DEVIATIONS they deviate from the mean
  # We call this score a Z-SCORE
  # You can calculate it for any data point in the set

# PLEASE TRY IT YOURSELF
  # Calculate the z-scores for 2 different hamsters in the array
  # how many standard deviations do they deviate from the mean?
def calculate_deviation(dataset, index):
  difference = dataset[index] - np.mean(dataset)
  deviation = difference / np.std(dataset)
  return deviation
calculate_deviation(hamster_size, 30)
calculate_deviation(hamster_size, 60)

""" Standard Deviation: read-only """
# Now that we know how many STANDARD DEVIATIONS a given point is removed from the mean
  # We would like to know what percentile this score correlates to
  # But there's not really an easy way to do that
    # It's a whole statistical math thing we're not going to get into
      # And which we do NOT EXPECT you to know
  # But luckily, we have the ***INTERNET***!!!
    # Traditionally, if you didn't want to calculate a percentile from z you'd use a lookup table
      # You'd also just use a table because it's way quicker
      # Doing the math yourself is usually just a waste of time
    # It's really just a table where you look up z-scores and find the corresponding percentile
    # Nowadays, we can just use websites to do this for us
    # Take a look over at https://mathcracker.com/z-score-percentile-calculator
      # You can enter your z-score and it will tell you the corresponding percentile
      # Easy peasy lemon squeezy


""" Outliers: read-only """
# Outliers... They're tricky
# A really high or really low value in a given dataset can really mess up your MEAN
  # For example, say we have a population of giant people whose general height is around 2.80m
  # And let's also say the rest of the people are between 2.70m and 2.90m
  # If we now throw in Mark Heeren, who is around 1.90m, he could really mess up the MEAN
    # Because he is really short, compared to the giants, his score will influence the MEAN a lot
    # And so his score will drag down the average height (MEAN) of the giant's height dataset
  # Sometimes, you don't want that
  # So you take a look at the data (after visualizing it with MatPlotLib, for example)
    # And if you visually see an outlier, you can find it in the data
    # If you find it screws with the mean a lot, you can choose to remove it
  # But removing an outlier should never be done lightly
    # In some cases, it's an obvious error in data collection and removal is a good choice
    # In other cases, it could just be a naturally occurring extreme score
      # This especially happens with what we call natural data
      # For example, data from psychological research
  # So, just because a data point is an outlier is in itself no grounds for removal
    # It's really a case-by-case, dataset-by-dataset kind of consideration
# For now, it's more than enough to know about the existence of outliers
  # We take a look at them later on when we're taking on MatPlotLib

# VERY OPTIONALLY TRY IT FOR YOURSELF
  # If you're feeling up for it, you can write a function for detecting possible outliers
  # What you need:
    # Get yourself an array with data
#bunny_size = np.random.normal(loc=4, scale=2, size=100)
#print(bunny_size)
bunny_size = np.random.normal(loc=4, scale=2, size=100)
    # For every data point, transform it into a z-score
        # Store them all in a new array
bunny_zscores = []
    # Now you have an array of z-scores!
for i in range(0, len(bunny_size)):
  bunny_zscores.append(calculate_deviation(bunny_size, i))
bunny_zscores = np.array(bunny_zscores)
    # Make a variable called max_std or something
    # Give it a value of 2 or 3, depending on how many STANDARD DEVIATIONS you are willing to tolerate
max_std = 2
    # Loop over the array, removing all data points which violate the maximum you set
    # Transform all the z-scores back into real scores, into a new array
bunny_newsize = []
for i in range(0, len(bunny_zscores)):
  if abs(bunny_zscore[i]) < max_std:
    bunny_newsize.append(bunny_size[i])
    # Behold the awesomeness of what you just did
      # Optional: Fear what you have now become
      # Optional: Forever wonder if you've rightly removed the outlier(s)

