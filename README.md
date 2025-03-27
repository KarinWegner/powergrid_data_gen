This program is a synthetic data generator for Powerline and Transformer components. 
When ran, it generates 3 LogObjects, each containing log start- and end dates, a component with randomized data specifications and a logbook containing houly logged data with timestamps for 5 to 10 days.

Data is generated based on transformer power rating och power line length. Transformer has a wider span, may be altered in the future.

Voltage categories:
----------------------------------------------
low_voltage           |                      |
                      |                      |
medium_voltage        |- Powerline span      |
                      |                      |
high_voltage          |                      |- Transformer span
-----------------------                      |
extra_high_voltage                           |
                                             |
ultra_high_voltage                           |
----------------------------------------------

