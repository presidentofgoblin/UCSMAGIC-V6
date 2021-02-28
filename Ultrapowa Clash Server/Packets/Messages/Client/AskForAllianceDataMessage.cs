﻿using System;
using System.IO;
using UCS.Core;
using UCS.Core.Network;
using UCS.Helpers;
using UCS.Logic;
using UCS.Packets.Messages.Server;

namespace UCS.Packets.Messages.Client
{
    // Packet 14302
    internal class AskForAllianceDataMessage : Message
    {
        long m_vAllianceId;

        public AskForAllianceDataMessage(Packets.Client client, PacketReader br) : base(client, br)
        {
        }

        public override void Decode()
        {
            using (PacketReader br = new PacketReader(new MemoryStream(GetData())))
            {
                m_vAllianceId = br.ReadInt64WithEndian();
            }
        }

        public override async void Process(Level level)
        {
            try
            {
                Alliance alliance = await ObjectManager.GetAlliance(m_vAllianceId);
                if (alliance != null)
                    PacketProcessor.Send(new AllianceDataMessage(Client, alliance));
            } catch (Exception) { }
        }
    }
}