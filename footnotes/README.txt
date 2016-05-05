********************
Getting started
********************
1. Get a key for Entity Linking from Microsoft.com/Cognitive. Put it into Utilities/Keys const ELIS
2. Build and deploy the project to your local host
3. Now you can test footnotes in the web chat on the page, or through the Bot Framework Emulator
		URL: https://<local host>/api/messages
		App id: footnote
		App secrect: cdf9f800627b47b2accf21f9a182e624


********************
Navigating the project
********************
Reading & responding to messages with ELIS: Controllers/MessagesController.cs
Editing the website & webchat: /default.htm

To connect up to your bot instead of footnotes, update:
* Web.config <appsettings> tag
* default.htm WebChat iframe src
* emulator app id & secret