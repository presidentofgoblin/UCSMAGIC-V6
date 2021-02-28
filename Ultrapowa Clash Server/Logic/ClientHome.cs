using System.Collections.Generic;
using UCS.Helpers;

namespace UCS.Logic
{
    internal class ClientHome : Base
    {
        readonly long m_vId;
        string village;
        int m_vShieldTime;
        int m_vProtectionTime;

        public ClientHome() : base(0)
        {
        }

        public ClientHome(long id) : base(0)
        {
            m_vId = id;
        }

        public override byte[] Encode()
        {
            string events = @"{""events"":[{""id"":,""version"":1,""visibleTime"":""20161219T080000.000Z"",""startTime"":""20161220T100000.000Z"",""endTime"":""20161222T100000.000Z"",""boomboxEntry"":"",""eventEntryName"":""TroopDiscountHog"",""inboxEntryId"":0,""notification"":""TID_LOCAL_NOTIFICATION_EVENT_TROOP_DISCOUNT"",""functions"":[{""name"":""TroopDiscount"",""parameters"":[""4000011"",""10""]},{""name"":""UseTroop"",""parameters"":[""4000011"",""3"",""20"",""30"",""300""]}]},{""id"":2,""version"":1,""visibleTime"":""20161219T080000.000Z"",""startTime"":""20161222T100000.000Z"",""endTime"":""20161230T100000.000Z"",""boomboxEntry"":"",""eventEntryName"":""CollectorBoost"",""inboxEntryId"":0,""notification"":""TID_LOCAL_NOTIFICATION_EVENT_ALL_COLLECTORS_BOOST"",""functions"":[{""name"":""BuildingBoost"",""parameters"":[""1000004"",""1""]},{""name"":""BuildingBoost"",""parameters"":[""1000002"",""1""]},{""name"":""BuildingBoost"",""parameters"":[""1000023"",""1""]}]},{""id"":3,""version"":1,""visibleTime"":""20161219T080000.000Z"",""startTime"":""20161222T100000.000Z"",""endTime"":""20170106T100000.000Z"",""boomboxEntry"":"""",""eventEntryName"":""ChristmasSpell"",""inboxEntryId"":0,""notification"":""TID_LOCAL_NOTIFICATION_EVENT_SPECIAL_SPELL"",""functions"":[{""name"":""EnableSpell"",""parameters"":[""26000006""]}]}]}""";

            List<byte> data = new List<byte>();
            data.AddInt64(m_vId);

            data.AddInt32(m_vShieldTime); // Shield
            data.AddInt32(m_vProtectionTime); // Protection

            data.AddInt32(0);
            data.Add(1);
            data.AddCompressedString(village);
            data.Add(1);
            data.AddCompressedString(events);
            return data.ToArray();
        }

        public string GetHomeJSON() => village;

        public void SetHomeJSON(string json) => village = json;

        public void SetShieldTime(int seconds) => m_vShieldTime = seconds;

        public int GetShieldTime() => m_vShieldTime;

        public void SetProtectionTime(int time) => m_vProtectionTime = time;

        public int GetProtectionTime() => m_vProtectionTime;
    }
}
