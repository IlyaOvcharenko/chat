using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace Web.Hubs
{
    public class ChatHub : Hub
    {
        public void SendMessageQueue(List<Message> messages)
        {        
            foreach (var message in messages)
            {
                message.Author = Context.User.Identity.Name;
                message.DateTime = DateTime.Now;
            }
            Clients.All.addMessages(messages);
        }
    }

    public class Message
    {
        public string Author { get; set; }

        public string Text { get; set; }

        public DateTime? DateTime { get; set; }
    }
}