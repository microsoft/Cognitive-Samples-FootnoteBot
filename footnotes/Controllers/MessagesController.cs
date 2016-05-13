using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Connector.Utilities;
using Newtonsoft.Json;
using System.Web.Configuration;
using footnotes.Utilities;

//Step 1: Add the Entity Linking SDK
using Microsoft.ProjectOxford.EntityLinking;
using Microsoft.ProjectOxford.EntityLinking.Contract;


namespace footnotes
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        /// <summary>
        /// POST: api/Messages
        /// Receive a message from a user and reply to it
        /// </summary>
        public async Task<Message> Post([FromBody]Message message)
        {
            if (message.Type == "Message")
            {

                //Set up return message
                Message replyMessage = null;

                //Step 2: Invoke ELIS through helper method
                var entityLinkResult = await getEntityLink(message.Text);


                //Step 3: Create our reply 
                if (entityLinkResult != null & entityLinkResult.Length > 0)
                    replyMessage = message.CreateReplyMessage($"Footnotes: {entityLinkResult}");


                //Step 4: Profit!
                return replyMessage;
            }
            else
            {
                return HandleSystemMessage(message);
            }
        }


        public static async Task<string> getEntityLink(string messageBody)
        {
            //Set up response
            var response = "";

            try
            {
                //Step 2a: Call into the Entity Linking API;
                EntityLink[] entityResult;
                var ELISClient = new EntityLinkingServiceClient(Keys.ELIS);
                entityResult = await ELISClient.LinkAsync(messageBody);

                //Step 2b: Store the results as a comma separated list of wikipedia URLs
                response += string.Join(", ", entityResult.Select(i => "https://en.wikipedia.org/wiki/" + i.WikipediaID.Replace(" ", "_")).ToList());

            }
            catch (Exception ex)
            {
                response = ex.ToString();
            }

            //Return response
            return response;
        }


        private Message HandleSystemMessage(Message message)
        {
            if (message.Type == "Ping")
            {
                Message reply = message.CreateReplyMessage();
                reply.Type = "Ping";
                return reply;
            }
            else if (message.Type == "DeleteUserData")
            {
                // Implement user deletion here
                // If we handle user deletion, return a real message
            }
            else if (message.Type == "BotAddedToConversation")
            {
            }
            else if (message.Type == "BotRemovedFromConversation")
            {
            }
            else if (message.Type == "UserAddedToConversation")
            {
            }
            else if (message.Type == "UserRemovedFromConversation")
            {
            }
            else if (message.Type == "EndOfConversation")
            {
            }

            return null;
        }
    }
}