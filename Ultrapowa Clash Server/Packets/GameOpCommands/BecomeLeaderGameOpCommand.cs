using UCS.Core;
using UCS.Core.Network;
using UCS.Logic;
using UCS.Packets.Messages.Server;

namespace UCS.Packets.GameOpCommands
{
    internal class BecomeLeaderGameOpCommand : GameOpCommand
    {
        readonly string[] m_vArgs;

        public BecomeLeaderGameOpCommand(string[] args)
        {
            m_vArgs = args;
            SetRequiredAccountPrivileges(5);
        }

        public override async void Execute(Level level)
        {
            if (level.GetAccountPrivileges() >= GetRequiredAccountPrivileges())
            {
                var clanid = level.GetPlayerAvatar().GetAllianceId();
                if (clanid != 0)
                {
                    Alliance _Alliance = await ObjectManager.GetAlliance(level.GetPlayerAvatar().GetAllianceId());

                    foreach (var pl in _Alliance.GetAllianceMembers())
                    {
                        if (pl.GetRole() == 2)
                        {
                            pl.SetRole(4);
                            break;
                        }
                    }
                    level.GetPlayerAvatar().SetAllianceRole(2);
                }
            }
            else
            {
                var p = new GlobalChatLineMessage(level.GetClient());
                p.SetChatMessage("GameOp command failed. Access to Admin GameOP is prohibited.");
                p.SetPlayerId(0);
                p.SetLeagueId(22);
                p.SetPlayerName("UCS Bot");
                PacketProcessor.Send(p);
            }
        }
    }
}
