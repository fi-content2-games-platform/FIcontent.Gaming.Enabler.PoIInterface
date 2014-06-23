using System;

namespace PoIInterface.PoI
{
    public partial struct PoIInfo
    {
        private Location _location;
        public Location Location
        {
            get { return _location; }
            set { _location = value; }
        }
                     
        private string _id;
        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private string _shortName;
        public string ShortName
        {
            get { return _shortName; }
            set { _shortName = value; }
        }

        #region Overrides

        public override string ToString()
        {
            return string.Format("{0}:\t{1}", this._id, this._name);
        }

        #endregion

    }
}
