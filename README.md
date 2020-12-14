# SolrSetup
Download the AddOn for SolrWayback here: https://github.com/MadsGreen/SolrSetup/releases/tag/SolrSetup_1.0
If you dont want to build the AddOn youself, download the Addon above and extract it in your SolrWayback_package_X.X.X folder like this:

![Screenshot](https://i.imgur.com/cqeX8KE.png)

This is an AddOn to SolrWayback https://github.com/netarchivesuite/solrwayback
SolrSetup is an GUI for the SolrWayback install guide. This is done to ease the install of SolrWayback. 
All cmd commands for the installation and setup are automated. 



# Build
To build the AddOn you need Visual Studio 19 or newer. 
1. Download the project and open the .sln project file in VS19. 
2. Press F5 to debug the project and close the pop-up debug of SolrSetup
3. Navigate to the debug folder of the project "WHERE_YOU_PLACED_THE_PROJECT\GUNAISARETARD\bin\Debug\"
4. Copy all files within the "Debug" folder and nagivtage to your "SolrWayback_package_X.X.X" folder.
5. If you do not have an "Addon" folder here, create one and paste all files from "Debug" here. If you do have the "Addon" folder paste the files in it. 
6. In the project folder you have an "Indexing" folder with a batch (.bat) file named "Indexing". Copy the batch file in the "Addon" folder. 
7. In the SolrWayback_package_X.X.X\Addon you should now have the following files:
  - Indexing.bat
  - Guna.UI2.dll
  - SolrSetup.exe
  - SolrSetup.exe.config
  - SolrSetup.pdb
8. Run SolrSetup.exe and you are good to go

# Install
To install SolrWayback run the SolrSetup.exe and press "Install"
This copies the files of the propertyfolder into your user folder and creats Java_HOME, JAVA_JRE & CATALINA_HOME enviroment variables.

# Start SolrWayback 
To start up SolrWayback, press the power icon in the middle of the UI cirkel. 
This opens a TomCat window, which is your local host of SolrWayback. It also run a hidden cmd localhost which is your netarchive localhost.

These windows has to remain open in order for SolrWayback to work.

# Index
In order to index warc files TomCat has to be active. 
When you select multiple warc files, it will index the files one by one. This can take a while depeding on the size of the files.
After indexing SolrSetup will restart both local hosts and you will experience TomCat and CMD windows open and close. 
You will have one TomCat window open in the end and one CMD. Keep these running (do not close down) in order for SolrWayback to work. 

# Clearing indexing
If press "Clear index" it will delete all of your previous indexed files. 

# Shutdown
If you shutdown SolrSetup by press the "X" (top right) it will also shutdown your localhosts. Please close only when done using SolrWayback. 
It will not clear any indexed files. 

# Other
If you experience launch problems or difficulties, please try to restart the application. 
There are bugs in the application, sorry but its not made by professionals. 
