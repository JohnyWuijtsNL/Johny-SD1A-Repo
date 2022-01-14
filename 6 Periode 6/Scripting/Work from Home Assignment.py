# This assignment about data and .csv files can be done from home on your laptop.
# You do not need a pi-top, but you will need to install the PyCharm IDE for
# running this Python script.
#
# First, download and install PyCharm from https://www.jetbrains.com/pycharm/download/
#
# Then, watch the instruction video "Open Python script in PyCharm and run it",
# which can be found on the "Training: Python" web page. The video will help
# you get started with PyCharm.
#
# Before you do this assignment, make sure that you have completed the
# "Basic Syntax.py" assignment from previous lesson. It can be found
# in the "Lesson 1 and 2" folder on the "Training: Python" web page.

# ******************************** THE ASSIGNMENT ********************************
# 1. Complete the script below so you can create a .csv file with data
# 2. Import the .csv file in Microsoft Excel (choose Data -> From Text/CSV in
#    the menu bar).
# 3. Create a bar chart for the data that was imported
# ********************************************************************************

# The following line will make the csv functions from Python available to this
# script
import csv


# On this line, we set the name of our .csv file
csv_file_name = 'body-length.csv'


# Here, we will open the file and write data to it
with open(csv_file_name, 'w', newline='') as csvfile:
    data_writer = csv.writer(csvfile, delimiter=',', quotechar='"', quoting=csv.QUOTE_MINIMAL)

    # Let's write the header row
    data_writer.writerow(['Name', 'Length'])

    # We will add 10 rows of data to the file. The loop is already there, but you need
    # to complete the code.
    for i in range(0, 10):
        # TO DO: ask for the name of a friend or family member and then for his/her body length.
        # store the results in the name and length variables.
        # If you need help about reading user input, then search the web for 'python user input'.
        # You will also need to convert the second answer (body length) to a float
        # (search the web for 'python string to float').
        name = ''
        length = 0.0
        length_chosen = False
        print("New user, please enter your name.")
        while name == '':
            name = input()
        print(name + ", please input your length in meters.")
        while not length_chosen:
            length_chosen = True
            length = input()
            try:
                length = float(length)
            except ValueError:
                print("Invalid input! Please try again.")
                length_chosen = False
            if length_chosen:
                if length > 2.72:
                    print("You're either taller than the tallest person to ever exist, or you've entered your length wrong.")
                    length_chosen = False
                if length < 0.599:
                    print("You either entered your length wrong, or you've not been born yet.")
                    length_chosen = False
                
        # And finally, write a line of code that adds the name and length to the .csv file
        # Search the web for 'python csv write row' for help.
        data_writer.writerow([name, length])



