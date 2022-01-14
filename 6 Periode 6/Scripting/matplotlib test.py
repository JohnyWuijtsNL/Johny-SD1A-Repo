import matplotlib.pyplot as plt
import numpy as np

def formula (x_value):
    return x_value * x_value

xpoints = np.array(list(range(-20, 21)))
ypoints = np.empty(xpoints.shape)
for i in range(0, xpoints.size):
    ypoints[i] = formula(xpoints[i])
plt.plot(xpoints, ypoints)
plt.show()
