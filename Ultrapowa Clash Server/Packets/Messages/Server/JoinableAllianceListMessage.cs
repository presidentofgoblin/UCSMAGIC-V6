using System.Collections.Generic;
using System.Threading.Tasks;
using UCS.Helpers;
using UCS.Logic;

namespace UCS.Packets.Messages.Server
{
    // Packet 24304
    internal class JoinableAllianceListMessage : Message
    {
        List<Alliance> m_vAlliances;

        public JoinableAllianceListMessage(Packets.Client client) : base(client)
        {
            SetMessageType(24304);
            m_vAlliances = new List<Alliance>();
        }

        public override void Encode()
        {
            List<byte> pack = new List<byte>();
            pack.AddInt32(m_vAlliances.Count);

            foreach(Alliance alliance in m_vAlliances)
            {               
                if(alliance != null)
                {
                    pack.AddRange(alliance.EncodeFullEntry());
                }
            }

            Encrypt(pack.ToArray());
        }

        public void SetJoinableAlliances(List<Alliance> alliances)
        {
            m_vAlliances = alliances;
        }
    }
}
