﻿using System;
using System.Collections.Generic;
using UCS.Helpers;
using UCS.Logic;

namespace UCS.Packets.Messages.Server
{
    // Packet 24107
    internal class EnemyHomeDataMessage : Message
    {
        public EnemyHomeDataMessage(Packets.Client client, Level ownerLevel, Level visitorLevel) : base(client)
        {
            SetMessageType(24107);
            m_vOwnerLevel = ownerLevel;
            m_vVisitorLevel = visitorLevel;
        }

        public override async void Encode()
        {
            try
            {
                List<byte> data = new List<byte>();
                ClientHome ch = new ClientHome(m_vOwnerLevel.GetPlayerAvatar().GetId());
                ch.SetShieldTime(m_vOwnerLevel.GetPlayerAvatar().GetShieldTime);
                ch.SetHomeJSON(m_vOwnerLevel.SaveToJSON());
                ch.SetProtectionTime(m_vOwnerLevel.GetPlayerAvatar().GetProtectionTime);

                data.AddInt32((int)TimeSpan.FromSeconds(100).TotalSeconds);
                data.AddInt32(-1);
                data.AddInt32((int)Client.GetLevel().GetTime().Subtract(new DateTime(1970, 1, 1)).TotalSeconds);
                data.AddRange(ch.Encode());
                data.AddRange(await m_vOwnerLevel.GetPlayerAvatar().Encode());
                data.AddRange(await m_vVisitorLevel.GetPlayerAvatar().Encode());
                data.AddInt32(3);
                data.AddInt32(0);
                data.Add(0);

                Encrypt(data.ToArray());
            } catch (Exception) { }
        }

        readonly Level m_vOwnerLevel;
        readonly Level m_vVisitorLevel;
    }
}
