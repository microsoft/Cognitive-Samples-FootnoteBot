using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.Bot.Connector;
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
        public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
        {
            if (activity.Type == ActivityTypes.Message)
            {

                //Set up Bot Framework Connector for passing messages
                ConnectorClient connector = new ConnectorClient(new Uri(activity.ServiceUrl));

                //Step 2: Invoke ELIS through helper method with user data
                var entityLinkResult = await getEntityLink(activity.Text);

                Activity replyMessage = activity.CreateReply(null);

                // Post expects an await variable
                await connector.Conversations.ReplyToActivityAsync(replyMessage);

                //Step 3: Create our reply if a result was found
                if (entityLinkResult != null & entityLinkResult.Length > 0)
                    replyMessage = activity.CreateReply($"Footnotes: {entityLinkResult}");
                await connector.Conversations.ReplyToActivityAsync(replyMessage);

            }
            else
            {
                HandleSystemMessage(activity);
            }

            //Step 4: respond with article if found or nothing if one is not found
            var response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
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


        private Activity HandleSystemMessage(Activity message)
        {
            if (message.Type == ActivityTypes.DeleteUserData)
            {
                // Implement user deletion here
                // If we handle user deletion, return a real message
            }
            else if (message.Type == ActivityTypes.ConversationUpdate)
            {
                // Handle conversation state changes, like members being added and removed
                // Use Activity.MembersAdded and Activity.MembersRemoved and Activity.Action for info
                // Not available in all channels
            }
            else if (message.Type == ActivityTypes.ContactRelationUpdate)
            {
                // Handle add/remove from contact lists
                // Activity.From + Activity.Action represent what happened
            }
            else if (message.Type == ActivityTypes.Typing)
            {
                // Handle knowing tha the user is typing
            }
            else if (message.Type == ActivityTypes.Ping)
            {
            }

            return null;
        }
    }
}