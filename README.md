Footnotes, a sample for Microsoft Cognitive Services
====================================
Footnotes is a simple bot written using the [Entity Linking Intelligence Service](<https://www.microsoft.com/cognitive-services/en-us/entity-linking-intelligence-service>) and deployed through [Bot Framework](<https://www.botframework.com/>). It listens to a conversations and adds wikipedia links as a "footnote" every time it recognizes a concept.

Getting started
===============
1. Get a key for Entity Linking from [Microsoft.com/Cognitive](<https://www.microsoft.com/cognitive-services/en-us/sign-up>). Put it into project file Utilities/Keys const ELIS
2. Build and deploy the project to your local host
3. Now you can test footnotes in the web chat on the page, or through the Bot Framework Emulator
 -   URL: https:/<local host>/api/messages
 -   App id: footnote
 -   App secrect: cdf9f800627b47b2accf21f9a182e624


Navigating the project
===============
**Reading & responding to messages with ELIS:** Controllers/MessagesController.cs  
**Editing the website & webchat:** /default.htm

Remember! If you want to actually deploy the bot on new channels, you'll need to make a bot on [BotFramework.com](<https://www.botframework.com/>) and connect this project to it. In order to do that, make your bot and then update the following with your new app id & secret:  
- Web.config <appsettings> tag
- default.htm WebChat iframe src
- emulator app id & secret


Contributing
============
We welcome contributions. Feel free to file issues & pull requests on the repo and we'll address them as we can.

For questions, feedback, or suggestions about Microsoft Cognitive Services, reach out to us directly on our [Cognitive Services UserVoice Forum](<https://cognitive.uservoice.com>).



License
=======
All Microsoft Cognitive Services SDKs and samples are licensed with the MIT License. For more details, see [LICENSE](</LICENSE.md>).
