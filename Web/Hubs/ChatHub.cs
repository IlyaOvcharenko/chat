using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;
using BusinessLogic;
using Data;
using Microsoft.AspNet.SignalR.Hubs;

namespace Web.Hubs
{
    public class ChatHub : Hub
    {
        private IUserActivityService userActivityService;
        private IUserService userService;
        public ChatHub(IUserActivityService userActivityService, IUserService userService)
        {
            this.userActivityService = userActivityService;
            this.userService = userService;
        }
        public static IDictionary<string, HashSet<string>> Connections = new ConcurrentDictionary<string, HashSet<string>>();

        public void SendMessageQueue(List<Message> messages)
        {
            if(!Context.User.Identity.IsAuthenticated)
               return;
            foreach (var message in messages)
            {
                message.Author = Context.User.Identity.Name;
                message.DateTime = DateTime.Now;
            }
            Clients.All.addMessages(messages);
            RegisterActivity(Context, messages.Count);
        }

        public override Task OnConnected()
        {
            if (!Context.User.Identity.IsAuthenticated)
                return base.OnConnected();
            if (!Connections.ContainsKey(Context.User.Identity.Name) )
            {
                Connections.Add(new KeyValuePair<string, HashSet<string>>(Context.User.Identity.Name,
                    new HashSet<string> {Context.ConnectionId}));
                Clients.Others.userConnected(Context.User.Identity.Name);
            }
            else
            {
                if(Connections[Context.User.Identity.Name] == null)
                    Connections[Context.User.Identity.Name] = new HashSet<string>();
                Connections[Context.User.Identity.Name].Add(Context.ConnectionId);
            }
            Clients.All.changeUserList(Connections.Keys.OrderBy(c => c));
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            if (!Connections.ContainsKey(Context.User.Identity.Name) || !Connections[Context.User.Identity.Name].Contains(Context.ConnectionId))
                return base.OnDisconnected(stopCalled);
            Connections[Context.User.Identity.Name].Remove(Context.ConnectionId);
            if (Connections[Context.User.Identity.Name].Count == 0)
            {
                Connections.Remove(Context.User.Identity.Name);
                Clients.Others.userDisconnected(Context.User.Identity.Name);
            }
            Clients.All.changeUserList(Connections.Keys.OrderBy(c => c));
            return base.OnDisconnected(stopCalled);
        }

        private void RegisterActivity(HubCallerContext context, int messageCount)
        {
            string ipAddress;
            object ipAddressObject;
            Context.Request.Environment.TryGetValue("server.RemoteIpAddress", out ipAddressObject);
            ipAddress = ipAddressObject != null ? (string) ipAddressObject : string.Empty;
            var user = userService.GetUserByLogin(context.User.Identity.Name);
            userActivityService.RegisterActivity(new UserActivity {ClientIpAddress = ipAddress , DateTime = DateTime.Now, Date = DateTime.Today, MessageCount = messageCount, UserId = user?.Id ?? 0 });
        }
    }

    public class Message
    {
        public string Author { get; set; }

        public string Text { get; set; }

        public DateTime? DateTime { get; set; }
    }
}