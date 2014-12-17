#region LGPL License
/*----------------------------------------------------------------------------
* This file (CKReleaser\GUI\ActivityMonitorListItemClient.cs) is part of CiviKey. 
*  
* CiviKey is free software: you can redistribute it and/or modify 
* it under the terms of the GNU Lesser General Public License as published 
* by the Free Software Foundation, either version 3 of the License, or 
* (at your option) any later version. 
*  
* CiviKey is distributed in the hope that it will be useful, 
* but WITHOUT ANY WARRANTY; without even the implied warranty of
* MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the 
* GNU Lesser General Public License for more details. 
* You should have received a copy of the GNU Lesser General Public License 
* along with CiviKey.  If not, see <http://www.gnu.org/licenses/>. 
*  
* Copyright © 2007-2014, 
*     Invenietis <http://www.invenietis.com>,
*     In’Tech INFO <http://www.intechinfo.fr>,
* All rights reserved. 
*-----------------------------------------------------------------------------*/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CK.Core;

namespace CK.Releaser
{
    public class ActivityMonitorListItemClient : ActivityMonitorTextHelperClient
    {
        readonly Dictionary<string,ListViewGroup> _groups;
        Stack<ListViewGroup> _current;
        ListViewGroup _noGroup;

        /// <summary>
        /// Initializes a new ActivityMonitorListItemClient that will cacth logs in ListViewItems.
        /// Its Filter is Debug (the actual filter is guaranted to be debug on the monitor whenever this client is registered in a monitor).
        /// </summary>
        public ActivityMonitorListItemClient()
        {
            Filter = LogFilter.Debug;
            _groups = new Dictionary<string, ListViewGroup>();
            _noGroup = new ListViewGroup( "Logs", "" );
            _current = new Stack<ListViewGroup>();
            _current.Push( _noGroup );
        }

        /// <summary>
        /// Adds captured groups (solution and projects related items in their own group) into the list.
        /// Returns a group of uncategorized items named "Logs" with a blanck header.
        /// </summary>
        /// <param name="l"></param>
        /// <returns></returns>
        public ListViewGroup AddAndReset( ListView l )
        {
            foreach( var g in _groups.Values )
            {
                l.Groups.Add( g );
                foreach( ListViewItem item in g.Items )
                {
                    l.Items.Add( item );
                }
            }
            var noGroup = _noGroup;
            _noGroup = new ListViewGroup( "Logs", "" );
            _groups.Clear();
            _current.Clear();
            _current.Push( _noGroup ); 
            return noGroup;
        }

        protected override void OnContinueOnSameLevel( ActivityMonitorLogData data )
        {
            AddToCurrent( data );
        }

        protected override void OnEnterLevel( ActivityMonitorLogData data )
        {
            AddToCurrent( data );
        }

        void AddToCurrent( ActivityMonitorLogData data )
        {
            LogLevel level = data.MaskedLevel;
            if( level < LogLevel.Info ) return;
            AddSimpleLogEntry( _current.Peek().Items, level, data.Text );
        }

        public static void AddSimpleLogEntry( ListView.ListViewItemCollection items, LogLevel level, string text )
        {
            int imageIndex;
            switch( (level&LogLevel.Mask) )
            {
                case LogLevel.Trace: imageIndex = 0; break;
                case LogLevel.Info: imageIndex = 1; break;
                case LogLevel.Warn: imageIndex = 2; break;
                case LogLevel.Error: imageIndex = 3; break;
                default: imageIndex = 4; break;
            }
            items.Add( text, imageIndex );
        }

        protected override void OnGroupClose( IActivityLogGroup group, IReadOnlyList<ActivityLogGroupConclusion> conclusions )
        {
            if( group.GroupTags.Overlaps( ValidationContext.ContextProcessingTag ) )
            {
                _current.Pop();
            }
        }

        protected override void OnGroupOpen( IActivityLogGroup group )
        {
            if( group.GroupTags.Overlaps( ValidationContext.ContextProcessingTag ) )
            {
                ListViewGroup g;
                if( !_groups.TryGetValue( group.GroupText, out g ) )
                {
                    g = new ListViewGroup( group.GroupText, HorizontalAlignment.Left );
                    _groups.Add( group.GroupText, g );
                }
                _current.Push( g );
            }
        }

        protected override void OnLeaveLevel( LogLevel level )
        {
        }
    }
}
