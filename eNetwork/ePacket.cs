using System;
using System.Collections.Generic;

namespace eNetwork
{
    [Serializable]
    public class ePacket
    {

        // Variables
        #region Variables

        public Dictionary<string, object> datas;
        public string Name { get { return name; } }
        private string name;

        #endregion

        // Constructor
        public ePacket(string name)
        {
            this.name = name;
            datas = new Dictionary<string, object>();
        }

    }
}
