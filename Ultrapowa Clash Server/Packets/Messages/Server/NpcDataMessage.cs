﻿using System;
using System.Collections.Generic;
using System.Text;
using UCS.Core;
using UCS.Helpers;
using UCS.Logic;
using UCS.Packets.Messages.Client;

namespace UCS.Packets.Messages.Server
{
    // Packet 24133
    internal class NpcDataMessage : Message
    {
        public NpcDataMessage(Packets.Client client, Level level, AttackNpcMessage cnam) : base(client)
        {
            SetMessageType(24133);
            Player = level;
            LevelId = cnam.LevelId;
            JsonBase = ObjectManager.NpcLevels[LevelId];
        }

        public override async void Encode()
        {
            try
            {
                ClientHome ownerHome = new ClientHome(Player.GetPlayerAvatar().GetId());
                ownerHome.SetShieldTime(Player.GetPlayerAvatar().GetShieldTime);
                ownerHome.SetProtectionTime(Player.GetPlayerAvatar().GetProtectionTime);
                ownerHome.SetHomeJSON(JsonBase);

                List<byte> data = new List<byte>();

                data.AddInt32(0);
                data.AddInt32((int)Player.GetTime().Subtract(new DateTime(1970, 1, 1)).TotalSeconds);
                data.AddRange(ownerHome.Encode());
                data.AddRange(await Player.GetPlayerAvatar().Encode());
                data.AddInt32(LevelId);

                Encrypt(data.ToArray());
            } catch (Exception) { }
        }

        public string JsonBase { get; set; }
        public int LevelId { get; set; }
        public Level Player { get; set; }
    }
}