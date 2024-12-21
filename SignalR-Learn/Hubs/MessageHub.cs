using Microsoft.AspNetCore.SignalR;

namespace SignalR_Learn.Hubs
{
    public class MessageHub : Hub
    {
        //public async Task SendMessageAsync(string message,IEnumerable<string> connectionIds)
        //public async Task SendMessageAsync(string message,string groupName) //Group için
        //public async Task SendMessageAsync(string message,string groupName, IEnumerable<string> connectionIds) //Group  Except için
        public async Task SendMessageAsync(string message,List<string> groups) //Groups için
        {
            #region Client Türleri
            #region Caller
            //Sadece server'a bildirim gönderen client ile ilteişimi kurar
            await Clients.Caller.SendAsync("recieveMessage", message);
            #endregion

            #region Other
            //Server'a bildirim gönderen client dışındaki Server'a bağlı tüm clientlarla iletişim kurar. 
            await Clients.Others.SendAsync("recieveMessage", message);
            #endregion

            #region All
            //Server'a bağlı tüm clientlarla iletişim kurar.
            await Clients.All.SendAsync("recieveMessage", message);
            #endregion
            #endregion

            #region Hub Clients Metodları

            #region AllExcept
            //Belirtilen client hariç server'a bağlı tüm clientlara bildiride bulunur.
            Clients.AllExcept(connectionIds).SendAsync("recieveMessage",message);
            #endregion
            #region Client
            //Sadece belirlenen client'a bildiride bulunur.
            Clients.Client(connectionIds.First()).SendAsync("recieveMessage", message);
            #endregion
            #region Clients
            //Sadece belirlenen clientlara'a bildiride bulunur.
            Clients.Clients(connectionIds).SendAsync("recieveMessage", message);
            #endregion
            #region Group
            //Belirtilen grouptaki tüm clientlara bildiride bulunur.
            //Önce gruplar oluşuturulur ve ardından clientlar grouplara subsc. edilir.
            Clients.Group(groupName).SendAsync("reciveMesage", message);
            #endregion
            #region GroupExcept
            //Belirtilen group dışındaki tüm clientlara bildiride bulunur.
            Clients.GroupExcept(groupName, connectionIds).SendAsync("reciveMesage", message); // bu grup namedeki bu idler dışındakilere gönder.
            #endregion
            #region Groups
            //Belirtilen gruplara bildiride bulunur.
            Clients.Groups(groups).SendAsync("recieveMessages", message);
            #endregion
            #region OthersInGroup
            //Bildiride bulunan client harici tüm gruplara bildiride bulunur.
            Clients.OthersInGroup(groupName).SendAsync("reciveMesage", message); // bu grup namedeki bu idler dışındakilere gönder.
            #endregion
            #region User
            //Auth olmuş kullanıcıya bildiride bulunur
            #endregion
            #region Users
            //Auth olmuş kullanıcılara bildiride bulunur.
            #endregion

            #endregion

        }

        public override async Task OnConnectedAsync()
        {
            await Clients.Caller.SendAsync("getConnectionId",Context.ConnectionId);

        }

        public async Task addGroup(string connectionId, string groupName) //Clienttan grup ismi almamızı sağlar
        {
           await Groups.AddToGroupAsync(connectionId, groupName);
        }
    }
}
