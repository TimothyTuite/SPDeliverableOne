# SeniorProject first Deliverable 
Senior Project first deliverable 

# Tables
Tables in the application 

# Schools
Will hold the basic school data 

Attributes<br />

_ID : String <br />
EDITORS_ID : String         //THE ID OF the USER PROFILE selected to edit the school<br />
NAME: String <br />
PHONE: String<br />
EMAIL: String<br />
ADDRESS: String<br />
ENROLLEMNT: Int<br />




# School's Programs
Relational Table, stores which schools have what programs
in a way that allows for schools to have unlimited programs.

Attributes<br />

_ID: String
SCHOOL_ID: String   // this will be the schools ID from the school table<br />
PROGRAM_ID: String  //This will be the programs ID from the program table<br />

# Programs
Tables that stores the possible programs a school may have.
Athletics, Performing Arts, Advanced Placement etc.

Attributes<br />

_ID: String<br />
PROGRAM_NAME: String<br />
PROGRAM_DESCRIPTION: String<br />

# photos
Table to store Photos 

Attributes <br />

_ID: String<br />
SCHOOL_ID: String<br />
PICTURE: Blob<br />
