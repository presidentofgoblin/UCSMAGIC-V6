using System;
using System.Collections.Generic;
using UCS.Helpers;
using UCS.Logic;

namespace UCS.Packets.Messages.Server
{
    // Packet 24334
    internal class AvatarProfileMessage : Message
    {
        Level m_vLevel;

        public AvatarProfileMessage(Packets.Client client) : base(client)
        {
            SetMessageType(24334);
        }

        public override async void Encode()
        {
            try
            {
                var pack = new List<byte>();
                var ch = new ClientHome(m_vLevel.GetPlayerAvatar().GetId());
                ch.SetHomeJSON(m_vLevel.SaveToJSON());

                pack.AddRange(await m_vLevel.GetPlayerAvatar().Encode());
                pack.AddCompressedString(ch.GetHomeJSON());

                pack.AddInt32(m_vLevel.GetPlayerAvatar().GetDonated()); //Donated
                pack.AddInt32(m_vLevel.GetPlayerAvatar().GetReceived()); //Received
                pack.AddInt32(0); //War Cooldown

                pack.AddInt32(0); //Unknown
                pack.Add(0); //Unknown


                Encrypt(pack.ToArray());
            } catch (Exception) { }
        }

        public void SetLevel(Level level)
        {
            m_vLevel = level;
        }
    }
}
