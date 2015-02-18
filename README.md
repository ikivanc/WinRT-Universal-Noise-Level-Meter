WinRT-Universal-Noise-Level-Meter
=================================

Winrt Universal application which measures the ambient noise levels using the microphone and save existing noise level in Microsoft Azure, using Azure Mobile Services.


<b>Description</b><br>
In this demo script we'll build a Universal (Windows 8.1 + Windows Phone 8.1) application which measures the ambient noise levels using the microphone. Every 3 seconds these apps will save existing noise level in Microsoft Azure, using Azure Mobile Services.

I modified below winrt noise level sample for windows 8 and upgrated to win8.1 universal app and created a new Windows phone version. https://github.com/richorama/WinRTdB 

Windows Phone Application looks like below and works with Windows 8.1 System.
 

Windows 8 application looks like below and works with Windows 8.1 Operating System.
 

<b>Preperation / Pre-Requests</b><br>

-	Visual Studio 2013 Update 2 or above (for Universal App development)
-	Microsoft Azure account (for Azure Mobile Services)
-	Download Sample Windows Store Universal App from repository.

<b>Creating a backend using Azure Mobile Services</b><br>
1.	First of all in azure portal let's create a mobile services
 
 
2.	Give a name for your mobile service url and select your database type, region of your service where it'll be hosted and backend type if you want to modify, you can pick C# or Javascript.
 
3.	Give a name to your database, and choose a server region.
 
 
 
4.	After that your service will be ready!
 
 

5.	Click on it and go to dashboard for more details, talk about platform choices they can create a new application from scratch or they can tie this backend into their existing applications. After that download C# backend from “Connect an existing windows or windows phone application” 

 
 
6.	Open Azure Mobile Services solution after downloading from previous step. Solution will look like below, and make sure you all files in it.
 

7.	This is your mobile service on your pc and you need to publish this service on Azure. In order to make it happen right click on the project and Click Publish.
 
 
8.	When you click publish you’ll have a pop-up for publish options. First you need to select your Azure mobile services target, click on first item “Microsoft Azure Mobile Services” button.
 
 

9.	You need to login with your Azure account in order to see your mobile services in dropdown list. After that select your Created azure Mobile Services.
 
 


10.	When you selec your service your connection details will be automatically filled in publish step. Such as your host server, site name, username and password in order to deploy your solution.



11.	 Congratulations your azure mobile service is up and running. Now we can move to windows store application development.
 
Editing Universal Windows Store Application

12.	Open the noise level meter universal application solution from here. Go to shared folder and open “App.xaml.cs” file


13.	Change below connection end-point with your new Azure Mobile Services end-point. And run this application on both Windows Phone and Windows 8.1. Tell the differences on both platforms they’re sharing DataModel, all images and AudioamplitudeStream class. Main pages are differen on different platforms because there extra modules for dashboard on windows 8. 

Try to debug the application and discover how it works.

14.	Application is very simple and easy to use you can see the screen shot of it below. There is a progress bar which stands for dB level, value and dashboard button to check real time results if you’re using couple of devices.
 

15.	Whenever you click on Dashboard button, you’ll see list of evetns on the right side. You can hide by clicking on button. There will be value, device type and time. Windows device will be labeled as “Windows 8.1” and windows Phone wil appear as “Windows Phone 8.1”
 

16.	Through web, you can browser your DB Server and show meatedB database and show your real time values after you finish the demo.
 
 
And Have fun with your demo! :) Feel free to modify and give feedback to ikivanc@microsoft.com 

Thanks! 
