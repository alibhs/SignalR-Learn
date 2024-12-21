using Microsoft.AspNetCore.SignalR;
using SignalR_Learn.Hubs;

namespace SignalR_Learn.Business
{
    public class MyBusiness 
    {
        readonly IHubContext<MyHub> _hubContext; //IHubContext interface üzerinden myhub'ı bu controllerda tetikleyebilicez. 

        public MyBusiness(IHubContext<MyHub> hubContext)
        {
            _hubContext=hubContext;
        }

        public async Task SendMessageAsyncAction(string message) // ilgili fonksiyonu burda tanımlayıp myhub'daymış gibi tetikelyebilirim.
        {
            //Bağlı olan tüm clientlara mesaji göndermeyi sağlar.
            await _hubContext.Clients.All.SendAsync("recieverMessage", message);
            //Ekstra İşlemler....
        }

    }
}
