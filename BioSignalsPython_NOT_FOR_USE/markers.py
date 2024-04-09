import pyxdf
import matplotlib.pyplot as plt
import numpy as np
from datetime import datetime, timedelta
import time

data, header = pyxdf.load_xdf('markers.xdf')

for stream in data:
    time_stamps = stream["time_stamps"]
    markers = stream["time_series"]

    print(markers)

plt.show()