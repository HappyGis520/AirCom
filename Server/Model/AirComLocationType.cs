using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace NetPlan.Model
{
    /// <summary>
    /// AirCom站点位置信息
    /// </summary>
    public partial class AirComLocationType 
    {

        private double longitudeField;


        private double latitudeField;

        private bool _LongitudeSpecified = false;


        public double LongitudeField
        {
            get
            {
                return this.longitudeField;
            }
            set
            {
                this.longitudeField = value;
            }
        }
        [XmlAttribute]
        public bool LongitudeSpecified
        {
            get { return _LongitudeSpecified; }
            set { _LongitudeSpecified = value;  }
        }



        public double Latitude
        {
            get
            {
                return this.latitudeField;
            }
            set
            {
                this.latitudeField = value;

            }
        }



    }


    [Serializable]
    public partial class MyLocationTypeType //: object//, System.ComponentModel.INotifyPropertyChanged
    {
        private double longitudeField;

        //private bool longitudeFieldSpecified;

        private double latitudeField;

        //private bool latitudeFieldSpecified;

        private uint ePSGField;

        private bool ePSGFieldSpecified;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute]
        public double Longitude
        {
            get
            {
                return this.longitudeField;
            }
            set
            {
                this.longitudeField = value;
                //this.RaisePropertyChanged("Longitude");
            }
        }

        /// <remarks/>
        //[System.Xml.Serialization.XmlIgnoreAttribute()]
        //public bool LongitudeSpecified
        //{
        //    get
        //    {
        //        return this.longitudeFieldSpecified;
        //    }
        //    set
        //    {
        //        this.longitudeFieldSpecified = value;
        //        this.RaisePropertyChanged("LongitudeSpecified");
        //    }
        //}

        /// <remarks/>

        public double Latitude
        {
            get
            {
                return 120.33333;
            }
            set
            {
                this.latitudeField = value;
                //this.RaisePropertyChanged("Latitude");
            }
        }

        /// <remarks/>
        //[System.Xml.Serialization.XmlIgnoreAttribute()]
        //public bool LatitudeSpecified
        //{
        //    get
        //    {
        //        return this.latitudeFieldSpecified;
        //    }
        //    set
        //    {
        //        this.latitudeFieldSpecified = value;
        //        this.RaisePropertyChanged("LatitudeSpecified");
        //    }
        //}

        /// <remarks/>
        //[System.Xml.Serialization.XmlElement()]
        public uint EPSG
        {
            get
            {
                return this.ePSGField;
            }
            set
            {
                this.ePSGField = value;
                //this.RaisePropertyChanged("EPSG");
            }
        }

        public bool EPSGSpecified
        {
            get
            {
                return this.ePSGFieldSpecified;
            }
            set
            {
                this.ePSGFieldSpecified = value;
                //this.RaisePropertyChanged("EPSGSpecified");
            }
        }

        //public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        //protected void RaisePropertyChanged(string propertyName)
        //{
        //    System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
        //    if ((propertyChanged != null))
        //    {
        //        propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
        //    }
        //}
    }
}
