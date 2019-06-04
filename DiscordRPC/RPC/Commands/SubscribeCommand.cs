using DiscordRPC.RPC.Payload;

namespace DiscordRPC.RPC.Commands
{
	internal class SubscribeCommand : ICommand
	{
		public ServerEvent Event { get; }
		public bool IsUnsubscribe { get; }

        public SubscribeCommand(ServerEvent @event, bool isUnsubscribe)
        {
            Event = @event;
            IsUnsubscribe = isUnsubscribe;
        }

        public PayloadBase PreparePayload(long nonce)
		{
			return new EventPayload(nonce)
			{
				Command = IsUnsubscribe ? Command.Unsubscribe : Command.Subscribe,
				Event = Event
			};
		}
	}
}
