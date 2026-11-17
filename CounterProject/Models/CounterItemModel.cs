using System.ComponentModel;

namespace CounterProject.Models;

public class CounterItemModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    private string name;
    public string Name
    {
        get { return name; }
        set
        {
            name = value;
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("Name"));
            }
        }
    }

    private int value;
    public int Value
    {
        get { return value; }
        set
        {
            this.value = value;
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("Value"));
            }
        }
    }
}