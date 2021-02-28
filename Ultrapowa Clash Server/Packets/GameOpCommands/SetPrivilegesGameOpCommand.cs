﻿using System;
using UCS.Core;
using UCS.Logic;

namespace UCS.Packets.GameOpCommands
{
    internal class SetPrivilegesGameOpCommand : GameOpCommand
    {
        readonly string[] m_vArgs;

        public SetPrivilegesGameOpCommand(string[] args)
        {
            m_vArgs = args;
            SetRequiredAccountPrivileges(4);
        }

        public override async void Execute(Level level)
        {
            if (level.GetAccountPrivileges() >= GetRequiredAccountPrivileges())
            {
                if (m_vArgs.Length >= 3)
                {
                    try
                    {
                        var id = Convert.ToInt64(m_vArgs[1]);
                        var accountPrivileges = Convert.ToByte(m_vArgs[2]);
                        var l = await ResourcesManager.GetPlayer(id);
                        if (accountPrivileges < level.GetAccountPrivileges())
                        {
                            if (l != null)
                            {
                                l.SetAccountPrivileges(accountPrivileges);
                            }
                            else
                            {
                                //Debugger.WriteLine("SetPrivileges failed: id " + id + " not found");
                            }
                        }
                        else
                        {
                            //Debugger.WriteLine("SetPrivileges failed: target privileges too high");
                        }
                    }
                    catch 
                    {
                        ////Debugger.WriteLine("SetPrivileges failed with error: " + ex);
                    }
                }
            }
            else
            {
                SendCommandFailedMessage(level.GetClient());
            }
        }
    }
}