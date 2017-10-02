# SeniorProject first Deliverable 
Senior Project first deliverable 

# Tables
Tables in the application 

# Schools
Will hold the basic school data 

Attributes

_ID : String
NAME: String
PHONE: String
EMAIL: String
ADDRESS: String
ENROLLEMNT: Int




# School's Programs
Relational Table, stores which schools have what programs
in a way that allows for schools to have unlimited programs.

Attributes

_ID: String
SCHOOL_ID: String   // this will be the schools ID from the school table
PROGRAM_ID: String  //This will be the programs ID from the program table

# Programs
Tables that stores the possible programs a school may have.
Athletics, Performing Arts, Advanced Placement etc.

Attributes

_ID: String
PROGRAM_NAME: String
PROGRAM_DESCRIPTION: String

# photos
Table to store Photos 

Attributes 

_ID: String
SCHOOL_ID: String
PICTURE: Blob
