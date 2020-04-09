# KrankenHause
Did some work with threading and delegates. There are 4 different threads who should work together. Simulate a hospital and logs any changes to the simulation in a database using entity framework.

# Thread one 
adds 30 patients with random name, age,id number and so on. Only run once in the kod and shuts down when it has added 30 patients.

# Thread two 
Sorts the patients to IVA and Sanatorium, oldest get to sanatorium, sickets to IVA.
Runs every 5 seconds.

# Thread three 
updates the symtoms of the patients, it runs every 3 seconds.

# Thread four 
sorts the patients in to afterlife and recovered.
If the patients symtom level is above 9, it gets sorted to afterlife.
If its below 1 it gets sorted to recovered. We also log recovered 
and patients sent to afterlife in two local files, with datetime, fullname, age and any other personal info.

