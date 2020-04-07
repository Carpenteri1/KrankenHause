# KrankenHause
Did some work with threading and delegates. There are 4 different threads who should work together. Simulate a hospital and logs any changes to the simulation in a database using entity framework

Thread one adds 30 patients with random name, age,id number and so on.

Thread two sorts the patients to IVA and Sanatorium, oldest get to sanatorium, sickets to IVA'

Thread three updates the symtoms of the patients.

Thread four sorts the patients in to afterlife and recovered.

