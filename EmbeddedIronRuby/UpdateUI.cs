using System;

namespace EmbeddedIronRuby
{
    public class UpdateUI
    {
        public delegate void DataChangedEventHandler(object sender, DataChangedEventArgs e);
        public event DataChangedEventHandler DataChanged;

        public UpdateUI()
        {
            DataChanged += Updated;
        }

        void Updated(object sender, DataChangedEventArgs e)
        {
            Console.WriteLine("I've been called (This is C#) " + e.DataFromRuby);
        }

        public virtual void Updated(string data)
        {
            if (DataChanged != null)
            {
                var args = new DataChangedEventArgs();
                args.DataFromRuby = data;
                DataChanged(this, args);
            }
        }
    }

    public class DataChangedEventArgs : EventArgs
    {
        public string DataFromRuby { get; set; }
    }
}