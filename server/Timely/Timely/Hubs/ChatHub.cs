using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using Repository.Entity;
using Service.Dtos;
using Service.Interfaces;
using System.Threading.Tasks;

namespace Timely.Hubs
{
    public class ChatHub : Hub
    {
        // שליחת הודעה לכל המשלוחנים באזור מסוים
        public async Task SendMessageToArea(string area, string orderJson)
        {
            var newOrder = JsonConvert.DeserializeObject<Order>(orderJson);
            await Clients.Group(area).SendAsync("ReceiveMessage", newOrder);
            Console.WriteLine($"Message sent to area: {area}");
        }

        // התראה שמשימה נלקחה באזור מסוים
        public async Task NotifyTaskTaken(int orderId, string area)
        {
            await Clients.Group(area).SendAsync("TaskTaken", orderId);
            Console.WriteLine($"Task {orderId} taken in area: {area}");
        }

        // הצטרפות לקבוצה של האזור
        public async Task JoinGroup(string area)
        {
            try
            {
                Console.WriteLine($"Joining group: {area}");
                await Groups.AddToGroupAsync(Context.ConnectionId, area);
                Console.WriteLine($"Successfully joined group: {area}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error joining group: {area} - {ex.Message}");
                await Clients.Caller.SendAsync("Error", "Failed to join the group.");
            }
        }

        // יציאה מהקבוצה של האזור
        public async Task LeaveGroup(string area)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, area);
            Console.WriteLine($"Left group: {area}");
        }

        // לאירועים של ניתוק
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            Console.WriteLine($"Connection {Context.ConnectionId} disconnected.");
            await base.OnDisconnectedAsync(exception);
        }
    }

}
