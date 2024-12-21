using Microsoft.AspNetCore.SignalR;
using SignalR_Learn.Interfaces;

namespace SignalR_Learn.Hubs
{
    public class MyHub : Hub<IMessageClient>// <IMessageClient> ile kullanılacak fonksiyonları IMessageClient interfacesinden al.
    {
        static List<string> clients = new List<string>();
        public async Task SendMessageAsync(string message)
        {
            //Bağlı olan tüm clientlara mesaji göndermeyi sağlar.
            //await Clients.All.SendAsync("recieverMessage",message);
            //Ekstra İşlemler....
        }
         
        //Bağlantı olayları Loglama için hayati öneme sahiptir !!!
        public override async Task OnConnectedAsync() //Sisteme bir bağlantı gerçekleştiği zaman tetiklenecek.
        {
            //Hub'a bağlantı gerçekleştiren clientlara sistem tarafından verilen unique değerlerdir. Clientları birbirinden ayırır. (ConnectionId)
             
           /* await Clients.All.SendAsync("userJoined", Context.ConnectionId);*/ //Tüm clientlara giriş yapan kullanıcının id'sini gönderir.
           await Clients.All.UserJoined(Context.ConnectionId);

            #region Tüm clientları listeleye ekleme
            clients.Add(Context.ConnectionId);
            //await Clients.All.SendAsync("clients", clients); //normal tanımlama
           await Clients.All.Clients(clients); // IMessageClient ile tanımlama
            #endregion
        }

        public override async Task OnDisconnectedAsync(Exception? exception) //Sistemden bir bağlantı koptuğunda tetiklenecek.
        {
            //await Clients.All.SendAsync("userLeft", Context.ConnectionId); //normal tanımlama
            await Clients.All.UserLeaved(Context.ConnectionId); // IMessageClient ile tanımlama

            #region Tüm clientları listeden çıkarma
            clients.Remove(Context.ConnectionId);
            //await Clients.All.SendAsync("clients", clients);
            #endregion
        }
    }
}
